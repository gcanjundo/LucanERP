using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Comercial
{
    public class PromocaoDTO:RetornoDTO
    {

        public PromocaoDTO()
        {
            Valor = 0;
        }
         
        public DateTime ValidationStartDate { get; set; }
        public DateTime ValidationEndDate { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string Recorrencia { get; set; }
        public string Tipo { get; set; }
        public bool AllProducts { get; set; }
        public decimal Valor { get; set; }
        public decimal MontanteMinimo { get; set; }
        public decimal MontantePercentualCalculo { get; set; }
        public decimal ValorMonetarioFixo { get; set; }
        public decimal QuatidadeCompra { get; set; }
        public string ForCustomer { get; set; }
        public DateTime LimiteUtilizacaoDate { get; set; }
        public List<ArtigoDTO> ProductList { get; set; }
        public List<EntidadeDTO> CustomerList { get; set; }
        public string Unidade { get; set; }
        public bool AllSalesValues { get; set; }
        public int CategoriaID { get; set; }
        public int MarcaID { get; set; }
        public int ModeloID { get; set; }
        public int TablePriceID { get; set; }
        public int ProductID { get; set; }

    }

    

     

}
