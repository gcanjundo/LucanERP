using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class DepartamentoRN
    {
        private static DepartamentoRN _instancia;

        private DepartamentoDAO dao;

        public DepartamentoRN()
        {
          dao = new DepartamentoDAO();
        }

        public static DepartamentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new DepartamentoRN();
            }

            return _instancia;
        }

        public DepartamentoDTO Salvar(DepartamentoDTO dto) 
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

        public DepartamentoDTO Excluir(DepartamentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<DepartamentoDTO> ObterPorFiltro(DepartamentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<DepartamentoDTO> ListaDepartamento(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new DepartamentoDTO(0,""));
        }

        public DepartamentoDTO ObterPorPK(DepartamentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
