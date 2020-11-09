using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class TipoActividadeRN
    {
        private static TipoActividadeRN _instancia;

        private TipoActividadeDAO dao;

        public TipoActividadeRN()
        {
          dao = new TipoActividadeDAO();
        }

        public static TipoActividadeRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TipoActividadeRN();
            }

            return _instancia;
        }

        public TipoActividadeDTO Salvar(TipoActividadeDTO dto) 
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

        public TipoActividadeDTO Excluir(TipoActividadeDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<TipoActividadeDTO> ObterPorFiltro(TipoActividadeDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<TipoActividadeDTO> ListaTipoActividadeForDropDownList()
        { 
            var lista = dao.ObterPorFiltro(new TipoActividadeDTO(0,""));

            lista.Insert(-1, new TipoActividadeDTO { Codigo = -1, Descricao = "-SELECCIONE-" });

            return lista;
        }

        public TipoActividadeDTO ObterPorPK(TipoActividadeDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
