using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class VinculoRN
    {
        private static VinculoRN _instancia;

        private VinculoDAO dao;

        public VinculoRN()
        {
          dao= new VinculoDAO();
        }

        public static VinculoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new VinculoRN();
            }

            return _instancia;
        }

        public VinculoDTO Salvar(VinculoDTO dto) 
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

        public VinculoDTO Excluir(VinculoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<VinculoDTO> ObterPorFiltro(VinculoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<VinculoDTO> ListaVinculos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new VinculoDTO(0,descricao));
        }

        public VinculoDTO ObterPorPK(VinculoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
