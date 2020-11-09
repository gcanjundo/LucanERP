
using DataAccessLayer.Comercial.Stock;
using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccessLayer.Comercial
{
    public class ItemFaturacaoDAO:ConexaoDB
    {
        public ItemFaturacaoDTO Adicionar(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_ADICIONAR";

                 
                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@FATURA", dto.Fatura);
                AddParameter("@PRECO", dto.PrecoUnitario);
                AddParameter("@QUANTIDADE", dto.Quantidade);
                AddParameter("@DESCONTO", dto.Desconto);
                AddParameter("@DESC_FATURA", dto.DescontoFatura);
                AddParameter("@IMPOSTOS", dto.Imposto);
                AddParameter("@VALOR_DESC", dto.ValorDesconto);
                AddParameter("@VALOR_IMP",  dto.ValorImposto);
                AddParameter("@TOTAL", dto.TotalLiquido);
                AddParameter("@COMENTARIOS", dto.Notas!=null ? dto.Notas.Replace(";", "\n") : dto.Notas); 
                AddParameter("@ORDEM", dto.NroOrdenacao);
                AddParameter("@ITEM_STATUS", dto.ItemStatus ?? (object)DBNull.Value);
                AddParameter("@COMPOSICAO_ID", dto.ComposeID);
                AddParameter("@LOTE_ID", dto.LoteID);
                AddParameter("@DIMENSAO_ID", dto.DimensaoID);
                AddParameter("@QTD_SATISFEITA", dto.QuantidadeSatisfeita);
                AddParameter("@QTD_RESERVADA", dto.QuantidadeReservada);
                AddParameter("@RETENCAO", dto.Retencao);
                AddParameter("@ORIGEM_ID", dto.DocOrigemID==0 ? -1: dto.DocOrigemID);
                AddParameter("@ORIGEM_LINE", dto.DocOrigemLineNumber);
                AddParameter("@ENVIO_ID", dto.DocEnvioID ==0 ? -1 : dto.DocEnvioID);
                AddParameter("@ENVIO_LINE", dto.DocEnvioLineNumber);
                AddParameter("@DATA_ENTREGA", dto.DataEntrega == DateTime.MinValue ? (object)DBNull.Value : dto.DataEntrega);
                AddParameter("@DESC_FIN", dto.DescontoFinanceiro);
                AddParameter("@VALOR_DESC_FIN", dto.ValorDescontoFinanceiro);
                AddParameter("@DESCONTO_NUMERARIO", dto.DescontoNumerario);
                AddParameter("@SERIALNUMBER_ID", dto.SerialNumberID);
                AddParameter("@WAREHOUSE_ID", dto.ArmazemID);
                AddParameter("@PRECO_CUSTO", dto.PrecoCusto); 
                AddParameter("@TAX_ID", dto.TaxID <= 0 ? (object)DBNull.Value : dto.TaxID);
                AddParameter("@PRIOR_STOCK", dto.ExistenciaAnterior);
                AddParameter("@ORIGINAL_PRICE", dto.PrecoUnitarioOriginalCurrency);
                AddParameter("@COMPRIMENTO", dto.HeightSize);
                AddParameter("@LARGURA", dto.WidthSize);
                AddParameter("@ACUMULA", dto.Acumula ? 1 : 0);
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("CURRENCY_ID", dto.OriginalCurrencyID);
                AddParameter("VENDEDOR_ID", string.IsNullOrEmpty(dto.FuncionarioID) ? (object)DBNull.Value : dto.FuncionarioID);
                AddParameter("PRECO_MILHEIRO", dto.PrecoMilheiro);
                AddParameter("COMISSAO", dto.Comissao);
                AddParameter("VALOR_COMISSAO", dto.TotalComissaoLinha);
                AddParameter("@DOC_ORIGEM_TYPE_ID", dto.DocOrigemTypeID <= 0 ? (object)DBNull.Value : dto.DocOrigemTypeID);
                AddParameter("@DOC_ENVIO_TYPE_ID", dto.DocEnvioTypeID <= 0 ? (object)DBNull.Value : dto.DocEnvioTypeID);
                AddParameter("@DATA_PREVISAO", dto.DataPrevisaoEntrega == DateTime.MinValue ? (object)DBNull.Value : dto.DataPrevisaoEntrega);
                AddParameter("@VALOR_UTENTE", dto.PrecoUtente);
                AddParameter("@VALOR_ENTIDADE", dto.PrecoEntidade);
                AddParameter("@TITULO", dto.ItemNotes);
                AddParameter("@RETENCAO_ID", dto.RetencaoID <= 0 ? (object)DBNull.Value : dto.RetencaoID);
                AddParameter("@GENERAL_COMMENT", dto.Comentarios);
                AddParameter("@PESO", dto.Weight);
                AddParameter("@QUANTIDADE_ITENS", dto.QuantidadeItens);
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
                SavePlanilhaCalculo(dto);
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
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_EXCLUIR"; 

                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@FATURA", dto.Fatura); 

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
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_OBTERPORFILTRO";

                 
                AddParameter("@FATURA", dto.Fatura);
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
                        PrecoUnitario = decimal.Parse(dr[4].ToString()),
                        Desconto = decimal.Parse(dr[5].ToString()), /*(decimal.Parse(dr[5].ToString()) * 100) / (decimal.Parse(dr[4].ToString()) * decimal.Parse(dr[3].ToString())),*/
                        DescontoFatura = decimal.Parse(dr[6].ToString()),
                        Imposto = decimal.Parse(dr[7].ToString()) <= 0 ? 0 : decimal.Parse(dr[7].ToString()),
                        ValorDesconto = decimal.Parse(dr[8].ToString()),
                        ValorImposto = decimal.Parse(dr[9].ToString()),
                        TotalLiquido = decimal.Parse(dr[10].ToString()),
                        Notas = dr[11].ToString(),
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
                        DesignacaoEntidade = dr["FAT_NOME_CLIENTE"].ToString(),
                        SocialName = dr["FAT_RECEPCAO"].ToString(),
                        LookupField1 = dr["FAT_LOCAL_DESCARGA"].ToString(),
                        TituloDocumento = dr["DOC_DESCRICAO"].ToString(),
                        LookupField2 = dr["FAT_REFERENCIA"].ToString(),
                        DataEntrada = !string.IsNullOrEmpty(dr["FAT_DATA_EMISSAO"].ToString()) ? DateTime.Parse(dr["FAT_DATA_EMISSAO"].ToString()) : DateTime.MinValue,
                        LookupDate1 = !string.IsNullOrEmpty(dr["FAT_DATA_ENTREGA"].ToString()) ? DateTime.Parse(dr["FAT_DATA_ENTREGA"].ToString()) : DateTime.MinValue,
                        LookupField3 = dr["DOC_FORMATO"].ToString(),
                        TaxID = dr["FAT_ITEM_IMPOSTO_ID"].ToString() != "" ? int.Parse(dr["FAT_ITEM_IMPOSTO_ID"].ToString()) : 0,
                        UnidadeID = dr["UNI_SIGLA"].ToString(),

                        WidthSize = !string.IsNullOrEmpty(dr["FAT_ITEM_LARGURA"].ToString()) ? decimal.Parse(dr["FAT_ITEM_LARGURA"].ToString()) : 0,
                        PrecoUnitarioOriginalCurrency = !string.IsNullOrEmpty(dr["FAT_ORIGINAL_PRICE"].ToString()) ? decimal.Parse(dr["FAT_ORIGINAL_PRICE"].ToString()) : 0,
                        HeightSize = !string.IsNullOrEmpty(dr["FAT_ITEM_COMPRIMENTO"].ToString()) ? decimal.Parse(dr["FAT_ITEM_COMPRIMENTO"].ToString()) : 0,
                        OriginalCurrencyID = !string.IsNullOrEmpty(dr["FAT_ORIGINAL_CURRENCY_ID"].ToString()) ? int.Parse(dr["FAT_ORIGINAL_CURRENCY_ID"].ToString()) : 1,

                    };
                    dto.PrecoMilheiro = dr["FAT_PRECO_MILHEIRO"].ToString() == "" ? 0 : decimal.Parse(dr["FAT_PRECO_MILHEIRO"].ToString());
                    dto.FactorConversao = dr["UNI_FACTOR_CONVERSAO"].ToString();
                    dto.ValorConversao = dr["UNI_QUANTIDADE"].ToString() == "" ? 0 : decimal.Parse(dr["UNI_QUANTIDADE"].ToString());
                    if (dto.PrecoMilheiro > 0)
                    {
                        
                        dto.TotalLiquido = (dto.Quantidade * dto.PrecoMilheiro) - dto.ValorDesconto + dto.ValorImposto;
                    }
                    else
                    {
                        dto.TotalLiquido = (dto.Quantidade * dto.PrecoUnitario) - dto.ValorDesconto + dto.ValorImposto;
                    }

                    dto.CookerID = dr["GAR_CODIGO_ENTIDADE"].ToString() != "" ? int.Parse(dr["GAR_CODIGO_ENTIDADE"].ToString()) : -1;
                    dto.FuncionarioID = dr["NOME_TECNICO_EXECUTOR"].ToString();
                    dto.DataPrevisaoEntrega = !string.IsNullOrEmpty(dr["FAT_ITEM_PREVISAO_ENTREGA"].ToString()) ? DateTime.Parse(dr["FAT_ITEM_PREVISAO_ENTREGA"].ToString()) : DateTime.MinValue;
                    dto.ReadyDate = !string.IsNullOrEmpty(dr["FAT_ITEM_DATA_TERMINO"].ToString()) ? DateTime.Parse(dr["FAT_ITEM_DATA_TERMINO"].ToString()) : DateTime.MinValue;
                    dto.ItemNotes = dr["FAT_ITEM_TITULO"].ToString();
                    dto.PrecoEntidade = string.IsNullOrEmpty(dr["FAT_ITEM_VALOR_ENTIDADE"].ToString()) ? 0 : decimal.Parse(dr["FAT_ITEM_VALOR_ENTIDADE"].ToString());
                    dto.PrecoUtente = string.IsNullOrEmpty(dr["FAT_ITEM_VALOR_UTENTE"].ToString()) ? 0 : decimal.Parse(dr["FAT_ITEM_VALOR_UTENTE"].ToString());
                    dto.RetencaoID = string.IsNullOrEmpty(dr["FAT_ITEM_RETENCAO_ID"].ToString()) ? 0 : int.Parse(dr["FAT_ITEM_RETENCAO_ID"].ToString()); 
                    dto.Url = File.Exists(dr["ART_IMAGEM"].ToString()) ? dr["ART_IMAGEM"].ToString() : "../imagens/SemFoto.jpg";
                    dto.QuantidadeItens = (int)decimal.Parse(dr["UNI_QUANTIDADE"].ToString());

                    if (dto.QuantidadeItens == 0)
                        dto.QuantidadeItens = 1;

                    dto.Weight = decimal.Parse(dr["FAT_ITEM_PESO"].ToString() == "" ? "0" : dr["FAT_ITEM_PESO"].ToString());
                    dto.ProductType = dr["ART_TIPO"].ToString();
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

        public List<ItemFaturacaoDTO> ObterLucro(FaturaDTO pInvoice)
        {
            ItemFaturacaoDTO dto = new ItemFaturacaoDTO();
            var productsList = new List<ItemFaturacaoDTO>();
            try
            {
                ComandText = "stp_COM_OBTERLUCROPORARTIGO";

                AddParameter("@FATURA", pInvoice.Codigo);
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

        public ItemFaturacaoDTO TerminarServico(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_SETREADY";

                AddParameter("@ITEM_ID", dto.Codigo);
                AddParameter("@DATA_TERMINO", dto.ReadyDate == DateTime.MinValue ? DateTime.Now : dto.ReadyDate);
                AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public List<ItemFaturacaoDTO> ObterEncomendaItensPorFiltro(FaturaDTO dto)
        {

            List<ItemFaturacaoDTO> lista = new List<ItemFaturacaoDTO>();
            try
            {
                ComandText = "stp_COM_ENCOMENDA_OBTER_ITENS";


                AddParameter("@CUSTOMER_ID", dto.Entidade);
                AddParameter("@TELEFONE", dto.LookupField1??string.Empty);
                AddParameter("@DELIVERYMAN_ID", -1);
                AddParameter("@EMISSAO_INI", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("@EMISSAO_TERM", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);
                AddParameter("@ESTADO", dto.Status);
                AddParameter("@ORDER_ID", dto.Codigo);
                AddParameter("@EMPLOYEE_ID", dto.FuncionarioID??"-1");
                AddParameter("@ORDER_NUMBER", dto.Numeracao);

                MySqlDataReader dr = ExecuteReader();


                while (dr.Read())
                {
                    var item = new ItemFaturacaoDTO();
                    item.Codigo = int.Parse(dr[0].ToString());
                    item.Artigo = int.Parse(dr[1].ToString());
                    item.Fatura = int.Parse(dr[2].ToString());
                    item.Quantidade = decimal.Parse(dr[3].ToString());
                    item.PrecoUnitario = decimal.Parse(dr[4].ToString());
                    item.Desconto = decimal.Parse(dr[5].ToString()); /*(decimal.Parse(dr[5].ToString()) * 100) / (decimal.Parse(dr[4].ToString()) * decimal.Parse(dr[3].ToString()));*/
                    item.DescontoFatura = decimal.Parse(dr[6].ToString());
                    item.Imposto = decimal.Parse(dr[7].ToString()) <= 0 ? 0 : decimal.Parse(dr[7].ToString());
                    item.ValorDesconto = decimal.Parse(dr[8].ToString());
                    item.ValorImposto = decimal.Parse(dr[9].ToString());
                    item.TotalLiquido = decimal.Parse(dr[10].ToString());
                    item.Notas = dr[11].ToString();
                    item.NroOrdenacao = int.Parse(dr[12].ToString());
                    item.ItemStatus = dr[13].ToString() != "" ? dr[13].ToString() : "0";
                    item.ComposeID = !string.IsNullOrEmpty(dr[14].ToString()) ? int.Parse(dr[14].ToString()) : 0;
                    item.LoteID = !string.IsNullOrEmpty(dr[15].ToString()) ? int.Parse(dr[15].ToString()) : 0;
                    item.DimensaoID = !string.IsNullOrEmpty(dr[16].ToString()) ? int.Parse(dr[16].ToString()) : 0;
                    item.QuantidadeSatisfeita = !string.IsNullOrEmpty(dr[17].ToString()) ? decimal.Parse(dr[17].ToString()) : 0;
                    item.QuantidadeReservada = !string.IsNullOrEmpty(dr[18].ToString()) ? decimal.Parse(dr[18].ToString()) : 0;
                    item.Retencao = !string.IsNullOrEmpty(dr[19].ToString()) ? decimal.Parse(dr[19].ToString()) : 0;
                    item.DocOrigemID = !string.IsNullOrEmpty(dr[20].ToString()) ? int.Parse(dr[20].ToString()) : 0;
                    item.DocOrigemLineNumber = !string.IsNullOrEmpty(dr[21].ToString()) ? int.Parse(dr[21].ToString()) : 0;
                    item.DocEnvioID = !string.IsNullOrEmpty(dr[22].ToString()) ? int.Parse(dr[22].ToString()) : 0;
                    item.DocEnvioLineNumber = !string.IsNullOrEmpty(dr[23].ToString()) ? int.Parse(dr[23].ToString()) : 0;
                    item.DataEntrega = !string.IsNullOrEmpty(dr[24].ToString()) ? DateTime.Parse(dr[24].ToString()) : DateTime.MinValue;
                    item.DescontoFinanceiro = !string.IsNullOrEmpty(dr[25].ToString()) ? decimal.Parse(dr[25].ToString()) : 0;
                    item.ValorDescontoFinanceiro = !string.IsNullOrEmpty(dr[26].ToString()) ? decimal.Parse(dr[26].ToString()) : 0;
                    item.DescontoNumerario = !string.IsNullOrEmpty(dr[27].ToString()) ? decimal.Parse(dr[27].ToString()) : 0;
                    item.SerialNumberID = !string.IsNullOrEmpty(dr[28].ToString()) ? int.Parse(dr[28].ToString()) : 0;
                    item.Designacao = dr["ART_DESIGNACAO"].ToString();
                    item.Referencia = dr["ART_REFERENCIA"].ToString();
                    item.BarCode = dr["ART_CODIGO_BARRAS"].ToString();
                    item.Unidade = dr["UNI_SIGLA"].ToString();
                    item.MovimentaStock = dr["ART_MOVIMENTA_STOCK"].ToString() == "1" ? true : false;
                    item.ArmazemID = dr["FAT_ITEM_WAREHOUSE_ID"].ToString() == "" ? int.Parse(dr["FAT_CODIGO_ARMAZEM"].ToString()) : int.Parse(dr["FAT_ITEM_WAREHOUSE_ID"].ToString());
                    item.DesignacaoEntidade = dr["FAT_NOME_CLIENTE"].ToString();
                    item.SocialName = dr["FAT_RECEPCAO"].ToString();
                    item.LookupField1 = dr["FAT_LOCAL_DESCARGA"].ToString();
                    item.TituloDocumento = dr["DOC_DESCRICAO"].ToString();
                    item.LookupField2 = dr["FAT_REFERENCIA"].ToString() + " - " + item.NroOrdenacao;
                    item.DataEntrada = !string.IsNullOrEmpty(dr["FAT_DATA_EMISSAO"].ToString()) ? DateTime.Parse(dr["FAT_DATA_EMISSAO"].ToString()) : DateTime.MinValue;
                    item.LookupDate1 = !string.IsNullOrEmpty(dr["FAT_DATA_ENTREGA"].ToString()) ? DateTime.Parse(dr["FAT_DATA_ENTREGA"].ToString()) : DateTime.MinValue;
                    item.LookupField3 = dr["DOC_FORMATO"].ToString();
                    item.TaxID = dr["FAT_ITEM_IMPOSTO_ID"].ToString() != "" ? int.Parse(dr["FAT_ITEM_IMPOSTO_ID"].ToString()) : 0;
                    item.UnidadeID = dr["UNI_SIGLA"].ToString();

                    item.WidthSize = !string.IsNullOrEmpty(dr["FAT_ITEM_LARGURA"].ToString()) ? decimal.Parse(dr["FAT_ITEM_LARGURA"].ToString()) : 0;
                    item.PrecoUnitarioOriginalCurrency = !string.IsNullOrEmpty(dr["FAT_ORIGINAL_PRICE"].ToString()) ? decimal.Parse(dr["FAT_ORIGINAL_PRICE"].ToString()) : 0;
                    item.HeightSize = !string.IsNullOrEmpty(dr["FAT_ITEM_COMPRIMENTO"].ToString()) ? decimal.Parse(dr["FAT_ITEM_COMPRIMENTO"].ToString()) : 0;
                    item.OriginalCurrencyID = !string.IsNullOrEmpty(dr["FAT_ORIGINAL_CURRENCY_ID"].ToString()) ? int.Parse(dr["FAT_ORIGINAL_CURRENCY_ID"].ToString()) : 1;
                    item.PrecoMilheiro = dr["FAT_PRECO_MILHEIRO"].ToString() == "" ? 0 : decimal.Parse(dr["FAT_PRECO_MILHEIRO"].ToString());
                    item.FactorConversao = dr["UNI_FACTOR_CONVERSAO"].ToString();
                    item.ValorConversao = dr["UNI_QUANTIDADE"].ToString() == "" ? 0 : decimal.Parse(dr["UNI_QUANTIDADE"].ToString());
                    item.CookerID = dr["GAR_CODIGO_ENTIDADE"].ToString() != "" ? int.Parse(dr["GAR_CODIGO_ENTIDADE"].ToString()) : -1;
                    item.FuncionarioID = dr["NOME_TECNICO_EXECUTOR"].ToString();
                    item.DataPrevisaoEntrega = !string.IsNullOrEmpty(dr["FAT_ITEM_PREVISAO_ENTREGA"].ToString()) ? DateTime.Parse(dr["FAT_ITEM_PREVISAO_ENTREGA"].ToString()) : DateTime.MinValue;
                    item.ReadyDate = !string.IsNullOrEmpty(dr["FAT_ITEM_DATA_TERMINO"].ToString()) ? DateTime.Parse(dr["FAT_ITEM_DATA_TERMINO"].ToString()) : DateTime.MinValue;
                    item.ItemNotes = dr["FAT_ITEM_TITULO"].ToString();
                    item.PrecoEntidade = string.IsNullOrEmpty(dr["FAT_ITEM_VALOR_ENTIDADE"].ToString()) ? 0 : decimal.Parse(dr["FAT_ITEM_VALOR_ENTIDADE"].ToString());
                    item.PrecoUtente = string.IsNullOrEmpty(dr["FAT_ITEM_VALOR_UTENTE"].ToString()) ? 0 : decimal.Parse(dr["FAT_ITEM_VALOR_UTENTE"].ToString());
                    item.RetencaoID = string.IsNullOrEmpty(dr["FAT_ITEM_RETENCAO_ID"].ToString()) ? 0 : int.Parse(dr["FAT_ITEM_RETENCAO_ID"].ToString());
                    item.QuantidadeItens = dr["UNI_QUANTIDADE"].ToString() == "" ? 1 : (int)decimal.Parse(dr["UNI_QUANTIDADE"].ToString());
                    item.Entidade = int.Parse(dr["FAT_CODIGO_CLIENTE"].ToString());

                    if (item.PrecoMilheiro > 0)
                    {

                        item.TotalLiquido = (item.Quantidade * item.PrecoMilheiro) - item.ValorDesconto + item.ValorImposto;
                    }
                    else
                    {
                        item.TotalLiquido = (item.Quantidade * item.PrecoUnitario) - item.ValorDesconto + item.ValorImposto;
                    }


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
