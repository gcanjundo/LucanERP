using DataAccessLayer.Tesouraria;
using Dominio.Comercial;
using Dominio.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tesouraria
{
    public class MovimentoRN
    {
        private static MovimentoRN _instancia;
        private MovimentoDAO dao;
        private GenericRN _genericClass;
        public MovimentoRN()
        {
            dao = new MovimentoDAO();
            _genericClass = new GenericRN();
        }

        public static MovimentoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new MovimentoRN();
            }

            return _instancia;
        }

        public MovimentoDTO Adicionar(MovimentoDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public List<MovimentoDTO> GetPaymentInfo(MovimentoDTO dto)
        {
            return dao.ObterDadosPagamento(dto);
        }

        public List<MovimentoDTO> ObterRecebimentoDeClientes(MovimentoDTO dto)
        {
            return dao.ObterRecebimentoCliente(dto);
        }

        public List<MovimentoDTO> GetAnualSalesReceipts(MovimentoDTO dto)
        {

            var resumedList = new List<MovimentoDTO>();
            foreach (var month in ObterRecebimentoDeClientes(dto))
            {
                month.Descritivo = _genericClass.GetMonthName(month.NroOrdenacao);
                resumedList.Add(month);
            }
            return resumedList.OrderBy(t => t.NroOrdenacao).ToList();
        }

        public List<MovimentoDTO> GetDailySalesReceipts(MovimentoDTO dto)
        { 
            return dao.ObterRecebimentoDiario(dto);
        } 

        public List<MetodoPagamentoDTO> ObterResumoRecebimentoPorPaymentMethod(MovimentoDTO dto)
        {
            return dao.ObterResumoRecebimentoPorPaymentMethod(dto);
        }

        public List<MovimentoDTO> ObterPorFiltro(MovimentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public MovimentoDTO SaveMovimentoPeriodico(MovimentoDTO dto, List<MovimentoDTO> pPeriodicMovimentList)
        {
            var movimentHeader = dao.AddMovimentoPeriodico(dto); 
            foreach(var moviment in pPeriodicMovimentList)
            {
                moviment.PeriodicoID = movimentHeader.Codigo;
                Adicionar(moviment);
            }

            return movimentHeader;
        }

        public List<MovimentoDTO> ObterPeriodicos(MovimentoDTO dto)
        {
            return dao.ObterPeriodicos(dto);
        }

        public MovimentoDTO ObterPorPK(MovimentoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public MovimentoDTO AnularMovimento(MovimentoDTO dto)
        {
            string UserID = dto.Utilizador, Reasons = dto.Descritivo;
            dto = ObterPorPK(dto);

            if (dto.SerieID > 0)
            {
                dto.Utilizador = UserID;
                dto.Descritivo = Reasons;
                return dao.Anular(dto);
            }
            else
            {
                dto.Sucesso = false;
                if (dto.MensagemErro == string.Empty)
                     dto.MensagemErro = "alert('Apenas os Lançamentos de Directos de Débito ou Crédito podem ser anulados');";

                return dto;
            }
        }

        public Tuple<List<RubricaDTO>, List<RubricaDTO>> GetCashFlowList(MovimentoDTO dto)
        {
            List<RubricaDTO> ReceiptsList =new List<RubricaDTO>();
            List<RubricaDTO> PaymentsList = new List<RubricaDTO>();
            var lista = MovimentoRN.GetInstance().ObterPorFiltro(dto);
            var CreditsList = lista.Where(t=>t.Movimento=="E").ToList();
            var DebitsList = lista.Where(t => t.Movimento == "S").ToList();

            var RubricasList = RubricaRN.GetInstance().GetAllList();
            var GroupList = RubricasList.Where(t => t.RubricaID <= 0).ToList();

            var IncomeRubricaList = RubricasList.Where(t => (t.Movimento == "R" || t.Movimento == "E") && t.RubricaID <=0).ToList();
            var OutComeRubricasList = RubricasList.Where(t => (t.Movimento == "S" || t.Movimento == "D") && t.RubricaID <= 0).ToList();
            foreach (var rubrica in IncomeRubricaList)
            {
                var ItemList = RubricasList.Where(t => t.RubricaID == rubrica.Codigo).ToList();
                rubrica.LookupNumericField1 = CreditsList.Where(t=> t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 1).Sum(t => t.Valor);
                rubrica.LookupNumericField2 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 2).Sum(t => t.Valor);
                rubrica.LookupNumericField3 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 3).Sum(t => t.Valor);
                rubrica.LookupNumericField4 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 4).Sum(t => t.Valor);
                rubrica.LookupNumericField5 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 5).Sum(t => t.Valor);
                rubrica.LookupNumericField6 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 6).Sum(t => t.Valor);
                rubrica.LookupNumericField7 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 7).Sum(t => t.Valor);
                rubrica.LookupNumericField8 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 8).Sum(t => t.Valor);
                rubrica.LookupNumericField9 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 9).Sum(t => t.Valor);
                rubrica.LookupNumericField10 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 10).Sum(t => t.Valor);
                rubrica.LookupNumericField11 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 11).Sum(t => t.Valor);
                rubrica.LookupNumericField12 = CreditsList.Where(t => t.FluxoCaixa == rubrica.Codigo && t.DataTransacao.Month == 12).Sum(t => t.Valor);
                foreach(var subGroup in RubricasList.Where(t=>t.RubricaID == rubrica.Codigo).ToList())
                {
                    rubrica.LookupNumericField1 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 1).Sum(t => t.Valor);
                    rubrica.LookupNumericField2 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 2).Sum(t => t.Valor);
                    rubrica.LookupNumericField3 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 3).Sum(t => t.Valor);
                    rubrica.LookupNumericField4 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 4).Sum(t => t.Valor);
                    rubrica.LookupNumericField5 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 5).Sum(t => t.Valor);
                    rubrica.LookupNumericField6 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 6).Sum(t => t.Valor);
                    rubrica.LookupNumericField7 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 7).Sum(t => t.Valor);
                    rubrica.LookupNumericField8 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 8).Sum(t => t.Valor);
                    rubrica.LookupNumericField9 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 9).Sum(t => t.Valor);
                    rubrica.LookupNumericField10 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 10).Sum(t => t.Valor);
                    rubrica.LookupNumericField11 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 11).Sum(t => t.Valor);
                    rubrica.LookupNumericField12 += CreditsList.Where(t => t.FluxoCaixa == subGroup.Codigo && t.DataTransacao.Month == 12).Sum(t => t.Valor);

                    foreach (var subtItemGroup in RubricasList.Where(t => t.RubricaID == subGroup.Codigo).ToList())
                    {
                        rubrica.LookupNumericField1 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 1).Sum(t => t.Valor);
                        rubrica.LookupNumericField2 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 2).Sum(t => t.Valor);
                        rubrica.LookupNumericField3 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 3).Sum(t => t.Valor);
                        rubrica.LookupNumericField4 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 4).Sum(t => t.Valor);
                        rubrica.LookupNumericField5 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 5).Sum(t => t.Valor);
                        rubrica.LookupNumericField6 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 6).Sum(t => t.Valor);
                        rubrica.LookupNumericField7 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 7).Sum(t => t.Valor);
                        rubrica.LookupNumericField8 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 8).Sum(t => t.Valor);
                        rubrica.LookupNumericField9 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 9).Sum(t => t.Valor);
                        rubrica.LookupNumericField10 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 10).Sum(t => t.Valor);
                        rubrica.LookupNumericField11 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 11).Sum(t => t.Valor);
                        rubrica.LookupNumericField12 += CreditsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 12).Sum(t => t.Valor);
                    }
                }

                ReceiptsList.Add(rubrica); 
            }



            foreach (var rubricaOUT in OutComeRubricasList)
            {
                var ItemList = RubricasList.Where(t => t.RubricaID == rubricaOUT.Codigo).ToList();
                rubricaOUT.LookupNumericField1 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 1).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField2 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 2).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField3 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 3).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField4 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 4).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField5 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 5).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField6 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 6).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField7 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 7).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField8 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 8).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField9 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 9).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField10 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 10).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField11 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 11).Sum(t => t.Valor);
                rubricaOUT.LookupNumericField12 = DebitsList.Where(t => t.FluxoCaixa == rubricaOUT.Codigo && t.DataTransacao.Month == 12).Sum(t => t.Valor);
                foreach (var subGroupOUT in RubricasList.Where(t => t.RubricaID == rubricaOUT.Codigo).ToList())
                {
                    rubricaOUT.LookupNumericField1 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 1).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField2 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 2).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField3 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 3).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField4 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 4).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField5 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 5).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField6 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 6).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField7 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 7).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField8 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 8).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField9 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 9).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField10 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 10).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField11 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 11).Sum(t => t.Valor);
                    rubricaOUT.LookupNumericField12 += DebitsList.Where(t => t.FluxoCaixa == subGroupOUT.Codigo && t.DataTransacao.Month == 12).Sum(t => t.Valor);

                    foreach (var subtItemGroup in RubricasList.Where(t => t.RubricaID == subGroupOUT.Codigo).ToList())
                    {
                        rubricaOUT.LookupNumericField1 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 1).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField2 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 2).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField3 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 3).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField4 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 4).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField5 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 5).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField6 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 6).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField7 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 7).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField8 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 8).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField9 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 9).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField10 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 10).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField11 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 11).Sum(t => t.Valor);
                        rubricaOUT.LookupNumericField12 += DebitsList.Where(t => t.FluxoCaixa == subtItemGroup.Codigo && t.DataTransacao.Month == 12).Sum(t => t.Valor);
                    }
                }
                PaymentsList.Add(rubricaOUT);
            }

            return new Tuple<List<RubricaDTO>, List<RubricaDTO>>(ReceiptsList, PaymentsList);   
        }
    }
}
