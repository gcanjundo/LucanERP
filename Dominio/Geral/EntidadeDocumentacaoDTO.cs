using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class EntidadeDocumentacaoDTO:RetornoDTO
    {
        
         
        public int Documento { get; set; }  
        public string Numero { get; set; }
        public DateTime Emissao { get; set; } 
        public DateTime Validade { get; set; } 
        public string LocalEmissao { get; set; }
        public string NomeDocumento { get; set; }
    }
}
