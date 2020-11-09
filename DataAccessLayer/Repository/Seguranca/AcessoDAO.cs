using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class AcessoDAO:ConexaoDB
    {
        ConexaoDB BaseDados = new ConexaoDB();
        public AcessoDTO Inserir(AcessoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_SIS_ACESSO_NOVO";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@MAQUINA", dto.Maquina);
                BaseDados.AddParameter("@IP", dto.IP);
                BaseDados.AddParameter("@SYS_FROM", dto.CurrentSystem);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.ExecuteNonQuery();
                dto.DataLogin = DateTime.Now;
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto; 
        }

        public void Alterar(AcessoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_SIS_ACESSO_ALTERAR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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


        public List<AcessoDTO> ObterPorFiltro(AcessoDTO dto)
        {
            List<AcessoDTO> coleccao;
            try
            {
                BaseDados.ComandText = "stp_SIS_ACESSO_OBTERPORFILTRO";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@MAQUINA", dto.Maquina);
                BaseDados.AddParameter("@IP", dto.IP);
                BaseDados.AddParameter("@DATA_LOGIN_INI", DateTime.Today);
                BaseDados.AddParameter("@DATA_LOGIN_FIM", DateTime.Today);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<AcessoDTO>();

                while (dr.Read())
                {
                    dto = new AcessoDTO();

                    UtilizadorDAO daoUtilizador = new UtilizadorDAO();
                    UtilizadorDTO dtoUtilizador = new UtilizadorDTO();

                    dto.Codigo = Int32.Parse(dr["ACE_CODIGO"].ToString());

                    dtoUtilizador.Utilizador = dr["ACE_UTILIZADOR"].ToString();

                    dto.Maquina = dr["ACE_MAQUINA"].ToString();
                    if (dr["ACE_STATUS"].ToString().Equals("A"))
                    {
                        dto.StatusSessao = "Activo";
                    }
                    else
                    {
                        dto.StatusSessao = "Inactivo";
                    }
                    dto.IP = dr["ACE_IP"].ToString();

                    if (dr["ACE_DATA_LOGIN"].ToString() !="")
                    {
                        dto.DataLogin = DateTime.Parse(dr["ACE_DATA_LOGIN"].ToString());
                    }

                    if (dr["ACE_HORA_LOGIN"].ToString() !="")
                    {
                        dto.HoraLogin = DateTime.Parse(dr["ACE_HORA_LOGIN"].ToString());
                    }

                    if (dr["ACE_DATA_LOGOUT"].ToString() !="")
                    {
                        dto.DataLogout = DateTime.Parse(dr["ACE_DATA_LOGOUT"].ToString());
                    }

                    if (dr["ACE_HORA_LOGOUT"].ToString() !="")
                    {
                        dto.HoraLogout = DateTime.Parse(dr["ACE_HORA_LOGOUT"].ToString());
                    }

                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                coleccao = new List<AcessoDTO>();
                dto = new AcessoDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }


        public List<AcessoDTO> ObterPorStatus(AcessoDTO dto)
        {
            List<AcessoDTO> coleccao;

            try
            {
                BaseDados.ComandText = "stp_SIS_ACESSO_STATUS";


                BaseDados.AddParameter("@ESTADO", dto.StatusSessao);


                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<AcessoDTO>();

                while (dr.Read())
                {
                    dto = new AcessoDTO();

                    UtilizadorDAO daoUtilizador = new UtilizadorDAO();
                    UtilizadorDTO dtoUtilizador = new UtilizadorDTO();

                    dto.Codigo = Int32.Parse(dr["ACE_CODIGO"].ToString());

                    dtoUtilizador.Utilizador = dr["ACE_UTILIZADOR"].ToString();

                    dto.Maquina = dr["ACE_MAQUINA"].ToString();
                    if (dr["ACE_STATUS"].ToString().Equals("A"))
                    {
                        dto.StatusSessao = "Activo";
                    }
                    else
                    {
                        dto.StatusSessao = "Inactivo";
                    }
                    dto.IP = dr["ACE_IP"].ToString();

                    if (dr["ACE_DATA_LOGIN"].ToString() !="")
                    {
                        dto.DataLogin = DateTime.Parse(dr["ACE_DATA_LOGIN"].ToString());
                    }

                    if (dr["ACE_HORA_LOGIN"].ToString() !="")
                    {
                        dto.HoraLogin = DateTime.Parse(dr["ACE_HORA_LOGIN"].ToString());
                    }

                    if (dr["ACE_DATA_LOGOUT"].ToString() !="")
                    {
                        dto.DataLogout = DateTime.Parse(dr["ACE_DATA_LOGOUT"].ToString());
                    }

                    if (dr["ACE_HORA_LOGOUT"].ToString() !="")
                    {
                        dto.HoraLogout = DateTime.Parse(dr["ACE_HORA_LOGOUT"].ToString());
                    }

                    coleccao.Add(dto);
                }

            }
            catch (Exception ex)
            {
                coleccao = new List<AcessoDTO>();
                dto = new AcessoDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;

        }


        public AcessoDTO ObterPorPK(AcessoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_ACESSO_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AcessoDTO();
                if (dr.Read())
                {
                    dto = new AcessoDTO();

                    UtilizadorDAO daoUtilizador = new UtilizadorDAO();
                    UtilizadorDTO dtoUtilizador = new UtilizadorDTO();

                    dto.Codigo = Int32.Parse(dr["ACE_CODIGO"].ToString());

                    dtoUtilizador.Utilizador = dr["ACE_UTILIZADOR"].ToString();

                    dto.Maquina = dr["ACE_MAQUINA"].ToString();
                    dto.StatusSessao = dr["ACE_STATUS"].ToString();

                    dto.IP = dr["ACE_IP"].ToString();

                    if (dr["ACE_DATA_LOGIN"].ToString() !="")
                    {
                        dto.DataLogin = DateTime.Parse(dr["ACE_DATA_LOGIN"].ToString());
                    }

                    if (dr["ACE_HORA_LOGIN"].ToString() !="")
                    {
                        dto.HoraLogin = DateTime.Parse(dr["ACE_HORA_LOGIN"].ToString());
                    }

                    if (dr["ACE_DATA_LOGOUT"].ToString() !="")
                    {
                        dto.DataLogout = DateTime.Parse(dr["ACE_DATA_LOGOUT"].ToString());
                    }

                    if (dr["ACE_HORA_LOGOUT"].ToString() !="")
                    {
                        dto.HoraLogout = DateTime.Parse(dr["ACE_HORA_LOGOUT"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }


            return dto;

        }


        public Boolean SessaoIniciada(AcessoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_ACESSO_INICIADO";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AcessoDTO();
                while (dr.Read())
                {
                    dto = new AcessoDTO();
                    if (dr["ACE_STATUS"].ToString().Equals("A"))
                    {
                        dto.Sucesso = true;
                        break;
                    }
                    else
                    {
                        dto.Sucesso = false;
                    }
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
            return dto.Sucesso;

        }

        public AcessoDTO GetLastSession(AcessoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACESSO_LAST_LOGIN";

                BaseDados.AddParameter("@USERNAME", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.CurrentPassword??string.Empty);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AcessoDTO();
                if (dr.Read())
                {
                    dto = new AcessoDTO(); 
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.HoraLogin = DateTime.Parse(dr[1].ToString());
                    dto.Utilizador = dr[2].ToString();
                    dto.CurrentSystem = dr[3].ToString();
                    dto.Filial = dr[4].ToString();
                    dto.Maquina = dr[5].ToString();

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
