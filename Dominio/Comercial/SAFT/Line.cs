﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "Line", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Line
    {
        [XmlElement(ElementName = "LineNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string LineNumber { get; set; }
        [XmlElement(ElementName = "ProductCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductCode { get; set; }
        [XmlElement(ElementName = "ProductDescription", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductDescription { get; set; }
        [XmlElement(ElementName = "Quantity", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Quantity { get; set; }
        [XmlElement(ElementName = "UnitOfMeasure", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string UnitOfMeasure { get; set; }
        [XmlElement(ElementName = "UnitPrice", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string UnitPrice { get; set; }

        [XmlElement(ElementName = "TaxBase", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxBase { get; set; }

        [XmlElement(ElementName = "TaxPointDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxPointDate { get; set; } 

        [XmlElement(ElementName = "Description", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Description { get; set; }

        [XmlElement(ElementName = "ProductSerialNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductSerialNumber { get; set; }
        [XmlElement(ElementName = "DebitAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DebitAmount { get; set; }
        [XmlElement(ElementName = "CreditAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CreditAmount { get; set; }

        [XmlElement(ElementName = "Tax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Tax Tax { get; set; }
        //[XmlElement(ElementName = "TaxExemptions", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        // public TaxExemptions TaxExemptions { get; set; } 
        [XmlElement(ElementName = "TaxExemptionReason", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptionReason { get; set; }

        [XmlElement(ElementName = "TaxExemptionCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxExemptionCode { get; set; }

        [XmlElement(ElementName = "SettlementAmount", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SettlementAmount { get; set; }

        [XmlElement(ElementName = "CustomsInformation", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public CustomsInformation CustomsInformation { get; set; }

        [XmlElement(ElementName = "SourceDocumentID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public SourceDocumentID SourceDocumentID { get; set; }

       
        
    }
}
