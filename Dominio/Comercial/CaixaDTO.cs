using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Comercial
{
   
    
    public class CaixaDTO:RetornoDTO
    { 
         
        public DateTime Data { get; set;} 
        public DateTime Abertura { get; set; } 
        public decimal ValorInicial { get; set; }  
        public DateTime Fecho { get; set; } 
        public decimal ValorFinal { get; set; } 
    }
}
