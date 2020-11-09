using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class DiagnosticoRN
    {
        private static DiagnosticoRN _instancia;

        private DiagnosticoDAO dao;

        public DiagnosticoRN()
        {
          dao = new DiagnosticoDAO();
        }

        public static DiagnosticoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new DiagnosticoRN();
            }

            return _instancia;
        }

        public DiagnosticoDTO Salvar(DiagnosticoDTO dto) 
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

        public DiagnosticoDTO Excluir(DiagnosticoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<DiagnosticoDTO> ObterPorFiltro(DiagnosticoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        

        public DiagnosticoDTO ObterPorPK(DiagnosticoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

        public List<DiagnosticoDTO> ListaDiagnosticoInicial(DiagnosticoDTO dto)
        {
            dto.Sigla = "I"; 
            return ObterPorFiltro(dto);
        }

        public List<DiagnosticoDTO> ListaDiagnosticoFinal(DiagnosticoDTO dto)
        {
            dto.Sigla = "F";
            return ObterPorFiltro(dto);
        }
    }
}
