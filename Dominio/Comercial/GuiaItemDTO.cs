using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class GuiaItemDTO:ItemFaturacaoDTO
    {
        public decimal Excesso { get; set; }
        public string Marca { get; set; }
        public string Substracto { get; set; }
        public string CustomerName { get; set; }
        public string ReceiverName { get; set; }
        public string DeliveredName { get; set; } 
    }
}
