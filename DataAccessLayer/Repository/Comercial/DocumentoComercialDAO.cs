using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Comercial;
using MySql.Data.MySqlClient;
using Dominio.Geral;


namespace DataAccessLayer.Comercial
{
    public class DocumentoComercialDAO: ConexaoDB 
    {
         

        public DocumentoComercialDTO Adicionar(DocumentoComercialDTO dto)
        {
            try
            {
                ComandText = "stp_COM_DOCUMENTO_ADICIONAR";
                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);
                AddParameter("@TIPO", dto.Tipo);
                AddParameter("@STOCK", dto.Stock);
                AddParameter("@CONTA", dto.ContaCorrente);
                AddParameter("@CAIXA", dto.Caixa);
                AddParameter("@FORMATO", dto.Formato);
                AddParameter("@ESTADO", dto.Estado); 
                AddParameter("@CATEGORIA", dto.Categoria);
                if(dto.ParentID!=null && dto.ParentID > 0 )
                  AddParameter("@PARENT", dto.ParentID);
                else
                    AddParameter("@PARENT", DBNull.Value);
                AddParameter("@LINK", dto.Link);
                AddParameter("@TEMPLATE", dto.Template); 
               dto.Codigo = ExecuteInsert();
               dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally 
            {
                FecharConexao();
            }

            return dto;
        }

