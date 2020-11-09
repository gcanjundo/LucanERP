using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class ContratoDTO:RetornoDTO
    {
        public int ContratoID { get; set; }
        public int ContratoTipo { get; set; }
        public DateTime DataContrato { get; set; }
        public int SerieID { get; set; }
        public int Numeracao { get; set; }
        public string Versao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public bool IsRenovavel { get; set; }
        public decimal Valor { get; set; }
        public decimal DescontoCliente { get; set; }
        public decimal DescontoComercial { get; set; }
        public int Moeda { get; set; }
        public decimal Cambio { get; set; }
        public int TemplatePaymentPlanID { get; set; } // Modelo de Avença
        public int PaymentMethod { get; set; }
        public int PaymentTerms { get; set; }
        public string Notes { get; set; }



    }
}
