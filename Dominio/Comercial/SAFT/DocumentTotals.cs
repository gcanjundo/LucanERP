using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "DocumentTotals", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class DocumentTotals
    {
        [XmlElement(ElementName = "TaxPayable", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxPayable { get; set; }
        [XmlElement(ElementName = "NetTotal", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string NetTotal { get; set; }
        [XmlElement(ElementName = "GrossTotal", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string GrossTotal { get; set; }
        [XmlElement(ElementName = "InputTax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string InputTax { get; set; }
    }
}