        public DocumentoComercialDTO Alterar(DocumentoComercialDTO dto)
        {
            try
            {
                ComandText = "stp_COM_DOCUMENTO_ALTERAR";

                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);
                AddParameter("@TIPO", dto.Tipo);
                AddParameter("@STOCK", dto.Stock);
                AddParameter("@CONTA", dto.ContaCorrente);
                AddParameter("@CAIXA", dto.Caixa);
                AddParameter("@FORMATO", dto.Formato);
                AddParameter("@ESTADO", dto.Estado);
                AddParameter("@CATEGORIA", dto.Categoria);
                AddParameter("@CODIGO", dto.Codigo);
                if (dto.ParentID != null && dto.ParentID > 0)
                    AddParameter("@PARENT", dto.ParentID);
                else
                    AddParameter("@PARENT", DBNull.Value);
                AddParameter("@LINK", dto.Link);
                AddParameter("@TEMPLATE", dto.Template); 
                
                ExecuteNonQuery();
                
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public DocumentoComercialDTO Eliminar(DocumentoComercialDTO dto)
        {
            try
            {
                ComandText = "stp_COM_DOCUMENTO_EXCLUIR";
                 
                AddParameter("@CODIGO", dto.Codigo);

                dto.Codigo = ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<DocumentoComercialDTO> ObterPorFiltro(DocumentoComercialDTO dto)
        {
            List<DocumentoComercialDTO> listaDocumentos;
            try
            {
                ComandText = "stp_COM_DOCUMENTO_OBTERPORFILTRO";

                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@TIPO", dto.Tipo == null);
                AddParameter("@CATEGORIA", dto.Categoria);

                MySqlDataReader dr = ExecuteReader();

                listaDocumentos = new List<DocumentoComercialDTO>();

                while(dr.Read())
                {
                   dto = new DocumentoComercialDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Tipo = dr[3].ToString();
                   dto.Stock = dr[4].ToString();
                   dto.ContaCorrente = dr[5].ToString();
                   dto.Caixa = dr[6].ToString();
                   dto.Formato = dr[7].ToString();
                   dto.Estado = int.Parse(dr[8].ToString());
                   dto.Categoria = dr[9].ToString();
                   dto.Status = int.Parse(dr[8].ToString());
                    dto.Url = dr[11].ToString();
                   listaDocumentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DocumentoComercialDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDocumentos = new List<DocumentoComercialDTO>();
                listaDocumentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaDocumentos;
        }

        public DocumentoComercialDTO ObterPorPK(DocumentoComercialDTO dto)
        {
            try
            {
                ComandText = "stp_COM_DOCUMENTO_OBTERPORPK";

                AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new DocumentoComercialDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Tipo = dr[3].ToString();
                    dto.Stock = dr[4].ToString();
                    dto.ContaCorrente = dr[5].ToString();
                    dto.Caixa = dr[6].ToString();
                    dto.Formato = dr[7].ToString();
                    dto.Estado = int.Parse(dr[8].ToString());
                    dto.Categoria = dr[9].ToString();
                    dto.ParentID = int.Parse(dr[10].ToString() == null || dr[10].ToString() ==string.Empty ? "-1": dr[10].ToString());
                    dto.Link = dr[11].ToString();
                    dto.Template = dr[12].ToString();
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<DocumentoComercialDTO> ObterDefault()
        {
            List<DocumentoComercialDTO> listaDocumentos;
            DocumentoComercialDTO dto;
            try
            {
                ComandText = "stp_COM_DOCUMENTO_OBTERDEFAULT";

                
                MySqlDataReader dr = ExecuteReader();

                listaDocumentos = new List<DocumentoComercialDTO>();

                while (dr.Read())
                {
                    dto = new DocumentoComercialDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString(); 

                    listaDocumentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DocumentoComercialDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDocumentos = new List<DocumentoComercialDTO>();
                listaDocumentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaDocumentos;
        }

        public void AdicionarAccess(DocumentoComercialDTO dto)
        {
            try
            {
                ComandText = "stp_COM_DOCUMENTO_PERMISSOES_ADICIONAR";
                AddParameter("@DOCUMENT", dto.Codigo);
                AddParameter("@ALLOW_INSERT", dto.AllowInsert);
                AddParameter("@ALLOW_DELETE", dto.AllowDelete);
                AddParameter("@ALLOW_UPDATE", dto.AllowUpdate);
                AddParameter("@ALLOW_SELECT", dto.AllowSelect);
                AddParameter("@PERFIL", dto.UserProfile);
                AddParameter("@ACCOUNT", dto.SocialName == null ? string.Empty : dto.SocialName);
                AddParameter("@UTILIZADOR", dto.Utilizador);

                ExecuteNonQuery();

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            } 
        }

        public List<DocumentoComercialDTO> ObterPermissoes(DocumentoComercialDTO dto)
        {
            List<DocumentoComercialDTO> listaDocumentos; 
            try
            {
                ComandText = "stp_COM_DOCUMENTO_OBTERPERMISSOES";
                AddParameter("@DOCUMENTO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                MySqlDataReader dr = ExecuteReader();

                listaDocumentos = new List<DocumentoComercialDTO>();

                while (dr.Read())
                {
                    dto = new DocumentoComercialDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.AllowDelete = int.Parse(dr[2].ToString());
                    dto.AllowInsert = int.Parse(dr[3].ToString());
                    dto.AllowSelect = int.Parse(dr[4].ToString());
                    dto.AllowUpdate = int.Parse(dr[5].ToString());

                    listaDocumentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DocumentoComercialDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDocumentos = new List<DocumentoComercialDTO>();
                listaDocumentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaDocumentos;
        }


        public List<DocumentoDTO> GetDocumentsFormat(DocumentoDTO dto)
        {
            List<DocumentoDTO> listaDocumentos;
            try
            {
                ComandText = "stp_COM_DOCUMENTOS_OBTERFORMATOS";

                AddParameter("DOCUMENT_TYPE", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

                listaDocumentos = new List<DocumentoDTO>();

                while (dr.Read())
                {
                    dto = new DocumentoDTO();


                    dto.Sigla = dr[0].ToString();
                    dto.Descricao = dr[1].ToString();
                    dto.Operacao = dr[2].ToString();

                    listaDocumentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DocumentoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDocumentos = new List<DocumentoDTO>();
                listaDocumentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }
            listaDocumentos.Insert(0, new DocumentoDTO(-1, "-SELECCIONE-", "-1"));
            return listaDocumentos;
        }

        public List<DocumentoComercialDTO> ObterDocumentosConversao(DocumentoComercialDTO dto)
        {
            List<DocumentoComercialDTO> listaDocumentos;
             
            try
            {
                ComandText = "stp_COM_DOCUMENTO_OBTERCONVERSAO";
                int OriginalDocID = dto.Codigo;
                
                MySqlDataReader dr = ExecuteReader();

                listaDocumentos = new List<DocumentoComercialDTO>();

                while (dr.Read())
                {
                    if(int.Parse(dr[0].ToString()) == OriginalDocID)
                    {
                        dto = new DocumentoComercialDTO
                        {
                            Codigo = int.Parse(dr[1].ToString()),
                            Descricao = dr[2].ToString()
                        };
                        listaDocumentos.Add(dto);
                    }
                }

            }
            catch (Exception ex)
            {
                dto = new DocumentoComercialDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                listaDocumentos = new List<DocumentoComercialDTO>
                {
                    dto
                };
            }
            finally
            {
                FecharConexao();
            } 

            return listaDocumentos;
        }
    }
}
