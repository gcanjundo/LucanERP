using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dominio.Comercial;
using DataAccessLayer.Comercial.Stock;

namespace DataAccessLayer.Comercial
{
    
    
    public class ArtigoDAO 
    {
        ConexaoDB bdContext;
        public ArtigoDAO()
        {
            bdContext = new ConexaoDB();
        }


        public ArtigoDTO Adicionar(ArtigoDTO dto)
        {
            try
            {
                bdContext.ComandText = "stp_GER_ARTIGO_ADICIONAR";

                bdContext.AddParameter("@CODIGO", dto.Codigo);
                bdContext.AddParameter("@CODIGO_BARRAS", dto.CodigoBarras);
                bdContext.AddParameter("@TIPO", dto.Tipo);
                bdContext.AddParameter("@DESIGNACAO", dto.Designacao.ToUpper());
                bdContext.AddParameter("@CATEGORIA", dto.Categoria);
                bdContext.AddParameter("@REFERENCIA", dto.Referencia);
                bdContext.AddParameter("@UNIDADE_VENDA", dto.UnidadeVenda);
                bdContext.AddParameter("@PRECO_CUSTO", dto.PrecoCusto);
                if (dto.ImpostoID > 0)
                    bdContext.AddParameter("@IMPOSTO_ID", dto.ImpostoID);
                else
                    bdContext.AddParameter("@IMPOSTO_ID", DBNull.Value);
                bdContext.AddParameter("@PRECO_VENDA", dto.PrecoVenda);
                bdContext.AddParameter("@IMAGEM", dto.FotoArtigo);
                bdContext.AddParameter("@MOV_STOCK", dto.MovimentaStock);
                bdContext.AddParameter("@MARGEM", dto.MargemLucro);
                bdContext.AddParameter("@FILIAL", dto.Filial);
                bdContext.AddParameter("@ABERTURA", dto.DataCadastro);
                if (!string.IsNullOrEmpty(dto.UnidadeCompra) && !dto.UnidadeCompra.Equals("-1"))
                    bdContext.AddParameter("@UNIDADE_COMPRA", dto.ImpostoID);
                else
                    bdContext.AddParameter("@UNIDADE_COMPRA", DBNull.Value);
                bdContext.AddParameter("@QTD_COMPRA", dto.QtdUndCompra == 0 ? 1 : dto.QtdUndCompra);
                bdContext.AddParameter("@QTD_VENDA", dto.QtdUndVenda == 0 ? 1 : dto.QtdUndVenda);
                if (!string.IsNullOrEmpty(dto.Marca) && !dto.Marca.Equals("-1"))
                    bdContext.AddParameter("@MARCA", dto.Marca);
                else
                    bdContext.AddParameter("@MARCA", DBNull.Value);
                if (dto.DataValidade != DateTime.MinValue)
                    bdContext.AddParameter("@VALIDADE", dto.DataValidade);
                else
                    bdContext.AddParameter("@VALIDADE", DBNull.Value);

                bdContext.AddParameter("@PESO", dto.Peso);
                bdContext.AddParameter("@VOLUME", dto.Volume);
                bdContext.AddParameter("@CURTA", dto.CurtaDescricao);
                if (!string.IsNullOrEmpty(dto.Fabricante) && !dto.Fabricante.Equals("-1"))
                    bdContext.AddParameter("@FABRICANTE", dto.Fabricante);
                else
                    bdContext.AddParameter("@FABRICANTE", DBNull.Value);
                bdContext.AddParameter("@ANO", dto.AnoFabrico);
                bdContext.AddParameter("@PEDIDO_COZINHA", dto.PedidoCozinha);
                bdContext.AddParameter("@DEVOLUCAO", dto.SujeitoDevolucao);
                bdContext.AddParameter("@IMPOSTO_INCLUIDO", dto.ImpostoIncluido);
                bdContext.AddParameter("@PCM_PADRAO", dto.PrecoCustoMedioPadrao);
                bdContext.AddParameter("@PCM", dto.PrecoCusto);
                bdContext.AddParameter("@DESCONTO", dto.Desconto);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);
                bdContext.AddParameter("@COMPOSTO", dto.IsComposto);
                bdContext.AddParameter("@DIMENSOES", dto.TemDimensoes);
                bdContext.AddParameter("@PVP", dto.MultiplosPrecos);
                bdContext.AddParameter("@DESCONTINUADO", dto.Descontinuado);
                bdContext.AddParameter("@LOTES", dto.ControladoPorLotes);
                bdContext.AddParameter("@TAX_PERCENT", dto.PercentualImposto);
                bdContext.AddParameter("@POS", dto.DisponivelPOS);
                bdContext.AddParameter("@INCOME_UNIT", dto.IncomeUnit <= 0 ? (object)DBNull.Value : dto.IncomeUnit);
                bdContext.AddParameter("@INCOME_QTD", dto.IncomeQuatity);
                bdContext.AddParameter("@OUTCOME_UNIT", dto.OutComeUnit <= 0 ? (object)DBNull.Value : dto.OutComeUnit);
                bdContext.AddParameter("@OUTCOME_QTD", dto.OutComeQuantity);
                bdContext.AddParameter("@REFERENCE_UNIT", dto.ReferenceUnit <= 0 ? (object)DBNull.Value : dto.ReferenceUnit);
                bdContext.AddParameter("@REFERENCE_QTD", dto.ReferenceQuantity);
                bdContext.AddParameter("@TIME_DELIVERY", dto.DelieveryDeadLine);
                bdContext.AddParameter("@SUPPLIER_ID", dto.MainSupplier <= 0 ? (object)DBNull.Value : dto.MainSupplier);
                bdContext.AddParameter("@DESCRIPTION", dto.ComercialDescription);
                bdContext.AddParameter("@SALEMAN_ID", dto.MainSalesman <= 0 ? (object)DBNull.Value : dto.MainSalesman);
                bdContext.AddParameter("@COMISSAO", dto.ComissaoValue);
                bdContext.AddParameter("@TAX_VALUE", dto.ImpostoLiquido);
                bdContext.AddParameter("@NOTES", dto.Notes);
                bdContext.AddParameter("@CALORIAS", dto.Calorias);
                bdContext.AddParameter("@PROTEINAS", dto.Proteinas);
                bdContext.AddParameter("@GORDURAS", dto.Gorduras);
                bdContext.AddParameter("@CARBOHIDRATOS", dto.Carbohidratos);
                bdContext.AddParameter("@VIDEOS", dto.VideoPath);
                bdContext.AddParameter("@BALANCE", dto.Balanca);
                bdContext.AddParameter("@COMPOSE_TYPE", dto.ComposeType);
                bdContext.AddParameter("@COMPOSE_PRICE", dto.ComposePrice);
                bdContext.AddParameter("@EXTERNAL_SERVICE", dto.IsExternalService);
                bdContext.AddParameter("@WITH_ORDER", dto.OnlyWithOrder);
                bdContext.AddParameter("@REST_PRODUCT", dto.RestProduct);
                bdContext.AddParameter("@RETAIL_ID", dto.RetailID <= 0 ? (object)DBNull.Value : dto.RetailID);
                bdContext.AddParameter("@VASILHAME_ID", dto.VasilhameID <= 0 ? (object)DBNull.Value : dto.VasilhameID);
                bdContext.AddParameter("@STOCK_BREAK_ACTION", dto.AlertaRupturaStock);
                bdContext.AddParameter("@LOTE_MANAGEMENT", dto.LoteManagement);
                bdContext.AddParameter("@ORDER_RESERVE", dto.StockCaptive);
                bdContext.AddParameter("@LOW_STOCK", dto.AllowedSaleWithLowStock);
                bdContext.AddParameter("@PREPARATION_TIME", dto.DuracaoPreparo <= 0 ? (object)DBNull.Value : dto.DuracaoPreparo);
                bdContext.AddParameter("@STOCK_RUBRICA_ID", dto.StockAccountID <= 0 ? (object)DBNull.Value : dto.StockAccountID);
                bdContext.AddParameter("@COMPRA_RUBRICA_ID", dto.PurchageAccountID <= 0 ? (object)DBNull.Value : dto.PurchageAccountID);
                bdContext.AddParameter("@VENDA_RUBRICA_ID", dto.SalesAccountID <= 0 ? (object)DBNull.Value : dto.SalesAccountID);
                bdContext.AddParameter("@DEVOLUCAO_COMPRA_ID", dto.PurchageRefundAccountID <= 0 ? (object)DBNull.Value : dto.PurchageRefundAccountID);
                bdContext.AddParameter("@DEVOLUCAO_VENDA_ID", dto.SalesRefundAccountID <= 0 ? (object)DBNull.Value : dto.SalesRefundAccountID);
                bdContext.AddParameter("@WITH_RETENTION", dto.WithRetention ? 1 : 0);
                bdContext.AddParameter("@CURRENCY_ID", dto.CurrencyID == -1 ? (object)DBNull.Value : dto.CurrencyID);
                bdContext.AddParameter("@STOCK_TYPE", dto.StockType == "-1" ? (object)DBNull.Value : dto.StockType);
                bdContext.AddParameter("@CUSTOMER_ID", dto.CustomerID == -1 ? (object)DBNull.Value : dto.CustomerID);
                bdContext.AddParameter("@WIDTH", dto.Largura);
                bdContext.AddParameter("@HEIGTH", dto.Comprimento);

                dto.Codigo = bdContext.ExecuteInsert();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
                if (dto.MovimentaStock && dto.Tipo.Equals("P"))
                {
                    foreach (var artigo in dto.InfoStock)
                    {
                        artigo.ArtigoID = dto.Codigo; 
                        new StockDAO().Adicionar(artigo);
                    }
                }

                if (dto.IsComposto == 1)
                {
                    foreach (var artigo in dto.ListaComposicao)
                    {
                        artigo.ArtigoVirtualID = dto.Codigo;
                        new ComposicaoDAO().Adicionar(artigo);
                    }
                }

                if (dto.ControladoPorLotes == 1)
                {
                    foreach (var lote in dto.ListaLotes)
                    {
                        lote.ProductID = dto.Codigo;
                        lote.Utilizador = dto.Utilizador;
                        lote.IncomeUnit = lote.IncomeUnit <= 0 ? dto.IncomeUnit : lote.IncomeUnit;
                        lote.OutComeUnit = lote.OutComeUnit <= 0 ? dto.OutComeUnit : lote.OutComeUnit;
                        lote.ReferenceUnit = lote.ReferenceUnit <= 0 ? dto.ReferenceUnit : lote.ReferenceUnit;
                        lote.UnidadeVenda = lote.UnidadeVenda == "-1" ? dto.UnidadeVenda : lote.UnidadeVenda;
                        lote.UnidadeCompra = lote.UnidadeCompra =="-1" ? dto.UnidadeCompra : lote.UnidadeCompra;
                        new LoteDAO().Adicionar(lote);
                    }
                }
                

                if(string.IsNullOrEmpty(dto.LoteReference) || string.IsNullOrEmpty(dto.SerialNumber))
                {
                    AddSigleLoteInfo(dto);
                }

                if(dto.ArtigoVestuarioID > 0)
                {
                    AddLaudryItem(dto);
                }
                 
            }

