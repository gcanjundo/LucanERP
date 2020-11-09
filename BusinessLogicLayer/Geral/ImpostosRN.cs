using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class ImpostosRN
    {
        private static ImpostosRN _instancia;

        private ImpostosDAO dao;

        public ImpostosRN()
        {
          dao = new ImpostosDAO();
        }

        public static ImpostosRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ImpostosRN();
            }

            return _instancia;
        }

        public ImpostosDTO Salvar(ImpostosDTO dto)
        {
            return dao.Adicionar(dto); 
        }

        

        public void Apagar(ImpostosDTO dto)
        {
            dao.Eliminar(dto);
        }

        public ImpostosDTO ObterPorPK(ImpostosDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<ImpostosDTO> ObterPorFiltro(ImpostosDTO dto)
        {
              
            return dao.ObterPorFiltro(dto);
        }
    }
}
