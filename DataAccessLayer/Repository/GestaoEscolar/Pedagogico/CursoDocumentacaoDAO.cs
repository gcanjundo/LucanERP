using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class CursoDocumentacaoDAO
    {
        readonly ConexaoDB BaseDados;

        public CursoDocumentacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public CursoDocumentacaoDTO Adicionar(CursoDocumentacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_DOCUMENTACAO_ADICIONAR";

                //BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@VERSAO", dto.Versao);
                BaseDados.AddParameter("@DOCUMENTO", dto.DescricaoDocumento);
                BaseDados.AddParameter("@QUANTIDADE", dto.Quantidade);
                BaseDados.AddParameter("@CURSO", dto.Curso);
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

        public CursoDocumentacaoDTO Alterar(CursoDocumentacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_DOCUMENTACAO_ALTERAR";

                BaseDados.AddParameter("@VERSAO", dto.Versao);
                BaseDados.AddParameter("@DOCUMENTO", dto.DescricaoDocumento);
                BaseDados.AddParameter("@QUANTIDADE", dto.Quantidade);
                BaseDados.AddParameter("@CURSO", dto.Curso);
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

        public CursoDocumentacaoDTO Apagar(CursoDocumentacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_DOCUMENTACAO_EXCLUIR";

                 
                BaseDados.AddParameter("@DOCUMENTO", dto.Codigo);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                

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

       
        public List<CursoDocumentacaoDTO> ObterPorFiltro(CursoDocumentacaoDTO dto)
        {
            List<CursoDocumentacaoDTO> documentos;

            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_DOCUMENTACAO_OBTERPORFILTRO";

                //BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@CURSO", dto.Curso);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                documentos = new List<CursoDocumentacaoDTO>();
                while (dr.Read())
                {
                    dto = new CursoDocumentacaoDTO();
                     
                    
                    dto.Versao = dr[3];
                    dto.Curso = int.Parse(dr[0]);
                    dto.Quantidade = int.Parse(dr[2]);
                     
                    
                    dto.DescricaoDocumento = dr[1];
                    dto.Codigo = int.Parse(dr[4]);
                    documentos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new CursoDocumentacaoDTO();
                documentos = new List<CursoDocumentacaoDTO>();
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
