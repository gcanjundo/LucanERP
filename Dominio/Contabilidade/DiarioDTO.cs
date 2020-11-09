using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contabilidade
{
    public class DiarioDTO:TabelaGeral
    {
        
    }

    public class DiarioDocumentoDTO:TabelaGeral
    {
        public int DiarioID { get; set; }
        public DiarioDTO Diario { get; set; }
        public int FiscalCodeID { get; set; }
    }

    public class DiarioDocumentoPlanoDTO
    {
        public int Numero { get; set; }
        public int DocumentoID { get; set; }
        public DiarioDocumentoDTO DiarioDocumento { get; set; }
        public string Natureza { get; set; }
        public decimal FactorMultiplicador { get; set; }
        public decimal ValorFixo { get; set; } 
    }
}
