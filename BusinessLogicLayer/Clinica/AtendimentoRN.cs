using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class AtendimentoRN
    {
        private static AtendimentoRN _instancia;

        private AtendimentoDAO dao;

        public AtendimentoRN()
        {
          dao = new AtendimentoDAO();
        }

        public static AtendimentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AtendimentoRN();
            }

            return _instancia;
        }

        public AtendimentoDTO Salvar(AtendimentoDTO dto) 
        {
                return dao.Adicionar(dto);            
        }

        
        public List<AtendimentoDTO> ObterPorFiltro(AtendimentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<AtendimentoDTO> Listar(string pFrom, string pTo, string pEspecialidade, string pMedico, string pPaciente, string pTipo, string pFiltro)
        {
            AtendimentoDTO dto = new AtendimentoDTO();
            if (string.IsNullOrEmpty(pFrom))
            {
                pFrom = DateTime.MinValue.ToShortDateString();
            }

            if (string.IsNullOrEmpty(pTo))
            {
                pTo = DateTime.MinValue.ToShortDateString();
            }
             
            dto.DataInicio = Convert.ToDateTime(pFrom);
            dto.DataTermino = Convert.ToDateTime(pTo);
            dto.Especialidade = pEspecialidade;
            dto.Profissional = pMedico;
            dto.Paciente = pPaciente;
            dto.ActoPrincipal = pTipo;
            dto.Filtro = pFiltro;
            return dao.ObterPorFiltro(dto);
        }

        public List<AtendimentoDTO> ObterMarcacoes(AtendimentoDTO dto, List<EscalaDTO> pAgenda)
        {

            var marcacoes = dao.ObterMarcacoes(dto);
            if (dto.Hora != string.Empty)
            {
                string[] hora = dto.Hora.Split(':');
                dto.BookedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(hora[0]), int.Parse(hora[1]), 0);
            }

            var profissional = new ProfissionaDTO { Codigo = int.Parse(dto.Profissional) };
            if(marcacoes.Count == 0)
            {

            }

            foreach (var escala in EscalaRN.GetInstance().ObterEscala(new EscalaDTO { EspecialidadeID = dto.EspecialidadeID, Data = dto.DataInicio, Profissional = profissional }))
            {
                if (!marcacoes.Exists(t => t.ProfissionalID == escala.Profissional.Codigo && t.BookedTime.TimeOfDay == escala.InicioPeriodo1.TimeOfDay ||
                 t.BookedTime.TimeOfDay == escala.InicioPeriodo2.TimeOfDay || t.BookedTime.TimeOfDay == escala.InicioPeriodo3.TimeOfDay ||
                 t.BookedTime.TimeOfDay == escala.InicioPeriodo4.TimeOfDay))
                {

                }
            }

            return marcacoes;
        }

        public AtendimentoDTO ObterPorPK(AtendimentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        } 

        public void Cancelar(AtendimentoDTO dto)
        {
            dto.Situacao = "11";
            dao.ActualizacaoStatus(dto);
        }

        public void IniciarAtendimento(AtendimentoDTO dto)
        {
            dto.Situacao = "10";
            dao.ActualizacaoStatus(dto);
        }

        public void FinalizarAtendimento(AtendimentoDTO dto)
        {
            dto.Situacao = "6";
            dao.ActualizacaoStatus(dto);
        }

        public void AdicionarAnamnese(AtendimentoDTO dto)
        {
            dao.AdicionarAnamnese(dto);
        }

        public void AdicionarExamesFisicos(AtendimentoDTO dto)
        {
            dao.AddExamesFisicos(dto);
        }

        public AtendimentoDTO ObterExameFisico(AtendimentoDTO dto)
        {
            return dao.ObterExameFisico(dto);
        }

        public AtendimentoDTO ObterAnamnese(AtendimentoDTO dto)
        {
            return dao.ObterAnamnese(dto);
        }
    }
}
