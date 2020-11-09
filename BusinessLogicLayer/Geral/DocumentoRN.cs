using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class DocumentoRN
    {
        private static DocumentoRN _instancia;

        private DocumentoDAO dao;

        public DocumentoRN()
        {
          dao = new DocumentoDAO();
        }

        public static DocumentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new DocumentoRN();
            }

            return _instancia;
        }

        public DocumentoDTO Salvar(DocumentoDTO dto) 
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

        public DocumentoDTO Excluir(DocumentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<DocumentoDTO> ObterPorFiltro(DocumentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<DocumentoDTO> ListaDocumentos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new DocumentoDTO(0,""));
        }

        public DocumentoDTO ObterPorPK(DocumentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
