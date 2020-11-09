using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class RacaRN
    {
        private static RacaRN _instancia;

        private RacaDAO dao;

        public RacaRN()
        {
          dao = new RacaDAO();
        }

        public static RacaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new RacaRN();
            }

            return _instancia;
        }

        public RacaDTO Salvar(RacaDTO dto) 
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

        public RacaDTO Excluir(RacaDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<RacaDTO> ObterPorFiltro(RacaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<RacaDTO> ListaRacas(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new RacaDTO(0,""));
        }

        public RacaDTO ObterPorPK(RacaDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
