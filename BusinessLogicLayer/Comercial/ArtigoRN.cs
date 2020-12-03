using BusinessLogicLayer.Clinica;
using BusinessLogicLayer.Geral;
using DataAccessLayer.Comercial;
using DataAccessLayer.Comercial.Stock;
using Dominio.Comercial;
using Dominio.Comercial.Stock;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{
    public class ArtigoRN
    {
        private static ArtigoRN _instancia;
        private ArtigoDAO daoProduct;
        private StockDAO daoStock; 

        public ArtigoRN()
        {
            daoProduct = new ArtigoDAO();
            daoStock = new StockDAO();
        }

        public static ArtigoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ArtigoRN();
            }

            return _instancia;
        }

        public Tuple<int, string> Salvar(ArtigoDTO dto)
        {
            var _genericClass = new GenericRN();
            dto = daoProduct.Adicionar(dto);

            if (string.IsNullOrEmpty(dto.MensagemErro))
            {
                int ItemID = dto.Codigo;
                ProductPriceListRN.GetInstance().Gravar(dto.PricesList, dto);
                return new Tuple<int, string>(dto.Codigo, _genericClass.SuccessMessage());
            }
            else
            {
                string errorMessage = _genericClass.ErrorMessage(" Ops!!! Ocorreu um erro ao guardar os dados do artigo:" + dto.MensagemErro.Replace("'", string.Empty));
                if (dto.Codigo > 0)
                {
                    errorMessage += "window.location.href='FichaArtigo?pP=" + dto.Codigo + "'";
                }
                return new Tuple<int, string>(-1, errorMessage);
            }
        }

        public void Apagar(ArtigoDTO dto)
        {
            daoProduct.Excluir(dto);
        }

        public ArtigoDTO ObterPorPK(ArtigoDTO dto)
        {
            int warehouseID = dto.ArmazemID, tablePrice = dto.PriceID;
            dto = daoProduct.ObterPorPK(dto);

            if(warehouseID > 0)
            {
                dto.InfoStock = new List<StockInfoDTO>() { daoStock.StockActualArmazem(dto.Codigo, warehouseID, DateTime.MinValue, DateTime.Now) };
                dto.Quantidade = dto.InfoStock.Count == 1 ? dto.InfoStock[0].Actual : dto.Quantidade;
                dto.ArmazemID = warehouseID;
            }

            if(tablePrice > 0)
            {
                var productPrice = ProductPriceListRN.GetInstance().ObterPorPK(new ProductPriceListDTO { Codigo = dto.Codigo, PriceTableID = tablePrice });
                dto.PrecoVenda = productPrice.PrecoVenda > 0 ? productPrice.PrecoVenda : dto.PrecoVenda;
                dto.ImpostoLiquido = dto.PrecoVenda * (dto.PercentualImposto / 100);
                dto.Preco = dto.PrecoVenda;
            }

            return dto;
        }

        public List<ArtigoDTO> ObterPorFiltro(ArtigoDTO dto)
        {
            return daoProduct.ObterPorFiltro(dto);
        }

        public List<ArtigoDTO> ObterPorTodos(string pFilial, string pUtilizador)
        {
            ArtigoDTO dto = new ArtigoDTO
            {
                Designacao = string.Empty,
                Referencia = string.Empty,
                CodigoBarras = string.Empty,
                Categoria = "-1",
                Filial = pFilial,
                Utilizador = pUtilizador,
                ValidadeIni = string.Empty,
                ValidadeTerm = string.Empty,
                WareHouseName = "-1"
            };
            return ObterPorFiltro(dto);
        }

        public List<ArtigoDTO> ObterPorFiltroForDropDowList(ArtigoDTO dto)
        {
            var productList = new List<ArtigoDTO>();

            foreach(var item in ObterPorFiltro(dto))
            {
                item.Designacao = item.Referencia + " - " + item.Designacao;
                productList.Add(item);
            }
            productList.Insert(0, new ArtigoDTO(-1, "-Todos-"));
            return productList;
        }

        public List<ArtigoDTO> ObterStockDown(List<ArtigoDTO> lista)
        {
            List<ArtigoDTO> RuptureList = new List<ArtigoDTO>();
            foreach(var item in lista.Where(p => p.MovimentaStock && (p.Quantidade <= p.StockMinimo /*|| (p.Quantidade - p.StockMinimo <=5)*/)).ToList())
            {
                item.QuantidadeRuptura = item.StockMinimo - item.Quantidade;
                RuptureList.Add(item);
            }

            return RuptureList;
        }

        public List<ArtigoDTO> ObterStockExcess(List<ArtigoDTO> lista)
        {
            List<ArtigoDTO> ExcessList = new List<ArtigoDTO>();
            foreach(var item in lista.Where(p => p.Quantidade > p.StockMaximo && p.MovimentaStock).ToList())
            {
                item.QuantidadeExcesso = item.Quantidade - item.StockMaximo;
                ExcessList.Add(item);
            }

            return ExcessList;
        }

        public List<ArtigoDTO> ObterPorValidade(List<ArtigoDTO> lista, ArtigoDTO dto)
        {
            lista = lista.Where(t => t.DataValidade > DateTime.MinValue && ((t.MovimentaStock && t.Quantidade > 0) || !t.MovimentaStock)).ToList();

            if (lista != null && lista.Count > 0)
            {
                if (!string.IsNullOrEmpty(dto.Tipo))
                {
                    if (dto.Tipo == "E")
                    {
                        // Lista Apenas Expirados 
                        lista = lista.Where(t => t.DataValidade < DateTime.Today).ToList();
                    }
                    else if (dto.Tipo == "P")
                    {
                        //Prestes a Expirar ou Expiram hoje
                        dto.ValidadeTerm = string.IsNullOrEmpty(dto.ValidadeTerm) ? DateTime.Today.ToString() : dto.ValidadeTerm;
                        lista = lista.Where(t => t.DataValidade >= DateTime.Parse(dto.ValidadeIni) && t.DataValidade <= DateTime.Parse(dto.ValidadeTerm)).ToList();
                    }
                } 
            }
            else
            {
                lista = new List<ArtigoDTO>();
            }
            
            return lista;
        }



        public List<ArtigoDTO> ObterProdutosMaisVendido(ArtigoDTO dto)
        {
            return daoProduct.GetBestSellers(dto).OrderByDescending(t => t.Quantidade).ToList();
        }

        public decimal calculaPrecoVenda(string pPrecoCusto, string pMargemLucro)
        {
            decimal custo = 0, margem = 0, venda = 0, margemreal = 0, percentagem = 0;
            if (!string.IsNullOrEmpty(pPrecoCusto))
            {
                custo = Convert.ToDecimal(pPrecoCusto);
            }

            if (!string.IsNullOrEmpty(pMargemLucro))
            {
                margem = Convert.ToDecimal(pMargemLucro);
            }
            if (margem > 0)
            {
                margemreal = (margem / 100);
                percentagem = (custo * margemreal);
                venda = (custo + percentagem); 
            }
            return venda;
        }

        public decimal calculaMargemLucro(string pPrecoCusto, string pPrecoVenda)
        {
            decimal custo = 0, venda = 0, lucro = 0, lucro1 = 0, lucro2 = 0;
            if (!string.IsNullOrEmpty(pPrecoCusto))
            {
                custo = Convert.ToDecimal(pPrecoCusto);
            }

            if (!string.IsNullOrEmpty(pPrecoVenda))
            {
                venda = Convert.ToDecimal(pPrecoVenda);
            }
            if (custo > 0 && venda > 0)
            {
                lucro = venda - custo;
                lucro1 = (lucro * 100);
                if (venda == 0)
                {
                    lucro2 = 0;
                }
                else
                {
                    lucro2 = (lucro1 / venda);
                }
            }


            return Math.Round(lucro2);
        }

        public List<ArtigoDTO> ListarStock(string pArmazem, string pArtigo, string pFilial)
        {
            StockInfoDTO dto = new StockInfoDTO();
            dto.ArmazemID = Convert.ToInt32(pArmazem);
            dto.DesignacaoArtigo = pArtigo;
            dto.Filial = pFilial;
            return daoStock.ObterPorFiltro(dto);
        }





        public List<string> GetProductsAutoCompleteForStock(string pArgs, string pField, string pFilial, int pArmazem, string pUtilizador)
        {
            List<string> prodList = new List<string>();


            ArtigoDTO dto = new ArtigoDTO(0);
            dto.Utilizador = pUtilizador;
            dto.WareHouseName = pArmazem.ToString();
            dto.Designacao = pArgs;
            dto.Referencia = pArgs;
            dto.CodigoBarras = pArgs;
            dto.Filial = pFilial;
            dto.PrecoVenda = new GenericRN().ValorDecimal(pArgs);
            List<ArtigoDTO> lista = ObterPorFiltro(dto).Where(t => /*t.ProductStatus == "A" &&*/ t.MovimentaStock == true).ToList();
            if(pArmazem > 0)
            {
                lista = lista.Where(t => t.ArmazemID == pArmazem).ToList();
            }

            if (pField != "BarCode")
            {
                foreach (var artigo in lista)
                {     
                    prodList.Add(artigo.Codigo + ";" + artigo.FotoArtigo.Replace("~", "..") + ";" + artigo.Referencia + ";" + artigo.Designacao + ";" + artigo.Categoria + ";" +
                        artigo.PrecoVenda + ";" + artigo.PrecoCusto + ";" + artigo.CodigoBarras + ";" + artigo.Quantidade + ";" + artigo.MovimentaStock + ";" + artigo.ValidadeIni + ";" +
                         artigo.ImpostoID + ";" + artigo.PedidoCozinha + ";" + artigo.PercentualImposto + ";" + artigo.PrecoCustoMedioPadrao + ";" + artigo.UnidadeCompra + ";" + artigo.ArmazemID + ";" + artigo.UnidadeVenda +
                         ";" + artigo.ImpostoIncluido + ";" + artigo.IsComposto + ";" + artigo.ControladoPorLotes + ";" + artigo.TemDimensoes + ";" + artigo.Tipo + ";" + artigo.ImpostoLiquido + ";" + artigo.LookupField11 + ";" + artigo.WareHouseName + ";" +
                         artigo.IncomeUnit + ";" + artigo.ReferenceUnit + ";" + artigo.OutComeQuantity+";"+artigo.UnidadeID+ ";"+artigo.Comprimento+ ";"+artigo.Largura); 
                }
            }
            else
            {
                //List<ArtigoDTO> artigo = ObterPorFiltro(dto).Find(p => p.CodigoBarras == args) == null ? new ArtigoDTO() : ObterPorFiltro(dto).Find(p => p.CodigoBarras == args);
                foreach (var artigo in lista)
                {
                    if (artigo.CodigoBarras == pArgs)
                    {
                        prodList.Add(artigo.Codigo + ";" + artigo.FotoArtigo.Replace("~", "..") + ";" + artigo.Referencia + ";" + artigo.Designacao + ";" + artigo.Categoria + ";" +
                       artigo.PrecoVenda + ";" + artigo.PrecoCusto + ";" + artigo.CodigoBarras + ";" + artigo.Quantidade + ";" + artigo.MovimentaStock + ";" + artigo.ValidadeIni + ";" +
                        artigo.ImpostoID + ";" + artigo.PedidoCozinha + ";" + artigo.PercentualImposto + ";" + artigo.PrecoCustoMedioPadrao + ";" + artigo.UnidadeCompra + ";" + artigo.ArmazemID + ";" + artigo.UnidadeVenda +
                        ";" + artigo.ImpostoIncluido + ";" + artigo.IsComposto + ";" + artigo.ControladoPorLotes + ";" + artigo.TemDimensoes + ";" + artigo.Tipo + ";" + artigo.ImpostoLiquido + ";" + artigo.LookupField11 + ";" + artigo.WareHouseName + ";" +
                         artigo.IncomeUnit + ";" + artigo.ReferenceUnit + ";" + artigo.OutComeQuantity + ";" + artigo.Desconto + ";" + artigo.UnidadeID+";" + artigo.Comprimento + ";" + artigo.Largura);
                    }
                }
            }

            return prodList;

        }

        public List<string> GetProductsAutoComplete(string args, string field, string pFilial, string pUtilizador)
        {
            List<string> prodList = new List<string>();


            ArtigoDTO dto = new ArtigoDTO(0);
            dto.Utilizador = pUtilizador;
            dto.Designacao = args;
            dto.Referencia = args;
            dto.CodigoBarras = args;
            dto.Filial = pFilial;
            dto.PrecoVenda = new GenericRN().ValorDecimal(args);
            List<ArtigoDTO> lista = ObterPorFiltro(dto)/*.Where(t=> t.ProductStatus == "A" && (!t.MovimentaStock || (t.MovimentaStock && t.Quantidade > 0))).ToList()*/;
            if (field != "BarCode")
            {
                
                foreach (var artigo in lista)
                {
                    prodList.Add(artigo.Codigo + ";" + artigo.FotoArtigo.Replace("~", "..") + ";" + artigo.Referencia + ";" + artigo.Designacao + ";" + artigo.Categoria + ";" +
                        artigo.PrecoVenda + ";" + artigo.PrecoCusto + ";" + artigo.CodigoBarras + ";" + artigo.Quantidade + ";" + artigo.MovimentaStock + ";" + artigo.ValidadeIni + ";" +
                         artigo.ImpostoID + ";" + artigo.PedidoCozinha + ";" + artigo.PercentualImposto + ";" + artigo.PrecoCustoMedioPadrao + ";" + artigo.UnidadeCompra + ";" + artigo.ArmazemID + ";" + artigo.UnidadeVenda +
                         ";" + artigo.ImpostoIncluido + ";" + artigo.IsComposto + ";" + artigo.ControladoPorLotes + ";" + artigo.TemDimensoes + ";" + artigo.Tipo + ";" + artigo.ImpostoLiquido + ";" + artigo.LookupField11 + ";" +
                         artigo.WareHouseName + ";" + artigo.Desconto + ";" + artigo.ComercialDescription + ";" + (artigo.WithRetention ? 1 : 0) + ";" + artigo.CurrencyID + ";" 
                         + artigo.UnidadeID + ";" + artigo.CustomerID); 
                }
            }
            else
            {
                //List<ArtigoDTO> artigo = ObterPorFiltro(dto).Find(p => p.CodigoBarras == args) == null ? new ArtigoDTO() : ObterPorFiltro(dto).Find(p => p.CodigoBarras == args);
                foreach (var artigo in lista)
                {
                    if (artigo.CodigoBarras == args)
                    {
                        prodList.Add(artigo.Codigo + ";" + artigo.FotoArtigo.Replace("~", "..") + ";" + artigo.Referencia + ";" + artigo.Designacao + ";" + artigo.Categoria + ";" +
                       artigo.PrecoVenda + ";" + artigo.PrecoCusto + ";" + artigo.CodigoBarras + ";" + artigo.Quantidade + ";" + artigo.MovimentaStock + ";" + artigo.ValidadeIni + ";" +
                        artigo.ImpostoID + ";" + artigo.PedidoCozinha + ";" + artigo.PercentualImposto + ";" + artigo.PrecoCustoMedioPadrao + ";" + artigo.UnidadeCompra + ";" + artigo.ArmazemID + ";" + artigo.UnidadeVenda +
                        ";" + artigo.ImpostoIncluido + ";" + artigo.IsComposto + ";" + artigo.ControladoPorLotes + ";" + artigo.TemDimensoes + ";" + artigo.Tipo + ";" +
                        artigo.ImpostoLiquido + ";" + artigo.LookupField11 + ";" + artigo.WareHouseName + ";" + artigo.Desconto + ";" + artigo.ComercialDescription + ";" + 
                        (artigo.WithRetention ? 1 : 0)+";"+artigo.CurrencyID + ";" + artigo.UnidadeID + ";" + artigo.CustomerID);
                    }
                }
            }

            return prodList;
        }


        public List<string> GetSalesProductsAutoComplete(string args, string field, string pFilial, int pArmazem, string pUtilizador)
        {
            List<string> prodList = new List<string>();

            ArtigoDTO dto = new ArtigoDTO(0);
            dto.Utilizador = pUtilizador;
            dto.Designacao = args;
            dto.Referencia = args;
            dto.CodigoBarras = args;
            dto.Filial = pFilial;
            dto.PrecoVenda = new GenericRN().ValorDecimal(args);
            dto.WareHouseName = pArmazem.ToString();

            List<ArtigoDTO> lista = ObterPorFiltro(dto);
            bool CanAdd = true;
            foreach (var artigo in lista)
            {
                if (field == "BarCode" && artigo.CodigoBarras != args)
                {
                    CanAdd = false;
                }

                if (!artigo.MovimentaStock || (artigo.MovimentaStock && artigo.Quantidade > 0) && CanAdd)
                {
                    if (artigo.MovimentaStock && pArmazem <= 0 && artigo.InfoStock.Count > 0)
                    {
                        var WarehouseStockInfo = artigo.InfoStock.Where(t => t.Actual > 0).ToList().FirstOrDefault();
                        if (WarehouseStockInfo != null)
                        {
                            artigo.ArmazemID = WarehouseStockInfo.ArmazemID;
                            artigo.WareHouseName = WarehouseStockInfo.WareHouseName;
                            artigo.Quantidade = WarehouseStockInfo.Actual;
                        }
                        else
                        {
                            CanAdd = false;
                        }
                    }  

                    if (CanAdd)
                    {

                       prodList.Add(artigo.Codigo + ";" + artigo.FotoArtigo.Replace("~", "..") + ";" + artigo.Referencia + ";" + artigo.Designacao + ";" + artigo.Categoria + ";" +
                       artigo.PrecoVenda + ";" + artigo.PrecoCusto + ";" + artigo.CodigoBarras + ";" + artigo.Quantidade + ";" + artigo.MovimentaStock + ";" + artigo.ValidadeIni + ";" +
                       artigo.ImpostoID + ";" + artigo.PedidoCozinha + ";" + artigo.PercentualImposto + ";" + artigo.PrecoCustoMedioPadrao + ";" + artigo.UnidadeCompra + ";" + artigo.ArmazemID + ";" + artigo.UnidadeVenda +
                       ";" + artigo.ImpostoIncluido + ";" + artigo.IsComposto + ";" + artigo.ControladoPorLotes + ";" + artigo.TemDimensoes + ";" + artigo.Tipo + ";" + 
                       artigo.ImpostoLiquido + ";" + artigo.LookupField11 + ";" + artigo.WareHouseName + ";" + artigo.Desconto + ";" + artigo.ComercialDescription + ";" +
                       (artigo.WithRetention ? 1 : 0)+";"+artigo.CurrencyID + ";" + artigo.UnidadeID);
                    }
                }
            }


            return prodList;
        }
        public string GetProductStockInfo(ArtigoDTO dto)
        {
            dto = ObterPorFiltro(dto)[0];

            return " Existência em Stock: " + dto.Quantidade;
        }


        public List<ArtigoDTO> ObterProdutosVendidosPorPeriodo(ArtigoDTO dto)
        {
            return daoProduct.ObterArtigosVendidosPorPeriodo(dto);
        }

        public List<ArtigoDTO> ObterArtigoStockNegativo(ArtigoDTO dto)
        {
            return daoProduct.ObterArtigoStockNegativo(dto);
        }

        public List<ArtigoDTO> ObterPorCategoria(ArtigoDTO dto)
        {
            dto.WareHouseName = dto.WareHouseName == null ? "-1": dto.WareHouseName;
            return daoProduct.ObterPorCategoria(dto);
        }


        public List<ArtigoDTO> ConsultaStock(StockInfoDTO dto) => daoProduct.ObterStock(dto);

        public List<ItemMovimentoStockDTO> GetAllProductToInventoryList(string pWarehouseID, string pFilial, string pFamily, int pLoteID, int pSerialID, int pSizeID)
        {
            StockInfoDTO dto = new StockInfoDTO();
            dto.ArmazemID = Convert.ToInt32(pWarehouseID);
            dto.DesignacaoArtigo = string.Empty;
            dto.Filial = pFilial;
            var product = new ArtigoDTO();
            product.LoteID = pLoteID;
            product.DimesaoID = pSizeID;
            product.SerialNumberID = pSerialID;
            product.SemelhanteID = -1;
            product.Categoria = pFamily;
            product.Utilizador = string.Empty;
            dto.Product = product;
            if (dto.ArmazemID > 0)
                return daoStock.GetToInventory(dto);
            else
                return new List<ItemMovimentoStockDTO>();
        }



        public List<ItemFaturacaoDTO> GetComposeItemList(ArtigoDTO dto, List<ItemFaturacaoDTO> productList)
        {
            int OrderNumber = productList.Count + 1;
            foreach (var item in new ComposicaoDAO().ObterPorFiltro(dto))
            {
                ItemFaturacaoDTO _item = new ItemFaturacaoDTO();

                _item.NroOrdenacao = OrderNumber;
                OrderNumber++;
                _item.ComposeID = item.ArtigoVirtualID;
                _item.Artigo = item.Codigo;
                _item.Referencia = item.Referencia;
                _item.Designacao = item.Designacao;
                _item.PrecoUnitario = item.PrecoVenda;
                _item.Quantidade = item.Quantidade;
                _item.Desconto = item.Desconto;
                _item.Imposto = item.PercentualImposto;
                _item.TotalLiquido = item.PrecoCusto;
                _item.MovimentaStock = item.MovimentaStock;
                _item.Notas = string.Empty;
                _item.ArmazemID = int.Parse(item.WareHouseName);
                _item.Composicao = false;

                productList.Add(_item);

            }


            return productList;
        }

        public List<StockInfoDTO> GetDefaultProductWarehouseList()
        {
            var lista = new List<StockInfoDTO>();
            foreach (var warehouse in ArmazemRN.GetInstance().ObterPorFiltro(new ArmazemDTO(-1, "", "")))
            {
                var dto = new StockInfoDTO();
                dto.ArmazemID = warehouse.Codigo;
                dto.WareHouseName = warehouse.Descricao;
                dto.ArtigoID = -1;
                dto.Actual = 0;
                dto.Minima = 0;
                dto.Maxima = 0;

                lista.Add(dto);
            }

            return lista;
        }

        public List<ItemFaturacaoDTO> ObterSalesExtract(ItemFaturacaoDTO dto)
        {
            var ExtractList = daoProduct.ObterSalesExtract(dto);
            if (dto.LookupDate1 != DateTime.MinValue && dto.LookupDate2 != DateTime.MinValue)

                ExtractList = ExtractList.Where(t => t.DataEntrada >= dto.LookupDate1 && t.DataEntrada <= dto.LookupDate2).ToList();
            else if (dto.LookupDate1 != DateTime.MinValue && dto.LookupDate2 == DateTime.MinValue)
                ExtractList = ExtractList.Where(t => t.DataEntrada >= dto.LookupDate1).ToList();
            else
            if (dto.LookupDate1 == DateTime.MinValue && dto.LookupDate2 != DateTime.MinValue)
                ExtractList = ExtractList.Where(t => t.DataEntrada <= dto.LookupDate2).ToList();

            return ExtractList;

        }

        public List<ArtigoDTO> ObterRetailProductList(ArtigoDTO dto)
        {
            return daoProduct.GetRetailList(dto);
        }

        public List<ArtigoDTO> GetStockLote(LoteDTO dto)
        {
            var Product = new StockInfoDTO();
            Product.LookupNumericField1 = dto.Codigo;
            Product.ArmazemID = int.Parse(dto.WareHouseName);
            Product.Filial = "-1";
            Product.DesignacaoArtigo = "";
            Product.Reference = "";
            Product.BarCode = "";
            return daoProduct.ObterStock(Product);
        }


        public List<ArtigoDTO> GetProductListForHealtSales(string pUtilizador, string pArgs, string pFilial, string pArmazem, int pConvenioID, int pProductID)
        {

            ArtigoDTO dto = new ArtigoDTO(0)
            {
                Utilizador = pUtilizador,
                Designacao = pArgs,
                Referencia = pArgs,
                CodigoBarras = pArgs,
                Filial = pFilial,
                PrecoVenda = new GenericRN().ValorDecimal(pArgs),
                WareHouseName = pArmazem,
                ConvenioID = pConvenioID,
                Codigo = pProductID
            };

            List<ArtigoDTO> lista = ObterPorFiltro(dto);
            if(pProductID <=0)
             lista.Insert(0, new ArtigoDTO { Codigo = -1, Designacao = "-SELECCIONE-" });

            return lista;


        }

        public List<ArtigoDTO> ObterProdutosVendidosPorCategoria(ArtigoDTO dto)
        {
            var CategoryList = CategoriaRN.GetInstance().ObterPorFiltro(new CategoriaDTO
            {
                Codigo = -1,
                Descricao = "",
                Sigla = "",
                Categoria = "-1",
                Filial = dto.Filial
            });
            var productList = ObterProdutosVendidosPorPeriodo(dto);
            var CategoryBillList = new List<ArtigoDTO>();

            foreach (var category in CategoryList)
            {
                var ProductCategoryList = productList.Where(t => t.Categoria == category.Codigo.ToString() || t.SubCategoria == category.Codigo.ToString()).ToList();

                if (ProductCategoryList != null)
                {
                    dto = new ArtigoDTO
                    {
                        Designacao = category.Descricao.ToUpper(),
                        PrecoCusto = ProductCategoryList.Sum(t => t.PrecoCusto),
                        Quantidade = ProductCategoryList.Sum(t => t.Quantidade),
                        PrecoVenda = ProductCategoryList.Sum(t => t.PrecoVenda),
                        ValorStockPVP = ProductCategoryList.Sum(t => t.ValorStockPVP),
                        Desconto = ProductCategoryList.Sum(t => t.Desconto),
                        ImpostoLiquido = ProductCategoryList.Sum(t => t.ImpostoLiquido),
                        PrevisaoLucro = ProductCategoryList.Sum(t => t.PrevisaoLucro)
                    };

                    CategoryBillList.Add(dto);
                }
            }

            return CategoryBillList.OrderBy(t => t.Designacao).ToList();
        }

        public string ObterVendasPorSubFamilias(ArtigoDTO dto)
        {
            string tableColumns = "";
            var CategoryList = CategoriaRN.GetInstance().ObterPorFiltro(new CategoriaDTO
            {
                Codigo = -1,
                Descricao = "",
                Sigla = "",
                Categoria = "-1",
                Filial = dto.Filial
            });
            var productList = ObterProdutosVendidosPorPeriodo(dto).OrderBy(t => t.Designacao).ToList();
            int CategoryID = (dto.Categoria != null && dto.Categoria != "-1" && dto.Categoria != "" && dto.Categoria != "0") ? int.Parse(dto.Categoria) : -1;
            if (dto.Categoria != null && dto.Categoria != "-1" && dto.Categoria != "" && dto.Categoria != "0")
            {
                CategoryList = CategoryList.Where(t => t.Codigo == int.Parse(dto.Categoria)).ToList();
                
            }
            
            int SubCategoryID = (dto.SubCategoria != null && dto.SubCategoria != "" && dto.SubCategoria != "-1" && dto.SubCategoria != "0") ? int.Parse(dto.SubCategoria) : -1;
            foreach (var category in CategoryList)
            {
                List<ArtigoDTO> ProductCategoryList = new List<ArtigoDTO>();
                if(SubCategoryID > 0)
                {
                    ProductCategoryList = productList.Where(t => t.Categoria == SubCategoryID.ToString()).ToList();
                }
                else
                {
                    ProductCategoryList = productList.Where(t => t.Categoria == category.Codigo.ToString() || t.SubCategoria == category.Codigo.ToString()).ToList();
                }

                decimal totalCategoryBilling = SubCategoryID > 0 ? ProductCategoryList.Where(t=>t.Categoria == SubCategoryID.ToString()).Sum(t => (t.ValorStockPVP - t.Desconto) + t.ImpostoLiquido)
                    : ProductCategoryList.Sum(t => (t.ValorStockPVP - t.Desconto) + t.ImpostoLiquido);
                if (ProductCategoryList != null && ProductCategoryList.Sum(t => t.Quantidade) > 0)
                {
                    tableColumns += "<tr style='font-size:10px'>" +
                    "<td colspan='9' style='background-color:#FFA500; text-align:left'><b>" + category.Descricao.ToUpper() + "</b></td><tr>";

                    var categoryItem = SubCategoryID > 0 ? ProductCategoryList.Where(t => t.Categoria == SubCategoryID.ToString()).ToList()
                    : ProductCategoryList.Where(t => t.Categoria == category.Codigo.ToString()).ToList();

                    decimal total = (categoryItem.Sum(t => t.ValorStockPVP) - categoryItem.Sum(t => t.Desconto)) + categoryItem.Sum(t => t.ImpostoLiquido);
                    if (total > 0 && SubCategoryID <=0)
                    {
                        tableColumns += "<tr style='font-size:10px'><td style='text-align: left; background-color:yellow'>&nbsp" + category.Codigo + " - " + category.Descricao.ToUpper() + "</td>" +
                    "<td style='text-align: center; background-color:yellow'>&nbsp" + String.Format("{0:N2}", categoryItem.Sum(t => t.Quantidade)) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>&nbsp" + String.Format("{0:N2}", categoryItem.Sum(t => t.PrecoCusto)) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", categoryItem.Sum(t => t.PrecoVenda)) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", categoryItem.Sum(t => t.ValorStockPVP)) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", categoryItem.Sum(t => t.Desconto)) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", categoryItem.Sum(t => t.ImpostoLiquido)) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", total) + "</td>" +
                    "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", categoryItem.Sum(t => t.PrevisaoLucro)) + "</td></tr>";

                        if (dto.LookupField1 != null && dto.LookupField1 != "")
                        {
                            string prodID = string.Empty;
                            foreach (var item in categoryItem.ToList())
                            {
                                if (prodID != item.Designacao)
                                {
                                    var product = categoryItem.Where(t => t.Designacao == item.Designacao).ToList();
                                    tableColumns += "<tr style='font-size:10px'><td style='text-align: left'>&nbsp" + item.Designacao.ToUpper() + "</td>" +
                                    "<td style='text-align: center'>&nbsp" + String.Format("{0:N2}", product.Sum(t => t.Quantidade)) + "</td>" +
                                    "<td style='text-align: right'>&nbsp" + String.Format("{0:N2}", product.Sum(t => t.PrecoCusto)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.PrecoVenda)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ValorStockPVP)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.Desconto)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ImpostoLiquido)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ValorLiquido)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.PrevisaoLucro)) + "</td></tr>";
                                    prodID = item.Designacao;
                                }
                            }
                        }

                    }

                    var SubCategoryList = SubCategoryID > 0 ? productList.Where(t => t.Categoria == SubCategoryID.ToString()).ToList() : productList.Where(t => t.SubCategoria == category.Codigo.ToString()).ToList();
                    int id = 0;
                    if(SubCategoryList.Count > 0)
                    {
                        foreach (var subCategoryItem in SubCategoryList.ToList())
                        {
                            id = int.Parse(subCategoryItem.Categoria);

                            var SubCategoryLine = SubCategoryList.Where(t => t.Categoria == id.ToString()).ToList();
                            if (SubCategoryLine.Count > 0)
                            {
                                decimal totalLiquido = (SubCategoryLine.Sum(t => t.ValorStockPVP) - SubCategoryLine.Sum(t => t.Desconto)) + SubCategoryLine.Sum(t => t.ImpostoLiquido);
                                tableColumns += "<tr style='font-size:10px'><td style='text-align: left ; background-color:yellow'>&nbsp" + subCategoryItem.Categoria + " - " + subCategoryItem.CurtaDescricao.ToUpper() + "</td>" +
                                "<td style='text-align: center; background-color:yellow'>&nbsp" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.Quantidade)) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>&nbsp" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.PrecoCusto)) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.PrecoVenda)) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.ValorStockPVP)) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.Desconto)) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.ImpostoLiquido)) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", totalLiquido) + "</td>" +
                                "<td style='text-align: right; background-color:yellow'>" + String.Format("{0:N2}", SubCategoryLine.Sum(t => t.PrevisaoLucro)) + "</td></tr>";
                            }
                            if (dto.LookupField1 != null && dto.LookupField1 != "")
                            {
                                string prodID = string.Empty;
                                foreach (var item in SubCategoryLine)
                                {
                                    if (prodID != item.Designacao)
                                    {
                                        var product = SubCategoryList.Where(t => t.Designacao == item.Designacao).ToList();
                                        tableColumns += "<tr style='font-size:10px'><td style='text-align: left'>&nbsp" + item.Designacao.ToUpper() + "</td>" +
                                        "<td style='text-align: center'>&nbsp" + String.Format("{0:N2}", product.Sum(t => t.Quantidade)) + "</td>" +
                                        "<td style='text-align: right'>&nbsp" + String.Format("{0:N2}", product.Sum(t => t.PrecoCusto)) + "</td>" +
                                        "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.PrecoVenda)) + "</td>" +
                                        "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ValorStockPVP)) + "</td>" +
                                        "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.Desconto)) + "</td>" +
                                        "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ImpostoLiquido)) + "</td>" +
                                        "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ValorLiquido)) + "</td>" +
                                        "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.PrevisaoLucro)) + "</td></tr>";
                                        prodID = item.Designacao;
                                    }
                                }
                            }
                            SubCategoryList = SubCategoryList.Where(t => t.Categoria != id.ToString()).ToList();
                        }
                    }
                    else
                    {
                        /*
                        if (dto.LookupField1 != null && dto.LookupField1 != "")
                        {
                            string prodID = string.Empty;
                            foreach (var item in ProductCategoryList.ToList())
                            {
                                if (prodID != item.Designacao)
                                {
                                    var product = ProductCategoryList.Where(t => t.Designacao == item.Designacao).ToList();
                                    tableColumns += "<tr style='font-size:10px'><td style='text-align: left'>&nbsp" + item.Designacao.ToUpper() + "</td>" +
                                    "<td style='text-align: center'>&nbsp" + String.Format("{0:N2}", product.Sum(t => t.Quantidade)) + "</td>" +
                                    "<td style='text-align: right'>&nbsp" + String.Format("{0:N2}", product.Sum(t => t.PrecoCusto)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.PrecoVenda)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ValorStockPVP)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.Desconto)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ImpostoLiquido)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.ValorLiquido)) + "</td>" +
                                    "<td style='text-align: right'>" + String.Format("{0:N2}", product.Sum(t => t.PrevisaoLucro)) + "</td></tr>";
                                    prodID = item.Designacao;
                                }
                            }
                        }*/
                    }
                    
                    tableColumns += "<tr style='background-color: #AFEEEE; font-size: 10px'>" +
                    "<th style='text-align:right'>TOTAL EM " + category.Descricao.ToUpper() + "</th>" +
                    " <th style = 'text-align: center' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.Quantidade)) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.PrecoCusto)) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.PrecoVenda)) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.ValorStockPVP)) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.Desconto)) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.ImpostoLiquido)) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", totalCategoryBilling) + "</th>" +
                    " <th style = 'text-align: right' > " + String.Format("{0:N2}", ProductCategoryList.Sum(t => t.PrevisaoLucro)) + "</th>" +
                    "</tr>";
                }
            }

            if(CategoryID <= 0)
            {
                tableColumns += "<tr style='background-color: silver; font-size: 10px'>" +
                    "<th style='text-align:right'>TOTAL GERAL</th>" +
                    " <th style = 'text-align: center'> " + String.Format("{0:N2}", productList.Sum(t => t.Quantidade)) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", productList.Sum(t => t.PrecoCusto)) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", productList.Sum(t => t.PrecoVenda)) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", productList.Sum(t => t.ValorStockPVP)) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", productList.Sum(t => t.Desconto)) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", productList.Sum(t => t.ImpostoLiquido)) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", ((productList.Sum(t => t.ValorStockPVP) - productList.Sum(t => t.Desconto)) + productList.Sum(t => t.ImpostoLiquido))) + "</th>" +
                    " <th style = 'text-align: right'> " + String.Format("{0:N2}", productList.Sum(t => t.PrevisaoLucro)) + "</th>" +
                    "</tr>";
            }

            return tableColumns;
        }

        public List<ArtigoDTO> ObterServicos(ArtigoDTO dto)
        {
            dto.Codigo = 0;
            dto.Designacao = dto.Designacao == null ? string.Empty : dto.Designacao;
            dto.Referencia = dto.Referencia == null ? string.Empty : dto.Referencia;
            dto.CodigoBarras = dto.CodigoBarras == null ? string.Empty : dto.CodigoBarras;
            dto.Categoria = dto.Categoria == null ? string.Empty : dto.Categoria;
            dto.ValidadeIni = string.Empty;
            dto.ValidadeTerm = string.Empty;
            dto.WareHouseName = "-1";

            return ObterPorFiltro(dto).Where(t => t.Tipo == "S").ToList();
        }

        public List<ArtigoDTO> ObterDropDownListForServices(ArtigoDTO dto)
        {
            var lista = ObterServicos(dto);

            lista.Insert(0, new ArtigoDTO(-1, "-SELECCIONE-"));

            return lista;
        }

        public List<ArtigoDTO> ObterDropDownProductList(ArtigoDTO dto)
        {
            var lista = ObterPorFiltro(dto).Where(t=>t.Tipo=="P").ToList();

            lista.Insert(0, new ArtigoDTO(-1, "-SELECCIONE-"));

            return lista;
        }

        public List<ArtigoDTO> GetExpiringList(ArtigoDTO dto)
        {
            return ObterPorFiltro(dto);
        }

        public List<ArtigoDTO> ObterArtigoVestuarioPorFiltro(ArtigoDTO dto)
        {
            return daoProduct.ObterArtigoVestuarioPorFiltro(dto);
        }
        public List<ArtigoDTO> ObterServicosMaisSolicitados(ArtigoDTO dto)
        {
            return daoProduct.GetBestSellersArtigoVestuario(dto).OrderByDescending(t => t.Quantidade).ToList();
        }

        public List<ArtigoDTO> ObterLaundryItemListForSale(ArtigoDTO dto)
        {
            if (int.Parse(dto.Categoria) > 0)
            {
                return ObterArtigoVestuarioPorFiltro(dto);
            }
            else
            {
                return ObterServicosMaisSolicitados(dto);
            }
        }

        internal decimal SupplierProductLastPrice(ArtigoDTO dto)
        {
            return daoProduct.GetSupplierLastPrice(dto);
        } 
         
    }
}
