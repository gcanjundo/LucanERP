
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Lines", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Lines
    {
        [XmlElement(ElementName = "DebitLine", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public DebitLine DebitLine { get; set; }
        [XmlElement(ElementName = "CreditLine", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public CreditLine CreditLine { get; set; }
    }
}
