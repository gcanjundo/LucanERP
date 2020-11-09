using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class GrupoSanguineoRN
    {
        private static GrupoSanguineoRN _instancia;

        private GrupoSanguineoDAO dao;

        public GrupoSanguineoRN()
        {
          dao = new GrupoSanguineoDAO();
        }

        public static GrupoSanguineoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new GrupoSanguineoRN();
            }

            return _instancia;
        }

        public GrupoSanguineoDTO Salvar(GrupoSanguineoDTO dto) 
        {
            if (dto.Codigo > 0)
            {
                return dao.Alterar(dto);
            }
            else 
            {
                return dao.Adicionar(dto);
            }
        }

        public GrupoSanguineoDTO Excluir(GrupoSanguineoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<GrupoSanguineoDTO> ObterPorFiltro(GrupoSanguineoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<GrupoSanguineoDTO> ListaGruposSanguineos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new GrupoSanguineoDTO(0,""));
        }

        public GrupoSanguineoDTO ObterPorPK(GrupoSanguineoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
