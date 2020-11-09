using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class PermissaoFormularioDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();
        public void Inserir(PermissaoFormularioDTO dto)
        {
            

            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_NOVO";

                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.AddParameter("@INCLUSAO", dto.AllowInsert);
                BaseDados.AddParameter("@ALTERACAO", dto.AllowUpdate);
                BaseDados.AddParameter("@EXCLUSAO", dto.AllowDelete);
                BaseDados.AddParameter("@CONSULTA", dto.AllowSelect);
                BaseDados.AddParameter("@IMPRESSAO", dto.AllowPrint);
                BaseDados.AddParameter("@ACESSO", dto.AllowAccess);

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

        }

        public void ExcluirSolitarios(PermissaoFormularioDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_CORRIGIR";

                BaseDados.AddParameter("@TAG", dto.Formulario.TAG);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch(Exception ex) 
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public void Excluir(PermissaoFormularioDTO dto)
        {
            try
            {
               BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_EXCLUIR";
               BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);
               BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);

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

        }


        public void ExcluirTodas(PermissaoFormularioDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_EXCLUIR_TODOS";
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
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

        }

        public List<PermissaoFormularioDTO> ObterPermissoesFormulario(PermissaoFormularioDTO dto)
        {
            List<PermissaoFormularioDTO> coleccao = new List<PermissaoFormularioDTO>();

            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_OBTERPORPERFIL";

                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    dto.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);

                    PerfilDAO daoPerfil = new PerfilDAO();
                    PerfilDTO dtoPerfil = new PerfilDTO();
                    dtoPerfil.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_PERFIL"].ToString());
                    dto.Perfil = daoPerfil.ObterPorPK(dtoPerfil);

                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoFormularioDTO>();
                coleccao.Add(dto);
            }
            finally 
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public PermissaoFormularioDTO ObterPorPK(PermissaoFormularioDTO dto)
        {

            try
            {
                BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_OBTERPORPK";

                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                if (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    dto.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);

                    PerfilDAO daoPerfil = new PerfilDAO();
                    PerfilDTO dtoPerfil = new PerfilDTO();
                    dtoPerfil.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_PERFIL"].ToString());
                    dto.Perfil = daoPerfil.ObterPorPK(dtoPerfil);

                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());


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


        public List<PermissaoFormularioDTO> ObterPermissoesFormularioPorModulo(PermissaoFormularioDTO dto)
        {
            List<PermissaoFormularioDTO> coleccao;
            try
            {

                BaseDados.ComandText ="stp_SIS_FORMULARIO_PERMISSAO_OBTERPORMODULO";

                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.AddParameter("@MODULO", dto.Formulario.Modulo.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<PermissaoFormularioDTO>();
                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    dto.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);

                    PerfilDAO daoPerfil = new PerfilDAO();
                    PerfilDTO dtoPerfil = new PerfilDTO();
                    dtoPerfil.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_PERFIL"].ToString());
                    dto.Perfil = daoPerfil.ObterPorPK(dtoPerfil);

                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoFormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }


        public List<PermissaoFormularioDTO> ObterPermissoesFormularioPOS(UtilizadorDTO objUser)
        {
            List<PermissaoFormularioDTO> coleccao = new List<PermissaoFormularioDTO>();
            PermissaoFormularioDTO dto;
            try
            {
                BaseDados.ComandText = "stp_SIS_POS_FORMULARIO_PERMISSAO_PERFIL";

                BaseDados.AddParameter("@UTILIZADOR", objUser.Utilizador);
                BaseDados.AddParameter("@PERFIL", objUser.Perfil.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    dto.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());


                    dto.Formulario = new FormularioDTO(Int32.Parse(dr["FORM_PERM_CODIGO_FORMULARIO"].ToString()),
                        dr["FORM_TITULO"].ToString(), dr["FORM_LINK"].ToString());

                    dto.Perfil = new PerfilDTO { Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_PERFIL"].ToString()) };

                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoFormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public List<PermissaoFormularioDTO> ObterFormulariosPOS()
        {
            List<PermissaoFormularioDTO> coleccao = new List<PermissaoFormularioDTO>();
            PermissaoFormularioDTO dto;
            try
            {
                BaseDados.ComandText = "stp_SIS_POS_FORMULARIOS_POS";

                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    //dto.FormPermCodigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());
                    dto.Formulario = new FormularioDTO(Int32.Parse(dr["POS_FORM_CODIGO"].ToString()),
                        dr["FORM_TITULO"].ToString(), "");
                    dto.Perfil = new PerfilDTO();
                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoFormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public void AddPermissaoPOS(PermissaoFormularioDTO dto)
        {


            try
            {
                BaseDados.ComandText = "stp_SIS_POS_FORMULARIO_PERMISSAO_ADICIONAR";

                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.AddParameter("@INCLUSAO", dto.AllowInsert);
                BaseDados.AddParameter("@ALTERACAO", dto.AllowUpdate);
                BaseDados.AddParameter("@EXCLUSAO", dto.AllowDelete);
                BaseDados.AddParameter("@CONSULTA", dto.AllowSelect);
                BaseDados.AddParameter("@IMPRESSAO", dto.AllowPrint);
                BaseDados.AddParameter("@ACESSO", dto.AllowAccess);

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

        }

        public void AllowSysAdmin()
        {
            PermissaoFormularioDTO dto = new PermissaoFormularioDTO();
            try
            {
                BaseDados.ComandText = "stp_SIS_FORMULARIO_PERMISSAO_SYSADMIN_ADICIONAR";
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
        }

        public List<PermissaoFormularioDTO> ObterFormulariosREST()
        {
            List<PermissaoFormularioDTO> coleccao = new List<PermissaoFormularioDTO>();
            PermissaoFormularioDTO dto;
            try
            {
                BaseDados.ComandText = "stp_SIS_REST_FORMULARIOS_REST";


                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    //dto.FormPermCodigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());
                    dto.Formulario = new FormularioDTO(Int32.Parse(dr["FORM_CODIGO"].ToString()),
                        dr["FORM_TITULO"].ToString(), "");
                    dto.Perfil = new PerfilDTO();
                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoFormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public List<PermissaoFormularioDTO> ObterPermissoesFormularioREST(UtilizadorDTO objUser)
        {
            List<PermissaoFormularioDTO> coleccao = new List<PermissaoFormularioDTO>();
            PermissaoFormularioDTO dto;
            try
            {
                BaseDados.ComandText = "stp_SIS_REST_FORMULARIO_PERMISSAO_PERFIL";

                BaseDados.AddParameter("@UTILIZADOR", objUser.Utilizador);
                BaseDados.AddParameter("@PERFIL", objUser.Perfil.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["FORM_PERM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["FORM_PERM_ALTERACAO"].ToString());
                    dto.Codigo = Int32.Parse(dr["FORM_PERM_CODIGO"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["FORM_PERM_CONSULTA"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["FORM_PERM_EXCLUSAO"].ToString());


                    dto.Formulario = new FormularioDTO(Int32.Parse(dr["FORM_PERM_CODIGO_FORMULARIO"].ToString()),
                        dr["FORM_TITULO"].ToString(), dr["FORM_LINK"].ToString());

                    dto.Perfil = new PerfilDTO { Codigo = Int32.Parse(dr["FORM_PERM_CODIGO_PERFIL"].ToString()) };

                    dto.AllowPrint = Int32.Parse(dr["FORM_PERM_IMPRESSAO"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["FORM_PERM_INCLUSAO"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoFormularioDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }

        public void AddPermissaoREST(PermissaoFormularioDTO dto)
        {


            try
            {
                BaseDados.ComandText = "stp_SIS_REST_FORMULARIO_PERMISSAO_ADICIONAR";

                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.AddParameter("@INCLUSAO", dto.AllowInsert);
                BaseDados.AddParameter("@ALTERACAO", dto.AllowUpdate);
                BaseDados.AddParameter("@EXCLUSAO", dto.AllowDelete);
                BaseDados.AddParameter("@CONSULTA", dto.AllowSelect);
                BaseDados.AddParameter("@IMPRESSAO", dto.AllowPrint);
                BaseDados.AddParameter("@ACESSO", dto.AllowAccess);

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

        }
    }
}
