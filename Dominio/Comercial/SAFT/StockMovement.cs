﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "StockMovement", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class StockMovement
    {
        [XmlElement(ElementName = "DocumentNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DocumentNumber { get; set; }
        [XmlElement(ElementName = "DocumentStatus", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public DocumentStatus DocumentStatus { get; set; }
        [XmlElement(ElementName = "Hash", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Hash { get; set; }
        [XmlElement(ElementName = "HashControl", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string HashControl { get; set; }
        [XmlElement(ElementName = "Period", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Period { get; set; }
        [XmlElement(ElementName = "MovementDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementDate { get; set; }
        [XmlElement(ElementName = "MovementType", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementType { get; set; }
        [XmlElement(ElementName = "SystemEntryDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SystemEntryDate { get; set; }
        [XmlElement(ElementName = "TransactionID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TransactionID { get; set; }
        [XmlElement(ElementName = "CustomerID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CustomerID { get; set; }
        [XmlElement(ElementName = "SupplierID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SupplierID { get; set; }
        [XmlElement(ElementName = "SourceID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SourceID { get; set; }
        [XmlElement(ElementName = "EACCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string EACCode { get; set; }
        [XmlElement(ElementName = "MovementComments", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementComments { get; set; }
        [XmlElement(ElementName = "ShipTo", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public ShipTo ShipTo { get; set; }
        [XmlElement(ElementName = "ShipFrom", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public ShipFrom ShipFrom { get; set; }
        [XmlElement(ElementName = "MovementEndTime", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementEndTime { get; set; }
        [XmlElement(ElementName = "MovementStartTime", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string MovementStartTime { get; set; }
        [XmlElement(ElementName = "AGTDocCodeID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AGTDocCodeID { get; set; }
        [XmlElement(ElementName = "Line", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public PaymentsLines Line { get; set; }
        [XmlElement(ElementName = "DocumentTotals", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public CustomerInvoiceDocumentTotals DocumentTotals { get; set; }
    }
}
