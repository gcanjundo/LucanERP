using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.SAFT
{
    public class SaftDTO: RetornoDTO
    {
        public int SaftID { get; set; } 
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public string FileType { get; set; }
        public int FiscalYear { get; set; }
        public int Mounth { get; set; }
        public string Notes { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

    }
}
