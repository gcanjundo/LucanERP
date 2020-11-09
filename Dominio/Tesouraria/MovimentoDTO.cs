using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Tesouraria
{
    public class MovimentoDTO: RetornoDTO
    {
        public MovimentoDTO()
        {
            DocumentType = -1;
            DocumentID = -1; 
            ContaCorrente = "-1";
            MetodoPagamento = -1;// int.Parse(ddlSearchPaymentMethod.SelectedValue);
            Documento = -1;// int.Parse(ddlSearchDocumento.SelectedValue);
            SerieID = -1;// int.Parse(ddlSearchSerie.SelectedValue);
            DesignacaoEntidade = string.Empty; // txtSearchEntidade.Text;
            DocumentReference = string.Empty;
            RefComprovantePagto = string.Empty;
            FluxoCaixa = -1;// int.Parse(ddlSearchRubrica.SelectedValue);
            DataIni = string.Empty;
            DataTerm = string.Empty;
            Movimento = "-1";
            Moeda = -1;
            Entidade = -1;
            Descritivo = string.Empty;
            Observacoes = "";

        }

        
        public int NumeroMovimento { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataTransacao { get; set; }
        public int Moeda { get; set; }
        public string Movimento { get; set; }
        public int MetodoPagamento { get; set; } 
        public string Descritivo { get; set; }
        public int FluxoCaixa { get; set; }
        public string ContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string Observacoes { get; set; }
        public int Documento { get; set; }
        public string RefComprovantePagto { get; set; } 
        public int Terminal { get; set; } 
        public int DocumentType { get; set; } 
        public int DocumentID { get; set; }
        public int CentroCustoID { get; set; }
        public string Rubrica { get; set; } 
        public string ContaTransferenciaDestino { get; set; }
        public string ContaTransferenciaOrigem { get; set; }
        public string DataIni { get; set; } 
        public string DataTerm { get; set; }
        public int BancoID { get; set; }
        public int SerieID { get; set; }
        public bool IsConciled { get; set; }
        public string DocumentReference { get; set; }
        public string PaymentMethodDesignation { get; set; }
        public int Periodicidade { get; set; }
        public int Dias { get; set; }
        public bool IsReal { get; set; }
        public decimal Cambio { get; set; }
        public int PeriodicoID { get; set; } 
        public decimal Saldo { get; set; } 
    }
    
}
