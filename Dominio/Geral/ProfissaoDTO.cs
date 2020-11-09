using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class ProfissaoDTO:TabelaGeral
    {
        public ProfissaoDTO()
        {

        }

        public ProfissaoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public ProfissaoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public ProfissaoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public ProfissaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public ProfissaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
