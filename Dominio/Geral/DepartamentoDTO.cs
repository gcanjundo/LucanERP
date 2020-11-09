using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class DepartamentoDTO: TabelaGeral
    {

         
        public string Seccao{ get; set; } 
        public string Classificacao { get; set; }
        public DepartamentoDTO()
        {

        }

        public DepartamentoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public DepartamentoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public DepartamentoDTO(int pCodigo, string pDescricao, string pSigla, string pSeccao, string pClassificacao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Seccao = pSeccao;
            Classificacao = pClassificacao;
        }

        public DepartamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pSeccao, string pClassificacao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Seccao = pSeccao;
            Classificacao = pClassificacao;
        }

        public DepartamentoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pSeccao, string pClassificacao, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Seccao = pSeccao;
            Classificacao = pClassificacao;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        public string NomePai { get; set; }
    }

     
}
