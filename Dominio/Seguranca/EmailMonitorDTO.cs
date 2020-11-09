
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class EmailMonitorDTO:RetornoDTO
    {
        
          
        public string Remetente { get; set; } 
        public string Endereco { get; set; } 
        public string Servidor { get; set; } 
        public string Usuario { get; set; } 
        public int Porta { get; set; } 
        public bool AtivaSSL { get; set; } 
        public bool UseDefaultCredencial { get; set; } 
    }
}
