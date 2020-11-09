using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Comercial
{
    public class MetodoPagamentoDTO:TabelaGeral
    {
        public MetodoPagamentoDTO()
        {

        }

        public string PaymentMode { get; set; }
        public decimal TotalLinha { get; set; }

        public DateTime Periodo { get; set; }

        public MetodoPagamentoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public MetodoPagamentoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public MetodoPagamentoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public MetodoPagamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public MetodoPagamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        public int POSVisible { get; set; }
        public string DescricaoPagamento { get; set; }
        public string Icon { get; set; }
    }


}
