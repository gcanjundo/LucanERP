using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Tax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Tax
    {
        [XmlElement(ElementName = "TaxType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxType { get; set; } 
        [XmlElement(ElementName = "TaxCountryRegion", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxCountryRegion { get; set; }
        [XmlElement(ElementName = "TaxCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxCode { get; set; }
        [XmlElement(ElementName = "TaxPercentage", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxPercentage { get; set; }

        [XmlElement(ElementName = "TaxAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxAmount { get; set; }
        
        
    }
}
