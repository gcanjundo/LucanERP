using DataAccessLayer.Comercial;
using Dominio.Comercial;
using System.Collections.Generic;

namespace BusinessLogicLayer.Comercial
{
    public class ComissaoRN
    {
        private static ComissaoRN _instancia;

        private ComissaoDAO dao;

        public ComissaoRN()
        {
          dao = new ComissaoDAO();
        }

        public static ComissaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ComissaoRN();
            }

            return _instancia;
        }

        public ComissaoDTO Salvar(ComissaoDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public ComissaoDTO Excluir(ComissaoDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<ComissaoDTO> ObterPorFiltro(ComissaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public ComissaoDTO ObterPorPK(ComissaoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
