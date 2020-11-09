
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Customer", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Customer
    {
        [XmlElement(ElementName = "CustomerID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CustomerID { get; set; }
        [XmlElement(ElementName = "AccountID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AccountID { get; set; }
        [XmlElement(ElementName = "CustomerTaxID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CustomerTaxID { get; set; }
        [XmlElement(ElementName = "CompanyName", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "Contact", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Contact { get; set; }
        [XmlElement(ElementName = "BillingAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public BillingAddress BillingAddress { get; set; }
        [XmlElement(ElementName = "ShipToAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public ShipToAddress ShipToAddress { get; set; }
        [XmlElement(ElementName = "Telephone", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Telephone { get; set; }
        [XmlElement(ElementName = "Fax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Fax { get; set; }
        [XmlElement(ElementName = "Email", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Website", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Website { get; set; }
        [XmlElement(ElementName = "SelfBillingIndicator", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SelfBillingIndicator { get; set; }
    }
}
