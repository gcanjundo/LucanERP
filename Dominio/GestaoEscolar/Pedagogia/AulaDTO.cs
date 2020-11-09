using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class AulaDTO:RetornoDTO
    { 
        public int TurmaID { get; set; }
        public int DisciplinaID { get; set; }
        public int DocenteID { get; set; }
        public DateTime Data { get; set; }
        public string Sumario { get; set; }
        public string Tipo { get; set; }
        public string TarefaExtra { get; set; }
        public int PeriodoID { get; set; }
        public int AvaliacaoID { get; set; }
        public int DiaSemana { get; set; }
        public string Hora { get; set; }
        public string Observacoes { get; set; }
        public string Sala { get; set; }
        public int NroAula { get; set; } 
        public UnidadeCurricularDTO Disciplina { get; set; }
        public TurmaDTO Turma { get; set; }
        public DocenteDTO Docente { get; set; } 

        public AulaDTO()
        {

        }

        public AulaDTO(UnidadeCurricularDTO pUnidade, TurmaDTO pTurma)
        {
            Disciplina = pUnidade;
            Turma = pTurma;
        }
    }

     
}
