using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.RecursosHumanos
{
    public class TipoProcessamentoDTO:TipoDTO
    {
        public List<TipoProcessamentoItemDTO> itemProcessamentoList { get; set; }
    }

    public class TipoProcessamentoItemDTO : RetornoDTO
    {
        public int TipoProcessamentoId { get; set; }
        public TipoProcessamentoDTO TipoDesconto { get; set; }
        public int AbonoDescontoId { get; set; }
        public AbonoDescontoDTO AbonoDesconto { get; set; } 
    }
}
