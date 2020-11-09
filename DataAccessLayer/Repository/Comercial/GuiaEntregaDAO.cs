using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial
{
    public class GuiaEntregaDAO : ConexaoDB
    {

        public GuiaEntregaDTO Adicionar(GuiaEntregaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@DATA_GUIA", dto.Emissao == DateTime.MinValue ? DateTime.Today : dto.Emissao);
                AddParameter("@DATA_ENTREGA", dto.DataDescarga);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SERIE", dto.Serie); 
                AddParameter("@ORIGEM", dto.DocumentoOrigem);
                AddParameter("@FUNCIONARIO", dto.DeliveryMan);
                AddParameter("@MOTORISTA", dto.ReceptorCarga ?? (object)DBNull.Value);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@CLIENTE", dto.Entidade);

                dto.Codigo = ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();

            }

            return dto;
        }

        

        public GuiaEntregaDTO Anular(GuiaEntregaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_ANULAR";

                AddParameter("@CODIGO", dto.Codigo); 
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@MOTIVO", dto.MotivoAnulacao);

                dto.Codigo = ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();

            }

            return dto;
        }

        public void SalvarItem(ItemFaturacaoDTO item)
        {
            GuiaItemDTO dto;
            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_ITEM_ADICIONAR";

                AddParameter("@GUIA_ID", item.Codigo);
                AddParameter("@ARTIGO", item.Artigo);
                AddParameter("@QUANTIDADE", item.QuantidadeSatisfeita);
                AddParameter("@ROLOS", item.QuantidadeRolo);
                AddParameter("@POR_ROLO", item.QuantidadeReservada);

                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto = new GuiaItemDTO
                {
                    MensagemErro = ex.Message.Replace("'", string.Empty)
                }; 
                 
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<GuiaEntregaDTO> ObterGuiasPorFiltro(GuiaEntregaDTO dto)
        {
            var lista = new List<GuiaEntregaDTO>();

            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_OBTERPORFILTRO";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@DATA_INI", dto.EmissaoIni == DateTime.MinValue ? (object)DBNull.Value : dto.EmissaoIni);
                AddParameter("@DATA_TERM", dto.EmissaoTerm == DateTime.MinValue ? (object)DBNull.Value : dto.EmissaoTerm);
                AddParameter("@FUNCIONARIO_ID", dto.FuncionarioID);
                AddParameter("@SERIE", dto.Serie??-1);
                AddParameter("@CUSTOMER_ID", dto.Entidade);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new GuiaEntregaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Emissao = DateTime.Parse(dr[1].ToString());
                    dto.DataEntrega = DateTime.Parse(dr[2].ToString());
                    dto.NomeEntidade = dr[19].ToString();
                    dto.FuncionarioID = dr[21].ToString(); 
                    dto.Referencia = dto.Codigo + "/" + dto.Emissao.Year;
                    lista.Add(dto);
                }

                
            }
            catch (Exception ex)
            {
                dto = new GuiaEntregaDTO
                {
                    MensagemErro = ex.Message.Replace("'", string.Empty)
                };

            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public void SalvarItems(ItemFaturacaoDTO item)
        {
            GuiaItemDTO dto;
            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_ITEMS_ADICIONAR";

                AddParameter("@ARTIGO", item.Artigo); 
                AddParameter("@DELIVERMAN", item.DesignacaoEntidade);
                AddParameter("@DATA_ENTREGA", item.DataEntrega);
                AddParameter("@UTILIZADOR", item.Utilizador);
                AddParameter("@DOCUMENT_ID", item.Fatura);
                AddParameter("@QUANTIDADE", item.QuantidadeSatisfeita); 
                AddParameter("@ITEM_CODIGO", item.Codigo); 
                AddParameter("@GUIA_ID", item.DocOrigemID); 

                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto = new GuiaItemDTO
                {
                    MensagemErro = ex.Message.Replace("'", string.Empty)
                };

            }
            finally
            {
                FecharConexao();
            }
        }

        public List<GuiaItemDTO> ObterGuiaItemsList(GuiaEntregaDTO dto)
        {
            List<GuiaItemDTO> lista = new List<GuiaItemDTO>();
            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_TOPRINT"; 
                
                AddParameter("@CODIGO", dto.Codigo);  

                MySqlDataReader dr = ExecuteReader(); 

                while (dr.Read())
                {
                    var item = new GuiaItemDTO
                    {
                        Referencia = dr[0].ToString(),
                        Designacao = dr[1].ToString(),
                        Marca = "",
                        Substracto = dr[3].ToString(),
                        QuantidadeRolo = decimal.Parse(dr[4].ToString()),
                        QuantidadeReservada = !string.IsNullOrEmpty(dr[5].ToString()) ? decimal.Parse(dr[5].ToString()) : 0,
                        Quantidade = decimal.Parse(dr[6].ToString()),
                        DescontoNumerario = !string.IsNullOrEmpty(dr[7].ToString()) ? decimal.Parse(dr[7].ToString()) : 0, // Excesso
                        QuantidadeSatisfeita = !string.IsNullOrEmpty(dr[8].ToString()) ? decimal.Parse(dr[8].ToString()) : 0,
                        DesignacaoEntidade = dr[9].ToString(),
                        SocialName = dr[10].ToString(),
                        LookupField1 = dr["FAT_REFERENCIA"].ToString(),
                        DataEntrada = !string.IsNullOrEmpty(dr["FAT_DATA_EMISSAO"].ToString()) ? DateTime.Parse(dr["FAT_DATA_EMISSAO"].ToString()) : DateTime.MinValue,
                        DataEntrega = !string.IsNullOrEmpty(dr["GUIA_DATA_ENTREGA"].ToString()) ? DateTime.Parse(dr["GUIA_DATA_ENTREGA"].ToString()) : DateTime.MinValue,
                        FuncionarioID = dr["ENT_NOME_COMPLETO"].ToString(),
                        CreatedDate = DateTime.Parse(dr["GUIA_UPDATED_DATE"].ToString()),
                        Notas = dr["FAT_ITEM_COMENTARIOS"].ToString()
                    };
                    item.DescontoNumerario = item.DescontoNumerario < 0 ? 0 : item.DescontoNumerario;
                    lista.Add(item);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<GuiaItemDTO> ObterItemDeliveryExtractList(GuiaItemDTO dto)
        {
            List<GuiaItemDTO> lista = new List<GuiaItemDTO>();
            try
            {
                ComandText = "stp_COM_GUIA_ENTREGA_EXTRACT_LIST";

                AddParameter("@CUSTOMER_ID", dto.Entidade);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    GuiaItemDTO item = new GuiaItemDTO
                    {
                        Referencia = dr[0].ToString(),
                        Designacao = dr[1].ToString(),
                        Marca = "",
                        Substracto = dr[3].ToString(),
                        QuantidadeRolo = decimal.Parse(dr[4].ToString()),
                        QuantidadeReservada = !string.IsNullOrEmpty(dr[5].ToString()) ? decimal.Parse(dr[5].ToString()) : 0,
                        Quantidade = decimal.Parse(dr[6].ToString()),
                        DescontoNumerario = !string.IsNullOrEmpty(dr[7].ToString()) ? decimal.Parse(dr[7].ToString()) : 0, // Excesso
                        QuantidadeSatisfeita = !string.IsNullOrEmpty(dr[8].ToString()) ? decimal.Parse(dr[8].ToString()) : 0,
                        DesignacaoEntidade = dr[9].ToString(),
                        SocialName = dr[10].ToString(),
                        LookupField1 = dr["FAT_REFERENCIA"].ToString(),
                        DataEntrada = !string.IsNullOrEmpty(dr["FAT_DATA_EMISSAO"].ToString()) ? DateTime.Parse(dr["FAT_DATA_EMISSAO"].ToString()) : DateTime.MinValue,
                        DataEntrega = !string.IsNullOrEmpty(dr["GUIA_DATA_ENTREGA"].ToString()) ? DateTime.Parse(dr["GUIA_DATA_ENTREGA"].ToString()) : DateTime.MinValue,
                        FuncionarioID = dr["ENT_NOME_COMPLETO"].ToString(),
                        Entidade = int.Parse(dr["FAT_CODIGO_CLIENTE"].ToString())
                    };
                    item.DescontoNumerario = item.DescontoNumerario < 0 ? 0 : item.DescontoNumerario;
                    lista.Add(item);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

    }
}
