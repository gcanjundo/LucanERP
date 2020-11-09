using BusinessLogicLayer.Comercial.Stock;
using BusinessLogicLayer.Tesouraria;
using BusinessLogicLayer.Geral;
using DataAccessLayer.Comercial;
using Dominio.Comercial;
using Dominio.Comercial.Stock;
using Dominio.Tesouraria;
using Dominio.Geral;
using Dominio.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Oficina;
using System.Security.Cryptography;
using BusinessLogicLayer.Comercial.SAFT;
using System.IO;
using DataAccessLayer.Comercial.Stock;

namespace BusinessLogicLayer.Comercial
{
    public class FaturaClienteRN
    {
        private static FaturaClienteRN _instancia;
        private FaturaDAO dao;
        private ItemFaturacaoDAO daoItem;
        private GenericRN clsConfig;
        private string Paid = "3", RegimeGeral = "IVA", RegimeTransitorio = "M00", RegimeNaoSujeicao = "M04", empresaEmRegimeGeral = "N", 
        empresaEmRegimeTransitorio = "T", empresaEmRegimeNaoSujeito = "I", empresaEmRegimeEspecialCabinda = "C";
        public FaturaClienteRN()
        {
            dao = new FaturaDAO();
            daoItem = new ItemFaturacaoDAO();
            clsConfig = new GenericRN();
        }

        public static FaturaClienteRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new FaturaClienteRN();
            }

