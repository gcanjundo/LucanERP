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
    public class SolicitacaoDocumentosDAO
    {
        readonly ConexaoDB BaseDados;

        public SolicitacaoDocumentosDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public SolicitacaoDTO Inserir(SolicitacaoDTO dto)
        {
           
            try
            {

                BaseDados.ComandText = "stp_ACA_DECLARACAO_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@SOLICITANTE", dto.Solicitante.Codigo);
                BaseDados.AddParameter("@OPERADOR", dto.Emissor);
                BaseDados.AddParameter("@ASSINATURA", dto.De);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@MOTIVO", dto.Motivo);
                BaseDados.AddParameter("@RECIBO", dto.Recibo);                
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@ENTIDADE", dto.Instituicao);
                BaseDados.AddParameter("@OBSERVACOES", dto.Observacoes);
                BaseDados.AddParameter("@PARECER", dto.ParecerPedagogia);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@ANO_CURRICULAR", dto.AnoCivil);

                dto.Codigo = BaseDados.ExecuteInsert();
                
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
                BaseDados.ComandText = "stp_ACA_DECLARACAO_EXCLUIR";

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

                BaseDados.ComandText = "stp_ACA_DECLARACAO_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new SolicitacaoDTO();

                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["DEC_CODIGO"].ToString());
                    dto.Numero = dr["DEC_NUMERO"].ToString();

                    AlunoDTO dtoAluno = new AlunoDTO();
                    dtoAluno.Codigo = Int32.Parse(dr["DEC_CODIGO_SOLICITANTE"].ToString());
                    dtoAluno.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dtoAluno.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.Solicitante =dtoAluno;

                    dto.Documento = dr["DEC_DOCUMENTO"].ToString();
                    dto.Status = dr["DEC_STATUS"].ToString();
                    dto.Emissor = dr["DEC_OPERADOR"].ToString();
                    dto.Data = Convert.ToDateTime(dr["DEC_DATA_SOLICITACAO"].ToString());
                    dto.Recibo = dr["DEC_RECIBO"].ToString();
                    dto.Motivo = dr["DEC_MOTIVO"].ToString();
                    dto.Instituicao = dr["DEC_INSTITUICAO"].ToString();
                    dto.ParecerPedagogia = dr["DEC_PARECER_ACADEMICO"].ToString();
                    dto.Observacoes = dr["DEC_OBS"].ToString();
                    dto.AnoLectivo = int.Parse(dr["DEC_ANO_LECTIVO"].ToString() == "" ? "-1" : dr["DEC_ANO_LECTIVO"].ToString());
                    dto.AnoCivil = int.Parse(dr["DEC_ANO_CURRICULAR"].ToString() == "" ? "-1" : dr["DEC_ANO_CURRICULAR"].ToString()); // Classe
                    dto.DocFile = dr["DOC_TEMPLATE"].ToString();

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
                
                BaseDados.ComandText = "stp_ACA_DECLARACAO_OBTERPORFILTRO";


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

                    dto.Codigo = Int32.Parse(dr["DEC_CODIGO"].ToString());
                    
                    AlunoDTO dtoAluno = new AlunoDTO();
                    dtoAluno.NomeCompleto = dr["SOLICITANTE"].ToString();
                    dto.Solicitante = dtoAluno;
                    dto.Data = Convert.ToDateTime(dr["DEC_DATA_SOLICITACAO"].ToString());
                    dto.Documento =  dr["DEC_DOCUMENTO"].ToString();
                    dto.Status = dr["SITUACAO"].ToString();
                    dto.Emissor = dr["UTI_NOME"].ToString();
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


        public bool ConfirmacaoRecibo(SolicitacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_SOLICITACAO_DOCUMENTO_CONFIRMAR_RECIBO";

                BaseDados.AddParameter("FATURA", dto.Recibo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    if (dr[0].Equals(dto.Recibo))
                    {
                        dto.Sucesso = true;
                        break;
                    }
                     
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

            return dto.Sucesso;
        }

        public void RegistarStatus(SolicitacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_DECLARACAO_ACTUALIZACAO_STATUS";
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("UTILIZADOR", dto.Emissor);
                BaseDados.AddParameter("ACCAO", dto.Status);

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }
        }

        public SolicitacaoDTO ObterRequisicao(SolicitacaoDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_ACA_DECLARACAO_OBTER_REQUISICAO";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ANO", dto.Numero);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new SolicitacaoDTO();

                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["DEC_CODIGO"].ToString());
                    dto.Numero = dr["DEC_NUMERO"].ToString();

                    AlunoDTO dtoAluno = new AlunoDTO();
                    dtoAluno.Codigo = Int32.Parse(dr["DEC_SOLICITANTE"].ToString());
                    dtoAluno.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dtoAluno.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                    dtoAluno.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    dtoAluno.Curso = dr["CURSO"].ToString();
                    dtoAluno.Turno = dr["TURMA"].ToString();
                    dtoAluno.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dto.Solicitante = dtoAluno;
                    dto.Documento = dr["DEC_DOCUMENTO"].ToString();
                    dto.Status = dr["DEC_STATUS"].ToString();
                    dto.Emissor = dr["DEC_OPERADOR"].ToString();
                    dto.Data = Convert.ToDateTime(dr["DEC_DATA_SOLICITACAO"].ToString());
                    dto.Recibo = dr["DEC_RECIBO"].ToString();
                    dto.Motivo = dr["DEC_MOTIVO"].ToString();



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

        public SolicitacaoDTO ObterSemNotas(SolicitacaoDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_ACA_DECLARACAO_OBTERSEMNOTAS";
                BaseDados.AddParameter("@CODIGO", dto.Codigo); 

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new SolicitacaoDTO();

                while (dr.Read())
                {
                    AlunoDTO dtoAluno = new AlunoDTO();

                    dto.Numero = dr["DEC_NUMERO"].ToString();
                    dtoAluno.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                    dtoAluno.Nacionalidade = dr["PAI_NACIONALIDADE"].ToString();
                    dtoAluno.NomeDocumento = dr["DOC_DESCRICAO"].ToString();
                    dtoAluno.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    dto.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dtoAluno.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dtoAluno.Curso = dr["CURSO"].ToString();
                    dtoAluno.Turno = dr["TURMA"].ToString() + "(" + dr["TURNO"].ToString() + ")";
                    dto.Motivo = dr["DEC_MOTIVO"].ToString();
                    dto.Emissor = dr["UTI_NOME"].ToString();
                    dtoAluno.NomePai = dr["ALU_NOME_PAI"].ToString();
                    dtoAluno.NomeMae = dr["ALU_NOME_MAE"].ToString();
                    dtoAluno.DataNascimento = DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString());
                    dtoAluno.ClasseProveniencia = dr["PLAN_DESCRICAO"].ToString();
                    dto.Instituicao = dr["DEC_INSTITUICAO"].ToString();
                    dtoAluno.NomeMunicipio = dr["MUN_DESCRICAO"].ToString();
                    dtoAluno.Responsavel = dr["PROV_DESCRICAO"].ToString();
                    dtoAluno.LocalEmissao = dr["ENT_DOC_LOCAL_EMISSAO"].ToString();
                    dtoAluno.Emissao = DateTime.Parse(dr["DATA_EMISSAO"].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr["DATA_EMISSAO"].ToString());
                    dto.NivelEnsino = dr["CLASSIFICACAO_FINAL"].ToString();
                    dtoAluno.SiglaTurma = dr["TURMA"].ToString();
                    dtoAluno.Turno = dr["TURNO"].ToString();
                    dtoAluno.Encarregado = dr["ALU_ENCARREGADO"].ToString();
                    dtoAluno.TelEncarregado = dr["ALU_ENCARREGADO_TELEFONE"].ToString();
                    dtoAluno.Sexo = dr["SEXO"].ToString();
                    dtoAluno.Telefone = dr["ENT_TELEFONE"].ToString();
                    
                    dto.Solicitante = dtoAluno; 

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

        public List<SolicitacaoDTO> ObterComNotas(SolicitacaoDTO dto)
        {

            List<SolicitacaoDTO> declaracoes = new List<SolicitacaoDTO>();
            try
            {

                BaseDados.ComandText = "stp_ACA_DECLARACAO_OBTERCOMNOTAS";

                 
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ANO", dto.Solicitante.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();


                while (dr.Read())
                {
                    dto = new SolicitacaoDTO();

                    AlunoDTO dtoAluno = new AlunoDTO();

                    dto.Numero = dr["DEC_NUMERO"].ToString();
                    dtoAluno.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                    dtoAluno.Nacionalidade = dr["PAI_NACIONALIDADE"].ToString();
                    dtoAluno.NomeDocumento = dr["DOC_DESCRICAO"].ToString();
                    dtoAluno.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    dtoAluno.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dtoAluno.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dtoAluno.Curso = dr["CURSO"].ToString();
                    dtoAluno.Turno = dr["TURMA"].ToString() + "(" + dr["TURNO"].ToString() + ")";
                    dto.Motivo = dr["DEC_MOTIVO"].ToString();
                    dto.Emissor = dr["UTI_NOME"].ToString();
                    dto.Disciplina = dr["DISCIPLINA"].ToString();
                    dto.Nota = decimal.Parse(dr["NOTA_FINAL"].ToString());
                    dtoAluno.Emissao = DateTime.Parse(dr["DATA_EMISSAO"].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr["DATA_EMISSAO"].ToString());

                    dto.Solicitante = dtoAluno; 

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