            return dto;
        }


        void AddSigleLoteInfo(ArtigoDTO dto)
        {
            try
            {

                bdContext.ComandText = "stp_GER_ARTIGO_SIGLE_LOTE_ADICIONAR";

                bdContext.AddParameter("@CODIGO", dto.Codigo);
                bdContext.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
             
        }

        public ArtigoDTO Excluir(ArtigoDTO dto)
        {
            try
            {

                bdContext.ComandText = "stp_GER_ARTIGO_EXCLUIR";

                bdContext.AddParameter("@CODIGO", dto.Codigo);
                bdContext.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }

            return dto;
        } 


        public ArtigoDTO ObterPorPK(ArtigoDTO dto)
        {
            
            try
            {
                
                bdContext.ComandText = "stp_GER_ARTIGO_OBTERPORPK";

                bdContext.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = bdContext.ExecuteReader();

                dto = new ArtigoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.CodigoBarras = dr[1].ToString();
                    dto.Referencia = dr[2].ToString();
                    dto.Tipo = dr[3].ToString();
                    dto.Designacao = dr[4].ToString().ToUpper();
                    dto.Categoria = dr[5].ToString() ?? "-1";
                    dto.Marca = dr[6].ToString() ?? "-1";
                    dto.UnidadeVenda = dr[7].ToString() ?? "-1";
                    dto.PrecoCusto = decimal.Parse(dr[8].ToString() ?? "0");
                    dto.ImpostoID = int.Parse(dr[9].ToString()); 
                    dto.PrecoVenda = decimal.Parse(dr[10].ToString() ?? "0");
                    dto.FotoArtigo = dr[11].ToString();
                    dto.Filial = dr[12].ToString();
                    int stock = int.Parse(dr[13].ToString());
                    if (stock == 1)
                        dto.MovimentaStock = true;
                    else
                        dto.MovimentaStock = false;
                    dto.MargemLucro = decimal.Parse(dr[14].ToString() ?? "0");

                    dto.ProductStatus = dr[15].ToString();
                    if (dr[16].ToString() != null)
                    {
                        dto.DataCadastro = Convert.ToDateTime(dr[16].ToString());
                    }
                    if (dr[17].ToString() != null && !dr[17].ToString().Equals(""))
                    {
                        dto.DataValidade = Convert.ToDateTime(dr[17].ToString());
                    }
                    dto.Peso = decimal.Parse(dr[18].ToString() ?? "0");
                    dto.Volume = decimal.Parse(dr[19].ToString() ?? "0");
                    dto.QtdUndVenda = decimal.Parse(dr[20].ToString() ?? "0");
                    dto.QtdUndCompra = decimal.Parse(dr[21].ToString() ?? "0");
                    dto.UnidadeCompra= dr[22].ToString() ?? "-1";
                    dto.Fabricante = dr[23].ToString();
                    dto.AnoFabrico = dr[24].ToString();
                    dto.Modelo = "-1";
                    dto.SubCategoria = "-1";
                    string marca = dr["MAR_ROOT"].ToString() == null || dr["MAR_ROOT"].ToString() == string.Empty ? "-1" : dr["MAR_ROOT"].ToString();
                    if (marca != "-1")
                    {
                        dto.Modelo = dto.Marca;
                        dto.Marca = marca;
                    }

                    string categoria = dr["CAT_CATEGORIA"].ToString() == null ? "-1" : dr["CAT_CATEGORIA"].ToString();
                    if (categoria != "-1")
                    {
                        dto.SubCategoria = dto.Categoria;
                        dto.Categoria = categoria;
                    }
                    dto.PercentualImposto = dr["ART_PERCENT_IMPOSTO"].ToString() ==null || dr["ART_PERCENT_IMPOSTO"].ToString()==string.Empty ? 0 : decimal.Parse(dr["ART_PERCENT_IMPOSTO"].ToString());
                    //dto.ImpostoID = dr["IMP_SIGLA"].ToString();
                    dto.PedidoCozinha = short.Parse(dr["ART_PEDIDO_COZINHA"].ToString());
                    dto.ImpostoIncluido = short.Parse(dr["ART_IMPOSTO_INCLUIDO"].ToString());
                    dto.SujeitoDevolucao = short.Parse(dr["ART_DEVOLUCAO"].ToString());

                    dto.ControladoPorLotes = short.Parse(dr["ART_ALLOW_LOTE"].ToString());
                    dto.TemDimensoes = short.Parse(dr["ART_ALLOW_DIMENSOES"].ToString());
                    dto.IsComposto = short.Parse(dr["ART_COMPOSTO"].ToString());
                    dto.MultiplosPrecos = short.Parse(dr["ART_TABELA_PRECO"].ToString());
                    dto.Descontinuado = short.Parse(dr["ART_DESCONTINUADO"].ToString()); 
                    dto.ImpostoLiquido = decimal.Parse(dr["ART_VALOR_IMPOSTO"].ToString()); 
                    dto.DisponivelPOS = short.Parse(dr["ART_POS"].ToString());
                    dto.OnlyWithOrder = short.Parse(dr["ART_ONLY_ORDER"].ToString());
                    dto.RestProduct = dr["ART_RESTPOS"].ToString() == "1" ? short.Parse("1") : short.Parse("0");
                    dto.RetailID = int.Parse(dr["ART_RETAIL_ID"].ToString()); 
                    dto.AlertaRupturaStock = int.Parse(dr["ART_ALERTA_RUPTURA"].ToString());
                    dto.VasilhameID = int.Parse(dr["ART_VASILHAME_ID"].ToString());
                    dto.LoteManagement =dr["ART_LOTE_MANAGMENT"].ToString();
                    dto.StockCaptive = int.Parse(dr["ART_ALLOW_DOWN_STOCK"].ToString());
                    dto.AllowedSaleWithLowStock = int.Parse(dr["ART_DESCONTINUADO"].ToString());
                    dto.ReferenceUnit = int.Parse(dr["UNIDADE_ARMAZENAMENTO"].ToString());
                    dto.IncomeUnit = int.Parse(dr["UNIDADE_ENTRADA"].ToString());
                    dto.OutComeUnit = int.Parse(dr["UNIDADE_SAIDA_STOCK"].ToString());
                    dto.PrecoCustoMedioPadrao = decimal.Parse(dr["ART_PCM_CALCULADO"].ToString());
                    dto.LastProductIncomeDate = DateTime.Parse(dr["ART_ULTIMA_ENTRADA"].ToString() == "" ? DateTime.MinValue.ToString() : dr["ART_ULTIMA_ENTRADA"].ToString());
                    dto.LastProductOutcomeDate = DateTime.Parse(dr["ART_ULTIMA_SAIDA"].ToString() == "" ? DateTime.MinValue.ToString() : dr["ART_ULTIMA_SAIDA"].ToString());
                    dto.LookupDate1 = DateTime.Parse(dr["ART_DATA_CONTAGEM"].ToString() == "" ? DateTime.MinValue.ToString() : dr["ART_DATA_CONTAGEM"].ToString());

                    dto.PrecoCustoMedioPadrao = dto.PrecoCustoMedioPadrao <= 0 ? dto.PrecoCusto : dto.PrecoCustoMedioPadrao;
                    dto.Desconto = decimal.Parse(dr["ART_DESCONTO"].ToString());

                    dto.Preco = dto.PrecoVenda;
                    dto.ComercialDescription = dr["ART_DESCRICAO_COMERCIAL"].ToString();
                    dto.Notes = dr["ART_OBSERVACOES"].ToString();
                    dto.WithRetention = dr["ART_RETENCAO_ID"].ToString() != "1" ? false : true;
                    dto.CurrencyID = dr["ART_CURRENCY_ID"].ToString()== string.Empty ? -1 : int.Parse(dr["ART_CURRENCY_ID"].ToString());
                    dto.StockType = dr["ART_STOCK_TYPE"].ToString() == string.Empty ? "-1" : dr["ART_STOCK_TYPE"].ToString();
                    dto.CustomerID = dr["ART_CUSTOMER_ID"].ToString() == string.Empty ? -1 : int.Parse(dr["ART_CUSTOMER_ID"].ToString());
                    dto.DuracaoPreparo = int.Parse(dr["ART_TEMPO_PREPARACAO"].ToString());
                    dto.DelieveryDeadLine = int.Parse(dr["ART_PRAZO_ENTREGA"].ToString()); 
                    dto.ArtigoVestuarioID = int.Parse(dr["ART_VESTUARIO_ID"].ToString());
                    dto.Largura = decimal.Parse(dr["ART_WIDTH"].ToString());
                    dto.Comprimento = decimal.Parse(dr["ART_HEIGHT"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
                if(dto.MovimentaStock && dto.Tipo.Equals("P"))
                {
                    dto.InfoStock = new StockDAO().ObterPorArtigo(dto);
                    dto.Quantidade = dto.InfoStock.Sum(t => t.Actual);
                    dto.ValorStockPCM = dto.Quantidade * dto.PrecoCustoMedioPadrao;
                    dto.ValorStockPVP = dto.Quantidade * dto.PrecoVenda;
                    dto.PrevisaoLucro = dto.ValorStockPVP - dto.ValorStockPCM;

                    
                }
                dto.PrevisaoLucro = dto.ValorStockPVP - dto.ValorStockPCM;
                if (dto.PrecoVenda > 0)
                    dto.MargemLucro = dto.MargemLucro == 0 ? (((dto.PrecoVenda - dto.PrecoCustoMedioPadrao) * 100) / dto.PrecoVenda) : dto.MargemLucro;
                if (dto.IsComposto == 1)
                {
                    dto.ListaComposicao = new ComposicaoDAO().ObterPorFiltro(dto);
                }

                if(dto.ControladoPorLotes == 1)
                {
                    dto.ListaLotes = new LoteDAO().ObterPorFiltro(new LoteDTO(dto));
                }
            }

            return dto;
        }

        public List<ArtigoDTO> ObterPorFiltro(ArtigoDTO dto)
        {
            int codArmazem = dto.WareHouseName == null || dto.WareHouseName == "" ? -1 : int.Parse(dto.WareHouseName);

            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_GER_ARTIGO_OBTERPORFILTRO";

                bdContext.AddParameter("@NOME", dto.Designacao);
                bdContext.AddParameter("@CODIGO_BARRAS", dto.CodigoBarras ?? string.Empty);
                bdContext.AddParameter("@FILIAL", dto.Filial);
                bdContext.AddParameter("@REFERENCIA", dto.Referencia ?? string.Empty);
                bdContext.AddParameter("@CODIGO", dto.Codigo);
                bdContext.AddParameter("@CATEGORIA", string.IsNullOrEmpty(dto.Categoria) ? "-1" : dto.Categoria);
                if (!string.IsNullOrEmpty(dto.ValidadeIni) && dto.ValidadeIni!=DateTime.MinValue.ToString())
                    bdContext.AddParameter("@DATA_INI", Convert.ToDateTime(dto.ValidadeIni.Replace("/", "-")));
                else
                    bdContext.AddParameter("@DATA_INI", DBNull.Value);

                if (!string.IsNullOrEmpty(dto.ValidadeTerm) && dto.ValidadeTerm != DateTime.MinValue.ToString())
                    bdContext.AddParameter("@DATA_TERM", Convert.ToDateTime(dto.ValidadeTerm.Replace("/", "-")));
                else
                    bdContext.AddParameter("@DATA_TERM", DBNull.Value);

                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);
                bdContext.AddParameter("@ARMAZEM", dto.WareHouseName ?? (object)DBNull.Value);
                bdContext.AddParameter("@PVP", dto.PrecoVenda);
                bdContext.AddParameter("@PRODUCT_TYPE", dto.Tipo ?? (object)DBNull.Value);
                bdContext.AddParameter("@CONVENIO_ID", dto.ConvenioID);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {

                    dto = new ArtigoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.CodigoBarras = dr[1].ToString();
                    dto.Referencia = dr[2].ToString();


                    dto.Designacao = dr[4].ToString().ToUpper();
                    dto.Categoria = dr[5].ToString();

                    dto.PrecoVenda = decimal.Parse(dr[6].ToString() ?? "0");

                    dto.UnidadeVenda = dr[7].ToString();
                    dto.FotoArtigo = dr[8].ToString();
                    dto.PercentualImposto = dr[9].ToString() == null || dr[9].ToString() == string.Empty ? 0 : decimal.Parse(dr[9].ToString());
                    if (dr[10].ToString() == "1")
                    {
                        dto.MovimentaStock = true;
                        //dto.Quantidade = new StockDAO().StockActual(dto.Codigo);
                        dto.ArmazemID = codArmazem > 0 ? codArmazem : int.Parse(dr["ARMAZEM"].ToString());
                        StockInfoDTO stockInfo = new StockDAO().StockActualArmazem(dto.Codigo, dto.ArmazemID, DateTime.MaxValue, DateTime.MaxValue);
                        dto.Quantidade = stockInfo.Actual;
                        dto.WareHouseName = stockInfo.WareHouseName;
                    }
                    else
                    {
                        dto.MovimentaStock = false; 
                        dto.WareHouseName = "-1";
                        dto.Quantidade = 0;
                    }

                    dto.PrecoCusto = decimal.Parse(dr[11].ToString());
                    dto.ValorStockPCM = (decimal.Parse(dr[18].ToString() ?? "0") * dto.Quantidade);
                    dto.DataCadastro = Convert.ToDateTime(dr[12].ToString());
                    dto.Imposto = dr[13].ToString();
                    if (!string.IsNullOrEmpty(dr[14].ToString()))
                    {
                        dto.DataValidade = DateTime.Parse(dr[14].ToString());
                        dto.ValidadeIni = dto.DataValidade.ToString("dd/MM/yyyy");
                    }

                    dto.ImpostoIncluido = short.Parse(dr[15].ToString());
                    dto.PedidoCozinha = short.Parse(dr[16].ToString());
                    dto.Desconto = decimal.Parse(dr[17].ToString());
                    dto.PrecoCustoMedioPadrao = decimal.Parse(dr[18].ToString());
                    dto.SujeitoDevolucao = short.Parse(dr["ART_DEVOLUCAO"].ToString());
                    dto.ControladoPorLotes = short.Parse(dr["ART_ALLOW_LOTE"].ToString());
                    dto.TemDimensoes = short.Parse(dr["ART_ALLOW_DIMENSOES"].ToString());
                    dto.IsComposto = short.Parse(dr["ART_COMPOSTO"].ToString());
                    dto.MultiplosPrecos = short.Parse(dr["ART_TABELA_PRECO"].ToString());
                    dto.Descontinuado = short.Parse(dr["ART_DESCONTINUADO"].ToString());

                    dto.ValorStockPVP = dto.Quantidade * dto.PrecoVenda;
                    dto.MargemLucro = dto.ValorStockPVP - dto.ValorStockPCM;
                    dto.Tipo = dr["ART_TIPO"].ToString(); 
                    dto.ImpostoID = int.Parse(dr["IMP_CODIGO"].ToString());
                    dto.ImpostoLiquido = decimal.Parse(dr["ART_VALOR_IMPOSTO"].ToString());
                    dto.LookupField11 = dr["IMP_DESCRICAO"].ToString();
                    dto.DisponivelPOS = dr["ART_POS"].ToString() == "1" ? short.Parse("1") : short.Parse("0");
                    dto.OnlyWithOrder = dr["ART_ONLY_ORDER"].ToString() == "1" ? short.Parse("1") : short.Parse("0");
                    dto.RestProduct = dr["ART_RESTPOS"].ToString() == "1" ? short.Parse("1") : short.Parse("0");
                    dto.RetailID = int.Parse(dr["ART_RETAIL_ID"].ToString());
                    dto.AlertaRupturaStock = int.Parse(dr["ART_ALERTA_RUPTURA"].ToString());
                    dto.VasilhameID = int.Parse(dr["ART_VASILHAME_ID"].ToString());
                    dto.LoteManagement = dr["ART_LOTE_MANAGMENT"].ToString();
                    dto.AllowedSaleWithLowStock = int.Parse(dr["ART_ALLOW_RESERVE_STOCK"].ToString());
                    dto.StockCaptive = int.Parse(dr["ART_ALLOW_DOWN_STOCK"].ToString());
                    
                    dto.ReferenceUnit = int.Parse(dr["UNIDADE_ARMAZENAMENTO"].ToString());
                    dto.IncomeUnit = int.Parse(dr["UNIDADE_ENTRADA"].ToString());
                    dto.OutComeUnit = int.Parse(dr["UNIDADE_SAIDA_STOCK"].ToString());
                    dto.UnidadeID = int.Parse(dr["ART_UNIDADE_VENDA"].ToString());
                    dto.ComercialDescription = dr["ART_DESCRICAO_COMERCIAL"].ToString();
                    dto.Notes = dr["ART_DESCRICAO_COMERCIAL"].ToString();
                    dto.LookupField2 = dr["IMP_TIPO"].ToString();

                    dto.Preco = dto.PrecoVenda + dto.ImpostoLiquido;
                    dto.ComercialDescription = dr["ART_DESCRICAO_COMERCIAL"].ToString();
                    dto.WithRetention = dr["ART_RETENCAO_ID"].ToString() != "1" ? false : true;
                    dto.CurrencyID = dr["ART_CURRENCY_ID"].ToString()== string.Empty ? -1 : int.Parse(dr["ART_CURRENCY_ID"].ToString());
                    dto.CustomerID = dr["ART_CUSTOMER_ID"].ToString()=="" ? -1 : int.Parse(dr["ART_CUSTOMER_ID"].ToString());
                    dto.DuracaoPreparo = int.Parse(dr["ART_TEMPO_PREPARACAO"].ToString());
                    dto.DelieveryDeadLine = int.Parse(dr["ART_PRAZO_ENTREGA"].ToString());
                    dto.Largura = decimal.Parse(dr["ART_WIDTH"].ToString());
                    dto.Comprimento = decimal.Parse(dr["ART_HEIGHT"].ToString());

                    if (!lista.Exists(t => t.Codigo == dto.Codigo))
                    {
                        lista.Add(dto);
                    }
                    else
                    {
                        var ix = lista.FindIndex(t => t.Codigo == dto.Codigo);
                        if(dto.CustomerID == lista[ix].CustomerID)
                        {

                            var Qty = lista[ix].Quantidade;
                            dto.WareHouseName = "TODOS";
                            dto.ArmazemID = -1;
                            dto.Quantidade += Qty;
                            dto.ValorStockPVP = dto.Quantidade * dto.PrecoVenda;
                            dto.ValorStockPCM = (decimal.Parse(dr[11].ToString() ?? "0") * dto.Quantidade);
                            dto.MargemLucro = dto.ValorStockPVP - dto.ValorStockPCM;
                            dto.InfoStock = new StockDAO().ObterPorArtigo(dto);
                            lista[ix] = dto;
                        }
                    }

                    dto.PrecoConvenio = decimal.Parse(dr["COB_PRECO_ACORDADO"].ToString());
                    dto.ValorEntidade = decimal.Parse(dr["COB_VALOR_PARCEIRO"].ToString());
                    dto.ValorUtente = decimal.Parse(dr["COB_VALOR_UTENTE"].ToString());


                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }

        public List<ArtigoDTO> GetRetailList(ArtigoDTO dto)
        {
            
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_GER_ARTIGO_RETAIL_LIST";

                bdContext.AddParameter("@PRODUCT_ID", dto.Codigo);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ArtigoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        CodigoBarras = dr[1].ToString(),
                        Referencia = dr[2].ToString(),
                        Designacao = dr[3].ToString().ToUpper(),
                        RetailID = int.Parse(dr[4].ToString()),
                        IncomeUnit = int.Parse(dr[5].ToString()),
                        ReferenceUnit = int.Parse(dr[6].ToString()),
                        OutComeUnit = int.Parse(dr[7].ToString()),
                        WareHouseName = dr[8].ToString(),
                    };
                    dto.Quantidade = new StockDAO().StockActualArmazem(dto.Codigo, int.Parse(dto.WareHouseName), DateTime.MaxValue, DateTime.MaxValue).Actual;
                lista.Add(dto); 
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }

        public List<ArtigoDTO> GetBestSellers(ArtigoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_FIN_ARTIGOS_MAIS_VENDIDO";

                bdContext.AddParameter("@FILIAL", dto.Filial);
                bdContext.AddParameter("@ANO", dto.AnoFabrico==null ? "-1" : dto.AnoFabrico);
                bdContext.AddParameter("@WAREHOUSE_ID", dto.ArmazemID);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ArtigoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Designacao = dr[1].ToString().ToUpper(),
                        Quantidade = decimal.Parse(dr[2].ToString()),
                        ValorStockPVP = decimal.Parse(dr[2].ToString()) * decimal.Parse(dr[3].ToString()),
                        PrecoVenda = decimal.Parse(dr[3].ToString()),
                        FotoArtigo = dr[4].ToString(), 
                        MovimentaStock = dr[5].ToString() == "1" ? true : false,
                        PedidoCozinha = dr[6].ToString() == "1" ? short.Parse("1") : short.Parse("0"),
                        RestProduct = dr[7].ToString() == "1" ? short.Parse("1") : short.Parse("0"),
                       /* Preco = decimal.Parse(dr[8].ToString()),
                        ImpostoID = int.Parse(dr[9].ToString() == "" ? "-1" : dr[9].ToString()),
                        PercentualImposto = decimal.Parse(dr[10].ToString()), 
                        Desconto = decimal.Parse(dr[11].ToString()),*/
                    };/*
                    dto.Preco = dto.Desconto > 0 ? dto.Preco - (dto.Preco * (dto.Desconto / 100)) : dto.Preco;
                    dto.ImpostoLiquido = dr[8].ToString() == "" || dr[8].ToString() == "0" ? dto.Preco * (dto.PercentualImposto / 100) : decimal.Parse(dr[8].ToString());
                    */
                    if (dto.Quantidade > 0)
                    {
                        if (lista.Exists(t => t.Codigo == dto.Codigo))
                        {
                            var product = lista.Where(t => t.Codigo == dto.Codigo).SingleOrDefault();

                            dto.Quantidade += product.Quantidade;
                            dto.ValorStockPVP = (dto.ValorStockPVP + product.ValorStockPVP); // VALOR VENDIDO
                            dto.PrecoVenda = ((dto.PrecoVenda + product.PrecoVenda) / 2);


                            lista = lista.Where(t => t.Codigo != product.Codigo).ToList();
                        }
                        lista.Add(dto);
                    }
                      
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
                 
            }
            return lista;
        }



