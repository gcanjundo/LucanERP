using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Restauracao
{
    public class AtendimentoItemDTO : RetornoDTO
    {
        
        public int Atendimento { get; set; }
        public int Artigo { get; set; } 
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        public string Solicitante { get; set; }
        public DateTime DataLancamento { get; set; } 
        public string Notes { get; set; }
        public object OrderID { get; set; }
        public String PrinterDestiny { get; set; } 
        public bool Saved { get; set; }
        public bool MoveStock { get; set; }
        public bool Deleted { get; set; }
        public string DeleteNotes { get; set; }
        public int Cooker { get; set; }
        public decimal DescontoLinha { get; set; }
        public string Prioridade { get; set; }
        public decimal ValorImposto { get; set; }
        public int TaxID { get; set; }
        public bool IsPaid { get; set; }
        public decimal TaxValue { get; set; }
        public decimal DiscountValue { get; set; } 

        public AtendimentoItemDTO()
        {
            Codigo = -1;
            Atendimento = -1;
            Artigo = -1;
            Quantidade = 0;
            Preco = 0;
            Total = 0;
            Solicitante = string.Empty;
            DataLancamento = DateTime.MinValue;
            Filial = string.Empty;
            OrderID = 0;
            Notes = string.Empty;
            Deleted = false;
            Saved = false;
            MoveStock = false;
            DeleteNotes = string.Empty;
            Cooker = -1;
            Situacao = string.Empty;
        }

        public AtendimentoItemDTO(int pCodigo)
        {
            Codigo = pCodigo;
            Atendimento = -1;
            Artigo = -1;
            Quantidade = 0;
            Preco = 0;
            Total = 0;
            Solicitante = string.Empty;
            DataLancamento = DateTime.MinValue;
            Filial = string.Empty;
        }

        public AtendimentoItemDTO(int pCodigo, int pAtendimento, int pProduto, decimal pQuantidade, decimal pPreco, decimal pTotal, string pUtilizador, DateTime pDataLancamento, string pFilial)
        {
            Codigo = pCodigo;
            Atendimento = pAtendimento;
            Artigo = pProduto;
            Quantidade = pQuantidade;
            Preco = pPreco;
            Total = pTotal;
            Solicitante = pUtilizador;
            DataLancamento = pDataLancamento;
            Filial = pFilial;
        }


    }
}
