using Dominio.Contabilidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Tesouraria
{
    public class RubricaDTO:RetornoDTO
    {
        
        public int RubricaID { get; set; }
        public string Classificacao { get; set; }
        
        public string Natureza { get; set; }
        public int PlanGeralContaID { get; set; }
        public string Movimento { get; set; } // D - Despesas: R-Receitas
        public int Agrupamento { get; set; }
        public bool IncideDRE { get; set; }
        public string Destino { get; set; }
        public bool IsFixa { get; set; }
        public decimal LookupNumericField12 { get; set; }

        public RubricaDTO()
        {
            Designacao = string.Empty;
        }

        public RubricaDTO(int pCodigo, string pDesignation)
        {
            Codigo = pCodigo;
            Designacao = pDesignation;
        }
    }

    public class MovimentoPlanoContaDTO:RetornoDTO
    {
        public int FluxoCaixaID { get; set; }
        public int PlanoContaID { get; set; }
        public PlanoContaDTO PlanoConta { get; set; }
    }
}
