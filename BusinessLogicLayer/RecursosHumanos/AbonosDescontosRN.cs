using System;
using System.Collections.Generic;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class AbonosDescontosRN
    {
        private static AbonosDescontosRN _instancia;

        private readonly AbonoDescontoDAO dao;

        public AbonosDescontosRN()
        {
          dao= new AbonoDescontoDAO();
        }

        public static AbonosDescontosRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AbonosDescontosRN();
            }

            return _instancia;
        }

        public AbonoDescontoDTO Salvar(AbonoDescontoDTO dto) 
        {
            return dao.Adicionar(dto);
        }

        public AbonoDescontoDTO Excluir(AbonoDescontoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<AbonoDescontoDTO> ObterPorFiltro(AbonoDescontoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<AbonoDescontoDTO> ListaAbonos(string descricao, string categoria)
        {
            if (descricao == null)
            {
                descricao = "";
            }

            if (String.IsNullOrEmpty(categoria))
            {
                categoria = string.Empty;

            }
            return dao.ObterPorFiltro(new AbonoDescontoDTO {Descricao = descricao, Categoria = categoria });
        }

        public AbonoDescontoDTO ObterPorPK(AbonoDescontoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
