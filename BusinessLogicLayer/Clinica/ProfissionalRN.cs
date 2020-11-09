using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class ProfissionalRN
    {
        private static ProfissionalRN _instancia;

        private ProfissionalDAO daoMedico;
        EspecialidadeDAO daoEspecialidade;

        public ProfissionalRN()
        {
            daoMedico = new ProfissionalDAO();
            daoEspecialidade = new EspecialidadeDAO();
        }

        public static ProfissionalRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ProfissionalRN();
            }

            return _instancia;
        }

        public ProfissionaDTO Salvar(ProfissionaDTO dto, List<EspecialidadeProfissionalDTO> pEspecilidadesList, string pEspecialidadeRemovida)
        {
            dto = dto.Codigo > 0 ? daoMedico.Alterar(dto) : daoMedico.Adicionar(dto);
            
            foreach (var item in pEspecilidadesList)
            {
                item.ProfissionalID = dto.Codigo;
                item.Estado = 1;
                daoEspecialidade.AddEspecialidadeProfissional(item);
            }

            if(!string.IsNullOrEmpty(pEspecialidadeRemovida) && pEspecialidadeRemovida != ";")
            {
                string[] especilidadeProfissional = pEspecialidadeRemovida.Split(';');
                for(int i =0; i<especilidadeProfissional.Length; i++)
                {
                    string[] especialidadeDeleted = especilidadeProfissional[i].Split('_');
                    daoEspecialidade.AddEspecialidadeProfissional(new EspecialidadeProfissionalDTO
                    {
                        Codigo = int.Parse(especialidadeDeleted[0]),
                        EspecialidadeID = int.Parse(especialidadeDeleted[1]),
                        ProfissionalID = dto.Codigo,
                        Estado = 0

                    });
                }
            }

            return dto;
        }

        public ProfissionaDTO Eliminar(ProfissionaDTO dto) 
        {
            return daoMedico.Excluir(dto);
        }

        public List<ProfissionaDTO> ObterPorFiltro(ProfissionaDTO dto)
        {
            return daoMedico.ObterPorFiltro(dto);
        }

        public ProfissionaDTO ObterPorPK(ProfissionaDTO dto) 
        {
            return daoMedico.ObterPorPK(dto);
        }

        public List<ProfissionaDTO> ObterForDropDownList(ProfissionaDTO dto)
        {
            var lista = ObterPorFiltro(dto);
            lista.Insert(0, new ProfissionaDTO { NomeCompleto = "-SELECCIONE-", Codigo = -1 });

            return lista;
        }

        public List<ProfissionaDTO> GetMedicalProfessional(ProfissionaDTO dto)
        {
            return ObterPorFiltro(dto).Where(t => t.Filial == dto.Filial).ToList(); 
        } 
        public List<ProfissionaDTO> GetNurseProfessional(ProfissionaDTO dto)
        {
            return ObterPorFiltro(dto).Where(t => t.Filial == dto.Filial).ToList();
        }

        public List<ProfissionaDTO> GetLaboratoryAnalistProfessional(ProfissionaDTO dto)
        {
            return ObterPorFiltro(dto).Where(t => t.Filial == dto.Filial).ToList();
        }

        public List<EspecialidadeProfissionalDTO> ObterEspecialidades(EspecialidadeProfissionalDTO dto)
        {
            return daoEspecialidade.ObterGetByProfessional(dto);
        }
    }
}
