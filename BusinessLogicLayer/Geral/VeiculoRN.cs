using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class VeiculoRN 
    {
        private static VeiculoRN _instancia;

        private VeiculoDAO dao;

        public VeiculoRN()
        {
          dao = new VeiculoDAO();
        }

        public static VeiculoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new VeiculoRN();
            }

            return _instancia;
        }

        public VeiculoDTO Salvar(VeiculoDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public VeiculoDTO Excluir(VeiculoDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<VeiculoDTO> ObterPorFiltro(VeiculoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public VeiculoDTO ObterPorPK(VeiculoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
         
        
    }
}
