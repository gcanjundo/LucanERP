using DataAccessLayer.Comercial.Lavandaria;
using Dominio.Geral;
using Dominio.Comercial.Lavandaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Lavandaria
{
    public class VestuarioRN
    {
        private static VestuarioRN _instancia;

        private readonly VestuarioDAO dao;

        public VestuarioRN()
        {
            dao = new VestuarioDAO();
        }

        public static VestuarioRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new VestuarioRN();
            }

            return _instancia;
        }

        public VestuarioDTO Salvar(VestuarioDTO dto)
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

        public VestuarioDTO Excluir(VestuarioDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<VestuarioDTO> ObterPorFiltro(VestuarioDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

       
        public VestuarioDTO ObterPorPK(VestuarioDTO dto)
        {
            return dao.ObterPorPK(dto);
        }


        public List<VestuarioDTO> ObterVestuarioForDropDownList()
        {
            var lista = ObterPorFiltro(new VestuarioDTO { Descricao = string.Empty });
            lista.Insert(0, new VestuarioDTO { Descricao = "-SELECCIONE-", Codigo = -1 });
            return lista;

        }
       
    }
}
