using Dominio.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class LaboratorioRequisicaoExameDTO : RetornoDTO
    {
        
        public int CustomerOrderID { get; set; }
        public int ProfissionalSolicitanteID { get; set; }
        public int PacientID { get; set; }
        public DateTime DataRequisicao { get; set; }
        public DateTime DataPrevisaoEntrega { get; set; }
        public DateTime DateEntrega  { get; set; }
        public DateTime InicioProcessamento { get; set; }
        public DateTime TerminoProcessamento { get; set; }
        public string Observacoes { get; set; }
        public string OtherRequesterDetails { get; set; }
        public int ConvenioID { get; set; }
        public string NroBeneficiario { get; set; }
        public bool Paid { get; set; }
        public decimal TotalExames { get; set; }
        public decimal TotalDesconto { get; set; }
        public decimal TotalUtente { get; set; }
        public decimal TotalEntidade { get; set; }
        public decimal TotalGeral { get; set; }
        public decimal TotalLquidado { get; set; } 
        public List<LaboratorioRequisicaoExameDetalhesDTO> RequisicaoExamesList { get; set; }
        public int Priority { get; set; }
    }

    public class LaboratorioRequisicaoExameDetalhesDTO:RetornoDTO
    {
        
        public int RequisicaoID { get; set; }
        public int ExameID { get; set; } 
        public int OrderItemID { get; set; }
        public DateTime PrevisionDeliveryDate { get; set; }
        public DateTime ProcessBeginDate { get; set; }
        public DateTime CollectionDate { get; set; }
        public int ProfessionalCollectionID { get; set; }
        public bool IsSelfCollection { get; set; }
        public int ProcessProfessionalID { get; set; }
        public string TechnicalNotes { get; set; }
        public decimal ResultReferenceValue { get; set; }
        public string Result { get; set; }
        public string ResultNotes { get; set; }
        public DateTime ResultDate { get; set; }
        public DateTime ProcessEndDate { get; set; }
        public string OtherNotes { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorUtente { get; set; }
        public decimal ValorEntidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
