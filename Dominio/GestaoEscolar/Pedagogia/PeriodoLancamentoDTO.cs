using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class PeriodoLancamentoDTO:RetornoDTO
    {
        
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int PeriodoLectivoID { get; set; }
        public bool IsPeriodoExtra { get; set; }
        public bool IsDeleted { get; set; }
        public int ExameID { get; set; }
    }
}
