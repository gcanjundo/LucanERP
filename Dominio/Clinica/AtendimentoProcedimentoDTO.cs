using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class AtendimentoProcedimentoDTO:RetornoDTO
    {
        
        public int Atendimento { get; set; }
        public int Procedimento { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorLiquidado { get; set; }
        public decimal ValorTotal { get; set; }
        public string ItemDesignation { get; set; }

        public AtendimentoProcedimentoDTO() 
        {
 
        }

        public AtendimentoProcedimentoDTO(int pAtendimento)
        {

        }

        public AtendimentoProcedimentoDTO(int pCodigo, int pAtendimento, int pProcedimento, decimal pPrecoUnitario, decimal pQuantidade, decimal pValorLiquidado, decimal pValorTotal, int pStatus)
        {
            Codigo = pCodigo;
            Atendimento = pAtendimento;
            Procedimento = pProcedimento;
            PrecoUnitario = pPrecoUnitario;
            Quantidade = pQuantidade;
            ValorTotal = pValorTotal;
            ValorLiquidado = pValorLiquidado;
            Status = pStatus;
        }

    }
}
