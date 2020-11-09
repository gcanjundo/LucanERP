using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Restauracao
{
    public class ReservaDTO:RetornoDTO
    { 
        public string Referencia { get; set; } 
        public DateTime HoraInicio { get; set; } 
        public DateTime HoraTermino { get; set; }
        public EntidadeDTO Cliente { get; set; }
        public int CustomerID { get; set; }
        public string Mesa { get; set; }
        public string BookingStatus { get; set; }
        public int Ocupantes { get; set; }
        public string Tipo { get; set; }
        public string Notas { get; set; }
    }
}
