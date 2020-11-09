using DataAccessLayer.Comercial.Lavandaria;
using Dominio.Comercial.Lavandaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Lavandaria
{
    public class RoupaCorRN
    {
        private static RoupaCorRN _instancia;

        private RoupaCorDAO dao;

        public RoupaCorRN()
        {
            dao = new RoupaCorDAO();

        }

        public static RoupaCorRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new RoupaCorRN();
            }

            return _instancia;
        }

        public RoupaCorDTO Salvar(RoupaCorDTO dto)
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

        public RoupaCorDTO Excluir(RoupaCorDTO dto)
        {
            return dao.Eliminar(dto);
        }
        public RoupaCorDTO ObterPorPK(RoupaCorDTO dto)
        {
            return dao.ObterPorPK(dto);
        } 

       

    }
}
