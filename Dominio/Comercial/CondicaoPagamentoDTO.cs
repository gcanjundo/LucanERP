using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class CondicaoPagamentoDTO:TabelaGeral
    {
         public CondicaoPagamentoDTO()
        {

        }

        public CondicaoPagamentoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public CondicaoPagamentoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public CondicaoPagamentoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public CondicaoPagamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public CondicaoPagamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }


        public decimal DescontoFinaceiro { get; set; }

        public int Vencimento { get; set; }

        public int Periodicidade { get; set; }

        public string Pagamento { get; set; }

        public decimal NroPrestacoes { get; set; }

        public decimal EntradaInicial { get; set; }
    }
}
