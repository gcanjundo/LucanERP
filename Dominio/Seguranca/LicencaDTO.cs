using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Seguranca
{
    public class LicencaDTO:RetornoDTO
    {
        public string LicenseID { get; set; } 
        public string ValidateDate { get; set; }
        public string FiscalYear { get; set; }
        public string GenereatedDate { get; set; }
        public string HostName { get; set; }
        public string HostMacAddress { get; set; }
        public string LicType { get; set; }
        public string LicDesignation { get; set; }
    }
}
