using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.RecursosHumanos
{
    public class PeriodoProcessamentoDTO:TabelaGeral
    {
         public PeriodoProcessamentoDTO()
        {

        }

        public PeriodoProcessamentoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public PeriodoProcessamentoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public PeriodoProcessamentoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public PeriodoProcessamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public PeriodoProcessamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }
    }
}
