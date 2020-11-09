using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
     
    public class EntidadeDocumentacaoRN
    {
        private static EntidadeDocumentacaoRN _instancia;

        private EntidadeDocumentacaoDAO dao;

        public EntidadeDocumentacaoRN()
        {
          dao = new EntidadeDocumentacaoDAO();
        }

        public static EntidadeDocumentacaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new EntidadeDocumentacaoRN();
            }

            return _instancia;
        }

        public EntidadeDocumentacaoDTO Salvar(EntidadeDocumentacaoDTO dto) 
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

        public EntidadeDocumentacaoDTO Eliminar(EntidadeDocumentacaoDTO dto) 
        {
            return dao.Apagar(dto);
        }

        public List<EntidadeDocumentacaoDTO> ObterPorFiltro(EntidadeDocumentacaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        internal void ExcluirPorEntidade(int codigo)
        {
            throw new NotImplementedException();
        }

        public EntidadeDocumentacaoDTO ObterPorPK(EntidadeDocumentacaoDTO dto) 
        {
            return dao.ObterPorCodigo(dto);
        }

       
    }
}
