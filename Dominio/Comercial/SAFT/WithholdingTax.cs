using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "WithholdingTax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class WithholdingTax
    {
        [XmlElement(ElementName = "WithholdingTaxType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WithholdingTaxType { get; set; }
        [XmlElement(ElementName = "WithholdingTaxDescription", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WithholdingTaxDescription { get; set; }
        [XmlElement(ElementName = "WithholdingTaxAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WithholdingTaxAmount { get; set; }
    }
}
