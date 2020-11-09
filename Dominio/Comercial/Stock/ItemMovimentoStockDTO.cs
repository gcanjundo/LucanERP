using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Stock
{
   public class ItemMovimentoStockDTO:RetornoDTO
    {
        

        public int ArmazemOrigem { get; set; }
        public int AramzemDestino { get; set; }
        public int ArtigoID { get; set; }
        public decimal Existencia { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Qtd_Actual { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal Novo_PrecoCustoMedio { get; set; }
        public decimal PrecoCustoMedio_Actual { get; set; }
        public decimal ValorTotal { get; set; }
        
        public decimal TotalLiquido { get; set; }
        public int Movimento { get; set; }
        public int Operacao { get; set; } 
        public int DimensaoID { get; set; }
        public int SerieID { get; set; }
        public int PriceID { get; set; }
        public int LoteID { get; set; }
        public int SerialNumberID { get; set; }
        public int ComposeID { get; set; } 
        public ItemMovimentoStockDTO()
        {
            ArtigoID = -1;
            Designacao = string.Empty;
        } 

        public decimal MargemLucro { get; set; } 
        public string Referencia { get; set; } 
        public string Unidade { get; set; } 
        public decimal ContagemFisica { get; set; } 
        public decimal Acrto { get; set; } 
        public string Classificacao { get; set; } 
        public decimal PrecoCusto { get; set; } 
        public decimal PrecoVenda { get; set; } 
        public string Armazem { get; set; } 
        public string FotoArtigo { get; set; } 
        public string BarCode { get; set; } 
        public DateTime DataContagem { get; set; }
        public decimal HeightSize { get; set; }
        public decimal WidthSize { get; set; }
        public string LoteReference { get; set; }
        public decimal QuantidadeItens { get; set; }
        public string WarehouseFrom { get; set; }
        public string WarehouseDestiny { get; set; }
        public int TransferID { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal ValorStockPCM { get; set; }
    }
}
