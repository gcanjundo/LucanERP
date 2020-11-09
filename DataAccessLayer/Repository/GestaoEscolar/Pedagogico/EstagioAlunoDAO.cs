using Dominio.GestaoEscolar.Pedagogia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class EstagioAlunoDAO
    {
        readonly ConexaoDB BaseDados;

        public EstagioAlunoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public void Adicionar(EstagioAlunoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_ALUNO_ADICIONAR";

                BaseDados.AddParameter("@ESTAGIO", dto.EstagioID);
                BaseDados.AddParameter("@ALUNO", dto.MatriculaID); 
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@PAYMENT_ID", dto.PaymentID);

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

        public void Excluir(EstagioAlunoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_ALUNO_EXCLUIR";

                BaseDados.AddParameter("@ESTAGIO", dto.EstagioID);
                BaseDados.AddParameter("@ALUNO", dto.AlunoID);
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

        public void LancarNota(EstagioAlunoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_ALUNO_ADDNOTA";

                BaseDados.AddParameter("@ESTAGIO", dto.EstagioID);
                BaseDados.AddParameter("@ALUNO", dto.MatriculaID);
                BaseDados.AddParameter("@TEORIA", dto.Teoria);
                BaseDados.AddParameter("@PRATICA", dto.Pratica);
                BaseDados.AddParameter("@FINAL", dto.NotaFinal);
                BaseDados.AddParameter("@NOTES", dto.Observacoes);
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

        public List<EstagioAlunoDTO> ObterPorFiltro(EstagioAlunoDTO dto)
        {
            var lista = new List<EstagioAlunoDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_ALUNOS_OBTERPORFILTRO";

                BaseDados.AddParameter("@ESTAGIO", dto.EstagioID);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                int ordem = 1;
                foreach (var dr in reader)
                {
                    dto = new EstagioAlunoDTO();
                    dto.Ordem = ordem;
                    dto.EstagioID = int.Parse(dr["EST_CODIGO_ESTAGIO"].ToString());
                    dto.MatriculaID = int.Parse(dr["EST_CODIGO_ALUNO"].ToString());
                    dto.Teoria = dr["EST_NOTA_TEORIA"].ToString() != null ? decimal.Parse(dr["EST_NOTA_TEORIA"].ToString()) : -1;
                    dto.Pratica = dr["EST_NOTA_PRATICA"].ToString()!=null ? decimal.Parse(dr["EST_NOTA_PRATICA"].ToString()): -1;
                    dto.NotaFinal = dr["EST_NOTA_FINAL"].ToString()!=null ? decimal.Parse(dr["EST_NOTA_FINAL"].ToString()): -1;
                    dto.Observacoes = dr["EST_NOTES"].ToString();
                    dto.CreatedBy = dr["EST_CREATED_BY"].ToString();
                    dto.CreatedDate = DateTime.Parse(dr["EST_CREATED_DATE"].ToString());
                    dto.DeletedBy = dr["EST_DELETED_BY"].ToString();
                    dto.DeletedDate = dr["EST_DELETED_DATE"].ToString()!=null ? DateTime.Parse(dr["EST_DELETED_DATE"].ToString()) : DateTime.MinValue;
                    dto.AlunoID = int.Parse(dr["EST_CODIGO_ALUNO"].ToString());
                    dto.NroInscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.NroProcesso = dr["ALU_NUMERO_MANUAL"].ToString();
                    dto.SocialName = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.Curso = dr["CUR_NOME"].ToString()+" "+ dr["PLAN_DESCRICAO"].ToString()+" "+ dr["TUR_ABREVIATURA"].ToString();
                    dto.PaymentID = dr["EST_PAYMENT_ID"].ToString() != null ? int.Parse(dr["EST_PAYMENT_ID"].ToString()) : dto.PaymentID;
                    lista.Add(dto);
                    ordem++;

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

        public List<EstagioAlunoDTO> ObterNotas(EstagioAlunoDTO dto)
        {
            var lista = new List<EstagioAlunoDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_ALUNO_OBTERNOTAS";

                BaseDados.AddParameter("@TURMA", dto.TurmaID);
                BaseDados.AddParameter("@ALUNO_ID", dto.AlunoID);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@MATRICULA_ID", dto.MatriculaID);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                int ordem = 1;
                foreach (var dr in reader)
                {
                    dto = new EstagioAlunoDTO();
                    dto.Ordem = ordem; 
                    dto.MatriculaID = int.Parse(dr["EST_CODIGO_ALUNO"].ToString()); 
                    dto.NotaFinal = dr["EST_NOTA_FINAL"].ToString() != null ? decimal.Parse(dr["EST_NOTA_FINAL"].ToString()) : -1; 
                    lista.Add(dto);
                    ordem++;

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
