using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.RecursosHumanos
{
    public class AbonoDescontoDTO:TabelaGeral
    {
        public string Categoria { get; set; }
        public bool IsCalculado { get; set; }

        public decimal Valor { get; set; }

        public string TipoValor { get; set; }
        public decimal ContribuicaoFuncionarioValor { get; set; }
        public decimal ContribuicaoFuncionarioPercentagem { get; set; }
        public decimal ContribuicaoEmpregadorValor { get; set; }
        public decimal ContribuicaoEmpregadorPercentagem { get; set; }
        public decimal ContribuicaoFuncionarioMinValor { get; set; }
        public decimal ContribuicaoFuncionarioMaxValor { get; set; }
        public decimal ContribuicaoEmpregadorMinValor { get; set; }
        public decimal ContribuicaoEmpregadorMaxValor { get; set; }
        public bool IsIRT { get; set; }
        public bool IsVencimento { get; set; }
        public bool IsINSS { get; set; }
        public bool IsSubsidioNatal { get; set; }
        public bool IsSubsidioFerias { get; set; }
        public bool IncideSalarioBase { get; set; }
        public bool DescontaIRT { get; set; }
        public bool DescontaINSS { get; set; }
        public int TipoProcessamentoId { get; set; }
    }
}
