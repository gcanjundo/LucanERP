using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class AcomodacaoRN
    {
        private static AcomodacaoRN _instancia;

        private AcomodacaoDAO dao;

        public AcomodacaoRN()
        {
          dao = new AcomodacaoDAO();
        }

        public static AcomodacaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AcomodacaoRN();
            }

            return _instancia;
        }

        public AcomodacaoDTO Salvar(AcomodacaoDTO dto) 
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

        public AcomodacaoDTO Excluir(AcomodacaoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<AcomodacaoDTO> ObterPorFiltro(AcomodacaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<AcomodacaoDTO> ListaAcomodacoes(string descricao, int tipo)
        {
            if (descricao==null) 
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new AcomodacaoDTO(descricao, tipo));
        }

        public AcomodacaoDTO ObterPorPK(AcomodacaoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
