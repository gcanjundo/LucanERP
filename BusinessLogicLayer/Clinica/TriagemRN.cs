using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class TriagemRN
    {
         private static TriagemRN _instancia;

        private TriagemDAO dao;

        public TriagemRN()
        {
          dao = new TriagemDAO();
        }

        public static TriagemRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TriagemRN();
            }

            return _instancia;
        }

        public TriagemDTO Salvar(TriagemDTO dto) 
        {
            return dao.Salvar(dto);
        }

        public List<TriagemDTO> ObterTriagem(TriagemDTO dto)
        {
            return dao.ObterTriagem(dto);
        }


    }
}
