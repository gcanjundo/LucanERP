using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.RecursosHumanos
{
    public  class EscaloesIRTDTO: RetornoDTO
    {
        
        public decimal  SalarioMinimo { get; set; } 
        public decimal  SalarioMaximo { get; set; } 
        public decimal ValorMinimoDesconto { get; set; } 
        public decimal  PercentualDesconto { get; set; }
        public decimal ValorExcesso { get; set; }
        public decimal ValorDescontar { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public EscaloesIRTDTO()
        {
            Codigo = 0;
            SalarioMinimo = 0;
            SalarioMaximo =0;
            ValorMinimoDesconto =0;
            PercentualDesconto = 0;
        }

        public EscaloesIRTDTO(int pCodigo, decimal pValorMinimo, decimal pValorMaximo, decimal pValorMinDesconto, decimal pPercentagemDesconto)
        {
            Codigo = pCodigo;
            SalarioMinimo = pValorMinimo;
            SalarioMaximo = pValorMaximo;
            ValorMinimoDesconto = pValorMinDesconto;
            PercentualDesconto = pPercentagemDesconto;
        }
    }
}
