using System.Collections.Generic;

namespace Dominio.Comercial
{
    public class ReceiptDTO:RetornoDTO
    {
        /*public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyVAT { get; set; }

        public string CompanyPhone { get; set; }*/

        public int Fatura { get; set; }
        
        public string ReceiptNumber { get; set; }

        public string Customer { get; set; }

        public string SalesDate { get; set; }

        public string SalesTime { get; set; }

        public string Subtotal { get; set; }
        
        public string totalItems { get; set; }
        
        public string Discount { get; set; }

        public string PaymentMethod { get; set; }

        public string IPC { get; set; }

        public string TotalToPay { get; set; }

        public string Paid { get; set; }

        public string Change { get; set; }

        public List<ItemFaturacaoDTO> SalesItems { get; set; }

        public string DocumentType { get; set; }
        
        public string CustomerNIF { get; set; } 
        
        public string TableNumber { get; set; }

        public string DocumentReference { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public string CustomerPhoneAlternate { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerAddressLine1 { get; set; }

        public string CustomerAddressLine2 { get; set; }

        public string DocumentCurrency { get; set; }

        public string DocumentRate { get; set; }

        public string CustomerDiscount { get; set; }

        public string DocumentDiscount { get; set; }

        public string DocumentDueDate { get; set; }

        public string ProductCode { get; set; }

        public string ProductBarcode { get; set; }

        public string ProductDesignation { get; set; }

        public string ProductQuantity { get; set; }

        public string ProductPrice { get; set; }

        public string ProductDiscount { get; set; }

        public string ProductTax { get; set; }


        public string ProductTotal { get; set; }

        public string DocumentDiscountValue { get; set; }

        public string DocumentTaxValue { get; set; }

        public string ValorExtenso { get; set; }

        public string DocumentBalance { get; set; }

        public string CustomerCurrentBalance { get; set; }

        public string CustomerBalanceUsed { get; set; }

        public string DocumentID { get; set; }

        public string ComercialNotes { get; set; }
        public string Hour { get; set; }
        public string ExpeditionDate { get; set; }
        public string Matricula { get; set; }
        public string ExpeditionPlace { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryDate { get; set; }
        public string ReceiverPeople { get; set; }
        public string DeliveryPeople { get; set; }
        public string ReceiverPeoplePhone1 { get; set; }
        public string ReceiverPeoplePhone2 { get; set; }
        public int TaxID { get; set; }
        public string DocumentNotes { get; set; }
        public string ProductNotes { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentBarcode { get; set; }
        public string ProductUnity { get; set; }
        public decimal CustomerDiscountValue { get; set; }
        public decimal FinanceDiscountValue { get; set; }
        public decimal ProductDiscountValue { get; set; }
        public string HashTag { get; set; }
        public decimal RetencaoLinha { get; set; }
        public string RetencaoDocumento { get; set; }
        public string FooterDocumentNotes { get; set; }
        public string HeaderNotes { get; set; }
        public string ValorUtente { get; set; }
        public string ValorEntidade { get; set; }
        public string TituloLinha { get; set; }
        public string CarMark { get; set; }
        public string CarModel { get; set; }
        public string CarRegistrationID { get; set; }
        public string CarYear { get; set; }
        public string CarKM { get; set; }
        public string CarColor { get; set; }
        public string ContractReference { get; set; }
        public string SESNumber { get; set; }
        public string ShipName { get; set; }
        public string DistritoUrbano { get; set; }
        public int CustomerCurrencyID { get; set; }
    }
}
