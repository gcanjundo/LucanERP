using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class ReligiaoRN
    {
        private static ReligiaoRN _instancia;

        private ReligiaoDAO dao;

        public ReligiaoRN()
        {
          dao = new ReligiaoDAO();
        }

        public static ReligiaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ReligiaoRN();
            }

            return _instancia;
        }

        public ReligiaoDTO Salvar(ReligiaoDTO dto) 
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

        public ReligiaoDTO Excluir(ReligiaoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<ReligiaoDTO> ObterPorFiltro(ReligiaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<ReligiaoDTO> ListaReligioes(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new ReligiaoDTO(0,""));
        }

        public ReligiaoDTO ObterPorPK(ReligiaoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