            return _instancia;
        }

        FaturaDTO AddInvoiceNumber(FaturaDTO dto, string pPrivateKeyFileName)
        {
                dto = dao.SetOrderNumber(dto);
                dto.Hash = dto.Emissao.ToString("yyyy-MM-dd") + ";" + dto.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss") + ";" + dto.Referencia + ";" + dto.ValorTotal.ToString().Replace(",", ".") + ";";
                if (!string.IsNullOrEmpty(dto.PriorHash))
                {
                    dto.Hash += dto.PriorHash;
                }
                GenerateCustomerDocumentsHash(dto, pPrivateKeyFileName);

            return dto;
        }

        public FaturaDTO Salvar(FaturaDTO dto, DocumentoComercialDTO oDocument, bool IsDraft, string pPrivateKeyFileName)
        { 
            var _DocumentItemsList = dto.ListaArtigos;
            var UserName = dto.Utilizador;
            if(dto.Parcela <= 1)
            {
                dto = _DocumentItemsList.Count > 0 && dto.Parcela <= 1 ? dao.Adicionar(dto) : new FaturaDTO();

                if (dto.Codigo > 0)
                {
                    dto.Utilizador = UserName;
                    if (dto.StatusDocumento == 8)
                    {
                        IsDraft = false;
                    }
                    
                    if (dto.Parcela <= 1)
                    {
                        dto = SaveDocumentProductsList(dto, IsDraft, oDocument, _DocumentItemsList, false, pPrivateKeyFileName);
                    }
                }  
            }
            else
            {
                dao.GravarParcela(dto);
            }

            if (dto.ListaArtigos.Count == 0)
            {
                dto.ListaArtigos = _DocumentItemsList; 
            }

            return dto;
        }

        public void SaveHash(FaturaDTO dto)
        {
            dao.SaveHashAssign(dto);
        }

        private FaturaDTO SaveDocumentProductsList(FaturaDTO dto, bool IsDraft, DocumentoComercialDTO oDocument, List<ItemFaturacaoDTO> _DocumentItemsList, bool IsEstorno, string pPrivateKeyFileName)
        {
            List<ItemMovimentoStockDTO> StockProductsList = new List<ItemMovimentoStockDTO>();
             
            foreach (var item in _DocumentItemsList)
            {   
                item.Quantidade = (IsEstorno || (oDocument.Stock=="S" && item.MovimentaStock)) ? -item.Quantidade : item.Quantidade;
                item.Fatura = dto.Codigo;
                item.ValorDesconto = item.Desconto > 0 && item.ValorDesconto == 0 ? ((item.Quantidade * item.PrecoUnitario) * (item.Desconto / 100)) : item.ValorDesconto;
                item.Imposto = item.Imposto == 0 && item.ValorImposto > 0 ? ((item.Quantidade * item.PrecoUnitario) / (item.ValorImposto * 100)) : item.Imposto; 
                item.ValorImposto = item.Imposto > 0 && item.ValorImposto == 0 ? ((item.Quantidade * item.PrecoUnitario) * (item.Imposto / 100)) : item.ValorImposto;
                 
                var addedItem = daoItem.Adicionar(item); 

                if (addedItem.Sucesso)
                {
                    if (!IsDraft && !dto.IsAtendimento && dto.Codigo > 0 && item.MovimentaStock && (oDocument != null && !string.IsNullOrEmpty(oDocument.Stock)))
                    {
                        if (item.Quantidade < 0 && oDocument.Stock == "S")
                        {
                            item.Quantidade *= -1;
                        }

                        ItemMovimentoStockDTO product = new ItemMovimentoStockDTO
                        {
                            ArtigoID = item.Artigo,
                            Designacao = item.Designacao,
                            Existencia = item.ExistenciaAnterior <= 0 ? new StockDAO().StockActual(item.Artigo, item.ArmazemID) : item.ExistenciaAnterior,
                            PrecoUnitario = item.PrecoUnitario,
                            TotalLiquido = item.PrecoUnitario * item.Quantidade,
                            Quantidade = oDocument.Stock == "S" ? -item.Quantidade : item.Quantidade
                        };
                        product.ValorTotal = (product.Existencia + product.Quantidade) * item.PrecoUnitario;
                        product.Operacao = oDocument.Stock == "E" ? 1 : 2;
                        product.ArmazemOrigem = item.ArmazemID;
                        product.AramzemDestino = item.ArmazemID;
                        product.Utilizador = dto.Utilizador;
                        product.SerieID = item.StockOutcomeSerieID;
                        StockProductsList.Add(product);
                    }
                }
                else
                {
                    if (!IsDraft)
                    {
                        dto.MensagemErro = addedItem.MensagemErro;
                        dao.Abortar(dto);//Excluir(dto);
                        break;
                    }
                } 
            }

            if (dto.MensagemErro == string.Empty)
            {
               dto = AddInvoiceNumber(dto, pPrivateKeyFileName);
                if (StockProductsList.Count > 0 && (oDocument != null && !string.IsNullOrEmpty(oDocument.Stock)))
                {
                    MovimentoStockRN.GetInstance().GenerateStockMovimentFromSalesDocument(StockProductsList, dto, oDocument.Stock);
                }
            }
            else
            {
                if (!IsDraft)
                {
                    dto.MensagemErro = dto.MensagemErro;
                    dao.Abortar(dto);//Excluir(dto); 
                }
            }



            return dto;
        }

        public List<FaturaDTO> CustomerResumeHistoricList(FaturaDTO dto)
        {
            return new List<FaturaDTO>();
        }

        public List<FaturaDTO> ObterEncomendas(FaturaDTO dto)
        {
            dto.TituloDocumento = "INVOICE_E";
            return ObterFaturaPorFiltro(dto).Where(t=>t.TituloDocumento == "INVOICE_E").ToList();
        }

        public List<FaturaDTO> ObterProformas(FaturaDTO dto)
        {
            dto.TituloDocumento = "INVOICE_O";
            return ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "INVOICE_O").ToList();
        }

        public List<FaturaDTO> ObterFaturaPorFiltro(FaturaDTO dto)
        {
            if(dto.EmissaoIni > dto.EmissaoTerm)
            {
                if(dto.EmissaoTerm > DateTime.MinValue)
                {
                    var Date = dto.EmissaoIni;
                    dto.EmissaoIni = dto.EmissaoTerm;
                    dto.EmissaoTerm = Date;
                }
            }

            if (dto.ValidadeIni > dto.ValidadeTerm)
            {
                if(dto.EmissaoTerm > DateTime.MinValue)
                {
                    var Date = dto.ValidadeIni;
                    dto.ValidadeIni = dto.ValidadeTerm;
                    dto.ValidadeTerm = Date;
                }
            }
            return dao.ObterPorFiltro(dto);
        }


        public List<FaturaDTO> ObterDocumentoRetificativos(FaturaDTO dto)
        {
            dto.TituloDocumento = "INVOICE_D";
            var lista = ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "INVOICE_D").ToList();

            foreach(var note in lista)
            {
                CorrigeNotaCredito(note);
            }

            return lista;
        }

        public void CorrigeNotaCredito(FaturaDTO dto)
        {
            dao.CorrigeDocumentosAnulados(dto);
        }

        public List<FaturaDTO> ObterDocumentoTransporte(FaturaDTO dto)
        {
            dto.TituloDocumento = "TRANSP_V";
            return ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "TRANSP_V").ToList();
        }

        bool InEditMode(int pStatus)
        {
            if (pStatus >= 1 && pStatus <= 4 || pStatus == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<FaturaDTO> ObterDocumentosVenda(FaturaDTO dto)
        {
            dto.TituloDocumento = "";
            var salesList = ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "INVOICE" 
            || t.TituloDocumento == "INVOICE_R" || t.TituloDocumento == "INVOICE_A").ToList();

            var _newSalesList = new List<FaturaDTO>();

            foreach (var sale in salesList)
            {

                var productsList = ObterLucros(sale);
                sale.Lucro = productsList.Sum(t => t.Lucro);

                if (!_newSalesList.Exists(t=>t.Referencia == sale.Referencia) || InEditMode(dto.StatusDocumento) )
                { 
                    _newSalesList.Add(sale);
                }
                else
                {
                    var document = _newSalesList.Where(t => t.Referencia == sale.Referencia).SingleOrDefault();

                    int index = _newSalesList.FindIndex(t => t.Referencia == sale.Referencia);
                    sale.Codigo = document.Codigo;
                    sale.TotalImpostos = document.TotalImpostos + sale.TotalImpostos;
                    sale.TotalDescontos = document.TotalDescontos + sale.TotalDescontos;
                    sale.TotalIliquido = document.TotalIliquido + sale.TotalIliquido;
                    sale.ValorTotal = (sale.TotalIliquido - sale.TotalDescontos) + sale.TotalImpostos;
                     
                    _newSalesList[index] = sale; 
                }

                
            }

            return _newSalesList.OrderBy(t=>t.Emissao).ToList();
        }

        public List<FaturaDTO> ObterSalesExtractList(FaturaDTO dto)
        {
            dto.TituloDocumento = "";
            var salesList = ObterFaturaPorFiltro(dto).Where(t => (t.TituloDocumento == "INVOICE" || t.TituloDocumento == "INVOICE_R"
            || t.TituloDocumento == "INVOICE_A") && t.StatusDocumento == 8 && t.Activo).ToList();

            var _newSalesList = new List<FaturaDTO>();

            foreach (var sale in salesList)
            { 

                if (sale.TituloDocumento == "INVOICE_D")
                {
                    sale.TotalIliquido *= (-1);
                    sale.TotalDescontos *= (-1);
                    sale.TotalImpostos *= (-1);
                    sale.ValorTotal *= (-1);
                    sale.ValorPago *= (-1);
                    sale.Saldo *= (-1);
                }

                if (sale.TituloDocumento == "INVOICE_A")
                {
                    sale.TotalIliquido =0;
                    sale.TotalDescontos =0; 
                    sale.ValorTotal =0; 
                }

                var productsList = ObterLucros(sale);
                sale.Lucro = productsList.Sum(t => t.Lucro);
                
                if (!_newSalesList.Exists(t => t.Referencia == sale.Referencia))
                {
                    if (!sale.Activo)
                    {
                        sale.Referencia += "(ANULADO)";

                    }

                    _newSalesList.Add(sale);
                }
                else
                {
                    var document = _newSalesList.Where(t => t.Referencia == sale.Referencia).SingleOrDefault();

                    int index = _newSalesList.FindIndex(t => t.Referencia == sale.Referencia);
                    sale.Codigo = document.Codigo;
                    sale.TotalImpostos = document.TotalImpostos + sale.TotalImpostos;
                    sale.TotalDescontos = document.TotalDescontos + sale.TotalDescontos;
                    sale.TotalIliquido = document.TotalIliquido + sale.TotalIliquido;
                    sale.ValorTotal = (sale.TotalIliquido - sale.TotalDescontos) + sale.TotalImpostos;

                    if (!sale.Activo)
                    {
                        sale.Referencia += "(ANULADO)";

                    }

                    _newSalesList[index] = sale;
                }


            }

            return _newSalesList.OrderBy(t => t.Emissao).ToList();
        }

        public Tuple<List<FaturaDTO>, List<FaturaDTO>> GetAllSalesDocumentList(FaturaDTO dto)
        {
            var lista = ObterFaturaPorFiltro(dto);
            dto.FiscalYear = -1;
            return new Tuple<List<FaturaDTO>, List<FaturaDTO>>(lista, ObterDocumentoRetificativos(dto));
        }

        public string GetMapaGlobalIVA(FaturaDTO dto)
        {
            string html ="";
            
            foreach(var item in dao.MapaGlobalIVA(dto))
            {
                html += "<tr>"+
                    "<td style='text-align: left'>" + item.Referencia + "</td>" +
                    "<td style='text-align: right'>" + String.Format("{0:N2}", item.ValorRetencao) + "%</td>" +
                    "<td style='text-align: center'>" + String.Format("{0:N2}", item.ValorIncidencia) + "</td>" +
                    "<td style='text-align: right'>" + String.Format("{0:N2}", item.TotalImpostos) + "</td>"+
                    "</tr>";
            }

            return html;
        }

        public List<FaturaDTO> ObterDailyExtractoVendas(List<FaturaDTO> pList, List<MovimentoDTO> pReceivedPaymentList, string pFilial)
        {  

            

            MovimentoDTO dto = new MovimentoDTO
            {
                DataIni = pList.Min(t => t.Emissao).ToString(),
                DataTerm = pList.Max(t => t.Emissao).ToString(),
                Movimento = "L",
                Filial = pFilial
            };
            var DailyReceive = pReceivedPaymentList != null && pReceivedPaymentList.Count > 0 ? MovimentoRN.GetInstance().GetDailySalesReceipts(dto).ToList() :
                pReceivedPaymentList.Where(t => t.DataTransacao >= pList.Min(a => a.Emissao) && t.DataTransacao <= pList.Max(a => a.Emissao));

            pList = pList.Where(t => t.TituloDocumento != "INVOICE_A").ToList(); 

            var SalesByDays = from s in pList
                            group s by s.Emissao into g
                                  select new FaturaDTO()
                                  {
                                      Emissao = g.Key,
                                      TotalIliquido = g.Sum(t => t.TotalIliquido),
                                      TotalDescontos = g.Sum(t => t.TotalDescontos),
                                      ValorIncidencia = (g.Sum(t => t.TotalIliquido) - g.Sum(t => t.TotalDescontos)),
                                      TotalImpostos = g.Sum(t => t.TotalImpostos),
                                      ValorTotal = g.Sum(t => t.ValorTotal),
                                      ValorPago = DailyReceive.Where(t=>t.DataTransacao == g.Key).Sum(t=>t.Valor),
                                      Saldo = g.Sum(t=>t.Saldo),
                                      Lucro = g.Sum(t=>t.Lucro)
                                  };



            return SalesByDays.ToList();

        }

            public List<ItemFaturacaoDTO> ObterLucros(FaturaDTO dto)
        {
            return new ItemFaturacaoDAO().ObterLucro(dto);
        }


        public List<FaturaDTO> ObterDocumentosPendentes(FaturaDTO dto)
        {
            if (dto.EmissaoIni > dto.EmissaoTerm)
            {
                var Date = dto.EmissaoIni;
                dto.EmissaoIni = dto.EmissaoTerm;
                dto.EmissaoTerm = Date;
            }

            if (dto.ValidadeIni > dto.ValidadeTerm)
            {
                var Date = dto.ValidadeIni;
                dto.ValidadeIni = dto.ValidadeTerm;
                dto.ValidadeTerm = Date;
            }
            return dao.ObterPendentes(dto).Where(t => t.Activo && (t.TituloDocumento == "INVOICE" || t.TituloDocumento == "INVOICE_D" || t.TituloDocumento == "INVOICE_A")).ToList();
        }

        public List<ContaCorrenteDTO> MapaAntiguidadeSaldo(FaturaDTO dto)
        {
            var lista = ObterDocumentosPendentes(dto).Where(t => t.TituloDocumento == "INVOICE").OrderBy(t=>t.Entidade).ToList();
            List<ContaCorrenteDTO> mapaList = new List<ContaCorrenteDTO>();
             
            foreach(var invoice in lista.ToList())
            {
                if(!mapaList.Exists(t=>t.Entidade == invoice.Entidade)){
                    var mapa = new ContaCorrenteDTO();

                    mapa.DividaCorrente = lista.Where(t => t.Entidade == invoice.Entidade && t.DiasAtrasado <= 0).Sum(t => t.ValorTotal - t.ValorPago);
                    mapa.Escalao1 = lista.Where(t => t.Entidade == invoice.Entidade && (t.DiasAtrasado > 0 && t.DiasAtrasado <= 90)).Sum(t => t.Saldo);
                    mapa.Escalao2 = lista.Where(t => t.Entidade == invoice.Entidade && (t.DiasAtrasado > 0 && t.DiasAtrasado > 90 && t.DiasAtrasado <= 180)).Sum(t => t.Saldo);
                    mapa.Escalao3 = lista.Where(t => t.Entidade == invoice.Entidade && (t.DiasAtrasado > 0 && t.DiasAtrasado > 180 && t.DiasAtrasado <=360)).Sum(t => t.Saldo);
                    mapa.Escalao4 = lista.Where(t => t.Entidade == invoice.Entidade && (t.DiasAtrasado > 0 && t.DiasAtrasado > 360 && t.DiasAtrasado <= 480)).Sum(t => t.Saldo);
                    mapa.Escalao5 = lista.Where(t => t.Entidade == invoice.Entidade && (t.DiasAtrasado > 0 && t.DiasAtrasado > 480 && t.DiasAtrasado <= 660)).Sum(t => t.Saldo);
                    mapa.Escalao6 = lista.Where(t => t.Entidade == invoice.Entidade && (t.DiasAtrasado > 0 && t.DiasAtrasado > 660)).Sum(t => t.Saldo);
                    mapa.Valor = mapa.DividaCorrente + mapa.Escalao1 + mapa.Escalao2 + mapa.Escalao3 + mapa.Escalao4+mapa.Escalao5 + mapa.Escalao6;
                    mapa.Entidade = invoice.Entidade;
                    mapa.DesignacaoEntidade = invoice.NomeEntidade.ToUpper();
                    mapaList.Add(mapa);
                } 
                lista = lista.Where(t => t.Entidade != invoice.Entidade).ToList();
            }

            return mapaList;
        }

        public List<FaturaDTO> ObterVendasDiarias(FaturaDTO dto)
        {
            return dao.ObterVendasDiarias(dto);
        }

        public FaturaDTO ObterPorPK(FaturaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }



        public FaturaDTO Excluir(FaturaDTO dto)
        {
            var UserName = dto.Utilizador;
            dto = ObterPorPK(dto);
            if(dto.Activo)
            {
                foreach (var item in dto.ListaArtigos)
                {
                    dao.ExcluirItem(item);

                }
                dto.MotivoAnulacao = "Erro de Lançamento";
                dto.Utilizador = UserName;

                return dao.Excluir(dto);
            }
            else
            {
                dto.MensagemErro = "alert('Este Documento já está anulado');";

                return dto;
            }

        }

        public ItemFaturacaoDTO RemoverItem(ItemFaturacaoDTO dto)
        {
            return daoItem.Excluir(dto);
        }

        public List<ItemFaturacaoDTO> ListaItem(ItemFaturacaoDTO dto)
        {
            return daoItem.ObterPorFiltro(dto);
        }

        public List<ItemFaturacaoDTO> ObterEncomendaItensPorFiltro(FaturaDTO dto)
        {
            return daoItem.ObterEncomendaItensPorFiltro(dto);
        }

        public List<ItemFaturacaoDTO> MapaEntregas(FaturaDTO pFilter)
        {
            ItemFaturacaoDTO dto = new ItemFaturacaoDTO();
            dto.Fatura = -1;
            dto.LookupDate1 = pFilter.LookupDate1;
            dto.LookupDate2 = pFilter.LookupDate2;

            if (dto.Fatura > 0 || dto.LookupDate1 > DateTime.MinValue || dto.LookupDate1 > DateTime.MinValue)
            {
                if (dto.Fatura > 0)
                {
                    return ListaItem(dto);
                }
                else
                {
                    dto.LookupDate1 = dto.LookupDate1 == DateTime.MinValue || dto.LookupDate1 > DateTime.Today ? DateTime.Today : dto.LookupDate1;
                    dto.LookupDate2 = dto.LookupDate2 == DateTime.MinValue || dto.LookupDate2 < DateTime.Today ? DateTime.Today : dto.LookupDate2;
                    return ListaItem(dto).Where(t=>t.LookupField3=="INVOICE_E").OrderBy(t=>t.LookupDate1).ToList();
                }
            }
            else
            {
                return new List<ItemFaturacaoDTO>();
            }
        }


        public FaturaDTO GravarDocumento(FaturaDTO dto, object PaymentReceivedList, object pPOSDefaultCashAccount, int pTerminal, DocumentoComercialDTO pDocument, bool pEditMode, string pPrivateKeyFileName)
        {
            if(dto.Documento > 0)
            {
                 
               dto = SaveMovimentoCaixa(dto, PaymentReceivedList, pTerminal, pPOSDefaultCashAccount, pDocument, pEditMode, pPrivateKeyFileName);
            }
            else
            {
                dto.MensagemErro = "Seleccione o Documento";
            }

            return dto;
        }

        public FaturaDTO SaveMovimentoCaixa(FaturaDTO dto,  object PaymentReceivedList, int pTerminal, object pPOSDefaultCashAccount, DocumentoComercialDTO pDocument, bool pEditMode, string pPrivateKeyFileName)
        {
            bool isFR = new GenericRN().IsDocumentoAutoLiquidacao(pDocument), isNCD = new GenericRN().IsCashDevolution(pDocument),
                isFA = new GenericRN().IsDocumentoAdiantamento(pDocument), podeProsseguir=true;
            

            List<PagamentoDTO> pagtos = new List<PagamentoDTO>();
            if (PaymentReceivedList != null)
            {
                if (isFR || isFA)
                {
                    pagtos = (List<PagamentoDTO>)PaymentReceivedList;
                    if(pagtos.Sum(t => t.Value) < (dto.ValorTotal - dto.ValorRetencao))
                    {
                        podeProsseguir = false;
                    }
                    else
                    {
                        dto.ValorPago = pagtos.Sum(t => t.Value);
                        dto.Troco = dto.ValorPago - (dto.ValorTotal - dto.ValorRetencao);
                        dto.Saldo =0;
                        dto.StatusPagamento = Paid;
                    }
                    
                }
                dto.StatusDocumento = 8;
            }
            else
            {
                podeProsseguir = false;
            }

            if (podeProsseguir)
            {
                var UserName = dto.Utilizador;

                dto = isFA || (isFR && (dto.Codigo <= 0 && dto.ValorPago > 0 && dto.ValorPago >= dto.ValorTotal - dto.ValorRetencao)) || (isNCD && (dto.Codigo <= 0 && dto.ValorPago < 0)) ? Salvar(dto, pDocument, pEditMode, pPrivateKeyFileName) : dto;

                if (dto.Codigo > 0)
                {

                    pagtos = isFA ? (List<PagamentoDTO>)PaymentReceivedList : pagtos;
                    if (pagtos.Count > 0 && pagtos.Sum(t => t.Value) > 0)
                    {
                        MovimentoDTO transacao;
                        foreach (var item in pagtos)
                        {
                            transacao = new MovimentoDTO
                            {
                                ContaCorrente = item.Account,
                                DataLancamento = DateTime.Now,
                                DataTransacao = DateTime.Now,
                                Moeda = dto.Moeda,
                                Movimento = "E",
                                MetodoPagamento = item.PaymentMethod,
                                Utilizador = UserName,
                                Filial = dto.Filial,
                                Descritivo = dto.Referencia,
                                FluxoCaixa = -1,
                                CentroCustoID = -1,
                                Valor = item.Value,
                                Entidade = dto.Entidade,
                                Observacoes = "",
                                RefComprovantePagto = item.DocumentNumber,
                                Documento = dto.Codigo,
                                Terminal = pTerminal,
                                DocumentType = dto.Documento,
                                DocumentID = dto.Codigo,
                                IsReal = true,
                                SerieID = -1

                            };

                            dto.MensagemErro = MovimentoRN.GetInstance().Adicionar(transacao).MensagemErro;
                        }

                        if (dto.Troco > 0)
                        {
                            transacao = new MovimentoDTO
                            {
                                ContaCorrente = pPOSDefaultCashAccount.ToString(),
                                DataLancamento = DateTime.Now,
                                DataTransacao = DateTime.Now,
                                Moeda = dto.Moeda,
                                Movimento = "S",
                                MetodoPagamento = 1,
                                Utilizador = dto.Utilizador,
                                Filial = dto.Filial,
                                Descritivo = "TROCO " + dto.Referencia,
                                FluxoCaixa = -1,
                                CentroCustoID = -1,
                                Valor = dto.Troco,
                                Entidade = dto.Entidade,
                                Observacoes = "",
                                RefComprovantePagto = "",
                                Documento = dto.Codigo,
                                Terminal = pTerminal,
                                DocumentType = dto.Documento,
                                DocumentID = dto.Codigo,
                                IsReal = true,
                                SerieID = -1

                            };
                            MovimentoRN.GetInstance().Adicionar(transacao);
                        }

                    }
                }
            }
            else
            {
                dto.MensagemErro = "alert('Por favor certifique-se que o valor total pago que foi o lançado no sistema é igual ou superior ao valor que o cliente deve pagar ');";
            }

            return dto;

        }

        public List<FaturaDTO> ObterMovimentoDoDiaCaixa(FaturaDTO dto)
        {
            return dao.ObterMovimentoCaixaDia(dto);
        }

        public List<ItemFaturacaoDTO> ObterListaDeArtigosVendidosEntreDatas(FaturaDTO dto)
        {
            return dao.ObterListaDeArtigosVendidosEntreDatas(dto);
        }

        public List<MetodoPagamentoDTO> ObterPagtosFatura(FaturaDTO dto)
        {
            return dao.ObterPagamentos(dto);
        }

        public List<ReceiptDTO> GetReceiptData(FaturaDTO dto)
        {
            return dao.ObterDadosImpressao(dto);
        }

        public List<PagamentoDTO> ReceivePayment(string[] paymentDetails, object pLista, object pDefaultPOSAccount)
        {

            List<PagamentoDTO> payments = (List<PagamentoDTO>)pLista ?? new List<PagamentoDTO>();

            PagamentoDTO dto = new PagamentoDTO();
            dto.Account = paymentDetails[3] == "-1" ? (string)pDefaultPOSAccount : paymentDetails[3];
            /*if (paymentDetails[4] == "D" && pDefaultPOSAccount != null)
                dto.Account = pDefaultPOSAccount == "1";*/
            dto.DocumentNumber = string.Empty;// paymentDetails[4];
            dto.PaymentDescription = paymentDetails[1] + "_" + paymentDetails[3] + "_" + paymentDetails[4];
            dto.Value = setDecimalValue(paymentDetails[2]);
            dto.PaymentMethod = Convert.ToInt32(paymentDetails[0]);

            if (payments != null && payments.Count > 0)
            {
                payments.RemoveAll(t => t.PaymentDescription == dto.PaymentDescription && t.Account == dto.Account);

            }
            payments.Add(dto);

            return payments;
        }

        private decimal setDecimalValue(String valor)
        {
            valor = valor.Replace(".", "");
            int numero;
            string campo = "";
            decimal retorno = 0;
            foreach (char caracter in valor)
            {
                bool res = int.TryParse(caracter.ToString(), out numero);

                if (res.Equals(true) || caracter.Equals(',') || caracter.Equals('-'))
                {
                    campo += caracter;
                }

            }

            if (campo != null && !campo.Equals(""))
            {
                retorno = Convert.ToDecimal(campo);
            }

            return retorno;


        }

        public string GetDocumentPaymentMethods(string pDocID, string pDocTypeID)
        {
            MovimentoDTO dto = new MovimentoDTO
            {
                Documento = int.Parse(pDocID),
                DocumentType = int.Parse(pDocTypeID)
            };
            string table = "";
            foreach (var method in MovimentoRN.GetInstance().GetPaymentInfo(dto))
            {
                table += "<tr>" +
               "<td style='width: 100px; text-align: center'>" + method.DataTransacao.ToString("dd-MM-yyyy") + "</td>" +
               "<td style='text-align: center'>" + method.Descritivo + "</td>" +
               "<td style='text-align: center'>" + method.ContaCorrente + "</td>" +
               "<td style='text-align: center'>" + method.RefComprovantePagto + "</td>" +
               "<td style='text-align: center'>" + String.Format("{0:N2}", method.Valor) + "</td>" +
               "</tr>";
            }


            return table;
        }

        public string TrimestalAnalyses(List<FaturaDTO> incomeAnalyses)
        {
            var PrimeiroTrimestre = incomeAnalyses.Where(t => t.Emissao.Month <= 3).ToList();
            var SegundoTrimestre = incomeAnalyses.Where(t => t.Emissao.Month > 3 && t.Emissao.Month <= 6).ToList();
            var TerceiroTrimestre = incomeAnalyses.Where(t => t.Emissao.Month > 6 && t.Emissao.Month <= 9).ToList();
            var QuartoTrimestre = incomeAnalyses.Where(t => t.Emissao.Month > 9 && t.Emissao.Month <= 12).ToList();

            decimal totalGeralFaturacao = incomeAnalyses.Sum(t => t.ValorFaturado), totalGeralRecebido = incomeAnalyses.Sum(t => t.ValorPago);
            List<FaturaDTO> periodo = new List<FaturaDTO>();
            string html = "";
            totalGeralRecebido = totalGeralRecebido == 0 ? 1 : totalGeralRecebido;
            totalGeralFaturacao = totalGeralFaturacao == 0 ? 1 : totalGeralFaturacao;

            for (int i = 1; i <= 4; i++)
            {
                html += "<tr><td class='text-left'><label><small>" + i + "º TRIMESTRE</small></label></td>";
                if (i == 1)
                    periodo = PrimeiroTrimestre;
                else if (i == 2)
                    periodo = SegundoTrimestre;
                else if (i == 3)
                    periodo = TerceiroTrimestre;
                if (i == 4)
                    periodo = QuartoTrimestre;
                decimal totalVendas = periodo.Sum(t => t.TotalIliquido),
                totalDescontos = periodo.Sum(t => t.TotalDescontos),
                totalImpostos = periodo.Sum(t => t.TotalImpostos),
                totalFaturado = periodo.Sum(t => t.ValorFaturado),
                totalRecebimentos = periodo.Sum(t => t.ValorPago),
                totalRetencao = periodo.Sum(t => t.ValorRetencao);


                html += "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalVendas) + "</small></label></td>" +
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalDescontos) + "</small></label></td>" +
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalRetencao) + "</small></label></td>" +
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalImpostos) + "</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalFaturado) + "</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", (Math.Round((totalFaturado * 100)/totalGeralFaturacao))) + "%</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalRecebimentos) + "</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", (Math.Round((totalRecebimentos * 100) / totalGeralRecebido))) + "%</small></label></td>" +
                "</tr>";
            }

            html += "<tr><td class='text-right'><label><small>TOTAL</small></label></td>"+
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", incomeAnalyses.Sum(t => t.TotalIliquido)) + "</small></label></td>" +
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", incomeAnalyses.Sum(t => t.TotalDescontos)) + "</small></label></td>" +
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", incomeAnalyses.Sum(t => t.ValorRetencao)) + "</small></label></td>" +
                "<td class='text-right'><label><small>" + String.Format("{0:N2}", incomeAnalyses.Sum(t => t.TotalImpostos)) + "</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalGeralFaturacao == 1 ? 0 : totalGeralFaturacao) + "</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", (Math.Round((totalGeralFaturacao * 100) / totalGeralFaturacao))) + "%</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", totalGeralRecebido == 1 ? 0 : totalGeralRecebido) + "</small></label></td>" +
                    "<td class='text-right'><label><small>" + String.Format("{0:N2}", (Math.Round((totalGeralRecebido * 100) / totalGeralRecebido))) + "%</small></label></td>" +
                "</tr>";

            return html;
        }

        public List<string> GetDocumentByRefence(string docReference, string pFilial)
        {
            FaturaDTO dto = new FaturaDTO();
            dto.Referencia = docReference;
            dto.Documento = -1;
            dto.Filial = pFilial;
            dto.NomeEntidade = "";
            dto.Activo = true;
            dto.Entidade = -1;
            dto.StatusDocumento = 0;
            dto.EmissaoIni = DateTime.MinValue;
            dto.EmissaoTerm = DateTime.MinValue;
            dto.ValidadeIni = DateTime.MinValue;
            dto.ValidadeTerm = DateTime.MinValue;
            dto.StatusPagamento = "-1";

            var documentsList = ObterFaturaPorFiltro(dto);
            List<string> lista = new List<string>();
            foreach (var document in documentsList)
            {
               lista.Add(document.Codigo.ToString() + ";" + document.Referencia+";"+document.Documento);
            }

            return lista;
        }

        public List<FaturaDTO> GetInvoiceList(FaturaDTO dto)
        {
            return ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "INVOICE").ToList();
        }

        public List<FaturaDTO> GetBudgetList(FaturaDTO dto)
        {
            return ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "INVOICE_O").ToList();
        }

        public List<FaturaDTO> GetInvoiceSettlementList(FaturaDTO dto)
        {
            return ObterFaturaPorFiltro(dto).Where(t => t.TituloDocumento == "INVOICE_R").ToList();
        }

        public List<FaturaDTO> GetSalesOrderList(FaturaDTO dto)
        {
            var OrderList = ObterEncomendas(dto).Where(t => t.TituloDocumento == "INVOICE_E").ToList();

            foreach(var order in OrderList)
            {
                var ItemList = daoItem.ObterPorFiltro(new ItemFaturacaoDTO { Fatura = order.Codigo });
                order.Saldo = order.ValorTotal - order.ValorPago;
                order.Validade = ItemList.Max(t => t.DataPrevisaoEntrega);
                order.TotalItems = ItemList.Count; 
                order.TotalDelivered = ItemList.Where(t => t.DataEntrega > DateTime.MinValue).ToList().Count;
                order.TotalReady = ItemList.Where(t => t.ReadyDate > DateTime.MinValue).ToList().Count;
                order.UnderProcess = order.TotalItems - order.TotalDelivered - order.TotalReady;
            }

            return OrderList;
        }

        public List<FaturaDTO> GetCustomerExtractList(FaturaDTO dto)
        {
            var _generic = new GenericRN();

            var ExtractList = dao.ObterCustomerExtract(dto).Where(t=>t.TituloDocumento ==_generic.CUSTOMER_INVOICE_RECEIPT || t.TituloDocumento==_generic.CUSTOMER_INVOICE || t.TituloDocumento == _generic.CUSTOMER_CREDIT_NOTE
            || t.TituloDocumento == _generic.CUSTOMER_RECEIPT_CUSTOMER_PAYMENT || t.TituloDocumento == _generic.CUSTOMER_RECEIPT || t.TituloDocumento == _generic.CUSTOMER_INVOICE_ADVANCED || t.TituloDocumento==_generic.CUSTOMER_RECEIPT_ADVANCE).OrderBy(t => t.LookupDate1).ToList();

            if (dto.Documento > 0)
            {   
                ExtractList = ExtractList.Where(t => t.Documento == dto.Documento).ToList();
            }

            if (!string.IsNullOrEmpty(dto.Referencia))
            {
                ExtractList = ExtractList.Where(t => t.Referencia.Contains(dto.Referencia)).ToList();
            }

            if (dto.EmissaoIni != DateTime.MinValue && dto.EmissaoTerm != DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao >= dto.EmissaoIni && t.Emissao <= dto.EmissaoTerm).ToList();
            }
            else if (dto.EmissaoIni != DateTime.MinValue && dto.EmissaoTerm == DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao >= dto.EmissaoIni).ToList();
            }
            else if (dto.EmissaoIni == DateTime.MinValue && dto.EmissaoTerm != DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao <= dto.EmissaoTerm).ToList();
            } 

            return ExtractList;
        }

        public List<FaturaDTO> GetCustomerMapaImpostoSeloList(FaturaDTO dto)
        {
            return dao.ObterImpostoSelo(dto);
        }

        public Tuple<List<FaturaDTO>, List<FaturaDTO>> GetIndividualCustomerHistoricList(FaturaDTO dto)
        {
            List<FaturaDTO> CustomerHistoricExtractList = dao.ObterCustomerExtract(dto).OrderBy(t => t.LookupDate1).ToList();
            var ExtractList = CustomerHistoricExtractList.Where(t => t.TituloDocumento == "INVOICE_R" || t.TituloDocumento == "INVOICE" || t.TituloDocumento == "INVOICE_D"
            || t.TituloDocumento == "RECEIPT_P" || t.TituloDocumento == "RECEIPT_R" || t.TituloDocumento == "INVOICE_A").OrderBy(t => t.LookupDate1).ToList();

            if (dto.Documento > 0)
            {
                ExtractList = ExtractList.Where(t => t.Documento == dto.Documento).ToList();
            }

            if (!string.IsNullOrEmpty(dto.Referencia))
            {
                ExtractList = ExtractList.Where(t => t.Referencia.Contains(dto.Referencia)).ToList();
            }

            if (dto.EmissaoIni != DateTime.MinValue && dto.EmissaoTerm != DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao >= dto.EmissaoIni && t.Emissao <= dto.EmissaoTerm).ToList();
            }
            else if (dto.EmissaoIni != DateTime.MinValue && dto.EmissaoTerm == DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao >= dto.EmissaoIni).ToList();
            }
            else if (dto.EmissaoIni == DateTime.MinValue && dto.EmissaoTerm != DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao <= dto.EmissaoTerm).ToList();
            }

            return new Tuple<List<FaturaDTO>, List<FaturaDTO>>(CustomerHistoricExtractList, ExtractList);
        }


        public List<FaturaDTO> GetAllCustomerDocumentList(FaturaDTO dto)
        {
            var ExtractList = dao.ObterCustomerExtract(dto).OrderBy(t => t.LookupDate1).ToList();

            if (dto.Documento > 0)
            {
                ExtractList = ExtractList.Where(t => t.Documento == dto.Documento).ToList();
            }

            if (dto.Referencia != string.Empty)
            {
                ExtractList = ExtractList.Where(t => t.Referencia.Contains(dto.Referencia)).ToList();
            }

            if (dto.EmissaoIni != DateTime.MinValue && dto.EmissaoTerm != DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao >= dto.EmissaoIni && t.Emissao <= dto.EmissaoTerm).ToList();
            }
            else if (dto.EmissaoIni != DateTime.MinValue && dto.EmissaoTerm == DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao >= dto.EmissaoIni).ToList();
            }
            else if (dto.EmissaoIni == DateTime.MinValue && dto.EmissaoTerm != DateTime.MinValue)
            {
                ExtractList = ExtractList.Where(t => t.Emissao <= dto.EmissaoTerm).ToList();
            }

            return ExtractList;
        }



        public List<FaturaDTO> GenerateParcelas(string pSchedule, decimal pTotalParcelas, decimal pEntrada, decimal pBruto, decimal pDescontos, decimal pImpostos, decimal pLiquido)
        {
            var PaymentTermsList = new List<FaturaDTO>();
            string[] PaymentSchedule = pSchedule.Split('_');
            int CurrentParcel =(int)pTotalParcelas;
            for (int i = 0; i < pTotalParcelas; i++)
            {
                var _term = new FaturaDTO
                {
                    Validade = DateTime.Parse(PaymentSchedule[i]),
                    Parcela = i + 1
                };

                if (pEntrada > 0 && i == 0)
                {
                    _term.TotalIliquido = pBruto * (pEntrada / 100);
                    _term.TotalDescontos = pDescontos * (pEntrada / 100);
                    _term.TotalImpostos = pImpostos * (pEntrada / 100);
                    _term.ValorTotal = pLiquido * (pEntrada / 100);
                    CurrentParcel--; 
                    pBruto = (pBruto - _term.TotalIliquido)/CurrentParcel;
                    pDescontos = (pDescontos - _term.TotalDescontos) > 0 ? (pDescontos - _term.TotalDescontos) / CurrentParcel : 0;
                    pImpostos = (pImpostos - _term.TotalImpostos) > 0 ? (pImpostos - _term.TotalImpostos) / CurrentParcel : 0;
                    pLiquido =  _term.ValorTotal / CurrentParcel;
                    pEntrada = 0;
                    
                }
                else
                {
                    _term.TotalIliquido = pBruto;
                    _term.TotalDescontos = pDescontos;
                    _term.TotalImpostos = pImpostos;
                    _term.ValorTotal = pLiquido; 
                }

                PaymentTermsList.Add(_term);
            }

            return PaymentTermsList;

        } 
         

        public FaturaDTO SaveDocumentTranches(List<FaturaDTO> _parcelasList, FaturaDTO dto, DocumentoComercialDTO _documentType, bool IsDraft, string pPrivateKeyFileName)
        {
            var Doc = new FaturaDTO();
            var _statusPagamento = dto.StatusPagamento;
            foreach (var _parcela in _parcelasList)
            {     
                dto.Parcela = _parcela.Parcela;
                dto.Validade = _parcela.Validade;
                dto.ValorTotal = _parcela.ValorTotal;
                dto.TotalImpostos = _parcela.TotalImpostos;
                dto.TotalDescontos = _parcela.TotalDescontos;
                dto.TotalIliquido = _parcela.TotalIliquido;
                dto.StatusPagamento = _statusPagamento;
                if (_parcela.Parcela > 1 & dto.Codigo > 0)
                {
                    dto.Codigo = -1;
                    
                }
                dto = Salvar(dto, _documentType, IsDraft, pPrivateKeyFileName);

                if (_parcela.Parcela == 1)
                {
                    if (dto.Codigo > 0)
                        Doc = dto;
                    else
                        break;
                }
            }
             
            
            Doc.TotalImpostos = _parcelasList.Sum(t => t.TotalImpostos);
            Doc.TotalDescontos = _parcelasList.Sum(t => t.TotalDescontos);
            Doc.TotalIliquido = _parcelasList.Sum(t => t.TotalIliquido); 
            Doc.ValorTotal = (Doc.TotalIliquido- Doc.TotalDescontos)+ Doc.TotalImpostos;

            return Doc;
        }

        public string GenerateInvoicePDF(List<ReceiptDTO> itens, AcessoDTO pSessionInfo, int pCopyNumbers, string pLogoPath)
        {
            var dto = itens[0];
            string pCompanyName = dto.CompanyName, html = " <html>" +
"<head><title>KitandaSoft GE | Previsualizar Documento" +
"</title><style>" +
" table.tableclassname {" +
  "width: 400px;" +
"}" +
"* { margin: 0; padding: 0; } " +
".wrap { width: 80%; margin: 0 auto; font-size:11px;}" +
"page {" +
 "   background: white;" +
 "   display: block;" +
 "   margin: 0 auto;" +
 "   margin-bottom: 0.5cm;" +
 "  box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);" +
 "font-size:11px;" +

"}" +

"html {" +
 "   background: #999;" +
  "  cursor: default;" +
"}" +
"body {" +
 "   box-sizing: border-box;" +
  "  margin: 0 auto;" +
   " padding: 0.5in;" +
   " width: 8.5in;" +
   " background: #FFF;" +
   " border-radius: 1px;" +
   " box-shadow: 0 0 1in -0.25in rgba(0, 0, 0, 0.5);" +
   " font: 11px/1.4 Consolas;" +
"}" +
"footer {" +
 "   font-size: 9px;" +
  "  color: black;" +
   " text-align: center;" +

"}" +
"@media print {" +
 "   footer {" +
  "      position: fixed;" +
   "     bottom: 0;" +
    "    text-align: center;" +
     "   font-size: 7px;  " +
   " }" +
    ".content-block, p { " +
     "   page-break-inside: avoid;" +
    "}" +


"}" +
".tableContent{" +
 "   height: 296mm;" +
"}" +
"table { table-layout: fixed; width: 100%;  }" +
"table { border-collapse: separate; border-spacing: 3px; }" +
"th, td { border-width: 1px; position: relative; text-align: left;}" +
"th, td { border-radius: 0.25em; border-style: solid;   }" +
"th { background: #EEE; border-color: #BBB; }" +
"td { border-color: #DDD; }" +

".header { height: 5px; width: 100%; margin: 20px 0; background: #222; text-align: center; color: white; font: bold 11px Consolas; text-decoration: uppercase; letter-spacing: 1px; padding: 0px 0px; }" +
".headerDocx { height: 20px; width: 100%; margin: 20px 0; background: #222; text-align: center; color: white; font: bold 18px Consolas; text-decoration: uppercase; letter-spacing: 1px; padding: 0px 0px; }" +
".currency{" +
"    text-align:right;" +
 "   width:80px;" +
"}" +
".description{" +
 "   width: 300px;" +
"}" +
".qty{" +
 "   text-align:center;" +
  "  width:50px;" +
"}" +
".address { width: 400px; height: 120px; float: left; margin-top:20px; font-size:11px; margin-left:10px }" +
".customer { overflow: hidden; margin-left:10px; margin-top:50px; }" +

".logo { text-align: right; float: right; position: relative; margin-top: 5px; margin-right:20px; border: 1px solid #fff; width: 270px; height: 92px; vertical-align:top; margin-bottom:10px;  }" +

".customer-title { font-size: 12px; font-weight: bold; float: left; width:70%;  }" +

".meta { margin-top: 1px; width: 30%; float: right; }" +
".companyName{" +
"    font-weight:800; " +
"    font-size:15px;" +
"} " +

 ".terms { " +
  "  font-size:7px; " +
   " position: absolute;" +
   " right: 0;" +
    "bottom: 0;" +
    "left: 0;" +
   " padding: 1rem;" +
   " text-align: center;" +
   " margin-top:2px;" +
 "}" +

" .operador" +
" {" +
 "    text-align:center; " +
  "   margin-bottom:5px;" +

 "}" +
"fieldset {" +
 "   padding: 1em;" +
  "  border: 1px solid #ccc;" +
   " text-align: left;" +
    "width: 90%;" +
    "vertical-align: top;" +
"}" +


"fieldset.login label, fieldset.register label, fieldset.changePassword label" +
"{" +
 "   display: block;" +
"}" +

"fieldset label.inline " +
"{" +
 "   display: inline;" +
"}" +
"legend " +
"{" +
   " font-size: 1.1em;" +
   " font-weight: 600;" +
"} " +

".img-cicle{" +
"   border-radius: 15px 50px;" +
"    border-width:1px;" +
"    border-color: #DDD;" +
"   border-style: solid;" +

"}" +

"@page { margin: 0; }" +
"</style></head>" +
"<body> " +
"<div id='wrap'>";
            string DocumentCurrency = itens[0].DocumentCurrency == string.Empty ? pSessionInfo.Settings.CurrencySimbol : itens[0].DocumentCurrency;
            for (int i = 1; i <= pCopyNumbers; i++)
            {
                html += " <table style='width: 100%; height: 100%' class='tableContent'>" +

                      "<tr>" +
                          "<td>" +
                                  "<div class='address'>" +
                                      "<span class='companyName'>" + pSessionInfo.Settings.BranchDetails.NomeComercial + "</span>" +
                                      "<br />" +
                                      "Nº de Contribuinte: " + pSessionInfo.Settings.BranchDetails.Identificacao + "<br />" + pSessionInfo.Settings.BranchDetails.Rua +
                                      "<br />" + pSessionInfo.Settings.BranchDetails.Bairro + "<br/>" +
                                      "(+244) -" + pSessionInfo.Settings.BranchDetails.Telefone;
                if (pSessionInfo.Settings.BranchDetails.TelefoneAlt != "")
                {
                    html += " / " + pSessionInfo.Settings.BranchDetails.TelefoneAlt;
                }
                html += "<br />" + pSessionInfo.Settings.BranchDetails.Email +
                "<br />" + pSessionInfo.Settings.BranchDetails.WebSite +
            "</div>" +

            "<div class='logo'>" +


              "  <img class='img-cicle' src='" + pLogoPath + "' style='width: 100%; height: 100%;' />" +
              "</div>"+
              
              "<div style='clear: both'></div>" +

                "<div class='customer'>" +

                  "<br/><p><hr style='border-width:1px; color:black; width:98%'/>  <h2><strong> &nbsp;" + dto.DocumentType + " Nº " + dto.DocumentReference + "</strong></h2>" +

                    "<hr style='border-width:1px; color:black; width:98%'/></p>" +
                    "<div class='customer-title'>" +

                     "Exmo.(a) Sr(a),< br /> <b><span style='color: navy; font-size: 16px'>" + dto.Customer + "</span></b><br />" +
                     "MORADA: " + dto.CustomerAddressLine1 + " " + dto.CustomerAddressLine2 + "<br/>TEL: " + itens[0].CustomerPhoneNumber + "<br/> " + itens[0].CustomerNIF +
                     "<br />" +

                    "</div>" +

                    "<table class='meta'>" +
                     "   <tr>" +
                      "      <td class='header' colspan='2'>" + DocumentCopyNumber(i) + "</td>" +
                        "</tr>" +
                        "<tr>" +
                          "  <td style='text-align: center; font-weight: 600' colspan='2'>" + dto.DocumentType + " Nº <br />" + dto.DocumentReference +
                          "</td>" +
                       " </tr>" +
                       " <tr>" +
                       "     <th style='font-size:12px'>Data Emissão:</th>" +
                       "     <td style='text-align: center; font-size:12px;'>" + DateTime.Parse(itens[0].SalesDate).ToString("dd/MM/yyyy") + "</td>" +
                       " </tr>" +
                    "</table></div>" +
                     "<table class='items'>" +
                     
                     "<thead>   <tr>" +

                      "      <th style='width: 60px; text-align: center; font-size:11px;'>Moeda</th>" +
                       "     <th style='width: 60px; text-align: center; font-size:11px;'>Câmbio</th>" +
                        "    <th style='width: 90px; text-align: center; font-size:11px;'>Desc. Fin.(%)</th>" +
                         "   <th style='width: 90px; text-align: center; font-size:11px;'>Desc. Cli.(%)</th>" +
                          "  <th style='text-align: center; font-size:11px;'>Condição de Pagto</th>" +

                           " <th style='width: 100px; text-align: center; font-size:11px;'>Vencimento</th>" +
                        "</tr></thead>" +
                        "<tbody><tr>" + 
                         "<td style='text-align: center; font-size:11px;'>" + DocumentCurrency + "</td>" +
                         "<td style='text-align: center; font-size:11px;'>" + clsConfig.FormatarPreco(decimal.Parse(itens[0].DocumentRate)) + "</td>" +
                         "<td style='text-align: center; font-size:11px;'>" + clsConfig.FormatarPreco(decimal.Parse(itens[0].DocumentDiscount)) + "</td>" +
                         "<td style='text-align: center; font-size:11px;'>" + clsConfig.FormatarPreco(decimal.Parse(itens[0].CustomerDiscount)) + "</td>" +
                         "<td style='text-align: center; font-size:11px;'>" + itens[0].PaymentMethod + "</td>" +
                         "<td style='text-align: center; font-size:11px;'>" + DateTime.Parse(itens[0].DocumentDueDate).ToString("dd/MM/yyyy") + "</td>" +
                         "</tr> </tbody>" + 
                    "</table>" +
                    "<br />" +
                    "<table class='items'>" +
                    "<thead>"+
                    "<tr>"+
                    "<th style = 'width:100px; text-align:center; font-size:11px;'> Artigo </th>" +
                    "<th style='text-align:center; font-size:11px;'>Designação</th>" +
                    "<th style = 'width:60px; text-align:center; font-size:11px;'> Qtde</th>" +
                    "<th style='width:80px; text-align:center; font-size:11px;'>Preço</th>" +
                    "<th style ='width:50px; text-align:center; font-size:11px;' > Desc.</th> " +
                    "<th style='width:50px; text-align:center; font-size:11px;'>Imposto</th>" +
                    "<th style = 'width:80px; text-align:center; font-size:11px;' > Valor </th>" +
                    "</tr>"+
                    "</thead>"+
                    "<tbody>"+
                            
                    "</tbody>"+
                    "</table>"+
                 " </td>" +
                "</tr>" +
              "</table>" +
              "<footer>" +
               "   ****Processado por KitandaSoft GE - Desenvolvido por LucanSoft SI/TI, Lda - (+244) 924 313 857/933 406 840 | www.lucansolucoes.co.ao****" +
              "</footer>";
            }

            html += "</div>" +
            "</body>" +
            "</html>";


            return html;
        }


        public string GetBanckAccounts(AcessoDTO pSessionInfo)
        {
            List<ContaBancariaDTO> AccountsList = ContaBancariaRN.GetInstance().ListaContaBancarias(new ContaBancariaDTO(""))
            .Where(t=> t.Filial == pSessionInfo.Filial && t.Tipo == "B" && t.Situacao=="A").ToList();

            if(pSessionInfo.CustomerCurrencyId > 0 && pSessionInfo.CustomerCurrencyId != pSessionInfo.Settings.Currency)
            {
                AccountsList = AccountsList.Where(t => t.Moeda == pSessionInfo.CustomerCurrencyId).ToList();
            }

            string contas = "<span> ", Beneficiario= pSessionInfo.Settings.BranchDetails.NomeComercial;
            int BancoID =0;

            foreach (var account in AccountsList.Where(t=>t.Banco!=BancoID).ToList())
            {
                var Accounts = AccountsList.Where(t => t.Banco == account.Banco).ToList();
                BancoID = Accounts.FirstOrDefault().Banco;
                
                if(Accounts.Count > 1)
                {
                    foreach (var conta in Accounts.Where(t=>t.NumeroConta!=account.NumeroConta).ToList())
                    {
                        contas += " CONTA (" + account.SiglaMoeda + "): " + account.NumeroConta + " - IBAN: " + account.IBAN;
                    }

                }
                else
                {
                    contas += account.NomeBanco + " - CONTA (" + account.SiglaMoeda + "): " + account.NumeroConta + " - IBAN: " + account.IBAN;
                }
                contas += "<br/>"; 
            }

            Beneficiario = (AccountsList.Count > 0 && !string.IsNullOrEmpty(AccountsList[0].Beneficiario)) ? AccountsList[0].Beneficiario : Beneficiario;
            //pBackgroundColor = !string.IsNullOrEmpty(pBackgroundColor) ? pBackgroundColor : "#000080";
            return "<div style='width: 100%; font-size:9px; margin-left: 5px;'>" +
            "<div style='width: 100%; background-color: #0085B2;'>" +
                "<span style='font-size: 11px; color: white; font-weight: 600;'>COORDENADAS BANCÁRIAS - " + Beneficiario + "</span></div>" +
                "<div style='width: 100%;' ><b>" + contas + "</b></span>" +
                 
                 "</div>"+
            "</div>";
        } 

        public string GetTaxesLabel(List<ReceiptDTO> pItemsList, string pRegimeFiscal)
        {

            string taxesIncideLine = "";
            var taxesList = clsConfig.ListaImpostos();
            if (pRegimeFiscal == empresaEmRegimeGeral || pRegimeFiscal == empresaEmRegimeTransitorio || pRegimeFiscal == empresaEmRegimeEspecialCabinda)
            {
                 
                foreach (var item in pItemsList.ToList())
                {
                    if (pItemsList.Count > 0)
                    {
                        var tax = taxesList.Where(t => t.Codigo == item.TaxID).SingleOrDefault();

                        var incidencia = pItemsList.Where(t => t.TaxID == item.TaxID).ToList();

                        if(incidencia.Count > 0)
                        {
                            decimal IncidenciaValue = incidencia.Sum(t => (decimal.Parse(t.ProductQuantity) * decimal.Parse(t.ProductPrice)));
                            decimal desconto = decimal.Parse(incidencia.FirstOrDefault().DocumentDiscountValue);
                            IncidenciaValue -= desconto;
                            decimal taxValue = IncidenciaValue * (tax.Valor / 100),
                            total = taxValue > 0 ? IncidenciaValue + taxValue : 0;

                            pItemsList = pItemsList.Where(t => t.TaxID != item.TaxID).ToList();
                            taxesIncideLine += "<tr>";
                            if (total > 0 || tax.Sigla != RegimeTransitorio)
                            {
                                taxesIncideLine += "<td style='text-align:left'>" + tax.Sigla + "</td>" +
                                "<td style='text-align:right'>" + clsConfig.FormatarNumero(IncidenciaValue) + "</td>" +
                                "<td style='text-align:right'>" + clsConfig.FormatarNumero(taxValue) + "</td>" +
                                "<td style='text-align:right'>" + clsConfig.FormatarNumero(total) + "</td>" +
                                "</tr>";
                            }/*
                        else
                        {
                            taxesIncideLine += "<td style='text-align:left' colspan='4'>" + tax.Descricao.Replace("(0,00%)", string.Empty) + "</td> </tr>";
                        }*/
                        }
                    }
                }

                if (pRegimeFiscal == empresaEmRegimeTransitorio)
                    taxesIncideLine += "<tr><td style='text-align:left; border:none' colspan='4'>" + taxesList.Where(t => t.Sigla == RegimeTransitorio).SingleOrDefault().Descricao.Replace("(0,00%)", string.Empty) +"</td></tr>";
            }
            else
            {
                var tax = taxesList;
                 
                taxesIncideLine += "<tr><td style='text-align:left; border:none' colspan='4'>" + tax.Where(t => t.Sigla == RegimeNaoSujeicao).SingleOrDefault().Descricao.Replace("(0,00%)", string.Empty) + "</td></tr>";
            }

            return taxesIncideLine; 
        }

        public string GetTaxesLabel(List<ItemFaturacaoDTO> pItemsList, string pRegimeFiscal)
        {

            string taxesIncideLine = "<tr>";

            if (pRegimeFiscal== empresaEmRegimeGeral || pRegimeFiscal == empresaEmRegimeTransitorio || pRegimeFiscal == empresaEmRegimeEspecialCabinda)
            {
                foreach (var item in pItemsList.ToList())
                {
                    if (pItemsList.Count > 0)
                    {

                        if (item.TaxID > 0)
                        {
                            var tax = clsConfig.ListaImpostos().Where(t => t.Codigo == (item.TaxID == 0 ? -1 : item.TaxID)).SingleOrDefault();

                            var incidencia = pItemsList.Where(t => t.TaxID == (item.TaxID == 0 ? -1 : item.TaxID)).ToList();

                            decimal IncidenciaValue = incidencia.Sum(t => t.Quantidade * (t.PrecoMilheiro > 0 ? t.PrecoMilheiro : t.PrecoUnitario));
                            IncidenciaValue -= (incidencia.Sum(t => t.ValorDesconto));
                            decimal taxValue = IncidenciaValue * (tax.Valor / 100),
                            total = taxValue > 0 ? IncidenciaValue + taxValue : 0;

                            pItemsList = pItemsList.Where(t => t.TaxID != item.TaxID).ToList();

                            if (total > 0 || tax.Sigla != RegimeTransitorio)
                            {
                                taxesIncideLine += "<td style='text-align:left'>" + tax.Sigla + "</td>" +
                            "<td style='text-align:right'>" + clsConfig.FormatarNumero(IncidenciaValue) + "</td>" +
                            "<td style='text-align:right'>" + clsConfig.FormatarNumero(taxValue) + "</td>" +
                            "<td style='text-align:right'>" + clsConfig.FormatarNumero(total) + "</td>" +
                            "</tr>";
                            }
                            else
                            {
                                taxesIncideLine += "<td style='text-align:left' colspan='4'>" + tax.Descricao + "</td>" +
                            "</tr>";
                            }
                        }
                        pItemsList.Where(t => t.TaxID != item.TaxID);
                    }
                }

                if (pRegimeFiscal == empresaEmRegimeTransitorio)
                    taxesIncideLine += "<tr><td style='text-align:left; border:none' colspan='4'>" + clsConfig.ListaImpostos().Where(t => t.Sigla == RegimeTransitorio).SingleOrDefault().Descricao.Replace("(0,00%)", string.Empty) + "</td></tr>";
            }
            else
            {
                var tax = clsConfig.ListaImpostos();

                taxesIncideLine += "<tr><td style='text-align:left; border:none' colspan='4'>" + tax.Where(t => t.Sigla == "M04").SingleOrDefault().Descricao.Replace("(0,00%)", string.Empty) + "</td></tr>";
            }


            return taxesIncideLine;
        }

         

        public List<ReceiptDTO> InvoiceTaxList(List<ReceiptDTO> pItemsList)
        {
            var taxesIncidenceList = new List<ReceiptDTO>();
            foreach (var item in pItemsList.ToList())
            {
                if (item.TaxID > 0)
                {
                    var tax = clsConfig.ListaImpostos().Where(t => t.Codigo == (item.TaxID == 0 ? -1 : item.TaxID)).SingleOrDefault();

                    var incidencia = pItemsList.Where(t => t.TaxID == (item.TaxID == 0 ? -1 : item.TaxID)).ToList();

                    decimal IncidenciaValue = incidencia.Sum(t => decimal.Parse(t.ProductQuantity) * decimal.Parse(t.ProductPrice));
                    IncidenciaValue -= (incidencia.Sum(t => decimal.Parse(t.DocumentDiscountValue)));
                    decimal taxValue = IncidenciaValue * (tax.Valor / 100),
                    total = IncidenciaValue + taxValue;

                    pItemsList = pItemsList.Where(t => t.TaxID != item.TaxID).ToList();

                    var taxIncidence = new ReceiptDTO();
                    taxIncidence.ProductDesignation = tax.Sigla;
                    taxIncidence.ProductPrice = clsConfig.FormatarNumero(IncidenciaValue);
                    taxIncidence.ProductTax = clsConfig.FormatarNumero(taxValue);
                    taxIncidence.ProductTotal = clsConfig.FormatarNumero(total);
                    taxIncidence.ComercialNotes = tax.Descricao;
                    taxesIncidenceList.Add(taxIncidence);
                }
            }

            return taxesIncidenceList;
        }

        public string DocumentCopyNumber(int pNumber)
        {
            string Via = "";

            switch (pNumber)
            {
                case 1:
                    Via = "ORIGINAL";
                    break;
                case 2:
                    Via = "DUPLICADO";
                    break;
                case 3:
                    Via = "TRIPLICADO";
                    break;
                case 4:
                    Via = "QUADRUPLICADO";
                    break;
                case 5:
                    Via = "QUINTUPLICADO";
                    break;
                case 6:
                    Via = "SEXTUPLICADO";
                    break;
            }

            return Via;
        }

        private string GetPaymentMethods(string pDocID, string pDocTypeID)
        {
            return GetDocumentPaymentMethods(pDocID, pDocTypeID);
        }

        public string GetDocumentLines(List<ReceiptDTO> itens)
        {
            string linhasTabela = "";
            int totalLinhas = itens.Count;

            foreach (var item in itens)
            {  
                item.ProductNotes = item.ProductNotes.Replace(";", "<br/>");

                linhasTabela += "<tr class='item-row'>" +
                        "<td class='description' style='font-size:8px; vertical-align:top; border:none'>" + item.ProductCode + "</td>" +
                        "<td class='description' style='font-size:8px; vertical-align:top; border:none'>" + item.ProductDesignation + "<br/><i>" + item.ProductNotes + "<i></td>" +
                        "<td class='qty' style='font-size:8px; vertical-align:top; border:none'>" + clsConfig.FormatarPreco(decimal.Parse(item.ProductQuantity)) + "</td>" +
                        "<td class='description' style='font-size:8px; vertical-align:top; text-align:right; border:none'>" + item.ProductUnity + "</td>" +
                         "<td class='currency' style='font-size:8px; vertical-align:top; border:none'>" + clsConfig.FormatarPreco(decimal.Parse(item.ProductPrice)) + "</td>" +
                         "<td class='currency' style='font-size:8px; vertical-align:top; border:none'>" + clsConfig.FormatarNumero(decimal.Parse(item.ProductDiscount)) + "</td>" +
                         "<td class='currency' style='font-size:8px; vertical-align:top; border:none'>" + clsConfig.FormatarNumero(decimal.Parse(item.ProductTax)) + "</td>" +
                         "<td class='currency' style='font-size:8px; vertical-align:top; border:none'>" + clsConfig.FormatarNumero(decimal.Parse(item.ProductTotal)) + "</td>" +
                         "</tr>";
                /*
                if (item.ProductDesignation.Length > 100)
                {
                    totalLinhas++;
                }*/

            }


            if (totalLinhas < 16 && totalLinhas > 0)
            {
                totalLinhas = (16 - totalLinhas) -2; // Calcula a Diferença de Linhas

                linhasTabela +=
                     "<tr class='item-row'>" +
                     "<td colspan='8' style='border:none'>";
                totalLinhas--;
                for (int i = 1; i <= totalLinhas; i++)
                {
                    linhasTabela += "&nbsp;<br/>";
                }

                linhasTabela += "</td>" +
                     " </tr>";
            } 
            return linhasTabela;
        }

        public List<ItemFaturacaoDTO> GetInvoiceItemList(List<ReceiptDTO> pItems)
        {
            var itemList = new List<ItemFaturacaoDTO>();

            foreach (var item in pItems)
            {
                if (!item.DocumentReference.Equals("") && !string.IsNullOrEmpty(item.ProductBarcode))
                {
                    var product = new ItemFaturacaoDTO();
                    product.Referencia = item.ProductCode;
                    product.Designacao = item.ProductDesignation;
                    product.Notas = item.ProductNotes;
                    product.Quantidade = decimal.Parse(item.ProductQuantity);
                    product.PrecoUnitario = decimal.Parse(item.ProductPrice);
                    product.Desconto = decimal.Parse(item.ProductDiscount);
                    product.Imposto = decimal.Parse(item.ProductTax);
                    product.TotalLiquido = decimal.Parse(item.ProductTotal);
                    itemList.Add(product);
                }
            }

            return itemList;
        }

        public Tuple<bool, string> UpdateCustomerOrderPayment(FaturaDTO invoice)
        { 
           return dao.SaveCustomerOrderPayment(invoice);
        }

        public List<FaturaDTO> GetMonthlySalesReport(FaturaDTO dto, List<FaturaDTO> lista)
        {
            lista = lista!=null && lista.Exists(t=>t.Emissao >=dto.EmissaoIni && t.Emissao <= dto.EmissaoTerm) ? lista : ObterVendasDiarias(dto).Where(t=>t.Emissao.Year == dto.EmissaoIni.Year).ToList();
            
            for(int i=1; i<=12; i++)
            {
                var month = new FaturaDTO
                {
                    DescricaoDocumento = new GenericRN().GetMonthName(i),
                    ValorFaturado = lista.Where(t => t.Emissao.Month == i).Sum(t => t.ValorFaturado),
                    TotalIliquido = lista.Where(t => t.Emissao.Month == i).Sum(t => t.TotalIliquido),
                    TotalDescontos = lista.Where(t => t.Emissao.Month == i).Sum(t => t.TotalDescontos),
                    TotalImpostos = lista.Where(t => t.Emissao.Month == i).Sum(t => t.TotalImpostos),
                    ValorRetencao = lista.Where(t => t.Emissao.Month == i).Sum(t => t.ValorRetencao),
                    ValorPago = lista.Where(t => t.Emissao.Month == i).Sum(t => t.ValorPago),  
                };

                month.Saldo = (month.ValorFaturado - month.ValorRetencao) - month.ValorPago;
                
                month.Emissao = new DateTime(dto.EmissaoIni.Year, i, 1);
                lista = lista.Where(t => t.Emissao.Month != i).ToList(); 
                lista.Add(month);


            }

            return lista;
        }

        public List<FaturaDTO> GetBestCustomerList(FaturaDTO dto)
        {
            var list = new List<FaturaDTO>();
            foreach (var invoice in ObterSalesExtractList(dto))
            {
                if (list.Exists(t => t.Entidade == invoice.Entidade))
                {
                    var doc = list.Where(t => t.Entidade == invoice.Entidade).First();
                    list = list.Where(t => t.Entidade != invoice.Entidade).ToList();
                    invoice.ValorTotal += doc.ValorTotal;
                    invoice.ValorPago += doc.ValorPago;
                }

                list.Add(invoice);
            }
            return list.OrderByDescending(t => t.ValorTotal).ToList();
        }

        public Tuple<bool, DateTime> IssueDataValid(int pSerieID, string pFilial, string pIssueDate)
        {
            FaturaDTO dto = new FaturaDTO();
            dto.Filial = pFilial;
            dto.Serie = pSerieID;
            var document = dao.GetLastInserted(dto); 
            if(DateTime.Parse(pIssueDate) < document.Emissao)
            {
                return new Tuple<bool, DateTime>(false, document.Emissao);
            }
            else
            {
                return new Tuple<bool, DateTime>(true, DateTime.Parse(pIssueDate));
            }
        }

        public ReceiptDTO GravarConsultaMesa(ReceiptDTO dto)
        {
            var receipt = dto; 
            dto = dao.GetConsultaMesa(dto);

            receipt.DocumentType = dto.DocumentType;
            receipt.Utilizador = dto.Utilizador;
            receipt.DocumentReference = dto.DocumentReference;
            receipt.TituloDocumento = dto.DocumentType; 

            return receipt;
        }

        public List<FaturaDTO> GetPOSExtractList(FaturaDTO dto)
        {
            return dao.GetPosExtractList(dto);
        }

        public ProductPriceListDTO TablePriceSeleted(int pProductID, string pCustomerTablePriceId)
        {
            var ProductPriceList = ProductPriceListRN.GetInstance().ObterPorFitro(new ProductPriceListDTO(pProductID, int.Parse(pCustomerTablePriceId)));
            return ProductPriceList != null && ProductPriceList.Count > 0 ? ProductPriceList.Where(t => t.PriceTableID == int.Parse(pCustomerTablePriceId)).SingleOrDefault()
                : new ProductPriceListDTO();
        }

         

        public Tuple<List<ItemFaturacaoDTO>, string> AddProductToInvoice(List<ItemFaturacaoDTO> lista, string[] pNewProductDetails, string pCustomerTablePriceId, 
            AcessoDTO pSessionInfo, int pCustomerID, decimal pTaxRate, int pCustomerTaxID)
        {
            var ProductID = int.Parse(pNewProductDetails[0]);
            string pvpAlertMSG = string.Empty;
            pCustomerTablePriceId = pCustomerTablePriceId == "-1" ? "1" : pCustomerTablePriceId;
            decimal PrecoUnitario = 0;
            if (!lista.Exists(t=>t.Artigo == ProductID) || (lista.Exists(t => t.Artigo == ProductID) && !pSessionInfo.Settings.InvoiceAcumulaQuantidade))
            {
                var ProductPrice = TablePriceSeleted(ProductID, pCustomerTablePriceId);
                var TaxesList = ImpostosRN.GetInstance().ObterPorFiltro(new ImpostosDTO
                {
                    Descricao = string.Empty
                });

                ProductPrice.ImpostoID = pCustomerTaxID > 0 ? pCustomerTaxID : ProductPrice.ImpostoID;
                pNewProductDetails[11] = pCustomerTaxID > 0 ? pCustomerTaxID.ToString() : pNewProductDetails[11];

                if (pSessionInfo.Settings.BranchDetails.CustomerFiscalCodeID==empresaEmRegimeGeral || 
                    pSessionInfo.Settings.BranchDetails.CustomerFiscalCodeID == empresaEmRegimeEspecialCabinda)
                {
                    if(ProductPrice.ImpostoID > 0 || (!string.IsNullOrEmpty(pNewProductDetails[11]) && pNewProductDetails[11]!="0"))
                    {
                        var ItemTax = TaxesList.Where(t => t.Codigo == int.Parse(pNewProductDetails[11])).SingleOrDefault();
                        
                        if (ItemTax==null || ItemTax.Tipo != RegimeGeral)
                        {
                             
                            pvpAlertMSG = "ShowMessage('O Imposto definido na ficha do artigo não corresponde a taxa normal do IVA.', 'Alert');"; 
                        }else if (ItemTax.Sigla == RegimeNaoSujeicao || ItemTax.Sigla == RegimeTransitorio)
                        { 
                            pvpAlertMSG = "ShowMessage('A Isenção definida na ficha do Artigo só é aplicável ao Regime Transitório ou Regime de não Sujeição. " +
                                "Para poder faturar este artigo, deve alterar o tipo de isenção na ficha de cadastro do Artigo', 'Alert');";
                        }
                        else
                        {
                            ProductPrice.PercentualImposto = ItemTax.Valor;
                        }
                    }
                    else 
                    {
                        pvpAlertMSG = "ShowMessage('O Artigo tem não definido na sua ficha a taxa de IVA e Isenção.', 'Alert');";
                    }
                }
                else
                {
                    
                    if (pSessionInfo.Settings.BranchDetails.CustomerFiscalCodeID == empresaEmRegimeTransitorio)
                    {
                        //ProductPrice.ImpostoID = TaxesList.Where(t => t.Sigla == RegimeTransitorio).SingleOrDefault().Codigo;

                        var tax = TaxesList.Where(t => t.Codigo == int.Parse(pNewProductDetails[11])).SingleOrDefault();
                        if (tax.Sigla == RegimeTransitorio)
                            ProductPrice.ImpostoID = tax.Codigo;
                        else
                            pvpAlertMSG = "ShowMessage('O Imposto definido na ficha do Artigo não Corresponde ao Regime Transitório. Deverá fazer a alteração na ficha do artigo antes poder factura-lo');";
                    }
                    else
                    {
                        ProductPrice.ImpostoID = TaxesList.Where(t => t.Sigla == RegimeNaoSujeicao).SingleOrDefault().Codigo; 
                    }
                    pNewProductDetails[11] = ProductPrice.ImpostoID.ToString();
                    ProductPrice.PercentualImposto = 0;
                    pNewProductDetails[13] = "0";
                    pNewProductDetails[23] = "0";
                }


                if (pvpAlertMSG == string.Empty)
                {
                    PrecoUnitario = clsConfig.ValorDecimal(pNewProductDetails[5]);

                    decimal CurrentExchange = pTaxRate;

                    if (int.Parse(pNewProductDetails[29]) > 1)
                    {
                        CurrentExchange = pTaxRate <=1 ? CambioRN.GetInstance().ObterCambioActualizado(new CambioDTO
                        {
                            Moeda = pNewProductDetails[29],
                            Filial = pSessionInfo.Filial
                        }).CambioCompra : pTaxRate;

                        PrecoUnitario = clsConfig.ValorDecimal(pNewProductDetails[5]) * CurrentExchange;
                    }
                    ItemFaturacaoDTO dto = new ItemFaturacaoDTO
                    {
                        NroOrdenacao = lista.Count + 1,
                        Artigo = ProductID,
                        Referencia = pNewProductDetails[2],
                        Designacao = pNewProductDetails[3],
                        PrecoUnitario = PrecoUnitario,
                        Quantidade = 1,
                        MovimentaStock = pNewProductDetails[9] == "True" ? true : false,
                        Notas = string.Empty,
                        ArmazemID = int.Parse(pNewProductDetails[16]),
                        Composicao = pNewProductDetails[19] != "1" ? false : true,
                        ComposeID = -1,
                        PrecoUnitarioOriginalCurrency = clsConfig.ValorDecimal(pNewProductDetails[5]),
                        OriginalCurrencyID = int.Parse(pNewProductDetails[29]) <= 0 ? pSessionInfo.Settings.Currency : int.Parse(pNewProductDetails[29]),
                        UnidadeID = pNewProductDetails[17].ToUpper(),
                        Unidade = pNewProductDetails[30],
                        ProductType = pNewProductDetails[22],
                        Cambio = CurrentExchange
                    };

                     
                    var _unidade = clsConfig.ListaUnidadesMedidas().Where(t => t.Codigo == int.Parse(dto.Unidade)).ToList().Single();

                    if (_unidade != null)
                    {
                        dto.Unidade = _unidade.Sigla;
                        dto.Quantidade = _unidade.Quantidade;
                        dto.FactorConversao = _unidade.FactorConversao;
                        dto.ValorConversao = _unidade.Quantidade;
                    }

                    if (ProductPrice.PrecoVenda > 0 && dto.PrecoUnitario != ProductPrice.PrecoVenda)
                    {

                        dto.PrecoUnitario = PrecoUnitario > 0 ? PrecoUnitario : ProductPrice.PrecoVenda;
                        dto.TaxID = ProductPrice.ImpostoID;
                        dto.Imposto = ProductPrice.PercentualImposto;
                        dto.ValorImposto = (dto.PrecoUnitario * dto.Quantidade) * (dto.Imposto / 100);
                    }
                    else
                    {
                        var tax = TaxesList.Where(t => t.Codigo == int.Parse(pNewProductDetails[11])).SingleOrDefault();
                        dto.TaxID = pNewProductDetails[11] == null || pNewProductDetails[11] == "" ? -1 : int.Parse(pNewProductDetails[11]);
                        dto.Imposto = tax.Valor!=decimal.Parse(pNewProductDetails[13]) ? tax.Valor : decimal.Parse(pNewProductDetails[13]);
                        dto.ValorImposto =dto.Imposto != decimal.Parse(pNewProductDetails[13]) ? (dto.PrecoUnitario * dto.Quantidade) * (dto.Imposto / 100)
                        : decimal.Parse(pNewProductDetails[23]);
                    }

                    if (ProductPrice.PriceTableID > 1 && ProductPrice.PrecoVenda == 0)
                    {
                        pvpAlertMSG = "ShowMessage('O Cliente tem como preço padrão definido na sua ficha, o " + ProductPrice.CurtaDescricao + ", mas o artigo " + dto.Designacao + " não tem o " + ProductPrice.CurtaDescricao + " definido.', 'Alert');";
                    }

                    dto.Desconto = decimal.Parse(pNewProductDetails[26]) == 0 ? PromocaoRN.GetInstance().GetProductSalesPromotion(new ArtigoDTO(dto.Artigo)).Valor : decimal.Parse(pNewProductDetails[26]);
                    dto.ValorDesconto = dto.Desconto > 0 ? dto.PrecoUnitario * (dto.Desconto / 100) : dto.Desconto;


                    var totalLiquido = (dto.PrecoUnitario * dto.Quantidade) - dto.ValorDesconto;
                    dto.TotalLiquido = dto.Imposto > 0 ? totalLiquido + (totalLiquido * (dto.Imposto / 100)) : totalLiquido;
                    dto.PrecoCusto = decimal.Parse(pNewProductDetails[14]) <= 0 ? decimal.Parse(pNewProductDetails[6]) : decimal.Parse(pNewProductDetails[14]);
                    dto.ArmazemID = int.Parse(pNewProductDetails[16]);
                    dto.WareHouseName = pNewProductDetails[25];
                    dto.ExistenciaAnterior = decimal.Parse(pNewProductDetails[8]);
                    /*dto.SerialNumberID = int.Parse(pNewProductDetails[16]);
                    dto.DimensaoID = int.Parse(pNewProductDetails[16]);
                    dto.LoteID = int.Parse(pNewProductDetails[16]);
                    */
                    dto.ItemNotes = pNewProductDetails[27];
                    dto.Notas = pNewProductDetails[27];
                     
                    dto.WithRetention = pNewProductDetails[28] == "1" && pNewProductDetails[22] == "S" ? true : false;
                    if(dto.WithRetention && totalLiquido >= clsConfig.minimalWithholdValue)
                    {
                        dto.Retencao = dto.WithRetention ? (dto.PrecoUnitario > 0 ? totalLiquido * (pSessionInfo.Settings.TaxaNormalRetencao / 100) : decimal.Parse(pNewProductDetails[28])) : 0;
                    }
                    else
                    {
                        dto.Retencao = 0; 
                    }
                    
                    
                    
                    if (pCustomerID > 0 && pCustomerID == int.Parse(pNewProductDetails[31]) && decimal.Parse(pNewProductDetails[8]) > 0)
                    {
                        pvpAlertMSG += "ShowMessage('O Cliente ainda tem em Stock: " + decimal.Parse(pNewProductDetails[8]) + " Unidades', 'Alert');";
                    }
                    lista.Add(dto);
                }
                
            }
            else
            {
                int index = lista.FindIndex(t => t.Artigo == ProductID);
                var artigo = lista[index];

                artigo.Quantidade++;
                
                artigo.ValorDesconto = (artigo.PrecoUnitario /* dto.Quantidade*/) * (artigo.Desconto / 100)/*+ (artigo.PrecoUnitario * DocumentDiscount)*/;
                artigo.Retencao = artigo.WithRetention && artigo.ProductType == "S" ? ((pSessionInfo.Settings.TaxaNormalRetencao / 100) * (artigo.PrecoUnitario - artigo.ValorDesconto)) * artigo.Quantidade : 0;
                artigo.ValorImposto = ((artigo.PrecoUnitario /* dto.Quantidade*/) - artigo.ValorDesconto) * (artigo.Imposto / 100);
                artigo.TotalLiquido = ((artigo.PrecoUnitario - artigo.ValorDesconto) + artigo.ValorImposto) * artigo.Quantidade;
                lista[index] = artigo;
            }

            return new Tuple<List<ItemFaturacaoDTO>, string>(lista.OrderBy(p => p.NroOrdenacao).ToList(), pvpAlertMSG);

        }

        public List<ItemFaturacaoDTO> AddWorkOrderItemsToInvoice(List<OrderDetailDTO> pList, ConfiguracaoDTO pSettingsInfo)
        {
            List<ItemFaturacaoDTO> InvoiceItemsList = new List<ItemFaturacaoDTO>();
            int itemOrderNro = 1;
            foreach(var workOrderItem in pList)
            {
                ArtigoDTO dto = ArtigoRN.GetInstance().ObterPorPK(new ArtigoDTO(workOrderItem.ItemID));

                var item = new ItemFaturacaoDTO
                {
                    NroOrdenacao = itemOrderNro,
                    Artigo = dto.Codigo,
                    Designacao = dto.Designacao,
                    Referencia = dto.Referencia,
                    Quantidade = workOrderItem.Quantity,
                    PrecoUnitario = workOrderItem.UnitPrice,
                    Desconto = workOrderItem.Discount,
                    Imposto = dto.PercentualImposto 
                    
                };
                item.TaxID = item.Imposto > 0 ? ImpostosRN.GetInstance().ObterPorFiltro(new ImpostosDTO(-1, "", "")).Where(t => t.Valor == item.Imposto).FirstOrDefault().Codigo : -1; 
                item.ValorDesconto = item.Desconto > 0 ? item.PrecoUnitario * (item.Desconto / 100) : dto.Desconto;
                item.ValorImposto = item.TaxID > 0 ? ((item.PrecoUnitario - item.ValorDesconto)*(item.Imposto/100)) : 0;
                item.PrecoCusto = dto.PrecoCusto;
                item.UnidadeID = dto.UnidadeVenda;
                item.Unidade = UnidadeRN.GetInstance().ObterPorPK(new UnidadeDTO(int.Parse(dto.UnidadeVenda))).Sigla.ToUpper();
                item.MovimentaStock = dto.MovimentaStock;
                item.DescontoNumerario = 0;
                item.ItemNotes = workOrderItem.Notes;
                item.ArmazemID = workOrderItem.WareHouseID;
                item.ExistenciaAnterior = !dto.MovimentaStock ? 0 : dto.Quantidade;
                item.WareHouseName = dto.WareHouseName;
                item.Url = dto.FotoArtigo;

                item.Retencao = dto.WithRetention ? (((item.PrecoUnitario - item.ValorDesconto) * item.Quantidade) * (pSettingsInfo.TaxaNormalRetencao/100)) : 0;
                var totalLiquido = (item.PrecoUnitario * item.Quantidade) - item.ValorDesconto;
                item.TotalLiquido = item.Imposto > 0 ? totalLiquido + (totalLiquido * (item.Imposto / 100)) : totalLiquido;

                InvoiceItemsList.Add(item);
            }

            return InvoiceItemsList;
        }

        public ReciboDTO CreateCustomerReceiptToInvoice(List<FaturaDTO> pInvoicesList, AcessoDTO pSessionInfo, List<DocumentoComercialDTO> pDocumentList)
        { 
             

            var oDocument = pDocumentList.Where(t => t.Codigo == 18).SingleOrDefault(); 
            ReciboDTO dto = new ReciboDTO(); 
            dto.Entidade = EntidadeRN.GetInstance().GetByPK(new EntidadeDTO { Codigo = pInvoicesList[0].Entidade }).Codigo; 
            dto.Documento = oDocument.Codigo;
            dto.Serie = SerieRN.GetInstance().ObterPorFiltro(new SerieDTO { Documento = dto.Documento, Estado = 1, Ano = DateTime.Today.Year, Filial = pSessionInfo.Filial, Descricao = string.Empty }).FirstOrDefault().Codigo;
            dto.ValorPago = pSessionInfo.PaymentReceivedList.Sum(t=>t.Value);
            dto.Moeda = pSessionInfo.Settings.Currency.ToString();
            dto.Cambio = 1;
            dto.Utilizador = pSessionInfo.Utilizador;
            dto.Filial = pSessionInfo.Filial;
            dto.Emissao = DateTime.Now;
            dto.Numeracao = -1;
            dto.Referencia = string.Empty;
            dto.CustomerVAT = string.Empty;
            dto.Observacoes = string.Empty;
            dto.ValorDocumento = dto.ValorPago;

            dto = ReciboClienteRN.GetInstance().GenerateReceipt(dto, pInvoicesList, pSessionInfo.PaymentReceivedList, oDocument.Caixa);
            if (dto.Sucesso)
                UpdateCustomerOrderPayment(pInvoicesList[0]);


            return dto;

        } 

         

        public FaturaDTO EstornarDocumento(FaturaDTO dto, string pRefundType, PosDTO pUserPOS, string pPrivateKeyFileName)
        {
            if (pRefundType == "NCP" || pRefundType == "NC")
            { 
                 
                dto.Codigo = int.MinValue;
                dto.Referencia = string.Empty;
                dto.Documento = pRefundType == "D" ? pUserPOS.CashRefundDocumentID : pUserPOS.CreditRefundDocumentID;
                dto.Numeracao = int.MinValue;
                dto.Serie = pRefundType == "D" ? pUserPOS.CashRefundSerieID : pUserPOS.CreditRefundSerieID;
                
                
                dto.TotalIliquido = -dto.TotalIliquido;// + clsConfig.ValorDecimal(txtTotalDescontos.Text) - clsConfig.ValorDecimal(txtTotalImpostos.Text);
                dto.TotalImpostos = -dto.TotalImpostos;
                dto.TotalDescontos = -dto.TotalDescontos;
                dto.ValorTotal = -dto.ValorTotal;
                dto.TotalLiquido = dto.ValorTotal;
                dto.ValorPago = -dto.ValorTotal;
                dto.Troco = 0;
                var _DocumentItemsList = dto.ListaArtigos;
                if(dto.DocumentoOrigem > 0 && dto.Documento > 0)
                {
                    dto = dao.Adicionar(dto);
                    if (dto.Codigo > 0)
                    {
                        if (dto.Parcela <= 1)
                        {
                            var oDoc = new DocumentoComercialDTO(dto.Documento, "", "", 1, "E", "", "", "");
                           dto = SaveDocumentProductsList(dto,  false, oDoc, _DocumentItemsList, true, pPrivateKeyFileName);
                        }
                    }

                    if (dto.ListaArtigos.Count == 0)
                    {
                        dto.ListaArtigos = _DocumentItemsList;
                    }
                }  
            }

            return dto;
        }


        public List<FaturaDTO> ObterExtractoFaturacaoViatura(FaturaDTO dto)
        {
            dto.TituloDocumento = "";
            var salesList = dao.ObterPorVeiculos(dto).Where(t => t.TituloDocumento == "INVOICE" || t.TituloDocumento == "INVOICE_R" && t.StatusDocumento == 8).ToList();

            var _newSalesList = new List<FaturaDTO>();

            foreach (var sale in salesList)
            { 
                var productsList = ObterLucros(sale);
                sale.Lucro = productsList.Sum(t => t.Lucro); 
                if (!_newSalesList.Exists(t => t.Referencia == sale.Referencia))
                {
                    _newSalesList.Add(sale);
                }
                else
                {
                    var document = _newSalesList.Where(t => t.Referencia == sale.Referencia).SingleOrDefault(); 
                    int index = _newSalesList.FindIndex(t => t.Referencia == sale.Referencia);
                    sale.Codigo = document.Codigo;
                    sale.TotalImpostos = document.TotalImpostos + sale.TotalImpostos;
                    sale.TotalDescontos = document.TotalDescontos + sale.TotalDescontos;
                    sale.TotalIliquido = document.TotalIliquido + sale.TotalIliquido;
                    sale.ValorTotal = (sale.TotalIliquido - sale.TotalDescontos) + sale.TotalImpostos;
                    sale.ValorPago = document.ValorPago + sale.ValorPago;
                    sale.Saldo = document.Saldo + sale.Saldo; 
                    _newSalesList[index] = sale;
                }  
            }

            return _newSalesList.OrderBy(t => t.Emissao).ToList();
        }

        public List<FaturaDTO> GetMounthlySalesAnalyses(FaturaDTO dto)
        {
            List<FaturaDTO> lista = ObterSalesExtractList(dto);

            foreach(var invoice in lista)
            {
                invoice.Lucro = ObterLucros(invoice).Sum(t => t.Lucro);
            }

            MovimentoDTO movimento = new MovimentoDTO();
            movimento.DataIni = DateTime.Today.ToShortDateString();
            movimento.DataTerm = DateTime.Today.ToString();
            movimento.Filial = dto.Filial;
            var recebimentos = MovimentoRN.GetInstance().ObterRecebimentoDeClientes(movimento);
            for (int i = 1; i <= 12; i++)
            {
                decimal recebimentoPeriodo = 0;
                if (recebimentos.Where(t => t.NroOrdenacao == i)!=null)
                {
                    /*
                    recebimentoPeriodo = recebimentos.Where(t => t.NroOrdenacao == i).ToList().Count > 1 ?
                    :
                    recebimentos.Where(t => t.NroOrdenacao == i).ToList().SingleOrDefault().Valor;*/
                    recebimentoPeriodo = recebimentos.Where(t => t.NroOrdenacao == i).ToList().Sum(t => t.Valor);
                }
                decimal totalFaturado = lista.Where(t => t.Emissao.Month == i).Sum(t => t.ValorTotal),
                totalLucro = lista.Where(t => t.Emissao.Month == i).Sum(t => t.Lucro),
                totalRetencao = lista.Where(t => t.Emissao.Month == i).Sum(t => t.ValorRetencao),
                totalLiquidado = lista.Where(t => t.Emissao.Month == i).Sum(t => t.ValorPago);

                var month = new FaturaDTO
                {
                    DescricaoDocumento = new GenericRN().GetMonthName(i),
                    TotalIliquido = lista.Where(t => t.Emissao.Month == i).Sum(t => t.TotalIliquido),
                    TotalDescontos = lista.Where(t => t.Emissao.Month == i).Sum(t => t.TotalDescontos),
                    TotalImpostos = lista.Where(t => t.Emissao.Month == i).Sum(t => t.TotalImpostos),
                    ValorFaturado = totalFaturado,
                    ValorPago = recebimentoPeriodo, //lista.Where(t => t.Emissao.Month == i && t.TituloDocumento=="INVOICE_R").Sum(t => t.ValorPago),
                    Saldo = (totalFaturado - totalRetencao) - totalLiquidado,
                    Lucro = totalLucro,
                    ValorRetencao = totalRetencao,
                };
                month.Saldo = month.Saldo < 0 ? 0 : month.Saldo;
                month.Emissao = new DateTime(dto.EmissaoIni.Year, i, 1);
                lista = lista.Where(t => t.Emissao.Month != i).ToList();
                lista.Add(month);
            }

            return lista;
        }

        public void GenerateCustomerDocumentsHash(FaturaDTO dto, string pPrivateKeyFileName)
        {
            //List<CustomerInvoice> InvoicesList = new List<CustomerInvoice>();

           
        #pragma warning disable IDE0068 // Usar o padrão de descarte recomendado
                    object hasher = SHA1.Create();
        #pragma warning restore IDE0068 // Usar o padrão de descarte recomendado

            using (RSACryptoServiceProvider rsaCryptokey = new RSACryptoServiceProvider(1024))
            {
                rsaCryptokey.FromXmlString(GetRSAPrivateKey(pPrivateKeyFileName));

                byte[] stringToHashBuffer = Encoding.UTF8.GetBytes(dto.Hash);
                byte[] r = rsaCryptokey.SignData(stringToHashBuffer, hasher);

                dto.Hash = Convert.ToBase64String(r);
                SaveHash(dto);
            }
        }

        public string GetRSAPrivateKey(string pPrivateKeyFileName)
        {
            if (File.Exists(pPrivateKeyFileName))
            {
                string privatekey = File.ReadAllText(pPrivateKeyFileName).Trim();

                if (privatekey.StartsWith(RSAKeys.PEM_PRIV_HEADER) && privatekey.EndsWith(RSAKeys.PEM_PRIV_FOOTER))
                {
                    //this is a pem file
                    RSAKeys rsa = new RSAKeys();
                    rsa.DecodePEMKey(privatekey);

                    return rsa.PrivateKey;
                }
                else
                {
                    return privatekey;
                }
            }

            return string.Empty;
        }


        public List<FaturaDTO> ResumePeddingByCustomer(List<FaturaDTO> pLista)
        {
            pLista = pLista.OrderBy(t => t.Entidade).ToList();
            var PendingList = new List<FaturaDTO>();
            int CustomerID = 0;

            foreach (var document in pLista)
            {
                if (document.Entidade != CustomerID)
                {
                    CustomerID = document.Entidade; 
                    PendingList.Add(new FaturaDTO
                    {
                        Entidade = document.Entidade,
                        NomeEntidade = document.NomeEntidade,
                        Parcela = pLista.Count(t => t.Entidade == document.Entidade),
                        ValorTotal = pLista.Where(t => t.Entidade == document.Entidade).Sum(t => t.ValorTotal),
                        ValorPago = pLista.Where(t => t.Entidade == document.Entidade).Sum(t => t.ValorPago - t.Troco),
                        Saldo = pLista.Where(t => t.Entidade == document.Entidade).Sum(t => t.ValorTotal) - pLista.Where(t => t.Entidade == document.Entidade).Sum(t => t.ValorPago - t.Troco),
                        DiasAtrasado = pLista.Where(t => t.Entidade == document.Entidade).Sum(t => t.DiasAtrasado)
                    });
                }
            }

            return PendingList;
        }

        public void TerminarServico(ItemFaturacaoDTO dto)
        {
            daoItem.TerminarServico(dto);
        }

        public List<FaturaDTO> ObterPendentesPorPeriodoVencimento(FaturaDTO dto, string pFilter)
        {
            List<FaturaDTO> DocumentosObtidos = ObterDocumentosPendentes(dto);
             

            if (!string.IsNullOrEmpty(pFilter) && pFilter!="-1")
            {
                if (pFilter == "< 1" || pFilter == "<1")
                {
                    DocumentosObtidos = DocumentosObtidos.Where(t => t.DiasAtrasado <= 0).ToList();
                }else
                if (pFilter == "<=30" || pFilter == "<= 30")
                {
                    DocumentosObtidos = DocumentosObtidos.Where(t => t.DiasAtrasado > 0 && t.DiasAtrasado <= 30).ToList();
                }else
                if (pFilter == "< 60" || pFilter == "<60")
                {
                    DocumentosObtidos = DocumentosObtidos.Where(t => t.DiasAtrasado > 30 && t.DiasAtrasado <= 60).ToList();
                }
                else if (pFilter == ">60 and <90" || pFilter == "> 60 and < 90")
                {
                    DocumentosObtidos = DocumentosObtidos.Where(t => t.DiasAtrasado > 60 && t.DiasAtrasado < 90).ToList();
                }
                else if (pFilter == ">90 and <120" || pFilter== "> 90 and < 120")
                {
                    DocumentosObtidos = DocumentosObtidos.Where(t => t.DiasAtrasado > 90 && t.DiasAtrasado < 120).ToList();
                }
                else if (pFilter == "> 120" || pFilter == ">120")
                {
                    DocumentosObtidos = DocumentosObtidos.Where(t => t.DiasAtrasado >= 120).ToList();
                } 
            }
            return DocumentosObtidos;
        }

        public List<FaturaDTO> GetCustomerOrderPaymentsDocuments(FaturaDTO dto)
        {
            return dao.GetCustomerOrderPayments(dto);
        }

    }
}
