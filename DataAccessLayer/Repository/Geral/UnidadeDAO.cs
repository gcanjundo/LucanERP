using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class UnidadeDAO:ConexaoDB 
    {
         

        public UnidadeDTO Adicionar(UnidadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_UNIDADE_MEDICAO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("QUANTIDADE", dto.Quantidade);
                AddParameter("FACTOR", dto.FactorConversao);

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

        public UnidadeDTO Alterar(UnidadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_UNIDADE_MEDICAO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("QUANTIDADE", dto.Quantidade);
                AddParameter("FACTOR", dto.FactorConversao);

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

        public UnidadeDTO Eliminar(UnidadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_UNIDADE_MEDICAO_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

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

        public List<UnidadeDTO> ObterPorFiltro(UnidadeDTO dto)
        {
            List<UnidadeDTO> lista = new List<UnidadeDTO>();
            try
            {
                ComandText = "stp_GER_UNIDADE_MEDICAO_OBTERPORFILTRO";

                AddParameter("@DESCRICAO", dto.Descricao ?? string.Empty); 
                

                MySqlDataReader dr = ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new UnidadeDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString().ToUpper();
                   dto.Estado = int.Parse(dr[3].ToString());
                   dto.Quantidade = decimal.Parse(dr[4].ToString() == "" ? "1" : dr[4].ToString());
                   dto.FactorConversao = dr[5].ToString();

                   lista.Add(dto);
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

            return lista;
        }

        public UnidadeDTO ObterPorPK(UnidadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_UNIDADE_MEDICAO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new UnidadeDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Quantidade = decimal.Parse(dr[4].ToString() == "" ? "1" : dr[4].ToString());
                    dto.FactorConversao = dr[5].ToString();

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

        public List<UnidadeDTO> ObterLista()
        {
            List<UnidadeDTO> lista = new List<UnidadeDTO>();
            UnidadeDTO dto;
            try
            {
                ComandText = "stp_GER_UNIDADE_MEDICAO_OBTERPORFILTRO";

                AddParameter("@DESCRICAO", string.Empty);


                MySqlDataReader dr = ExecuteReader();



                while (dr.Read())
                {
                    dto = new UnidadeDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Quantidade = decimal.Parse(dr[4].ToString() == "" ? "0" : dr[4].ToString());

                    dto.Descricao = dto.Sigla + " - " + dto.Descricao + " " + dto.Quantidade + " Unidades";
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new UnidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Descricao = dto.MensagemErro;
                lista = new List<UnidadeDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }
    }
}
