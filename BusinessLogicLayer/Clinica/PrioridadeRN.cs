using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class PrioridadeRN
    {
        
        private static PrioridadeRN _instancia;

        private PrioridadeDAO dao;

        public PrioridadeRN()
        {
          dao = new PrioridadeDAO();
        }

        public static PrioridadeRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PrioridadeRN();
            }

            return _instancia;
        }

        public PrioridadeDTO Salvar(PrioridadeDTO dto)
        {
            if (dto.Codigo > 0)
                return dao.Alterar(dto);
            else
                return dao.Adicionar(dto);

        }

        public bool Excluir(PrioridadeDTO dto)
        {
            if (dao.Eliminar(dto))
                return true;
            else
                return false;
        }

        public List<PrioridadeDTO> ObterPorFiltro(PrioridadeDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<PrioridadeDTO> ListaPrioridade()
        {
            List<PrioridadeDTO> lista =  ObterPorFiltro(new PrioridadeDTO());

            lista.Insert(0, new PrioridadeDTO(-1, "-Seleccione-"));

            return lista;
        }



        public PrioridadeDTO ObterPorPK(PrioridadeDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
