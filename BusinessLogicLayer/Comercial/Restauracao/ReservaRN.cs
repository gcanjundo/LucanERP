using DataAccessLayer.Comercial.Restauracao;
using Dominio.Comercial.Restauracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Restauracao
{
    public class ReservaRN
    {

        private static ReservaRN _instancia;

        private ReservaDAO dao;

        public ReservaRN()
        {
            dao = new ReservaDAO();
        }

        public static ReservaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ReservaRN();
            }

            return _instancia;
        }

        public ReservaDTO ReservarMesa(ReservaDTO dto)
        {
            dto.BookingStatus = "R";
           return dao.Reservar(dto);
        }

        public List<ReservaDTO> ObterPorFiltro(ReservaDTO dto)
        {
            return dao.ObterReservasPorFiltro(dto);
        }

        public void ActualizarReserva(ReservaDTO dto)
        {
            dao.ActualizarReserva(dto);
        }

        public void Cancelar(ReservaDTO dto)
        {
            dto.BookingStatus = "A";
            ActualizarReserva(dto);
        }

        


    }
}
