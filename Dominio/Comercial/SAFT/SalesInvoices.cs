using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "SalesInvoices", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SalesInvoices
    {
        [XmlElement(ElementName = "NumberOfEntries", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string NumberOfEntries { get; set; }
        [XmlElement(ElementName = "TotalDebit", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalDebit { get; set; }
        [XmlElement(ElementName = "TotalCredit", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalCredit { get; set; }
        [XmlElement(ElementName = "Invoice", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<CustomerInvoice> Invoice { get; set; }
    }

}
