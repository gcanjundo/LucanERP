using DataAccessLayer.Tesouraria;
using Dominio.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tesouraria
{
    public class CambioRN
    {
         private static CambioRN _instancia;

        private CambioDAO dao;

        public CambioRN()
        {
          dao = new CambioDAO();
        }

        public static CambioRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new CambioRN();
            }

            return _instancia;
        }

        // Actualiza o Cambio
        public CambioDTO Salvar(CambioDTO dto)
        {
            return dao.Inserir(dto);  
        } 

        public List<CambioDTO> ObterPorFiltro(CambioDTO dto)
        {
            List<CambioDTO> lista =  dao.ObterPorFiltro(dto);
            return lista;
        }

        public CambioDTO ObterCambioActualizado(CambioDTO dto)
        {
            return dao.ObterCambioActual(dto);
        }

        
    }
}
