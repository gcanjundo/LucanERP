 
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "DocumentStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class PaymentsDocumentStatus
    {
          
        [XmlElement(ElementName = "PaymentStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string PaymentStatus { get; set; }
        [XmlElement(ElementName = "PaymentStatusDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string PaymentStatusDate { get; set; }
        //[XmlElement(ElementName = "InvoiceStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        //public string InvoiceStatus { get; set; }
        [XmlElement(ElementName = "Reason", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Reason { get; set; }
        [XmlElement(ElementName = "SourceID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "SourcePayment", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourcePayment { get; set; }
    }
}
