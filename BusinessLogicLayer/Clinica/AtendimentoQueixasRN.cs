using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class AtendimentoQueixasRN
    {
        private static AtendimentoQueixasRN _instancia;

        private AtendimentoQueixasDAO dao;

        public AtendimentoQueixasRN()
        {
          dao = new AtendimentoQueixasDAO();
        }

        public static AtendimentoQueixasRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AtendimentoQueixasRN();
            }

            return _instancia;
        }

        public AtendimentoQueixasDTO Salvar(AtendimentoQueixasDTO dto) 
        {
                return dao.Adicionar(dto);
        }

        public AtendimentoQueixasDTO Excluir(AtendimentoQueixasDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<AtendimentoQueixasDTO> ObterPorFiltro(AtendimentoQueixasDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        

        public AtendimentoQueixasDTO ObterPorPK(AtendimentoQueixasDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
