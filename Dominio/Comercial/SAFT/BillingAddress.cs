using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
     

    [XmlRoot(ElementName = "BillingAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class BillingAddress:Address
    {
        /*
        [XmlElement(ElementName = "BuildingNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string BuildingNumber { get; set; }
        [XmlElement(ElementName = "StreetName", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string StreetName { get; set; }
        [XmlElement(ElementName = "AddressDetail", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AddressDetail { get; set; }
        [XmlElement(ElementName = "City", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string City { get; set; }
        [XmlElement(ElementName = "PostalCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "Province", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]

        public string Province { get; set; }
        [XmlElement(ElementName = "Country", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Country { get; set; }*/
    }
}
