using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class NotaDTO:RetornoDTO
    {
        public bool isValidaEpocaNormal;
        public bool isValidaEpocaExame;
        public bool isValidaEpocaEspecial;

        public int NumeroOrdem { get; set; }
        public int Matricula { get; set; }
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public string Inscricao { get; set; }
        public string NomeCompleto { get; set; }
        public decimal PP1T1 { get; set; }
        public decimal PP2T1 { get; set; }
        public decimal TP1T1 { get; set; }
        public decimal TP2T1 { get; set; }
        public decimal MTPT1 { get; set; }
        public decimal MACT1 { get; set; }
        public decimal MT1 { get; set; }
        public decimal PP1T2 { get; set; }
        public decimal PP2T2 { get; set; }
        public decimal TP1T2 { get; set; }
        public decimal TP2T2 { get; set; }
        public decimal MTPT2 { get; set; }
        public decimal MACT2 { get; set; }
        public decimal MT2 { get; set; }
        public decimal PP1T3 { get; set; }
        public decimal PP2T3 { get; set; }
        public decimal TP1T3 { get; set; }
        public decimal TP2T3 { get; set; }
        public decimal MTPT3 { get; set; }
        public decimal MACT3 { get; set; }
        public decimal MT3 { get; set; }
        public decimal CAP { get; set; }
        public decimal CPE { get; set; }
        public decimal CF { get; set; }
        public decimal Exame { get; set; }
        public decimal Recurso { get; set; }
        public decimal CAA { get; set; } // Classificação Anual do Ano Anterior Serve de Base para o Cálculo da CFD
        public decimal CFD { get; set; } 
        public DateTime DataLancamento { get; set; } 
        public string Curso { get; set; }
        public string Classe { get; set; }
        public string Turno { get; set; }
        public string Ciclo { get; set; }
        public string Formacao { get; set; }
        public string NroProcesso { get; set; }
        public bool IsFinalizante { get; set; }
        public bool IsProjectoFinal { get; set; }
        public decimal CA10 { get; set; }
        public bool IsAnual { get; set; }
        public bool IsTerminal { get; set; }
        public bool IsTerminalEspecial { get; set; }
        public bool IsContinua { get; set; }
        public bool IsPAP { get; set; }

        public NotaDTO()
        {
            PP1T1 = -1;
            PP1T2 = -1;

            PP2T1 = -1;
            PP2T2 = -1;

            PP1T3 = -1;
            PP2T3 = -1;

            TP1T1 = -1;
            TP2T1 = -1;

            TP1T2 = -1;
            TP2T2 = -1;

            TP1T3 = -1;
            TP2T3 = -1;

            MACT1 = -1;
            MACT2 = -1;
            MACT3 = -1;

            MTPT1 = -1;
            MTPT2 = -1;
            MTPT3 = -1;
            CPE = -1;
            CF = -1;
            CAP = -1;
        } 
        public NotaDTO(int pMatricula, string pTurma, string pDisciplina, string pInscricao, string pNome)
        {
            Matricula = pMatricula;
            Turma = pTurma;
            Disciplina = pDisciplina;
            Inscricao = pInscricao;
            NomeCompleto = pNome;
        }

        public NotaDTO(string pDisciplina, string pTurma,  int pMatricula, int pAno, string pFilial, string pAvaliacao)
        {
            Matricula = pMatricula;
            Turma = pTurma;
            Disciplina = pDisciplina;
            AnoLectivo = pAno;
            Filial = pFilial;
            Avaliacao = pAvaliacao;
        }



        public NotaDTO(int pMatricula, int pAno, string pFilial)
        {
            Matricula = pMatricula; 
            AnoLectivo = pAno;
            Filial = pFilial;
        }

        public NotaDTO(int pMatricula, int pDisciplina, int pAno, decimal pNota)
        {
            Matricula = pMatricula; 
            DisciplinaID = pDisciplina;
            AnoLectivo = pAno;
            CFD = pNota;
        }

        public string Avaliacao { get; set; }

        public string Periodo { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public string AlunoID { get; set; }
        public string Observacao { get; set; }
        public string Resultado { get; set; }

        public string DirectorTurma { get; set; }

        public string Sala { get; set; }

        public string Sexo { get; set; }

        public int DisciplinaID { get; set; }

        public int TurmaID { get; set; }
        public string Picture { get; set; }
        public string Pai { get; set; }
        public string Mae { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Encarregado { get; set; }
        public string LocalNascimento { get; set; }
        public string Naturalidade { get; set; }
        public decimal NotaOriginal { get; set; }
        public string Epoca { get; set; }
    }

}
