using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class PacienteRN
    {
        private static PacienteRN _instancia;

        private PacienteDAO daoPaciente;

        public PacienteRN()
        {
          daoPaciente = new PacienteDAO();
        }

        public static PacienteRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PacienteRN();
            }

            return _instancia;
        }

        public PacienteDTO Salvar(PacienteDTO dto) 
        {
            if (dto.Codigo > 0)
            {
                return daoPaciente.Alterar(dto);
            }
            else 
            {
                return daoPaciente.Adicionar(dto);
            }
        }

        public PacienteDTO Eliminar(PacienteDTO dto) 
        {
            return daoPaciente.Excluir(dto);
        }

        public List<PacienteDTO> ObterPorFiltro(PacienteDTO dto)
        {
            return daoPaciente.ObterPorFiltro(dto);
        }

        public PacienteDTO ObterPorPK(PacienteDTO dto) 
        {
            return daoPaciente.ObterPorPK(dto);
        }

        public List<PacienteDTO> ListaUtentesForDropDownList(string pFilial)
        {
            var lista = ObterPorFiltro(new PacienteDTO { NomeCompleto = string.Empty, DataNascimento = DateTime.MinValue });
            lista.Insert(0, new PacienteDTO { NomeCompleto = "-SELECCIONE-", Codigo = -1, PacienteID="-1", DesignacaoEntidade = "-SELECCIONE-" });

            return lista;
        }
    }
}
