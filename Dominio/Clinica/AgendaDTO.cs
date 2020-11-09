using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class AgendaDTO:RetornoDTO
    {
        

        public string Marcacao { get; set; }

        public DateTime DataMarcacao { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Termino { get; set; }

        public DateTime DataRealizacao { get; set; }

        public string Profissional { get; set; }

        public  string Especialidade { get; set; }

        public string Utente { get; set; } 
         
        public string Observacao { get; set; }
         
    }
}
