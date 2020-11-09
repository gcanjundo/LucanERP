using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class PermissaoModuloDAO:ConexaoDB
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public void Inserir(PermissaoModuloDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_PERMISSAO_NOVO";
                
                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                BaseDados.AddParameter("@VISIBILIDADE", dto.Visibilidade);
                BaseDados.AddParameter("@AUTORIZACAO", dto.Autorizar);
                BaseDados.AddParameter("@ACESSO", dto.Acesso);

                BaseDados.ExecuteNonQuery();
            }catch(Exception ex)
            {
               dto.Sucesso=false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public void Excluir(PermissaoModuloDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_PERMISSAO_EXCLUIR";

                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
               

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

        public void ExcluirTodas(PermissaoModuloDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_PERMISSAO_EXCLUIR_TODOS";

                
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);


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

        public List<PermissaoModuloDTO> ObterPermissoesModulo(PermissaoModuloDTO dto)
        {
            List<PermissaoModuloDTO> coleccao = new List<PermissaoModuloDTO>();

            try
            {
                BaseDados.ComandText ="stp_SIS_MODULO_PERMISSAO_OBTERPORPERFIL";
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PermissaoModuloDTO();

                    dto.Codigo = Int32.Parse(dr["MOD_PERM_CODIGO"].ToString());
                    dto.Acesso = Int32.Parse(dr["MOD_PERM_ACESSO"].ToString());
                    dto.Autorizar = Int32.Parse(dr["MOD_PERM_VISIBILIDADE"].ToString());
                    dto.Codigo = Int32.Parse(dr["MOD_PERM_AUTORIZAR"].ToString());

                    ModuloDTO dtoMod = new ModuloDTO();
                    ModuloDAO daoMod = new ModuloDAO();
                    dtoMod.Codigo = Int32.Parse(dr["MOD_PERM_CODIGO_MODULO"].ToString());
                    dto.Modulo = daoMod.ObterPorPK(dtoMod);

                    PerfilDAO daoPerfil = new PerfilDAO();
                    PerfilDTO dtoPerfil = new PerfilDTO();
                    dtoPerfil.Codigo = Int32.Parse(dr["MOD_PERM_CODIGO_PERFIL"].ToString());
                    dto.Perfil = daoPerfil.ObterPorPK(dtoPerfil);


                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PermissaoModuloDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoModuloDTO>();
                coleccao.Add(dto);
            }
            finally 
            {
                BaseDados.FecharConexao();
            }
             
            return coleccao;

        }


        public PermissaoModuloDTO ObterPermissaoModuloPorPK(PermissaoModuloDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_MODULO_PERMISSAO_OBTERPORPK";

                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new PermissaoModuloDTO();
                if (dr.Read())
                {
                   

                    dto.Codigo = Int32.Parse(dr["MOD_PERM_CODIGO"].ToString());
                    dto.Acesso = Int32.Parse(dr["MOD_PERM_ACESSO"].ToString());
                    dto.Autorizar = Int32.Parse(dr["MOD_PERM_VISIBILIDADE"].ToString());
                    dto.Codigo = Int32.Parse(dr["MOD_PERM_AUTORIZAR"].ToString());

                    ModuloDTO dtoMod = new ModuloDTO();
                    ModuloDAO daoMod = new ModuloDAO();
                    dtoMod.Codigo = Int32.Parse(dr["MOD_PERM_CODIGO_MODULO"].ToString());
                    dto.Modulo = daoMod.ObterPorPK(dtoMod);

                    PerfilDAO daoPerfil = new PerfilDAO();
                    PerfilDTO dtoPerfil = new PerfilDTO();
                    dtoPerfil.Codigo = Int32.Parse(dr["MOD_PERM_CODIGO_PERFIL"].ToString());
                    dto.Perfil = daoPerfil.ObterPorPK(dtoPerfil);


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
