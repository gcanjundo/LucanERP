using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class TurmaAlunoDAO
    {
        readonly ConexaoDB BaseDados;

        public TurmaAlunoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public TurmaAlunoDTO Adicionar(TurmaAlunoDTO dto) 
        {
            try 
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_ADICIONAR";

                BaseDados.AddParameter("ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("TURMA", dto.Turma.Codigo);
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

        public TurmaAlunoDTO AlterarEnsinoSuperior(TurmaAlunoDTO dtoAnterior, TurmaAlunoDTO dtoNova)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_ALTERAR";

                BaseDados.AddParameter("MATRICULA", dtoAnterior.Matricula.Codigo);
                BaseDados.AddParameter("ACTUAL", dtoAnterior.Turma.Codigo);
                BaseDados.AddParameter("NOVA", dtoNova.Turma.Codigo);
                BaseDados.AddParameter("ANO", dtoAnterior.Turma.Ano);
                BaseDados.AddParameter("CLASSE", dtoNova.Turma.Classe);
                BaseDados.AddParameter("OPERACAO", dtoNova.Operacao);

                BaseDados.ExecuteNonQuery();

                dtoNova.Sucesso = true;
            }
            catch (Exception ex)
            {
                dtoNova.Sucesso = false;
                dtoNova.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dtoNova;
        }

        public TurmaAlunoDTO TrocarClasse(TurmaAlunoDTO dtoAnterior, TurmaAlunoDTO dtoNova)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_ALTERAR_EG";

                BaseDados.AddParameter("MATRICULA", dtoAnterior.Matricula.Codigo);
                BaseDados.AddParameter("ACTUAL", dtoAnterior.Turma.Codigo);
                BaseDados.AddParameter("NOVA", dtoNova.Turma.Codigo);
                BaseDados.AddParameter("ANO", dtoAnterior.Turma.Ano);
                BaseDados.AddParameter("CLASSE", dtoNova.Turma.Classe);
                BaseDados.AddParameter("OPERACAO", dtoNova.Operacao);

                BaseDados.ExecuteNonQuery();

                dtoNova.Sucesso = true;
            }
            catch (Exception ex)
            {
                dtoNova.Sucesso = false;
                dtoNova.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dtoNova;
        }

        public List<TurmaAlunoDTO> ListaAlunos(TurmaAlunoDTO dto) 
        {
            List<TurmaAlunoDTO> lista;
            try
            {
                lista = new List<TurmaAlunoDTO>();
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_OBTERPORFILTRO";

                BaseDados.AddParameter("ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("TURMA", dto.Turma.Codigo);
                BaseDados.AddParameter("ANO", dto.Matricula.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) 
                {
                    dto = new TurmaAlunoDTO();
                    dto.Turma = new TurmaDAO().ObterPorPK(new TurmaDTO(int.Parse(dr["TUR_ALU_CODIGO_TURMA"].ToString())));

                    dto.Matricula = new MatriculaDAO().ObterPorPK(new MatriculaDTO(int.Parse(dr["TUR_ALU_CODIGO_ALUNO"].ToString())));

                    lista.Add(dto);
                }

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                lista = new List<TurmaAlunoDTO>();
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

        public void Remover(TurmaAlunoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_TURMA_ALUNO_REMOVER";

                BaseDados.AddParameter("MATRICULA", dto.Matricula.Codigo);
                BaseDados.AddParameter("TURMA", dto.Turma.Codigo);
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
    }
}
