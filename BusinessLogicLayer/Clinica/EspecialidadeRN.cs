using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class EspecialidadeRN
    {
        private static EspecialidadeRN _instancia;

        private EspecialidadeDAO dao;

        public EspecialidadeRN()
        {
          dao = new EspecialidadeDAO();
        }

        public static EspecialidadeRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new EspecialidadeRN();
            }

            return _instancia;
        }

        public EspecialidadeDTO Salvar(EspecialidadeDTO dto) 
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

        public EspecialidadeDTO Excluir(EspecialidadeDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<EspecialidadeDTO> ObterPorFiltro(EspecialidadeDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<EspecialidadeDTO> ListaEspecialidades(string descricao, string categoria)
        {
            if (descricao==null) 
            {
                descricao = "";
            }

            if (categoria == null || categoria.Equals("-1"))
            {
                categoria = "";
            }
            return dao.ObterPorFiltro(new EspecialidadeDTO(0, descricao, categoria));
        }

        public EspecialidadeDTO ObterPorPK(EspecialidadeDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

         
    }
}
