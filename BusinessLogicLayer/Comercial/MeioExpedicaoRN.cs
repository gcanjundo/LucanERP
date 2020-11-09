using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Comercial;
using Dominio.Comercial;

namespace BusinessLogicLayer.Comercial
{
    public class MeioExpedicaoRN
    {
        private static MeioExpedicaoRN _instancia;

        private MeioExpedicaoDAO dao;

        public MeioExpedicaoRN()
        {
          dao = new MeioExpedicaoDAO();
        }

        public static MeioExpedicaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MeioExpedicaoRN();
            }

            return _instancia;
        }

        public MeioExpedicaoDTO Salvar(MeioExpedicaoDTO dto) 
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

        public bool Excluir(MeioExpedicaoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<MeioExpedicaoDTO> ObterPorFiltro(MeioExpedicaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<MeioExpedicaoDTO> ListaMeioExpedicao(string pDescricao)
        {
            if (pDescricao == null)
            {
                pDescricao = "";
            }
            return dao.ObterPorFiltro(new MeioExpedicaoDTO(0, pDescricao));
        }

        public MeioExpedicaoDTO ObterPorPK(MeioExpedicaoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
