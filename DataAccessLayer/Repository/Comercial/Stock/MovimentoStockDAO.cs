using Dominio.Comercial;
using Dominio.Comercial.Stock;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial.Stock
{
   public class MovimentoStockDAO:ConexaoDB
    {
       public MovimentoStockDTO Adicionar(MovimentoStockDTO dto)
       {
           try
           {
               ComandText = "stp_STOCK_MOVIMENTO_DIRECTOS_ADICIONAR";
               AddParameter("@CODIGO", dto.Codigo);
               AddParameter("@DOCUMENTO", dto.Documento);
               AddParameter("@REFERENCIA", dto.Referencia);
               AddParameter("@OPERACAO", dto.Operacao);
               AddParameter("@NUMERACAO", dto.Numeracao);
               AddParameter("@DATA_STOCK", dto.DataStock);
               AddParameter("@LANCAMENTO", dto.Lancamento);
               AddParameter("@ARMAZEM_FROM", dto.ArmazemFrom);
               AddParameter("@ARMAZEM_TO", dto.ArmazemTo == 0 ? -1 : dto.ArmazemTo);
               AddParameter("@USER_UPDATE", dto.Utilizador);
               AddParameter("@USER_CREATE", dto.Utilizador);
               AddParameter("@UPDATE_DATE", DateTime.Today);
               AddParameter("@SERIE", dto.Serie);
               AddParameter("@FUNCIONARIO", dto.FuncionarioID);
               AddParameter("@DOCUMENT_ID", dto.DocumentID == null ? -1 : dto.DocumentID);
               AddParameter("@ENTITY_ID", dto.EntityID == null ? -1 : dto.EntityID);
               AddParameter("@ARMAZEM_ID", dto.ArmazemID==0 ? dto.ArmazemFrom : dto.ArmazemID);
               AddParameter("@TRANSFER_ID", dto.TranferenciaID == null ? -1 : dto.TranferenciaID);
               AddParameter("@DOCUMENT_FROM", dto.DocumentTypeFromID == null ? -1 : dto.DocumentTypeFromID);
               AddParameter("@MOTIVO_ID", dto.MotivoID <=0  ? (object)DBNull.Value : dto.MotivoID);

                dto.Codigo = ExecuteInsert();
               dto.Sucesso = true;
           }
           catch (Exception ex)
           {
               dto.MensagemErro += ex.Message.Replace("'", string.Empty);
               dto.Sucesso = false;
           }
           finally
           {
               FecharConexao();
           }
           return dto;
       }

        public MovimentoStockDTO Excluir(MovimentoStockDTO dto)
        {
            try
            {
                ComandText = "stp_STOCK_MOVIMENTO_DIRECTOS_ANULAR";
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador); 
                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro += ex.Message.Replace("'", string.Empty);
                dto.Sucesso = false;
            }
            finally
            {
                FecharConexao();
            }
            return dto;
        }

       public ItemMovimentoStockDTO AdicionarArtigo(ItemMovimentoStockDTO dto)
       {
            try
            {
                ComandText = "stp_STOCK_MOVIMENTO_DIRECTO_ARTIGO_ADICIONAR";

                AddParameter("@MOVIMENTO", dto.Movimento);
                AddParameter("@ARTIGO", dto.ArtigoID);
                AddParameter("@DATA_MOVIMENTO", DateTime.Today);
                AddParameter("@EXISTENCIA", dto.Existencia);
                AddParameter("ARMAZEM_ORIGEM", dto.ArmazemOrigem);
                AddParameter("ARMAZEM_DESTINO", dto.AramzemDestino);
                AddParameter("@QUANTIDADE", dto.Quantidade);
                AddParameter("@QTD_FINAL", dto.Existencia < 0 && dto.Quantidade > 0 ? dto.Quantidade : dto.Existencia + dto.Quantidade);
                AddParameter("@PRECO_UNITARIO", dto.PrecoUnitario);
                AddParameter("@TIPO", dto.Operacao); 
                AddParameter("@PRECO_CUSTO", dto.PrecoCompra);
                AddParameter("@VALOR_PVP", dto.Quantidade * dto.PrecoUnitario);
                AddParameter("@VALOR_PC", dto.Quantidade * dto.PrecoCompra);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@STATUS", dto.Status);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@LOTE_ID", dto.LoteID <= 0 ? (object)DBNull.Value : dto.LoteID);
                AddParameter("@SERIAL_NUMBER_ID", dto.SerialNumberID <= 0 ? (object)DBNull.Value : dto.SerialNumberID);
                AddParameter("@COMPOSE_ID", dto.ComposeID <= 0 ? (object)DBNull.Value : dto.ComposeID);
                AddParameter("@SIZE_ID", dto.DimensaoID <= 0 ? (object)DBNull.Value : dto.DimensaoID);
                AddParameter("@DATA_CONTAGEM", dto.DataContagem == DateTime.MinValue ? (object)DBNull.Value : dto.DataContagem);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                FecharConexao();
                if (dto.Sucesso && dto.HeightSize > 0 && dto.WidthSize > 0)
                    AddProductSizes(dto);
            }
           return dto;
       }

        private void AddProductSizes(ItemMovimentoStockDTO dto)
        {
            try
            {
                ComandText = "stp_STOCK_MOVIMENTOS_DIRECTOS_ARTIGOS_SIZES_ADICIONAR";

                AddParameter("@MOVIMENT_ID", dto.Movimento);
                AddParameter("@PRODUCT_ID", dto.ArtigoID);
                AddParameter("@HIGHT_SIZE", dto.HeightSize);
                AddParameter("@WIDTH_SIZE", dto.WidthSize);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                FecharConexao();
            }
        }

       public ItemMovimentoStockDTO MovimentaStock(ItemMovimentoStockDTO dto, MovimentoStockDTO mov)
       {
           try
           {
               ComandText = "stp_STOCK_MOVIMENTO_STOCK";
               AddParameter("@ARTIGO", dto.ArtigoID);
               AddParameter("@ARMAZEM", mov.ArmazemFrom);
               AddParameter("@ARMAZEM_TO", mov.ArmazemTo);
               AddParameter("@ENTRADA", DateTime.Today);
               AddParameter("@SAIDA", DateTime.Today);
               if (dto.Operacao == 1)
               {
                   AddParameter("@QUANTIDADE", dto.Existencia + dto.Quantidade);
                   AddParameter("@QUANTIDADEE", dto.Quantidade);
               }
               else
               {
                   if (dto.Operacao == 2)
                   {
                       AddParameter("@QUANTIDADE", dto.Existencia - dto.Quantidade);
                       AddParameter("@QUANTIDADEE", dto.Quantidade);
                   }
                   /*
                   if (dto.Operacao == "TA")
                   {
                       AddParameter("@QUANTIDADE", dto.Existencia - dto.Quantidade);
                       AddParameter("@QUANTIDADEE", dto.Quantidade);
                   }*/
               }
               AddParameter("@PRECO_UNITARIO", dto.PrecoCompra);
               AddParameter("@ULTIMO_PRECO", dto.PrecoUnitario);
               AddParameter("@CUSTO_MEDIO", dto.Novo_PrecoCustoMedio);
               AddParameter("@OPERACAO", mov.Operacao);
               ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               dto.MensagemErro += ex.Message.Replace("'", string.Empty);
           }
           finally
           {
               FecharConexao();
           }
           return dto;
       }

       public ItemMovimentoStockDTO MovimentaArtigo(ItemMovimentoStockDTO dto)
       {
           try
           {
               ComandText = "stp_STOCK_MOVIMENTO_ARTIGO";
               AddParameter("@ARTIGO", dto.ArtigoID);
               AddParameter("@PRECO_CUSTO", dto.PrecoCompra);
               AddParameter("@PRECO_VENDA", dto.TotalLiquido);
               //AddParameter("@MARGEM_LUCRO", dto.MargemLucro);
               ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               dto.MensagemErro += ex.Message.Replace("'", string.Empty);
           }
           finally
           {
               FecharConexao();
           }
           return dto;
       }

        

       public SerieDTO ObterSerieStock(SerieDTO dto)
       {
           try
           {
               ComandText = "stp_STOCK_MOVIMENTO_DIRECTO_OBTERSERIE";
               AddParameter("@DOCUMENTO", dto.Codigo);
               AddParameter("@TERMINO", DateTime.Today);

               MySqlDataReader dr = ExecuteReader();
               dto = new SerieDTO();
               if (dr.Read())
               {
                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Ano = int.Parse(dr[1].ToString());
                   dto.Numeracao = int.Parse(dr[2].ToString());

                   dr.Close();
               }
           }
           catch (Exception ex)
           {
               dto.Sucesso = false;
               dto.MensagemErro += ex.Message.Replace("'", "");
           }
           finally
           {
               FecharConexao();
           }
           return dto;
       }

        public void RecalculaStocks()
        {
            throw new NotImplementedException();
        }

        public MovimentoStockDTO ObterPorPK(MovimentoStockDTO dto)
       {
           try
           {
               ComandText = "stp_STOCK_MOVIMENTO_DIRECTO_OBTERPORPK";
               AddParameter("@CODIGO", dto.Codigo);

               MySqlDataReader dr = ExecuteReader();

               dto = new MovimentoStockDTO();
               if (dr.Read())
               {
                   dto.Codigo = int.Parse(dr["MOV_CODIGO"].ToString());
                   dto.Documento = int.Parse(dr["MOV_CODIGO_DOCUMENTO"].ToString());
                   dto.Referencia = dr["MOV_REFERENCIA"].ToString();
                   dto.Numeracao = int.Parse(dr["MOV_NUMERACAO"].ToString());
                   dto.DataStock = DateTime.Parse(dr["MOV_DATA_STOCK"].ToString());
                   dto.Lancamento = DateTime.Parse(dr["MOV_DATA_LANCAMENTO"].ToString());
                   dto.ArmazemFrom = int.Parse(dr["MOV_CODIGO_ARMAZEM_FROM"].ToString() == null ? "-1" : dr["MOV_CODIGO_ARMAZEM_FROM"].ToString());
                   dto.ArmazemTo = int.Parse(dr["MOV_CODIGO_ARMAZEM_TO"].ToString() == null ? "-1" : dr["MOV_CODIGO_ARMAZEM_TO"].ToString());
                   dto.FuncionarioID = dr["MOV_FUNCIONARIO"].ToString();

                   dr.Close();
               }
           }
           catch (Exception ex)
           {
               dto.Sucesso = false;
               dto.MensagemErro = ex.Message.Replace("'", string.Empty);
           }
           finally
           {
               FecharConexao();
           }
           return dto;
       }

       public List<MovimentoStockDTO> ObterPorFiltro(MovimentoStockDTO dto)
       {
           List<MovimentoStockDTO> lista= new List<MovimentoStockDTO>();
           try
           {

               ComandText = "stp_STOCK_MOVIMENTO_DIRECTO_OBTERPORFILTRO";

               AddParameter("@REFERENCIA", dto.Referencia);
               AddParameter("@INICIO", dto.DataStock);
               AddParameter("@TERMINO", dto.Lancamento);
               AddParameter("@ARMAZEM", dto.ArmazemFrom);
               AddParameter("@INVOICE_ID", dto.DocumentoFrom);

                MySqlDataReader dr = ExecuteReader();
               
               while(dr.Read())
               {
                   dto = new MovimentoStockDTO();
                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.NomeDocumento = dr[1].ToString();
                   dto.Referencia = dr[2].ToString();
                   dto.NomeArmazemFrom = dr[3].ToString();
                   dto.NomeArmazemTo = dr[4].ToString();
                   dto.Lancamento = DateTime.Parse(dr[5].ToString()); 
                   dto.Utilizador = dr[6].ToString();
                   lista.Add(dto);
               }
           }
           catch (Exception ex)
           {
               dto.Sucesso = false;
               dto.MensagemErro = ex.Message.Replace("'", string.Empty);
           }
           finally
           {
               FecharConexao();
           }
           return lista;
       }

        public List<ItemMovimentoStockDTO> ObterItemsList(ItemMovimentoStockDTO dto)
        {
            List<ItemMovimentoStockDTO> lista = new List<ItemMovimentoStockDTO>();
            try
            {
                ComandText = "stp_STOCK_MOVIMENTO_DIRECTO_ARTIGOS_OBTERPORFILTRO";

                AddParameter("MOVIMENT_ID", dto.Movimento);
                AddParameter("DATA_INI", dto.LookupDate1 == DateTime.MinValue ?(object)DBNull.Value : dto.LookupDate1);
                AddParameter("DATA_TERM", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ItemMovimentoStockDTO();
                    dto.ArtigoID = int.Parse(dr[0].ToString());
                    dto.Referencia = dr[1].ToString();
                    dto.Designacao = dr[2].ToString();
                    dto.Quantidade = decimal.Parse(dr[3].ToString());
                    dto.Unidade = dr[4].ToString();
                    dto.Armazem = dr[5].ToString();
                    dto.LookupField1 = dr[6].ToString(); // Armazem de Destino
                    dto.LookupField2 = dr[7].ToString(); // Lote
                    dto.PrecoUnitario = decimal.Parse(dr[8].ToString());
                    if (dto.PrecoUnitario <= 0)
                        dto.PrecoUnitario = decimal.Parse(dr[9].ToString());
                    
                    dto.ValorTotal = dto.Quantidade   * dto.PrecoUnitario;

                    dto.ValorTotal = dto.ValorTotal < 0 ? dto.ValorTotal * (-1) : dto.ValorTotal;
                    
                    dto.TituloDocumento = dr[10].ToString();
                    dto.Status = dr[12].ToString() == "0" ? 0 : 1;
                    dto.CreatedDate = DateTime.Parse(dr[11].ToString());
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
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
