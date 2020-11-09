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
    public class DisciplinaPlanoDAO
    {

        readonly ConexaoDB BaseDados;

        public DisciplinaPlanoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public UnidadeCurricularDTO Inserir(UnidadeCurricularDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_ADICIONAR";

                BaseDados.AddParameter("@PLANO", dto.PlanoCurricular.Codigo);

                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina.Codigo);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                /*
                if (dto.Tipo.Equals("1"))
                {
                    BaseDados.AddParameter("@PERIODO", 0);
                }
                else
                {
                    
                }*/
                BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivo.Codigo);
                BaseDados.AddParameter("@CLASSIFICACAO", dto.Classificacao);
                BaseDados.AddParameter("@CARGA", dto.CargaHoraria);
                BaseDados.AddParameter("@CONTEUDO", dto.Conteudo);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@TURNO", dto.Turma);
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

        public UnidadeCurricularDTO Alterar(UnidadeCurricularDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_ALTERAR";





                BaseDados.AddParameter("@PLANO", dto.PlanoCurricular.Codigo);

                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina.Codigo);
                BaseDados.AddParameter("@TIPO", dto.Tipo);

                BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivo.Codigo);
                BaseDados.AddParameter("@CLASSIFICACAO", dto.Classificacao);
                BaseDados.AddParameter("@CARGA", dto.CargaHoraria);
                BaseDados.AddParameter("@CONTEUDO", dto.Conteudo);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UNIDADE", dto.Disciplina.Descricao.ToUpper());
                BaseDados.AddParameter("@SIGLA", dto.Disciplina.Sigla);
                BaseDados.AddParameter("@TURNO", dto.Turma);

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

        public UnidadeCurricularDTO AddOtherInfo(UnidadeCurricularDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_ADDOTHERINFO"; 
                 
                BaseDados.AddParameter("@CLASSIFICACAO", dto.Classificacao);
                BaseDados.AddParameter("@CARGA", dto.CargaHoraria);  
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ORDEM", dto.Ordem);
                BaseDados.AddParameter("@UNIDADE", dto.DocumentDesignation);
                BaseDados.AddParameter("@PROVA_GLOBAL", dto.AllowPG == true ? 1 : 0);
                BaseDados.AddParameter("@EXAME", dto.AllowEX == true ? 1 : 0);
                BaseDados.AddParameter("@RECURSO", dto.AllowRC == true ? 1 : 0);
                BaseDados.AddParameter("@PERIODO_ID", dto.PeriodoLectivo.Codigo <=0 ? (object)DBNull.Value : dto.PeriodoLectivo.Codigo);
                BaseDados.AddParameter("@NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("@TEORIA", dto.CargaTeorica);
                BaseDados.AddParameter("@PRATICA", dto.CargaTeoriaPratica);
                BaseDados.AddParameter("@TRABALHOS", dto.PraticaLaboratorial);
                BaseDados.AddParameter("@CARGA_PERIODO", dto.CargaHorariaPeriodo);
                BaseDados.AddParameter("@NOME", dto.NomeDisciplina);
                BaseDados.AddParameter("@PROCEDENTE", dto.PrecedenteID <= 0 ? (object)DBNull.Value : dto.PrecedenteID);
                BaseDados.AddParameter("FAMILIA", string.Empty);
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

        public void Apagar(UnidadeCurricularDTO dto)
        {
           

            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_EXCLUIR";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
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



        }

        public UnidadeCurricularDTO ObterPorPK(UnidadeCurricularDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_OBTERPORPK";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                if (dto.Disciplina != null)
                {
                    BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina.Codigo);
                }
                else
                {
                    BaseDados.AddParameter("@DISCIPLINA", -1);
                }

                if (dto.PlanoCurricular != null)
                {
                    BaseDados.AddParameter("@PLANO", dto.PlanoCurricular.Codigo);
                }
                else
                {
                    BaseDados.AddParameter("@PLANO", -1);
                }

                if (dto.PeriodoLectivo != null)
                {
                    BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivo.Codigo);
                }
                else
                {
                    BaseDados.AddParameter("@PERIODO", -1);
                }

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new UnidadeCurricularDTO();

                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["DIS_PLAN_CODIGO"].ToString());

                 

                    dto.PeriodoLectivo = new PeriodoLectivoDTO(Int32.Parse(dr["DIS_PLAN_CODIGO_PERIODO"].ToString()), dr["DIS_PLAN_CODIGO_PERIODO"].ToString(), -1, 1);
                    dto.PlanoCurricular = new AnoCurricularDTO(Int32.Parse(dr["DIS_PLAN_CODIGO_PLANO"].ToString()), dr["PLAN_DESCRICAO"].ToString());
                    dto.Disciplina = new DisciplinaDTO(Int32.Parse(dr["DIS_PLAN_CODIGO_DISCIPLINA"].ToString()), dr["DIS_DESCRICAO"].ToString(), dr["DIS_SIGLA"].ToString());

                    dto.CargaHoraria = Int32.Parse(dr["DIS_PLAN_CARGA_HORARIA"].ToString());
                    dto.Classificacao = dr["DIS_PLAN_NIVEL"].ToString();

                    
                    dto.Tipo = dr["DIS_PLAN_TIPO"].ToString();
                    dto.Conteudo = dr["DIS_PLAN_CONTEUDO_PROGRAMATICO"].ToString();
                    dto.Status = int.Parse(dr["DIS_PLAN_STATUS"].ToString());
                    if (!dto.Classificacao.Equals("-1"))
                    {
                        dto.NomeDisciplina = dto.Disciplina.Descricao.ToUpper() + " " + dto.Classificacao;
                    }
                    else
                    {
                        dto.NomeDisciplina = dto.Disciplina.Descricao.ToUpper();
                    } 
                    dto.Turma = dr["DIS_PLAN_TURNO"].ToString();
                    dto.Creditos = dr["DIS_PLAN_CREDITO"].ToString();
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

        public List<UnidadeCurricularDTO> ObterPorFiltro(UnidadeCurricularDTO dto)
        {


            List<UnidadeCurricularDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_OBTERPORFILTRO";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivo.Codigo);
                BaseDados.AddParameter("@CURSO", dto.PlanoCurricular.Ramo.RamCurso.Codigo);
                BaseDados.AddParameter("@RAMO", dto.PlanoCurricular.Ramo.RamCodigo);
                BaseDados.AddParameter("@CLASSE", dto.PlanoCurricular.AnoCurricular);
                BaseDados.AddParameter("@PLANO", dto.PlanoCurricular.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<UnidadeCurricularDTO>();
                
                while (dr.Read())
                {
                    dto = new UnidadeCurricularDTO();

                    dto.Codigo = Int32.Parse(dr["DIS_PLAN_CODIGO"].ToString());

                    AnoCurricularDTO dtoPlano = new AnoCurricularDTO();
                    dtoPlano.Codigo = Int32.Parse(dr["DIS_PLAN_CODIGO_PLANO"].ToString());
                    dtoPlano.Descricao = dr["PLAN_DESCRICAO"].ToString();
                    dtoPlano.Ramo = new RamoDTO(int.Parse(dr["CUR_CODIGO"].ToString()), dr["CUR_NOME"].ToString(), null, dr["CUR_ABREVIATURA"].ToString(), 0, 0, 1, dr["CUR_AREA_FORMACAO"].ToString());
                     
                    if (!dr["DIS_PLAN_CODIGO_PERIODO"].ToString().Equals("0"))
                    {
                        dto.PeriodoLectivo = new PeriodoLectivoDTO(int.Parse(dr["DIS_PLAN_CODIGO_PERIODO"].ToString()), dr["PER_DESCRICAO"].ToString(), -1, 1);
                    }
                    else
                    {
                        dto.PeriodoLectivo = new PeriodoLectivoDTO(0, "ANUAL", -1, 1); 

                    } 

                    
                    dto.PlanoCurricular = dtoPlano;
                    dto.Disciplina = new DisciplinaDTO(int.Parse(dr["DIS_PLAN_CODIGO_DISCIPLINA"].ToString()), dr["DIS_DESCRICAO"].ToString(), dr["DIS_SIGLA"].ToString()); 

                    dto.CargaHoraria = Int32.Parse(dr["DIS_PLAN_CARGA_HORARIA"].ToString());
                    dto.Classificacao = dr["DIS_PLAN_NIVEL"].ToString();


                    dto.Tipo = dr["DIS_PLAN_TIPO"].ToString();
                    dto.Conteudo = dr["DIS_PLAN_CONTEUDO_PROGRAMATICO"].ToString();
                    dto.Status = int.Parse(dr["DIS_PLAN_STATUS"].ToString());
                    dto.Creditos = dr["DIS_PLAN_CREDITO"].ToString();
                    dto.NomeDisciplina = dto.Disciplina.Sigla.ToUpper()+" - " +dto.Disciplina.Descricao.ToUpper() + " (" + dto.PeriodoLectivo.Descricao+")";
                    dto.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dto.NivelEnsino = dr["CUR_FORMACAO"].ToString();
                    dto.Turma = dr["DIS_PLAN_TURNO"].ToString();
                    dto.AllowEX = dr["DIS_PLAN_EXAME"].ToString() == "1" ? true : false;
                    dto.AllowPG = dr["DIS_PLAN_PROVA_GLOBAL"].ToString() == "1" ? true : false;
                    dto.AllowRC = dr["DIS_PLAN_RECURSO"].ToString() == "1" ? true : false;
                    dto.NomeDisciplina = dr["DIS_DESCRICAO"].ToString();
                    dto.DocumentDesignation = dr["DIS_PLAN_DESIGNACAO"].ToString() != string.Empty ? dr["DIS_PLAN_DESIGNACAO"].ToString() : dto.Disciplina.Sigla;
                    dto.Ordem = int.Parse(dr["DIS_PLAN_ORDEM"].ToString() ?? "1");
                    dto.CargaTeorica = int.Parse(dr["DIS_PLAN_TEORIA"].ToString() ?? "0");
                    dto.CargaTeoriaPratica = int.Parse(dr["DIS_PLAN_TEORIA_PRATICA"].ToString() ?? "0");
                    dto.PraticaLaboratorial = int.Parse(dr["DIS_PLAN_NRO_TRABALHOS"].ToString() ?? "0");
                    dto.Nivel = dr["DIS_PLAN_NIVEL"].ToString();
                    dto.CargaHorariaPeriodo = int.Parse(dr["DIS_PLAN_HORAS_SEMESTRAL"].ToString() ?? "0");
                    dto.PrecedenteID = int.Parse(dr["DIS_PLAN_PROCEDENTE"].ToString() ?? "-1");
                    dto.AnoCivil = int.Parse(dr["DIS_PLAN_ANO_LECTIVO"].ToString());
                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                dto = new UnidadeCurricularDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.NomeDisciplina = dto.MensagemErro;
                lista = new List<UnidadeCurricularDTO>();
                lista.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public UnidadeCurricularDTO AddPrecedente(UnidadeCurricularDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_ACA_DISCIPLINA_PLANO_ADDPRECEDENTE";

                 
                BaseDados.AddParameter("@DISCIPLINA_ID", dto.Codigo);
                 
                BaseDados.AddParameter("@PRECEDENTE_ID", dto.PrecedenteID <= 0 ? (object)DBNull.Value : dto.PrecedenteID);
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
    }
}
