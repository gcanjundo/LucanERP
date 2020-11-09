using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class CategoriaDTO:TabelaGeral
    {
        public string Categoria { get; set; }
        public int TablePriceID { get; set; }
        public int PaymentTermsID { get; set; }
        public decimal LimiteCredito { get; set; }
        public int PromocaoID { get; set; }
        public decimal ValorDesconto { get; set; }
        public bool IsArtigoRest { get; set; }

        public CategoriaDTO()
        {

        }

        public CategoriaDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public CategoriaDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public CategoriaDTO(int pCodigo, string pDescricao, string pSigla, string pCategoria)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Categoria = pCategoria;
        }

        public CategoriaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pCategoria)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Categoria = pCategoria;
        }

        public CategoriaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pCategoria, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
            Categoria = pCategoria;
        }

         
    }
}
