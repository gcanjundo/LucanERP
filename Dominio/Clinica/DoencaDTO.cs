using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class DoencaDTO:TabelaGeral
    {
       
        public DoencaDTO()
        {

        }

        public DoencaDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }


        public DoencaDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao; 
        }

        public DoencaDTO(string pDescricao, string pSigla, int pIndice, int pNroRegistos)
        {
            this.Codigo = 0;
            this.Descricao = pDescricao;
            IndicePagina = pIndice;
            this.Sigla = pSigla;
            RegistosPorPagina = pNroRegistos;
        }

        public DoencaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public DoencaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public DoencaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
