using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class GrupoSalarialRN
    {
        private static GrupoSalarialRN _instancia;

        private GrupoSalarialDAO dao;

        public GrupoSalarialRN()
        {
          dao= new GrupoSalarialDAO();
        }

        public static GrupoSalarialRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new GrupoSalarialRN();
            }

            return _instancia;
        }

        public GrupoSalarialDTO Salvar(GrupoSalarialDTO dto)
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

        public GrupoSalarialDTO Excluir(GrupoSalarialDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<GrupoSalarialDTO> ObterPorFiltro(GrupoSalarialDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<GrupoSalarialDTO> ListaTabelaSalarial(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new GrupoSalarialDTO(descricao, 0));
        }

        public GrupoSalarialDTO ObterPorPK(GrupoSalarialDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
