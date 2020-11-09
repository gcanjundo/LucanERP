using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Currency", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Currency
    {
        [XmlElement(ElementName = "CurrencyCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CurrencyCode { get; set; }
        [XmlElement(ElementName = "CurrencyAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CurrencyAmount { get; set; }
        [XmlElement(ElementName = "ExchangeRate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ExchageRate { get; set; }
    }
}
