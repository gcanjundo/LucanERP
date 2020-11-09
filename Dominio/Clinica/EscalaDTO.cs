using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class EscalaDTO: RetornoDTO
    { 
         
        public int Dia { get; set; } 
        public string DescricaoDia { get; set; }
        public ProfissionaDTO Profissional { get; set; }
        public EspecialidadeDTO Especialidade { get; set; }
        public DateTime InicioPeriodo1 { get; set; }    
        public DateTime TerminoPeriodo1 { get; set; }
        public DateTime InicioPeriodo2 { get; set; }
        public DateTime TerminoPeriodo2 { get; set; }
        public DateTime InicioPeriodo3 { get; set; }
        public DateTime TerminoPeriodo3 { get; set; }
        public DateTime InicioPeriodo4 { get; set; }
        public DateTime TerminoPeriodo4 { get; set; }
        public int EspecialidadeID { get; set; }
        public DateTime Data { get; set; }
    }
}
