using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Geral
{
    public class MarcaDTO: TabelaGeral
    {
        
        public string Localizacao { get; set; }

        public MarcaDTO()
        {

        }

        public MarcaDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public MarcaDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public MarcaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public MarcaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public MarcaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        public MarcaDTO(int pCodigo, string pDescricao, string pSigla, int pStatus, string pParent)
        {
            // TODO: Complete member initialization
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pStatus;
            Localizacao = pParent;
        }

        
    }
}
