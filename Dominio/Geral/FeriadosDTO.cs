using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class FeriadosDTO:TabelaGeral
    {
        private string _dia = "";

        public string Dia
        {
            get { return _dia; }
            set { _dia = value; }
        }

        public FeriadosDTO()
        {

        }

        public FeriadosDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public FeriadosDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public FeriadosDTO(int pCodigo, string pDescricao, string pSigla, string pDia)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Dia = pDia;
        }

        public FeriadosDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pDia)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Dia = pDia;
        }

        public FeriadosDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pDia, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Dia = pDia;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }
    }
}
