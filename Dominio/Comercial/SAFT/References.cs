

using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "References", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class References
    {
        [XmlElement(ElementName = "Reference", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Reference { get; set; }
        [XmlElement(ElementName = "Reason", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Reason { get; set; }
    }
}
