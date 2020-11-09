using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
   public class ResumoDTO:RetornoDTO
    {
        public string ReceiptNumber { get; set; }
        public List<MetodoPagamentoDTO> MetodosPagamentos = new List<MetodoPagamentoDTO>();
    }
}
