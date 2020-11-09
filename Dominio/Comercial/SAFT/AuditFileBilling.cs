
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    /*
    [XmlRoot(ElementName = "AuditFile")]
    public class AuditFileBilling : AuditFile
    {
        [XmlElement(ElementName = "Header")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "MasterFiles")]
        public MasterFilesBilling MasterFiles { get; set; }
        [XmlElement(ElementName = "SourceDocuments")]
        public SourceDocumentsBilling SourceDocuments { get; set; }
    }*/


    [XmlRoot(ElementName = "AuditFile", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class AuditFileBilling : AuditFile
    {
         
    }














}
