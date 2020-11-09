using DataAccessLayer.Seguranca;
using Dominio.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Seguranca
{
    public class PeriodoFaturacaoRN
    {
        private static PeriodoFaturacaoRN _instancia;

        private PeriodoFaturacaoDAO dao;

        public PeriodoFaturacaoRN()
        {
          dao = new PeriodoFaturacaoDAO();
        }

        public static PeriodoFaturacaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PeriodoFaturacaoRN();
            }

            return _instancia;
        }

        public AnoFaturacaoDTO Salvar(AnoFaturacaoDTO dto) 
        {
            return dao.Adicionar(dto);
        }

        public AnoFaturacaoDTO Excluir(AnoFaturacaoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        List<AnoFaturacaoDTO> ObterPorFiltro(int pFilial)
        {
            AnoFaturacaoDTO dto = new AnoFaturacaoDTO();
            dto.Filial = pFilial.ToString();
            return dao.ObterPorFiltro(dto);
        }

        public List<AnoFaturacaoDTO> GetForDropDowList(int pFilial)
        {
            var lista = ObterPorFiltro(pFilial).ToList();

            lista.Insert(0, new AnoFaturacaoDTO { Ano = -1, Descricao = "-TODOS-" });

            return lista; 

        }

         

        public AnoFaturacaoDTO ObterPorPK(AnoFaturacaoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
