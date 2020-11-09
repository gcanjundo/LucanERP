using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class MunicipioRN
    {
        private static MunicipioRN _instancia;

        private MunicipioDAO dao;

        public MunicipioRN()
        {
          dao = new MunicipioDAO();
        }

        public static MunicipioRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MunicipioRN();
            }

            return _instancia;
        }

        public MunicipioDTO Salvar(MunicipioDTO dto)
        {
            if (dto.Codigo > 0)
                return dao.Alterar(dto);
            else
                return dao.Adicionar(dto);

        }

        public bool Excluir(MunicipioDTO dto)
        {
            if (dao.Eliminar(dto))
                return true;
            else
                return false;
        }

        public List<MunicipioDTO> ObterPorFiltro(MunicipioDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public MunicipioDTO ObterPorPK(MunicipioDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
