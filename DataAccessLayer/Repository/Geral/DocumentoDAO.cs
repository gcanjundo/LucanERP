using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class DocumentoDAO:ConexaoDB 
    {
         

        public DocumentoDTO Adicionar(DocumentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DOCUMENTO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public DocumentoDTO Alterar(DocumentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DOCUMENTO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);

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

        public DocumentoDTO Eliminar(DocumentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DOCUMENTO_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

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

        public List<DocumentoDTO> ObterPorFiltro(DocumentoDTO dto)
        {
            List<DocumentoDTO> listaDocumentos;
            try
            {
                ComandText = "stp_GER_DOCUMENTO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                listaDocumentos = new List<DocumentoDTO>();

                while(dr.Read())
                {
                   dto = new DocumentoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());
                   dto.Tipo = dr[4].ToString();

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

            return listaDocumentos;
        }

        public DocumentoDTO ObterPorPK(DocumentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DOCUMENTO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new DocumentoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                   
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


       
    }
}
