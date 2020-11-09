using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class StatusRN 
    {
        private static StatusRN _instancia;

        private StatusDAO dao;

        public StatusRN()
        {
          dao = new StatusDAO();
        }

        public static StatusRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new StatusRN();
            }

            return _instancia;
        }

        public StatusDTO Salvar(StatusDTO dto)
        {
            if (dto.Codigo > 0)
            {
                return dao.Alterar(dto);
            }
            else 
            {
                return dao.Adicionar(dto);
            }
        }

        public bool Excluir(StatusDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<StatusDTO> ObterPorFiltro(StatusDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public StatusDTO ObterPorPK(StatusDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<StatusDTO> GetBillingDocumentStatusList()
        {
            return dao.DocumentStatusList();
        }

        public List<StatusDTO> GetDocumentLinesStatusList()
        {
            return dao.DocumentLineStatusList();
        }

        public List<StatusDTO> DocumentPaymentStatusList()
        {
            return dao.PaymentStatusList();
        }

        public List<StatusDTO> GetCustomerOrderStatusList()
        {
            return dao.CustomerOrderStatusList();
        }

        public List<StatusDTO> CustomerRequestServiceStatusList()
        {
            return dao.CustomerRequestServiceStatusList();
        }


        public StatusDTO GetStatusDocument(int pStautsID)
        {
            return GetBillingDocumentStatusList().Where(t => t.Codigo == pStautsID).SingleOrDefault();
        }

        public StatusDTO DocumentLineStatus(int pStautsID)
        {
            return GetDocumentLinesStatusList().Where(t => t.Codigo == pStautsID).SingleOrDefault();
        }

        public StatusDTO GetCustomerOrderStatus(int pStautsID)
        {
            return GetCustomerOrderStatusList().Where(t => t.Codigo == pStautsID).SingleOrDefault();
        }

        public StatusDTO GetPaymentStatus(int pStautsID)
        {
            return DocumentPaymentStatusList().Where(t => t.Codigo == pStautsID).SingleOrDefault();
        }

        public List<StatusDTO> GetClinicalScheduleStatusList()
        {
            return dao.ClinicalScheduleStatusList();
        }

        public List<StatusDTO> DocumentStatusListForSaft()
        {
            List<StatusDTO> lista = new List<StatusDTO>();
            lista.Add(new StatusDTO(1, "NORMAL", "N"));
            lista.Add(new StatusDTO(2, "AUTOFATURAÇÃO", "S"));
            lista.Add(new StatusDTO(3, "DOCUMENTO ANULADO", "A"));
            lista.Add(new StatusDTO(4, "DOCUMENTO DE RESUMO DOUTROS DOCUMENTOS CRIADOS OUTRAS APLICAÇÕES E GERADO NESTA APLICAÇÃO", "R"));
            lista.Add(new StatusDTO(5, "DOCUMENTO FACTURADO", "F"));
            return lista;
        }

    }
}
