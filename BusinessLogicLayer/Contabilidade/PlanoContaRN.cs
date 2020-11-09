using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Contabilidade;
using DataAccessLayer.Contabilidade;


namespace BusinessLogicLayer.Contabilidade
{
    public class PlanoContaRN 
    {
        private static PlanoContaRN _instancia;

        private PlanoContaDAO dao;

        public PlanoContaRN()
        {
          dao = new PlanoContaDAO();
        }

        public static PlanoContaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PlanoContaRN();
            }

            return _instancia;
        }

        public PlanoContaDTO Salvar(PlanoContaDTO dto)
        {
            return dao.Inserir(dto);
        }

        

        public List<PlanoContaDTO> ObterPorFiltro(PlanoContaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public PlanoContaDTO ObterPorPK(PlanoContaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
