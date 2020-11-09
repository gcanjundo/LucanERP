using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class IdiomaRN
    {
        private static IdiomaRN _instancia;

        private IdiomaDAO dao;

        public IdiomaRN()
        {
          dao = new IdiomaDAO();
        }

        public static IdiomaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new IdiomaRN();
            }

            return _instancia;
        }

        public IdiomaDTO Salvar(IdiomaDTO dto) 
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

        public IdiomaDTO Excluir(IdiomaDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<IdiomaDTO> ObterPorFiltro(IdiomaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<IdiomaDTO> ListaIdiomas(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new IdiomaDTO(0,""));
        }

        public IdiomaDTO ObterPorPK(IdiomaDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
