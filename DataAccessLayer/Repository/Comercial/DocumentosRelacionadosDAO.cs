
using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial
{
    public class DocumentosRelacionadosDAO : ConexaoDB
    {
        public DocumentosRelacionadosDTO Adicionar(DocumentosRelacionadosDTO dto)
        {
            try
            {
                ComandText = "stp_COM_DOCUMENTOS_RELACIONADOS_ADICIONAR";

                AddParameter("@MAIN_DOC_ID", dto.MainDocumentID);
                AddParameter("@MAIN_TYPE_ID", dto.MainDocumnetTypeID);
                AddParameter("@RELATED_DOC_ID", dto.RelatedDocumentID);
                AddParameter("@RELATED_TYPE_ID", dto.RelatedDocumentTypeID);
                AddParameter("@UTILIZADOR", dto.Utilizador); 

                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<DocumentosRelacionadosDTO> ObterPorFiltro(DocumentosRelacionadosDTO dto)
        {
            List<DocumentosRelacionadosDTO> lista = new List<DocumentosRelacionadosDTO>();

            try
            {
                ComandText = "stp_COM_DOCUMENTOS_RELACIONADOS_OBTERPORFILTRO";

                AddParameter("@DOC_ID", dto.MainDocumentID);
                AddParameter("@DOC_TYPE_ID", dto.MainDocumnetTypeID);
                
                int DocID = dto.MainDocumentID;

                MySqlDataReader dr = ExecuteReader();

                while(dr.Read())
                {
                    dto = new DocumentosRelacionadosDTO();

                    dto.MainDocumentID = int.Parse(dr[0].ToString());

                    if(dto.MainDocumentID != DocID)
                    {
                        dto.LookupField1 = dr[4].ToString();
                        dto.LookupField2 = dr[6].ToString();
                        dto.ValorLiquido = 0; //decimal.Parse(dr[8].ToString());
                    }
                    else{
                        dto.LookupField1 = dr[5].ToString();
                        dto.LookupField2 = dr[7].ToString();
                        dto.ValorLiquido = 0;// decimal.Parse(dr[9].ToString());
                    }

                    lista.Add(dto);
                }


            }catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        

         
    }
}
