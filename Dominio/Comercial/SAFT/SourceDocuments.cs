using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "SourceDocuments", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SourceDocuments
    {
        [XmlElement(ElementName = "SalesInvoices", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public SalesInvoices SalesInvoices { get; set; }
        [XmlElement(ElementName = "MovementOfGoods", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public MovementOfGoods MovementOfGoods { get; set; }
        [XmlElement(ElementName = "WorkingDocuments", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public WorkingDocuments WorkingDocuments { get; set; }
        [XmlElement(ElementName = "Payments", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Payments Payments { get; set; }
        [XmlElement(ElementName = "PurchaseInvoices", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public SupplierInvoices PurchaseInvoices { get; set; }
    }
}
