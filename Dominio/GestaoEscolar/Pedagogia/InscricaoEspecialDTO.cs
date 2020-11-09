using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class InscricaoDisciplinaDTO:RetornoDTO
    {
        
        public string Aluno { get; set; } 
        public string Disciplina { get; set; } 
        public string Turma { get; set; }
        public string Regime { get; set; }
        public string Exame { get; set; }

    }
}
