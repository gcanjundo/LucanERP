using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "DebitLine", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class DebitLine
    {
        [XmlElement(ElementName = "RecordID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string RecordID { get; set; }
        [XmlElement(ElementName = "AccountID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AccountID { get; set; }
        [XmlElement(ElementName = "SystemEntryDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SystemEntryDate { get; set; }
        [XmlElement(ElementName = "Description", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Description { get; set; }
        [XmlElement(ElementName = "DebitAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DebitAmount { get; set; }
    }
}
