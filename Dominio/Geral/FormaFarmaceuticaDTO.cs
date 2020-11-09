using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class FormaFarmaceuticaDTO:TabelaGeral
    {
        public FormaFarmaceuticaDTO()
        {

        }

        public FormaFarmaceuticaDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public FormaFarmaceuticaDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public FormaFarmaceuticaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public FormaFarmaceuticaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public FormaFarmaceuticaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
