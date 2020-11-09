using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.RecursosHumanos
{
    public class FolhaProcessamentoDTO:RetornoDTO
    {
        public int PeriodoProcessamentoId { get; set; }
        public string Referencia { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime DataFaltasExtra { get; set; }
        public int Mes { get; set; } 
        public string Periodo { get; set; } // MM-AAAA
        public int DiasProcessados { get; set; }
        public bool IsProcessada { get; set; }
        public string MotivoAnulacao { get; set; }
        public List<FolhaProcessamentoFuncionarioDTO> FuncionarioItens { get; set; }

    }
     

    public class FolhaProcessamentoFuncionarioDTO : RetornoDTO
    {
        public int FolhaProcessamentoId { get; set; }
        public FolhaProcessamentoDTO FolhaProcessamento { get; set; }
        public int FuncionarioId { get; set; }
        public FuncionarioDTO Funcionario { get; set; }
        public int TipoProcessamentoId { get; set; }
        public TipoProcessamentoDTO TipoProcessamento { get; set; }
        public int ModoProcessamentoId { get; set; }
        public ModoProcessamentoDTO ModoProcessamento { get; set; }
        public int PeriodoId { get; set; }
        public PeriodoProcessamentoDTO Periodo { get; set; }
        public decimal VencimentoBruto { get; set; }
        public decimal Descontos { get; set; } 
        public decimal TotalLiquido { get; set; }
        public bool IsReciboEmitido { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime InicioPeriodo { get; set; }
        public DateTime TerminoPeriodo { get; set; }
    } 
}
