using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace DataAccessLayer.Geral
{
    public class TaskDAO :ConexaoDB
    {
        public TaskDTO Adicionar(TaskDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TASK_ADICIONAR";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("TASK_ID", dto.Task);
                AddParameter("TITULO", dto.Titulo);
                AddParameter("USER_OWNER", dto.UtilizadorProprietario);
                AddParameter("EXECUTOR_ID", dto.ExecutorID);
                AddParameter("SCHEDULE_DATE", dto.CreatedDate);
                AddParameter("SCHEDULE_END_DATE", dto.ScheduleEndDate);
                AddParameter("USER_EXECUTOR", dto.UtilizadorExecutor);
                AddParameter("TARGET_ID", dto.TargetID);
                AddParameter("PRIORITY", dto.PrioridadeID);
                AddParameter("TASK_STATUS", dto.Status);
                AddParameter("EXECUTION_DATE", dto.BeginImplementationDate == DateTime.MinValue ? (object)DBNull.Value : dto.BeginImplementationDate);
                AddParameter("DETAILS", dto.Details);
                AddParameter("PERSON_CONTACT", dto.PessoaContacto);
                AddParameter("CONTACT", dto.ContactoPessoaContacto);
                AddParameter("ALLDAY", dto.AllDay == true ? 1 : 0);
                AddParameter("RECURRENCE", dto.Recorrencia);
                AddParameter("END_DATE", dto.EndImplementationDate == DateTime.MinValue ? (object)DBNull.Value : dto.EndImplementationDate);
                AddParameter("REPORT", dto.TaskReport);
                AddParameter("INTERNAL_NOTES", dto.InternalNotes);
                AddParameter("COMPANY_ID", dto.Filial);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                dto.Codigo = ExecuteInsert();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            } 

            return dto;
        }


        public TaskDTO Delete(TaskDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TASK_DELETE";

                AddParameter("CODIGO", dto.Codigo); 
                AddParameter("@UTILIZADOR", dto.Utilizador);

                ExecuteNonQuery();
                dto.Sucesso = true; 
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<TaskDTO> ObterPorFiltro(TaskDTO dto)
        {
            List<TaskDTO> lista = new List<TaskDTO>();

            try
            {
                ComandText = "stp_GER_TASK_OBTERPORFILTRO";

                AddParameter("CODIGO", dto.Codigo); 
                AddParameter("@SCHEDULE_FROM", dto.ScheduleDate);
                AddParameter("@SCHEDULE_UNTIL", dto.ScheduleEndDate);
                AddParameter("@COMPANY_ID", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new TaskDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Task = new TipoActividadeDTO(int.Parse(dr[1].ToString()), dr[30].ToString());
                    dto.Titulo = dr[2].ToString();
                    dto.UtilizadorProprietario = dr[3].ToString();
                    dto.ExecutorID = int.Parse(dr[4].ToString() == "" ? "-1" : dr[4].ToString());
                    dto.ScheduleDate = dr[5].ToString()!="" ? DateTime.Parse(dr[5].ToString()) : DateTime.Now;
                    dto.UtilizadorExecutor = dr[6].ToString();
                    dto.TargetID = dr[7].ToString()!="" ? int.Parse(dr[7].ToString()) : -1;
                    dto.PrioridadeID = int.Parse(dr[8].ToString());
                    dto.Status = int.Parse(dr[9].ToString());
                    dto.BeginImplementationDate = !string.IsNullOrEmpty(dr[10].ToString()) ? DateTime.Parse(dr[10].ToString()) : DateTime.MinValue;
                    dto.Details = dr[11].ToString();
                    dto.PessoaContacto = dr[12].ToString();
                    dto.ContactoPessoaContacto = dr[13].ToString();
                    dto.AllDay = dr[14].ToString() == "1" ? true : false;
                    dto.Recorrencia = int.Parse(dr[15].ToString());
                    dto.EndImplementationDate = !string.IsNullOrEmpty(dr[16].ToString()) ? DateTime.Parse(dr[16].ToString()) : DateTime.MinValue;
                    dto.TaskReport = dr[17].ToString();
                    dto.InternalNotes = dr[18].ToString();
                    dto.Cancelled = dr[23].ToString() == "1" ? true : false;
                    dto.ScheduleEndDate = !string.IsNullOrEmpty(dr[27].ToString()) ? DateTime.Parse(dr[27].ToString()) : DateTime.MinValue;
                    dto.LookupField1 = dto.Task.Descricao;
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<TaskDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }
    }
}
