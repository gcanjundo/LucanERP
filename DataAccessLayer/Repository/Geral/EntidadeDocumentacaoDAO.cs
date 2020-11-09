using System;
using System.Collections.Generic;
using System.Linq;

using MySql.Data.MySqlClient;
using Dominio.Geral;

namespace DataAccessLayer.Geral
{
    public class EntidadeDocumentacaoDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public EntidadeDocumentacaoDTO Adicionar(EntidadeDocumentacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "spt_ACA_ENTIDADE_DOCUMENTOS_ADICIONAR";

                BaseDados.AddParameter("@NUMERO", dto.Numero);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);
                BaseDados.AddParameter("@EMISSAO", dto.Emissao);
                BaseDados.AddParameter("@VALIDADE", dto.Validade);
                BaseDados.AddParameter("@LOCAL_EMISSAO", dto.LocalEmissao);

                BaseDados.ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dto;

        }

        public EntidadeDocumentacaoDTO Alterar(EntidadeDocumentacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "spt_ACA_ENTIDADE_DOCUMENTOS_ALTERAR";

                BaseDados.AddParameter("@NUMERO", dto.Numero);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);
                BaseDados.AddParameter("@EMISSAO", dto.Emissao);
                BaseDados.AddParameter("@VALIDADE", dto.Validade);
                BaseDados.AddParameter("@LOCAL_EMISSAO", dto.LocalEmissao);
                

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dto;

        }

        public EntidadeDocumentacaoDTO Apagar(EntidadeDocumentacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "spt_ACA_ENTIDADE_DOCUMENTOS_EXCLUIR";


                 
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                
                

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dto;

        }

        public EntidadeDocumentacaoDTO ObterPorCodigo(EntidadeDocumentacaoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "spt_ACA_ENTIDADE_DOCUMENTOS_OBTERPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new EntidadeDocumentacaoDTO();
                if (dr.Read())
                {
                     
                    dto.Numero = dr[3].ToString();
                    dto.Entidade = int.Parse(dr[1].ToString());
                    dto.Documento = int.Parse(dr[2].ToString());
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Emissao = Convert.ToDateTime(dr[4].ToString());
                    dto.Validade = Convert.ToDateTime(dr[5].ToString());
                    dto.LocalEmissao = dr[6].ToString();


                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }
            return dto;
        }

        public List<EntidadeDocumentacaoDTO> ObterPorFiltro(EntidadeDocumentacaoDTO dto)
        {
            List<EntidadeDocumentacaoDTO> documentos;

            try
            {
                BaseDados.ComandText = "spt_ACA_ENTIDADE_DOCUMENTOS_OBTERPORFILTRO";

                 
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                documentos = new List<EntidadeDocumentacaoDTO>();
                while (dr.Read())
                {
                    dto = new EntidadeDocumentacaoDTO();


                    dto.Numero = dr[3].ToString();
                    dto.Entidade = int.Parse(dr[1].ToString());
                    dto.Documento = int.Parse(dr[2].ToString());
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Emissao = Convert.ToDateTime(dr[4].ToString());
                    dto.Validade = Convert.ToDateTime(dr[5].ToString());
                    dto.LocalEmissao = dr[6].ToString();
                    dto.NomeDocumento = dr[7].ToString();
                    documentos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new EntidadeDocumentacaoDTO();
                documentos = new List<EntidadeDocumentacaoDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                documentos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();

            }
            return documentos;

        }
    }
}
