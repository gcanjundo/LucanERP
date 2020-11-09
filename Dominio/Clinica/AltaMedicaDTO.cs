using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class AltaMedicaDTO:TabelaGeral
    {
        public AltaMedicaDTO()
        {

        }

        public AltaMedicaDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public AltaMedicaDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public AltaMedicaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public AltaMedicaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public AltaMedicaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
