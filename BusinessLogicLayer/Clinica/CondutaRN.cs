using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class CondutaRN
    {
        private static CondutaRN _instancia;

        private CondutaDAO dao;

        public CondutaRN()
        {
          dao = new CondutaDAO();
        }

        public static CondutaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new CondutaRN();
            }

            return _instancia;
        }

        public ProcedimentoDTO Salvar(ProcedimentoDTO dto) 
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

        public ProcedimentoDTO Excluir(ProcedimentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<ProcedimentoDTO> ObterPorFiltro(ProcedimentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        

        public ProcedimentoDTO ObterPorPK(ProcedimentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
