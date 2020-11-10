using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;
using System.IO;

namespace DataAccessLayer.Seguranca
{
    public class UtilizadorDAO
    {

        ConexaoDB BaseDados = new ConexaoDB();

        public UtilizadorDTO Inserir(UtilizadorDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_SIS_UTILIZADOR_NOVO";
                
                BaseDados.AddParameter("@NOME", dto.SocialName);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.SenhaNova== null || dto.SenhaNova == "" ? dto.CurrentPassword : dto.SenhaNova);
                BaseDados.AddParameter("@ESTADO", dto.Situacao);
                BaseDados.AddParameter("@EMAIL", dto.Email);
                
                if (dto.Perfil.Codigo > 0)
                {
                    BaseDados.AddParameter("@PERFIL", dto.Perfil.Codigo);
                }
                else
                {
                    BaseDados.AddParameter("@PERFIL", DBNull.Value);
                }

                BaseDados.AddParameter("@SUPERVISOR", dto.Supervisor);
                BaseDados.AddParameter("@IDIOMA_ID", dto.IdiomaID);
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

        public UtilizadorDTO AlterSenha(UtilizadorDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_UTILIZADOR_ALTERAR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.SenhaNova);

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
            return dto;
        }


        public void Eliminar(UtilizadorDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_UTILIZADOR_EXCLUIR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                dto.Codigo = BaseDados.ExecuteNonQuery();
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

        public UtilizadorDTO ObterPorPK(UtilizadorDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_SIS_UTILIZADOR_OBTERPORPK";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);


                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new UtilizadorDTO();
                if (dr.Read())
                {

                    dto.Codigo = int.Parse(dr["UTI_ENTITY_ID"].ToString());
                    dto.SocialName = dr["UTI_NOME"].ToString();
                    dto.Utilizador = dr["UTI_UTILIZADOR"].ToString();
                    dto.CurrentPassword = dr["UTI_SENHA"].ToString();
                    dto.Situacao = dr["UTI_ESTADO"].ToString();
                    dto.Email = dr["UTI_EMAIL"].ToString(); 
                    dto.Perfil = new PerfilDTO
                    {
                        Codigo = int.Parse(dr["UTI_CODIGO_PERFIL"].ToString())
                    };
                    dto.Supervisor = int.Parse(dr["UTI_SUPERVISOR"].ToString() == string.Empty ? "0" : dr["UTI_SUPERVISOR"].ToString());
                    dto.IdiomaID = int.Parse(dr["UTI_IDIOMA"].ToString() == string.Empty ? "1" : dr["UTI_IDIOMA"].ToString());
                    dto.PathFoto = File.Exists(dr["ENT_FOTOGRAFIA_PATH"].ToString()) ? dr["ENT_FOTOGRAFIA_PATH"].ToString() : "~/images/SemFoto";

                }
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

        public UtilizadorDTO ObterPorID(UtilizadorDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_SIS_UTILIZADOR_OBTERPORID";

                BaseDados.AddParameter("@USER_ID", dto.Codigo);


                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new UtilizadorDTO();
                if (dr.Read())
                {

                    dto.Codigo = int.Parse(dr["UTI_CODIGO"].ToString());
                    dto.SocialName = dr["UTI_NOME"].ToString();
                    dto.Utilizador = dr["UTI_UTILIZADOR"].ToString();
                    dto.CurrentPassword = dr["UTI_SENHA"].ToString();
                    dto.Situacao = dr["UTI_ESTADO"].ToString();
                    dto.Email = dr["UTI_EMAIL"].ToString();
                    dto.Perfil = new PerfilDTO
                    {
                        Codigo = int.Parse(dr["UTI_CODIGO_PERFIL"].ToString())
                    };
                    dto.Supervisor = int.Parse(dr["UTI_SUPERVISOR"].ToString() == string.Empty ? "0" : dr["UTI_SUPERVISOR"].ToString());
                    dto.IdiomaID = int.Parse(dr["UTI_IDIOMA"].ToString() == string.Empty ? "1" : dr["UTI_IDIOMA"].ToString());

                }
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

        public List<UtilizadorDTO> ObterPorSituacao(UtilizadorDTO dto)
        {

            List<UtilizadorDTO> coleccao;

            try
            {

                BaseDados.ComandText = "stp_SIS_UTILIZADOR_OBTERPORSituacao";
                BaseDados.AddParameter("@ESTADO", dto.Situacao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<UtilizadorDTO>();

                while (dr.Read())
                {
                    dto = new UtilizadorDTO();

                    dto.SocialName = dr["UTI_NOME"].ToString();
                    dto.Utilizador = dr["UTI_UTILIZADOR"].ToString();
                    dto.CurrentPassword = dr["UTI_SENHA"].ToString();

                    if (dr["UTI_ESTADO"].ToString().Equals("A"))
                    {
                        dto.Situacao = "Activo";

                    }
                    else
                        if (dr["UTI_ESTADO"].ToString().Equals("B"))
                        {
                            dto.Situacao = "Bloqueado";

                        }
                        else
                        {
                            dto.Situacao = "Inactivo";

                        }

                    dto.Email = dr["UTI_EMAIL"].ToString();
                    dto.Codigo = int.Parse(dr["UTI_CODIGO"].ToString());
                     
                    dto.Perfil = new PerfilDTO
                    {
                        Codigo = int.Parse(dr["UTI_CODIGO_PERFIL"].ToString())
                    };



                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new UtilizadorDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<UtilizadorDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;


        }

        public UtilizadorDTO ConfirmarLogin(UtilizadorDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_SIS_UTILIZADOR_CONFIRMALOGIN";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.CurrentPassword);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new UtilizadorDTO();
                if (dr.Read())
                {

                    dto.Sucesso = true;
                }
                else
                {
                    dto.Sucesso = false;
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

        public List<UtilizadorDTO> ObterPorFiltro(UtilizadorDTO dto)
        {

            List<UtilizadorDTO> coleccao;

            try
            {

                BaseDados.ComandText = "stp_SIS_UTILIZADOR_OBTERTODOS";

                BaseDados.AddParameter("@NOME", dto.SocialName);
                BaseDados.AddParameter("@SESSAO", dto.Utilizador);
                BaseDados.AddParameter("@UTILIZADOR", dto.Email);// Filter By User

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<UtilizadorDTO>();

                while (dr.Read())
                {
                    dto = new UtilizadorDTO();

                    dto.SocialName = dr["UTI_NOME"].ToString().ToUpper();
                    dto.Utilizador = dr["UTI_UTILIZADOR"].ToString();
                    dto.CurrentPassword = dr["UTI_SENHA"].ToString();

                    if (dr["UTI_ESTADO"].ToString().Equals("A"))
                    {
                        dto.Situacao = "Activo";

                    }
                    else
                        if (dr["UTI_ESTADO"].ToString().Equals("B"))
                        {
                            dto.Situacao = "Bloqueado";

                        }
                        else
                        {
                            dto.Situacao = "Inactivo";

                        }

                    dto.Email = dr["UTI_EMAIL"].ToString();
                    dto.Codigo = int.Parse(dr["UTI_CODIGO"].ToString());
                     
                    dto.Perfil = new PerfilDTO
                    {
                        Codigo = int.Parse(dr["UTI_CODIGO_PERFIL"].ToString() == "" ? "-1" : dr["UTI_CODIGO_PERFIL"].ToString())
                    };
                    dto.DescricaoPerfil = dto.Perfil.Designacao; 
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new UtilizadorDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<UtilizadorDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;


        }



        public void ResetarSenha(UtilizadorDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_UTILIZADOR_RESETAR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.CurrentPassword);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

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

        public int Superivisor(UtilizadorDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_SIS_UTILIZADOR_CONFIRMALOGIN";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.CurrentPassword);


                dto = new UtilizadorDTO();
                dto.Supervisor = 0;

                MySqlDataReader dr = BaseDados.ExecuteReader(); 
                if (dr.Read())
                {
                    dto.Supervisor = int.Parse(dr["UTI_SUPERVISOR"].ToString() == string.Empty ? "0" : dr["UTI_SUPERVISOR"].ToString()); 
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

            return dto.Supervisor;
        }
    }
}
