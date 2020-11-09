using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Geral
{
    public class PrinterDTO:TabelaGeral
    {
        public PrinterDTO()
        {
            
        }



        public PrinterDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public string AddressIP { get; set; }
        public string NetworkPath { get; set; }
        public string InternalName { get; set; }

        public PrinterDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public PrinterDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public PrinterDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public PrinterDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
