using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class ProvinciaRN
    {
        
        private static ProvinciaRN _instancia;

        private ProvinciaDAO dao;

        public ProvinciaRN()
        {
          dao = new ProvinciaDAO();
        }

        public static ProvinciaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ProvinciaRN();
            }

            return _instancia;
        }

        public ProvinciaDTO Salvar(ProvinciaDTO dto)
        {
            if (dto.Codigo > 0)
                return dao.Alterar(dto);
            else
                return dao.Adicionar(dto);

        }

        public bool Excluir(ProvinciaDTO dto)
        {
            if (dao.Eliminar(dto))
                return true;
            else
                return false;
        }

        public List<ProvinciaDTO> ObterPorFiltro(ProvinciaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<ProvinciaDTO> ObterPorFiltro(int pais, string descricao)
        {
            if (descricao==null) 
            {
                descricao = String.Empty;
            }
            return dao.ObterPorFiltro(new ProvinciaDTO(descricao, pais));
        }



        public ProvinciaDTO ObterPorPK(ProvinciaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
