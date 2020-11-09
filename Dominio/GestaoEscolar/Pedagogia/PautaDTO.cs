using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class PautaDTO:RetornoDTO
    {
         

        public int Periodo { get; set; }

        public string DsPeriodo { get; set; }

        public int Turma { get; set; }

        public string DsTurma { get; set; }

        public int Disciplina { get; set; }

        public string DsDisciplina { get; set; }

        public int Prova { get; set; }

        public DateTime DataProva { get; set; }

        public string DsAnoLectivo { get; set; }

        public string PauDsAvaliacao { get; set; }

        public Int32 Aluno { get; set; }

        public String InscricaoAluno { get; set; }

        public String NomeAluno { get; set; }
        public Decimal PrimeiraNota { get; set; }

        public Decimal SegundaNota { get; set; }

        public Decimal TerceiraNota { get; set; }

        public Decimal QuartaNota { get; set; }

        public Decimal Frequencia { get; set; }

        public Decimal PauMedia { get; set; }

        public Decimal PauExame { get; set; }

        public Decimal PauRecurso { get; set; }

        public Decimal PauExameOral { get; set; }

        public Decimal PauRecursoOral { get; set; }

        public Decimal PauExaEspecial { get; set; }

        public Decimal PauExaEspecialOral { get; set; }

        public Decimal PauNotaFinal { get; set; }

        public String PauNotaExtenso { get; set; }

        public Decimal PauMelhoria { get; set; }

        public String PauSituacao { get; set; }

        public String PauCurso { get; set; }

        public String PauDsRamo { get; set; }

        public String PauClasse { get; set; }


        public string Nivel { get; set; }

        public string Docente { get; set; }

        public string PauPeriodo { get; set; }

        public bool isValidada { get; set; }

        public PautaDTO()
        {

        }

        public PautaDTO(int pTurma, int pDisciplina)
        {
            Turma = pTurma;
            Disciplina = pDisciplina;
            AnoLectivo = -1;
            Aluno = -1;
            NomeAluno = "";
            Prova = -1;
        }

        public PautaDTO(int pAluno)
        {
            Turma = -1;
            Disciplina = -1;
            AnoLectivo = -1;
            Aluno = pAluno;
            NomeAluno = "";
            Prova = -1;
        }

        public PautaDTO(string pTurma, string pDisciplina, string pAnoLectivo, int pAluno, string pNomeAluno, int pProva)
        {
            // TODO: Complete member initialization
            Turma = Convert.ToInt32(pTurma);
            Disciplina = Convert.ToInt32(pDisciplina);
            AnoLectivo = Convert.ToInt32(pAnoLectivo);
            Aluno = pAluno;
            NomeAluno = pNomeAluno;
            Prova = pProva;

        }

        public string UserValidador { get; set; }

        public int Ano { get; set; }

        public Decimal NotaAvaliacaoContinua { get; set; }
    }

}
