using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class MoedaDTO:TabelaGeral
    {
        public MoedaDTO()
        {
            Codigo = int.MinValue;
            Descricao = string.Empty;
            Sigla = string.Empty;
            Estado = 1;
            Valor = 0;
            Data = DateTime.Today;
        }

        public MoedaDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public MoedaDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public MoedaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public MoedaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public MoedaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }


        public DateTime Data { get; set; }

        public decimal Valor { get; set; }
    }
}
