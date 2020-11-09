using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class GrupoClientesRN
    {
        private static GrupoClientesRN _instancia;

        private GrupoClientesDAO dao;

        public GrupoClientesRN()
        {
          dao = new GrupoClientesDAO();
        }

        public static GrupoClientesRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new GrupoClientesRN();
            }

            return _instancia;
        }

        public CategoriaDTO Salvar(CategoriaDTO dto)
        {
            return dao.Adicionar(dto);
        }

        

        public void Apagar(CategoriaDTO dto)
        {
            dao.Eliminar(dto);
        }

        public CategoriaDTO ObterPorPK(CategoriaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<CategoriaDTO> ObterPorFiltro(CategoriaDTO dto)
        {
              
            return dao.ObterPorFiltro(dto);
        }
    }
}
