using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Settlement", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Settlement
    {
        [XmlElement(ElementName = "SettlementDiscount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SettlementDiscount { get; set; }
        [XmlElement(ElementName = "SettlementAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SettlementAmount { get; set; }
        [XmlElement(ElementName = "SettlementDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SettlementDate { get; set; }
        [XmlElement(ElementName = "PaymentTerms", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string PaymentTerms { get; set; }


    }
}
