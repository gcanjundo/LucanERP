using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "DocumentTotals", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class SupplierInvoiceDocumentTotal
    {
        [XmlElement(ElementName = "InputTax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string InputTax { get; set; }

        [XmlElement(ElementName = "TaxBase", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxBase { get; set; }

        [XmlElement(ElementName = "GrossTotal", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string GrossTotal { get; set; }

        [XmlElement(ElementName = "DeductibleTax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DeductibleTax { get; set; }

        [XmlElement(ElementName = "DeductiblePercentage", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DeductiblePercentage { get; set; }

        [XmlElement(ElementName = "CurrencyCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CurrencyCode { get; set; }
        [XmlElement(ElementName = "CurrencyAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CurrencyAmount { get; set; }
        /*
        [XmlElement(ElementName = "Settlement", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Settlement Settlement { get; set; }
        
        [XmlElement(ElementName = "Payment", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public CustomerInvoicePayment[] Payment { get; set; }*/
    }
}
