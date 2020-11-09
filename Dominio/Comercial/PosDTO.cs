using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class PosDTO:TabelaGeral
    {

        public DateTime DataHoje { get; set; }
        public string Accao { get; set; }
        public int WarehouseID { get; set; }
        public int DocumentSerieID { get; set; } 
        public int CustomerDefault { get; set; }
        public int PaymentCondition { get; set; }
        public int DefaultDocument { get; set; }
        public int CashRefundDocumentID { get; set; }
        public int CashRefundSerieID { get; set; }
        public int CreditRefundDocumentID { get; set; }
        public int CreditRefundSerieID { get; set; }
        public int PriceTableID { get; set; }
        public bool PreventCloseWithSuspendSale { get; set; }
        public bool AllowCalendar { get; set; }
        public decimal FundoManeio { get; set; }
        public int PaymentMethodID { get; set; }
        public string PinCode { get; set; }
        public bool ForRest { get; set; }

        public PosDTO()
        {

        }

        public PosDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public PosDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public PosDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public PosDTO(string pUserName, string pCompanyID)
        {
             
            Descricao = pUserName;
            Filial = pCompanyID;
        }

        public PosDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public PosDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        public PosDTO(string pFelial, DateTime pDataHoje, string pUtilizador, string pAccao)
        {
            Filial = pFelial;
            DataHoje = pDataHoje;
            Utilizador = pUtilizador;
            Accao = pAccao;
        }
        public PosDTO(string pFelial, DateTime pDataHoje, string pAccao)
        {
            Filial = pFelial;
            DataHoje = pDataHoje;
            Accao = pAccao;
        }

         
    }
}
