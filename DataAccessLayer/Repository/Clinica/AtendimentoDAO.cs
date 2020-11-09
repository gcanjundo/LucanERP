using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Clinica
{
    public class AtendimentoDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public AtendimentoDTO Adicionar(AtendimentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_ADICIONAR";

                BaseDados.AddParameter("PACIENTE_ID", dto.Paciente);
                BaseDados.AddParameter("ENTIDADE_ID", dto.Entidade);
                BaseDados.AddParameter("FILIAL_ID", dto.Filial);
                BaseDados.AddParameter("MARCACAO", dto.DataRegisto); 
                BaseDados.AddParameter("DEPARTAMENTO_ID", dto.Departamento); 
                BaseDados.AddParameter("ESPECIALIDADE_ID", dto.Especialidade);
                BaseDados.AddParameter("SERVICO_ID", dto.ActoPrincipal);
                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional); 
                BaseDados.AddParameter("STATUS_ID", dto.Status); 
                BaseDados.AddParameter("TIPO", dto.Situacao);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);  
                BaseDados.AddParameter("OBSERVACOES", dto.Obs);
                BaseDados.AddParameter("CONVENIO_ID", dto.PlanoSaude);
                BaseDados.AddParameter("APOLICE", dto.NumeroBeneficiario);
                BaseDados.AddParameter("HORA_CHEGADA", dto.DataChegada == DateTime.MinValue ? (object)DBNull.Value : dto.DataChegada);

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

        public AtendimentoDTO ActualizacaoStatus(AtendimentoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_ALTERAR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("SITUACAO", dto.Situacao);
                BaseDados.AddParameter("DESFECHO", dto.Desfecho);
                BaseDados.AddParameter("OBS", dto.Obs);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
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

         

        public List<AtendimentoDTO> ObterPorFiltro(AtendimentoDTO dto) 
        {
            List <AtendimentoDTO> lista = new List<AtendimentoDTO>();

            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_OBTERPORFILTRO";

                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional);
                BaseDados.AddParameter("PACIENTE", dto.Paciente); 

                if(dto.DataInicio!=DateTime.MinValue)
                  BaseDados.AddParameter("DATA_INI", dto.DataInicio.ToShortDateString().Replace("/", "-"));
                else
                  BaseDados.AddParameter("DATA_INI", DBNull.Value);

                if (dto.DataTermino != DateTime.MinValue)
                    BaseDados.AddParameter("DATA_TERM", dto.DataTermino.ToShortDateString().Replace("/", "-"));
                else
                    BaseDados.AddParameter("DATA_TERM", DBNull.Value);

                BaseDados.AddParameter("FILTRO", dto.Filtro);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) 
                {
                    dto = new AtendimentoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.DataRegisto = DateTime.Parse(dr[1].ToString());
                    dto.DataInicio = DateTime.Parse(dr[2].ToString()); 
                    dto.Paciente =  dr[4].ToString();
                    dto.PlanoSaude = dr[5].ToString();
                    dto.ActoPrincipal = dr[6].ToString();
                    dto.Especialidade = dr[7].ToString(); 
                    dto.Profissional = dr[8].ToString(); 
                    dto.Departamento = dr[9].ToString();
                    dto.Filial = dr[10].ToString();
                    dto.Fotografia = dr[14].ToString(); 
                    dto.Registo = NumeroProcesso(dto.Codigo); 
                    
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

        public AtendimentoDTO ObterPorPK(AtendimentoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new AtendimentoDTO();

               if (dr.Read())
                {
                    

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Paciente = dr[2].ToString();
                    dto.PlanoSaude = "-1";
                    dto.Profissional = dr[4].ToString();
                    dto.Especialidade = dr[5].ToString();
                    dto.Situacao = dr[6].ToString();
                    dto.ActoPrincipal = dr[7].ToString();
                    dto.Obs = dr[8].ToString();
                    dto.Prioridade = dr[9].ToString();
                    dto.Ambulatorio = dr[10].ToString();

                    if (dr[11].ToString() != null && dr[11].ToString() != "")
                    {
                        dto.DataInicio = Convert.ToDateTime(dr[11].ToString());
                    }

                    if (dr[12].ToString() != null && dr[12].ToString() != "")
                    {
                        dto.DataTermino = Convert.ToDateTime(dr[12].ToString());
                    }
                    dto.Desfecho = dr[13].ToString();

                     
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
        public String NumeroProcesso(int codigo)
        {
            String matricula = "";

            if (codigo < 10)
            {
                matricula = "00000";
            }
            else if (codigo < 100)
            {
                matricula = "0000";
            }
            else if (codigo < 1000)
            {
                matricula = "000";
            }
            else if (codigo < 10000)
            {
                matricula = "00";
            }
            else if (codigo < 100000)
            {
                matricula = "0";
            }

            return matricula + codigo.ToString();

        }


        public void AdicionarAnamnese(AtendimentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_ANAMNESE_ADICIONAR";

                BaseDados.AddParameter("ATENDIMENTO", dto.Codigo);
                BaseDados.AddParameter("DOENCA", dto.HistoriaDoencaActual);
                BaseDados.AddParameter("ALERGIA", dto.Alergia);
                BaseDados.AddParameter("HSOCIAL", dto.HistoricoSocial);
                BaseDados.AddParameter("ADVERTENCIA", dto.Advertencias);
                BaseDados.AddParameter("HFAMILIAR", dto.HistoricoFamiliar);
                BaseDados.AddParameter("HPESSOAL", dto.HistoricoPessoal);
                BaseDados.AddParameter("HMEDICO", dto.HistoricoMedico);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();


            }catch(Exception ex){
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }
        }

        public void AddExamesFisicos(AtendimentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_EXAME_FISICO_ADICIONAR";

                BaseDados.AddParameter("ATENDIMENTO", dto.Codigo);
                BaseDados.AddParameter("CABECA", dto.Cabeca);
                BaseDados.AddParameter("CARDIACA", dto.AuscultacaoCardiaca);
                BaseDados.AddParameter("PULMONAR", dto.AuscultacaoPulmonar);
                BaseDados.AddParameter("TORAX", dto.ObsTorax);
                BaseDados.AddParameter("ABDOMEN", dto.Abdomen);
                BaseDados.AddParameter("NERVOSO", dto.SistemaNervoso);
                BaseDados.AddParameter("MEMBROS", dto.Membros);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

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

        public AtendimentoDTO ObterAnamnese(AtendimentoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_ANAMNESE_OBTERPORPK";

                BaseDados.AddParameter("ATENDIMENTO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new AtendimentoDTO();

                if (dr.Read())
                {
                   

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Alergia = dr[1].ToString();
                    dto.HistoricoSocial = dr[2].ToString();
                    dto.Advertencias = dr[3].ToString();
                    dto.HistoricoFamiliar = dr[4].ToString();
                    dto.HistoricoPessoal = dr[5].ToString();
                    dto.HistoricoMedico = dr[6].ToString();
                    dto.HistoriaDoencaActual = dr[7].ToString(); 
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

        public AtendimentoDTO ObterExameFisico(AtendimentoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_CLI_ADMISSAO_EXAME_FISICO_OBTERPORPK";

                BaseDados.AddParameter("ATENDIMENTO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new AtendimentoDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Cabeca = dr[1].ToString();
                    dto.AuscultacaoPulmonar = dr[2].ToString();
                    dto.AuscultacaoCardiaca = dr[3].ToString();
                    dto.ObsTorax = dr[4].ToString();
                    dto.Abdomen = dr[5].ToString();
                    dto.SistemaNervoso = dr[6].ToString();
                    dto.Membros = dr[7].ToString();
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

        public AtendimentoDTO AdicionarMarcacao(AtendimentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_MARCACAO_ADICIONAR";

                BaseDados.AddParameter("BOOKING_ID", dto.Codigo);
                BaseDados.AddParameter("PACIENTE_ID", dto.Paciente);
                BaseDados.AddParameter("COMPANY_ID", dto.Filial); 
                BaseDados.AddParameter("ESPECIALIDADE_ID", dto.Especialidade);
                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional); 
                BaseDados.AddParameter("SCHEDULE_DATE", dto.BookedDate);
                BaseDados.AddParameter("SCHEDULE_BEGIN", dto.BookedDate.TimeOfDay);
                BaseDados.AddParameter("SCHEDULE_END", dto.BookedDate.TimeOfDay);
                BaseDados.AddParameter("BOOKING_ID", dto.Status);  
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador); 

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

        public List<AtendimentoDTO> ObterMarcacoes(AtendimentoDTO dto)
        {
            List<AtendimentoDTO> lista = new List<AtendimentoDTO>();

            try
            {
                BaseDados.ComandText = "stp_CLI_MARCACAO_OBTERPORFILTRO";

                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Profissional); 
                BaseDados.AddParameter("ESPECIALIDADE_ID", dto.Especialidade);
                BaseDados.AddParameter("PACIENTE_ID", dto.Paciente);
                BaseDados.AddParameter("HORA", dto.BookedDate == DateTime.MinValue ? (object)DBNull.Value : dto.BookedDate.TimeOfDay);
                BaseDados.AddParameter("DATA_INI", dto.DataInicio == DateTime.MinValue ? (object)DBNull.Value : dto.DataInicio);
                BaseDados.AddParameter("DATA_TERM", dto.DataTermino == DateTime.MinValue ? (object)DBNull.Value : dto.DataTermino);
                BaseDados.AddParameter("STATUS_ID", dto.Status);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("FILIAL", dto.Filial);  

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AtendimentoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.PacienteID = int.Parse(dr[1].ToString());
                    dto.Paciente = dr[2].ToString(); 
                    dto.Filial = dr[3].ToString();
                    dto.EspecialidadeID = int.Parse(dr[4].ToString());
                    dto.Especialidade = dr[5].ToString();
                    dto.ProfissionalID = int.Parse(dr[6].ToString());
                    dto.Profissional = dr[7].ToString();
                    dto.BookedDate = DateTime.Parse(dr[8].ToString());
                    dto.BookedTime = DateTime.Parse(dr[9].ToString());
                    dto.Status = int.Parse(dr[10].ToString());
                    dto.Descricao = dr[11].ToString(); //Descricao Status

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
    }
}