        public List<ArtigoDTO> ObterArtigosVendidosPorPeriodo(ArtigoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_COM_OBTERARTIGOS_VENDIDOS_ENTRE_DATAS";

                bdContext.AddParameter("@FILIAL", dto.Filial);
                bdContext.AddParameter("@DATA_INI", dto.DataCadastro);
                bdContext.AddParameter("@DATA_FIN", dto.DataValidade);
                bdContext.AddParameter("@WAREHOUSE_ID", dto.ArmazemID);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    dto = new ArtigoDTO
                    {
                        Designacao = dr[0].ToString().ToUpper(),
                        PrecoCusto = decimal.Parse(dr[1].ToString()),
                        Quantidade = decimal.Parse(dr[2].ToString()),
                        PrecoVenda = decimal.Parse(dr[3].ToString()),
                        ValorStockPVP = decimal.Parse(dr[6].ToString()),
                        Desconto = decimal.Parse(dr[4].ToString()),
                        ImpostoLiquido = decimal.Parse(dr[5].ToString()),
                        MargemLucro = decimal.Parse(dr[7].ToString()),
                        Categoria = dr[8].ToString(),
                        SubCategoria = dr[9].ToString(),
                        CurtaDescricao = dr[10].ToString() 
                    };
                    
                    if(dto.MargemLucro > 0 && dto.PrecoCusto == 0)
                    {
                        dto.PrecoCusto = dto.PrecoVenda - (dto.PrecoVenda * (dto.MargemLucro / 100));
                    }
                    
                    if(lista.Exists(t=>t.Designacao == dto.Designacao))
                    {
                         
                        var product = lista.Where(t => t.Designacao == dto.Designacao).SingleOrDefault();
                        lista = lista.Where(t => t.Designacao != product.Designacao).ToList();  

                        dto.PrecoCusto = (dto.PrecoCusto + product.PrecoCusto) / 2;
                        dto.PrecoVenda = (dto.PrecoVenda + product.PrecoVenda) / 2; 
                        dto.Quantidade += product.Quantidade;
                        dto.ValorStockPVP +=product.ValorStockPVP;
                        dto.ImpostoLiquido += product.ImpostoLiquido;
                        dto.Desconto += product.Desconto;
                         
                    }

                    dto.PrevisaoLucro = dto.PrecoCusto > 0 ? dto.ValorStockPVP - (dto.PrecoCusto * dto.Quantidade) : 0; //(dto.PrecoVenda - dto.PrecoCusto) * dto.Quantidade;
                    if (dto.PrevisaoLucro > 0)
                        dto.MargemLucro = (dto.PrecoVenda * dto.Quantidade) / ((dto.PrecoVenda * dto.Quantidade) * 100);
                    else
                        dto.MargemLucro = dto.PrecoVenda * dto.Quantidade;

                    dto.ValorLiquido = (dto.ValorStockPVP - dto.Desconto) + dto.ImpostoLiquido;
                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        } 

