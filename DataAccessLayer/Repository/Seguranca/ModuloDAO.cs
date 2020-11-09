using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class ModuloDAO:ConexaoDB
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public void Inserir(ModuloDTO dto)
        {
            
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_NOVO";
                
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@LINK", dto.Link);
                BaseDados.AddParameter("@TAG", dto.TAG);
                BaseDados.AddParameter("@INDICE", dto.Indice);
                BaseDados.AddParameter("@ABREVIADO", dto.ShortName);
                BaseDados.AddParameter("@IMAGEM", dto.Imagem);
                BaseDados.ExecuteNonQuery();
            }catch(Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public void Alterar(ModuloDTO dto)
        {
           
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_ALTERAR";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@LINK", dto.Link);
                BaseDados.AddParameter("@TAG", dto.TAG);
                BaseDados.AddParameter("@INDICE", dto.Indice);
                BaseDados.AddParameter("@ABREVIADO", dto.ShortName);
                BaseDados.AddParameter("@IMAGEM", dto.Imagem);
             
                BaseDados.ExecuteNonQuery();
            }catch(Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public List<ModuloDTO> ObterTodos()
        {
            List<ModuloDTO> coleccao;
            ModuloDTO dto;
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_OBTERTODOS";

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<ModuloDTO>();
                while (dr.Read())
                {
                    dto = new ModuloDTO();
                    dto.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Descricao = dr["MOD_DESCRICAO"].ToString();
                    dto.Link = dr["MOD_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["MOD_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["MOD_INDICE"].ToString());
                    dto.Imagem = dr["MOD_IMAGEM"].ToString();
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new ModuloDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<ModuloDTO>();
                coleccao.Add(dto);
            }
            finally 
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public void Eliminar(ModuloDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }
        }

        public ModuloDTO ObterPorPK(ModuloDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_MODULO_OBTERPORPK";
                 
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new ModuloDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Descricao = dr["MOD_DESCRICAO"].ToString();
                    dto.Link = dr["MOD_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["MOD_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["MOD_INDICE"].ToString());
                    dto.Imagem = dr["MOD_IMAGEM"].ToString();
                    dto.ShortName = dr["MOD_ABREVIACAO"].ToString();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public ModuloDTO ObterPorNome(ModuloDTO dto)
        {
           
            try
            {

                BaseDados.ComandText ="stp_SIS_MODULO_OBTERPORNOME";
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new ModuloDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Descricao = dr["MOD_DESCRICAO"].ToString();
                    dto.Link = dr["MOD_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["MOD_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["MOD_INDICE"].ToString());
                    dto.Imagem = dr["MOD_IMAGEM"].ToString();
                    dto.ShortName = dr["MOD_ABREVIACAO"].ToString();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public ModuloDTO ObterPorPKIndice(ModuloDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_MODULO_OBTERPORINDICE";

                BaseDados.AddParameter("@INDICE", dto.Indice);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new ModuloDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Descricao = dr["MOD_DESCRICAO"].ToString();
                    dto.Link = dr["MOD_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["MOD_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["MOD_INDICE"].ToString());
                    dto.Imagem = dr["MOD_IMAGEM"].ToString();
                    dto.ShortName = dr["MOD_ABREVIACAO"].ToString();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public ModuloDTO ObterPorTAG(ModuloDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_MODULO_OBTERPORTAG";

                BaseDados.AddParameter("@TAG", dto.TAG);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new ModuloDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Descricao = dr["MOD_DESCRICAO"].ToString();
                    dto.Link = dr["MOD_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["MOD_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["MOD_INDICE"].ToString());
                    dto.Imagem = dr["MOD_IMAGEM"].ToString();
                    dto.ShortName = dr["MOD_ABREVIACAO"].ToString();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        

        
    }
}
