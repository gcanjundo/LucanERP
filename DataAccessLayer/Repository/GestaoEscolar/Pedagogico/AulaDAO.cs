using Dominio.GestaoEscolar.Pedagogia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    

    public class AulaDAO
    {
        readonly ConexaoDB BaseDados;

        public AulaDAO()
        {
            BaseDados = new ConexaoDB();
        }


        public AulaDTO Adicionar(AulaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_HORARIO_AULA_ADICIONAR"; 

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@HORARIO", dto.Hora);
                BaseDados.AddParameter("@DATA_AULA", dto.Data);
                BaseDados.AddParameter("@TURMA_ID", dto.TurmaID);
                BaseDados.AddParameter("@DISCIPLINA_ID", dto.DisciplinaID);
                BaseDados.AddParameter("@DOCENTE_ID", dto.DocenteID);
                BaseDados.AddParameter("@SUMARIO", dto.Sumario);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@TIPO_AULA", dto.Tipo);
                BaseDados.AddParameter("@TPC", dto.TarefaExtra);
                BaseDados.AddParameter("@PERIODO_ID", dto.PeriodoID);
                BaseDados.AddParameter("@AVALIACAO_ID", dto.AvaliacaoID <=0 ? (object)DBNull.Value : dto.AvaliacaoID);
                BaseDados.AddParameter("@DIA", dto.DiaSemana);
                BaseDados.AddParameter("@HORA", (dto.Hora.Split(' ')[0]).Replace(":", string.Empty));
                BaseDados.AddParameter("@OBSERVACOES", dto.Observacoes);
                BaseDados.AddParameter("@SALA", dto.Sala);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                dto.Codigo = BaseDados.ExecuteInsert();
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

        public AulaDTO Excluir(AulaDTO dto)
        {

            BaseDados.ComandText = "stp_ACA_HORARIO_AULA_EXCLUIR";


            BaseDados.AddParameter("@CODIGO", dto.Codigo);
            BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

            try
            {

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

        public List<AulaDTO> ObterPorFiltro(AulaDTO dto)
        {

            BaseDados.ComandText = "stp_ACA_HORARIO_AULA_OBTERPORFILTRO";
            var lista = new List<AulaDTO>();

            try
            {
                
                BaseDados.AddParameter("@DATA_INI", dto.Data == DateTime.MinValue ? (object) DBNull.Value : dto.Data);
                BaseDados.AddParameter("@DATA_TERM", dto.DataTerminoMatricula == DateTime.MinValue.ToString() ? (object)DBNull.Value : dto.DataTerminoMatricula);
                BaseDados.AddParameter("@TURMA", dto.TurmaID);
                BaseDados.AddParameter("@DISCIPLINA", dto.DisciplinaID);
                BaseDados.AddParameter("@DOCENTE", dto.DocenteID);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                foreach (var dr in reader)
                {
                    dto = new AulaDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.NroAula = dr[1] == null ? 1 : int.Parse(dr[1]);
                    dto.Hora = dr[2].Length == 3 ? "0" + dr[2] : dr[2];
                    dto.Turma = new TurmaDTO(int.Parse(dr[3]), dr[25]);
                    dto.Disciplina = new UnidadeCurricularDTO(int.Parse(dr[4]))
                    {
                        NomeDisciplina = dr[27]
                    };
                    dto.Docente = new DocenteDTO(dr[5], dr[26]);
                    dto.Data = DateTime.Parse(dr[6]);
                    dto.Sumario = dr[7];
                    dto.Tipo = dr[9];
                    dto.TarefaExtra = dr[10];
                    dto.PeriodoID = dr[11] == null ? -1 : int.Parse(dr[11]);
                    dto.AvaliacaoID = int.Parse(dr[12] == null ? "-1" : dr[12]);
                    dto.DiaSemana = int.Parse(dr[13]);
                    dto.Observacoes = dr[15];
                    dto.Sala = dr[28];
                    dto.AnoLectivo = int.Parse(dr[24]);
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

        public AulaDTO ObterPorPK(AulaDTO dto)
        {

            BaseDados.ComandText = "stp_ACA_HORARIO_AULA_OBTERPORPK";
             
            try
            {
                BaseDados.AddParameter("@CODIGO", dto.Codigo);


                MySqlDataReader dr = BaseDados.ExecuteReader();

                foreach (var dr in reader)
                {
                    dto = new AulaDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.NroAula = dr[1] == null ? 1 : int.Parse(dr[1]);
                    dto.Hora = dr[2].Length == 3 ? "0" + dr[2] : dr[2];
                    dto.TurmaID = int.Parse(dr[3]);
                    dto.DisciplinaID = int.Parse(dr[4]);
                    dto.DocenteID = int.Parse(dr[5] == null ? "-1" : dr[5]);
                    dto.Data = DateTime.Parse(dr[6]);
                    dto.Sumario = dr[7];
                    dto.Tipo = dr[9];
                    dto.TarefaExtra = dr[10];
                    dto.PeriodoID = dr[11] == null ? -1 : int.Parse(dr[11]);
                    dto.AvaliacaoID = int.Parse(dr[12] == null ? "-1" : dr[12]);
                    dto.DiaSemana = int.Parse(dr[13]);
                    dto.Observacoes = dr[15];
                    dto.Sala = dr[16];
                    dto.AnoLectivo = int.Parse(dr[24]);
                    dto.Turma = new TurmaDTO(int.Parse(dr[29]), int.Parse(dr[28]));
                    break;
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
