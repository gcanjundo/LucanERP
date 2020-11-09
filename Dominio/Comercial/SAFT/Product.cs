using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Product", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Product
    {
        [XmlElement(ElementName = "ProductType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductType { get; set; }
        [XmlElement(ElementName = "ProductCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductCode { get; set; }
        [XmlElement(ElementName = "ProductGroup", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductGroup { get; set; }
        [XmlElement(ElementName = "ProductDescription", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductDescription { get; set; }
        [XmlElement(ElementName = "ProductNumberCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductNumberCode { get; set; }
        
        [XmlElement(ElementName = "CustomsDetails", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CustomsDetails { get; set; }
    }

}
