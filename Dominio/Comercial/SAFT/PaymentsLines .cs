using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Line", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class PaymentsLines
    {
        [XmlElement(ElementName = "LineNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string LineNumber { get; set; }
        [XmlElement(ElementName = "SourceDocumentID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public SourceDocumentID SourceDocumentID { get; set; }
        [XmlElement(ElementName = "SettlementAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SettlementAmount { get; set; }
        [XmlElement(ElementName = "DebitAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DebitAmount { get; set; }
        [XmlElement(ElementName = "CreditAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CreditAmount { get; set; }
        [XmlElement(ElementName = "Tax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Tax Tax { get; set; }
        [XmlElement(ElementName = "TaxExemptionReason", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptionReason { get; set; }
        [XmlElement(ElementName = "TaxExemptionCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptionCode { get; set; }
        [XmlElement(ElementName = "TaxExemptions", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptions { get; set; }
    }
}
