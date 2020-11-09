using System.Collections.Generic;
using DataAccessLayer.Seguranca;
using Dominio.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class EmailMonitorRN
    {
        private static EmailMonitorRN _instancia;

        private readonly EmailMonitorDAO dao;

        public EmailMonitorRN()
        {
          dao = new EmailMonitorDAO();
        }

        public static EmailMonitorRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new EmailMonitorRN();
            }

            return _instancia;
        }

        public EmailMonitorDTO Adicionar(EmailMonitorDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public void Apagar(EmailMonitorDTO dto)
        {
            dao.Apagar(dto);
        }

        public EmailMonitorDTO Conta(EmailMonitorDTO dto)
        {
            return dao.ObterPorCodigo(dto);
        }

        public List<EmailMonitorDTO> ListaContasEmail(EmailMonitorDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }
    }
}
