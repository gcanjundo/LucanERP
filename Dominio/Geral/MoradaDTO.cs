using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Geral
{
    public class MoradaDTO: RetornoDTO
    {
        public string BuildingNumber { get; set; }
        public string StreetName { get; set; }
        public string AddressDetail { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; } 
        public string PhoneNumber { get; set; } 
    }
}
