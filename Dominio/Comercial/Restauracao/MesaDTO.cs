using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Restauracao
{
    public class MesaDTO:TabelaGeral
    {
        
        public MesaDTO()
        {

        }

        public MesaDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public MesaDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public MesaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public MesaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public MesaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pLocalizacao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Localizacao = pLocalizacao;
        }

        public MesaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        public string Localizacao { get; set; }
        public int Lugares { get; set; } 
        public int CustomerID { get; set; }
        public DateTime BookingDate { get; set; }
        public string Observacao { get; set; }
        public int BookingID { get; set; }

    }
}
