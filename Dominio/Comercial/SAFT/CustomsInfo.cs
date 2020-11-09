using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    [XmlRoot(ElementName = "CustomsInformation")]
    public class CustomsInformation
    {
        [XmlElement(ElementName = "ARCNo")]
        public string ARCNo { get; set; }
        [XmlElement(ElementName = "IECAmount")]
        public string IECAmount { get; set; }
    }
}
