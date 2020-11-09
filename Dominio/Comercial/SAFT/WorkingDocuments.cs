using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "WorkingDocuments", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class WorkingDocuments
    {
        [XmlElement(ElementName = "NumberOfEntries", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string NumberOfEntries { get; set; }
        [XmlElement(ElementName = "TotalDebit", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalDebit { get; set; }
        [XmlElement(ElementName = "TotalCredit", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TotalCredit { get; set; }
        [XmlElement(ElementName = "WorkDocument", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public WorkDocument[] WorkDocument { get; set; }
    }
}
