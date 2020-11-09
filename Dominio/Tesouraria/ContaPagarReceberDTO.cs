using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Tesouraria
{
    public class ContaPagarReceberDTO:RetornoDTO
    {
        
        public int Documento { get; set; }
        public string Referencia { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Vencimento { get; set; }
        public int Parcela { get; set; } 
        public string Natureza { get; set; }
        public int RubricaID { get; set; }
        public decimal Valor { get; set; }
        public decimal Pendente { get; set; }
        public int Moeda { get; set; }
        public decimal Cambio { get; set; }
        public int Periodicidade { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
        public decimal Saldo { get; set; }
        public decimal ValorIncidencia { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal ValorRetencao { get; set; }
        public int IsReal { get; set; }
        public int Dias { get; set; }
    }
}
