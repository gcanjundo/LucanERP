using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class AcomodacaoDTO:TabelaGeral
    {
        
        public string Extensao { get; set; } 
        public int Tipo { get; set; } 
        public string DescricaoTipo { get; set; }
        public AcomodacaoDTO()
        {

        }

        public AcomodacaoDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public AcomodacaoDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public AcomodacaoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public AcomodacaoDTO(string pDescricao, int pTipo)
        {
            this.Codigo = 0;
            this.Descricao = pDescricao;
            this.Tipo = pTipo;
            this.Sigla = "";
        }

        public AcomodacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pExtensao, int pTipo)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.Extensao = pExtensao;
            this.Tipo = pTipo;
        }

        public AcomodacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pExtensao, int pTipo, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
            this.Extensao = pExtensao;
            this.Tipo = pTipo;
        }
        

    }
}
