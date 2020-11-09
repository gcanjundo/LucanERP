using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;
using Dominio.Geral;
using Dominio.Seguranca;

namespace DataAccessLayer.Clinica
{
    public class ConvenioDAO: ConexaoDB 
    {
         

        public ConvenioDTO Adicionar(ConvenioDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONVENIO_ADICIONAR";


                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("DESCONTO", dto.ValorParceiro);
                AddParameter("ENTIDADE", dto.Entidade);
                AddParameter("TIPO", dto.Sigla);
                AddParameter("VALOR_UTENTE", dto.ValorUtente);
                AddParameter("VALIDADE", dto.Validade);
                AddParameter("PRECO_PROPOSTO", dto.PrecoProposto);
                AddParameter("PERCENTUAL_PROPOSTO", dto.PercentualProposto); 
                AddParameter("VALOR_ACORDADO", dto.ValorAcordado);
                AddParameter("UTILIZADOR", dto.Utilizador);


                dto.Codigo = ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

                return dto;
        }

        public ConvenioDTO Alterar(ConvenioDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONVENIO_ALTERAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("DESCONTO", dto.ValorParceiro);
                AddParameter("ENTIDADE", dto.Entidade);
                AddParameter("TIPO", dto.Sigla);
                AddParameter("VALOR_UTENTE", dto.ValorUtente);
                AddParameter("VALIDADE", dto.Validade); 
                AddParameter("PRECO_PROPOSTO", dto.PrecoProposto);
                AddParameter("PERCENTUAL_PROPOSTO", dto.PercentualProposto);
                AddParameter("VALOR_ACORDADO", dto.ValorAcordado);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("CODIGO", dto.Codigo); 

               ExecuteNonQuery();
               dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public ConvenioDTO Eliminar(ConvenioDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONVENIO_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("UTILIZADOR", dto.Utilizador);

                dto.Codigo = ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<ConvenioDTO> ObterPorFiltro(ConvenioDTO dto)
        {
            List<ConvenioDTO> listaSeguroSaudes = new List<ConvenioDTO>();
            try
            {
                ComandText = "stp_CLI_CONVENIO_OBTERPORFILTRO";

                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@SEGURADORA", dto.Entidade);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ConvenioDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.ValorParceiro = Convert.ToDecimal(dr[2].ToString());
                    dto.Estado = int.Parse(dr[3].ToString());

                    if (dr[4].ToString() != null && !dr[4].ToString().Equals(""))
                    {
                        dto.Entidade = int.Parse(dr[4].ToString());
                        dto.DesignacaoEntidade = dr[15].ToString();
                        dto.SocialName = dr[16].ToString();
                    }
                    else
                    {
                        dto.Entidade = -1;
                        dto.DesignacaoEntidade = "PARTICULAR";
                        dto.SocialName = "PARTICULAR";
                    }

                    dto.Sigla = dr[5].ToString();
                    dto.ValorUtente = Convert.ToDecimal(dr[6].ToString());
                    dto.PrecoProposto = Convert.ToDecimal(dr[7].ToString() == "" ? "0" : dr[7].ToString());
                    dto.PercentualProposto = Convert.ToDecimal(dr[8].ToString() == "" ? "0" : dr[8].ToString());
                    dto.ValorAcordado = Convert.ToDecimal(dr[9].ToString() == "" ? "0" : dr[9].ToString());
                    dto.Validade = dr[10].ToString() != "" ? DateTime.Parse(dr[10].ToString()) : DateTime.MinValue;

                    listaSeguroSaudes.Add(dto);
                }

            }
            catch (Exception ex)
            { 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", ""); 
            }
            finally
            {
                FecharConexao();
            }

            return listaSeguroSaudes;
        }

        public ConvenioDTO ObterPorPK(ConvenioDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONVENIO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new ConvenioDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.ValorParceiro = Convert.ToDecimal(dr[2].ToString());
                    dto.Estado = int.Parse(dr[3].ToString());

                    if (dr[4].ToString() != null && !dr[4].ToString().Equals(""))
                    {
                        dto.Entidade = int.Parse(dr[4].ToString());
                        dto.DesignacaoEntidade = dr[8].ToString();
                        dto.SocialName = dr[9].ToString();
                    }
                    else
                    {
                        dto.DesignacaoEntidade = "PARTICULAR";
                        dto.SocialName = "PARTICULAR";
                    }

                    dto.Sigla = dr[5].ToString();
                    dto.ValorUtente = Convert.ToDecimal(dr[6].ToString());
                    dto.PrecoProposto = Convert.ToDecimal(dr[7].ToString() == "" ? "0" : dr[7].ToString());
                    dto.PercentualProposto = Convert.ToDecimal(dr[8].ToString() == "" ? "0" : dr[8].ToString());
                    dto.ValorAcordado = Convert.ToDecimal(dr[9].ToString() == "" ? "0" : dr[9].ToString());
                    dto.Validade = dr[10].ToString() != "" ? DateTime.Parse(dr[10].ToString()) : DateTime.MinValue;
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<EmpresaDTO> Empresas() 
        {
            List<EmpresaDTO> es;

            try
            {
                 ComandText = "stp_CLI_CONVENIO_EMPRESAS";

                MySqlDataReader dr =ExecuteReader();
                es = new List<EmpresaDTO>();
                while (dr.Read())
                {
                    EmpresaDTO dto = new EmpresaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.NomeCompleto = dr[1].ToString(); 

                    es.Add(dto);
                }


            }
            catch (Exception ex)
            {
                EmpresaDTO dto = new EmpresaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                es = new List<EmpresaDTO>();
                es.Add(dto);

            }
            finally
            {

               FecharConexao();
            }

            return es;
        }
    }
}
