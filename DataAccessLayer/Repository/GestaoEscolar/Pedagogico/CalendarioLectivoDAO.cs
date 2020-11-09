using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class CalendarioLectivoDAO
    {
        readonly ConexaoDB BaseDados;

        public CalendarioLectivoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public CalendarioLectivoDTO Adicionar(CalendarioLectivoDTO dto)
        {

            BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_CALENDARIO_ADICIONAR";

            
            try
            {


                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);

               dto.Codigo = BaseDados.ExecuteInsert();
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

        public CalendarioLectivoDTO Alterar(CalendarioLectivoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_CALENDARIO_ALTERAR";

                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
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

        public CalendarioLectivoDTO Apagar(CalendarioLectivoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_CALENDARIO_EXCLUIR";

                 
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

        public CalendarioLectivoDTO ObterPorCodigo(CalendarioLectivoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_CALENDARIO_OBTERPORPK";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new CalendarioLectivoDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["CAL_CODIGO"].ToString());
                    dto.Inicio = Convert.ToDateTime(dr["CAL_INICIO"].ToString());
                    dto.Termino = Convert.ToDateTime(dr["CAL_TERMINO"].ToString());
                    dto.Periodo = int.Parse(dr["CAL_CODIGO_PERIODO"].ToString());
                    dto.NomePeriodo = dr["PER_DESCRICAO"].ToString();
                    dto.Descricao = dr["DESCRICAO"].ToString();
                    if (dr["CAL_AVA_CODIGO_AVALIACAO"].ToString() != null && !dr["CAL_AVA_CODIGO_AVALIACAO"].ToString().Equals(String.Empty))
                    {
                        dto.Avaliacao = int.Parse(dr["CAL_AVA_CODIGO_AVALIACAO"].ToString());
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

            return dto;
        }

        public List<CalendarioLectivoDTO> ObterPorFiltro(CalendarioLectivoDTO dto)
        {


            List<CalendarioLectivoDTO> calendario;
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_CALENDARIO_OBTERPORFILTRO";
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@AVALIACAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                calendario = new List<CalendarioLectivoDTO>();

                while (dr.Read())
                {
                    dto = new CalendarioLectivoDTO();

                    dto.Codigo = int.Parse(dr["CAL_CODIGO"].ToString());
                    dto.Inicio = Convert.ToDateTime(dr["CAL_INICIO"].ToString());
                    dto.Termino = Convert.ToDateTime(dr["CAL_TERMINO"].ToString());
                    dto.Periodo = int.Parse(dr["CAL_CODIGO_PERIODO"].ToString());
                    dto.NomePeriodo = dr["PER_DESCRICAO"].ToString();
                    dto.Descricao = dr["DESCRICAO"].ToString();
                    if (dr["CAL_AVA_CODIGO_AVALIACAO"].ToString() != null && !dr["CAL_AVA_CODIGO_AVALIACAO"].ToString().Equals(String.Empty))
                    {
                        dto.Avaliacao = int.Parse(dr["CAL_AVA_CODIGO_AVALIACAO"].ToString());
                    }
                    dto.AnoLectivo = int.Parse(dr["ANO_CODIGO"].ToString());

                    calendario.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new CalendarioLectivoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                calendario = new List<CalendarioLectivoDTO>();
                calendario.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();

            }
            return calendario;

        }

    }
}
