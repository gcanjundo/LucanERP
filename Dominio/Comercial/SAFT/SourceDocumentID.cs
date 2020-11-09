
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{

    [XmlRoot(ElementName = "SourceDocumentID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SourceDocumentID
    {
        [XmlElement(ElementName = "OriginatingON", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string OriginatingON { get; set; }
        [XmlElement(ElementName = "InvoiceDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string InvoiceDate { get; set; }
    }
}