        public List<ArtigoDTO> ObterArtigoStockNegativo(ArtigoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_COM_OBTER_ARTIGO_STOCK_NEGATIVO";

                bdContext.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    dto = new ArtigoDTO();
                    dto.Designacao = dr[0].ToString().ToUpper();
                    dto.Quantidade = decimal.Parse(dr[1].ToString());
                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }

        public List<ArtigoDTO> ObterPorCategoria(ArtigoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_GER_ARTIGO_OBTERPORCATEGORIA";

                bdContext.AddParameter("@ID_CATEGORIA", dto.Categoria);
                bdContext.AddParameter("@WAREHOUSE", dto.WareHouseName);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ArtigoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        CodigoBarras = dr[1].ToString(),
                        Referencia = dr[2].ToString(), 
                        Designacao = dr[4].ToString(),
                        Categoria = dr[5].ToString(), 
                        OutComeUnit = int.Parse(dr[7].ToString()),
                        UnidadeVenda = dr[7].ToString(),
                        PrecoCusto = decimal.Parse(dr[8].ToString() == string.Empty ? "0" : dr[8].ToString()),
                        PrecoCustoMedioPadrao = decimal.Parse(dr[8].ToString()),
                        ImpostoID = int.Parse(dr[9].ToString()),
                        PrecoVenda = decimal.Parse(dr[10].ToString()==string.Empty ? "0" : dr[10].ToString()), 
                        FotoArtigo = dr[11].ToString(),
                        MovimentaStock = dr["ART_MOVIMENTA_STOCK"].ToString() == "1" ? true : false,
                        PedidoCozinha = short.Parse(dr["ART_PEDIDO_COZINHA"].ToString() == "" ? "0" : dr["ART_PEDIDO_COZINHA"].ToString()),
                        ControladoPorLotes = short.Parse(dr["ART_ALLOW_LOTE"].ToString() == "" ? "0" : dr["ART_ALLOW_LOTE"].ToString()),
                        TemDimensoes = short.Parse(dr["ART_ALLOW_DIMENSOES"].ToString() == "" ? "0" : dr["ART_ALLOW_DIMENSOES"].ToString()),
                        IsComposto = short.Parse(dr["ART_COMPOSTO"].ToString() == "" ? "0" : dr["ART_COMPOSTO"].ToString()),
                        MultiplosPrecos = short.Parse(dr["ART_TABELA_PRECO"].ToString() == "" ? "0" : dr["ART_TABELA_PRECO"].ToString()),
                        Descontinuado = short.Parse(dr["ART_DESCONTINUADO"].ToString() == "" ? "0" : dr["ART_DESCONTINUADO"].ToString()),
                        Quantidade = dr["STOCK_QUANTIDADE"].ToString() != "" ? decimal.Parse(dr["STOCK_QUANTIDADE"].ToString()) : 0,
                        RestProduct = dr["ART_RESTPOS"].ToString() == "1" ? short.Parse("1") : short.Parse("0"),
                        PercentualImposto = dr["ART_PERCENT_IMPOSTO"].ToString() == string.Empty ? 0 : decimal.Parse(dr["ART_PERCENT_IMPOSTO"].ToString()),
                        ImpostoLiquido = decimal.Parse(dr["ART_VALOR_IMPOSTO"].ToString() == string.Empty ? "0" : dr["ART_VALOR_IMPOSTO"].ToString()),
                        Imposto = dr["IMP_SIGLA"].ToString()


                    };  

