using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class TipoAcomodacaoDTO: TabelaGeral
    {
         public TipoAcomodacaoDTO()
        {

        }

        public TipoAcomodacaoDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public TipoAcomodacaoDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public TipoAcomodacaoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public TipoAcomodacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public TipoAcomodacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
        }
    }
}
