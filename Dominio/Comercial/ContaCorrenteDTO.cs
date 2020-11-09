using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Comercial;

namespace Dominio.Comercial
{
    public class ContaCorrenteDTO:RetornoDTO
    {
        
        public string Natureza { get; set; }
        public int Parcelamento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataConta { get; set; }
        public DateTime DataCobranca { get; set; }
        public DateTime DataPagamento { get; set; }
        public int DiasAtraso { get; set; }
        public decimal Escalao1 { get; set; }
        public decimal Escalao2 { get; set; }
        public decimal Escalao3 { get; set; }
        public decimal Escalao4 { get; set; }
        public decimal Escalao5 { get; set; }
        public decimal Escalao6 { get; set; }
        public decimal DividaCorrente { get; set; } 

    }
}
