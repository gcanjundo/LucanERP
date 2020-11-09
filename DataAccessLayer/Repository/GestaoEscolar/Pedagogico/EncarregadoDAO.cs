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
    public class EncarregadoDAO
    {

        readonly ConexaoDB BaseDados;

        public EncarregadoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public EncarregadoDTO Inserir(EncarregadoDTO dto)
        {
           

            BaseDados.ComandText = "stp_ACA_ALUNO_ENCARREGADO_ADICIONAR";

            
            
            

           BaseDados.AddParameter("@ALUNO", dto.EncAluno.Codigo);
           BaseDados.AddParameter("@NOME", dto.EncNome);
           BaseDados.AddParameter("@EMAIL", dto.EncEmail);
           BaseDados.AddParameter("@TELEFONE", dto.EncTelefone);
           BaseDados.AddParameter("@TELEMOVEL", dto.EncTelemovel);
           BaseDados.AddParameter("@PARENTESCO", dto.EncParentesco);

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

            return dto;
        }

       

        public EncarregadoDTO Apagar(EncarregadoDTO dto)
        {
            

            BaseDados.ComandText = "stp_ACA_ALUNO_ENCARREGADO_EXCLUIR";

            
            
            


           BaseDados.AddParameter("@CODIGO", dto.EncAluno.Codigo);
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

            return dto;
        }

        public ListaEncarregadosDTO ObterPorFiltro(EncarregadoDTO dto)
        {


            BaseDados.ComandText = "stp_ACA_ALUNO_ENCARREGADO_OBTERPORFILTRO";

            
            
            

           BaseDados.AddParameter("@NOME", dto.EncNome.Trim());
           BaseDados.AddParameter("@ALUNO", dto.EncAluno.NomeCompleto.Trim());

            ListaEncarregadosDTO encarregados = new ListaEncarregadosDTO();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();


                while (dr.Read())
                {
                    dto = new EncarregadoDTO();

                    AlunoDTO dtoAluno = new AlunoDTO();
                    dtoAluno.Codigo = Int32.Parse(dr["FIL_CODIGO_ALUNO"].ToString());

                    AlunoDAO daoAluno = new AlunoDAO();
                    dtoAluno = daoAluno.ObterPorPK(dtoAluno);

                    dto.EncAluno = dtoAluno;

                    dto.EncEmail = dr["FIL_EMAIL"].ToString();
                    dto.EncParentesco = dr["FIL_PARENTESCO"].ToString();
                    dto.EncNome = dr["FIL_NOME"].ToString();
                    dto.EncTelemovel = dr["FIL_TELEFONE1"].ToString();
                    dto.EncTelefone = dr["FIL_TELEFONE"].ToString();

                    encarregados.Add(dto);
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

            return encarregados;
        }

        public EncarregadoDTO ObterPorPK(EncarregadoDTO dto)
        {


            BaseDados.ComandText = "stp_ACA_ALUNO_ENCARREGADO_OBTERPORPK";

            
            
            

           BaseDados.AddParameter("@CODIGO", dto.EncAluno.Codigo);

            ListaEncarregadosDTO encarregados = new ListaEncarregadosDTO();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new EncarregadoDTO();
               while (dr.Read())
                {
                    

                    AlunoDTO dtoAluno = new AlunoDTO();
                    dtoAluno.Codigo = Int32.Parse(dr["FIL_CODIGO_ALUNO"].ToString());

                    AlunoDAO daoAluno = new AlunoDAO();
                    dtoAluno = daoAluno.ObterPorPK(dtoAluno);

                    dto.EncAluno = dtoAluno;

                    dto.EncEmail = dr["FIL_EMAIL"].ToString();
                    dto.EncParentesco = dr["FIL_PARENTESCO"].ToString();
                    dto.EncNome = dr["FIL_NOME"].ToString();
                    dto.EncTelemovel = dr["FIL_TELEFONE1"].ToString();
                    dto.EncTelefone = dr["FIL_TELEFONE"].ToString();

                    encarregados.Add(dto);
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
