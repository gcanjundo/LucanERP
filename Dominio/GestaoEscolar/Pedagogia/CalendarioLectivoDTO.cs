using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class CalendarioLectivoDTO:RetornoDTO
    {  
        public int PeriodoId { get; set; }

        public DateTime Inicio { get; set; } 
        public DateTime Termino { get; set; } 
          
        public bool IsPausa { get; set; }

        public int Avaliacao { get; set; }

        public DateTime InicioPeriodo { get; set; }

        public DateTime TerminoPeriodo { get; set; }
        public PeriodoLectivoDTO Periodo { get; set; }



        public CalendarioLectivoDTO()
        {
        
        }

        public CalendarioLectivoDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
        }

        public CalendarioLectivoDTO(int pPeriodo, string pEvento)
        {
            PeriodoId = pPeriodo;
            Descricao = pEvento;
        }
        

        
    }
}
