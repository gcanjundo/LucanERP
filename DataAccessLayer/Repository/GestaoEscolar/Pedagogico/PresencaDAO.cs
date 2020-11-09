using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class PresencaDAO 
    {
        readonly ConexaoDB BaseDados;

        public PresencaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PresencaDTO Salvar(PresencaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_FALTAS_ADICIONAR";

             
            
            try
            {
                BaseDados.AddParameter("@AULA_ID", dto.AulaID);
                BaseDados.AddParameter("@ALUNO", dto.MatriculaID);
                BaseDados.AddParameter("@FALTA", dto.Status);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@AVALIACAO", dto.NotaAvaliacao);

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


        
        public List<PresencaDTO> ListaPresenca(PresencaDTO dto)
        {
            
            

            List<PresencaDTO> lista = new List<PresencaDTO>();

            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_LISTA_PRESENCA";

                BaseDados.AddParameter("@ALUNO_ID", dto.MatriculaID);
                BaseDados.AddParameter("@AULA_ID", dto.AulaID);
                BaseDados.AddParameter("@PERIODO_ID", dto.Aula.PeriodoID);
                BaseDados.AddParameter("@INICIO", dto.DataIni == DateTime.MinValue ? (object)DBNull.Value : dto.DataIni);
                BaseDados.AddParameter("@TERMINO", dto.DataTerm == DateTime.MinValue ? (object)DBNull.Value : dto.DataTerm);
                BaseDados.AddParameter("@DISCIPLINA_ID", dto.Aula.Disciplina.Codigo);
                BaseDados.AddParameter("@TURMA", dto.Aula.Turma.Codigo);
                BaseDados.AddParameter("@DIA", dto.Aula.DiaSemana);

                MySqlDataReader dr = BaseDados.ExecuteReader();
               
                int ordem = 0;
                

                while (dr.Read())
                {
                    ordem++;


                    dto = new PresencaDTO
                    {
                        OrderNumber = ordem,
                        AulaID = int.Parse(dr[0]),
                        MatriculaID = int.Parse(dr[1]),
                        Status = dr[2],
                        AlunoID = int.Parse(dr[7]),
                        NroInscricao = dr[8],
                        SocialName = dr[9],


                        Aula = new AulaDTO
                        {
                            Turma = new TurmaDTO(int.Parse(dr[21]), dr[10]),
                            NroAula = int.Parse(dr[5]),
                            Hora = dr[20],
                            AvaliacaoID = int.Parse(dr[17]),
                            Tipo = dr[18],
                            Sumario = dr[6],
                            Docente = new DocenteDTO(dr[22], dr[23]),
                            Disciplina = new UnidadeCurricularDTO(0, new AnoCurricularDTO(int.Parse(dr[14]), new RamoDTO(int.Parse(dr[15]), dr[12]), -1, dr[11], ""), new PeriodoLectivoDTO(int.Parse(dr[16] == null ? "-1" : dr[16]), dr[4]), null, dr[19], "", -1, "", 1)
                            {
                                NomeDisciplina = dr[3],
                                AnoLectivo = int.Parse(dr[13])
                            }
                        },
                        NotaAvaliacao = decimal.Parse(dr[24])
                    };

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
