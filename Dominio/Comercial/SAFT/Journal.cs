

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Journal", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Journal
    {
        [XmlElement(ElementName = "JournalID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string JournalID { get; set; }
        [XmlElement(ElementName = "Description", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Transaction", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Transaction> Transaction { get; set; }
    }
}
