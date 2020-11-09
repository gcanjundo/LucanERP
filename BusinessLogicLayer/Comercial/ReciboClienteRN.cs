using BusinessLogicLayer.Tesouraria;
using DataAccessLayer.Comercial;
using Dominio.Comercial;
using Dominio.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{
    public class ReciboClienteRN
    {
         private static ReciboClienteRN _instancia;
        private ReciboDAO dao;
         

        public ReciboClienteRN()
        {
            dao = new ReciboDAO(); 
        }

        public static ReciboClienteRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ReciboClienteRN();
            }

            return _instancia;
        }

        public ReciboDTO GenerateReceipt(ReciboDTO pReceiptData, List<FaturaDTO> pPenddingList, object pPaymentData, string pTransaction)
        {
            
            pReceiptData.DocumentosLiquidados = new List<ReciboDocumentosDTO>();

            foreach (var document in pPenddingList)
            {
                var doc = new ReciboDocumentosDTO
                {
                    Activo = true,
                    Ordem = 1,
                    ReciboID = -1,
                    Documento = document.Documento,
                    DocumentID = document.Codigo,
                    Numeracao = document.Numeracao,
                    Referencia = document.Referencia,
                    ValorTotal = document.ValorTotal,
                    ValorPago = document.ValorFaturado,

                    DescontoEntidade = 0,
                    Troco = 0,
                    Utilizador = pReceiptData.Utilizador,
                    Saldo = document.Saldo - document.ValorPago
                };

                doc.ValorPago = doc.ValorPago < 0 ? doc.ValorPago * (-1) : doc.ValorPago;
                doc.ValorTotal = doc.ValorTotal < 0 ? doc.ValorTotal * (-1) : doc.ValorTotal;
                doc.Saldo = document.Saldo <= 0 ? 0 : document.Saldo;
                pReceiptData.DocumentosLiquidados.Add(doc);
            }

            return Salvar(pReceiptData, pPaymentData, pTransaction);
        }

        private ReciboDTO Salvar(ReciboDTO dto, object PaymentReceivedList, string pTransaction)
        {
            var InvoicesList = dto.DocumentosLiquidados.Where(t => t.ValorPago > 0).ToList();
            var EntityID = dto.Entidade;
            var UserName = dto.Utilizador;
            dto = dao.Adicionar(dto);
            dto.Entidade = EntityID;
            if (dto.Sucesso)
            {
                foreach (var documento in InvoicesList)
                {
                    documento.ReciboID = dto.Codigo;
                    dao.LiquidarDocumento(documento);
                }

                if (PaymentReceivedList != null)
                {

                    List<PagamentoDTO> pagtos = (List<PagamentoDTO>)PaymentReceivedList;
                    MovimentoDTO transacao;
                    foreach (var item in pagtos)
                    {
                        transacao = new MovimentoDTO();
                        transacao.ContaCorrente = item.Account;
                        transacao.DataTransacao = item.PaymentDate != DateTime.MinValue ? item.PaymentDate : DateTime.Now;
                        transacao.DataLancamento = DateTime.Now;
                        transacao.Moeda = int.Parse(dto.Moeda);
                        transacao.Movimento = pTransaction;
                        transacao.MetodoPagamento = item.PaymentMethod;
                        transacao.Utilizador = UserName;
                        transacao.Filial = dto.Filial;
                        transacao.Descritivo = dto.Referencia;
                        transacao.FluxoCaixa = -1;
                        transacao.Valor = item.Value;
                        transacao.Entidade = dto.Entidade;
                        transacao.Observacoes = "";
                        transacao.RefComprovantePagto = item.DocumentNumber;
                        transacao.Documento = -1;
                        transacao.Terminal = dto.SessionID >= 0 ? dto.SessionID : -1;
                        transacao.DocumentType = dto.Documento;
                        transacao.DocumentID = dto.Codigo;
                        MovimentoRN.GetInstance().Adicionar(transacao);
                    }
                }
            }
           return dto;
        }

        public ReciboDTO Anular(ReciboDTO dto)
        {
             
            return dao.Excluir(dto);
        }

        public List<ReciboDTO> ObterPorFiltro(ReciboDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public ReciboDTO ObterPorPK(ReciboDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public Tuple<List<ReciboDocumentosDTO>, List<FaturaDTO>> ObterDocumentosRelacionados(ReciboDocumentosDTO dto)
        {
            return dao.ObterDocumentos(dto);
        }

        public bool IsRegularizacaoContaCorrente(DocumentoComercialDTO dto)
        {
            return dto!=null && dto.ContaCorrente == "R" ? true : false;
        }
    }
}
