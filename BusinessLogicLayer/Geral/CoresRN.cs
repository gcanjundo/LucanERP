using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class CoresRN
    {
        private static CoresRN _instancia;

        private CoresDAO dao;

        public CoresRN()
        {
          dao = new CoresDAO();
        }

        public static CoresRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new CoresRN();
            }

            return _instancia;
        }

        public CoresDTO Salvar(CoresDTO dto) 
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

        public CoresDTO Excluir(CoresDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<CoresDTO> ObterPorFiltro(CoresDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<CoresDTO> ListaCoress(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new CoresDTO(0,""));
        }

        public CoresDTO ObterPorPK(CoresDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
