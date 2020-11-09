
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Payment")]
    public class CustomerInvoicePayment
    {
        [XmlElement(ElementName = "PaymentMechanism")]
        public string PaymentMechanism { get; set; }
        [XmlElement(ElementName = "PaymentAmount")]
        public string PaymentAmount { get; set; }
        [XmlElement(ElementName = "PaymentDate")]
        public string PaymentDate { get; set; }
    }
}
