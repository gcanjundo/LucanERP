using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "MovementOfGoods", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class MovementOfGoods
    {
        [XmlElement(ElementName = "NumberOfMovementLines", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string NumberOfMovementLines { get; set; }
        [XmlElement(ElementName = "TotalQuantityIssued", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalQuantityIssued { get; set; }
    }
}
