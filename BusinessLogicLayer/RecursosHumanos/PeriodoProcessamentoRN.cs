using System.Collections.Generic;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class PeriodoProcessamentoRN
    {
        private static PeriodoProcessamentoRN _instancia;

        private PeriodoProcessamentoDAO dao;

        public PeriodoProcessamentoRN()
        {
          dao= new PeriodoProcessamentoDAO();
        }

        public static PeriodoProcessamentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PeriodoProcessamentoRN();
            }

            return _instancia;
        }

        public PeriodoProcessamentoDTO Salvar(PeriodoProcessamentoDTO dto) 
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

        public PeriodoProcessamentoDTO Excluir(PeriodoProcessamentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<PeriodoProcessamentoDTO> ObterPorFiltro(PeriodoProcessamentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<PeriodoProcessamentoDTO> ListaRegimesLaborais(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new PeriodoProcessamentoDTO(0,descricao));
        }

        public PeriodoProcessamentoDTO ObterPorPK(PeriodoProcessamentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
