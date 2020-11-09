using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Dominio.Comercial;
using DataAccessLayer.Comercial;
using Dominio.Geral;

namespace BusinessLogicLayer.Comercial
{
    public class DocumentoComercialRN
    {
        private static DocumentoComercialRN _instancia;

        private DocumentoComercialDAO dao;
        private DocumentosRelacionadosDAO daoRelated;

        public DocumentoComercialRN()
        {
          dao = new DocumentoComercialDAO();
            daoRelated = new  DocumentosRelacionadosDAO();
        }

        public static DocumentoComercialRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new DocumentoComercialRN();
            }

            return _instancia;
        }

        public DocumentoComercialDTO Salvar(DocumentoComercialDTO dto) 
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

        public DocumentoComercialDTO Excluir(DocumentoComercialDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<DocumentoComercialDTO> ObterPorFiltro(DocumentoComercialDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<DocumentoComercialDTO> ObterDocumentosDefault()
        { 
            return dao.ObterDefault();
        }

        public List<DocumentoComercialDTO> ListaDocumentos(string descricao)
        {
             
            return dao.ObterPorFiltro(new DocumentoComercialDTO(0,""));
        }

        public DocumentoComercialDTO ObterPorPK(DocumentoComercialDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

        public List<DocumentoComercialDTO> ObterPermissoes(DocumentoComercialDTO dto)
        {
            List<DocumentoComercialDTO> lista = dao.ObterPermissoes(dto);
            if (lista.Count == 0)
            {
                foreach (var item in ObterPorFiltro(dto))
                {
                    item.AllowUpdate = 0;
                    item.AllowSelect = 0;
                    item.AllowInsert = 0;
                    item.AllowDelete = 0;
                    lista.Add(item);
                }
            }
            else
            {
                 
            }

            return lista;
        }

        public void AddPermissao(DocumentoComercialDTO dto)
        {
            dao.AdicionarAccess(dto);
        }

        public List<DocumentoDTO> GetDocumentsFormatList(DocumentoDTO dto)
        { 
            return dao.GetDocumentsFormat(dto);
        }

        public List<DocumentoComercialDTO>ObterParaConverter(DocumentoComercialDTO dto, bool IsPaid)
        {

            var DocsList = (dto!=null && dto.Codigo > 0) ?  (IsPaid ? new GenericRN().GetOriginalDocumentConversion(dto).Where(t => t.Formato != "RECEIPT_R").ToList() : new GenericRN().GetOriginalDocumentConversion(dto))
            : new List<DocumentoComercialDTO>();
            DocsList.Insert(0, new DocumentoComercialDTO(-1, "SELECCIONE-")); 
            return DocsList; 
        }

        public List<DocumentosRelacionadosDTO> ObterDocumentosRelacionados(DocumentosRelacionadosDTO dto)
        {
            return daoRelated.ObterPorFiltro(dto);
        }


        public string RelateDocument(DocumentosRelacionadosDTO dto)
        {
            string lines = "";
            foreach (var document in ObterDocumentosRelacionados(dto))
            {
                lines += "<tr><td><small>"+ document.LookupField1+" - "+ document.LookupField2 + "</small></td>";
            }

            return lines;
        }

        public List<DocumentoComercialDTO> ObterEmptyDocument()
        { 
            var DocsList = new List<DocumentoComercialDTO>();
            DocsList.Insert(0, new DocumentoComercialDTO(-1, "SELECCIONE-"));
            return DocsList;
        }


    }
}
