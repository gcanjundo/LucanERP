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
    public class SolicitacaoMovimentacaoDAO
    {
        readonly ConexaoDB BaseDados;

        public SolicitacaoMovimentacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public SolicitacaoDTO Inserir(SolicitacaoDTO dto)
        {
           
            try
            {
                BaseDados.ComandText = "stp_ACA_REQUISICAO_MOVIMENTACAO_ADICIONAR";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@MOVIMENT_TYPE", dto.Tipo);
                BaseDados.AddParameter("@STUDENT", dto.Solicitante.Codigo);
                BaseDados.AddParameter("@MOTIVO", dto.Motivo);
                BaseDados.AddParameter("@ESTADO", dto.Status); 
                 
                if (dto.DataDeferimento != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@DEFERIMENTO", dto.DataDeferimento);
                }
                else
                {
                    BaseDados.AddParameter("@DEFERIMENTO", DBNull.Value);
                }
                BaseDados.AddParameter("@MOTIVO_DEFERIMENTO", dto.MotivoDeferimento); 
                BaseDados.AddParameter("@ASSINATURA_DEFERIMENTO", dto.ResponsavelDeferimento); 
                BaseDados.AddParameter("@SOLICITANTE", dto.Solicitante.Responsavel); 
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@CURSO", dto.CursoDestino);
                BaseDados.AddParameter("@INSTITUICAO", dto.Instituicao); 
                BaseDados.AddParameter("@RECIBO_ID", dto.Recibo);
                BaseDados.AddParameter("@NOTAS_PEDAGOGIA", dto.ParecerPedagogia);
                BaseDados.AddParameter("@URGENCIA", dto.IsUrgente);
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

       

        public SolicitacaoDTO Apagar(SolicitacaoDTO dto)
        {
            

            
            try
            {
                BaseDados.ComandText = "stp_ACA_REQUISICAO_MOVIMENTACAO_EXCLUIR";

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

            return dto;
        }

        public SolicitacaoDTO ObterPorPK(SolicitacaoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_ACA_REQUISICAO_MOVIMENTACAO_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new SolicitacaoDTO();

                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["SOL_CODIGO"].ToString()); 
                    dto.Tipo = dr["SOL_TIPO"].ToString();
                    dto.Data = DateTime.Parse(dr["SOL_DATA"].ToString());
                    dto.Numero = dto.Tipo + "/" + dr["SOL_DATA"].ToString() + "/" + dr["ANO_ANO_LECTIVO"].ToString();

                    AlunoDTO oAluno = new AlunoDTO();
                    oAluno.Codigo = Int32.Parse(dr["SOL_STUDENT_ID"].ToString());
                    oAluno.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    oAluno.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                    oAluno.Responsavel = dr["SOL_SOLICITANTE"].ToString();
                    dto.Solicitante = oAluno;
                    dto.Motivo = dr["SOL_MOTIVO"].ToString();
                    dto.Status = dr["SOL_STATUS"].ToString();
                    dto.DataDeferimento = Convert.ToDateTime(dr["SOL_DATA_DEFERIMENTO"].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr["SOL_DATA_DEFERIMENTO"].ToString());
                    dto.MotivoDeferimento = dr["SOL_MOTIVO_DEFERIMENTO"].ToString(); 
                    dto.ResponsavelDeferimento = dr["SOL_ASSINATURA_DEFERIMENTO"].ToString();
                    dto.Filial = dr["SOL_FILIAL"].ToString();
                    dto.AnoLectivo = Int32.Parse(dr["SOL_ANO_LECTIVO"].ToString());
                    dto.CursoDestino = dr["SOL_COURSE_ID"].ToString();
                    dto.Instituicao = dr["SOL_INSTITUICAO"].ToString();
                    dto.Recibo = dr["SOL_FINANCIAL_DOCUMENT_ID"].ToString();
                    dto.ParecerPedagogia = dr["SOL_PARECE_PEDAGOGIA"].ToString();
                    dto.IsUrgente = int.Parse(dr["SOL_URGENTE"].ToString() == string.Empty ? "0" : dr["SOL_URGENTE"].ToString());
                    dto.Emissor = dr["SOL_CREATED_BY"].ToString();
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

        public List<SolicitacaoDTO> ObterPorFiltro(SolicitacaoDTO dto)
        {
             
            List<SolicitacaoDTO> declaracoes = new List<SolicitacaoDTO>();
            try
            {
                
                BaseDados.ComandText = "stp_ACA_REQUISICAO_MOVIMENTACAO_OBTERPORFILTRO";


            if (dto.De != null && !dto.De.Equals(""))
                BaseDados.AddParameter("@INICIO", dto.De.Replace('/', '-'));
            else
                BaseDados.AddParameter("@INICIO", DBNull.Value);

            if (dto.Ate != null && !dto.Ate.Equals(""))
                BaseDados.AddParameter("@TERMINO", dto.Ate.Replace('/', '-'));
            else
                BaseDados.AddParameter("@TERMINO", DBNull.Value);

               BaseDados.AddParameter("@SOLICITANTE", dto.Solicitante.NomeCompleto);
               BaseDados.AddParameter("@SOLICITACAO", dto.Codigo);
               BaseDados.AddParameter("@ESTADO", dto.Status);
               BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
               BaseDados.AddParameter("@FILIAL", dto.Filial);

                    MySqlDataReader dr = BaseDados.ExecuteReader();


                while (dr.Read())
                {
                    dto = new SolicitacaoDTO();

                    dto.Codigo = Int32.Parse(dr["SOL_CODIGO"].ToString());
                    
                    AlunoDTO dtoAluno = new AlunoDTO();
                    dtoAluno.NomeCompleto = dr["STUDENT_INFO"].ToString();
                    dtoAluno.Responsavel = dr["SOL_SOLICITANTE"].ToString();
                    dto.Solicitante = dtoAluno;
                    dto.Data = Convert.ToDateTime(dr["SOL_DATA"].ToString());
                    dto.Documento =  dr["SOLICITACAO"].ToString();
                    dto.Status = dr["SITUACAO"].ToString();
                    dto.Instituicao = dr["SOL_INSTITUICAO"].ToString();
                    dto.CursoDestino = dr["CUR_NOME"].ToString();
                    dto.SocialName = dr["UTI_NOME"].ToString();
                    dto.DataDeferimento = DateTime.Parse(dr["SOL_DATA_DEFERIMENTO"].ToString() == string.Empty ?
                        DateTime.MinValue.ToString() : dr["SOL_DATA_DEFERIMENTO"].ToString());
                    dto.Numero = dr["SOL_TIPO"].ToString() + "/" + dr["SOL_NUMERACAO"].ToString() + "/" + dr["ANO_ANO_LECTIVO"].ToString();
                    declaracoes.Add(dto);
                }

            }
            catch (Exception ex)
            {
                declaracoes = new List<SolicitacaoDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                declaracoes.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return declaracoes;
        }

    }
}
