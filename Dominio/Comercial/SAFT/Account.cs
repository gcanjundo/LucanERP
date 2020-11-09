using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Account", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Account
    {
        [XmlElement(ElementName = "AccountID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AccountID { get; set; }
        [XmlElement(ElementName = "AccountDescription", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AccountDescription { get; set; }
        [XmlElement(ElementName = "OpeningDebitBalance", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string OpeningDebitBalance { get; set; }
        [XmlElement(ElementName = "OpeningCreditBalance", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string OpeningCreditBalance { get; set; }
        [XmlElement(ElementName = "ClosingDebitBalance", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ClosingDebitBalance { get; set; }
        [XmlElement(ElementName = "ClosingCreditBalance", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ClosingCreditBalance { get; set; }
        [XmlElement(ElementName = "GroupingCategory", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string GroupingCategory { get; set; }
        [XmlElement(ElementName = "GroupingCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string GroupingCode { get; set; }
    }

}
