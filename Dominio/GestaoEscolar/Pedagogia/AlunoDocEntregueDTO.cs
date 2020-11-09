using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class AlunoDocEntregueDTO:RetornoDTO
    {
           
        public int StudentID { get; set; }
        public int DocumentID { get; set; } 
        public byte[] Ficheiro { get; set; }
        public string FileByte { get; set; }
        public string DocumentPath { get; set; }
        public string ContentType { get; set; }
        public int Quantidade { get; set; }
        public string DocName { get; set; }
        public string DocExtension { get; set; }
        
    }
}
