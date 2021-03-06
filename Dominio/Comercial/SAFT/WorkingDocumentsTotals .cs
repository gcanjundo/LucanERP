﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "DocumentTotals", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class WorkingDocumentsTotals
    {
        [XmlElement(ElementName = "TaxPayable", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxPayable { get; set; }
        [XmlElement(ElementName = "NetTotal", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string NetTotal { get; set; }
        [XmlElement(ElementName = "GrossTotal", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string GrossTotal { get; set; }
        [XmlElement(ElementName = "Currency", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public Currency Currency { get; set; } 
    }
}
