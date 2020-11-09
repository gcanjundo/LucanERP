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
    public class TriagemDAO
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public TriagemDTO Salvar(TriagemDTO dto) 
        {
            try 
            {
                BaseDados.ComandText = "stp_CLI_ATENDIMENTO_TRIAGEM_ADICIONAR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("ATENDIMENTO", dto.Atendimento);
                BaseDados.AddParameter("PULSO", dto.Pulso);
                BaseDados.AddParameter("FREQUENCIA", dto.Respiracao);
                BaseDados.AddParameter("TEMPERATURA", dto.Temperatura);
                BaseDados.AddParameter("PRESSAO", dto.Tensao);
                BaseDados.AddParameter("ALTURA", dto.Altura);
                BaseDados.AddParameter("PESO", dto.Peso);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("CEFALO", dto.PerimetroCefalico);
                BaseDados.AddParameter("CARDIACA", dto.FrequenciaCardiaca);
                BaseDados.AddParameter("GLICEMIA", dto.Glicemia);
                BaseDados.AddParameter("COLHEITA", dto.Colheita);

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            
            }catch(Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");;
            }finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public TriagemDTO Excluir(TriagemDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_ATENDIMENTO_TRIAGEM_EXCLUIR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                 
                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }


        

        public List<TriagemDTO> ObterTriagem(TriagemDTO dto)
        {
            List<TriagemDTO> lista = new List<TriagemDTO>();
            try
            {
                BaseDados.ComandText = "stp_CLI_ATENDIMENTO_TRIAGEM_OBTERPORFILTRO";

                BaseDados.AddParameter("ATENDIMENTO", dto.Atendimento);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                while (dr.Read()) 
                {
                    dto = new TriagemDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Atendimento = int.Parse(dr[1].ToString()); 
                    dto.Data = Convert.ToDateTime(dr[2].ToString());
                    dto.Pulso = dr[3].ToString();
                    dto.Respiracao = dr[4].ToString();
                    dto.Temperatura = dr[6].ToString();
                    dto.Tensao = dr[7].ToString();
                    dto.Altura = Convert.ToDouble(dr[8].ToString());
                    dto.Peso = Convert.ToDouble(dr[9].ToString());
                    
                    dto.PerimetroCefalico = dr[11].ToString();
                    dto.FrequenciaCardiaca = dr[12].ToString();
                    dto.Glicemia = dr[13].ToString();
                    dto.Colheita = dr[14].ToString();
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
