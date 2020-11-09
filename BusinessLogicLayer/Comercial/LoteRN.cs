using DataAccessLayer.Comercial;
using DataAccessLayer.Comercial.Stock;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{

    public class LoteRN
    {
        private static LoteRN _instancia;

        private LoteDAO dao;

        public LoteRN()
        {
          dao = new LoteDAO();
        }

        public static LoteRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new LoteRN();
            }

            return _instancia;
        }

        public void Gravar(List<LoteDTO>  pLista)
        {
            foreach (var dto in pLista)
            { 
                dao.Adicionar(dto);
            }
            
        }

        public List<LoteDTO> ObterPorFiltro(LoteDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public LoteDTO ObterPorPK(LoteDTO dto)
        {
            return ObterPorFiltro(dto)[0];
        }

        public LoteDTO Excluir(LoteDTO dto)
        {
            return dao.Excluir(dto);
        }



    }
}
