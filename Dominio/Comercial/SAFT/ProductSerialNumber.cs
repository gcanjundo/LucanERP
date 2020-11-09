 
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "ProductSerialNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class ProductSerialNumber
    {
        [XmlElement(ElementName = "SerialNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SerialNumber { get; set; }
    }
}
