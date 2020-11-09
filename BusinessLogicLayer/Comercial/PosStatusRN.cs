using DataAccessLayer.Comercial;
using Dominio.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Comercial 
{ 
    public class PosStatusRN
    {
        private static PosStatusRN _instancia;
        private PosStatusDAO dao;

        public PosStatusRN()
        {
            dao = new PosStatusDAO();
        }

        public static PosStatusRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PosStatusRN();
            }

            return _instancia;
        }

        public PosStatusDTO Salvar(PosStatusDTO dto)
        {  
            return dao.Adicionar(dto); 
        }

        public PosStatusDTO GetOpenPOS(PosStatusDTO dto)
        {
            var SessionList = dao.GetMyOpenSessionRegisterList(dto);

             
            if (SessionList.Count > 1)
            {
                dto.MensagemErro = "O Utilizador tem mais de uma sessão aberta.";
            }
            else  
            {
                if(SessionList!=null && SessionList.Count == 1)
                {
                    dto = SessionList[0];
                }
            }

            return dto;
        }

        public PosStatusDTO ObterCaixa(PosStatusDTO dto)
        {

            return dao.ObterPorPK(dto);
        }

        public List<PosStatusDTO> ObterTodasSessosesPorFilial(PosStatusDTO dto)
        {    
            return dao.ObterTodasSessoesPorFilial(dto);
        }

        public decimal SomatorioValorInicial(FaturaDTO dto)
        {
            PosStatusDTO pos = new PosStatusDTO
            {
                Filial = dto.Filial
            };

            List<PosStatusDTO> lista = dao.ObterPorFiltro(pos).Where(t=>t.Data >= dto.Inicio && t.Data <= dto.Termino).ToList();
            if (dto.Codigo > 0)
            {
                lista = lista.Where(t => t.Codigo == dto.Codigo).ToList();
            }
            return lista.Sum(t => t.SaldoInicial);
        }

        public bool IsPeriodoProrrogativo(DateTime DataCaixaAbertura)
        {  
            if (DataCaixaAbertura != DateTime.MinValue && DataCaixaAbertura < DateTime.Today)
            {
                var DiferenceTime = DateTime.Now - DataCaixaAbertura;

                if ((int)DiferenceTime.TotalHours <= 36)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<PosStatusDTO> ObterSessoesCaixa(PosStatusDTO dto) {

            var CashRegisterSessions = ObterTodasSessosesPorFilial(dto);

            if(dto.POS > 0)
            {
                CashRegisterSessions = CashRegisterSessions.Where(t => t.POS == dto.POS).ToList();
            }

            if(dto.Data != DateTime.MinValue)
            {  

                CashRegisterSessions = CashRegisterSessions.Where(t => dto.Data == DateTime.Today ? t.Data >= DateTime.Today.AddDays(-5) :  t.Data >= dto.Data).ToList();
            }
             

            if(dto.Status == 0)
            {
                CashRegisterSessions = CashRegisterSessions.Where(t => t.Fecho != DateTime.MinValue).ToList();
            }else if (dto.Status == 1)
            {
                CashRegisterSessions = CashRegisterSessions.Where(t => t.Fecho == DateTime.MinValue).ToList();
            } 

            return CashRegisterSessions;
        }

        
    }
}
