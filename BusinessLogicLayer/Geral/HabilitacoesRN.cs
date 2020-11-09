using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class HabilitacoesRN
    {
        private static HabilitacoesRN _instancia;

        private HabilitacoesDAO dao;

        public HabilitacoesRN()
        {
          dao = new HabilitacoesDAO();
        }

        public static HabilitacoesRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new HabilitacoesRN();
            }

            return _instancia;
        }

        public HabilitacoesDTO Salvar(HabilitacoesDTO dto) 
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

        public bool Excluir(HabilitacoesDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<HabilitacoesDTO> ObterPorFiltro(HabilitacoesDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<HabilitacoesDTO> ListaHabilitacoes(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new HabilitacoesDTO(0, descricao));
        }

        public HabilitacoesDTO ObterPorPK(HabilitacoesDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
