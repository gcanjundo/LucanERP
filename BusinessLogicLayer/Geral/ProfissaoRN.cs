using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class ProfissaoRN
    {
        private static ProfissaoRN _instancia;

        private ProfissaoDAO dao;

        public ProfissaoRN()
        {
          dao = new ProfissaoDAO();
        }

        public static ProfissaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ProfissaoRN();
            }

            return _instancia;
        }

        public ProfissaoDTO Salvar(ProfissaoDTO dto) 
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

        public bool Excluir(ProfissaoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<ProfissaoDTO> ObterPorFiltro(ProfissaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<ProfissaoDTO> ListaProfissoes(string descricao)
        {
            if (descricao==null) 
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new ProfissaoDTO(0, descricao));
        }

        public ProfissaoDTO ObterPorPK(ProfissaoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
