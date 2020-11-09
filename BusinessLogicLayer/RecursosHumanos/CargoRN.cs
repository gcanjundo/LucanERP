using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class CargoRN
    {
        private static CargoRN _instancia;

        private CargoDAO dao;

        public CargoRN()
        {
          dao= new CargoDAO();
        }

        public static CargoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new CargoRN();
            }

            return _instancia;
        }

        public CargoDTO Salvar(CargoDTO dto) 
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

        public CargoDTO Excluir(CargoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<CargoDTO> ObterPorFiltro(CargoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<CargoDTO> ListaCargos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new CargoDTO(0,""));
        }

        public CargoDTO ObterPorPK(CargoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
