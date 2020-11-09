using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class ArmazemRN
    {
        private static ArmazemRN _instancia;

        private ArmazemDAO dao;

        public ArmazemRN()
        {
          dao = new ArmazemDAO();
        }

        public static ArmazemRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ArmazemRN();
            }

            return _instancia;
        }

        public ArmazemDTO Salvar(ArmazemDTO dto)
        {
            return dao.Adicionar(dto);
        }

        

        public void Apagar(ArmazemDTO dto)
        {
            dao.Eliminar(dto);
        }

         

        public List<ArmazemDTO> ObterPorFiltro(ArmazemDTO dto)
        { 
            return dao.ObterPorFiltro(dto);
        }

        public void AddPermissao(ArmazemDTO dto)
        {
            dao.AddPermissao(dto);
        }

        public List<ArmazemDTO> ObterPermissoes(ArmazemDTO dto)
        {
            return dao.ObterPermissoesPorFiltro(dto);
        }

        public ArmazemDTO GetRestWarehouse(ArmazemDTO dto)
        {
            return dao.ObterForRest(dto);
        }

         
    }
}
