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
    public class MatriculaDAO
    {
        readonly ConexaoDB BaseDados;

        public MatriculaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public MatriculaDTO Adicionar(MatriculaDTO dto) 
        {
                
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_ADICIONAR";

                BaseDados.AddParameter("@DT_OPERACAO", dto.Data);
                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);
                BaseDados.AddParameter("@ESTADO", dto.Estado);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@ESTADO_INI", dto.Situacao);
                BaseDados.AddParameter("@ESTADO_TERM", dto.SituacaoFinal);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
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

        public MatriculaDTO Alterar(MatriculaDTO dto)
        {
            
            try
            {

                BaseDados.ComandText = "stp_ACA_MATRICULA_ALTERAR";

                BaseDados.AddParameter("@DT_OPERACAO", dto.Data);
                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);
                BaseDados.AddParameter("@ESTADO", dto.Estado);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@ESTADO_INI", dto.Situacao);
                BaseDados.AddParameter("@ESTADO_TERM", dto.SituacaoFinal);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public MatriculaDTO AlterarStatusFinal(MatriculaDTO dto)
        {
            
            try
            {

                BaseDados.ComandText = "stp_ACA_MATRICULA_ALTERAR_SITUACAO_FINAL";

               
                BaseDados.AddParameter("@ESTADO_TERM", dto.SituacaoFinal); 
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public MatriculaDTO Excluir(MatriculaDTO dto)
        {
            
            try
            {

                BaseDados.ComandText = "stp_ACA_MATRICULA_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public MatriculaDTO ObterPorPK(MatriculaDTO dto)
        {
            
            try
            {
               

                BaseDados.ComandText = "stp_ACA_MATRICULA_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
            
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MatriculaDTO();
               while (dr.Read()) 
               {
                   dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                   dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                   dto.Classe = int.Parse(dr["MAT_CODIGO_PLANO_CURRICULAR"].ToString());
                   dto.Data = dr["MAT_DATA"].ToString() != string.Empty ? Convert.ToDateTime(dr["MAT_DATA"].ToString()) : DateTime.MinValue;
                   dto.Estado = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                   dto.Situacao = dr["SITUACAO_INICIAL"].ToString(); 
                   if (dr["TUR_TIPO"].ToString() == null || dr["TUR_TIPO"].ToString()=="")
                   {
                       if (dr["MAT_CODIGO_TURMA"].ToString() !=null)
                       {
                           dto.Turma = int.Parse(dr["MAT_CODIGO_TURMA"].ToString());
                       }
                   }
                   else 
                   {
                       dto.Turma = -1;
                   }
                   dto.SituacaoFinal = dr["SITUACAO_FINAL"].ToString();
                   dto.NomeAluno = dr["ENT_NOME_COMPLETO"].ToString();
                   dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                   dto.Curso = int.Parse(dr["PLAN_CODIGO_RAMO"].ToString());
                   dto.Movimento = int.Parse(dr["EST_CODIGO_MOVIMENTO"].ToString());
                   dto.Turno = dr["TUR_TURNO"].ToString();
                   dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    dto.Aluno = new AlunoDTO(int.Parse(dr["MAT_CODIGO_ALUNO"].ToString()))
                    {
                        PathFoto = dr["PES_FOTO_PATH"].ToString()
                    };
                    dto.NomeDocumento = dr["DOC_DESCRICAO"].ToString();
                   dto.DescricaoEstado = dr["EST_DESCRICAO"].ToString();
                   dto.DescricaoAno = dr["ANO_ANO_LECTIVO"].ToString();
                   dto.Plano = new AnoCurricularDTO(dto.Classe, dr["PLAN_DESCRICAO"].ToString());
                   dto.NomeTurma = dr["TUR_ABREVIATURA"].ToString();
                   dto.Departamento = dr["CUR_ESPECIFICACAO"].ToString();
                   if (dr["MAT_CODIGO_FILIAL"].ToString() != null && dr["MAT_CODIGO_FILIAL"].ToString() != "")
                      dto.Filial = dr["MAT_CODIGO_FILIAL"].ToString();
                   dto.TerminoMatricula = Convert.ToDateTime(dr["ANO_TERMINO_MATRICULA"].ToString());
                   if (dr["MAT_DATA_PAGAMENTO"].ToString() !=null)
                   {
                       dto.DataPagto = DateTime.Parse(dr["MAT_DATA_PAGAMENTO"].ToString());
                   }
                   else
                   {
                       dto.DataPagto = DateTime.MinValue;
                   }
                   
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

        public MatriculaDTO ObterPorMovimento(MatriculaDTO dto)
        {

            

            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_OBTEREXISTENTE";


                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MatriculaDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                    dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                    dto.Classe = int.Parse(dr["MAT_CODIGO_PLANO_CURRICULAR"].ToString());
                    dto.Data = dr["MAT_DATA"].ToString() != string.Empty ? Convert.ToDateTime(dr["MAT_DATA"].ToString()) : DateTime.MinValue;
                    dto.Estado = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                    dto.Situacao = dr["SITUACAO_INICIAL"].ToString();

                    if (dr["TUR_TIPO"].ToString() == null || dr["TUR_TIPO"].ToString()=="")
                    {
                        if (dr["MAT_CODIGO_TURMA"].ToString() !=null)
                        {
                            dto.Turma = int.Parse(dr["MAT_CODIGO_TURMA"].ToString());
                        }
                    }
                    else
                    {
                        dto.Turma = -1;
                    }
                    dto.SituacaoFinal = dr["SITUACAO_FINAL"].ToString();
                    dto.NomeAluno = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.Curso = int.Parse(dr["PLAN_CODIGO_RAMO"].ToString());
                    dto.Movimento = int.Parse(dr["EST_CODIGO_MOVIMENTO"].ToString());
                    dto.Turno = dr["TUR_TURNO"].ToString();
                    dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    dto.Aluno = new AlunoDTO(int.Parse(dr["MAT_CODIGO_ALUNO"].ToString()))
                    {
                        PathFoto = dr["PES_FOTO_PATH"].ToString()
                    };
                    dto.NomeDocumento = dr["DOC_DESCRICAO"].ToString();
                    dto.DescricaoEstado = dr["EST_DESCRICAO"].ToString();
                    dto.DescricaoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.TerminoMatricula = Convert.ToDateTime(dr["ANO_TERMINO_MATRICULA"].ToString());
                    dto.Plano = new AnoCurricularDTO(dto.Classe, dr["PLAN_DESCRICAO"].ToString());
                    dto.NomeTurma = dr["TUR_ABREVIATURA"].ToString();
                    if (dr["MAT_CODIGO_FILIAL"].ToString() != null && dr["MAT_CODIGO_FILIAL"].ToString() != "")
                        dto.Filial = dr["MAT_CODIGO_FILIAL"].ToString();
                    if (dr["MAT_DATA_PAGAMENTO"].ToString() !=null)
                    {
                        dto.DataPagto = DateTime.Parse(dr["MAT_DATA_PAGAMENTO"].ToString());
                    }
                    else
                    {
                        dto.DataPagto = DateTime.MinValue;
                    }
                    dto.Departamento = dr["CUR_ESPECIFICACAO"].ToString();
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

        public List<MatriculaDTO> ObterPorFiltro(MatriculaDTO dto)
        {
            List<MatriculaDTO> lista;
            
            
            try
            {
                BaseDados.ComandText = "";
                if (dto.Origem != "")
                {
                    _commandText = "stp_ACA_MATRICULA_SERVICO_ACADEMICOS";
                }
                else
                {
                    _commandText = "stp_ACA_MATRICULA_OBTERPORFILTRO";
                }


                if (dto.DataIni != null && !dto.DataIni.Equals("") && !dto.DataIni.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("@DATA_INI", DateTime.Parse(dto.DataIni));
                }
                else
                {
                    BaseDados.AddParameter("@DATA_INI", DBNull.Value);
                }

                if (dto.DataTerm != null && !dto.DataTerm.Equals(""))
                {
                    BaseDados.AddParameter("@DATA_TERM", DateTime.Parse(dto.DataTerm));
                }
                else
                {
                    BaseDados.AddParameter("@DATA_TERM", DBNull.Value);
                }

                BaseDados.AddParameter("@MOVIMENTO", dto.Movimento);
                BaseDados.AddParameter("@SITUACAO", dto.Estado);
                BaseDados.AddParameter("@MATRICULA", !string.IsNullOrEmpty(dto.Inscricao) ? int.Parse(dto.Inscricao) : -1);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@NOME", dto.NomeAluno);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@TURNO", dto.Turno);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<MatriculaDTO>();
                
                while (dr.Read())
                {
                    dto = new MatriculaDTO();

                    dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                    dto.Data = Convert.ToDateTime(dr["MAT_DATA"].ToString());
                    dto.NomeAluno = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.Estado = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                    dto.DescricaoEstado = dr["EST_DESCRICAO"].ToString();
                    dto.Situacao = dr["MAT_SITUACAO_INICIAL"].ToString();
                    dto.Turma = int.Parse(dr["MAT_CODIGO_TURMA"].ToString());
                    dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                    dto.Classe = int.Parse(dr["MAT_CODIGO_PLANO_CURRICULAR"].ToString());
                    dto.SituacaoFinal = dr["MAT_SITUACAO_FINAL"].ToString();
                    dto.NomeTurma = dr["CUR_NOME"].ToString() + " - "+ dr["PLAN_DESCRICAO"].ToString();
                    var aluno = new AlunoDTO(int.Parse(dr["MAT_CODIGO_ALUNO"].ToString()))
                    {
                        PathFoto = dr["PES_FOTO_PATH"].ToString(),
                        DataInscricao = Convert.ToDateTime(dr["ALU_DATA_INSCRICAO"].ToString())
                    }; 
                    dto.Aluno = aluno;
                    dto.Departamento = dr["CUR_ESPECIFICACAO"].ToString();
                    dto.DataTerm = dr["ENT_DATA_NASCIMENTO"].ToString();
                    dto.Turno = dr["TUR_TURNO"].ToString();

                    if (dto.Turno == "N")
                    {
                        dto.Turno = "NOITE";
                    }
                    else if (dto.Turno == "T")
                    {
                        dto.Turno = "TARDE";
                    }
                    else if (dto.Turno == "M")
                    {
                        dto.Turno = "MANHÃ";
                    }

                    dto.NomeTurma += " " + dto.Turno;
                    dto.AnoCurricular = dr["PLAN_DESCRICAO"].ToString(); 
                    
                    dto.AnoCivil = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dto.DataPagto = dr["MAT_DATA_PAGAMENTO"].ToString() != null ? DateTime.Parse(dr["MAT_DATA_PAGAMENTO"].ToString()) : DateTime.MinValue;
                    dto.FaturaMatricula = dr["MAT_CODIGO_FATURA"].ToString() != null ? int.Parse(dr["MAT_CODIGO_FATURA"].ToString()) : 0;
                    dto.TemMatricula = dto.DataPagto > DateTime.MinValue && dto.FaturaMatricula > 0 ? true : false;
                    dto.TemIsencaoMatricula = dr["MAT_ISENCAO_MATRICULA"].ToString() == null || dr["MAT_ISENCAO_MATRICULA"].ToString() != "1" ? false : true;
                    dto.TemIsencaoPropina = dr["MAT_BOLSA_MENSALIDADE"].ToString() == null || dr["MAT_BOLSA_MENSALIDADE"].ToString() != "1" ? false : true;
                    dto.TurmaTemporaria = dr["TUR_TIPO"].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<MatriculaDTO>();
                dto = new MatriculaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.NomeAluno = ex.Message.Replace("'", "");
                lista.Add(dto);
                
            }
            finally
            {
               BaseDados.FecharConexao();
            }
            return lista;
        }

        public List<MatriculaDTO> ObterNaoMatriculados(AnoLectivoDTO ano)
        {

            
            List<MatriculaDTO> lista;
            MatriculaDTO dto;
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_NAOMATRICULADO";

                BaseDados.AddParameter("@ANO", ano.AnoCodigo);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<MatriculaDTO>();
                while (dr.Read())
                {
                   dto = new MatriculaDTO();

                    dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                    dto.Data = Convert.ToDateTime(dr["MAT_DATA"].ToString());
                    dto.Aluno = new AlunoDTO(int.Parse(dr["MAT_CODIGO_ALUNO"].ToString()));
                    dto.NomeAluno = dr["NOME"].ToString();
                    dto.Inscricao = dr["INSCRICAO"].ToString();
                    dto.Estado = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                    dto.Situacao = dr["MAT_SITUACAO_INICIAL"].ToString();
                    dto.Turma = int.Parse(dr["MAT_CODIGO_TURMA"].ToString());
                    dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                    dto.Classe = int.Parse(dr["CODIGO_PLANO_CURRICULAR"].ToString());
                    dto.SituacaoFinal = dr["MAT_SITUACAO_FINAL"].ToString();

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<MatriculaDTO>();
                dto = new MatriculaDTO();
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

        public Boolean ObterSituacao(MatriculaDTO dto)
        {
             
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_OBTERPORSITUACAO";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MatriculaDTO();
                
                foreach (var dr in reader)
                {
                    dto.Sucesso = true;
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

            return dto.Sucesso;
            
        }

        public MatriculaDTO ObterSituacaoAcademica(MatriculaDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_OBTER_SITUACAO";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MatriculaDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                    dto.Situacao = dr["ESTADO"].ToString();
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

        public MatriculaDTO ObterPorInscricao(MatriculaDTO dto)
        {
            

            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_OBTERPORALUNO";
            
            
                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MatriculaDTO();
                while (dr.Read())
                {


                    dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                    dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                    dto.Classe = int.Parse(dr["MAT_CODIGO_PLANO_CURRICULAR"].ToString());
                    dto.Data = dr["MAT_DATA"].ToString() !=string.Empty ? Convert.ToDateTime(dr["MAT_DATA"].ToString()) : DateTime.MinValue;
                    dto.Estado = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                    dto.Situacao = dr["SITUACAO_INICIAL"].ToString();

                    if (dr["TUR_TIPO"].ToString() == null || dr["TUR_TIPO"].ToString()=="")
                    {
                        if (dr["MAT_CODIGO_TURMA"].ToString() !=null)
                        {
                            dto.Turma = int.Parse(dr["MAT_CODIGO_TURMA"].ToString());
                        }
                    }
                    else
                    {
                        dto.Turma = -1;
                    }
                    dto.SituacaoFinal = dr["SITUACAO_FINAL"].ToString();
                    dto.NomeAluno = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.Curso = int.Parse(dr["PLAN_CODIGO_RAMO"].ToString());
                    dto.Movimento = int.Parse(dr["EST_CODIGO_MOVIMENTO"].ToString());
                    dto.Turno = dr["TUR_TURNO"].ToString();
                    dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    var aluno = new AlunoDTO(int.Parse(dr["MAT_CODIGO_ALUNO"].ToString()));
                    aluno.PathFoto = dr["PES_FOTO_PATH"].ToString();
                    dto.Aluno = aluno;
                    dto.NomeDocumento = dr["DOC_DESCRICAO"].ToString();
                    dto.DescricaoEstado = dr["EST_DESCRICAO"].ToString();
                    dto.DescricaoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.TerminoMatricula = Convert.ToDateTime(dr["ANO_TERMINO_MATRICULA"].ToString());
                    dto.Plano = new AnoCurricularDTO(dto.Classe, dr["PLAN_DESCRICAO"].ToString());
                    dto.NomeTurma = dr["TUR_ABREVIATURA"].ToString();
                    dto.Departamento = dr["CUR_ESPECIFICACAO"].ToString();
                    if (dr["MAT_CODIGO_FILIAL"].ToString() != null && dr["MAT_CODIGO_FILIAL"].ToString() != "")
                        dto.Filial = dr["MAT_CODIGO_FILIAL"].ToString();

                    if (dr["MAT_DATA_PAGAMENTO"].ToString() !=null)
                    {
                        dto.DataPagto = DateTime.Parse(dr["MAT_DATA_PAGAMENTO"].ToString());
                    }
                    else
                    {
                        dto.DataPagto = DateTime.MinValue;
                    }

                    
                   
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

        public List<MatriculaDTO> ObterHistorico(MatriculaDTO dto)
        {

            
            List<MatriculaDTO> lista;
             
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_HISTORICO";

                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<MatriculaDTO>();
                while (dr.Read())
                {
                    dto = new MatriculaDTO();
                    dto.Codigo = int.Parse(dr["MAT_CODIGO"].ToString());
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<MatriculaDTO>();
                dto = new MatriculaDTO();
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

        public RelatorioAlunoDTO ComprovanteMatricula(MatriculaDTO dto) 
        {
             
            RelatorioAlunoDTO item = new RelatorioAlunoDTO();


            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_CONFIRMACAO";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read()) 
                {
                    
                    item.Codigo = int.Parse(dr[0]);
                    item.Inscricao = dr[1];
                    item.Nome = dr[2];
                    item.Ramo = dr[3]; // Encarregado
                    item.Curso = dr[4];
                    item.Turma = dr[5];
                    item.CodigoTurma = int.Parse(dr[6]); // TurmaID
                    item.Periodo = dr[7]; // Turno
                    item.Sala = dr[8];
                    item.AnoCivil = int.Parse(dr[9]);
                    item.ItemID = int.Parse(dr[10]); // Curso;
                    item.Classe = dr[11];
                    break;
                }
            }
            catch (Exception ex)
            {
                item = new RelatorioAlunoDTO();
                item.MensagemErro = ex.Message;
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return item;
        }

        public List<RelatorioAlunoDTO> PlanoPagamento(MatriculaDTO dto)
        {
            List<RelatorioAlunoDTO> lista = new List<RelatorioAlunoDTO>();
            RelatorioAlunoDTO item;
            
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_PLANO_PAGAMENTO";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    item = new RelatorioAlunoDTO();

                    item.Nome = dr[0];
                    item.ValorGlobal = decimal.Parse(dr[1]);
                    item.Mensalidade = Convert.ToDecimal(dr[2]);
                    item.Periodo = dr[3];

                    lista.Add(item);

                }
            }
            catch (Exception ex)
            {
                item = new RelatorioAlunoDTO();
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return lista;
        }


        public List<PautaDTO> BoletimNotas(MatriculaDTO dto)
        {
            List<PautaDTO> lista = new List<PautaDTO>();
            

            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERBOLETIM";
                BaseDados.AddParameter("@MATRICULA", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                while (dr.Read())
                {
                    PautaDTO nota = new PautaDTO();
                    lista.Add(nota);
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public void AtualizarSituacaoEscolar(MatriculaDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_MATRICULA_ACTUALIZA_SITUACAO_ESCOLAR";

               
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SITUACAO", dto.Estado);
                BaseDados.AddParameter("@STATUS_INICIAL", dto.Situacao);
                BaseDados.AddParameter("@STATUS_FINAL", dto.SituacaoFinal);

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                BaseDados.FecharConexao();
            }
        }

        public void Activar(MatriculaDTO dto)
        {
            
            try
            {

                BaseDados.ComandText = "stp_ACA_MATRICULA_ACTIVAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
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

        public AnoLectivoDTO GetNextAcademicYearToPay(MatriculaDTO pMatricula)
        {

            var dto = new AnoLectivoDTO();
            
            try
            {
                BaseDados.ComandText = "stp_FIN_ITEM_COBRANCA_FATURA_DETALHES_ULTIMATAXAPAGA";

                BaseDados.AddParameter("@ALUNO", pMatricula.Aluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                    dto.AnoCodigo = Int32.Parse(dr["ANO_CODIGO"].ToString());
                    dto.AnoDescricao = dr["ANO_DESCRICAO"].ToString();
                    dto.AnoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.AnoInicio = DateTime.Parse(dr["ANO_INICIO"].ToString());
                    dto.AnoTermino = DateTime.Parse(dr["ANO_TERMINO"].ToString());
                    if (dr["ANO_INICIO_MATRICULA"].ToString() != null && dr["ANO_INICIO_MATRICULA"].ToString() !="")
                    {
                        dto.AnoInicioMatricula = DateTime.Parse(dr["ANO_INICIO_MATRICULA"].ToString());
                    }

                    if (dr["ANO_TERMINO_MATRICULA"].ToString() != null && dr["ANO_TERMINO_MATRICULA"].ToString() !="")
                    {
                        dto.AnoTerminoMatricula = DateTime.Parse(dr["ANO_TERMINO_MATRICULA"].ToString());
                    }
                    dto.AnoStatus = dr["ANO_STATUS"].ToString();
                    dto.NivelEnsino = dr["ANO_NIVEL"].ToString();
                    dto.TaxaInscricao = dr["ANO_TAXA_INSCRICAO"].ToString();
                    dto.MultaMatricula = dr["ANO_MULTA_MATRICULA"].ToString();
                    dto.Filial = dr["ANO_CODIGO_FILIAL"].ToString();
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
