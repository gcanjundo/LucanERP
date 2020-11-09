using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class ProvaDTO:RetornoDTO
    {
        
        public string Avaliacao { get; set; }
        public string Periodo { get; set; }
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public string Docente { get; set; }
        public DateTime DataProva { get; set; }
        public string Situacao { get; set; }

    }

}
