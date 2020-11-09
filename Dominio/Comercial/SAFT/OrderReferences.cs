

using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "OrderReferences", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class OrderReferences
    {
        [XmlElement(ElementName = "OriginatingON", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string OriginatingON { get; set; }
        [XmlElement(ElementName = "OrderDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string OrderDate { get; set; }
    }

    
}
