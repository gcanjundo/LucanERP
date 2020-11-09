using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class UnidadeRN 
    {
        private static UnidadeRN _instancia;

        private UnidadeDAO dao;

        public UnidadeRN()
        {
          dao = new UnidadeDAO();
        }

        public static UnidadeRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new UnidadeRN();
            }

            return _instancia;
        }

        public UnidadeDTO Salvar(UnidadeDTO dto)
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

        public UnidadeDTO Excluir(UnidadeDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<UnidadeDTO> ObterPorFiltro(UnidadeDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public UnidadeDTO ObterPorPK(UnidadeDTO dto)
        {
            return dao.ObterPorPK(dto);
        }


        public UnidadeDTO GetUnityByCode(UnidadeDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
        


    }
}
