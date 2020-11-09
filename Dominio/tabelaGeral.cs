using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class TabelaGeral: RetornoDTO
    {     
        public string Sigla { get; set; } 
        public int Estado { get; set; } 
        public int IndicePagina { get; set; }
        public int RegistosPorPagina { get; set; } 
        public string Operacao { get; set; }
    }
}
