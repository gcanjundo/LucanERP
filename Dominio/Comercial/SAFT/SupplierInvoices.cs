using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "PurchaseInvoices", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SupplierInvoices
    {
        [XmlElement(ElementName = "NumberOfEntries", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string NumberOfEntries { get; set; }
        [XmlElement(ElementName = "Invoice", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public List<SupplierInvoice> Invoice { get; set; }
    }
}
