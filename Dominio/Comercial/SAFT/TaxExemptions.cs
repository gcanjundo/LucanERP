using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "TaxExemptions", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class TaxExemptions
    {
        [XmlElement(ElementName = "TaxExemptionReason", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptionReason { get; set; }

        [XmlElement(ElementName = "TaxExemptionCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptionCode { get; set; }
    }
}