                    if (dto.Quantidade == 0)
                    {
                        StockInfoDTO stockInfo = new StockDAO().StockActualArmazem(dto.Codigo, (dto.WareHouseName == string.Empty || dto.WareHouseName == null) ? 1 : int.Parse(dto.WareHouseName),
                    DateTime.MaxValue, DateTime.MaxValue);
                        dto.Quantidade = stockInfo.Actual;
                    }
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }

        

        private bool IsNumeric(string pInput)
        {
            int text;

            return int.TryParse(pInput, out text);
        }



        public List<ArtigoDTO> ObterStock(StockInfoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                lista = new StockDAO().ObterPorFiltro(dto);
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }

        public List<ItemFaturacaoDTO> ObterSalesExtract(ItemFaturacaoDTO dto)
        {

            List<ItemFaturacaoDTO> lista = new List<ItemFaturacaoDTO>();
            try
            {
                bdContext.ComandText = "stp_COM_ARTIGO_EXTRACTO_VENDAS";


                bdContext.AddParameter("@ARTIGO", dto.Artigo);
                bdContext.AddParameter("@DATA_INI", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                bdContext.AddParameter("@DATA_TERM", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);
                bdContext.AddParameter("@SALESMAN_ID", dto.FuncionarioID?? "-1");

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ItemFaturacaoDTO
                    {
                        Referencia = dr[0].ToString(),
                        DataEntrada = !string.IsNullOrEmpty(dr[1].ToString()) ? DateTime.Parse(dr[1].ToString()) : DateTime.MinValue,
                        Designacao = dr[2].ToString(), 
                        PrecoCusto = decimal.Parse(dr[3].ToString()),
                        Quantidade = decimal.Parse(dr[4].ToString()),
                        PrecoUnitario = decimal.Parse(dr[5].ToString()),
                        ValorDesconto = decimal.Parse(dr[6].ToString()),
                        ValorImposto = decimal.Parse(dr[7].ToString()),
                        TotalLiquido = decimal.Parse(dr[8].ToString()), 
                        ValorDescontoFinanceiro = !string.IsNullOrEmpty(dr[9].ToString()) ? decimal.Parse(dr[9].ToString()) : 0,
                        DesignacaoEntidade = dr[10].ToString(),
                        Comissao = !string.IsNullOrEmpty(dr[11].ToString()) ? decimal.Parse(dr[11].ToString()) : 0,
                        TotalComissaoLinha= !string.IsNullOrEmpty(dr[12].ToString()) ? decimal.Parse(dr[12].ToString()) : 0,
                        FuncionarioID = dr[13].ToString(), 
                       
                    };
                    dto.TotalLiquido = (dto.Quantidade * dto.PrecoUnitario) - dto.ValorDesconto + dto.ValorImposto;
                    if(dto.ValorDescontoFinanceiro > 0 && dto.PrecoCusto <= 0)
                    {
                        dto.PrecoCusto = dto.PrecoUnitario - (dto.PrecoUnitario * (dto.ValorDescontoFinanceiro / 100));
                    }
                    dto.ValorDescontoFinanceiro = /*dto.ValorDescontoFinanceiro > 0 ? (dto.TotalLiquido * (dto.ValorDescontoFinanceiro / 100)) :*/ ((dto.PrecoUnitario - dto.PrecoCusto) * dto.Quantidade);

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                bdContext.FecharConexao();
            }

            return lista;
        }

        public void AddLaudryItem(ArtigoDTO dto)
        {
            try
            {

                bdContext.ComandText = "stp_LAV_VESTUARIO_ARTIGO_ADICIONAR";

                bdContext.AddParameter("@ARTIGO_ID", dto.Codigo);
                bdContext.AddParameter("@VESTUARIO_ID", dto.ArtigoVestuarioID);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);
                bdContext.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            } 
        }

        public List<ArtigoDTO> ObterArtigoVestuarioPorFiltro(ArtigoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_LAV_VESTURIO_ARTIGO_OBTERPORFILTRO";

                bdContext.AddParameter("CATEGORY_ID", dto.Categoria);
                bdContext.AddParameter("GENDER_ID", dto.ArtigoVestarioID);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    dto = new ArtigoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Designacao = dr[1].ToString();
                    dto.CurtaDescricao = dr[2].ToString();
                    dto.PrecoVenda = decimal.Parse(dr[3].ToString());
                    dto.FotoArtigo = dr[4].ToString();
                    dto.LookupField3 = dr[5].ToString();
                    dto.ImpostoID = int.Parse(dr["ART_CODIGO_IMPOSTO"].ToString());
                    dto.Imposto = dr["ART_IMPOSTO_INCLUIDO"].ToString();
                    dto.Desconto = decimal.Parse(dr["ART_DESCONTO"].ToString());
                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }
        public List<ArtigoDTO> GetBestSellersArtigoVestuario(ArtigoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            try
            {
                bdContext.ComandText = "stp_LAV_VESTURIO_ARTIGO_MAIS_VENDIDO";

                bdContext.AddParameter("@FILIAL", dto.Filial);
                bdContext.AddParameter("@ANO", dto.AnoFabrico);
                bdContext.AddParameter("@WAREHOUSE_ID", -1);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ArtigoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Designacao = dr[1].ToString().ToUpper(),
                        Quantidade = decimal.Parse(dr[2].ToString()),
                        ValorStockPVP = decimal.Parse(dr[2].ToString()) * decimal.Parse(dr[3].ToString()),
                        PrecoVenda = decimal.Parse(dr[3].ToString()),
                        FotoArtigo = dr[4].ToString(),
                        MovimentaStock = dr[5].ToString() == "1" ? true : false,
                        RestProduct = dr[6].ToString() == "1" ? short.Parse("1") : short.Parse("0"),
                        LookupField2 = dr[7].ToString(),
                        CurtaDescricao = dr["VEST_DESIGNACAO"].ToString(),
                        ImpostoID = int.Parse(dr["ART_CODIGO_IMPOSTO"].ToString()),
                        Imposto = dr["ART_IMPOSTO_INCLUIDO"].ToString(),
                        Desconto = decimal.Parse(dr["ART_DESCONTO"].ToString()),
                    };

                    if (dto.Quantidade > 0)
                    {
                        if (lista.Exists(t => t.Codigo == dto.Codigo))
                        {
                            var product = lista.Where(t => t.Codigo == dto.Codigo).SingleOrDefault();

                            dto.Quantidade += product.Quantidade;
                            dto.ValorStockPVP = (dto.ValorStockPVP + product.ValorStockPVP); // VALOR VENDIDO
                            dto.PrecoVenda = ((dto.PrecoVenda + product.PrecoVenda) / 2);


                            lista = lista.Where(t => t.Codigo != product.Codigo).ToList();
                        }
                        lista.Add(dto);
                    }

                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();

            }
            return lista;
        }

        public decimal GetSupplierLastPrice(ArtigoDTO dto)
        {
            decimal purchageLastPrice = 0;
            try
            {
                bdContext.ComandText = "stp_COM_ARTIGO_SUPPLIER_LAST_PRICE";

                bdContext.AddParameter("@SUPPLIER_ID", dto.Fornecedor);
                bdContext.AddParameter("@PRODUCT_ID", dto.Codigo);

                MySqlDataReader dr = bdContext.ExecuteReader();

                if (dr.Read())
                {
                    purchageLastPrice = decimal.Parse(dr[1].ToString());
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return purchageLastPrice;
        }


    }
}
