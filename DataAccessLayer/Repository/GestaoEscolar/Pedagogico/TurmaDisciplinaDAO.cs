using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class TurmaDisciplinaDAO
    {

        readonly ConexaoDB BaseDados;

        public TurmaDisciplinaDAO()
        {
            BaseDados = new ConexaoDB();

        }
        public TurmaDisciplinaDTO Adicionar(TurmaDisciplinaDTO dto) 
        {
            try 
            {
                BaseDados.ComandText = "stp_ACA_TURMA_DISCIPLINA_ADICIONAR";

                
                BaseDados.AddParameter("TURMA", dto.Turma.Codigo);
                BaseDados.AddParameter("DISCIPLINA", dto.Disciplina.Codigo);
                BaseDados.AddParameter("DOCENTE", dto.Docente.Codigo);
                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true; 
            }catch(Exception ex)
            {
              dto.Sucesso =false;
              dto.MensagemErro = ex.Message.Replace("'", "");
            }finally
            {
              BaseDados.FecharConexao();

            }
            return dto;
        }

        
        public List<TurmaDisciplinaDTO> ListaDisciplinas(TurmaDisciplinaDTO dto) 
        {
            List<TurmaDisciplinaDTO> lista;
            try
            {
                lista = new List<TurmaDisciplinaDTO>();
                BaseDados.ComandText = "stp_ACA_TURMA_DISCIPLINA_OBTERPORFILTRO";
                 
                BaseDados.AddParameter("TURMA", dto.Turma.Codigo);
                BaseDados.AddParameter("PERIODO", dto.Disciplina.Periodo);
                BaseDados.AddParameter("ANO", dto.Disciplina.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) 
                {
                    dto = new TurmaDisciplinaDTO();

                    dto.Turma = new TurmaDTO(int.Parse(dr["TUR_DIS_CODIGO_TURMA"].ToString()), -1, "", dr["TUR_ABREVIATURA"].ToString(), -1, 1, dr["TUR_SALA"].ToString(),
                    dr["TUR_TURNO"].ToString(), "-1", -1);

                    dto.Disciplina = new DisciplinaDTO(int.Parse(dr["TUR_DIS_CODIGO_DISCIPLINA"].ToString()), dr["DISCIPLINA"].ToString());

                    if (!string.IsNullOrEmpty(dr["TUR_DIS_CODIGO_DOCENTE"].ToString()))
                    {
                        dto.Docente = new DocenteDTO(dr["TUR_DIS_CODIGO_DOCENTE"].ToString(), dr["ENT_NOME_COMPLETO"].ToString());
                    }
                    else
                    {
                        dto.Docente = new DocenteDTO(-1);
                    }
                    
                    dto.CargaHoraria = decimal.Parse(dr["DIS_PLAN_CARGA_HORARIA"].ToString());
                    lista.Add(dto);
                }

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                lista = new List<TurmaDisciplinaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return lista;
        }

        public List<TurmaDisciplinaDTO> MapaAproveitamento(TurmaDisciplinaDTO dto)
        {
            List<TurmaDisciplinaDTO> lista;
            try
            {
                lista = new List<TurmaDisciplinaDTO>();
                BaseDados.ComandText = "stp_ACA_TURMA_DISCIPLINA_APROVEITAMENTO";

                BaseDados.AddParameter("TURMA", dto.Turma.Codigo); 
                BaseDados.AddParameter("ANO", dto.Disciplina.AnoLectivo); 

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new TurmaDisciplinaDTO();

                    dto.Turma = new TurmaDTO(int.Parse(dr["TUR_DIS_CODIGO_TURMA"].ToString()), -1, "", dr["TUR_ABREVIATURA"].ToString(), -1, 1, "",
                    dr["TUR_TURNO"].ToString(), "-1", -1);

                    dto.Disciplina = new DisciplinaDTO(int.Parse(dr["TUR_DIS_CODIGO_DISCIPLINA"].ToString()), dr["DISCIPLINA"].ToString());

                    if (!string.IsNullOrEmpty(dr["TUR_DIS_CODIGO_DOCENTE"].ToString()))
                    {
                        dto.Docente = new DocenteDTO(dr["TUR_DIS_CODIGO_DOCENTE"].ToString(), dr["ENT_NOME_COMPLETO"].ToString());
                    }
                    else
                    {
                        dto.Docente = new DocenteDTO(-1);
                    }

                    dto.CargaHoraria = decimal.Parse(dr["DIS_PLAN_CARGA_HORARIA"].ToString());
                    dto.MatriculadosMasculinos = int.Parse(dr["MAS_MATRICULADOS"].ToString());
                    dto.MatriculadosFemininos = int.Parse(dr["FEM_MATRICULADOS"].ToString());
                    dto.TotalMatriculados = int.Parse(dr["TOTAL_MATRICULADOS"].ToString());
                    dto.AreaFormacao = dr["CUR_AREA_FORMACAO"].ToString();
                    dto.Curso = dr["CUR_NOME"].ToString();
                    dto.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    lista.Add(dto);
                }

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                lista = new List<TurmaDisciplinaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return lista;
        } 
    }
}
