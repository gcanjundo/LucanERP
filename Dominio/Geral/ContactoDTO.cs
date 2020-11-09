using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class ContactoDTO:RetornoDTO
    {
       
        public int Pessoa { get; set; }

        public string NomePessoa { get; set; }

        public string Contacto { get; set; }
        public string Tipo { get; set; }

        public string IsPrincial
        {
            get;
            set;
        }
    }
}
