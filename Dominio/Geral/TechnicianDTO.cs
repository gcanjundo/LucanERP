using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Geral
{
    public class TechnicianDTO : RetornoDTO
    {
         
        public TechnicianDTO()
        {

        }

        public TechnicianDTO(string pNome, string pFilial)
        {
            Entity = new EntidadeDTO(pNome);
            Filial = pFilial;
        }

        public int ProfissionalID { get; set; }
        public EntidadeDTO Entity { get; set; }
        public int Comissao { get; set; }
        public decimal ValorComissao { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string Tipo { get; set; }
        public string PINCode { get; set; } 


    }
}
