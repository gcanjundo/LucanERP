using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class FormularioRN
    {
        private static FormularioRN _instancia;
        private FormularioDAO daoFormulario;

        public FormularioRN()
        {
            daoFormulario = new FormularioDAO();
        }

        public static FormularioRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new FormularioRN();
            }

            return _instancia;
        }

        public List<FormularioDTO> ObterTodos()
        {
            return daoFormulario.ObterTodos();
        }

        public FormularioDTO ObterPorPK(FormularioDTO dto)
        {
            FormularioDTO dtoForm = null;

            if (dto.Codigo > 0)
            {
                dtoForm = daoFormulario.ObterPorPK(dto);
            }
            else if (dto.TAG > 0)
            {
                dtoForm = daoFormulario.ObterPorTAG(dto);
            }
            else if (!string.IsNullOrEmpty(dto.ShortName))
            {
                dtoForm = daoFormulario.ObterPorTitulo(dto);
            }

            return dtoForm;
        }

        public List<FormularioDTO> ObterTodosFormularios()
        {
            return daoFormulario.ObterTodos();

        }

        public List<FormularioDTO> ObterFormulariosPorModulo(FormularioDTO dto)
        {
            List<FormularioDTO> lista = null;
            if (dto.Modulo != null && dto.Modulo.Codigo > 0)
            {
                lista = daoFormulario.ObterPorModulo(dto);
            }

            return lista;
        }

        public List<FormularioDTO> ObterFormulariosSubModulo()
        {
            List<FormularioDTO> formularios = ObterTodos();
            List<FormularioDTO> subModulos = new List<FormularioDTO>();
            foreach (FormularioDTO formulario in formularios)
            {
                if (formulario.Indice == 0)
                {
                    subModulos.Add(formulario);
                }
            }

            return subModulos;
        }

        public FormularioDTO ObterPorSubModulo(FormularioDTO dto) 
        {
            return daoFormulario.ObterPorSubModulo(dto);
        }
    }
}
