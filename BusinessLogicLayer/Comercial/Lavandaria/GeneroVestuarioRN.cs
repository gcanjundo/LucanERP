using DataAccessLayer.Comercial.Lavandaria;
using Dominio.Comercial.Lavandaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Lavandaria
{
    public class GeneroVestuarioRN
    {
        private static GeneroVestuarioRN _instancia;

        private GeneroVestuarioDAO dao;

        public GeneroVestuarioRN()
        {
            dao = new GeneroVestuarioDAO();
        }

        public static GeneroVestuarioRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new GeneroVestuarioRN();
            }

            return _instancia;
        }

        public GeneroVestuarioDTO Salvar(GeneroVestuarioDTO dto)
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

        public GeneroVestuarioDTO Excluir(GeneroVestuarioDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<GeneroVestuarioDTO> ObterPorFiltro(GeneroVestuarioDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public GeneroVestuarioDTO ObterPorPK(GeneroVestuarioDTO dto)
        {
            return dao.ObterPorPK(dto);
        } 
        

    }
}
