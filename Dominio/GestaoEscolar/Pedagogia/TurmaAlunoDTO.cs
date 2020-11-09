using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class TurmaAlunoDTO:RetornoDTO
    {

        public string Operacao { get; set; }


        public TurmaDTO Turma { get; set; }
        TurmaDTO _turma = new TurmaDTO();

         
        MatriculaDTO _matricula = new MatriculaDTO();

        public MatriculaDTO Matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }

        UnidadeCurricularDTO _disciplina = new UnidadeCurricularDTO();

        public UnidadeCurricularDTO Disciplina
        {
            get { return _disciplina; }
            set { _disciplina = value; }
        }

         

        public TurmaAlunoDTO() 
        {
        
        }

        public TurmaAlunoDTO(TurmaDTO pTurma, MatriculaDTO pMatricula)
        {
            this.Matricula = pMatricula;
            this.Turma = pTurma; 

        } 

        public TurmaAlunoDTO(TurmaDTO pTurma, MatriculaDTO pMatricula, string pOperacao, string pNivelEnsino)
        {
            this.Matricula = pMatricula;
            this.Turma = pTurma;
            Operacao = pOperacao;
            NivelEnsino = pNivelEnsino;
           

        }

        public TurmaAlunoDTO(TurmaDTO pTurma, MatriculaDTO pMatricula, UnidadeCurricularDTO pDisciplina, int pAnoLectivo) 
        {
            this.Matricula = pMatricula;
            this.Turma = pTurma;
            this.Disciplina = pDisciplina;
            this.AnoLectivo = pAnoLectivo;
        }

    }
}
