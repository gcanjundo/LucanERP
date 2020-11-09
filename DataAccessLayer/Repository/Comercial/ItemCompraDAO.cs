
using DataAccessLayer.Comercial.Stock;

using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial
{
    public class ItemCompraDAO: ConexaoDB
    {
        public ItemFaturacaoDTO Adicionar(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_ITEM_ADICIONAR";


                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@COMPRA", dto.Fatura);
                AddParameter("@PRECO", dto.PrecoCompra);
                AddParameter("@QUANTIDADE", dto.Quantidade);
                AddParameter("@DESCONTO", dto.Desconto);
                AddParameter("@DESC_COMPRA", dto.DescontoFatura);
                AddParameter("@IMPOSTOS", dto.Imposto);
                AddParameter("@VALOR_DESC", dto.ValorDesconto);
                AddParameter("@VALOR_IMP", dto.ValorImposto);
                AddParameter("@TOTAL", dto.TotalLiquido);
                AddParameter("@COMENTARIOS", dto.Notas);
                AddParameter("@ORDEM", dto.NroOrdenacao);
                AddParameter("@ITEM_STATUS", dto.ItemStatus ?? (object)DBNull.Value);
                AddParameter("@COMPOSICAO_ID", dto.ComposeID);
                AddParameter("@LOTE_ID", dto.LoteID);
                AddParameter("@DIMENSAO_ID", dto.DimensaoID);
                AddParameter("@QTD_ENCOMENDADA", dto.QuantidadeSatisfeita);
                AddParameter("@QTD_RESERVADA", dto.QuantidadeReservada);
                AddParameter("@RETENCAO", dto.Retencao);
                AddParameter("@ORIGEM_ID", dto.DocOrigemID == 0 ? -1 : dto.DocOrigemID);
                AddParameter("@ORIGEM_LINE", dto.DocOrigemLineNumber);
                AddParameter("@ENVIO_ID", dto.DocEnvioID == 0 ? -1 : dto.DocEnvioID);
                AddParameter("@ENVIO_LINE", dto.DocEnvioLineNumber);
                AddParameter("@DATA_ENTREGA", dto.DataEntrega == DateTime.MinValue ? (object)DBNull.Value : dto.DataEntrega);
                AddParameter("@DESC_FIN", dto.DescontoFinanceiro);
                AddParameter("@VALOR_DESC_FIN", dto.ValorDescontoFinanceiro);
                AddParameter("@DESCONTO_NUMERARIO", dto.DescontoNumerario);
                AddParameter("@SERIALNUMBER_ID", dto.SerialNumberID);
                AddParameter("@WAREHOUSE_ID", dto.ArmazemID);
                AddParameter("@PRECO_CUSTO", dto.PrecoCusto);
                AddParameter("@TAX_ID", dto.TaxID <= 0 ? (object)DBNull.Value : dto.TaxID);
                AddParameter("@VALOR_FRETE", dto.ItemValorFrete);
                AddParameter("@VALOR_SEGURO", dto.ItemValorSeguro);
                AddParameter("@IVA_IMPORTACAO", dto.ItemValorIva);
                AddParameter("@VALOR_ADUANEIRO", dto.ItemValorAduaneiro);
                AddParameter("@HONORARIO_DESPACHANTE", dto.ItemHonorarioDespachante);
                AddParameter("@TRANSPORTACAO_LOCAL", dto.ItemFreteTransporteLocal);
                AddParameter("@OUTROS_ENCARGOS", dto.ItemOutrosEncargos);


                ExecuteNonQuery();

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

        public ItemFaturacaoDTO AbateStockArtigo(ItemFaturacaoDTO dto, int Armazem)
        {
            try
            {
                ComandText = "stp_COM_STOCK_UPDATE_QTD_ARTIGO";

                decimal qtdAtual = new StockDAO().StockActual(dto.Artigo, Armazem);

                AddParameter("@ARMAZEM", Armazem);
                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@QUANTIDADE", qtdAtual - dto.Quantidade);

                ExecuteNonQuery();

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

        public ItemFaturacaoDTO Excluir(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_ITEM_EXCLUIR";

                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@COMPRA", dto.Fatura);

                ExecuteNonQuery();

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

        public List<ItemFaturacaoDTO> ObterPorFiltro(ItemFaturacaoDTO dto)
        {

            List<ItemFaturacaoDTO> lista = new List<ItemFaturacaoDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_ITEM_OBTERPORFILTRO";


                AddParameter("@COMPRA", dto.Fatura);
                AddParameter("@INICIO", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("@FINAL", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);

                MySqlDataReader dr = ExecuteReader();


                while (dr.Read())
                {
                    dto = new ItemFaturacaoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Artigo = int.Parse(dr[1].ToString()),
                        Fatura = int.Parse(dr[2].ToString()),
                        Quantidade = decimal.Parse(dr[3].ToString()),
                        PrecoCompra = decimal.Parse(dr[4].ToString()),
                        Desconto = decimal.Parse(dr[5].ToString()), /*(decimal.Parse(dr[5].ToString()) * 100) / (decimal.Parse(dr[4].ToString()) * decimal.Parse(dr[3].ToString())),*/
                        DescontoFatura = decimal.Parse(dr[6].ToString()),
                        Imposto = decimal.Parse(dr[7].ToString()) <= 0 ? 0 : decimal.Parse(dr[7].ToString()),
                        ValorDesconto = decimal.Parse(dr[8].ToString()),
                        ValorImposto = decimal.Parse(dr[9].ToString()),
                        TotalLiquido = decimal.Parse(dr[10].ToString()),
                        Comentarios = dr[11].ToString(),
                        NroOrdenacao = int.Parse(dr[12].ToString()),
                        ItemStatus = dr[13].ToString() != "" ? dr[13].ToString() : "0",
                        ComposeID = !string.IsNullOrEmpty(dr[14].ToString()) ? int.Parse(dr[14].ToString()) : 0,
                        LoteID = !string.IsNullOrEmpty(dr[15].ToString()) ? int.Parse(dr[15].ToString()) : 0,
                        DimensaoID = !string.IsNullOrEmpty(dr[16].ToString()) ? int.Parse(dr[16].ToString()) : 0,
                        QuantidadeSatisfeita = !string.IsNullOrEmpty(dr[17].ToString()) ? decimal.Parse(dr[17].ToString()) : 0,
                        QuantidadeReservada = !string.IsNullOrEmpty(dr[18].ToString()) ? decimal.Parse(dr[18].ToString()) : 0,
                        Retencao = !string.IsNullOrEmpty(dr[19].ToString()) ? decimal.Parse(dr[19].ToString()) : 0,
                        DocOrigemID = !string.IsNullOrEmpty(dr[20].ToString()) ? int.Parse(dr[20].ToString()) : 0,
                        DocOrigemLineNumber = !string.IsNullOrEmpty(dr[21].ToString()) ? int.Parse(dr[21].ToString()) : 0,
                        DocEnvioID = !string.IsNullOrEmpty(dr[22].ToString()) ? int.Parse(dr[22].ToString()) : 0,
                        DocEnvioLineNumber = !string.IsNullOrEmpty(dr[23].ToString()) ? int.Parse(dr[23].ToString()) : 0,
                        DataEntrega = !string.IsNullOrEmpty(dr[24].ToString()) ? DateTime.Parse(dr[24].ToString()) : DateTime.MinValue,
                        DescontoFinanceiro = !string.IsNullOrEmpty(dr[25].ToString()) ? decimal.Parse(dr[25].ToString()) : 0,
                        ValorDescontoFinanceiro = !string.IsNullOrEmpty(dr[26].ToString()) ? decimal.Parse(dr[26].ToString()) : 0,
                        DescontoNumerario = !string.IsNullOrEmpty(dr[27].ToString()) ? decimal.Parse(dr[27].ToString()) : 0,
                        SerialNumberID = !string.IsNullOrEmpty(dr[28].ToString()) ? int.Parse(dr[28].ToString()) : 0,
                        Designacao = dr["ART_DESIGNACAO"].ToString(),
                        Referencia = dr["ART_REFERENCIA"].ToString(),
                        BarCode = dr["ART_CODIGO_BARRAS"].ToString(),
                        Unidade = dr["UNI_SIGLA"].ToString(),
                        MovimentaStock = dr["ART_MOVIMENTA_STOCK"].ToString() == "1" ? true : false,
                        ArmazemID = dr["FAT_ITEM_WAREHOUSE_ID"].ToString() == "" ? int.Parse(dr["FAT_CODIGO_ARMAZEM"].ToString()) : int.Parse(dr["FAT_ITEM_WAREHOUSE_ID"].ToString()),
                        DesignacaoEntidade = dr["FAT_NOME_FORNECEDOR"].ToString(),
                        SocialName = dr["FAT_RECEPCAO"].ToString(),
                        LookupField1 = dr["FAT_LOCAL_DESCARGA"].ToString(),
                        TituloDocumento = dr["DOC_DESCRICAO"].ToString(),
                        LookupField2 = dr["FAT_REFERENCIA"].ToString(),
                        DataEntrada = !string.IsNullOrEmpty(dr["FAT_DATA_EMISSAO"].ToString()) ? DateTime.Parse(dr["FAT_DATA_EMISSAO"].ToString()) : DateTime.MinValue,
                        LookupDate1 = !string.IsNullOrEmpty(dr["FAT_DATA_ENTREGA"].ToString()) ? DateTime.Parse(dr["FAT_DATA_ENTREGA"].ToString()) : DateTime.MinValue,
                        LookupField3 = dr["DOC_FORMATO"].ToString(),
                        TaxID = dr["FAT_ITEM_IMPOSTO_ID"].ToString() != "" ? int.Parse(dr["FAT_ITEM_IMPOSTO_ID"].ToString()) : 0,
                        UnidadeID = dr["UNI_SIGLA"].ToString(),
                        ItemValorFrete = dr["FAT_ITEM_VALOR_FRETE"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_FRETE"].ToString()) : 0,
                        ItemValorSeguro = dr["FAT_ITEM_VALOR_SEGURO"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_SEGURO"].ToString()) : 0,
                        ItemValorIva = dr["FAT_ITEM_VALOR_IVA_IMPORTACAO"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_IVA_IMPORTACAO"].ToString()) : 0,
                        ItemValorAduaneiro = dr["FAT_ITEM_VALOR_ADUANEIRO"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_ADUANEIRO"].ToString()) : 0,
                        ItemHonorarioDespachante = dr["FAT_ITEM_VALOR_HONORARIO_DESPACHANTE"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_HONORARIO_DESPACHANTE"].ToString()) : 0,
                        ItemFreteTransporteLocal = dr["FAT_ITEM_VALOR_TRANSPORTACAO_LOCAL"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_TRANSPORTACAO_LOCAL"].ToString()) : 0,
                        ItemOutrosEncargos = dr["FAT_ITEM_VALOR_OUTROS_ENCARGOS"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_VALOR_OUTROS_ENCARGOS"].ToString()) : 0,
                        TotalEncargos = dr["FAT_ITEM_TOTAL_ENCARGOS"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_TOTAL_ENCARGOS"].ToString()) : 0,
                        PrecoCusto = dr["FAT_ITEM_PRECO_CUSTO"].ToString() != "" ? decimal.Parse(dr["FAT_ITEM_PRECO_CUSTO"].ToString()) : 0,
                        Notas = dr["FAT_ITEM_COMENTARIOS"].ToString()
                    };
                    dto.PrecoMilheiro = 0;
                    dto.Comentarios = dto.Comentarios == string.Empty ? dto.Notas : dto.Comentarios;
                    dto.CookerID = -1;
                    dto.PrecoUnitario = dto.PrecoCusto ==0 ? dto.PrecoCompra + dto.TotalEncargos : dto.PrecoCusto;
                    lista.Add(dto);
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

        public void ReposicaoStockArtigo(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_STOCK_UPDATE_QTD_ARTIGO";

                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@QUANTIDADE", new StockDAO().StockActual(dto.Artigo, 1) + dto.Quantidade);

                ExecuteNonQuery();

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
        }

        public List<ItemFaturacaoDTO> ObterGasto(FaturaDTO pInvoice)
        {
            ItemFaturacaoDTO dto = new ItemFaturacaoDTO();
            var productsList = new List<ItemFaturacaoDTO>();
            try
            {
                ComandText = "stp_COM_OBTERLUCROPORARTIGO";

                AddParameter("@COMPRA", pInvoice.Codigo);
                AddParameter("@DE", pInvoice.EmissaoIni);
                AddParameter("@ATE", pInvoice.EmissaoTerm);


                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ItemFaturacaoDTO();

                    dto.Artigo = int.Parse(dr[0].ToString());
                    dto.Referencia = dr[1].ToString();
                    dto.Designacao = dr[2].ToString();
                    dto.PrecoCusto = decimal.Parse(dr[3].ToString());
                    dto.Quantidade = decimal.Parse(dr[6].ToString());
                    dto.PrecoUnitario = decimal.Parse(dr[7].ToString());
                    dto.PrecoCusto = dto.PrecoCusto <= 0 ? decimal.Parse(dr[4].ToString()) : dto.PrecoCusto;
                    dto.PrecoCusto = dto.PrecoCusto <= 0 ? decimal.Parse(dr[5].ToString()) : dto.PrecoCusto;
                    dto.Lucro = (dto.Quantidade * dto.PrecoUnitario) - (dto.PrecoCusto * dto.Quantidade);
                    productsList.Add(dto);

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

            return productsList;
        }

        public List<ItemFaturacaoDTO> ObterMapaIVAResumido(ItemFaturacaoDTO dto)
        {
            List<ItemFaturacaoDTO> lista = new List<ItemFaturacaoDTO>();
            try
            {
                AddParameter("PERIODO_INI", dto.LookupDate1);
                AddParameter("PERIODO_TERM", dto.LookupDate2);
                AddParameter("COMPANY_ID", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ItemFaturacaoDTO();

                    dto.Designacao = dr[0].ToString();

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

        public void SavePlanilhaCalculo(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_PLANILHA_CALCULO_ROTULO_ADICIONAR";
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@HEIGHT", dto.HeightSize);
                AddParameter("@WIDTH", dto.WidthSize);
                AddParameter("@QUANTITY", dto.Quantidade);
                AddParameter("@CARREIRA", dto.Carreira);
                AddParameter("@M2", dto.QuantidadeM2);
                AddParameter("@DESPERDICIO", dto.Desperdicio);
                AddParameter("@DESPERDICIO_M2", dto.TotalDesperdicioM2);
                AddParameter("@ROLO", dto.QuantidadeRolo);
                AddParameter("@MATERIAL", dto.MaterialMM);
                AddParameter("@AQUISICAO", dto.PrecoCompra);
                AddParameter("@SUSTRATO", dto.CustoSubstractoM2);
                AddParameter("@CLICHE", dto.CustoCliche);
                AddParameter("@CILINDRO", dto.CustoCilindro);

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
        }


    }
}
