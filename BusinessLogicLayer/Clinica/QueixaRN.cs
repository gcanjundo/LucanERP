using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class QueixaRN
    {
        private static QueixaRN _instancia;

        private QueixaDAO dao;

        public QueixaRN()
        {
          dao = new QueixaDAO();
        }

        public static QueixaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new QueixaRN();
            }

            return _instancia;
        }

        public QueixaDTO Salvar(QueixaDTO dto) 
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

        public QueixaDTO Excluir(QueixaDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<QueixaDTO> ObterPorFiltro(QueixaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        

        public QueixaDTO ObterPorPK(QueixaDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
