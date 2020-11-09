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
    public class PautaDAO
    {
        readonly ConexaoDB BaseDados;

        public PautaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PautaDTO AdicionarNaPauta(PautaDTO dto) 
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_PAUTA_ADICIONAR";

                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@ALUNO", dto.Aluno);
                BaseDados.AddParameter("@NOTA", dto.PrimeiraNota);
                BaseDados.AddParameter("@INSCRICAO", dto.InscricaoAluno);
                BaseDados.AddParameter("@NOME", dto.NomeAluno);
                BaseDados.AddParameter("@PROVA", dto.PauDsAvaliacao);
                BaseDados.AddParameter("@AVALIACAO", dto.Prova);

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

        public PautaDTO LancarNota(PautaDTO dto) 
        {
           
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_ALTERAR";

                BaseDados.AddParameter("@NOTA", dto.PrimeiraNota);
                BaseDados.AddParameter("@LANCAMNETO", dto.DataProva);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@PROVA", dto.PauDsAvaliacao);
                BaseDados.AddParameter("@AVALIACAO", dto.Prova);
                BaseDados.AddParameter("@INSCRICAO", dto.InscricaoAluno);
                BaseDados.AddParameter("@NOME", dto.NomeAluno);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@ALUNO", dto.Aluno);
                BaseDados.AddParameter("@NIVEL", dto.Nivel);
                if (dto.isValidada)
                {
                    BaseDados.AddParameter("@VALIDADA", "S");
                }
                else
                {
                    BaseDados.AddParameter("@VALIDADA", "N");
                }

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

        public List<PautaDTO> ObterPautas(PautaDTO dto) 
        {
            BaseDados.ComandText = "stp_ACA_PAUTA_OBTERPORFILTRO";

           BaseDados.AddParameter("@TURMA", dto.Turma);
           BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
           BaseDados.AddParameter("@ANO", dto.AnoLectivo);
           BaseDados.AddParameter("@ALUNO", dto.Aluno);
           BaseDados.AddParameter("@NOME", dto.NomeAluno);
           BaseDados.AddParameter("@PROVA", dto.Prova);
            List<PautaDTO> pauta = new List<PautaDTO>();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                int codigo = 0;
                while (dr.Read())
                {
                    dto = new PautaDTO();
                    codigo++;
                    dto.Codigo = codigo;//Int32.Parse(dr[0]);
                    dto.DsDisciplina = dr[1];
                    dto.PauCurso = dr[2];
                    dto.PauClasse = dr[3];
                    dto.DsTurma = dr[4];
                    dto.PauPeriodo = dr[5];
                    dto.InscricaoAluno = dr[6];
                    dto.NomeAluno = dr[7];
                    dto.PrimeiraNota = Decimal.Parse(dr[8]);
                    dto.SegundaNota = Decimal.Parse(dr[9]);
                    dto.TerceiraNota = Decimal.Parse(dr[10]);
                    dto.QuartaNota = Decimal.Parse(dr[11]);
                    dto.Frequencia = Decimal.Parse(dr[12]);
                    if (dr[14]!=null)
                    {
                        dto.PauMedia = Decimal.Parse(dr[13]);
                    }
                    else 
                    {
                        dto.PauMedia = -1;
                    }
                    dto.PauExame = Decimal.Parse(dr[14]);
                    dto.PauExameOral = Decimal.Parse(dr[15]);
                    dto.PauRecurso = Decimal.Parse(dr[16]);
                    dto.PauRecursoOral = Decimal.Parse(dr[17]);
                    dto.PauExaEspecial = Decimal.Parse(dr[18]);
                    dto.PauExaEspecialOral = Decimal.Parse(dr[19]);
                    if (dr[21] != null)
                    {
                        dto.PauNotaFinal = Decimal.Parse(dr[20]);
                    }
                    else
                    {
                        dto.PauNotaFinal = -1;
                    }
                    
                    dto.PauNotaExtenso = dr[21];
                    dto.PauSituacao = dr[22];
                    dto.DsAnoLectivo = dr[23] ;
                    dto.Docente = dr[24];
                    if (dr[25] != null && dr[25].Equals("1"))
                    {
                        dto.isValidada = true;
                    }
                    else
                    {
                        dto.isValidada = false;
                    }
                     
                    pauta.Add(dto);

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

            return pauta;
        }

        public List<PautaDTO> ObterBoletim(PautaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_ALUNO_OBTERBOLETIM";

             
           BaseDados.AddParameter("@ALUNO", dto.Aluno);
           BaseDados.AddParameter("@CLASSE", dto.PauClasse);

            List<PautaDTO> pauta = new List<PautaDTO>();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PautaDTO();
                    dto.DsDisciplina = dr[1];
                    dto.PauCurso = dr[2];
                    dto.PauClasse = dr[3];
                    dto.DsTurma = dr[4];
                    dto.PauPeriodo = dr[5];
                    dto.InscricaoAluno = dr[6];
                    dto.NomeAluno = dr[7];
                    dto.PrimeiraNota = Decimal.Parse(dr[8]);
                    dto.SegundaNota = Decimal.Parse(dr[9]);
                    dto.TerceiraNota = Decimal.Parse(dr[10]);
                    dto.QuartaNota = Decimal.Parse(dr[11]);
                    if (dr[12]!=null)
                    {
                        dto.PauMedia = Decimal.Parse(dr[12]);
                    }
                    else
                    {
                        dto.PauMedia = -1;
                    }
                    dto.PauExame = Decimal.Parse(dr[13]);
                    dto.PauExameOral = Decimal.Parse(dr[14]);
                    dto.PauRecurso = Decimal.Parse(dr[15]);
                    dto.PauExaEspecial = Decimal.Parse(dr[16]);
                    dto.PauExaEspecialOral = Decimal.Parse(dr[17]);
                    if (dr[18]!=null)
                    {
                        dto.PauNotaFinal = Decimal.Parse(dr[18]);
                    }
                    else
                    {
                        dto.PauNotaFinal = -1;
                    }

                    dto.PauNotaExtenso = dr[19];
                    dto.PauSituacao = dr[20] == null ? "" : dr[20];

                    if (dr[23].Equals("VALIDADA"))
                    {
                        dto.isValidada = true;
                    }
                    dto.Disciplina = int.Parse(dr[24]);
                    dto.Ano = int.Parse(dr[25]);
                    pauta.Add(dto);

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

            return pauta;
        }

        public Int32 NegativasDoAluno(PautaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_PAUTA_NEGATIVAS";
            int negativas=0;
            
            
            

           BaseDados.AddParameter("@ALUNO", dto.Aluno);
           BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
           BaseDados.AddParameter("@TURMA", dto.Turma);
            List<PautaDTO> pauta = new List<PautaDTO>();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    negativas++;
                }
                return negativas;

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                return -1;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }
        
        private decimal CalculaMAC(PautaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_NOTA_OBTERAVALIACAO_CONTINUA";

            
            
            
            decimal mediaAC = -1, total=0, nota;
            try 
            {
               BaseDados.AddParameter("@TURMA", dto.Turma);
               BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
               BaseDados.AddParameter("@ALUNO", dto.Aluno);
               BaseDados.AddParameter("@PERIODO", dto.Periodo);
                 

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

                while (dr.Read()) 
                {
                    dto = new PautaDTO();
                    total = int.Parse(dr[0]);
                    if (total > 0) 
                    {
                        nota = decimal.Parse(dr[1]);
                        mediaAC = nota / total;
                    }
                }

                 

            }catch(Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
               
            }finally
            {
                BaseDados.FecharConexao();
            }

            return mediaAC;
        }

        public List<PautaDTO> ObterNotasMiniPauta(PautaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_PAUTA_MINIPAUTA"; 

           BaseDados.AddParameter("@TURMA", dto.Turma);
           BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
            if (dto.PauSituacao != null && dto.PauSituacao.Equals("MP"))
            {
               BaseDados.AddParameter("@PERIODO", -1);
            }
            else
            {
               BaseDados.AddParameter("@PERIODO", dto.Periodo);
            }
           BaseDados.AddParameter("@ALUNO", dto.Aluno);
           
            PautaDTO dtoPauta = dto;
            List<PautaDTO> lista = null;
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                lista = new List<PautaDTO>();
                while (dr.Read())
                {
                    dto = new PautaDTO();
                    dto.Aluno = Int32.Parse(dr[0]);
                    dto.InscricaoAluno = dr[1];
                    dto.NomeAluno = dr[2];
                    dto.PrimeiraNota = Decimal.Parse(dr[3]);
                    dto.SegundaNota = Decimal.Parse(dr[4]);
                    dto.TerceiraNota = Decimal.Parse(dr[5]);
                    dto.QuartaNota = Decimal.Parse(dr[6]);
                    dto.PauExame = Decimal.Parse(dr[7]);
                    dto.Frequencia = Decimal.Parse(dr[8]);

                    dtoPauta.Periodo = int.Parse(dr[17]);
                    dto.PauMelhoria = Decimal.Parse(dr[16]);
                    
                    if (dr[9]!=null)
                    {
                        dto.PauNotaFinal = Decimal.Parse(dr[9]);
                    }
                    else
                    {
                        dto.PauNotaFinal = -1;
                    }

                    dto.DsDisciplina = dr[10];
                    dto.PauCurso = dr[11];// +" " + dr[12];
                    //dto.PauDsRamo = dr[12];
                    dto.PauClasse = dr[12];
                    dto.DsTurma = dr[13];
                    dto.DsAnoLectivo = dr[14];
                    dto.DsPeriodo = dr[15];

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


        public PautaDTO Cabecalho(PautaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_PAUTA_CABECALHO";
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) 
                {
                    dto.PauCurso = dr[0];
                    dto.PauClasse = dr[1];
                    dto.DsDisciplina = dr[2];
                    dto.DsTurma = dr[3];
                    dto.DsAnoLectivo = dr[4];
                    dto.PauDsRamo = dr[5];
                    dto.DsPeriodo = dr[6]; // TIPO DE CURSO: PRIMÁRIO, SECUNDÁRIO, TÉCNICO, FORMAÇÃO DE PROFESSORES OU DE SAÚDE
                    dto.Docente = dr[7];
                    dto.Nivel = dr[8];
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally 
            {
                BaseDados.FecharConexao();
            }
            return dto;
        }

        public void RegistarNotas(PautaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_PAUTA_SALVAR";

                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                
                if (dto.isValidada)
                {
                    BaseDados.AddParameter("@VALIDADA", 1);
                    BaseDados.AddParameter("@VALIDADOR", dto.UserValidador);
                }
                else
                {
                    BaseDados.AddParameter("@VALIDADA", 0);
                    BaseDados.AddParameter("@VALIDADOR", DBNull.Value);
                }
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@ALUNO", dto.Aluno);//MATRICULA
                BaseDados.AddParameter("@INSCRICAO", dto.InscricaoAluno);
                BaseDados.AddParameter("@NOME", dto.NomeAluno);
                BaseDados.AddParameter("@PRIMEIRA", dto.PrimeiraNota);
                BaseDados.AddParameter("@SEGUNDA", dto.SegundaNota);
                BaseDados.AddParameter("@TERCEIRA", dto.TerceiraNota);
                BaseDados.AddParameter("@QUARTA", dto.QuartaNota);
                BaseDados.AddParameter("@MEDIA", dto.PauMedia);
                BaseDados.AddParameter("@EXAME", dto.PauExame);
                BaseDados.AddParameter("@EXAME_ORAL", dto.PauExameOral);
                BaseDados.AddParameter("@RECURSO", dto.PauRecurso);
                BaseDados.AddParameter("@ESPECIAL", dto.PauExaEspecial);
                BaseDados.AddParameter("@ORAL", dto.PauExaEspecialOral);
                BaseDados.AddParameter("@FINAL", dto.PauNotaFinal);
                if (!dto.PauNotaExtenso.Equals("&NBSP;"))
                 BaseDados.AddParameter("@EXTENSO", dto.PauNotaExtenso);
                else
                    BaseDados.AddParameter("@EXTENSO", DBNull.Value);

                BaseDados.AddParameter("@SITUACAO", dto.PauSituacao);

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

        public PautaDTO Remover(PautaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_PAUTA_REMOVER";

                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@INSCRICAO", dto.InscricaoAluno); 

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

        public List<PautaDTO> ObterDuplicadas(PautaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_PAUTA_OBTERDUPLICADA";


            BaseDados.AddParameter("@ALUNO", dto.InscricaoAluno);
            BaseDados.AddParameter("@CLASSE", dto.PauClasse);
            BaseDados.AddParameter("@DISCIPLINA", dto.DsDisciplina);

            List<PautaDTO> pauta = new List<PautaDTO>();
            PautaDTO cadeira = new PautaDTO();
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    cadeira = new PautaDTO();
                    cadeira.Disciplina = int.Parse(dr[0]);
                    cadeira.PauSituacao = dr[1];
                    cadeira.InscricaoAluno = dr[2];
                    pauta.Add(cadeira);

                }

            }
            catch (Exception ex)
            {
                cadeira.Sucesso = false;
                cadeira.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return pauta;
        }
    }
}
