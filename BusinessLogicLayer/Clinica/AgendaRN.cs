using DataAccessLayer.Clinica;
using Dominio.Clinica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Clinica
{
    public class AgendaRN
    {
        private static AgendaRN _instancia;

        private AgendaDAO dao;

        public AgendaRN()
        {
          dao = new AgendaDAO();
        }

        public static AgendaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AgendaRN();
            }

            return _instancia;
        }

        public AgendaDTO Salvar(AgendaDTO dto) 
        {
            return new AgendaDTO(); //dao.Adicionar(dto);
        }

        public AgendaDTO Excluir(AgendaDTO dto) 
        {
            return new AgendaDTO();//dao.Eliminar(dto);
        }

        public List<AgendaDTO> ObterPorFiltro(AgendaDTO dto)
        {
            return new List<AgendaDTO>(); //dao.ObterPorFiltro(dto);
        }

        public AgendaDTO ObterPorPK(AgendaDTO dto)
        {
            return new AgendaDTO();
        }
    }
}
