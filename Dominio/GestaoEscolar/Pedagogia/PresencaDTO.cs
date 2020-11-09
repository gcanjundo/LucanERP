using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class PresencaDTO:RetornoDTO
    {
        public PresencaDTO()
        {

        }

        public PresencaDTO(int pAula, int pMatricula, int pPresenca, string pUtilizador)
        {
            AulaID = pAula;
            MatriculaID = pMatricula;
            Status = pPresenca;
            Utilizador = pUtilizador;
        }

        public PresencaDTO(int pOrdem, int pAulaID, int pMatricula, int pPresenca, string pInscricao, string pNome, int pAlunoID, AulaDTO pAula)
        {
            AulaID = pAulaID;
            MatriculaID = pMatricula;
            Status = pPresenca;
            OrderNumber = pOrdem; 
            AlunoID = pAlunoID;
            NroInscricao = pInscricao;
            SocialName = pNome; 
            Aula = pAula; 
        }


        public int AulaID { get; set; }
        public int MatriculaID { get; set; } 
        public decimal NotaAvaliacao { get; set; } // Avaliação Contínua

        public int AlunoID { get; set; }
        public string NroInscricao { get; set; }
        public AulaDTO Aula { get; set; }
        public DateTime DataIni { get; set; }
        public DateTime DataTerm { get; set; }
    }
}
