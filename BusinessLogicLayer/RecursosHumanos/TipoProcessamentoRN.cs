using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class TipoProcessamentoRN
    {
        private static TipoProcessamentoRN _instancia;

        private TipoProcessamentoDAO dao;

        public TipoProcessamentoRN()
        {
          dao= new TipoProcessamentoDAO();
        }

        public static TipoProcessamentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TipoProcessamentoRN();
            }

            return _instancia;
        }

        public TipoProcessamentoDTO Salvar(TipoProcessamentoDTO dto) 
        {
            return dao.Adicionar(dto);
        }

        public TipoProcessamentoDTO Excluir(TipoProcessamentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<TipoProcessamentoDTO> ObterPorFiltro(TipoProcessamentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<TipoProcessamentoDTO> ListaTipoProcessamentos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new TipoProcessamentoDTO { Descricao = descricao });
        }

        public TipoProcessamentoDTO ObterPorPK(TipoProcessamentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
