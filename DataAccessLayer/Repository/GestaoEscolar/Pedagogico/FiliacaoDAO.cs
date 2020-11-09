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
    public class FiliacaoDAO
    {

        readonly ConexaoDB BaseDados;

        public FiliacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public FiliacaoDTO Adicionar(FiliacaoDTO dto) 
        {
            
            BaseDados.ComandText = "stp_ACA_FILIACAO_ADICIONAR";

            

           BaseDados.AddParameter("@NOME", dto.FilNome);
           BaseDados.AddParameter("@IDENTIFICACAO", dto.FilIdentificacao);
           BaseDados.AddParameter("@EMAIL", dto.FilEmail);
           BaseDados.AddParameter("@TELEFONE", dto.FilTelefone);
           BaseDados.AddParameter("@TELALTERNATIVO", dto.FiLTelAlternativo);
           BaseDados.AddParameter("@HABILITACOES", dto.FilHabilitacoesID);
           BaseDados.AddParameter("@INSTITUICAO", dto.FilInstituicaoID);
           BaseDados.AddParameter("@PROFISSAO", dto.FilProfissaoID);
           BaseDados.AddParameter("@CODIGO", dto.FilCodigo);
           BaseDados.AddParameter("@ALUNO", dto.FilAlunoFiliacao.AluFilCodigo);
           BaseDados.AddParameter("@PARENTESCO", dto.FilAlunoFiliacao.AluParentesco);
            
            if (dto.FilDtNascimento != DateTime.MinValue)
            {
               BaseDados.AddParameter("@DATA_NASCIMENTO", dto.FilDtNascimento);
            }
            else
            {
               BaseDados.AddParameter("@DATA_NASCIMENTO", DBNull.Value);
            }
            try
            {
                
                dto.FilCodigo = BaseDados.ExecuteInsert();
               

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

        public FiliacaoDTO JaExiste(FiliacaoDTO dto)
        {


            BaseDados.ComandText = "stp_ACA_FILIACAO_JAEXISTE";

            
            
            

           BaseDados.AddParameter("@IDENTIFICACAO", dto.FilIdentificacao);
            
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                dto = new FiliacaoDTO();
                while (dr.Read())
                {
                    dto.FilCodigo = int.Parse(dr["PAI_CODIGO"].ToString());
                    dto.FilEmail = dr["PAI_EMAIL"].ToString();
                    dto.FilHabilitacoesID = int.Parse(dr["PAI_CODIGO_Habilitacoes"].ToString());
                    dto.FilIdentificacao = dr["PAI_IDENTIFICACAO"].ToString();
                    dto.FilInstituicaoID = int.Parse(dr["PAI_CODIGO_INSTITUICAO"].ToString());
                    dto.FilNome = dr["PAI_NOME"].ToString();
                    dto.FilProfissaoID = int.Parse(dr["PAI_CODIGO_PROFISSAO"].ToString());
                    dto.FiLTelAlternativo = dr["PAI_TELEMOVEL"].ToString();
                    dto.FilTelefone = dr["PAI_TELEFONE"].ToString();

                    if (!dr["PAI_DATA_NASCIMENTO"].ToString().Equals(String.Empty))
                    {
                        dto.FilDtNascimento = Convert.ToDateTime(dr["PAI_DATA_NASCIMENTO"].ToString());
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

        public ListaFiliacaoDTO obterPorFiltro(FiliacaoDTO dto)
        {


            BaseDados.ComandText = "stp_ACA_ALUNO_FILIACAO_OBTERPORFILTRO";

            
            
            

           BaseDados.AddParameter("@ALUNO", dto.FilAlunoFiliacao.AluFilCodigo);
           BaseDados.AddParameter("@FILIACAO", dto.FilCodigo);


            ListaFiliacaoDTO pais = new ListaFiliacaoDTO();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                ListaAlunoFiliacaoDTO progenitores = new ListaAlunoFiliacaoDTO();
                while (dr.Read())
                {
                    dto = new FiliacaoDTO();

                    dto.FilCodigo = int.Parse(dr["PAI_CODIGO"].ToString());
                    dto.FilEmail = dr["PAI_EMAIL"].ToString();
                    dto.FilHabilitacoesID = int.Parse(dr["PAI_CODIGO_Habilitacoes"].ToString());
                    dto.FilIdentificacao = dr["PAI_IDENTIFICACAO"].ToString();
                    dto.FilInstituicaoID = int.Parse(dr["PAI_CODIGO_INSTITUICAO"].ToString());
                    dto.FilNome = dr["PAI_NOME"].ToString();
                    dto.FilProfissaoID = int.Parse(dr["PAI_CODIGO_PROFISSAO"].ToString());
                    dto.FiLTelAlternativo = dr["PAI_TELEMOVEL"].ToString();
                    dto.FilTelefone = dr["PAI_TELEFONE"].ToString();
                    if (!dr["PAI_DATA_NASCIMENTO"].ToString().Equals(String.Empty))
                    {
                      dto.FilDtNascimento = Convert.ToDateTime(dr["PAI_DATA_NASCIMENTO"].ToString());
                    }
                    AlunoFiliacaoDTO objProgenitor = new AlunoFiliacaoDTO();
                    objProgenitor.AluFilCodigo = int.Parse(dr["ALU_CODIGO_ALUNO"].ToString());
                    objProgenitor.AluFiliacao = int.Parse(dr["PAI_CODIGO"].ToString());
                    objProgenitor.AluParentesco = dr["ALU_PARENTESCO"].ToString();
                    dto.FilAlunoFiliacao = objProgenitor;
                    progenitores.Add(objProgenitor);
                    dto.Pais = progenitores;

                    pais.Add(dto);
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

            return pais;
        }
    }
}
