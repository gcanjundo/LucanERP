using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "WorkDocument", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class WorkDocument
    {
        [XmlElement(ElementName = "DocumentNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DocumentNumber { get; set; }
        [XmlElement(ElementName = "DocumentStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public WorkingDocumentStatus DocumentStatus { get; set; }
        [XmlElement(ElementName = "Hash", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Hash { get; set; }
        [XmlElement(ElementName = "HashControl", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string HashControl { get; set; }
        [XmlElement(ElementName = "Period", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Period { get; set; }
        [XmlElement(ElementName = "WorkDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WorkDate { get; set; }
        [XmlElement(ElementName = "WorkType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WorkType { get; set; }
        [XmlElement(ElementName = "SourceID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "SystemEntryDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SystemEntryDate { get; set; }
        [XmlElement(ElementName = "TransactionID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TransactionID { get; set; }
        [XmlElement(ElementName = "CustomerID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CustomerID { get; set; }
        [XmlElement(ElementName = "Line", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Line> Line { get; set; }
        [XmlElement(ElementName = "DocumentTotals", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public WorkingDocumentsTotals DocumentTotals { get; set; } 

        [XmlElement(ElementName = "EACCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string EACCode { get; set; }
        [XmlIgnore()]
        public int WorkID { get; set; }
    }
}
