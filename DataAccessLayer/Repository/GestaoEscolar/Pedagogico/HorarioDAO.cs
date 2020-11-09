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
    public class HorarioDAO
    {


        readonly ConexaoDB BaseDados;

        public HorarioDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public void Apagar(HorarioDTO dto)
        {
           
            try
            {
                BaseDados.ComandText = "stp_ACA_HORARIO_EXCLUIR";

                BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo); 

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

        public void ExcluirHorarioDocente(HorarioDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_ACA_HORARIO_DOCENTE_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.HorCodigo);

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

        public HorarioDTO Salvar(HorarioDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_ACA_HORARIO_ADICIONAR";


                BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo);
                BaseDados.AddParameter("@INICIO", dto.HorInicio);
                BaseDados.AddParameter("@TERMINO", dto.HorTermino);
                BaseDados.AddParameter("@SEGUNDA", dto.SegundaFeira);
                BaseDados.AddParameter("@TERCA", dto.TercaFeira);
                BaseDados.AddParameter("@QUARTA", dto.QuartaFeira);
                BaseDados.AddParameter("@QUINTA", dto.QuintaFeira);
                BaseDados.AddParameter("@SEXTA", dto.SextaFeira);
                BaseDados.AddParameter("@SABADO", dto.Sabado);
                BaseDados.AddParameter("DURACAO", dto.Duracao);
                BaseDados.AddParameter("@ANO", dto.HorAnoLectivo.AnoCodigo);
                BaseDados.AddParameter("@PERIODO", dto.HorPeriodo);
                BaseDados.AddParameter("FILIAL", dto.Filial);
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

        public List<HorarioDTO> ObterPorFiltro(HorarioDTO dto)
        {

            List<HorarioDTO> lista = new List<HorarioDTO>();
            try
            {


                BaseDados.ComandText = "stp_ACA_HORARIO_OBTERPORFILTRO";


                BaseDados.AddParameter("@HORA", -1);
                BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo);
                BaseDados.AddParameter("@CURSO", dto.HorTurma.Curso);
                BaseDados.AddParameter("@PERIODO", dto.HorPeriodo);
                BaseDados.AddParameter("@ANO", dto.HorAnoLectivo.AnoCodigo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new HorarioDTO();

                    dto.HorInicio = int.Parse(dr["HOR_INICIO"].ToString());
                    dto.HorTermino = int.Parse(dr["HOR_TERMINO"].ToString());

                    TurmaDTO dtoTurma = new TurmaDTO();
                    dtoTurma.Codigo = int.Parse(dr["HOR_CODIGO_TURMA"].ToString());
                    dtoTurma.Sigla = dr["TURMA"].ToString();
                    dtoTurma.Turno = dr["TUR_TURNO"].ToString();
                    dtoTurma.Classe = int.Parse(dr["PLAN_CODIGO"].ToString());
                    dto.HorTurma = dtoTurma;
                    UnidadeCurricularDTO dtoDisciplina = new UnidadeCurricularDTO();
                    
                    dtoDisciplina.Conteudo = dr["COOR_DESCRICAO"].ToString(); 
                    dtoDisciplina.PlanoCurricular = new AnoCurricularDTO(int.Parse(dr["PLAN_CODIGO"].ToString()), new RamoDTO(int.Parse(dr["CUR_CODIGO"].ToString()), dr["CUR_ESPECIFICACAO"].ToString(), null, "", 0, 0, 1, ""),
                    -1, dr["ANO_CURRICULAR"].ToString(), dr["CURSO"].ToString());
                    dto.HorDisiciplina = dtoDisciplina;
                    dto.SegundaFeira = dr["HOR_SEGUNDA"].ToString();
                    dto.TercaFeira = dr["HOR_TERCA"].ToString();
                    dto.QuartaFeira = dr["HOR_QUARTA"].ToString();
                    dto.QuintaFeira = dr["HOR_QUINTA"].ToString();
                    dto.SextaFeira = dr["HOR_SEXTA"].ToString();
                    dto.Sabado = dr["HOR_SABADO"].ToString();
                    dto.Horario = HorarioAula(dto.HorInicio, dto.HorTermino);
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

        public List<HorarioDTO> ObterFormatado(HorarioDTO dto)
        {

            List<HorarioDTO> lista = new List<HorarioDTO>();
            try
            {


                BaseDados.ComandText = "stp_ACA_HORARIO_IMPRESSAO";


                
                BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo);
                BaseDados.AddParameter("@CURSO", dto.HorTurma.Curso);
                BaseDados.AddParameter("@PERIODO", dto.HorPeriodo);
                BaseDados.AddParameter("@ANO", dto.HorAnoLectivo.AnoCodigo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new HorarioDTO();

                    dto.HorInicio = int.Parse(dr["HOR_INICIO"].ToString());
                    dto.HorTermino = int.Parse(dr["HOR_TERMINO"].ToString());

                    TurmaDTO dtoTurma = new TurmaDTO();
                    dtoTurma.Codigo = int.Parse(dr["HOR_CODIGO_TURMA"].ToString());
                    dtoTurma.Sigla = dr["TUR_ABREVIATURA"].ToString() + " " + dr["PLAN_DESCRICAO"].ToString();
                    if (dr["TUR_TURNO"].ToString().Equals("M"))
                        dtoTurma.Turno = "MANHÃ";
                    else if (dr["TUR_TURNO"].ToString().Equals("T"))
                        dtoTurma.Turno = "TARDE";
                    else
                        dtoTurma.Turno = "NOITE";

                    dto.HorTurma = dtoTurma;
                    dto.SegundaFeira = dr["SEGUNDA"].ToString();
                    dto.TercaFeira = dr["TERCA"].ToString();
                    dto.QuartaFeira = dr["QUARTA"].ToString();
                    dto.QuintaFeira = dr["QUINTA"].ToString();
                    dto.SextaFeira = dr["SEXTA"].ToString();
                    dto.Sabado = dr["SABADO"].ToString();

                    dto.Curso = dr["CUR_NOME"].ToString();
                    dto.Departamento = dr["COOR_DESCRICAO"].ToString();

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
        public HorarioDTO Copiar(HorarioDTO dto)
        {

            try{
                BaseDados.ComandText = "stp_ACA_HORARIO_COPIAR";


                BaseDados.AddParameter("@ORIGEM", dto.HorCodigo);
                BaseDados.AddParameter("@DESTINO", dto.HorTurma.Codigo);

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



        public List<HorarioDTO> ObterPorDia(HorarioDTO dto)
        {
            List<HorarioDTO> lista = new List<HorarioDTO>();
            try{
             BaseDados.ComandText = "stp_ACA_HORARIO_DOCENTE_OBTERPORFILTRO";

                BaseDados.AddParameter("@DOCENTE", -1);
                BaseDados.AddParameter("@DIA", dto.HorDiaSemana);
                BaseDados.AddParameter("@DISCIPLINA", -1);
                BaseDados.AddParameter("@HORA", dto.HorInicio);
                BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo);
                BaseDados.AddParameter("@ANO", dto.HorAnoLectivo.AnoCodigo);
                BaseDados.AddParameter("@CURSO", dto.HorTurma.Curso); 

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new HorarioDTO();

                    dto.HorDisiciplina = new UnidadeCurricularDTO(int.Parse(dr["HOR_DISCIPLINA"].ToString()));                     
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

        public void ExcluirPorPK(HorarioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_HORARIO_EXCLUIR_SEMANA";

                BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo);
                BaseDados.AddParameter("@HORA", dto.HorInicio);
                BaseDados.AddParameter("@PERIODO", dto.HorPeriodo);
                BaseDados.AddParameter("@ANO", dto.HorAnoLectivo.AnoCodigo);

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

        public List<HorarioDTO> ObterPorDocente(HorarioDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_HORARIO_DOCENTE_OBTERPORFILTRO";


            BaseDados.AddParameter("@DOCENTE", dto.HorDocente.Codigo);
            BaseDados.AddParameter("@DIA", dto.HorDiaSemana);
            BaseDados.AddParameter("@DISCIPLINA", dto.HorDisiciplina.Codigo);
            BaseDados.AddParameter("@HORA", dto.HorInicio);
            BaseDados.AddParameter("@TURMA", dto.HorTurma.Codigo);
            BaseDados.AddParameter("@ANO", dto.HorAnoLectivo.AnoCodigo);
            BaseDados.AddParameter("@CURSO", dto.HorDisiciplina.PlanoCurricular.Ramo.RamCodigo);
            BaseDados.AddParameter("PERIODO", dto.HorPeriodo);

            List<HorarioDTO> lista = new List<HorarioDTO>();
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new HorarioDTO();

                    dto.HorCodigo = Int32.Parse(dr["HOR_CODIGO"].ToString());
                    dto.HorDocente = new DocenteDTO(dr["HOR_CODIGO_DOCENTE"].ToString(), dr["NOME_DOCENTE"].ToString());
                    dto.HorDiaSemana = Int32.Parse(dr["HOR_DIA_SEMANA"].ToString());
                    dto.HorInicio = int.Parse(dr["HOR_INICIO"].ToString());
                    //dto.HorRegime = dr["HOR_REGIME"].ToString();
                    dto.HorTermino = int.Parse(dr["HOR_TERMINO"].ToString());

                    TurmaDTO dtoTurma = new TurmaDTO();
                    dtoTurma.Codigo = int.Parse(dr["HOR_TURMA"].ToString());
                    dtoTurma.Sigla = dr["TURMA"].ToString();
                    dtoTurma.Turno = dr["TUR_TURNO"].ToString();
                    dto.HorTurma = dtoTurma;//daoTurma.ObterPorPK(dtoTurma);
                    UnidadeCurricularDTO dtoDisciplina = new UnidadeCurricularDTO();
                    dtoDisciplina.NomeDisciplina = dr["DISCIPLINA"].ToString();
                    dtoDisciplina.Tipo = dr["DIS_SIGLA"].ToString();
                    dtoDisciplina.Conteudo = dr["COOR_DESCRICAO"].ToString();
                    dtoDisciplina.Codigo = int.Parse(dr["HOR_DISCIPLINA"].ToString());
                    dtoDisciplina.PlanoCurricular = new AnoCurricularDTO(int.Parse(dr["PLAN_CODIGO"].ToString()), new RamoDTO(int.Parse(dr["CUR_CODIGO"].ToString()), dr["CUR_ESPECIFICACAO"].ToString(), null, "", 0, 0, 1, ""),
                    -1, dr["ANO_CURRICULAR"].ToString(), dr["CURSO"].ToString());
                    dto.HorDisiciplina = dtoDisciplina;//new DisciplinaPlanoDAO().ObterPorPK(new DisciplinaPlanoDTO(int.Parse(dr["HOR_DISCIPLINA"].ToString())));
                    dto.HorAnoLectivo = new AnoLectivoDTO(dr["ANO_ANO_LECTIVO"].ToString());
                    if(!lista.Exists(t=>t.HorDiaSemana == dto.HorDiaSemana && t.HorDocente.Codigo== dto.HorDocente.Codigo && t.HorInicio== dto.HorInicio))
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

        public String HorarioAula(int inicio, int termino)
        {
            String hora = "";
            string horaInicio = inicio.ToString().Substring(0, 2);
            string horaTermino = termino.ToString().Substring(0, 2);
            string minutosInicio = "";
            string minutosTermino = "";

            if (inicio.ToString().Length == 3)
            {
                minutosInicio = "0" + inicio.ToString().Substring(0, 1) + ":" + inicio.ToString().Substring(1, 2);
                horaInicio = "";
            }
            else
            {
                minutosInicio += ":" + inicio.ToString().Substring(2, 2);
            }


            if (termino.ToString().Length == 3)
            {
                minutosTermino += "0" + termino.ToString().Substring(0, 1) + ":" + termino.ToString().Substring(1, 2);
                horaTermino = "";
            }
            else
            {
                minutosTermino += ":" + termino.ToString().Substring(2, 2);
            }
            horaInicio += minutosInicio;
            horaTermino += minutosTermino;
            hora = horaInicio + " - " + horaTermino;
            return hora;
        }

        public HorarioDTO ConfirmarDisponibilidadeHorario(HorarioDTO dto, int pPeriodoID)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_HORARIO_VALIDACAO";

                BaseDados.AddParameter("@DIA", dto.HorDiaSemana);
                BaseDados.AddParameter("@INICIO", dto.HorInicio);
                BaseDados.AddParameter("@TERMINO", dto.HorTermino);
                BaseDados.AddParameter("@DOCENTE_ID", dto.HorDocente.Codigo <=0 ? (object)DBNull.Value : dto.HorDocente.Codigo);
                BaseDados.AddParameter("@TURMA_ID", dto.HorTurma.Codigo);
                BaseDados.AddParameter("@DISCIPLINA_ID", dto.HorDisiciplina.Codigo); 
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@PERIODO_ID", pPeriodoID);

                var sucesso = BaseDados.ExecuteInsert();
                dto.Sucesso = sucesso == 1 ? true : false;
                dto.MensagemErro = sucesso == -1 ? "O Professor, "+dto.HorDocente.Nome+" já tem aula agendada para no Horário de "+dto.Horario : "";
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }finally
            {
                BaseDados.FecharConexao();

            }

            return dto;
        }
    }
}
