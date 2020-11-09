using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;


namespace BusinessLogicLayer.Geral
{
    public class PaisRN 
    {
        private static PaisRN _instancia;

        private PaisDAO dao;

        public PaisRN()
        {
          dao = new PaisDAO();
        }

        public static PaisRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PaisRN();
            }

            return _instancia;
        }

        public PaisDTO Salvar(PaisDTO dto)
        {
            if (dto.Codigo > 0)
                return dao.Alterar(dto);
            else
                return dao.Adicionar(dto);

        }

        public bool Excluir(PaisDTO dto)
        {
            if (dao.Eliminar(dto))
                return true;
            else
                return false;
        }

        public List<PaisDTO> ObterPorFiltro(PaisDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public PaisDTO ObterPorPK(PaisDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<PaisDTO> ListaPaises(string descricao)
        {
            return PaisRN.GetInstance().ObterPorFiltro(new PaisDTO(0, descricao));
        }

    }
}
