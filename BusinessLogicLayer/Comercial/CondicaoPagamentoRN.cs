using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Comercial;
using Dominio.Comercial;

namespace BusinessLogicLayer.Comercial
{
    public class CondicaoPagamentoRN
    {
        private static CondicaoPagamentoRN _instancia;

        private CondicaoPagamentoDAO dao;

        public CondicaoPagamentoRN()
        {
          dao = new CondicaoPagamentoDAO();
        }

        public static CondicaoPagamentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new CondicaoPagamentoRN();
            }

            return _instancia;
        }

        public CondicaoPagamentoDTO Salvar(CondicaoPagamentoDTO dto) 
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

        public bool Excluir(CondicaoPagamentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<CondicaoPagamentoDTO> ObterPorFiltro(CondicaoPagamentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<CondicaoPagamentoDTO> ListaCondicaoPagamento(string pDescricao)
        {
            if (pDescricao == null)
            {
                pDescricao = "";
            }
            return ObterPorFiltro(new CondicaoPagamentoDTO(0, pDescricao));
        }

        public CondicaoPagamentoDTO ObterPorPK(CondicaoPagamentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
