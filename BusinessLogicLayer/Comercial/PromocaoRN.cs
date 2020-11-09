using DataAccessLayer.Comercial;
using Dominio.Comercial;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{
    public class PromocaoRN
    {
        private static PromocaoRN _instancia;

        private PromocaoDAO dao;

        public PromocaoRN()
        {
            dao = new PromocaoDAO();
        }

        public static PromocaoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PromocaoRN();
            }

            return _instancia;
        }

        public PromocaoDTO GetProductSalesPromotion(ArtigoDTO dto)
        {
            return new PromocaoDTO();
        }

        public PromocaoDTO Salvar(PromocaoDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public PromocaoDTO Excluir(PromocaoDTO dto)
        {
            return dao.Excluir(dto);
        }

        public List<PromocaoDTO> ObterPorFiltro(PromocaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public PromocaoDTO ObterPorPK(PromocaoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<PromocaoDTO> ObterForDropDownList()
        {
            var list = new List<PromocaoDTO>() ;

            list.Add(new PromocaoDTO { Codigo = -1, Descricao = "SELECCIONE" });

            return list;
        }
    }
}
