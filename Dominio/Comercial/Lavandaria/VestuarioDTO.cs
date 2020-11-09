using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Lavandaria
{
  
        public class VestuarioDTO : TabelaGeral
        {
        public int GeneroID { get; set; }
        public int NroItems { get; set; }
        public string Unidade { get; set; }
        public string ImagePath { get; set; }
        public string ImageBlob { get; set; }



        public VestuarioDTO()
        {

        }

        public VestuarioDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public VestuarioDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public VestuarioDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            
        }

        public VestuarioDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public VestuarioDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Sucesso = pSucesso;
            MensagemErro = pMensagem;
        }


    }
    

}
