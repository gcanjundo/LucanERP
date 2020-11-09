using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clinica
{
    public class LaboratorioExameFaixaEtariaDTO:TabelaGeral
    {
        public string Sexo { get; set; }
        public int IdadeInicial { get; set; }
        public int IdadeFinal { get; set; }
        public string UnidadeFaixa { get; set; } 
    }
}
