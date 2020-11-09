

using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Transaction", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Transaction
    {
        [XmlElement(ElementName = "TransactionID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TransactionID { get; set; }
        [XmlElement(ElementName = "Period", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Period { get; set; }
        [XmlElement(ElementName = "TransactionDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TransactionDate { get; set; }
        [XmlElement(ElementName = "SourceID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "Description", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Description { get; set; }
        [XmlElement(ElementName = "DocArchivalNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DocArchivalNumber { get; set; }
        [XmlElement(ElementName = "TransactionType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TransactionType { get; set; }
        [XmlElement(ElementName = "GLPostingDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string GLPostingDate { get; set; }
        [XmlElement(ElementName = "Lines", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Lines Lines { get; set; }
    }
}
