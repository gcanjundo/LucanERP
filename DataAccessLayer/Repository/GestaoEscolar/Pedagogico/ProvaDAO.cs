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
    public class ProvaDAO 
    {
        readonly ConexaoDB BaseDados;

        public ProvaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public ProvaDTO Inserir(ProvaDTO dto)
        {
            

            try
            {
                BaseDados.ComandText = "stp_ACA_PROVA_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@DOCENTE", dto.Docente);
                BaseDados.AddParameter("@REALIZACAO", dto.DataProva);
                BaseDados.AddParameter("@SITUACAO", dto.Situacao);
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

        public ProvaDTO Alterar(ProvaDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_PROVA_ALTERAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@DOCENTE", dto.Docente);
                BaseDados.AddParameter("@REALIZACAO", DateTime.Now);
                BaseDados.AddParameter("@SITUACAO", dto.Situacao);
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

         
        public void Apagar(ProvaDTO dto)
        {
            
            BaseDados.ComandText = "stp_ACA_PROVA_EXCLUIR";

           BaseDados.AddParameter("@CODIGO", dto.Codigo);

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



        }

        public ProvaDTO ObterPorPK(ProvaDTO dto)
        {

            
            BaseDados.ComandText = "stp_ACA_PROVA_OBTERPORPK";
            
            BaseDados.AddParameter("@CODIGO", dto.Codigo);
            BaseDados.AddParameter("@PERIODO", dto.Periodo);
            BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
            BaseDados.AddParameter("@TURMA", dto.Turma);
            BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);

           List<NotaDTO> alunos = new List<NotaDTO>();
            try
            {

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto.Sucesso = true;
                while (dr.Read())
                {
                    dto = new ProvaDTO(); 
                    dto.Codigo = Int32.Parse(dr["PROV_CODIGO"].ToString());
                    dto.Periodo = dr["PROV_CODIGO_PERIODO"].ToString();
                    dto.Avaliacao = dr["AVA_ABREVIATURA"].ToString();
                    dto.Turma = dr["PROV_CODIGO_TURMA"].ToString();
                    dto.Disciplina = dr["PROV_CODIGO_DISCIPLINA"].ToString();
                    dto.Docente = dr["PROV_CODIGO_DOCENTE"].ToString();
                    dto.DataProva = Convert.ToDateTime(dr["PROV_DATA_PROVA"].ToString());
                    dto.Situacao = dr["PROV_STATUS"].ToString();
                    dto.Sucesso = true;
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

        public List<ProvaDTO> ObterPorFiltro(ProvaDTO dto)
        {

            
            List<ProvaDTO> provas = new List<ProvaDTO>();
            try
            {

                

                BaseDados.ComandText = "stp_ACA_PROVA_OBTERPORFILTRO";

                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
                BaseDados.AddParameter("@DOCENTE", dto.Docente);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@TURMA", dto.Turma);


                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ProvaDTO();

                    dto.Codigo = Int32.Parse(dr["PROV_CODIGO"].ToString());
                    dto.Periodo = dr["PER_DESCRICAO"].ToString();
                    dto.Avaliacao = dr["AVA_DESCRICAO"].ToString();
                    dto.Turma = dr["TUR_ABREVIATURA"].ToString();
                    dto.Disciplina = dr["DIS_DESCRICAO"].ToString();
                    dto.Docente = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.DataProva = Convert.ToDateTime(dr["PROV_DATA_PROVA"].ToString());
                    dto.Situacao = dr["STA_DESCRICAO"].ToString();  

                    provas.Add(dto);

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

            return provas;
        }
    }
}
