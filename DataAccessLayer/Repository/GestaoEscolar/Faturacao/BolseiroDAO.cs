using Dominio.GestaoEscolar.Pedagogia;
using Dominio.GestaoEscolar.Faturacao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class BolseiroDAO
    {
        readonly ConexaoDB BaseDados;

        public BolseiroDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public List<BolseiroDTO> Adicionar(BolseiroDTO dto)
        {
            List<BolseiroDTO> lista = new List<BolseiroDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_BOLSA_ADICIONAR";

                BaseDados.AddParameter("@ALUNO", dto.Aluno);
                BaseDados.AddParameter("@BOLSA", dto.Bolsa);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Estado);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new BolseiroDTO();
                    dto.Inicio = Convert.ToDateTime(dr["BOL_ALU_INICIO"].ToString());
                    dto.Aluno = dr["ALUNO"].ToString();
                    dto.Adesao = dto.Inicio.ToShortDateString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = ObterPorFiltro(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public List<BolseiroDTO> ObterPorFiltro(BolseiroDTO dto)
        {
            List<BolseiroDTO> lista = new List<BolseiroDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_BOLSA_OBTERPORFILTRO";
                
                BaseDados.AddParameter("@BOLSA", dto.Bolsa);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new BolseiroDTO
                    {
                        Inicio = Convert.ToDateTime(dr["BOL_ALU_INICIO"].ToString()),
                        Aluno = dr["ALUNO"].ToString()
                    };
                    dto.Adesao = dto.Inicio.ToShortDateString();
                    lista.Add(dto);
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

            return lista;
        }

        public bool ConfirmarCriterio(BolseiroDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_BOLSA_CRITERIO";

                BaseDados.AddParameter("@ALUNO", dto.Aluno);
                BaseDados.AddParameter("@CRITERIO", dto.Estado);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                    if (dto.Estado == "ME" && int.Parse(dr["TIPO"].ToString()) > 1)
                    {
                        dto.Sucesso = true;
                    }
                    else if ((dto.Estado == "FN" || dto.Estado == "PF") && int.Parse(dr["TIPO"].ToString()) == 1)
                    {
                        dto.Sucesso = true;
                    }
                    else if (dto.Estado == "QM")
                    {
                        dto.Sucesso = true;
                    }
                    else if (dto.Estado == "OT")
                    {
                        dto.Sucesso = true;
                    }
                    else
                    {
                        dto.Sucesso = false;
                    }
                    break;
                }
                 
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto.Sucesso;
        }

        public bool IsBolseiro(BolseiroDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_BOLSA_OBTERBOLSA";
                BaseDados.AddParameter("@ALUNO", dto.Aluno);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {

                    dto.Sucesso = true;
                    break;
                } 

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;
                dto.Sucesso = false;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto.Sucesso;
        }

       
    }
}
