    using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class UtilizadorPermissaoFormularioDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public PermissaoFormularioDTO Inserir(PermissaoFormularioDTO dto)
        {

            try
            {

                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_FORMULARIO_NOVO";
                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@INCLUSAO", dto.AllowInsert);
                BaseDados.AddParameter("@ALTERACAO", dto.AllowUpdate);
                BaseDados.AddParameter("@.AllowDelete", dto.AllowDelete);
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

            return dto;
            

        }

        public void ExcluirTodosAcessos(PermissaoFormularioDTO dto)
        {
           

            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_FORMULARIO_EXCLUIR_TODOS";
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.ExecuteNonQuery();
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


        public void Excluir(PermissaoFormularioDTO dto)
        {
            
            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_FORMULARIO_EXCLUIR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);
                BaseDados.ExecuteNonQuery();
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

        public void ExcluirDesemparelhados(PermissaoFormularioDTO dto)
        {
            
           

            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_CORRIGIR";
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@TAG", dto.Formulario.TAG);
                BaseDados.ExecuteNonQuery();
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

            List<PermissaoFormularioDTO> coleccao;
            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_FORMULARIO_OBTERPORUTILIZADOR";
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<PermissaoFormularioDTO>();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["UTI_PERM_FORM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["UTI_PERM_FORM_ALTERAR"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["UTI_PERM_FORM_CONSULTAR"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["UTI_PERM_FORM_EXCLUIR"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["UTI_PERM_FORM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);
                    dto.TituloDocumento = dto.Formulario.ShortName;

                    UtilizadorDAO daoUtilizador = new UtilizadorDAO();
                    UtilizadorDTO dtoUtilizador = new UtilizadorDTO();
                    dtoUtilizador.Utilizador = dr["UTI_PERM_FORM_UTILIZADOR"].ToString();
                    

                    dto.AllowPrint = Int32.Parse(dr["UTI_PERM_FORM_IMPRIMIR"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["UTI_PERM_FORM_INSERIR"].ToString());

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
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
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_FORMULARIO_OBTERPORPK";
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@FORMULARIO", dto.Formulario.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                if (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = int.Parse(dr["UTI_PERM_FORM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["UTI_PERM_FORM_ALTERAR"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["UTI_PERM_FORM_CONSULTAR"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["UTI_PERM_FORM_EXCLUIR"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["UTI_PERM_FORM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);
                    dto.TituloDocumento = dto.Formulario.ShortName;
                     
                     dto.Utilizador = dr["UTI_PERM_FORM_UTILIZADOR"].ToString();
                    

                    dto.AllowPrint = Int32.Parse(dr["UTI_PERM_FORM_IMPRIMIR"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["UTI_PERM_FORM_INSERIR"].ToString());
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
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
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_FORMULARIO_OBTERPORMODULO";
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@MODULO", dto.Formulario.Modulo.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<PermissaoFormularioDTO>();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["UTI_PERM_FORM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["UTI_PERM_FORM_ALTERAR"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["UTI_PERM_FORM_CONSULTAR"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["UTI_PERM_FORM_EXCLUIR"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["UTI_PERM_FORM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);
                    dto.TituloDocumento = dto.Formulario.ShortName;

                    UtilizadorDAO daoUtilizador = new UtilizadorDAO();
                    UtilizadorDTO dtoUtilizador = new UtilizadorDTO();
                    dtoUtilizador.Utilizador = dr["UTI_PERM_FORM_UTILIZADOR"].ToString();
                    

                    dto.AllowPrint = Int32.Parse(dr["UTI_PERM_FORM_IMPRIMIR"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["UTI_PERM_FORM_INSERIR"].ToString());

                    coleccao.Add(dto);
                }
             }
             catch (Exception ex)
             {
                 dto = new PermissaoFormularioDTO();
                 dto.Sucesso = false;
                 dto.MensagemErro = ex.Message.Replace("'", "");
                 coleccao = new List<PermissaoFormularioDTO>();
                 coleccao.Add(dto);
             }
             finally
             {
                 BaseDados.FecharConexao();
             }

             return coleccao;
         

        }

        public List<PermissaoFormularioDTO> ObterAcessosDoUtilizador(PermissaoFormularioDTO dto)
        {

            List<PermissaoFormularioDTO> coleccao;
            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_UTILIZADOR_ACESSO";
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@MODULO", -1);//dto.Formulario.FormModulo.ModCodigo); 

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<PermissaoFormularioDTO>();

                while (dr.Read())
                {
                    dto = new PermissaoFormularioDTO();
                    dto.AllowAccess = Int32.Parse(dr["UTI_PERM_FORM_ACESSO"].ToString());
                    dto.AllowUpdate = Int32.Parse(dr["UTI_PERM_FORM_ALTERAR"].ToString());
                    dto.AllowSelect = Int32.Parse(dr["UTI_PERM_FORM_CONSULTAR"].ToString());
                    dto.AllowDelete = Int32.Parse(dr["UTI_PERM_FORM_EXCLUIR"].ToString());

                    FormularioDTO dtoForm = new FormularioDTO();
                    FormularioDAO daoForm = new FormularioDAO();
                    dtoForm.Codigo = Int32.Parse(dr["UTI_PERM_FORM_CODIGO_FORMULARIO"].ToString());
                    dto.Formulario = daoForm.ObterPorPK(dtoForm);
                    dto.TituloDocumento = dto.Formulario.ShortName;
 
                    dto.AllowPrint = Int32.Parse(dr["UTI_PERM_FORM_IMPRIMIR"].ToString());
                    dto.AllowInsert = Int32.Parse(dr["UTI_PERM_FORM_INSERIR"].ToString());
 
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoFormularioDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<PermissaoFormularioDTO>();
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
