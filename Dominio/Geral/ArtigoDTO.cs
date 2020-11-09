using System;
using System.Collections.Generic;

namespace Dominio.Geral
{
    public class ArtigoDTO : RetornoDTO
    { 
        
        public string CodigoBarras { get; set; }
        public string Tipo { get; set; }
        
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        public string UnidadeVenda { get; set; }
        public string UnidadeCompra { get; set; }
        public decimal PrecoCusto { get; set; }
        public int ImpostoID { get; set; }
        public decimal PercentualImposto { get; set; } // Valor Percentual
        public string Imposto { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal Preco { get; set; }
        public decimal PVPWithTax { get; set; }
        public bool MovimentaStock { get; set; }
        public bool MultiplasVariantes { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QtdUndCompra { get; set; }
        public decimal QtdUndVenda { get; set; }
        public string FotoArtigo { get; set; }
        public string Referencia { get; set; }
        public decimal MargemLucro { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaCompra { get; set; }
        public string Marca { get; set; }
        public string Fabricante { get; set; }
        public decimal ComissaoValue { get; set; }
        public List<StockInfoDTO> InfoStock { get; set; }
        public List<ComposicaoDTO> ListaComposicao { get; set; }
        public List<SemelhanteDTO> ListaSemelhantes { get; set; }
        public List<LoteDTO> ListaLotes { get; set; }
        public List<NumeroSerieDTO> ListaNroSerie { get; set; }
        public List<DimensaoDTO> ListaDimensoes { get; set; }
        public string Fornecedor { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal Volume { get; set; }
        public decimal Peso { get; set; }
        public decimal ValorStockPCM { get; set; }
        public decimal ValorStockPVP { get; set; }
        public decimal ImpostoLiquido { get; set; } // Imposto em Valor Monetário
        public List<ProductPriceListDTO> PricesList { get; set; }
        public int RetailID { get; set; }
        public string RetailDesignation { get; set; }
        public int VasilhameID { get; set; }
        public string VasilhameDesignation { get; set; }
        public int AlertaRupturaStock { get; set; }
        public string LoteManagement { get; set; }
        public int StockCaptive { get; set; }
        public int AllowedSaleWithLowStock { get; set; }
        public int LoteID { get; set; }
        public int DimesaoID { get; set; }
        public int SerieID { get; set; }
        public int PriceID { get; set; }
        public int SemelhanteID { get; set; }
        public decimal StockMaximo { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal ValorLiquido { get; set; }
        public int CurrencyID { get; set; }
        public int ArtigoVestuarioID { get; set; } // ID Vestuario Lavandaria 
        public int ConvenioID { get; set; }
        public decimal PrecoConvenio { get; set; } 
        public decimal ValorEntidade { get; set; }
        public decimal ValorUtente { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public ArtigoDTO()
        {
            Codigo = -1;
            Designacao = "";
            CodigoBarras = "";
            Referencia = "";
            Fornecedor = "-1";
            Marca = "-1";
            Categoria = "-1";
            UnidadeCompra = "-1";
            UnidadeVenda = "-1";
            QtdUndCompra = 0;
            QtdUndVenda = 0;
            Peso = 0;
            Volume = 0;
            ImpostoID = -1;
            PrecoCusto = 0;
            PrecoVenda = 0;
            ValorStockPCM = 0;
            MargemLucro = 0;
            InfoStock = new List<StockInfoDTO>();
            ListaComposicao = new List<ComposicaoDTO>();
            ListaSemelhantes = new List<SemelhanteDTO>();
            ListaNroSerie = new List<NumeroSerieDTO>();
            ListaLotes = new List<LoteDTO>();
            ListaDimensoes = new List<DimensaoDTO>();
            PricesList = new List<ProductPriceListDTO>();
            IsComposto = 0;
            ControladoPorLotes = 0;
            SujeitoDevolucao = 0;
            MovimentaStock = false;
            ImpostoIncluido = 0;
            Descontinuado = 0;
            MultiplosPrecos = 1;
            ValorStockPVP = 0;


        }

        public ArtigoDTO(int pCodigo)
        {
            Codigo = pCodigo;

        }

        public ArtigoDTO(int pCodigo, string pProductDesignation)
        {
            Codigo = pCodigo;
            Designacao = pProductDesignation;
        }

        public string ProductStatus { get; set; } 

        public int ArmazemID { get; set; }

        public string Modelo { get; set; }

        public string AnoFabrico { get; set; }

        public string CurtaDescricao { get; set; }

        public short PedidoCozinha { get; set; }

        public short SujeitoDevolucao { get; set; }

        public short ImpostoIncluido { get; set; }

        public short TemDimensoes { get; set; }

        public decimal PrecoCustoMedioPadrao { get; set; }
        public decimal PrecoCustoUltimo { get; set; }

        public DateTime DataUltimaSaida { get; set; }

        public string ValidadeIni { get; set; }

        public string ValidadeTerm { get; set; }

        public decimal Desconto { get; set; }

        public DateTime DataLimiteFaturacao { get; set; }

        public DateTime DataFabrico { get; set; }

        public StockInfoDTO ResumoStock { get; set; }

        public short MultiplosPrecos { get; set; }

        public short IsComposto { get; set; }

        public short Descontinuado { get; set; }

        public string Tamanho { get; set; }

        public string Cor { get; set; }

        public short ControladoPorLotes { get; set; }

        public decimal PrevisaoLucro { get; set; }

        public short DisponivelPOS { get; set; }
        public int IncomeUnit { get; set; }
        public object IncomeQuatity { get; set; }
        public int OutComeUnit { get; set; }
        public object OutComeQuantity { get; set; }
        public int ReferenceUnit { get; set; }
        public object ReferenceQuantity { get; set; }
        public int DelieveryDeadLine { get; set; }
        public int MainSupplier { get; set; }
        public string ComercialDescription { get; set; }
        public int MainSalesman { get; set; } 
        public decimal Calorias { get; set; }
        public string Notes { get; set; }
        public decimal Proteinas { get; set; }
        public decimal Gorduras { get; set; }
        public decimal Carbohidratos { get; set; }
        public string VideoPath { get; set; }
        public string ComposeType { get; set; }
        public decimal Balanca { get; set; }
        public decimal ComposePrice { get; set; }
        public short IsExternalService { get; set; }
        public short OnlyWithOrder { get; set; }
        public short RestProduct { get; set; }
        public short SoldWithLowNegativeStock { get; set; }
        public string SerialNumber { get; set; }
        public int DuracaoPreparo { get; set; }
        public int StockAccountID { get; set; }
        public int PurchageAccountID { get; set; }
        public int SalesAccountID { get; set; }
        public int PurchageRefundAccountID { get; set; }
        public int SalesRefundAccountID { get; set; }
        public int UnidadeID { get; set; }
        public DateTime LastProductIncomeDate { get; set; }
        public DateTime LastProductOutcomeDate { get; set; }
        public int SerialNumberID { get; set; }
        public bool WithRetention { get; set; }
        public string TipoProcedimento { get; set; }
        public string Especialidade { get; set; }
        public string Procedimento { get; set; }
        public int CustomerID { get; set; }
        public string StockType { get; set; }
        public int ArtigoVestarioID { get; set; }
        public string LoteReference { get; set; }
        public decimal QuantidadeExcesso { get; set; }
        public decimal QuantidadeRuptura { get; set; }

    }

    public class StockInfoDTO : RetornoDTO
    {
        public int ArtigoID { get; set; }
        public int ArmazemID { get; set; } 
        public string DesignacaoArtigo { get; set; }
        public decimal Actual { get; set; }
        public decimal Minima { get; set; }
        public decimal Maxima { get; set; }
        public decimal ContagemFisica { get; set; }
        public decimal EncomendaClientes { get; set; }
        public decimal EncomendaFornecedor { get; set; }
        public DateTime UltimaContagem { get; set; }
        public string Reference { get; set; }
        public string BarCode { get; set; }
        public decimal QuantidadeCorrente { get; set; }
        public decimal PrecoCustoMedio { get; set; }
        public bool IsWarehousePOS { get; set; }
        public ArtigoDTO Product { get; set; } 
        public decimal Saldo { get; set; }



    }

    public class DimensaoDTO : ArtigoDTO
    {
        public int ProductID { get; set; }
        public int CorID { get; set; }
        public int TamanhoID { get; set; }
    }

    public class ComposicaoDTO : ArtigoDTO
    {
        public int ArtigoVirtualID { get; set; }
        public decimal Percentual { get; set; }
    }

    public class SemelhanteDTO : ArtigoDTO
    {
        public int ProductID { get; set; }
    }

    public class LoteDTO : ArtigoDTO
    {

        public LoteDTO()
        {

        }

        public LoteDTO(int pLoteID)
        {
            Codigo = pLoteID;
        }

        public LoteDTO(int pLoteID, string pReference)
        {
            Codigo = pLoteID;
            Referencia = pReference;
        }

        public LoteDTO(string pLoteID, string pWarehouseID, int pProductID)
        {
            Codigo = int.Parse(pLoteID);
            WareHouseName = pWarehouseID;
            ProductID = pProductID;
        }

        public LoteDTO(ArtigoDTO dto)
        {
            ProductID = dto.Codigo;
            Utilizador = dto.Utilizador;
        }


        public int ProductID { get; set; } 
        public StockInfoDTO StockData { get; set; }
    }

    public class NumeroSerieDTO : ArtigoDTO
    {
        public int ProductID { get; set; }
        
    }

    public class ProductPriceListDTO : ArtigoDTO
    {
        public ProductPriceListDTO()
        {

        }

        public ProductPriceListDTO(int pTableID, string pTableName)
        {
            PriceTableID = pTableID;
            TablePriceDesignation = pTableName;
        }

        public ProductPriceListDTO(int pProductID)
        {
            Codigo = pProductID;
        }

        public ProductPriceListDTO(int pTableID, int pProductID)
        {
            Codigo = pProductID;
            PriceTableID = pTableID;
            Categoria = "-1";
        }

        public int PriceTableID { get; set; }  
        public bool DefaultPrice { get; set; }
        public string TablePriceDesignation { get; set; }
    }

    public class ProductPrinterDTO : RetornoDTO
    {
        public int ProductID { get; set; }
        public int PrinterID { get; set; }
        public string PrinterName { get; set; }
        public bool PrinterDeleted { get; set; }
        public int CopyNumber { get; set; }
    }



}
