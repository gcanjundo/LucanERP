using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class QueixaDTO:TabelaGeral
    {
        public QueixaDTO()
        {

        }

        public QueixaDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public QueixaDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public QueixaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public QueixaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public QueixaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
        }


        

        public string CID { get; set; }

        public string Inclusao { get; set; }

        public string Exclusao { get; set; }
    }
}
