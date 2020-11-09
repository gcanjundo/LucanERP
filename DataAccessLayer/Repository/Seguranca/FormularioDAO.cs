using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class FormularioDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public void Inserir(FormularioDTO dto)
        {
           
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_NOVO";
                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@NOME", dto.Descricao);
                BaseDados.AddParameter("@TITULO", dto.ShortName);
                BaseDados.AddParameter("@LINK", dto.Link);
                BaseDados.AddParameter("@TAG", dto.TAG);
                BaseDados.AddParameter("@INDICE", dto.Indice);

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

        public void Alterar(FormularioDTO dto)
        {
            
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_ALTERAR";
                
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@NOME", dto.Descricao);
                BaseDados.AddParameter("@TITULO", dto.ShortName);
                BaseDados.AddParameter("@LINK", dto.Link);
                BaseDados.AddParameter("@TAG", dto.TAG);
                BaseDados.AddParameter("@INDICE", dto.Indice);

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

        public List<FormularioDTO> ObterTodos()
        {

            List<FormularioDTO> coleccao;
            FormularioDTO dto;
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERTODOS";

                MySqlDataReader dr = BaseDados.ExecuteReader();

                coleccao = new List<FormularioDTO>();

                while (dr.Read())
                {
                    dto = new FormularioDTO();
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

                    ModuloDTO dtoModulo = new ModuloDTO();
                    dtoModulo.Codigo = Int32.Parse(dr["FORM_CODIGO_MODULO"].ToString());
                    dto.Modulo = dtoModulo;

                    dto.Descricao = dr["FORM_NOME"].ToString();
                    dto.ShortName = dr["FORM_TITULO"].ToString();
                    dto.Link = dr["FORM_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["FORM_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["FORM_INDICE"].ToString());
                    dto.Imagem = dr["FORM_IMAGEM"].ToString();
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new FormularioDTO(); 
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<FormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public void Eliminar(FormularioDTO dto)
        {
            
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_EXCLUIR";
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

        public FormularioDTO ObterPorPK(FormularioDTO dto)
        {
            
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                 
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new FormularioDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

                    ModuloDTO dtoModulo = new ModuloDTO
                    {
                        Codigo = Int32.Parse(dr["FORM_CODIGO_MODULO"].ToString())
                    };
                    dto.Modulo = dtoModulo;
                    dto.Imagem = dr["FORM_IMAGEM"].ToString(); 
                    dto.Descricao = dr["FORM_NOME"].ToString();
                    dto.ShortName = dr["FORM_TITULO"].ToString();
                    dto.Link = dr["FORM_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["FORM_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["FORM_INDICE"].ToString());
                }
                dto.Sucesso = true;
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

        public FormularioDTO ObterPorTitulo(FormularioDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERPORTITULO";

                BaseDados.AddParameter("@DTITULO", dto.ShortName);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new FormularioDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

                    ModuloDTO dtoModulo = new ModuloDTO();
                    dtoModulo.Codigo = Int32.Parse(dr["FORM_CODIGO_MODULO"].ToString());
                    dto.Modulo = dtoModulo;

                    dto.Descricao = dr["FORM_NOME"].ToString();
                    dto.ShortName = dr["FORM_TITULO"].ToString();
                    dto.Link = dr["FORM_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["FORM_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["FORM_INDICE"].ToString());
                    dto.Imagem = dr["FORM_IMAGEM"].ToString();

                } dto.Sucesso = true;
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

        public List<FormularioDTO> ObterPorModulo(FormularioDTO dto)
        {
            List<FormularioDTO> coleccao;

            try
            {

                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERPORMODULO";
                 
                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<FormularioDTO>();
                while (dr.Read())
                {
                    dto = new FormularioDTO();
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

                    ModuloDTO dtoModulo = new ModuloDTO();
                    dtoModulo.Codigo = Int32.Parse(dr["FORM_CODIGO_MODULO"].ToString());
                    dto.Modulo = dtoModulo;

                    dto.Descricao = dr["FORM_NOME"].ToString();
                    dto.ShortName = dr["FORM_TITULO"].ToString();
                    dto.Link = dr["FORM_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["FORM_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["FORM_INDICE"].ToString());
                    dto.Imagem = dr["FORM_IMAGEM"].ToString();
                    dto.Imagem = dr["FORM_IMAGEM"].ToString();
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new FormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<FormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
             
            return coleccao;

        }

        public FormularioDTO ObterPorTAG(FormularioDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERPORTAG";

                BaseDados.AddParameter("@TAG", dto.TAG);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new FormularioDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

                    ModuloDTO dtoModulo = new ModuloDTO();
                    dtoModulo.Codigo = Int32.Parse(dr["FORM_CODIGO_MODULO"].ToString());
                    dto.Modulo = dtoModulo;

                    dto.Descricao = dr["FORM_NOME"].ToString();
                    dto.ShortName = dr["FORM_TITULO"].ToString();
                    dto.Link = dr["FORM_LINK"].ToString();
                    dto.TAG = Int32.Parse(dr["FORM_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["FORM_INDICE"].ToString());
                    dto.Imagem = dr["FORM_IMAGEM"].ToString();
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

        public FormularioDTO ObterPorSubModulo(FormularioDTO dto)
        {
            try
            {
                 

                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERSUBMODULO";
                 
                BaseDados.AddParameter("@TAG", dto.TAG);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new FormularioDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

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

        public List<FormularioDTO> ListaSubModulos()
        {
            List<FormularioDTO> coleccao;
            FormularioDTO dto;

            try
            {

                BaseDados.ComandText ="stp_SIS_FORMULARIO_OBTERSUBMODULOS";
                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<FormularioDTO>();
                while (dr.Read())
                {
                    dto = new FormularioDTO();
                    dto.Codigo = Int32.Parse(dr["FORM_CODIGO"].ToString());

                    dto.TAG = Int32.Parse(dr["FORM_TAG"].ToString());
                    dto.Indice = Int32.Parse(dr["FORM_INDICE"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new FormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<FormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return coleccao;

        }

         
    }
}
