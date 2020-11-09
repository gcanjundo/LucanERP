using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Supplier", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Supplier
    {
        [XmlElement(ElementName = "SupplierID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SupplierID { get; set; }
        [XmlElement(ElementName = "AccountID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AccountID { get; set; }
        [XmlElement(ElementName = "SupplierTaxID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SupplierTaxID { get; set; }
        [XmlElement(ElementName = "CompanyName", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "BillingAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public BillingAddress BillingAddress { get; set; }
        [XmlElement(ElementName = "ShipFromAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<ShipFromAddress> ShipFromAddress { get; set; }
        [XmlElement(ElementName = "Telephone", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Telephone { get; set; }
        [XmlElement(ElementName = "Fax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Fax { get; set; }
        [XmlElement(ElementName = "Website", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Website { get; set; }
        [XmlElement(ElementName = "SelfBillingIndicator", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SelfBillingIndicator { get; set; }
        [XmlElement(ElementName = "Contact", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Contact { get; set; }
        [XmlElement(ElementName = "Email", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Email { get; set; }
    }
}
