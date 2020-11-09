using DataAccessLayer.Comercial.Stock;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{

    public class DimensaoRN
    {
        private static DimensaoRN _instancia;

        private DimensaoDAO dao;

        public DimensaoRN()
        {
          dao = new DimensaoDAO();
        }

        public static DimensaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new DimensaoRN();
            }

            return _instancia;
        }

        public void Gravar(List<DimensaoDTO>  pLista)
        {
            foreach (var dto in pLista)
            {
                
                dao.Adicionar(dto);
            }
            
        }

        public List<DimensaoDTO> GetSizesAndCollors(DimensaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public DimensaoDTO ObterPorPK(DimensaoDTO dto)
        {
            return GetSizesAndCollors(dto)[0];
        }



    }
}
