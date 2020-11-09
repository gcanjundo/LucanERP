using BusinessLogicLayer.Comercial;
using BusinessLogicLayer.Tesouraria;
using BusinessLogicLayer.Geral;
using BusinessLogicLayer.Comercial.Restauracao;
using BusinessLogicLayer.Seguranca;
using DataAccessLayer.Geral;
using Dominio.Comercial; 
using Dominio.Tesouraria;
using Dominio.Geral;
using Dominio.Comercial.Restauracao;
using Dominio.Seguranca;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dominio.Oficina; 
using BusinessLogicLayer.Comercial.Stock;
using System.Diagnostics;
using Microsoft.Win32;
using Dominio.Clinica;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public class GenericRN
    {
        AcessoDTO pSessionInfo;

        public string LicFilePath { get; set; }

        public GenericRN()
        {
            LicFilePath = "5fwZGsS2dzkdYsohKCf0afV+YaJ4RtI+5GNDwC8YH90=";
        }

        public GenericRN(AcessoDTO pSession)
        {
            pSessionInfo = pSession;
        }

        public enum MessageType { Sucesso, Erro, Informação, Alerta };

        public enum DocumentType { Invoice, InvoiceReceipt, CreditNote, CustomerOrder, Budget, DevolutionInvoiceReceipt, TransportGuide, CustomerReceipt, CustomerPayment, IsAdvanceBalance, IsAdvanceInvoice}

        public enum StockOperations { Input, Output, Transfer, Count, Inventory, InicalStock}

        public enum DealType { Purchase, Sales, Leasing}
        public enum DealStatus { Activo, Suspenso, Cancelado, Terminado }

        public EmpresaDTO CadastrarEmpresa(EmpresaDTO dto)
        {
            return EmpresaRN.GetInstance().Salvar(dto);
        }

        public decimal minimalWithholdValue = 20001;

        //Document Formates
         public string CUSTOMER_INVOICE = "INVOICE", CUSTOMER_INVOICE_RECEIPT = "INVOICE_R", CUSTOMER_INVOICE_ADVANCED = "INVOICE_A", CUSTOMER_INVOICE_CUSTOMER_PAYMENT = "INVOICE_P", CUSTOMER_CREDIT_NOTE = "INVOICE_D", SERVICE_ORDER = "SERVICE_O",
            TICKET = "TICKET", CUSTOMER_RECEIPT = "RECEIPT_R", CUSTOMER_ORDER = "INVOICE_E", CUSTOMER_RECEIPT_ADVANCE="RECEIPT_A", CUSTOMER_RECEIPT_CUSTOMER_PAYMENT = "RECEIPT_P", CUSTOMER_BUDGET = "INVOICE_O"; 

        //Tipos de Documentos V-Vendas, C-Compras, X-POS, L-Conta Corrente(Recebimento e Pagamentos), T-Tesouraria G-Transportes
        const string salesDocumentType="V", purchageDocumentType="C", cashDocumentType = "X", paymentDocumentType="L", tresureDocumentType ="T", transportation="G", supplierDocumentType="F";

        // Movimentos dos Documentos E- Entrada, S-Saida, aplicável a qualquer documento
        const string cashIncome = "E", outCome = "S";
        public string ShowMessage(string Message, MessageType type)
        {
            return "ShowMessage('" + Message + "','" + type + "');";
        }

        public string SuccessMessage()
        {
            return "ShowMessage('Dados Gravados com sucesso','" + MessageType.Sucesso + "');";
        }

        public List<DocumentoComercialDTO> ListaDocumentoContaCorrenteFornecedor()
        {
            return GetAllDocumentsList().FindAll(d => (d.Tipo == supplierDocumentType && d.Categoria == paymentDocumentType) || (d.Tipo == paymentDocumentType && d.Categoria == supplierDocumentType)).OrderBy(t => t.Categoria).ToList();
        }

        public object ListaCategoriasExames(string v)
        {
            throw new NotImplementedException();
        }

        public string ErrorMessage(string Message)
        {
            return "ShowMessage('" + Message + "','" + MessageType.Erro + "');";
        }

        public Boolean TemAcesso(int pFormulario)
        {
            bool AccessAllowed = false;
            if (pSessionInfo != null)
            {
                var formularios =  pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pFormulario);
                if (formularios!=null && formularios.AllowAccess == 1)
                    AccessAllowed = true;
            }
            return AccessAllowed;

        }

        public Tuple<string, string>SAFTKeys(string pSystem)
        {
            if (pSystem == "REST")
                return new Tuple<string, string>("/Chaves/ChavePrivadaREST.txt", "/Chaves/ChavePublicaREST.txt");
            else if (pSystem == "POS")
                return new Tuple<string, string>("/Chaves/ChavePrivadaPOS.txt", "/Chaves/ChavePublicaPOS.txt");
            else  
                return new Tuple<string, string>("/Comercial/saft/ChavePrivada.txt", "/Comercial/saft/ChavePublica.txt");
        }

        public List<EntidadeDTO> ListaEntidadesWithConvenio()
        {
            var lista = EntidadeRN.GetInstance().ObterWithConvenio(new EntidadeDTO { Filial = pSessionInfo.Filial });
            lista.Insert(0, new EntidadeDTO{ Codigo =-1, NomeComercial= "-SELECCIONE-", NomeCompleto = "-SELECCIONE-"});
            return lista;
        }

        public String FormatarPreco(decimal pPreco, string pMoeda)
        {
            pMoeda = pMoeda == string.Empty ? (pSessionInfo!=null ? pSessionInfo.Settings.CurrencySimbol : string.Empty) : pMoeda; 
            return  String.Format(new CultureInfo("pt-PT"), "{0:N2}", pPreco) + " " + pMoeda;
        }

         

        public List<BancoDTO> GetBanksDropDownList(string pFilialID)
        {
            var bankList = BancoRN.GetInstance().ObterPorFiltro(new BancoDTO("", pFilialID));
            bankList.Insert(0, new BancoDTO(-1, "-SELECCIONE-", "-SELECCIONE-"));

            return bankList;
        }

        public List<TabelaPrecoDTO> GetProductAllTablePricesList(string Filial)
        {
            var priceList = TabelaPrecoRN.GetInstance().ObterPorFiltro(new TabelaPrecoDTO(0, ""));

            priceList.Insert(0, new TabelaPrecoDTO(-1, "-SELECCIONE-"));
            return priceList;
        }

        public List<DocumentoComercialDTO> ListaDocumentosRetificativos()
        {
            return ListaGeralDocumentosForDropDown().FindAll(d => d.Codigo <= 0 || d.Formato == CUSTOMER_INVOICE_CUSTOMER_PAYMENT || d.Formato == CUSTOMER_CREDIT_NOTE);
        }

        public List<TabelaPrecoDTO> GenericTablePriceList()
        {
            var priceList = TabelaPrecoRN.GetInstance().ObterPorFiltro(new TabelaPrecoDTO(-1, "", "")); 
            priceList.Insert(0, new TabelaPrecoDTO(-1, "-SELECCIONE-"));
            return priceList;
        }

        public String FormatarPreco(decimal preco)
        {

            string precoFormatado = String.Format(new CultureInfo("pt-PT"), "{0:N2}", preco);
            return precoFormatado;
        }

        public String FormatarPrecoStr(string preco, string pMoeda)
        {
 
            return String.Format(new CultureInfo("pt-PT"), "{0:N2}", decimal.Parse(preco)) + " " + pMoeda;
        }

        public String FormatarNumero(decimal valor)
        {
            
            return String.Format(new CultureInfo("pt-PT"), "{0:N2}", valor);
        }

        public decimal ValorDecimal(String valor)
        {
            if (valor!=null && valor.Trim() != "")
            {
                valor = valor.Replace(".", "");
                int numero;
                string campo = "";
                decimal retorno = 0;
                bool isDecimal = decimal.TryParse(valor, out retorno);
                if (!isDecimal)
                {
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
                        retorno = int.TryParse(campo.ToString(), out numero) == true ? Convert.ToDecimal(campo) : 0;
                    }
                }
                else
                {
                    
                    retorno = decimal.Parse(valor);
                }

                return retorno;
            }
            else
            {
                return 0;
            }


        }

        public List<StatusDTO> BudgetStatusList()
        {
            return StatusDocumentosComerciais().Where(t=>t.Codigo!=2 && t.Codigo!=3 && t.Codigo!=9 && t.Codigo != 8).ToList();
        }

        public string formataTotaisRecibo(string pValor)
        {
            if (pValor.Contains(" "))
            {
                string[] text = pValor.Split(' ');
                decimal valor = ValorDecimal(text[0]);
                pValor = valor.ToString().Replace(".", ",");
            }

            return pValor;

        }

        public bool IsNIFValido(EntidadeDTO dto)
        {
            if (dto.LookupField1 == "1" || dto.Identificacao.Contains("999999999") || dto.Identificacao == "CONSUMIDOR FINAL" || dto.Identificacao.Length != 11)
                return false;

            return true;
        }

        public void TerminarSessao(AcessoDTO dto)
        {
            AcessoRN.GetInstance().AlterarAcesso(dto);
        }

        public string GetSupplierTaxCode(string regimeFiscal)
        {
            if (regimeFiscal == "G")
                return "3"; // Regime Geral
            else if (regimeFiscal == "T")
                return "44"; // Regime Transitório
            else if (regimeFiscal == "C")
                return "5"; // Cabinda
            else
                return "5"; // Não Sujeição

        }

        public List<PaisDTO> ListaPaises()
        {
            List<PaisDTO> lista = PaisRN.GetInstance().ObterPorFiltro(new PaisDTO(-1, ""));

            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new PaisDTO(-1, "-Seleccione-", "-Seleccione-"));
            return lista;
        }

        public List<ProvinciaDTO> ListaProvincias(int pais)
        {
            List<ProvinciaDTO> lista = new List<ProvinciaDTO>();
            if (pais > 0)
            {
                lista = ProvinciaRN.GetInstance().ObterPorFiltro(pais, "");
            }

            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new ProvinciaDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<MunicipioDTO> ListaMunicipios(int provincia)
        {
            List<MunicipioDTO> lista = new List<MunicipioDTO>();
            if (provincia > 0)
            {
                lista = MunicipioRN.GetInstance().ObterPorFiltro(new MunicipioDTO("", provincia));
            }

            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new MunicipioDTO(-1, "-Seleccione-"));
            return lista;
        }



        public List<DepartamentoDTO> ListaDepartamentos()
        {
            List<DepartamentoDTO> Departamento = DepartamentoRN.GetInstance().ObterPorFiltro(new DepartamentoDTO(-1, ""));
            if (!Departamento.Exists(t => t.Codigo <= 0))
                Departamento.Insert(0, new DepartamentoDTO(-1, "-Seleccione-"));
            return Departamento;
        }



        public List<StatusDTO> ListaStatus(string sigla)
        {
            List<StatusDTO> Status = StatusRN.GetInstance().ObterPorFiltro(new StatusDTO(-1, "", sigla));
            if (!Status.Exists(t => t.Codigo <= 0))
                Status.Insert(0, new StatusDTO(-1, "-Seleccione-"));
            return Status;
        }

        public List<StatusDTO> StatusAtendimento(string sigla)
        {
            List<StatusDTO> Status = StatusRN.GetInstance().ObterPorFiltro(new StatusDTO(-1, "", sigla));

            return Status;
        }



        public List<HabilitacoesDTO> ListaHabilitacoes()
        {
            List<HabilitacoesDTO> lista = HabilitacoesRN.GetInstance().ObterPorFiltro(new HabilitacoesDTO(-1, ""));
            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new HabilitacoesDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<ProfissaoDTO> ListaProfissoes()
        {
            List<ProfissaoDTO> Profissao = ProfissaoRN.GetInstance().ObterPorFiltro(new ProfissaoDTO(-1, ""));
            if (!Profissao.Exists(t => t.Codigo <= 0))
                Profissao.Insert(0, new ProfissaoDTO(-1, "-Seleccione-"));
            return Profissao;
        }

        public List<DocumentoDTO> ListaDocumentos()
        {
            List<DocumentoDTO> Documento = DocumentoRN.GetInstance().ObterPorFiltro(new DocumentoDTO(-1, ""));
            if (!Documento.Exists(t => t.Codigo <= 0))
                Documento.Insert(0, new DocumentoDTO(-1, "-Seleccione-", "-Seleccione-"));
            return Documento;
        }

        public List<DocumentoComercialDTO> ListaGeralDocumentosForDropDown()
        {
            var lista = GetAllDocumentsList();
            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new DocumentoComercialDTO(-1, "-Seleccione-"));
            return lista;
        }

        
        public Tuple<List<DocumentoComercialDTO>, string> GetDocumentsForSalesDropDownList(string pDocType)
        {
            var DocumentsList = ListaGeralDocumentosForDropDown();
            string DocListTitle = "Emissão de Documentos Venda & Faturação de Clientes";
            if (string.IsNullOrEmpty(pDocType))
            {
                DocumentsList = DocumentsList.FindAll(t => t.Codigo <= 0 || t.Formato == CUSTOMER_INVOICE || 
                t.Formato == CUSTOMER_INVOICE_RECEIPT || t.Formato == CUSTOMER_INVOICE_ADVANCED || t.Formato== CUSTOMER_BUDGET ||
                t.Formato == CUSTOMER_ORDER).OrderBy(t=>t.Formato).ToList();
            }
            else
            if(pDocType == CUSTOMER_INVOICE || pDocType == CUSTOMER_INVOICE_RECEIPT)
            {
                DocumentsList = DocumentsList.FindAll(t => t.Codigo <= 0 || t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_INVOICE_RECEIPT || t.Formato == CUSTOMER_INVOICE_ADVANCED);
            }
            else
            {
                //&nbsp;Emissão INVOICE_Ode Vendas, Encomendas &amp; Faturação de Clientes
                if(pDocType == CUSTOMER_BUDGET)
                {
                    DocListTitle = "Emissão de Orçamentos, Propostas, Estimativas, Proformas ou Notas de Preço";
                }
                else if(pDocType == CUSTOMER_CREDIT_NOTE || pDocType == CUSTOMER_INVOICE_CUSTOMER_PAYMENT)
                {
                    DocListTitle = "Emissão de Documentos Rectificativos ou Devoluções, Notas de Crédito ou Débito";
                }else if(pDocType== SERVICE_ORDER)
                {
                    DocListTitle = "Emissão de Ordens e Fichas de Serviço";
                }
                else if (pDocType == CUSTOMER_ORDER)
                {
                    DocListTitle = "Emissão de Notas de Encomendas";
                }else if(pDocType == "TRANSP_V")
                {
                    DocListTitle = "Emissão de Guias de Transporte e Remessas";
                }

                DocumentsList = DocumentsList.FindAll(d => d.Codigo == -1 || /*((d.Tipo == "V" && d.Categoria == "F") || (d.Tipo == "C" && d.Categoria == "D") &&*/ d.Formato == pDocType);
            }

            return new Tuple<List<DocumentoComercialDTO>, string>(DocumentsList, DocListTitle);
        }


        public Tuple<List<DocumentoComercialDTO>, string> GetDocumentsForPurchageDropDownList(string pDocType)
        {
            var DocumentsList = ListaGeralDocumentosForDropDown();
            string DocListTitle = "Emissão de Orçamentos, Facturas e Encomendas à Fornecedores";
            if (pDocType == null || pDocType == "" || pDocType == "SUPPLIER" || pDocType == "SUPPLIER_I" || pDocType == "SUPPLIER_V")
            {
                DocumentsList = DocumentsList.FindAll(t => t.Codigo <= 0 || t.Formato == "SUPPLIER_I" || t.Formato == "SUPPLIER_V");
            }
            else
            {
                //&nbsp;Emissão INVOICE_Ode Vendas, Encomendas &amp; Faturação de Clientes
                if (pDocType == "SUPPLIER_O")
                {
                    DocListTitle = "Emissão de Orçamentos, Propostas, Estimativas, Proformas ou Notas de Preço";
                }
                else if (pDocType == "SUPPLIER_D" || pDocType == "SUPPLIER_P")
                {
                    DocListTitle = "Emissão de Documentos Rectificativos ou Devoluções, Notas de Crédito ou Débito";
                } 
                else if (pDocType == "SUPPLIER_E")
                {
                    DocListTitle = "Emissão de Encomendas à Fornecedores";
                }

                DocumentsList = DocumentsList.FindAll(d => d.Codigo == -1 || /*((d.Tipo == "V" && d.Categoria == "F") || (d.Tipo == "C" && d.Categoria == "D") &&*/ d.Formato == pDocType);
            }

            return new Tuple<List<DocumentoComercialDTO>, string>(DocumentsList, DocListTitle);
        }




        public List<DocumentoComercialDTO> ListaDocumentosStock()
        {
            return ListaGeralDocumentosForDropDown().FindAll(d => d.Tipo == "S" || d.Codigo == -1);
        }

        public List<DocumentoComercialDTO> ListaDocumentoContaCorrenteCliente()
        {
            return GetAllDocumentsList().FindAll(d => (d.Tipo == "V" && d.Categoria == "F") || (d.Tipo == "L" && d.Categoria == "C")).OrderBy(t => t.Categoria).ToList();
        }

        public List<DocumentoComercialDTO> ListaDocumentosFaturacao(string pUtilizador)
        {
            List<DocumentoComercialDTO> lista = new List<DocumentoComercialDTO>();

            List<DocumentoComercialDTO> allDocuments = ListaGeralDocumentosForDropDown().FindAll(d => d.Tipo == "V" && d.Categoria == "F");
            foreach (var document in allDocuments)
            {

                if (AcessoDTO.AdminMaster != pUtilizador)
                {
                    document.Utilizador = pUtilizador;
                    DocumentoComercialDTO dto = DocumentoComercialRN.GetInstance().ObterPermissoes(document)
                    .Where(t => t.Codigo == document.Codigo).ToList().SingleOrDefault();

                    if (dto != null /*&& dto.AllowInsert == 1*/)
                    {
                        lista.Add(document);
                    }
                }
                else
                {
                    lista = allDocuments.ToList();
                    break;
                }

            }

            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new DocumentoComercialDTO(-1, "-SELECCIONE-"));

            
            return lista;
        }


        public List<DocumentoComercialDTO> ListaDocumentosOficinaMecanica(string pUtilizador)
        {
            List<DocumentoComercialDTO> lista = new List<DocumentoComercialDTO>(); 
            List<DocumentoComercialDTO> allDocuments = ListaGeralDocumentosForDropDown().FindAll(d =>d.Formato== SERVICE_ORDER);
            foreach (var document in allDocuments)
            {

                if (AcessoDTO.AdminMaster != pUtilizador)
                {
                    document.Utilizador = pUtilizador;
                    DocumentoComercialDTO dto = DocumentoComercialRN.GetInstance().ObterPermissoes(document)
                    .Where(t => t.Codigo == document.Codigo).ToList().SingleOrDefault();

                    if (dto != null && dto.AllowInsert == 1)
                    {
                        lista.Add(dto);
                    }
                }
                else
                {
                    lista = allDocuments.ToList();
                    break;
                } 
            }

            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new DocumentoComercialDTO(-1, "-SELECCIONE-"));
            return lista;
        }

        public List<DocumentoComercialDTO> ListaDocumentosTesouraria()
        {
            var lista = GetAllDocumentsList().Where(t => t.Tipo == tresureDocumentType).ToList();
            lista.Insert(0, new DocumentoComercialDTO { Codigo = -1, Descricao = "-Seleccione-" });
            return lista;
        }

        public List<DocumentoComercialDTO> ListaDocumentosCaixa()
        {
            return GetAllDocumentsList().Where(t => (t.Categoria == cashDocumentType && t.Formato==TICKET) || t.Codigo <= 0).ToList();
        }



        public List<GrauParentescoDTO> ListaGrauParentescos()
        {
            List<GrauParentescoDTO> lista = GrauParentescoRN.GetInstance().ObterPorFiltro(new GrauParentescoDTO(-1, ""));
            if (!lista.Exists(t => t.Codigo <= 0))
                lista.Insert(0, new GrauParentescoDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<ArmazemDTO> ListaArmazens(string pFilial)
        {
            var Armazem = GetWareHouseAllowedList(pFilial);
            if (Armazem != null && Armazem.Count > 1)
            {
                Armazem.Insert(0, new ArmazemDTO(-1, "-SELECCIONE-"));
            }
            return Armazem;
        }

        public List<MotivoDTO> ListaMotivo(string pFiltro)
        {
            var lista = MotivoMovimentoRN.GetInstance().ObterPorFiltro(new MotivoDTO()).Where(t=>t.Sigla == pFiltro).ToList();
            if (lista != null && lista.Count > 1)
            {
                lista.Insert(0, new MotivoDTO(-1, "-SELECCIONE-"));
            }
            return lista;
        }

        public List<ArmazemDTO> ListaTodosArmazens(string pFilial)
        {
            var Armazem = GetWareHouseAllowedList(pFilial);
            if (Armazem != null && Armazem.Count > 1)
            {
                Armazem.Insert(0, new ArmazemDTO(-1, "-Todos-"));
            }
            return Armazem;
        }

        private List<ArmazemDTO> GetWareHouseAllowedList(string pFilial)
        {
            var dto = new ArmazemDTO(-1, "", "", 1, pFilial);
            dto.PerfilID = int.Parse(pSessionInfo.Filial);
            dto.Utilizador = pSessionInfo.Utilizador;
            List<ArmazemDTO> WarehouseList = ArmazemRN.GetInstance().ObterPermissoes(dto).Where(t => t.Status == 1).ToList();

            if (WarehouseList != null || WarehouseList.Count > 0)
            {
                WarehouseList = WarehouseList.Where(t => t.Filial == pFilial).ToList();
            }

            return WarehouseList;
        }

        public List<ImpostosDTO> ListaImpostos()
        {
            List<ImpostosDTO> lista = ImpostosRN.GetInstance().ObterPorFiltro(new ImpostosDTO(-1, ""));
            lista.Insert(0, new ImpostosDTO(-1, "-Seleccione-", "-Seleccione-"));
            return lista;
        }

        public List<RetencaoFonteDTO> ListaRetencao()
        {
            return RetencaoFonteRN.GetInstance().GetForDropDownList();
        }

        public List<CategoriaDTO> ListaGruposClientes()
        {
            List<CategoriaDTO> lista = GrupoClientesRN.GetInstance().ObterPorFiltro(new CategoriaDTO(-1, "", "", ""));
            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new CategoriaDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<CategoriaDTO> ListarCategorias(string pFilial)
        {
            List<CategoriaDTO> Categoria = CategoriaRN.GetInstance().ObterPorFiltro(new CategoriaDTO {
            Descricao="",
            Codigo=-1,
            Sigla="",
            Filial = pFilial});
            Categoria.Insert(0, new CategoriaDTO(-1, "-Seleccione-"));
            return Categoria;
        }

        public List<CategoriaDTO> ListarSubCategoria(string pFiltro, string pFilial)
        {
            List<CategoriaDTO> lista = new List<CategoriaDTO>();

            if (!string.IsNullOrEmpty(pFiltro) && pFiltro != "-1")
            {
                lista = CategoriaRN.GetInstance().ObterPorFiltro(new CategoriaDTO {
                    Descricao="",
                    Categoria= pFiltro,
                    Filial = pFilial,
                    Sigla =""
                });
            }
            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new CategoriaDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<MoedaDTO> ListaMoedas()
        {
            return MoedaRN.GetInstance().ObterPorFiltro(new MoedaDTO(-1, "", ""));
        }

        public List<MeioExpedicaoDTO> ListaModoExpedicao()
        {
            List<MeioExpedicaoDTO> lista = MeioExpedicaoRN.GetInstance().ListaMeioExpedicao("");
            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new MeioExpedicaoDTO(-1, "-Seleccione-"));
            return lista;
        }


        public List<SerieDTO> ListaSeries(int pDocumento, string pFilial)
        {
            List<SerieDTO> Serie = SerieRN.GetInstance().ObterPorFiltro(new SerieDTO
            { 
                Documento = pDocumento,
                Filial = pFilial
            });//pSessionInfo.Settings.SeriesList.Where(t => (t.Documento == pDocumento && t.Filial == pFilial) || t.Codigo <=0).ToList();

            Serie.Insert(0, new SerieDTO(-1, "-Seleccione-"));
            return Serie;
        }

         

        public List<CategoriaDTO> ListaActividadesComerciais()
        {
            List<CategoriaDTO> lista = CategoriaRN.GetInstance().ObterPorFiltro(new CategoriaDTO(-1, "", "COM", "-1"));
            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new CategoriaDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<UnidadeDTO> ListaUnidadesMedidas()
        {
            List<UnidadeDTO> lista = UnidadeRN.GetInstance().ObterPorFiltro(new UnidadeDTO(-1, "", ""));
            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new UnidadeDTO(-1, "-Seleccione-", "-Seleccione-"));
            return lista;
        }

        public List<MarcaDTO> ListaMarcas()
        {
            List<MarcaDTO> lista = MarcaRN.GetInstance().ListaMarca(string.Empty);

            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new MarcaDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<MarcaDTO> ListaModelos(string pMarca)
        {

            List<MarcaDTO> lista = new List<MarcaDTO>();
            if (!string.IsNullOrEmpty(pMarca) && pMarca != "-1")
            {
                lista = MarcaRN.GetInstance().ListaModelos(new MarcaDTO(int.Parse(pMarca)));
            }

            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new MarcaDTO(-1, "-Seleccione-"));
            return lista;
        }


        public List<CondicaoPagamentoDTO> ListaCondicaoPagamentos()
        {
            List<CondicaoPagamentoDTO> lista = CondicaoPagamentoRN.GetInstance().ListaCondicaoPagamento("");
            if (!lista.Exists(t => t.Codigo == -1))
                lista.Insert(0, new CondicaoPagamentoDTO(-1, "-Seleccione-"));
            return lista;
        }




        public List<CoresDTO> ListaCores()
        {
            List<CoresDTO> lista = CoresRN.GetInstance().ListaCoress(string.Empty);
            lista.Insert(0, new CoresDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<TamanhoDTO> ListaTamanhos()
        {
            List<TamanhoDTO> lista = TamanhoRN.GetInstance().ListaTamanhos(string.Empty);
            lista.Insert(0, new TamanhoDTO(-1, "-Seleccione-"));
            return lista;
        }

        public List<FabricanteDTO> ListaFabricantes()
        {
            List<FabricanteDTO> lista = FabricanteRN.GetInstance().ListaFabricantes(string.Empty);
            lista.Insert(0, new FabricanteDTO(-1, "-Seleccione-"));

            return lista;
        }
        public List<ProvinciaDTO> ListaProvinciaAngolanas()
        {
            ProvinciaDTO dto = new ProvinciaDTO(0, "", 1, 1);
            List<ProvinciaDTO> lista = ProvinciaRN.GetInstance().ObterPorFiltro(dto);

            lista.Insert(0, new ProvinciaDTO(-1, "-Seleccione-", 1, 1));

            return lista;
        }

        public List<EmpresaDTO> ListaMinhasFiliais(string pUtilizador)
        {
            List<EmpresaDTO> lista = EmpresaRN.GetInstance().ObterMinhasFiliais(pUtilizador);

            EmpresaDTO dto = new EmpresaDTO();
            dto.Codigo = -1;
            dto.NomeComercial = "-Seleccione-";

            lista.Insert(0, dto);

            return lista;
        }

        public List<EmpresaDTO> ListaTodasSucursais()
        {
            List<EmpresaDTO> lista = EmpresaRN.GetInstance().ObterTodas();

            EmpresaDTO dto = new EmpresaDTO();
            dto.Codigo = -1;
            dto.NomeComercial = "-Seleccione-";

            lista.Insert(0, dto);

            return lista;
        }

        public List<LocalizacaoDTO> LocalizacaoRestaurante()
        {
            List<LocalizacaoDTO> lista = LocalizacaoRN.GetInstance().ObterPorFiltro(new LocalizacaoDTO());
            lista.Insert(0, new LocalizacaoDTO(-1, "-SELECCIONE-"));

            return lista;
        }


        public int idade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.Month < dataNascimento.Month || (DateTime.Now.Month == dataNascimento.Month && DateTime.Now.Day < dataNascimento.Day))
                idade--;

            return idade;

        }

        public String CalcularIdade(DateTime dNascimento)
        {
            int idDias = 0, idMeses = 0, idAnos = 0; // (id = idade)

            string ta = "", tm = "", td = "";
            DateTime dAtual = DateTime.Now;
            if (dAtual < dNascimento)
            {

                return "Data de nascimento inválida ";

            }

            if (dAtual.Day < dNascimento.Day)
            {
                idDias = (DateTime.DaysInMonth(dAtual.Year, dAtual.Month - 1));

                idMeses = -1;

                if (idDias == 28 && dNascimento.Day == 29)

                    idDias = 29;

            }

            if (dAtual.Month < dNascimento.Month)
            {

                idMeses = idMeses + 12;

                idAnos = -1;

            }

            idDias = dAtual.Day - dNascimento.Day + idDias;

            idMeses = dAtual.Month - dNascimento.Month + idMeses;

            idAnos = dAtual.Year - dNascimento.Year + idAnos;

            if (idAnos > 1)

                ta = idAnos + " anos ";

            else if (idAnos == 1)

                ta = idAnos + "ano";

            if (idMeses > 1)

                tm = idMeses + " meses ";

            else if (idMeses == 1)

                tm = idMeses + " mês ";

            if (idDias > 1)

                td = idDias + " dias ";

            else if (idDias == 1)

                td = idDias + " dia ";

            return ta + tm + td;

        }

        public Boolean FazAniversarioHoje(DateTime dataNascimento)
        {
            string diaNascimento = dataNascimento.Day.ToString() + "/" + dataNascimento.Month.ToString();
            string diaAniversario = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString();

            if (diaNascimento.Equals(diaAniversario))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        public Boolean PodeCadastrar(int pForm)
        {
            bool ActionAllowed = false;
            if (pSessionInfo != null)
            {
                var formularios = pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pForm);
                if (formularios != null && formularios.AllowInsert == 1)
                    ActionAllowed = true;

            }

            return ActionAllowed;
        }

        public Boolean PodeAlterar(int pForm)
        {
            bool ActionAllowed = false;
            if (pSessionInfo != null)
            {
                var formularios = pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pForm);
                if (formularios != null && formularios.AllowUpdate == 1)
                    ActionAllowed = true;

            }

            return ActionAllowed;
        }

        public Boolean PodeImprimir(int pForm)
        {
            bool ActionAllowed = false;
            if (pSessionInfo != null)
            {
                var formularios = pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pForm);
                if (formularios != null && formularios.AllowPrint == 1)
                    ActionAllowed = true;

            }

            return ActionAllowed;
        }

        

        public List<DocumentoComercialDTO> ListaDocumentosCompras()
        {
            return ListaGeralDocumentosForDropDown().FindAll(d => d.Codigo == -1 || ((d.Tipo == "C" || d.Tipo == "T") && d.Categoria=="C")).ToList();
        }

        public Boolean PodeExcluir(int pForm)
        {
            bool ActionAllowed = false;
            if (pSessionInfo != null)
            {
                var formularios = pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pForm);
                if (formularios != null && formularios.AllowDelete == 1)
                    ActionAllowed = true;

            }

            return ActionAllowed;
        }

        public Boolean PodeConsultar(int pForm)
        {
            bool ActionAllowed = false;
            if (pSessionInfo != null)
            {
                var formularios = pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pForm);
                if (formularios != null && formularios.AllowSelect == 1)
                    ActionAllowed = true;

            }

            return ActionAllowed;
        }

        

        public string DiaDaSemana(DateTime data)
        {
            int dia = Convert.ToInt32(data.DayOfWeek);
            string[] nome_dia = new string[7];

            nome_dia[0] = "Domingo";
            nome_dia[1] = "Segunda-Feira";
            nome_dia[2] = "Terça-Feira";
            nome_dia[3] = "Quarta-Feira";
            nome_dia[4] = "Quinta-Feira";
            nome_dia[5] = "Sexta-Feira";
            nome_dia[6] = "Sábado";

            string nome_dia_semana = nome_dia[dia].ToString();

            return nome_dia_semana;
        }

        public string DiaSemana(int dia)
        {
            string[] nome_dia = new string[7];

            nome_dia[0] = "Domingo";
            nome_dia[1] = "Segunda-Feira";
            nome_dia[2] = "Terça-Feira";
            nome_dia[3] = "Quarta-Feira";
            nome_dia[4] = "Quinta-Feira";
            nome_dia[5] = "Sexta-Feira";
            nome_dia[6] = "Sábado";

            string nome_dia_semana = nome_dia[dia].ToString();

            return nome_dia_semana;
        }

        private string GetMonth(int pMonth)
        {
            string[] MonthName = new string[12];
            MonthName[0] = "Janeiro";
            MonthName[1] = "Fevereiro";
            MonthName[2] = "Março";
            MonthName[3] = "Abril";
            MonthName[4] = "Maio";
            MonthName[5] = "Junho";
            MonthName[6] = "Julho";
            MonthName[7] = "Agosto";
            MonthName[8] = "Setembro";
            MonthName[9] = "Outubro";
            MonthName[10] = "Novembro";
            MonthName[11] = "Dezembro";
            return MonthName[pMonth].ToString();
        }

        public string numero(int numero)
        {
            return String.Format("{0,22:D8}", numero);
        }

        public DateTime PrimeiroDiaSemana(DateTime dayInWeek)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public DateTime UltimoDiaSemana(DateTime dayInWeek)
        {
            return PrimeiroDiaSemana(dayInWeek).AddDays(7);
        }

        public string DataExtenso(DateTime date)
        {
            string dia = DiaSemana(date.Day);
            string mes = GetMonth(date.Month - 1);

            return DiaSemana(date.Day) + ", ";
        }

        public List<EmpresaDTO> ListaFiliaisUtilizador(string utilizador)
        {
            return EmpresaRN.GetInstance().ObterMinhasFiliais(utilizador);
        }

        public List<PerfilEmpresaDTO> ListaFiliaisPerfil(string perfil)
        {
            PerfilEmpresaDTO dto = new PerfilEmpresaDTO();
            dto.Perfil = new PerfilDTO { Codigo = int.Parse(perfil) };
            dto.Filial = "-1";
            return PerfilFilialRN.GetInstance().ListaPerfis(dto);
        }

        public string CreatePassword(int tamanho)
        {
            const string SenhaCaracteresValidos = "abcdefghijklmnopqrstuvwxyz1234567890@#!?";
            int valormaximo = SenhaCaracteresValidos.Length;

            Random random = new Random(DateTime.Now.Millisecond);

            System.Text.StringBuilder senha = new System.Text.StringBuilder(tamanho);

            for (int indice = 0; indice < tamanho; indice++)
                senha.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);

            return senha.ToString();
        }

        public bool isEmailValido(string enderecoEmail)
        {
            try
            {
                //define a expressão regulara para validar o email
                string texto_Validar = enderecoEmail.ToLower();
                System.Text.RegularExpressions.Regex expressaoRegex = new System.Text.RegularExpressions.Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    // o email é valido
                    return true;
                }
                else
                {
                    // o email é inválido
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public Tuple<bool, string> EnviarEmail(string assunto, string destinario, string mensagem, int remetente, string emissor)
        {
            try
            {
                string invalidos = "";
                EmailMonitorDTO dto = EmailMonitorRN.GetInstance().Conta(new EmailMonitorDTO { Codigo = remetente });

                destinario.Replace(",", ";").Replace(" ", ";");

                string[] mailToList = destinario.Split(';');

                using (MailMessage mm = new MailMessage())
                {
                    mm.From = new MailAddress(dto.Endereco, dto.Remetente);

                    if(mailToList.Length > 1)
                    {
                        foreach (var mail in mailToList)
                        {
                            if (isEmailValido(mail))
                                mm.To.Add(mail);
                            else
                                invalidos += mail + "\n";
                        }
                    }
                    else
                    {
                        if (isEmailValido(destinario))
                        {
                            mm.To.Add(destinario.ToLower());
                        }
                        else
                        {
                            return new Tuple<bool, string>(false, "Correio electrónico inválido");
                        }
                    }
                    
                    if (emissor.Equals("CC"))
                    {
                        mm.Bcc.Add(dto.Endereco.ToLower());
                    }
                    mm.Subject = assunto;
                    mm.Body = mensagem;
                    mm.IsBodyHtml = true;
                    mm.BodyEncoding = Encoding.UTF8;
                    mm.SubjectEncoding = Encoding.UTF8;

                    mm.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = dto.Servidor,
                        EnableSsl = dto.AtivaSSL,
                        UseDefaultCredentials = dto.UseDefaultCredencial,
                        Port = dto.Porta

                    };

                    if (dto.AtivaSSL)
                    {
                        smtp.EnableSsl = true;
                    }
                    else
                    {
                        smtp.EnableSsl = false;
                    }
                    smtp.UseDefaultCredentials = true;
                    NetworkCredential NetworkCred = new NetworkCredential(dto.Usuario, dto.CurrentPassword);


                    smtp.Credentials = NetworkCred;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(mm);
                    return new Tuple<bool, string>(true, invalidos);

                }



                
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message.Replace("'", "") + ex.StackTrace.Replace("'", string.Empty));
            }
        }

        public List<DocumentoComercialDTO> GetAllDocumentsList()
        {
            return DocumentoComercialRN.GetInstance().ObterPorFiltro(new DocumentoComercialDTO(-1, ""));
        }
        public bool DocumentoMovimentaCaixa(string pDocumento)
        {
            if (!string.IsNullOrEmpty(pDocumento) && int.Parse(pDocumento) > 0)
            {
                var documento = GetAllDocumentsList().Where(t => t.Codigo == int.Parse(pDocumento)).SingleOrDefault();

                if (documento!=null && !string.IsNullOrEmpty(documento.Caixa))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<StatusDTO> StatusDocumentosComerciais()
        {
            return StatusRN.GetInstance().GetBillingDocumentStatusList();
        }

        public List<StatusDTO> StatusDocumentLine()
        {
            return StatusRN.GetInstance().GetDocumentLinesStatusList();
        }


        public List<StatusDTO> GetFinancialStatusList()
        {
            return StatusRN.GetInstance().DocumentPaymentStatusList();
        }

        public List<StatusDTO> GetCustomerOrdersStatus()
        {
            return StatusRN.GetInstance().GetCustomerOrderStatusList();
        }

        public int Turno()
        {
            int turno = 1;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            {
                turno = 1;
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            {
                turno = 2;
            }
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 23)
            {
                turno = 3;
            }
            return turno;
        }

        public List<PerfilDTO> ListaPerfis(string pUserName)
        {
            List<PerfilDTO> lista = new List<PerfilDTO>();
            PerfilDTO dto = new PerfilDTO
            {
                Descricao = pUserName, 
                Codigo = -1,
                Designacao = "-Seleccione-"
            }; 

            lista = PerfilRN.GetInstance().ObterTodos(dto);
            lista.Insert(0, dto);

            return lista;
        }

        public bool IsSerieFaturacaoActiva()
        {
            if (pSessionInfo == null || pSessionInfo.Settings == null || pSessionInfo.Settings.PosSalesDefaultDocument <= 0)
            {
                return false;
            }
            else
            {
                int PosDefaultDocument = int.Parse(pSessionInfo.Settings.PosSalesDefaultDocument.ToString()), pAno = DateTime.Today.Year;

                SerieDTO objSerieFaturacao = ListaSeries(PosDefaultDocument, pSessionInfo.Filial).Find(t => t.Documento == PosDefaultDocument && t.Termino >= DateTime.Today);
                // Para haver séries activas é necessário haver séries do ano Actual
                if (objSerieFaturacao != null && objSerieFaturacao.Codigo > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        


        public string ReplaceDots(string pText)
        {
            pText = pText.Replace("á", "a").Replace("Á", "A")
            .Replace("À", "A").Replace("à", "a").Replace("â", "a")
            .Replace("ã", "a").Replace("Ã", "A").Replace("Â", "A")
            .Replace("É", "E").Replace("é", "e").Replace("ê", "e")
            .Replace("Ê", "E").Replace("ô", "o").Replace("ó", "o")
            .Replace("ó", "o").Replace("ò", "o").Replace("õ", "o")
            .Replace("ç", "c").Replace("Ç", "C").Replace("õ", "o")
            .Replace("í", "i").Replace("í", "i").Replace("Í", "I")
            .Replace("Ì", "I").Replace("ú", "u").Replace("Ú", "U")
            .Replace("Ù", "u").Replace("ù", "u");

            return pText;
        }


        
        public List<TurnoDTO> ListaTurnos()
        {
            List<TurnoDTO> lista = TurnoRN.GetInstance().ObterPorFiltro(new TurnoDTO(-1, ""));

            TurnoDTO dto = new TurnoDTO();
            dto.Codigo = -1;
            dto.Descricao = "-Seleccione-";

            lista.Insert(0, dto);

            return lista;
        }

        public List<ComissaoDTO> ListaComissoes()
        {
            List<ComissaoDTO> lista = ComissaoRN.GetInstance().ObterPorFiltro(new ComissaoDTO(-1, ""));

            ComissaoDTO dto = new ComissaoDTO();
            dto.Codigo = -1;
            dto.Descricao = "-Seleccione-";

            lista.Insert(0, dto);

            return lista;
        }

        public string SenhaPadrao()
        {
            return Encrypt("123456");
        }

        public bool isGerente()
        {
            if (pSessionInfo == null)
            {
                string pUser = pSessionInfo.Utilizador;
                int pProfile = pSessionInfo.UserProfile;

                return AcessoRN.GetInstance().HasGerenteProfile(pUser, pProfile) ? true : false;
            }
            else
            {
                return false;
            }
        }


        public string GetMonthName(int pMes)
        {
            string MesExtenso = "";

            switch (pMes)
            {
                case 1:
                    MesExtenso = "Janeiro";
                    break;
                case 2:
                    MesExtenso = "Fevereiro";
                    break;
                case 3:
                    MesExtenso = "Março";
                    break;
                case 4:
                    MesExtenso = "Abril";
                    break;
                case 5:
                    MesExtenso = "Maio";
                    break;
                case 6:
                    MesExtenso = "Junho";
                    break;
                case 7:
                    MesExtenso = "Julho";
                    break;
                case 8:
                    MesExtenso = "Agosto";
                    break;
                case 9:
                    MesExtenso = "Setembro";
                    break;
                case 10:
                    MesExtenso = "Outubro";
                    break;
                case 11:
                    MesExtenso = "Novembro";
                    break;
                case 12:
                    MesExtenso = "Dezembro";
                    break;
            }

            return MesExtenso;
        }


        // O método toExtenso recebe um _valor do tipo decimal
        public string ValorMonetarioPorExtenso(decimal valor)
        {
            if (valor < 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string valor_por_extenso = string.Empty;

                if (valor == 0)
                {
                    valor_por_extenso = "Zero";
                }
                else
                {
                    string strValor = valor.ToString("000000000000000.00");//"000000000000000.00"
                    

                    for (int i = 0; i <= 15; i += 3)
                    {
                        valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                        if (i == 0 & valor_por_extenso != string.Empty)
                        {
                            if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                                valor_por_extenso += " Trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                            else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                                valor_por_extenso += " Trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        }
                        else if (i == 3 & valor_por_extenso != string.Empty)
                        {
                            if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                                valor_por_extenso += " Bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                            else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                                valor_por_extenso += " Bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        }
                        else if (i == 6 & valor_por_extenso != string.Empty)
                        {
                            if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                                valor_por_extenso += " Milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                            else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                                valor_por_extenso += " Milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        }
                        else if (i == 9 & valor_por_extenso != string.Empty)
                            if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            {
                                valor_por_extenso = valor_por_extenso == "Um" ? string.Empty : valor_por_extenso;
                                valor_por_extenso += " Mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);
                            }


                        if (i == 12)
                        {
                            if (valor_por_extenso.Length > 8)
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Bilhão" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Milhão")
                                    valor_por_extenso += " de";
                                else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Bilhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Milhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "Trilhões")
                                    valor_por_extenso += " de";
                                else
                                        if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "Trilhões")
                                    valor_por_extenso += " de";

                            if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                                valor_por_extenso += " Kwanza";
                            else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                                valor_por_extenso += " Kwanzas";

                            if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                                valor_por_extenso += " E ";
                        }

                        if (i == 15)
                            if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                                valor_por_extenso += " Cêntimo";
                            else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                                valor_por_extenso += " Cêntimos";
                    }
                }

                return valor_por_extenso;
            }
        }

        private string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;

            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "Cem " : "Cento e ";
                else if (a == 2) montagem += "Duzentos ";
                else if (a == 3) montagem += "Trezentos ";
                else if (a == 4) montagem += "Quatrocentos ";
                else if (a == 5) montagem += "Quinhentos ";
                else if (a == 6) montagem += "Seiscentos ";
                else if (a == 7) montagem += "Setecentos ";
                else if (a == 8) montagem += "Oitocentos ";
                else if (a == 9) montagem += "Novecentos ";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? "" : string.Empty) + "Dez";
                    else if (c == 1) montagem += ((a > 0) ? "" : string.Empty) + "Onze";
                    else if (c == 2) montagem += ((a > 0) ? "" : string.Empty) + "Doze";
                    else if (c == 3) montagem += ((a > 0) ? "" : string.Empty) + "Treze";
                    else if (c == 4) montagem += ((a > 0) ? "" : string.Empty) + "Catorze";
                    else if (c == 5) montagem += ((a > 0) ? "" : string.Empty) + "Quinze";
                    else if (c == 6) montagem += ((a > 0) ? "" : string.Empty) + "Dezasseis";
                    else if (c == 7) montagem += ((a > 0) ? "" : string.Empty) + "DEzassete";
                    else if (c == 8) montagem += ((a > 0) ? "" : string.Empty) + "Dezoito";
                    else if (c == 9) montagem += ((a > 0) ? "" : string.Empty) + "Dezanove";
                }
                else if (b == 2) montagem += ((a > 0) ? "" : string.Empty) + "Vinte";
                else if (b == 3) montagem += ((a > 0) ? "" : string.Empty) + "Trinta";
                else if (b == 4) montagem += ((a > 0) ? "" : string.Empty) + "Quarenta";
                else if (b == 5) montagem += ((a > 0) ? "" : string.Empty) + "Cinquenta";
                else if (b == 6) montagem += ((a > 0) ? "" : string.Empty) + "Sessenta";
                else if (b == 7) montagem += ((a > 0) ? "" : string.Empty) + "Setenta";
                else if (b == 8) montagem += ((a > 0) ? "" : string.Empty) + "Oitenta";
                else if (b == 9) montagem += ((a > 0) ? "" : string.Empty) + "Noventa";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "Um";
                    else if (c == 2) montagem += "Dois";
                    else if (c == 3) montagem += "Três";
                    else if (c == 4) montagem += "Quatro";
                    else if (c == 5) montagem += "Cinco";
                    else if (c == 6) montagem += "Seis";
                    else if (c == 7) montagem += "Sete";
                    else if (c == 8) montagem += "Oito";
                    else if (c == 9) montagem += "Nove";

                return montagem.ToLower();
            }
        } 

        

        public string GetDueDateFromPaymentTerms(CondicaoPagamentoDTO pPaymentTerms, DateTime DocumentDate)
        {
            string DueDate = "";
            DateTime Date;

            if (pPaymentTerms.Pagamento == "PR") // Pagamento Parcelado...
            {
                Date = DocumentDate;
                int Prestacoes = 1;
                if (pPaymentTerms.EntradaInicial > 0)
                {
                    DueDate = Date.ToString("dd/MM/yyyy");
                    Prestacoes = (int)pPaymentTerms.NroPrestacoes;
                    Prestacoes--;
                }


                for (int i = 1; i <= Prestacoes; i++)
                {
                    //Date = Date.AddDays((double)pPaymentTerms.NroPrestacoes);
                    Date = Date.AddDays((double)pPaymentTerms.Periodicidade); 
                    DueDate += "_" + Date.ToString("dd/MM/yyyy");
                }

            }
            else
            {
                if (pPaymentTerms.Pagamento == "AP") //Após x Dias
                {
                    Date = DocumentDate.AddDays(int.Parse(pPaymentTerms.Sigla));
                    DueDate = Date.ToString();
                }
                else if (pPaymentTerms.Pagamento == "FM") // Pagamento no Final do Mês Corrente
                {
                    Date = new DateTime(DocumentDate.Year, DocumentDate.Month, 1).AddMonths(1).AddDays(-1);
                    DueDate = Date.ToString();
                }
                else if (pPaymentTerms.Pagamento == "PD") // Pagamento no Próximo Dia...
                {
                    int Dia = DocumentDate.Day;

                    if (Dia >= int.Parse(pPaymentTerms.Sigla))
                    {
                        Date = new DateTime(DocumentDate.Year, int.Parse(DocumentDate.AddMonths(1).ToString()), int.Parse(pPaymentTerms.Sigla));
                    }
                    else
                    {
                        Date = new DateTime(DocumentDate.Year, DocumentDate.Month, int.Parse(pPaymentTerms.Sigla));
                    }
                    DueDate = Date.ToString();
                }
                else if (pPaymentTerms.Pagamento == "FM") // A Pagar no próximo mês Dia...
                {
                    Date = new DateTime(DocumentDate.Year, int.Parse(DocumentDate.AddMonths(1).ToString()), int.Parse(pPaymentTerms.Sigla));
                    DueDate = Date.ToString();
                }  
            } 

            return DueDate == string.Empty ? DateTime.Today.ToString() : DueDate;
        }

        public int ReturnStockMove(string pAction)
        {
            if (pAction == "E" || pAction=="AP")
            {
                return 1;
            }
            else if (pAction == "S" || pAction=="AN")
            {
                return 2;
            }
            else if (pAction == "T")
            {
                return 3;
            }
            else
            {
                return 0;
            }
        } 
         


        public List<DocumentoComercialDTO> ListaDocumentosLiquidacaoClientes()
        {
            List<DocumentoComercialDTO> lista = ListaGeralDocumentosForDropDown().Where(t => t.Codigo == -1 || t.Tipo == "L" && (t.Categoria == "C" || t.Formato== "RECEIPT_P")).ToList();
            return lista;
        }

        public List<DocumentoComercialDTO> ListaDocumentosLiquidacaoFornecedores()
        {
            List<DocumentoComercialDTO> lista = ListaGeralDocumentosForDropDown().Where(t =>t.Codigo == -1 || t.Formato == "SUPPLIER_P" || t.Formato == "SUPPLIER_A" || t.Formato == "SUPPLIER_R").ToList();
            return lista;
        }

        public List<MetodoPagamentoDTO> ListaPaymentMethods()
        {
            List<MetodoPagamentoDTO> lista = MetodoPagamentoRN.GetInstance().ListaMetodoPagamento(string.Empty);
            lista.Insert(0, new MetodoPagamentoDTO { Codigo = -1, Descricao="-SELECCIONE-", DescricaoPagamento="-SELECCIONE-" });

            return lista;
        }

        public List<ContaBancariaDTO> GetBankAccounts(string pType, string pFilial, int pMoeda)
        {
            List<ContaBancariaDTO> lista = ContaBancariaRN.GetInstance().ListaForPayments(pType, pFilial, pMoeda); 
            return lista;
        }

        public List<EntidadeDTO> ListaVendedores(string pFilial)
        {
            return ListaFuncionarios(pFilial);
        }

        public string cambio(int pCurrency)
        {
            return FormatarPreco(CambioRN.GetInstance().ObterCambioActualizado(new CambioDTO(pCurrency.ToString())).CambioCompra);
        }
        
        public List<StatusDTO> AllWorkOrderStatusList()
        {
            var lista = new List<StatusDTO>();

            lista.Add(new StatusDTO(-1, "-SELECCIONE-", ""));
            lista.Add(new StatusDTO(1, "RASCUNHO", "")); 
            lista.Add(new StatusDTO(2, "AGUARDANDO APROVAÇÃO DO CLIENTE", "")); 
            lista.Add(new StatusDTO(3, "EM ANDAMENTO", ""));
            lista.Add(new StatusDTO(4, "ORÇAMENTO APROVADO", ""));
            lista.Add(new StatusDTO(5, "ENCERRADA", ""));
            lista.Add(new StatusDTO(6, "CANCELADA", ""));
            lista.Add(new StatusDTO(7, "AGENDADA", ""));
            lista.Add(new StatusDTO(8, "FECHADA", ""));
            lista.Add(new StatusDTO(9, "AGUARDANDO INÍCIO DE INTERVENÇÃO", "")); 



            return lista.OrderBy(t=>t.Codigo).ToList();
        }

         

        public List<EntidadeDTO> ListaFuncionarios(string pFilial)
        {
            var technician = new TechnicianDTO
            {
                Entity = new EntidadeDTO
                {
                    NomeCompleto = string.Empty,
                    Filial = pFilial
                }
            };

            var lista = new List<EntidadeDTO>();

            foreach(var tech in TechnicianRN.GetInstance().ObterPorFiltro(technician))
            {
                lista.Add(tech.Entity);
                
            }
            lista.Insert(0, new EntidadeDTO(-1, "-SELECCIONE-"));

            return lista;
        }

        public List<CategoriaDTO> ListaCategoriasVeiculos()
        {
            var lista = new CategoriaVeiculoDAO().ObterPorFiltro(new CategoriaDTO(0, ""));

            lista.Insert(0, new CategoriaDTO(-1, "-SELECCIONE-")); 
            return lista;
        }

        public List<StatusDTO> WorkOrderResolutionStatus()
        {
            var lista = new List<StatusDTO>();

            lista.Add(new StatusDTO(1, "PENDENTE"));
            lista.Add(new StatusDTO(2, "EM EXECUÇÃO"));
            lista.Add(new StatusDTO(3, "FINALIZADO"));

            return lista;
        }

        public List<PeriodoDTO> ListaPeriodicidade()
        {
            var lista = new List<PeriodoDTO>();
            lista.Add(new PeriodoDTO(-1, "SELECCIONE", "-1"));
            lista.Add(new PeriodoDTO(1, "SEMANAL", "7"));
            lista.Add(new PeriodoDTO(2, "QUINZENAL", "15"));
            lista.Add(new PeriodoDTO(3, "MENSAL", "30"));
            lista.Add(new PeriodoDTO(4, "BIMESTRAL", "60"));
            lista.Add(new PeriodoDTO(5, "TRIMESTRE", "90"));
            lista.Add(new PeriodoDTO(6, "QUADRIMESTRE", "120"));
            lista.Add(new PeriodoDTO(7, "SEMESTRE", "180"));
            lista.Add(new PeriodoDTO(8, "ANUAL", "360"));
            lista.Add(new PeriodoDTO(9, "OUTRO", "0"));
            return lista;
        }

        public bool InEditMode(FaturaDTO dto)
        {
            bool IsDraft = ((dto.StatusDocumento >= 1 && dto.StatusDocumento <= 4) || dto.StatusDocumento == 9) ? true : false;
            if(IsDraft || (dto.LookupField2!= "2ª VIA" && dto.ValorPago<=0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        public List<DocumentoComercialDTO> ListaDocFaturacaoPOS()
        {
            return ListaGeralDocumentosForDropDown().Where(t =>t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_BUDGET || t.Codigo == -1).ToList();
        }

        public bool IsPaid(FaturaDTO dto)
        {
            var document = GetAllDocumentsList().Where(t => t.Codigo == dto.Documento).First();

            if((document.Formato == CUSTOMER_INVOICE || document.Formato == CUSTOMER_ORDER) && dto.ValorPago < (dto.ValorTotal - dto.ValorRetencao))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string PaymetStatusDescription(FaturaDTO dto)
        { 
            if((dto.ValorPago > 0) && (dto.ValorPago < dto.ValorTotal))
            {
                int percentagemPaga = (int)Math.Round((dto.ValorPago * 100) / dto.ValorTotal);
                if(dto.Validade < DateTime.Today)
                {
                    percentagemPaga = 100 - percentagemPaga;
                    return "<span class='label label-danger'> " + percentagemPaga + "% DA LIQUIDAÇÃO ESTÁ EM ATRASO</span> - ";
                }
                else
                {
                    return "<span class='label label-warning'> PARCIALMENTE LIQUIDADO(A) " + percentagemPaga + "%</span> -";
                }
                
            }
            else
            {
                return "<span class='label label-danger'>NÃO LIQUIDADO(A)</span>";
            }
        }

        public string RedictSessionEnd(string PriorUrl)
        {
            return "~/Seguranca/SessaoExpirada?pReturnUrl=" + PriorUrl;
        }


        public string ShowForm(AcessoDTO pSessionInfo, int pForm)
        {
            if (!AcessoRN.GetInstance().isMasterAdmin(pSessionInfo.Utilizador))
            {
                var form = pSessionInfo.UserAccess.Find(t => t.Formulario.Codigo == pForm);
                if (form == null || form.AllowAccess == 0)
                {
                    return "class='hidden'";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public string CanAccessModulo(AcessoDTO pSessionInfo, int pModuloID)
        {
            if (!AcessoRN.GetInstance().isMasterAdmin(pSessionInfo.Utilizador))
            {
                var modulo = pSessionInfo.UserAccess.Find(t => t.Formulario.Modulo.Codigo == pModuloID);
                if (modulo == null || modulo.AllowAccess == 0)
                {
                    return "class='hidden'";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public bool IsDocumentoAutoLiquidacao(DocumentoComercialDTO oDocument)
        {
            return oDocument != null && GetDocumentType(oDocument).Equals(DocumentType.InvoiceReceipt) ? true : false;
        }

        public bool IsInvoice(DocumentoComercialDTO oDocument)
        {
            return oDocument != null && GetDocumentType(oDocument).Equals(DocumentType.Invoice) ? true : false;
        }

        public bool IsCreditNote(DocumentoComercialDTO oDocument)
        {
            return oDocument != null && GetDocumentType(oDocument).Equals(DocumentType.CreditNote) ? true : false;
        }

        public bool IsCashDevolution(DocumentoComercialDTO oDocument)
        {
            return oDocument != null && GetDocumentType(oDocument).Equals(DocumentType.DevolutionInvoiceReceipt) ? true : false;
        }

        public bool IsDocumentoAdiantamento(DocumentoComercialDTO oDocument)
        {
            return oDocument!=null && (GetDocumentType(oDocument).Equals(DocumentType.IsAdvanceInvoice) || GetDocumentType(oDocument).Equals(DocumentType.IsAdvanceBalance)) ? true : false;
        }
         


        public DocumentType GetDocumentType(DocumentoComercialDTO oDocument)
        {
            if (oDocument.Formato == CUSTOMER_INVOICE_RECEIPT)
            {
                return DocumentType.InvoiceReceipt;
            }
            else if (oDocument.Formato == CUSTOMER_INVOICE)
            {
                return DocumentType.Invoice;
            }
            else if (oDocument.Formato == CUSTOMER_ORDER)
            {
                return DocumentType.CustomerOrder;
            }
            else if (oDocument.Formato == CUSTOMER_CREDIT_NOTE || oDocument.Formato == CUSTOMER_INVOICE_CUSTOMER_PAYMENT)
            {
                if (oDocument.Formato == CUSTOMER_INVOICE_CUSTOMER_PAYMENT)
                    return DocumentType.DevolutionInvoiceReceipt;
                else
                    return DocumentType.CreditNote;
            }
            else if (oDocument.Formato == CUSTOMER_RECEIPT)
            {
                return DocumentType.CustomerReceipt;

            }
            else if (oDocument.ContaCorrente == "RECEIPT_P")
            {
                return DocumentType.CustomerPayment;
            }
            else if (oDocument.Formato == CUSTOMER_INVOICE_ADVANCED) 
            {
                return DocumentType.IsAdvanceInvoice;
            }
            else if (oDocument.Formato == "RECEIPT_A")
            {
                return DocumentType.IsAdvanceBalance;
            }
            else
                return DocumentType.Budget;
        }

        public bool MovimentaStock(string args)
        {
            return args == "True" ? true : false;
        }


        public bool IsCashRegister(int pProfileID)
        {
            if (pProfileID != AcessoDTO.CashRegisterProfile)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<DocumentoComercialDTO> GetOriginalDocumentConversion(DocumentoComercialDTO dto)
        {
            var ConversibleDocumentsList = DocumentoComercialRN.GetInstance().ListaDocumentos(string.Empty).Where(t => t.Status == 1).ToList();
            dto = ConversibleDocumentsList.Where(t => t.Codigo == dto.Codigo).SingleOrDefault();
            if (dto.Formato == CUSTOMER_INVOICE)
            {
                ConversibleDocumentsList = ConversibleDocumentsList
                .Where(t => (t.Formato == CUSTOMER_CREDIT_NOTE && t.Categoria == "C") || (t.Formato == CUSTOMER_RECEIPT && t.Categoria == "C") || (t.Formato == "TRANSP_V" && t.Categoria == dto.Tipo) || t.Formato == "SUPPLIER_E").ToList();

            }
            else if (dto.Formato == CUSTOMER_BUDGET)
            {
                ConversibleDocumentsList = ConversibleDocumentsList
                .Where(t => t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_INVOICE_RECEIPT || t.Formato == CUSTOMER_ORDER || t.Formato == "SUPPLIER_E" || (t.Formato == "TRANSP_V" && t.Categoria == dto.Categoria)).ToList();
            }
            else if (dto.Formato == CUSTOMER_INVOICE_RECEIPT)
            {
                ConversibleDocumentsList = ConversibleDocumentsList
                .Where(t => t.Formato == CUSTOMER_CREDIT_NOTE || t.Formato == CUSTOMER_INVOICE_CUSTOMER_PAYMENT || t.Formato == "RECEIPT_P" || (t.Formato == "TRANSP_V" && t.Categoria == dto.Tipo) || t.Formato == "SUPPLIER_E").ToList();
            }
            else if (dto.Formato == CUSTOMER_ORDER)
            {
                ConversibleDocumentsList = ConversibleDocumentsList
               .Where(t => t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_INVOICE_RECEIPT || (t.Formato == "TRANSP_C" && t.Categoria == dto.Tipo) || t.Formato == CUSTOMER_BUDGET || t.Formato == "SUPPLIER_E").ToList();
            }
            else if (dto.Formato == CUSTOMER_CREDIT_NOTE)
            {
                ConversibleDocumentsList = ConversibleDocumentsList
               .Where(t => t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_INVOICE_RECEIPT || (t.Formato == "TRANSP_V" && t.Categoria == dto.Tipo) || t.Formato == CUSTOMER_BUDGET || t.Formato == "SUPPLIER_E").ToList();
            }
            else if (dto.Formato == "TRANSP_C" && (dto.Categoria == "F" || dto.Categoria == "C"))
            {
                var DocumentList = ConversibleDocumentsList
                .Where(t => t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_INVOICE_RECEIPT).ToList();
                if (dto.Categoria == "F")
                {
                    foreach (var document in ConversibleDocumentsList.Where(t => t.Formato == "SUPPLIER_I" || dto.Formato == "SUPPLIER_V"))
                    {
                        DocumentList.Add(document);
                    }
                }

                ConversibleDocumentsList = DocumentList;

            }
            else if (dto.Formato == "SUPPLIER_I" || dto.Formato == "SUPPLIER_V")
            {
                ConversibleDocumentsList = ConversibleDocumentsList
               .Where(t => t.Formato == CUSTOMER_INVOICE || t.Formato == "SUPPLIER_A").ToList();

            }
            else if (dto.Formato == "SUPPLIER_O")
            {
                ConversibleDocumentsList = ConversibleDocumentsList
                .Where(t => t.Formato == CUSTOMER_BUDGET || t.Formato == CUSTOMER_ORDER || t.Formato == "SUPPLIER_E" ||
                t.Formato == "SUPPLIER_I" ||(t.Formato == "SUPPLIER_O" && t.Sigla!=t.Sigla)).ToList();
            }
            else if (dto.Formato == "SUPPLIER_E")
            {
                ConversibleDocumentsList = ConversibleDocumentsList
                .Where(t => t.Formato == CUSTOMER_BUDGET || t.Formato == CUSTOMER_ORDER || t.Formato == CUSTOMER_INVOICE || t.Formato == CUSTOMER_INVOICE_RECEIPT || 
                t.Formato=="SUPPLIER_I" || t.Formato== "SUPPLIER_V").ToList();
            }else
            {
                ConversibleDocumentsList = new List<DocumentoComercialDTO>();
            }

            return ConversibleDocumentsList;
        }

        public List<DocumentoComercialDTO> ListaDocumentosFornecedor()
        {
            return ListaGeralDocumentosForDropDown().Where(t=>t.Codigo <=0 || t.Formato== "TRANSP_C" || t.Formato.Contains("SUPPLIER_")).ToList();
        }

        public List<DocumentoComercialDTO> ListaDocumentosCompra()
        {
            return ListaDocumentosFornecedor().Where(t => t.Codigo <=0 || t.Formato == "SUPPLIER_V" || t.Formato == "SUPPLIER_I").ToList();
        }

        public List<DocumentoComercialDTO> ListaDocumentosEncomendasFornecedor()
        {
            return ListaDocumentosFornecedor().Where(t => t.Formato == "SUPPLIER_E").ToList();
        }

        public List<DocumentoComercialDTO> ListaDocumentosCotacoesOrcamentosFornecedor()
        {
            return ListaDocumentosFornecedor().Where(t => t.Formato == "SUPPLIER_O").ToList();
        }

        public List<DocumentoComercialDTO> ListaDocumentosTransporteFornecedor()
        {
            return ListaDocumentosFornecedor().Where(t => t.Formato == "TRANSP_C").ToList();
        }

        public List<DocumentoComercialDTO> ListaDocumentosRectificativosFornecedor()
        {
            return ListaDocumentosFornecedor().Where(t => t.Formato == "SUPPLIER_C").ToList();
        }

        public List<CaixaVelocidadeDTO> CaixaVelocidadesList()
        {
            List<CaixaVelocidadeDTO> lista = new List<CaixaVelocidadeDTO>
            {
                new CaixaVelocidadeDTO(-1, "-SELECCIONE-"),
                new CaixaVelocidadeDTO(1, "MANUAL"),
                new CaixaVelocidadeDTO(2, "AUTOMÁTICA") 
            };

            return lista;
        }

        public List<CombustivelDTO> CombustivelList()
        {
            List<CombustivelDTO> lista = new List<CombustivelDTO>
            {
                new CombustivelDTO(-1, "-SELECCIONE-"),
                new CombustivelDTO(1, "ÁLCOOL"),
                new CombustivelDTO(2, "GASOLEO"),
                new CombustivelDTO(3, "GASOLINA")

            };

            return lista;
        }

        

        public List<ClasseVeiculoDTO> ClasseVeiculoList()
        {
            List<ClasseVeiculoDTO> lista = new List<ClasseVeiculoDTO>
            {
                new ClasseVeiculoDTO(-1, "-SELECCIONE-"),
                new ClasseVeiculoDTO(1, "MOTOCICLO"),
                new ClasseVeiculoDTO(2, "LIGEIRO"),
                new ClasseVeiculoDTO(3, "PESADO")

            };

            return lista;
        }

         
        public bool IsCashRegister(AcessoDTO pSessionInfo)
        {
            if (pSessionInfo.UserProfile != AcessoDTO.CashRegisterProfile)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsGerente(AcessoDTO pSessionInfo)
        {
            if (pSessionInfo != null)
            {  

                return AcessoRN.GetInstance().HasGerenteProfile(pSessionInfo.Utilizador, pSessionInfo.UserProfile) ? true : false;
            }
            else
            {
                return false;
            }
        }

        public List<TipoDTO> EntityTypeList()
        {
            List<TipoDTO> lista = new List<TipoDTO>();
            lista.Add(new TipoDTO(-1, "-SELECCIONE-", "-1"));
            lista.Add(new TipoDTO(1, "CLIENTE", "C"));
            lista.Add(new TipoDTO(2, "FORNECEDOR", "F"));
            lista.Add(new TipoDTO(3, "CLIENTE E FORNECEDOR", "A"));
            lista.Add(new TipoDTO(4, "OUTROS CREDORES", "K"));
            lista.Add(new TipoDTO(5, "OUTROS DEVEDORES", "D"));
            lista.Add(new TipoDTO(6, "ENTIDADE PÚBLICA", "E"));

            return lista;
        }

        public List<AnoFaturacaoDTO> FiscalYearsList(int pFilial)
        {
            var lista = PeriodoFaturacaoRN.GetInstance().GetForDropDowList(pFilial);
            if(lista.Count <= 0)
            {
                lista.Insert(0, new AnoFaturacaoDTO() { Ano = -1 });
            }

            return lista;
        } 
         

        public DateTime LastDayOfMounth(int pMounth, int pYear)
        {
            return new DateTime(pYear, pMounth, 1).AddMonths(1).AddDays(-1);
        }

        public DocumentType GetDocumentTypeSupplier(DocumentoComercialDTO oDocument)
        {
            if (oDocument.Formato == "SUPPLIER_R")
            {
                return DocumentType.InvoiceReceipt;
            }
            else if (oDocument.Formato == "SUPPLIER_I")
            {
                return DocumentType.Invoice;
            }
            else if (oDocument.Formato == "SUPPLIER_E")
            {
                return DocumentType.CustomerOrder;
            }
            else if (oDocument.Formato == "SUPPLIER_D" || oDocument.Formato == "SUPPLIER_P")
            {
                if (oDocument.Formato == "SUPPLIER_P")
                    return DocumentType.DevolutionInvoiceReceipt;
                else
                    return DocumentType.CreditNote;
            }
            else if (oDocument.Formato == CUSTOMER_RECEIPT)
            {
                return DocumentType.CustomerReceipt;

            }
            else if (oDocument.ContaCorrente == "RECEIPT_P")
            {
                return DocumentType.CustomerPayment;
            }
            else
                return DocumentType.Budget;
        }

         

        public DateTime FirstDayOfWeek(DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public void EnableISS()
        {
            string command = "START /WAIT DISM /Online /Enable-Feature /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-ASP /FeatureName:IIS-ASPNET /FeatureName:IIS-BasicAuthentication /FeatureName:IIS-CGI "+
                "/FeatureName:IIS-ClientCertificateMappingAuthentication /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-CustomLogging /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DigestAuthentication /FeatureName:IIS-DirectoryBrowsing "+
                "/FeatureName:IIS-FTPExtensibility /FeatureName:IIS-FTPServer /FeatureName:IIS-FTPSvc /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HostableWebCore /FeatureName:IIS-HttpCompressionDynamic /FeatureName:IIS-HttpCompressionStatic"+
                "/FeatureName:IIS-HttpErrors /FeatureName:IIS-HttpLogging /FeatureName:IIS-HttpRedirect /FeatureName:IIS-HttpTracing /FeatureName:IIS-IIS6ManagementCompatibility /FeatureName:IIS-IISCertificateMappingAuthentication"+
                "/FeatureName:IIS-IPSecurity /FeatureName:IIS-ISAPIExtensions /FeatureName:IIS-ISAPIFilter /FeatureName:IIS-LegacyScripts /FeatureName:IIS-LegacySnapIn /FeatureName:IIS-LoggingLibraries /FeatureName:IIS-ManagementConsole"+
                "/FeatureName:IIS-ManagementScriptingTools /FeatureName:IIS-ManagementService /FeatureName:IIS-Metabase /FeatureName:IIS-NetFxExtensibility /FeatureName:IIS-ODBCLogging /FeatureName:IIS-Performance /FeatureName:IIS-RequestFiltering"+
                "/FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-ServerSideIncludes /FeatureName:IIS-StaticContent /FeatureName:IIS-URLAuthorization /FeatureName:IIS-WebDAV /FeatureName:IIS-WebServer"+
                "/FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-WebServerRole /FeatureName:IIS-WindowsAuthentication /FeatureName:IIS-WMICompatibility /FeatureName:WAS-ConfigurationAPI /FeatureName:WAS-NetFxEnvironment"+
                "/FeatureName:WAS-ProcessModel /FeatureName:WAS-WindowsActivationService";
             
            ProcessStartInfo pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            Process p = new Process();
            p.StartInfo = pStartInfo;
            p.Start();
        }

        private static string _iisRegKey = @"Software\Microsoft\InetStp";
        public string notSelectedItem = "-1";

        
        /*
        public bool CheckIfServiceIsRunning(string serviceName)
        {
            ServiceProcess.ServiceController mySC = new ServiceProcess.ServiceController(serviceName);
            if (mySC.Status == ServiceProcess.ServiceControllerStatus.Running)
            {
                // Service already running
                return true;
            }
            else
            {
                return false;
            }
        }*/


        public decimal SetQuantidadeConvertida(decimal pPrecoMilheiro, string pFactor, decimal pValorConversao)
        {
            if(pFactor =="/" || pFactor == "D")
            {
                return pPrecoMilheiro / pValorConversao;

            }else if (pFactor == "+" || pFactor == "A")
            {
                return pPrecoMilheiro + pValorConversao;

            }else if (pFactor == "-" || pFactor == "S")
            {
                return pPrecoMilheiro - pValorConversao;

            }else  if (pFactor == "x" || pFactor == "M")
            {
                return pPrecoMilheiro * pValorConversao;
            }
            else
            {
                return 0;
            }
        } 

        public List<LaboratorioExameFaixaEtariaDTO> GetGeneroList()
        {
            var list = new List<LaboratorioExameFaixaEtariaDTO>();

            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = -1, Descricao = "Seleccione", IdadeInicial = -1, IdadeFinal = -1, UnidadeFaixa = "N", Sigla = "-1" , Sexo = "T" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 1, Descricao = "Recém Nascido", IdadeInicial = 0, IdadeFinal = 12, UnidadeFaixa = "M", Sigla = "RN" , Sexo = "T" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 2, Descricao = "Criança Masculino", IdadeInicial = 1, IdadeFinal = 12, UnidadeFaixa = "A", Sigla = "CM" , Sexo = "T" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 3, Descricao = "Criança Feminino", IdadeInicial = 1, IdadeFinal = 12, UnidadeFaixa = "A", Sigla = "CF", Sexo = "T" }); 
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 4, Descricao = "Adolescente Masculino", IdadeInicial = 13, IdadeFinal = 17, UnidadeFaixa = "A", Sigla = "AM" , Sexo = "M" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 5, Descricao = "Adolescente Feminino", IdadeInicial = 13, IdadeFinal = 17, UnidadeFaixa = "A", Sigla = "AF" , Sexo = "F" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 6, Descricao = "Jovem Masculino", IdadeInicial = 18, IdadeFinal = 35, UnidadeFaixa = "N", Sigla = "JM" , Sexo = "M" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 7, Descricao = "Jovem Feminino", IdadeInicial = 18, IdadeFinal = 35, UnidadeFaixa = "N", Sigla = "JM" , Sexo = "F" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 8, Descricao = "Jovem/Adulto Masculino", IdadeInicial = 36, IdadeFinal = 50, UnidadeFaixa = "N", Sigla = "HM" , Sexo = "M" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 9, Descricao = "Jovem/Adulto(a) Feminino", IdadeInicial = 36, IdadeFinal = 50, UnidadeFaixa = "N", Sigla = "ML" , Sexo = "F" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 10, Descricao = "Jovem/Adulto Masculino", IdadeInicial = 51, IdadeFinal = 64, UnidadeFaixa = "N", Sigla = "HM", Sexo = "M" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 11, Descricao = "Jovem/Adulto(a) Feminino", IdadeInicial = 51, IdadeFinal = 64, UnidadeFaixa = "N", Sigla = "ML", Sexo = "F" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 12, Descricao = "Idoso Masculino", IdadeInicial = 65, IdadeFinal = 0, UnidadeFaixa = "N", Sigla = "IM" , Sexo = "M" });
            list.Add(new LaboratorioExameFaixaEtariaDTO { Codigo = 13, Descricao = "Idoso(a) Feminino", IdadeInicial = 65, IdadeFinal = 0, UnidadeFaixa = "N", Sigla = "IF" , Sexo = "F" });

            return list;
        }  

        public string getHtmlContaCorrente(FaturaDTO dto)
        {
            decimal saldoInicial = 0, debitoInicial = 0, creditoInicial = 0, totalDebito = 0, totalCredito = 0, saldoPerido = 0, saldoFinal=0;

            DateTime InitialDate = dto.EmissaoIni;
            dto.EmissaoIni = DateTime.MinValue;
            var ExtractList = FaturaClienteRN.GetInstance().GetCustomerExtractList(dto);


            string messageBody ="<span style='font-size:12px; font-family:Calibri'><b>Extracto de Conta Corrente do Cliente Desde:"+ExtractList.Min(t=>t.Emissao).ToString("dd/MM/yyyy")+" à "+
                ExtractList.Max(t=>t.Emissao).ToString("dd/MM/yyyy")+" </b><br/>"+

            ExtractList[0].Entidade + " - " + ExtractList[0].NomeEntidade+ "</span><br/><br/>";

            string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
            string htmlTableEnd = "</table>";
            string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
            string htmlHeaderRowEnd = "</tr>";
            string htmlTrStart = "<tr style=\"color:#555555;\">";
            string htmlTrEnd = "</tr>";
            string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
            string htmlTdEnd = "</td>";

            messageBody += htmlTableStart;
            messageBody += htmlHeaderRowStart;
            messageBody += htmlTdStart + "Data Doc." + htmlTdEnd;
            messageBody += htmlTdStart + "Documento" + htmlTdEnd;
            messageBody += htmlTdStart + "Moeda" + htmlTdEnd;
            messageBody += htmlTdStart + "Câmbio" + htmlTdEnd;
            messageBody += htmlTdStart + "Débito" + htmlTdEnd;
            messageBody += htmlTdStart + "Crédito" + htmlTdEnd;
            messageBody += htmlTdStart + "Saldo" + htmlTdEnd;
            messageBody += htmlHeaderRowEnd;

            
            

            if (InitialDate > DateTime.MinValue)
            {
                var PriorMovimentExtractList = ExtractList.Where(t => t.Emissao < InitialDate).ToList();
                saldoInicial = PriorMovimentExtractList.Sum(t => t.ValorTotal - t.ValorPago);
                creditoInicial = PriorMovimentExtractList.Sum(t => t.ValorPago);
                debitoInicial = PriorMovimentExtractList.Sum(t => t.ValorTotal);
            }
            else
            {
                InitialDate = ExtractList.Min(t => t.Emissao);
            }

            ExtractList = ExtractList.Where(t => t.Emissao >= InitialDate).ToList();

            messageBody += "<tr><td colspan=\"4\" style=\"text-align:right; \"> <b>Saldo Inicial</b></td>";
            messageBody += "<td style=\"text-align:right; \"><b>"+FormatarPreco(debitoInicial)+"</b></td>";
            messageBody += "<td style=\"text-align:right; \"><b>" + FormatarPreco(creditoInicial) + "</b></td>";
            messageBody += "<td style=\"text-align:right; \"><b>" + FormatarPreco(saldoInicial) + "</b></td></tr>";
            foreach (var documento in ExtractList)
            {
                messageBody += htmlTrStart;
                messageBody += htmlTdStart + documento.Emissao.ToString("dd/MM/yyyy") + htmlTdEnd;
                messageBody += htmlTdStart + documento.DescricaoDocumento+" "+documento.Referencia + htmlTdEnd;
                messageBody += htmlTdStart + documento.LookupField2 + htmlTdEnd;
                messageBody += htmlTdStart + FormatarPreco(documento.Cambio) + htmlTdEnd;
                if(IsDocumentoAutoLiquidacao(new DocumentoComercialDTO { Formato = documento.TituloDocumento }))
                {
                    messageBody += htmlTdStart + FormatarPreco(documento.ValorPago) + htmlTdEnd;
                    messageBody += htmlTdStart + FormatarPreco(documento.ValorTotal) + htmlTdEnd;
                }
                else
                {
                    if(documento.ValorPago > 0)
                        messageBody += htmlTdStart + FormatarPreco(documento.ValorPago) + htmlTdEnd;
                    else
                        messageBody += htmlTdStart + FormatarPreco(documento.ValorTotal) + htmlTdEnd;
                }
                messageBody += htmlTdStart + FormatarPreco(documento.LookupNumericField1) + htmlTdEnd;
                messageBody += htmlTrEnd;
            }
            messageBody += htmlTableEnd;
            totalCredito = ExtractList.Sum(t => t.ValorPago);
            totalDebito = ExtractList.Sum(t => t.ValorTotal);

            int p = ExtractList.Count - 1;
            saldoFinal = ExtractList.Count > 0 ? ExtractList[p].LookupNumericField1 : 0;

            messageBody += "<tr><td colspan=\"4\" style=\"text-align:right; \">Saldo do Periodo</td>";
            messageBody += "<td style=\"text-align:right; \">" + FormatarPreco(totalDebito) + "</b></td>";
            messageBody += "<td style=\"text-align:right; \">" + FormatarPreco(totalCredito) + "</td>";
            messageBody += "<td style=\"text-align:right; \">" + FormatarPreco(saldoFinal) + "</td></tr>";

            messageBody += "<tr><td colspan=\"4\" style=\"text-align:right; \">Saldo Final</td>";
            messageBody += "<td style=\"text-align:right; \">" + FormatarPreco(totalDebito + debitoInicial) + "</b></td>";
            messageBody += "<td style=\"text-align:right; \">" + FormatarPreco(totalCredito + creditoInicial ) + "</td>";
            messageBody += "<td style=\"text-align:right; \">" + FormatarPreco(saldoFinal + saldoInicial) + "</td></tr>"; 

            return messageBody; 

        }

        public List<DocumentoComercialDTO> GetTresurePeddingDocumentsList(string pAccountType)
        {
            return ListaDocumentosTesouraria();
        }

        public List<EscalaDTO> EscalaClinica(AcessoDTO dto)
        {
            var lista = new List<EscalaDTO>();

            lista.Insert(0, new EscalaDTO { Codigo = -1, Data = DateTime.MinValue, Descricao = string.Empty });

            return lista;
        } 

        
          
    } 

}
