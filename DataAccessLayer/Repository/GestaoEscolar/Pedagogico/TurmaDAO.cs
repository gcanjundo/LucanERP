using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class TurmaDAO
    {
        readonly ConexaoDB BaseDados;

        public TurmaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public TurmaDTO Inserir(TurmaDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ADICIONAR";

                BaseDados.AddParameter("@PLANO", dto.Classe);
                BaseDados.AddParameter("@TURNO", dto.Turno);
                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@LOTACAO", dto.Lotacao);
                BaseDados.AddParameter("@SIGLA", dto.Sigla.Trim());
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@SALA", dto.Sala);
                BaseDados.AddParameter("@FILIAL",dto.Filial);
                BaseDados.AddParameter("@ANO_FROM",dto.AnosFrom);
                BaseDados.AddParameter("@MESES_FROM", dto.MesesFrom);
                BaseDados.AddParameter("@DIAS_FROM", dto.DiasFrom);
                BaseDados.AddParameter("@ANO_UNTIL", dto.AnosUntil);
                BaseDados.AddParameter("@MESES_UNTIL", dto.MesesUntil);
                BaseDados.AddParameter("@DIAS_UNTIL", dto.DiasUntil);
                BaseDados.AddParameter("@LOCALIZACAO", dto.Localizacao);
                BaseDados.AddParameter("@DIRECTOR", dto.Director);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public TurmaDTO Alterar(TurmaDTO dto)
        {
            
            
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALTERAR"; 
                BaseDados.AddParameter("@PLANO", dto.Classe);
                BaseDados.AddParameter("@TURNO", dto.Turno);
                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@LOTACAO", dto.Lotacao);
                BaseDados.AddParameter("@SIGLA", dto.Sigla.Trim());
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@SALA", dto.Sala);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ANO_FROM", dto.AnosFrom);
                BaseDados.AddParameter("@MESES_FROM", dto.MesesFrom);
                BaseDados.AddParameter("@DIAS_FROM", dto.DiasFrom);
                BaseDados.AddParameter("@ANO_UNTIL", dto.AnosUntil);
                BaseDados.AddParameter("@MESES_UNTIL", dto.MesesUntil);
                BaseDados.AddParameter("@DIAS_UNTIL", dto.DiasUntil);
                BaseDados.AddParameter("@LOCALIZACAO", dto.Localizacao);
                BaseDados.AddParameter("@DIRECTOR", dto.Director);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public void Apagar(TurmaDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_EXCLUIR";


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



        }
        
        public TurmaDTO ObterPorPK(TurmaDTO dto)
        {
            

            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_OBTERPORPK";

                if (string.IsNullOrEmpty(dto.Sigla))
                {
                    dto.Sigla = string.Empty;
                }
                BaseDados.AddParameter("@SIGLA", string.IsNullOrEmpty(dto.Sigla) ? string.Empty : dto.Sigla??dto.Sigla.Trim());
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
    
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new TurmaDTO();
                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["TUR_CODIGO"].ToString());
                    dto.Nome = dr["TUR_NOME"].ToString();
                    dto.Sigla = dr["TUR_ABREVIATURA"].ToString();
                    dto.Lotacao = int.Parse(dr["TUR_LOTACAO"].ToString());
                    dto.Status = int.Parse(dr["TUR_STATUS"].ToString());
                    dto.Classe = Int32.Parse(dr["TUR_CODIGO_PLANO_CURRICULAR"].ToString());
                    dto.Sala = dr["TUR_SALA"].ToString();
                    dto.Status = int.Parse(dr["TUR_STATUS"].ToString());
                    dto.Turno = dr["TUR_TURNO"].ToString();
                    dto.AnosFrom = dr["TUR_ANOS_FROM"].ToString();
                    dto.MesesFrom = dr["TUR_MESES_FROM"].ToString();
                    dto.DiasFrom = dr["TUR_DIAS_FROM"].ToString();
                    dto.AnosUntil = dr["TUR_ANOS_UNTIL"].ToString();
                    dto.MesesUntil = dr["TUR_MESES_UNTIL"].ToString();
                    dto.DiasUntil = dr["TUR_DIAS_UNTIL"].ToString();
                    dto.Localizacao = dr["TUR_LOCALIZACAO"].ToString();
                    dto.Director = dr["TUR_DIRECTOR"].ToString() == string.Empty ? "-1" : dr["TUR_DIRECTOR"].ToString();
                    dto.AnoLectivo = int.Parse(dr["TUR_ANO_LECTIVO"].ToString() == string.Empty ? "-1" : dr["TUR_ANO_LECTIVO"].ToString());
                    dto.Curso = int.Parse(dr["PLAN_CODIGO_RAMO"].ToString());
                    dto.Ano = dto.AnoLectivo;
                    
                }
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

        public List<TurmaDTO> ObterPorFiltro(TurmaDTO dto)
        {


            List<TurmaDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_OBTERPORFILTRO";

               
                BaseDados.AddParameter("@PLANO", dto.Classe);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@TURNO", dto.Turno);
                if (dto.Sucesso)
                {

                    BaseDados.AddParameter("@TIPO", "P");
                }
                else
                {

                    BaseDados.AddParameter("@TIPO", "");
                }
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<TurmaDTO>();
                while (dr.Read())
                {
                    dto = new TurmaDTO();
                    dto.Codigo = Int32.Parse(dr["TUR_CODIGO"].ToString());
                    dto.Nome = dr["CUR_NOME"].ToString();
                    dto.Sigla = dr["TUR_ABREVIATURA"].ToString();
                    dto.Lotacao = int.Parse(dr["TUR_LOTACAO"].ToString());
                    dto.Status = int.Parse(dr["TUR_STATUS"].ToString());
                    dto.Classe = Int32.Parse(dr["TUR_CODIGO_PLANO_CURRICULAR"].ToString());
                    dto.Sala = dr["TUR_SALA"].ToString();
                    dto.Status = int.Parse(dr["TUR_STATUS"].ToString());
                    dto.Turno = dr["TUR_TURNO"].ToString(); 
                    dto.strClasse = dr["PLAN_DESCRICAO"].ToString();

                    dto.AnosFrom = dr["TUR_ANOS_FROM"].ToString();
                    dto.MesesFrom = dr["TUR_MESES_FROM"].ToString();
                    dto.DiasFrom = dr["TUR_DIAS_FROM"].ToString();
                    dto.AnosUntil = dr["TUR_ANOS_UNTIL"].ToString();
                    dto.MesesUntil = dr["TUR_MESES_UNTIL"].ToString();
                    dto.DiasUntil = dr["TUR_DIAS_UNTIL"].ToString();
                    dto.Localizacao = dr["TUR_LOCALIZACAO"].ToString();
                    dto.Director = dr["ENT_NOME_COMPLETO"].ToString() + " - " + dr["ENT_TELEFONE"].ToString();

                    if (dto.Turno == "M")
                    {
                        dto.Turno = "MANHÃ";
                    }
                    else if (dto.Turno == "T")
                    {
                        dto.Turno = "TARDE";
                    }
                    else if (dto.Turno == "N")
                    {
                        dto.Turno = "NOITE";
                    }
                    else if (dto.Turno == "U")
                    {
                        dto.Turno = "ÚNICO";
                    }

                    if (dr["TUR_TIPO"].ToString() == "P")
                    {
                        dto.Sucesso = true;
                    }
                    else
                    {
                        dto.Sucesso = false;
                    }

                    dto.AnoLectivo = int.Parse(dr["TUR_ANO_LECTIVO"].ToString() == string.Empty ? "-1" : dr["TUR_ANO_LECTIVO"].ToString());
                    dto.Curso = int.Parse(dr["CUR_CODIGO"].ToString());
                    dto.AnoCivil = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<TurmaDTO>();
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

        public TurmaDTO Lotacao(int turma, int ano) 
        {
            TurmaDTO dto = new TurmaDTO();
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_OBTERLOTACAO";

                BaseDados.AddParameter("@ANO", ano);
                BaseDados.AddParameter("@TURMA", turma);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) 
                {
                    if (!dr[0].Equals(null) && !dr[0].Equals(""))
                    {
                        dto.Matriculados = int.Parse(dr[0]);
                        dto.Masculinos = int.Parse(dr[1]);
                        dto.Femininos = int.Parse(dr[2]);
                    } 
                }
            }finally 
            {
                BaseDados.FecharConexao();
            }

             return dto;
        }

        public string Incluir(TurmaAlunoDTO dto) 
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_ADICIONAR";
                BaseDados.AddParameter("@TURMA", dto.Turma.Codigo);
                BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);

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

            return dto.MensagemErro;
        }

        public List<HorarioDTO> ObterDocentes(TurmaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_TURMA_OBTERDOCENTES";


            BaseDados.AddParameter("@TURMA", dto.Codigo);


            List<HorarioDTO> lista = new List<HorarioDTO>();
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    HorarioDTO turma = new HorarioDTO();


                    turma.HorDocente = new DocenteDTO("0", dr["ENT_NOME_COMPLETO"].ToString());
                    turma.HorDisiciplina = new UnidadeCurricularDTO(dr["DIS_DESCRICAO"].ToString());
                    turma.HorTurma = new TurmaDTO(0, 0, "", dr["AS TUR_ABREVIATURA"].ToString(), 0, 1, "", "", "-1", -1);
                    lista.Add(turma);

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

        public List<TurmaDTO> ObterPorClasse(TurmaDTO dto)
        {


            List<TurmaDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_OBTERPORCLASSE";


                BaseDados.AddParameter("@PLANO", dto.Classe);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@CURSO", dto.Curso); 
               

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<TurmaDTO>();
                while (dr.Read())
                {
                    dto = new TurmaDTO();
                    dto.Codigo = Int32.Parse(dr["TUR_CODIGO"].ToString());
                    dto.Sigla = dr["TURMA"].ToString();
                    dto.AnoLectivo = int.Parse(dr["TUR_ANO_LECTIVO"].ToString());
                    dto.Lotacao = int.Parse(dr["TUR_LOTACAO"].ToString());
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                lista = new List<TurmaDTO>();
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


        public List<TurmaDisciplinaDTO> ObterStatistic(TurmaDTO dto)
        {


            List<TurmaDisciplinaDTO> lista = new List<TurmaDisciplinaDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_MAPA_ESTATISTICA_TRIMESTRAL";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@PERIODO", dto.Turno);
                

                MySqlDataReader dr = BaseDados.ExecuteReader();
                 
                while (dr.Read())
                {
                   TurmaDisciplinaDTO  dados = new TurmaDisciplinaDTO();
                    dados.TurmaID = Int32.Parse(dr[0]);
                    dados.Curso = dr[1];
                    dados.AreaFormacao = dr[2];
                    dados.AnoCurricular = dr[3];
                    dados.TotalMatriculados = Int32.Parse(dr[4]);
                    dados.MatriculadosFemininos = Int32.Parse(dr[5]);
                    dados.TotalDesistentes = Int32.Parse(dr[6]);
                    dados.DesistentesFemininos = Int32.Parse(dr[7]);
                    dados.TotalAvaliados = Int32.Parse(dr[8]);
                    dados.AvaliadosFemininos = Int32.Parse(dr[9]);
                    dados.TotalPositivas = Int32.Parse(dr[10]);
                    dados.PositivasFemininos = Int32.Parse(dr[11]);
                    dados.TotalPercentagemPositivas = 0;
                    dados.TotalNegativas = Int32.Parse(dr[13]);
                    dados.NegativasFemininos = Int32.Parse(dr[14]);
                    dados.PercentagemNegativas = 0;


                    if (lista.Exists(t => t.AnoCurricular == dados.AnoCurricular))
                    {
                        var item = lista.Where(t => t.AnoCurricular == dados.AnoCurricular).SingleOrDefault();
                        lista = lista.Where((t => t.AnoCurricular != item.AnoCurricular)).ToList();

                        dados.TotalMatriculados += item.TotalMatriculados;
                        dados.MatriculadosFemininos += item.MatriculadosFemininos;
                        dados.TotalDesistentes += item.TotalDesistentes;
                        dados.DesistentesFemininos += item.DesistentesFemininos;
                        dados.TotalAvaliados += item.TotalAvaliados;
                        dados.AvaliadosFemininos += item.AvaliadosFemininos;
                        dados.TotalPositivas += item.TotalPositivas;
                        dados.PositivasFemininos += item.PositivasFemininos;
                        dados.TotalPercentagemPositivas = 0;
                        dados.TotalNegativas += item.TotalNegativas;
                        dados.NegativasFemininos += item.NegativasFemininos;
                        dados.PercentagemNegativas = 0;
                    }

                    lista.Add(dados);

                }

            }
            catch (Exception ex)
            {
                lista = new List<TurmaDisciplinaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(new TurmaDisciplinaDTO(dto, null, null));
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
