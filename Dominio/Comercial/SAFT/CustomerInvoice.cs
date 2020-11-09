using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Invoice")]
    public class CustomerInvoice
    {
        [XmlIgnore()]
        public int InvoiceID { get; set; }

        [XmlElement(ElementName = "InvoiceNo")]
        public string InvoiceNo { get; set; }
        [XmlElement(ElementName = "DocumentStatus")] 
        public DocumentStatus DocumentStatus { get; set; }
        [XmlElement(ElementName = "Hash")]
        public string Hash { get; set; }
        [XmlElement(ElementName = "HashControl")]
        public string HashControl { get; set; }
        [XmlElement(ElementName = "Period")]
        public string Period { get; set; }
        [XmlElement(ElementName = "InvoiceDate")]
        public string InvoiceDate { get; set; }
        [XmlElement(ElementName = "InvoiceType")]
        public string InvoiceType { get; set; }
        [XmlElement(ElementName = "SpecialRegimes")]
        public SpecialRegimes SpecialRegimes { get; set; }
        [XmlElement(ElementName = "SourceID")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "EACCode")]
        public string EACCode { get; set; }
        [XmlElement(ElementName = "SystemEntryDate")]
        public string SystemEntryDate { get; set; }
        [XmlElement(ElementName = "TransactionID")]
        public string TransactionID { get; set; }
        [XmlElement(ElementName = "CustomerID")]
        public string CustomerID { get; set; }
        [XmlElement(ElementName = "ShipTo")]
        public ShipTo ShipTo { get; set; }
        [XmlElement(ElementName = "ShipFrom")]
        public ShipFrom ShipFrom { get; set; }
        [XmlElement(ElementName = "MovementEndTime")]
        public string MovementEndTime { get; set; }
        [XmlElement(ElementName = "MovementStartTime")]
        public string MovementStartTime { get; set; }
        [XmlElement(ElementName = "Line")]
        public List<Line> Line { get; set; }
        [XmlElement(ElementName = "DocumentTotals")]
        public CustomerInvoiceDocumentTotals DocumentTotals { get; set; }
        [XmlElement(ElementName = "WithholdingTax")]
        public WithholdingTax WithholdingTax { get; set; }
    }
}
