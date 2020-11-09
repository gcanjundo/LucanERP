using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;

namespace Dominio.GestaoEscolar.Faturacao
{   

    public class MensalidadeAlunoDTO:RetornoDTO
    {
        public MensalidadeAlunoDTO(int pCodigo)
        { 
            Codigo = pCodigo;
        }

        public MensalidadeAlunoDTO()
        {

        }

        public MensalidadeAlunoDTO(MatriculaDTO pMatricula)
        {
            Matricula = pMatricula;
        } 
         
        public MatriculaDTO Matricula { get; set; } 
        public ParcelaMensalidadeDTO Parcela { get; set; } 

        public int Fatura { get; set; } 

        public DateTime DataCobranca { get; set; } 

        public String Multa { get; set; } 
        
        public String CalularMulta { get; set; }

        public decimal Valor { get; set; } 

        public string OrigemMulta { get; set; } 
        public int Quantidade { get; set; } 
        public string Classe { get; set; } 
        public decimal ValorLiquidado { get; set; }
        public decimal ValorDebito { get; set; }
        public decimal ValorMulta { get; set; }
        public decimal ValorDesconto { get; set; } 
        public object Filtro { get; set; }
        public bool IsFirstMonth { get; set; }
        public bool IsLastMonth { get; set; }
        public string ReferenciaContabil { get; set; }
        public decimal PercentagemMulta { get; set; }
        public decimal PercentagemDesconto { get; set; }
        public string MotivoMulta { get; set; }
        public string MotivoDesconto { get; set; }
        public string MotivoNegociacao { get; set; }
        public int BolsaID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DataLiquidacao { get; set; }
        public decimal ComercialDiscount { get; set; }
        public decimal PercentualComercialDiscount { get; set; }


    }

    
}
