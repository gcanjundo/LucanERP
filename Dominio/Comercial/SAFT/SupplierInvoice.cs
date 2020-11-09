using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    /*
    [XmlRoot(ElementName = "Invoice")]
    public class SupplierInvoice
    {
        [XmlElement(ElementName = "InvoiceDate")]


        [XmlElement(ElementName = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [XmlElement(ElementName = "Period")]
        public string Period { get; set; }

        [XmlElement(ElementName = "InvoiceDate")]
        public string InvoiceDate { get; set; }
        
        [XmlElement(ElementName = "InvoiceType")]
        public string InvoiceType { get; set; }
        
        [XmlElement(ElementName = "SourceID")]
        public string SourceID { get; set; }
        
        [XmlElement(ElementName = "SupplierID")]
        public string SupplierID { get; set; }
         
        [XmlElement(ElementName = "DocumentTotals")]
        public SupplierInvoiceDocumentTotal DocumentTotals { get; set; }
        public int InvoiceID { get; set; }
    }*/

    [XmlRoot(ElementName = "Invoice", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SupplierInvoice
    {
        [XmlElement(ElementName = "InvoiceNo", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string InvoiceNo { get; set; }
        [XmlElement(ElementName = "DocumentStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public DocumentStatus DocumentStatus { get; set; }
        [XmlElement(ElementName = "Hash", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Hash { get; set; }
        [XmlElement(ElementName = "HashControl", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string HashControl { get; set; }
        [XmlElement(ElementName = "InvoiceDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string InvoiceDate { get; set; }
        [XmlElement(ElementName = "InvoiceType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string InvoiceType { get; set; }
        [XmlElement(ElementName = "SpecialRegimes", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public SpecialRegimes SpecialRegimes { get; set; }
        [XmlElement(ElementName = "SourceID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "EACCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string EACCode { get; set; }
        [XmlElement(ElementName = "SystemEntryDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SystemEntryDate { get; set; }
        [XmlElement(ElementName = "TransactionID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TransactionID { get; set; }
        [XmlElement(ElementName = "CustomerID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CustomerID { get; set; }
        [XmlElement(ElementName = "MovementStartTime", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementStartTime { get; set; }
        [XmlElement(ElementName = "Line", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Line> Line { get; set; }
        [XmlElement(ElementName = "DocumentTotals", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public DocumentTotals DocumentTotals { get; set; }
        [XmlElement(ElementName = "WithholdingTax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<WithholdingTax> WithholdingTax { get; set; }
        [XmlElement(ElementName = "Period", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Period { get; set; }
        [XmlElement(ElementName = "ShipTo", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ShipTo { get; set; }
        [XmlElement(ElementName = "MovementEndTime", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementEndTime { get; set; }
        [XmlElement(ElementName = "SupplierID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SupplierID { get; set; }
    }

}
