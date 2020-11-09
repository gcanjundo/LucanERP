using Dominio.GestaoEscolar.Pedagogia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Faturacao
{
    public class BolseiroDTO:RetornoDTO
    {

        public BolseiroDTO()
        {
            // TODO: Complete member initialization
         
        }
        public BolseiroDTO(int pAluno)
        {
            // TODO: Complete member initialization
            Aluno = pAluno.ToString();
        }

        public BolseiroDTO(int pAluno, int pAno)
        {
            // TODO: Complete member initialization
            Aluno = pAluno.ToString();
            AnoLectivo = pAno;
        }

        public string Aluno { get; set; }
        public string Bolsa { get; set; }
        public DateTime Inicio { get; set; }
        public string Adesao { get; set; }
        public string Estado { get; set; }
    }
}
