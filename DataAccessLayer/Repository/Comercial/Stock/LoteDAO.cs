using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
 

namespace DataAccessLayer.Comercial.Stock
{
    public class LoteDAO: ConexaoDB
    {
        public LoteDTO Adicionar(LoteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_LOTE_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@CODIGO_BARRAS", dto.CodigoBarras);
                AddParameter("@ARTIGO_ID", dto.ProductID);
                AddParameter("@STATUS_ID", dto.ProductStatus);
                /*AddParameter("@EXISTENCIA", dto.StockData.Actual);
                AddParameter("@RECEPCAO", dto.StockData.ContagemFisica);
                AddParameter("@ENTRADA", dto.StockData.Maxima);
                AddParameter("@SAIDA", dto.StockData.Minima);*/
                AddParameter("@FABRICO", dto.DataFabrico);
                AddParameter("@VALIDADE", dto.DataValidade);
                AddParameter("@LIMITE", dto.DataLimiteFaturacao);
                AddParameter("@SUPPLIER_ID", dto.Fornecedor =="-1" ? (object)DBNull.Value : dto.Fornecedor); 
                AddParameter("@INCOME", dto.IncomeUnit == -1 ? (object)DBNull.Value : dto.IncomeUnit);
                AddParameter("@OUTCOME", dto.OutComeUnit == -1 ? (object)DBNull.Value : dto.OutComeUnit);
                AddParameter("@STOCK", dto.ReferenceUnit == -1 ? (object)DBNull.Value : dto.ReferenceUnit);
                AddParameter("@SALE", dto.UnidadeVenda == "-1" ? (object)DBNull.Value : dto.UnidadeVenda);
                AddParameter("@PURCHAGE", dto.UnidadeCompra == "-1" ? (object)DBNull.Value : dto.UnidadeCompra);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                ExecuteNonQuery();
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

        public List<LoteDTO> ObterPorFiltro(LoteDTO dto)
        {
            List<LoteDTO> lista = new List<LoteDTO>();
            
            try
            {
                ComandText = "stp_GER_ARTIGO_LOTE_OBTERPORFILTRO";

                AddParameter("@ARTIGO_ID", dto.ProductID);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@ARMAZEM_ID", dto.ArmazemID);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new LoteDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Referencia = dr[1].ToString();
                    dto.CodigoBarras = dr[2].ToString(); 
                    dto.ProductID = int.Parse(dr[3].ToString());
                    dto.DataFabrico = DateTime.Parse(dr[10].ToString());
                    dto.DataValidade = DateTime.Parse(dr[11].ToString());
                    dto.DataLimiteFaturacao = DateTime.Parse(dr[12].ToString());
                    /*dto.Armazem = dr[6].ToString();
                    dto.ArmazemID = int.Parse(dr[7].ToString());
                    dto.Quantidade = decimal.Parse(dr[8].ToString());*/

                    dto.IncomeUnit = int.Parse(dr[16].ToString() == "" ? "-1" : dr[16].ToString());
                    dto.OutComeUnit = int.Parse(dr[17].ToString() == "" ? "-1" : dr[17].ToString());
                    dto.ReferenceUnit = int.Parse(dr[18].ToString() == "" ? "-1" : dr[18].ToString());
                    dto.UnidadeVenda = dr[19].ToString() == "" ? "-1" : dr[19].ToString();
                    dto.UnidadeCompra = dr[20].ToString() == "" ? "-1" : dr[20].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new LoteDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public LoteDTO Excluir(LoteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_LOTE_EXCLUIR";

                AddParameter("@ARTIGO", dto.Codigo);

                ExecuteNonQuery();

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
    
    }
}
