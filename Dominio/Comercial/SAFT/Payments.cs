using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Payments", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Payments
    {
        [XmlElement(ElementName = "NumberOfEntries", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public int NumberOfEntries { get; set; }
        [XmlElement(ElementName = "TotalDebit", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalDebit { get; set; }
        [XmlElement(ElementName = "TotalCredit", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalCredit { get; set; }
        [XmlElement(ElementName = "Payment", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Payment> Payment { get; set; }
    }
}
