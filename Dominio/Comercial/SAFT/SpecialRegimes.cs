using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "SpecialRegimes", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SpecialRegimes
    {
        [XmlElement(ElementName = "SelfBillingIndicator", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SelfBillingIndicator { get; set; }
        [XmlElement(ElementName = "CashVATSchemeIndicator", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CashVATSchemeIndicator { get; set; }
        [XmlElement(ElementName = "ThirdPartiesBillingIndicator", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ThirdPartiesBillingIndicator { get; set; }
    }
}
