using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Comercial.Restauracao
{
    public class LocalizacaoDTO:TabelaGeral
    {
        public LocalizacaoDTO()
        {
            Descricao = String.Empty;
        }

        public LocalizacaoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public LocalizacaoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public LocalizacaoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public LocalizacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public LocalizacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
