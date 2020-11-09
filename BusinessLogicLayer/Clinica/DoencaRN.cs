using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class DoencaRN
    {
        private static DoencaRN _instancia;

        private DoencaDAO dao;

        public DoencaRN()
        {
          dao = new DoencaDAO();
        }

        public static DoencaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new DoencaRN();
            }

            return _instancia;
        }

        public DoencaDTO Salvar(DoencaDTO dto) 
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

        public DoencaDTO Excluir(DoencaDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<DoencaDTO> ObterPorFiltro(DoencaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<DoencaDTO> ListaDoencas(string descricao)
        {
            if (descricao==null) 
            {
                descricao = "";
            }
            List<DoencaDTO> lista = new List<DoencaDTO>();
            lista.Add(new DoencaDTO(-1, "-Seleccione-"));
            foreach (var dto in dao.ObterPorFiltro(new DoencaDTO(descricao, "", -1, -1)))
            {
                dto.Descricao = string.Format("{0} - {1}", dto.Sigla, dto.Descricao);
                lista.Add(dto);
            }
            return lista;
        }

        public DoencaDTO ObterPorPK(DoencaDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
