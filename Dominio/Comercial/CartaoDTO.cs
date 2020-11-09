using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class CartaoDTO:RetornoDTO
    {
        public int CardID { get; set; }
        public int CustomerID { get; set; }
        public int CardNumber { get; set; }
        public string Barcode { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Inicialalance { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal AcumulatedBalance { get; set; }
        public string Notes { get; set; }

    }
}
