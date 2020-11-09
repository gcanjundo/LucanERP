
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    /*
    [XmlRoot(ElementName = "PaymentMethod")]
    public class PaymentsPaymentMethod
    {
        [XmlElement(ElementName = "PaymentMechanism")]
        public string PaymentMechanism { get; set; }
        [XmlElement(ElementName = "PaymentAmount")]
        public string PaymentAmount { get; set; }
        [XmlElement(ElementName = "PaymentDate")]
        public string PaymentDate { get; set; }
    }*/

    [XmlRoot(ElementName = "PaymentMethod", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class PaymentsPaymentMethod
    {
        [XmlElement(ElementName = "PaymentAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string PaymentAmount { get; set; }
        [XmlElement(ElementName = "PaymentDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string PaymentDate { get; set; }
    }
}
