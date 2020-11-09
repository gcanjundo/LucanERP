using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class TabelaPrecoRN
    {
        private static TabelaPrecoRN _instancia;

        private TabelaPrecoDAO dao;

        public TabelaPrecoRN()
        {
          dao = new TabelaPrecoDAO();
        }

        public static TabelaPrecoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TabelaPrecoRN();
            }

            return _instancia;
        }

        public TabelaPrecoDTO Salvar(TabelaPrecoDTO dto)
        {
            if (dto.Codigo > 0)
                return dao.Alterar(dto);
            else
                return dao.Adicionar(dto);
        }

        

        public void Apagar(TabelaPrecoDTO dto)
        {
            dao.Eliminar(dto);
        }

        public TabelaPrecoDTO ObterPorPK(TabelaPrecoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<TabelaPrecoDTO> ObterPorFiltro(TabelaPrecoDTO dto)
        {
              
            return dao.ObterPorFiltro(dto);
        }

        public List<TabelaPrecoDTO> GetPriceTableList()
        {
            var dto = new TabelaPrecoDTO();
            
            var lista = ObterPorFiltro(dto);

            lista.Insert(0, new TabelaPrecoDTO(-1, "-SELECCIONE-", "-SELECCIONE-")); 

            return lista;
        }
    }
}
