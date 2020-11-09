using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dominio.Geral;
using Dominio.Clinica;

namespace DataAccessLayer.Clinica
{
    public class EscalaDAO
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public EscalaDTO Salvar(EscalaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_ESCALA_PROFISSIONAL_ADICIONAR";

                

                BaseDados.AddParameter("DIA", dto.Dia); 
                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional.Codigo); 
                BaseDados.AddParameter("ESPECIALIDADE_ID", dto.EspecialidadeID);
                BaseDados.AddParameter("T1_INICIO", dto.InicioPeriodo1 == DateTime.MinValue ? (object)DBNull.Value : dto.InicioPeriodo1);
                BaseDados.AddParameter("T1_TERMINO", dto.TerminoPeriodo1== DateTime.MinValue ? (object)DBNull.Value : dto.TerminoPeriodo1);
                BaseDados.AddParameter("T2_INICIO", dto.InicioPeriodo2 == DateTime.MinValue ? (object)DBNull.Value : dto.InicioPeriodo2);
                BaseDados.AddParameter("T2_TERMINO", dto.TerminoPeriodo2 == DateTime.MinValue ? (object)DBNull.Value : dto.TerminoPeriodo2);
                BaseDados.AddParameter("T3_INICIO", dto.InicioPeriodo3 == DateTime.MinValue ? (object)DBNull.Value : dto.InicioPeriodo3);
                BaseDados.AddParameter("T3_TERMINO", dto.TerminoPeriodo3 == DateTime.MinValue ? (object)DBNull.Value : dto.TerminoPeriodo3);
                BaseDados.AddParameter("T4_INICIO", dto.InicioPeriodo4 == DateTime.MinValue ? (object)DBNull.Value : dto.InicioPeriodo4);
                BaseDados.AddParameter("T4_TERMINO", dto.TerminoPeriodo4 == DateTime.MinValue ? (object)DBNull.Value : dto.TerminoPeriodo4);
                BaseDados.AddParameter("STATUS_ID", dto.Status);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();
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

        public EscalaDTO Excluir(EscalaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_ESCALA_PROFISSIONAL_EXCLUIR";



                BaseDados.AddParameter("DIA", dto.Dia);
                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional.Codigo);
                BaseDados.AddParameter("ESPECIALIDADE_ID", dto.Especialidade.Codigo);
                BaseDados.AddParameter("INICIO", dto.InicioPeriodo1); 

                BaseDados.ExecuteNonQuery();
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

        public List<EscalaDTO> ObterEscala(EscalaDTO dto)
        {
            List<EscalaDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_CLI_ESCALA_PROFISSIONAL_OBTERPORFILTRO";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional.Codigo);
                BaseDados.AddParameter("ESPECIALIDADE_ID", dto.EspecialidadeID);
                BaseDados.AddParameter("DIA", dto.Data == DateTime.MinValue ? (object)DBNull.Value : dto.Data);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EscalaDTO>();
                while (dr.Read())
                {
                    dto = new EscalaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Dia = int.Parse(dr[1].ToString());
                    dto.Profissional = new ProfissionaDTO 
                    { 
                        ProfissionalID = int.Parse(dr[2].ToString()), 
                        NomeCompleto = dr[16].ToString(), 
                        Codigo = int.Parse(dr[18].ToString()), 
                        Tratamento = dr[17].ToString() 
                    };
                    dto.InicioPeriodo1 = dr[3].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[3].ToString());
                    dto.TerminoPeriodo1 = dr[4].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[4].ToString());
                    dto.InicioPeriodo2 = dr[5].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[5].ToString());
                    dto.TerminoPeriodo2 = dr[6].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[6].ToString());
                    dto.InicioPeriodo3 = dr[7].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[7].ToString());
                    dto.TerminoPeriodo3 = dr[8].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[8].ToString());
                    dto.InicioPeriodo4 = dr[9].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[9].ToString());
                    dto.TerminoPeriodo4 = dr[10].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[10].ToString());
                    dto.DescricaoDia = DiaSemana(dto.Dia);

                    lista.Add(dto);
                    
                }

            }
            catch (Exception ex)
            {
                dto = new EscalaDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<EscalaDTO>();
                lista.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public string DiaSemana(int dia)
        {
            string[] nome_dia = new string[7];

            nome_dia[0] = "Domingo";
            nome_dia[1] = "Segunda-Feira";
            nome_dia[2] = "Terça-Feira";
            nome_dia[3] = "Quarta-Feira";
            nome_dia[4] = "Quinta-Feira";
            nome_dia[5] = "Sexta-Feira";
            nome_dia[6] = "Sábado";

            string nome_dia_semana = nome_dia[dia-1].ToString();

            return nome_dia_semana;
        }

    }
}
