using Dominio.RecursosHumanos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class DocenteDTO:FuncionarioDTO
    {
         
        public bool Estado { get; set; } 
        public string DescricaoEstado { get; set; }

        public DocenteDTO() 
        {
        
        }

        public DocenteDTO(string pCodigo, string pNome)
        {
            this.Codigo = Convert.ToInt32(pCodigo);
            this.NomeCompleto = pNome == null ? string.Empty : pNome;
        }

        public DocenteDTO(int pCodigo)
        {
            // TODO: Complete member initialization
            Codigo = pCodigo;
        }

       
        public int AnoCurricular { get; set; }

        public int Turma { get; set; } 
        public int Disciplina { get; set; }
        public int ProvinciaMorada { get; set; } 
    }
}
