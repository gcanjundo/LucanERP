using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "MasterFiles", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class MasterFiles
    {
        [XmlElement(ElementName = "GeneralLedgerAccounts", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<GeneralLedgerAccounts> GeneralLedgerAccounts { get; set; }
        [XmlElement(ElementName = "Customer", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Customer> Customer { get; set; }
        [XmlElement(ElementName = "Supplier", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Supplier> Supplier { get; set; }
        [XmlElement(ElementName = "Product", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<Product> Product { get; set; }
        [XmlElement(ElementName = "TaxTable", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public TaxTable TaxTable { get; set; }
    }
}
