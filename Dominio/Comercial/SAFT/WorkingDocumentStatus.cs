 
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    /*
    [XmlRoot(ElementName = "DocumentStatus")]
    public class WorkingDocumentStatus
    {
        [XmlElement(ElementName = "WorkStatus")]
        public string WorkStatus { get; set; }
        [XmlElement(ElementName = "WorkStatusDate")]
        public string WorkStatusDate { get; set; }
        [XmlElement(ElementName = "Reason")]
        public string Reason { get; set; }
        [XmlElement(ElementName = "SourceID")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "SourceBilling")]
        public string SourceBilling { get; set; } 
         
    }*/

    [XmlRoot(ElementName = "DocumentStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class WorkingDocumentStatus
    {  
        
        [XmlElement(ElementName = "WorkStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WorkStatus { get; set; }
        [XmlElement(ElementName = "WorkStatusDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string WorkStatusDate { get; set; } 
        [XmlElement(ElementName = "SourceID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "SourceBilling", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceBilling { get; set; } 
        [XmlElement(ElementName = "SourcePayment", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourcePayment { get; set; }
    }
}
