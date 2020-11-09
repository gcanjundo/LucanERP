using DataAccessLayer.Comercial.Lavandaria;
using Dominio.Comercial.Lavandaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Lavandaria
{
    public class RoupaMarcaRN
    {
        private static RoupaMarcaRN _instancia;

        private RoupaMarcaDAO dao;

        public RoupaMarcaRN()
        {
            dao = new RoupaMarcaDAO();
           
        }

        public static RoupaMarcaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new RoupaMarcaRN();
            }

            return _instancia;
        }

        public RoupaMarcaDTO Salvar(RoupaMarcaDTO dto)
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

        public RoupaMarcaDTO Excluir(RoupaMarcaDTO dto)
        {
            return dao.Eliminar(dto);
        }
        public RoupaMarcaDTO ObterPorPK(RoupaMarcaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
