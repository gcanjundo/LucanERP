using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Tesouraria
{
    public class PagamentoDTO
    {
        public PagamentoDTO()
        {
            PaymentMethod = -1;
            Order = 0;
            PaymentDescription = string.Empty;
            Value = 0;
            Account = string.Empty;
            DocumentNumber = string.Empty;
            PaymentDate = DateTime.MinValue;
        }

        public PagamentoDTO(int pOrder, int pID, string pDescription, decimal pValue, string pAccount, string pDocumentNumber, DateTime pDate)
        {
            PaymentMethod = pID;
            Order = pOrder;
            PaymentDescription = pDescription;
            Value = pValue;
            Account = pAccount;
            DocumentNumber = pDocumentNumber;
            PaymentDate = pDate;
        }
        public int PaymentMethod { get; set; }
        public string PaymentDescription { get; set; } 
        public decimal Value { get; set; }
        public string Account { get; set; }
        public string DocumentNumber { get; set; }

        public int Order { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
