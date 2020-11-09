using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class GrauParentescoRN
    {
        private static GrauParentescoRN _instancia;

        private GrauParentescoDAO dao;

        public GrauParentescoRN()
        {
          dao = new GrauParentescoDAO();
        }

        public static GrauParentescoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new GrauParentescoRN();
            }

            return _instancia;
        }

        public GrauParentescoDTO Salvar(GrauParentescoDTO dto) 
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

        public bool Excluir(GrauParentescoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<GrauParentescoDTO> ObterPorFiltro(GrauParentescoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<GrauParentescoDTO> ObterPorFiltro(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new GrauParentescoDTO(0,""));
        }

        public GrauParentescoDTO ObterPorPK(GrauParentescoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
