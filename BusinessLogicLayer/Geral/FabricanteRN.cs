using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class FabricanteRN
    {
        private static FabricanteRN _instancia;

        private FabricanteDAO dao;

        public FabricanteRN()
        {
          dao = new FabricanteDAO();
        }

        public static FabricanteRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new FabricanteRN();
            }

            return _instancia;
        }

        public FabricanteDTO Salvar(FabricanteDTO dto) 
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

        public FabricanteDTO Excluir(FabricanteDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<FabricanteDTO> ObterPorFiltro(FabricanteDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<FabricanteDTO> ListaFabricantes(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new FabricanteDTO(0,""));
        }

        public FabricanteDTO ObterPorPK(FabricanteDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
