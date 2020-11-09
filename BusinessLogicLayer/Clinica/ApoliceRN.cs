using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class ApoliceRN
    {
        private static ApoliceRN _instancia;

        private ApoliceDAO dao;

        public ApoliceRN()
        {
          dao = new ApoliceDAO();
        }

        public static ApoliceRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ApoliceRN();
            }

            return _instancia;
        }

        public ApoliceDTO Salvar(ApoliceDTO dto) 
        {
            return dao.Adicionar(dto);
        }

        public ApoliceDTO Excluir(ApoliceDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<ApoliceDTO> ObterPorFiltro(ApoliceDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<ApoliceDTO> ConveniosAssociados(int paciente, int convenio)
        {
            return dao.ObterPorFiltro(new ApoliceDTO(convenio, paciente));
        }

         
    }
}
