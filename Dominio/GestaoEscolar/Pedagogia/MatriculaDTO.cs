using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class MatriculaDTO:RetornoDTO
    { 
         
        public int Turma { get; set; }

        public DateTime Data { get; set; } 
        public int Estado { get; set; }

        public string DescricaoEstado { get; set; }

        public AlunoDTO Aluno { get; set; }   
        public int Movimento { get; set; } 
        public int Classe { get; set; } 
        public int Curso { get; set; } 
        public int Ramo { get; set; }
        public string Turno { get; set; }

        public string SituacaoFinal { get; set; }

        public AnoCurricularDTO Plano { get; set; }

        public DateTime TerminoMatricula { get; set; } 
         
        

        public MatriculaDTO() 
        {
        
        }

        public MatriculaDTO(int pMatricula)
        {
            this.Codigo = pMatricula;
        }

        public MatriculaDTO(string pInscricao) 
        {
            this.Aluno = new AlunoDTO(Convert.ToInt32(pInscricao));
        }

        public MatriculaDTO(int pMatricula, int pAno, int pMovimentacao, AlunoDTO pAluno, int pClasse, int pEstado, DateTime pInicio, DateTime pTermino, int pTurma, string pSituacaoInicial, string pSituacaoFinal, string pTurno)
        {
            this.Codigo = pMatricula;
            this.AnoLectivo = pAno;
            this.Aluno = pAluno;
            this.Classe = pClasse;
            DataInicio = pInicio;
            DataTermino = pTermino;
            this.Estado = pEstado;
            this.Situacao = pSituacaoInicial;
            this.SituacaoFinal = pSituacaoFinal;
            this.Turno = pTurno;
            this.Turma = pTurma;
            this.Curso = pMovimentacao;
        }

        public MatriculaDTO(int pMatricula, int pAno, DateTime pData, int pMovimentacao, AlunoDTO pAluno, int pClasse, int pEstado, int pTurma, string pSituacaoInicial, string pSituacaoFinal)
        {
            this.Codigo = pMatricula;
            this.AnoLectivo = pAno;
            this.Data = pData;
            this.Aluno = pAluno;
            this.Classe = pClasse;
            this.Estado = pEstado;
            this.Situacao = pSituacaoInicial;
            this.Turma = pTurma;
            this.SituacaoFinal = pSituacaoFinal;
        }

        public MatriculaDTO(string pAluno, int pALectivo)
        {
            // TODO: Complete member initialization
            Aluno = new AlunoDTO(Convert.ToInt32(pAluno));
            AnoLectivo = pALectivo;
        }

        public MatriculaDTO(AlunoDTO pAluno)
        {
            // TODO: Complete member initialization
            Aluno = pAluno;
        }

        public MatriculaDTO(int pID, int pAno)
        {
            Codigo = pID;
            AnoLectivo = pAno;
            Aluno = new AlunoDTO(pID);
        }

        

        public string Departamento { get; set; }

        public string Origem { get; set; }

        public string AnoCurricular { get; set; }

        public bool TemIsencaoPropina { get; set; } 
        public bool TemMatricula { get; set; } 
        public DateTime DataPagto { get; set; }
        public int FaturaMatricula { get; set; }
        public bool TemIsencaoMatricula { get; set; }
        public string TurmaTemporaria { get; set; }
    }
}
