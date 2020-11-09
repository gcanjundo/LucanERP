using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class ArmazemDTO : TabelaGeral
    {
        public double Quantidade { get; set; }

        public ArmazemDTO()
        {

        }

        public ArmazemDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public ArmazemDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }


        public ArmazemDTO(int pCodigo, string pDescricao, double pQuantidade)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Quantidade = pQuantidade;
        }
        public ArmazemDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public ArmazemDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pFilial)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Filial = pFilial;
        }

        public ArmazemDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }


        public bool AlertaStockMinimo { get; set; }

        public bool PermiteStockNegativo { get; set; }

        public bool AlertaStockNegativo { get; set; }

        public string Tipo { get; set; }

        public int AllowInsert { get; set; }
        public int AllowUpdate { get; set; }
        public int AllowDelete { get; set; }
        public int AllowPrint { get; set; }
        public int AllowSelect { get; set; }
        public int UtilizadoID { get; set; }
        public int PerfilID { get; set; }
        public bool EnablePOS { get; set; }
        public bool AllowOutcome { get; set; }
        public bool AllowIncome { get; set; }
        public bool IsForRest { get; set; }
        public int TablePriceID { get; set; }
    }


     

}
