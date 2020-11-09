using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class EstagioAlunoDTO: RetornoDTO
    {
        public int Ordem { get; set; }
        public int EstagioID { get; set; }
        public int MatriculaID { get; set; }
        public int AlunoID { get; set; }
        public string NroInscricao { get; set; }
        public string NroProcesso { get; set; }
        public string Curso { get; set; }
        public decimal Teoria { get; set; }
        public decimal Pratica { get; set; }
        public decimal NotaFinal { get; set; }
        public string Observacoes { get; set; } 
        public int PaymentID { get; set; }
        public int TurmaID { get; set; }

        public EstagioAlunoDTO()
        {

        }

        public EstagioAlunoDTO(int pEstagioID)
        {
            EstagioID = pEstagioID;
        }

         
    }
}
