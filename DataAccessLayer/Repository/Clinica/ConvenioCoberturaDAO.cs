using Dominio.Clinica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Clinica
{
    public class ConvenioCoberturaDAO : ConexaoDB 
    {
        public ConvenioCoberturaItemDTO Adicionar(ConvenioCoberturaItemDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_PLANO_CONVENIO_COBERTURA_ADICIONAR"; 
                 
                AddParameter("CONVENIO", dto.ConvenioID);
                AddParameter("SERVICO", dto.Artigo);
                AddParameter("VALOR_PARCEIRO", dto.ValorParceiro);
                AddParameter("VALOR_UTENTE", dto.ValorUtente);
                AddParameter("ACTIVO", dto.Status); 
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("PRECO_PUBLICO", dto.PrecoVendaPublico);
                AddParameter("PRECO_PROPOSTO", dto.PrecoProposto == 0 ? dto.PrecoVendaPublico : dto.PrecoProposto);
                AddParameter("PRECO_ACORDADO", dto.PrecoAcordado);
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

        public ConvenioCoberturaItemDTO Eliminar(ConvenioCoberturaItemDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_PLANO_CONVENIO_COBERTURA_EXCLUIR";

                AddParameter("CONVENIO", dto.ConvenioID);
                AddParameter("SERVICO", dto.Artigo);
                AddParameter("UTILIZADOR", dto.Utilizador);

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


        public List<ConvenioCoberturaItemDTO> ObterPorFiltro(ConvenioCoberturaItemDTO dto)
        {
            List<ConvenioCoberturaItemDTO> lista = new List<ConvenioCoberturaItemDTO>();

            try
            {
                ComandText = "stp_CLI_PLANO_CONVENIO_COBERTURA_OBTERPORFILTRO";

                AddParameter("@CONVENIO", dto.ConvenioID); 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ConvenioCoberturaItemDTO
                    {
                        ConvenioID = int.Parse(dr[0].ToString()),
                        Artigo = int.Parse(dr[1].ToString()),
                        ValorParceiro = decimal.Parse(dr[2].ToString()),
                        ValorUtente = decimal.Parse(dr[3].ToString()),
                        Status = int.Parse(dr[4].ToString()),
                        ItemDesignation = dr[12].ToString(),
                        AggrementDesignation = dr[14].ToString(),

                        Convenio = new ConvenioDTO
                        {
                            Codigo = int.Parse(dr[13].ToString()),
                            Descricao = dr[14].ToString(),
                            ValorParceiro = Convert.ToDecimal(dr[15].ToString()),
                            Estado = int.Parse(dr[16].ToString()),
                            Entidade = int.Parse(dr[17].ToString()),
                            Sigla = dr[18].ToString(),
                            ValorUtente = Convert.ToDecimal(dr[19].ToString()),
                            Validade = dr[20].ToString() != "" ? DateTime.Parse(dr[20].ToString()) : DateTime.MinValue,
                        }
                    };

                    dto.PrecoVendaPublico = decimal.Parse(dr[9].ToString());
                    dto.PrecoProposto = decimal.Parse(dr[10].ToString()) == 0 ? dto.PrecoVendaPublico : dto.PrecoProposto;
                    dto.PrecoAcordado = decimal.Parse(dr[11].ToString());

                    if (dto.ValorParceiro ==0 && dto.ValorUtente == 0 && dto.Convenio.ValorParceiro > 0)
                    {
                        if (dto.PrecoAcordado > 0)
                        {
                            dto.ValorParceiro = (dto.PrecoAcordado * dto.Convenio.ValorParceiro) / 100;
                            dto.ValorUtente = dto.PrecoAcordado - dto.ValorParceiro;
                        }
                        else 
                        {
                            dto.ValorParceiro = (dto.PrecoProposto * dto.Convenio.ValorParceiro) / 100;
                            dto.ValorUtente = dto.PrecoProposto - dto.ValorParceiro;
                        }
                    }

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
    }
}
