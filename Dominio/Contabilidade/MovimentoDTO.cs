using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contabilidade
{
    public class MovimentoDTO: RetornoDTO
    {
        public int DiarioDocumentoID { get; set; }
        public DiarioDocumentoDTO DiarioDocumento { get; set; }
        public int NumeroMovimento { get; set; }
        public DateTime DataCivil { get; set; }
        public DateTime DataFiscal { get; set; }
        public int SerieID { get; set; }
        public int FiscalYear { get; set; } 

    }

    public class MovimentoContaDTO:RetornoDTO
    {
        public int MovimentoID { get; set; }
        public MovimentoDTO Movimento { get; set; }
        public int ContaID { get; set; }
        public PlanoContaDTO Conta { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
    }
}
