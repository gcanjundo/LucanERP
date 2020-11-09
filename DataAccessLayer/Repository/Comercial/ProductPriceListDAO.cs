
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial
{
    public class ProductPriceListDAO
    {

        readonly ConexaoDB BaseDados;

        public ProductPriceListDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public ProductPriceListDTO Adicionar(ProductPriceListDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_ARTIGO_TABELA_PRECO_ADICIONAR";

                BaseDados.AddParameter("@PRODUCT_ID", dto.Codigo);
                BaseDados.AddParameter("@TABELA_ID", dto.PriceTableID);
                BaseDados.AddParameter("@PRECO", dto.PrecoVenda);
                BaseDados.AddParameter("@IMPOSTO_ID", dto.ImpostoID <= 0 ? (object)DBNull.Value : dto.ImpostoID); 
                BaseDados.AddParameter("@IMPOSTO", dto.PercentualImposto);
                BaseDados.AddParameter("@INCLUIDO", dto.ImpostoIncluido);
                BaseDados.AddParameter("@IMPOSTO_LIQUIDO", dto.ImpostoLiquido);
                BaseDados.AddParameter("@UNIDADE_ID", dto.UnidadeVenda == "-1" || dto.UnidadeVenda == "" ? (object)DBNull.Value : dto.UnidadeVenda);
                BaseDados.AddParameter("@QTD_UN", dto.QtdUndVenda);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador); 
                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public List<ProductPriceListDTO> ObterPorFiltro(ProductPriceListDTO dto)
        {
            List<ProductPriceListDTO> lista = new List<ProductPriceListDTO>();

            try
            {
                BaseDados.ComandText = "stp_COM_ARTIGO_TABELA_PRECO_OBTERPORFILTRO";

                BaseDados.AddParameter("@PRODUCT_ID", dto.Codigo);
                BaseDados.AddParameter("@TABELA_ID", dto.PriceTableID);
                BaseDados.AddParameter("@CATEGORIA_ID", string.IsNullOrEmpty(dto.Categoria) ? "-1" : dto.Categoria);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ProductPriceListDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.CodigoBarras = dr[1].ToString();
                    dto.Referencia = dr[2].ToString();
                    dto.Designacao = dr[3].ToString();
                    dto.PriceTableID = int.Parse(dr[4].ToString());
                    dto.TablePriceDesignation = dr[5].ToString();
                    dto.PrecoVenda = decimal.Parse(dr[6].ToString() ?? "0");
                    dto.PercentualImposto = dr[7].ToString() != "" ? decimal.Parse(dr[7].ToString()) : 0;
                    dto.ImpostoLiquido = dr[8].ToString() != "" ? decimal.Parse(dr[8].ToString()) : 0;
                    dto.DefaultPrice = dr[9].ToString() == "1" ? true : false;
                    dto.ImpostoID = int.Parse(dr[10].ToString());
                    dto.UnidadeVenda = dr[11].ToString();
                    dto.IncomeQuatity = dr[12].ToString() != "" ? decimal.Parse(dr[12].ToString()) : 0;
                    dto.ImpostoIncluido = dr[13].ToString() != "1" ? (short)0 : (short)1; 
                    dto.Preco = dto.PrecoVenda;

                    if (dto.ImpostoID > 0)
                    {
                        dto.ImpostoLiquido = dto.PrecoVenda * (dto.PercentualImposto / 100);

                        if(dto.ImpostoIncluido == 1)
                        {
                            dto.PVPWithTax = dto.PrecoVenda + dto.ImpostoLiquido; 
                        }
                    }
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new ProductPriceListDTO
                {
                    MensagemErro = ex.Message
                };
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public ProductPriceListDTO Excluir(ProductPriceListDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_ARTIGO_TABELA_PRECO_EXCLUIR";

                BaseDados.AddParameter("@PRODUCT_ID", dto.Codigo);

                BaseDados.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }
    }
}
