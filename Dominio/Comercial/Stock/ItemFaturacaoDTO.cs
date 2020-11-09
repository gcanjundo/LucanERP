using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class ItemFaturacaoDTO: RetornoDTO
    { 
        public int Fatura { get; set; }
        public int Artigo { get; set; }
        public string Referencia { get; set; } 
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal DescontoFatura { get; set; }
        public decimal Imposto { get; set; } 
        public decimal ValorDesconto { get; set; } 
        public decimal PercentagemDescontoNumerario { get; set; }
        public decimal ValorImposto { get; set; }
        public decimal TotalLiquido { get; set; } 
        public DateTime DataEntrada { get; set; }
        public string Notas { get; set; }
        public bool MovimentaStock { get; set; }
        public int ArmazemID { get; set; }
        public List<ArmazemDTO> ArmazemList { get; set; }
        public bool Composicao { get; set; }
        public int ComposeID { get; set; }
        public int LoteID { get; set; }
        public int DimensaoID { get; set; }
        public int SerialNumberID { get; set; }

        public List<ComposicaoDTO> ComposeList { get; set; }
        public bool KitchenOrder { get; set; }
        public bool OrderSaved { get; set; }
       public bool SavedNote { get; set; }
        public string ItemNotes { get; set; }
        public object PedidoID { get; set; }
        public string PrinterDestiny { get; set; }
        public bool Deleted { get; set; }
        public int CookerID { get; set; }
        public string Prioridade { get; set; } 
        public int TaxID { get; set; }
        public string UnidadeID { get; set; }
        public DateTime DataEntrega { get; set; }
        public string ItemStatus { get; set; }
        public decimal QuantidadeSatisfeita { get; set; }
        public decimal QuantidadeReservada { get; set; }
        public decimal Retencao { get; set; }
        public int DocOrigemID { get; set; }
        public int DocOrigemLineNumber { get; set; }
        public int DocEnvioID { get; set; } 
        public int DocEnvioLineNumber { get; set; }  
        public decimal DescontoFinanceiro { get; set; }
        public decimal ValorDescontoFinanceiro { get; set; }
        public decimal PrecoCusto { get; set; }
        public string Unidade { get; set; }
        public decimal DescontoNumerario { get; set; }
        public string BarCode { get; set; }
        public decimal ExistenciaAnterior { get; set; }
        public decimal Lucro { get; set; }
        public int OrigemLineNumber { get; set; }
        public int EnvioLineNumber { get; set; }
        public DateTime DataEnvio { get; set; }
        public decimal PrecoUnitarioOriginalCurrency { get; set; }
        public int OriginalCurrencyID { get; set; }
        public decimal WidthSize { get; set; }
        public decimal HeightSize { get; set; }
        public bool Acumula { get; set; }
        public string FactorConversao { get; set; }
        public decimal ValorConversao { get; set; }
        public decimal PrecoMilheiro { get; set; } 
        public decimal TotalComissaoLinha { get; set; }
        public decimal Comissao { get; set; }
        public decimal Carreira { get; set; }
        public decimal QuantidadeM2 { get; set; }
        public decimal Desperdicio { get; set; }
        public decimal TotalDesperdicioM2 { get; set; }
        public decimal QuantidadeRolo { get; set; }
        public decimal MaterialMM { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal CustoSubstractoM2 { get; set; }
        public decimal CustoCliche { get; set; }
        public decimal CustoCilindro { get; set; }
        public decimal CustoFaca { get; set; }
        public decimal MaoObra { get; set; }
        public decimal CustoTinta { get; set; }
        public decimal CustoVerniz { get; set; }
        public decimal OutrosCustos { get; set; }
        public decimal CustoProducao { get; set; }
        public decimal MargemLucro { get; set; }
        public decimal DimensaoRolo { get; set; }
        public DateTime DataPrevisaoEntrega { get; set; }
        public DateTime ReadyDate { get; set; }
        public int DocOrigemTypeID { get; set; }
        public int DocEnvioTypeID { get; set; }
        public decimal PrecoConvenio { get; set; }
        public decimal PrecoUtente { get; set; }
        public decimal PrecoEntidade { get; set; }
        public int RetencaoID { get; set; }
        public bool WithRetention { get; set; }
        public decimal Weight { get; set; }
        public int QuantidadeItens { get; set; }
        public string Comentarios { get; set; }
        public decimal ItemValorFrete { get; set; }
        public decimal ItemValorSeguro { get; set; }
        public decimal ItemValorIva { get; set; }
        public decimal ItemValorAduaneiro { get; set; }
        public decimal ItemHonorarioDespachante { get; set; }
        public decimal ItemFreteTransporteLocal { get; set; }
        public decimal ItemOutrosEncargos { get; set; }
        public decimal TotalEncargos { get; set; } 
        public string ProductType { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Cambio { get; set; }
        public decimal WithholdingTax { get; set; }

        public ItemFaturacaoDTO()
        {
            NroOrdenacao = 1;
            Fatura = -1;
            Artigo = -1;
            Referencia = string.Empty;
            Designacao = string.Empty;
            Quantidade = 0;
            PrecoUnitario = 0;
            Desconto = 0;
            DescontoFatura = 0;
            Imposto = 0;
            ValorDesconto = 0;
            ValorImposto = 0;
            TotalLiquido = 0;
            Notas = string.Empty;
            DataEntrada = DateTime.Now;
            ArmazemList = new List<ArmazemDTO>();
            ArmazemID = -1;
            Status = 1;
            Composicao = false;
            LoteID = 0;
            DimensaoID = 0;
           
        }

        public ItemFaturacaoDTO(int pOrdem)
        {
            NroOrdenacao = pOrdem;
            Fatura = -1;
            Artigo = -1;
            Referencia = string.Empty;
            Designacao = string.Empty;
            Quantidade = 0;
            PrecoUnitario = 0;
            Desconto = 0;
            DescontoFatura = 0;
            Imposto = 0;
            ValorDesconto = 0;
            ValorImposto = 0;
            TotalLiquido = 0;
            Notas = string.Empty;
            DataEntrada = DateTime.Now;
            ArmazemList = new List<ArmazemDTO>();
            ArmazemID = -1;
            Status = 1;
        }


         
    }
}
