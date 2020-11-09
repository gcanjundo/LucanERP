using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class PrioridadeDTO:TabelaGeral
    {
        public int TempoEspera { get; set; }
        public PrioridadeDTO()
        {

        }

        public PrioridadeDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public PrioridadeDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public PrioridadeDTO(int pCodigo, string pDescricao, string pSigla, int pEspera)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            TempoEspera = pEspera;
        }

        public PrioridadeDTO(int pCodigo, string pDescricao, string pSigla, int pEspera, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            TempoEspera = pEspera;
        }

        public PrioridadeDTO(int pCodigo, string pDescricao, string pSigla, int pEspera, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
            TempoEspera = pEspera;
        }
    }
}
