using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class TurnoRN 
    {
        private static TurnoRN _instancia;

        private TurnoDAO dao;

        public TurnoRN()
        {
          dao = new TurnoDAO();
        }

        public static TurnoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TurnoRN();
            }

            return _instancia;
        }

        public TurnoDTO Salvar(TurnoDTO dto)
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

        public bool Excluir(TurnoDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<TurnoDTO> ObterPorFiltro(TurnoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public TurnoDTO ObterPorPK(TurnoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public bool HasValidTurno(TurnoDTO dto)
        {
            var _turno = ObterPorPK(dto);
            if(_turno.StartTime < dto.StartTime && _turno.EndTime > dto.StartTime)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
    }
}
