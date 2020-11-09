using DataAccessLayer.Comercial.Lavandaria;
using Dominio.Comercial.Lavandaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Lavandaria
{
    public class RoupaDefeitoRN
    {
        private static RoupaDefeitoRN _instancia;

        private RoupaDefeitoDAO dao;

        public RoupaDefeitoRN()
        {
            dao = new RoupaDefeitoDAO();

        }

        public static RoupaDefeitoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new RoupaDefeitoRN();
            }

            return _instancia;
        }

        public RoupaDefeitoDTO Salvar(RoupaDefeitoDTO dto)
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

        public RoupaDefeitoDTO Excluir(RoupaDefeitoDTO dto)
        {
            return dao.Eliminar(dto);
        }
        public RoupaDefeitoDTO ObterPorPK(RoupaDefeitoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
      
        public List<RoupaDefeitoDTO> ListaDefeitos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new RoupaDefeitoDTO(0, ""));
        }

    }
}
