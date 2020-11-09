using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class SerieDTO:TabelaGeral
    {
        public int Ano { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int Documento { get; set; }
        public int Numeracao { get; set; }
        public int Copias { get; set; }
        public bool AllowFreeDocument { get; set; }
        public string RegimeFiscal { get; set; }
        public int CustomerID { get; set; }



        public SerieDTO()
        {

        }

        public SerieDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public SerieDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public SerieDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public SerieDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pFilial, int pAno, int pDocumento)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Filial = pFilial;
            Ano = pAno;
            Documento = pDocumento;
        }

        public SerieDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        public SerieDTO(int pDocumentID, int pAno, string pDescricao, string pFilial)
        {
            Documento = pDocumentID;
            Descricao = pDescricao;
            Ano = pAno;
            Filial = pFilial;
        }


       
    }
}
