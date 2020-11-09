using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class PerfilDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public PerfilDTO Inserir(PerfilDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_PERFIL_NOVO";
                BaseDados.AddParameter("@NOME", dto.Designacao);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@ESTADO", dto.Situacao);
                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SUPERVISOR", dto.Supervisor);

                dto.Codigo = BaseDados.ExecuteInsert();
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

        public PerfilDTO Alterar(PerfilDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_PERFIL_ALTERAR";


                BaseDados.AddParameter("@NOME", dto.Designacao);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@ESTADO", dto.Situacao);
                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SUPERVISOR", dto.Supervisor);

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

        public PerfilDTO Eliminar(PerfilDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_PERFIL_ELIMINAR";

 
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

        public List<PerfilDTO> ObterTodos(PerfilDTO dto)
        {
             List<PerfilDTO> coleccao;
             

             try
             {
                 BaseDados.ComandText ="stp_SIS_PERFIL_OBTERTODOS";
                 BaseDados.AddParameter("UTILIZADOR", dto.Descricao);
                 MySqlDataReader dr = BaseDados.ExecuteReader();

                 coleccao = new List<PerfilDTO>();
                 while (dr.Read())
                 {
                     dto = new PerfilDTO();
                     dto.Codigo = Int32.Parse(dr["PER_CODIGO"].ToString());
                     dto.Descricao = dr["PER_DESCRICAO"].ToString();
                     dto.Designacao = dr["PER_NOME"].ToString();
                     if (dr["PER_STATUS"].ToString().Equals("A"))
                     {
                         dto.Situacao = "Activo";
                     }
                     else
                         if (dr["PER_STATUS"].ToString().Equals("B"))
                         {
                             dto.Situacao = "Bloqueado";
                         }
                         else
                         {
                             dto.Situacao = "Inactivo";
                         } dto.Email = dr["PER_EMAIL"].ToString();


                     coleccao.Add(dto);
                 }
             }
             catch (Exception ex)
             {
                 dto = new PerfilDTO();
                 dto.Sucesso = false;
                 dto.MensagemErro = ex.Message.Replace("'", "");
                 coleccao = new List<PerfilDTO>();
                 coleccao.Add(dto);
             }
             finally
             {
                 BaseDados.FecharConexao();
             }
            return coleccao;

        }

        public List<PerfilDTO> ObterPorFiltro(PerfilDTO dto)
        {
            List<PerfilDTO> coleccao;

            try
            {
                BaseDados.ComandText ="stp_SIS_PERFIL_OBTERPORFILTRO";
                BaseDados.AddParameter("@NOME", dto.Designacao);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<PerfilDTO>();

                while (dr.Read())
                {
                    dto = new PerfilDTO
                    {
                        Codigo = Int32.Parse(dr["PER_CODIGO"].ToString()),
                        Descricao = dr["PER_DESCRICAO"].ToString(),
                        Designacao = dr["PER_NOME"].ToString(),
                        Email = dr["PER_EMAIL"].ToString(),
                        Supervisor = dr["PER_SUPERVISOR"].ToString() != "1" ? 0 : 1
                    };
                    if (dr["PER_STATUS"].ToString().Equals("A"))
                    {
                        dto.Situacao = "Activo";
                    }
                    else
                        if (dr["PER_STATUS"].ToString().Equals("B"))
                        {
                            dto.Situacao = "Bloqueado";
                        }
                        else
                        {
                            dto.Situacao = "Inactivo";
                        }
                    

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PerfilDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<PerfilDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return coleccao;

        }


        public PerfilDTO ObterPorPK(PerfilDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_PERFIL_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new PerfilDTO();
                if (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["PER_CODIGO"].ToString());
                    dto.Descricao = dr["PER_DESCRICAO"].ToString();
                    dto.Designacao = dr["PER_NOME"].ToString();
                    dto.Situacao = dr["PER_STATUS"].ToString();
                    dto.Email = dr["PER_EMAIL"].ToString();
                    dto.Supervisor = dr["PER_SUPERVISOR"].ToString() != "1" ? 0 : 1;
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


        public PerfilDTO ObterPorNome(PerfilDTO dto)
        {
            try
            {

                BaseDados.ComandText ="stp_SIS_PERFIL_OBTERPORNOME";

                BaseDados.AddParameter("@NOME", dto.Designacao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new PerfilDTO();
                if (dr.Read())
                {

                    dto.Codigo = Int32.Parse(dr["PER_CODIGO"].ToString());
                    dto.Descricao = dr["PER_DESCRICAO"].ToString();
                    dto.Designacao = dr["PER_NOME"].ToString();

                    if (dr["PER_STATUS"].ToString().Equals("A"))
                    {
                        dto.Situacao = "Activo";
                    }
                    else
                        if (dr["PER_STATUS"].ToString().Equals("B"))
                        {
                            dto.Situacao = "Bloqueado";
                        }
                        else
                        {
                            dto.Situacao = "Inactivo";
                        }

                    dto.Email = dr["PER_EMAIL"].ToString();



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
    }
}
