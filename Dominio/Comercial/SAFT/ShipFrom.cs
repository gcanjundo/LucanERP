

using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "ShipFrom", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class ShipFrom
    {
        [XmlElement(ElementName = "DeliveryID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DeliveryID { get; set; }
        [XmlElement(ElementName = "DeliveryDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DeliveryDate { get; set; }
        [XmlElement(ElementName = "WarehouseID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WarehouseID { get; set; }
        [XmlElement(ElementName = "LocationID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string LocationID { get; set; }
        [XmlElement(ElementName = "Address", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Address Address { get; set; }
    }
}
