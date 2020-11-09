using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class CoordenacaoDTO:TabelaGeral
    {
        public CoordenacaoDTO()
        {

        }

        public CoordenacaoDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public CoordenacaoDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public CoordenacaoDTO(int pCodigo, string pDescricao, string pSigla, string pFilial)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            Filial = pFilial;
        }

        public CoordenacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public CoordenacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
        }


        public string Nivel { get; set; } 
    }
}
