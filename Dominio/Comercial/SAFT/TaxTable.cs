using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "TaxTable", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class TaxTable
    {
        [XmlElement(ElementName = "TaxTableEntry", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<TaxTableEntry> TaxTableEntry { get; set; }
    }
}
