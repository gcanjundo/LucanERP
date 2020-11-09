using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;
using DataAccessLayer.Geral;
using DataAccessLayer.Seguranca;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class AlunoDAO
    {
        readonly ConexaoDB BaseDados;

        public AlunoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public AlunoDTO Adicionar(AlunoDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);
                BaseDados.AddParameter("@NOME_COMPLETO", dto.NomeCompleto);
                BaseDados.AddParameter("@DATA_NASCIMENTO", dto.DataNascimento);
                BaseDados.AddParameter("@NACIONALIDADE", dto.Nacionalidade);
                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", dto.MunicipioNascimento); // MUNICIPIO DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", DBNull.Value);

                }
                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", dto.MunicipioMorada); // MUNICIPIO DE MORADA

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", DBNull.Value);

                }

                BaseDados.AddParameter("@RUA", dto.Rua);
                BaseDados.AddParameter("@BAIRRO", dto.Bairro);
                BaseDados.AddParameter("@TELEFONE", dto.Telefone);
                BaseDados.AddParameter("@TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@INCLUSAO", DateTime.Now);
                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@SOBRENOME", dto.SobreNome);
                BaseDados.AddParameter("@SEXO", dto.Sexo);
                
                if (dto.EstadoCivil.Equals("-1"))
                {
                    dto.EstadoCivil = string.Empty;
                }
                BaseDados.AddParameter("@CIVIL", dto.EstadoCivil);
                BaseDados.AddParameter("@PAI", dto.NomePai);
                BaseDados.AddParameter("@MAE", dto.NomeMae);
                BaseDados.AddParameter("@MATRICULA", dto.Inscricao);
                BaseDados.AddParameter("@INSCRICAO", dto.DataInscricao);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@ENCARREGADO", dto.Encarregado);
                BaseDados.AddParameter("@TEL_ENCARREGADO", dto.TelEncarregado);
                BaseDados.AddParameter("@ENC_EMAIL", dto.EncEmail);
                BaseDados.AddParameter("@ENC_ALTERNATIVO", dto.EncTelAlternativo);
                BaseDados.AddParameter("@PARENTESCO", dto.Parentesco);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@SITUACAO", dto.Situacao);

                if (dto.Classe > 0)
                {
                    BaseDados.AddParameter("@CLASSE", dto.Classe);
                }
                else
                {
                    BaseDados.AddParameter("@CLASSE", DBNull.Value);
                }
                if (dto.Turma > 0)
                {
                    BaseDados.AddParameter("@TURMA", dto.Turma);
                }
                else
                {
                    BaseDados.AddParameter("@TURMA", DBNull.Value);
                }
                
                BaseDados.AddParameter("@STATUS_INI", dto.SituacaoInicial);
                BaseDados.AddParameter("@STATUS_TERM", dto.SituacaoFinal);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("@PAIS", dto.PaisNascimento);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@EMAIL_ALT", dto.WebSite);
                BaseDados.AddParameter("@TELF_FAX", dto.TelefoneFax);

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento); //PROVINCIA DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@NATURALIDADE", DBNull.Value);

                }

                
                if (int.Parse(dto.Filial)>0)
                    BaseDados.AddParameter("@FILIAL", dto.Filial);
                else
                    BaseDados.AddParameter("@FILIAL", DBNull.Value);

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                BaseDados.AddParameter("@TURNO", dto.Turno=="-1" ? "M" : dto.Turno);
                BaseDados.AddParameter("@ENC_DOC", dto.TituloDocumento);
                BaseDados.AddParameter("@DISTRITO", dto.Morada);
                BaseDados.AddParameter("@LINGUA", dto.OpcaoLingua);
                BaseDados.AddParameter("@INSTITUICAO", dto.EscolaProveniencia);
                BaseDados.AddParameter("@SAIDA", dto.SaidaProveniencia);
                BaseDados.AddParameter("@SITUACAO_PROVENIENCIA", dto.SituacaoProveniencia);
                BaseDados.AddParameter("@CURSO_PROVENIENCIA", dto.CursoProveniencia);
                BaseDados.AddParameter("@PROVENIENCIA_CLASSE", dto.ClasseProveniencia);

                BaseDados.AddParameter("@PROCESSO", dto.NroExterno == string.Empty ? "-1" : dto.NroExterno);
                BaseDados.AddParameter("@EMISSAO", dto.Emissao);
                BaseDados.AddParameter("@LOCAL_EMISSAO", dto.LocalEmissao);
                BaseDados.AddParameter("@SHORTNAME", dto.ShortName);
                BaseDados.AddParameter("@EXTERNO", dto.IsExterno);

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

        public AlunoDTO Alterar(AlunoDTO dto)
        {
            //stp_ACA_ALUNO_UPDATE_ID
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ALTERAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@NOME_COMPLETO", dto.Nome + " " + dto.SobreNome);
                BaseDados.AddParameter("@DATA_NASCIMENTO", dto.DataNascimento);

                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", dto.MunicipioNascimento); // MUNICIPIO DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", DBNull.Value);

                }

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento); //PROVINCIA DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@NATURALIDADE", DBNull.Value);

                }
                BaseDados.AddParameter("@NACIONALIDADE", dto.Nacionalidade);

                BaseDados.AddParameter("@PAIS", dto.PaisNascimento);

                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", dto.MunicipioMorada); // MUNICIPIO DE MORADA

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", DBNull.Value);

                }
                BaseDados.AddParameter("@BAIRRO", dto.Bairro);
                BaseDados.AddParameter("@RUA", dto.Rua);
                BaseDados.AddParameter("@TELEFONE", dto.Telefone);
                BaseDados.AddParameter("@TELF_ALT", dto.TelefoneAlt);


                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@INCLUSAO", DateTime.Now);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);

                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@SOBRENOME", dto.SobreNome);
                BaseDados.AddParameter("@SEXO", dto.Sexo);
                BaseDados.AddParameter("@PAI", dto.NomePai);
                BaseDados.AddParameter("@MAE", dto.NomeMae);
                BaseDados.AddParameter("@CIVIL", dto.EstadoCivil);

                if (dto.PathFoto != null && !dto.PathFoto.Equals(""))
                {
                    BaseDados.AddParameter("@NOME_FOTO", dto.PathFoto);
                    BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);
                    BaseDados.AddParameter("@FOTOGRAFIA", SqlDbType.VarBinary);

                }
                else
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", SqlDbType.VarBinary);
                    BaseDados.AddParameter("@NOME_FOTO", DBNull.Value);
                    BaseDados.AddParameter("@EXTENSAO", DBNull.Value);
                }



                BaseDados.AddParameter("@MATRICULA", dto.Inscricao);
                BaseDados.AddParameter("@INSCRICAO", dto.DataInscricao);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@ENCARREGADO", dto.Encarregado);
                BaseDados.AddParameter("@TEL_ENCARREGADO", dto.TelEncarregado);
                BaseDados.AddParameter("@ENC_EMAIL", dto.EncEmail);
                BaseDados.AddParameter("@ENC_ALTERNATIVO", dto.EncTelAlternativo);
                BaseDados.AddParameter("@PARENTESCO", dto.Parentesco);


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);

                if (dto.Classe > 0)
                {
                    BaseDados.AddParameter("@CLASSE", dto.Classe);
                }
                else
                {
                    BaseDados.AddParameter("@CLASSE", DBNull.Value);
                }

                BaseDados.AddParameter("@SITUACAO", dto.Situacao);

                if (dto.Turma > 0)
                {
                    BaseDados.AddParameter("@TURMA", dto.Turma);
                }
                else
                {
                    BaseDados.AddParameter("@TURMA", DBNull.Value);
                }
                BaseDados.AddParameter("@STATUS_INI", dto.SituacaoInicial);
                BaseDados.AddParameter("@STATUS_TERM", dto.SituacaoFinal);
                if (int.Parse(dto.Filial)>0)
                    BaseDados.AddParameter("@FILIAL", dto.Filial);
                else
                    BaseDados.AddParameter("@FILIAL", DBNull.Value);


                BaseDados.AddParameter("@PROCESSO", dto.NroExterno == string.Empty ? "-1" : dto.NroExterno);
                BaseDados.AddParameter("@EMISSAO", dto.Emissao);
                BaseDados.AddParameter("@LOCAL_EMISSAO", dto.LocalEmissao);
                BaseDados.AddParameter("@SHORTNAME", dto.SocialName);
                BaseDados.AddParameter("@EXTERNO", dto.IsExterno);

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

        public void AdicionarFoto(AlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ADICIONAR_FOTO";

                BaseDados.AddParameter("@INSCRICAO", dto.Inscricao);
                
                if (dto.PathFoto != null && !dto.PathFoto.Equals(""))
                {
                    BaseDados.AddParameter("@PATH", dto.PathFoto);
                    BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);

                }
                else
                {
                    BaseDados.AddParameter("@PATH", DBNull.Value);
                    BaseDados.AddParameter("@EXTENSAO", DBNull.Value);
                }
                BaseDados.AddParameter("@PROCESSO", dto.NroExterno == string.Empty ? "-1" : dto.NroExterno);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);


                
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

        public void AlterarDadosGerais(AlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ALTERAR_DADOS";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@NOME_COMPLETO", dto.NomeCompleto.ToUpper());
                BaseDados.AddParameter("@DT_NASCIMENTO", dto.DataNascimento);

                BaseDados.AddParameter("@NACIONALIDADE", dto.Nacionalidade);

                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", dto.MunicipioNascimento); // MUNICIPIO DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", DBNull.Value);

                }

                BaseDados.AddParameter("@NOME", dto.NomeCompleto.ToUpper());
                BaseDados.AddParameter("@SOBRENOME", dto.SobreNome);
                BaseDados.AddParameter("@SEXO", dto.Sexo);
                if (dto.EstadoCivil.Equals("-1"))
                {
                    dto.EstadoCivil = string.Empty;
                }
                BaseDados.AddParameter("@CIVIL", dto.EstadoCivil);

                if (dto.PathFoto != null && !dto.PathFoto.Equals(""))
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", SqlDbType.VarBinary);
                    BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);
                    BaseDados.AddParameter("@NOME_FOTO", dto.PathFoto);
                }
                else
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", SqlDbType.VarBinary);
                    BaseDados.AddParameter("@EXTENSAO", DBNull.Value);
                    BaseDados.AddParameter("@NOME_FOTO", DBNull.Value);
                }

                BaseDados.AddParameter("@PAIS", dto.PaisNascimento);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("@MATRICULA", dto.Inscricao);
                BaseDados.AddParameter("@INSCRICAO", dto.DataInscricao);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento); //PROVINCIA DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@NATURALIDADE", DBNull.Value);

                }
                BaseDados.AddParameter("@PROCESSO", dto.NroExterno == string.Empty ? "-1" : dto.NroExterno);
                BaseDados.AddParameter("@EMISSAO", dto.Emissao);
                BaseDados.AddParameter("@LOCAL_EMISSAO", dto.LocalEmissao);
                BaseDados.AddParameter("@SHORTNAME", dto.SocialName);
                BaseDados.AddParameter("@EXTERNO", dto.IsExterno);

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

        public List<AlunoDTO> ObterDesativados(AlunoDTO dto)
        {
            List<AlunoDTO> alunos = new List<AlunoDTO>();

            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTER_DESACTIVADOS";

                BaseDados.AddParameter("@NOME", dto.NomeCompleto);
                

                alunos = new List<AlunoDTO>();
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AlunoDTO();
                    if (int.Parse(dr["ALU_CODIGO"].ToString()) > 0)
                    {
                        dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString());
                        dto.DataInscricao = DateTime.Parse(dr["ALU_DATA_INSCRICAO"].ToString());
                        dto.DataNascimento = DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString());
                        dto.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                        dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                        dto.Telefone = dr["ENT_TELEFONE"].ToString();
                        //dto.NomeCompleto = Encoding.UTF8.GetString(Convert.FromBase64String(dto.NomeCompleto));
                        alunos.Add(dto); 
                    }
                }
            }
            catch (Exception ex)
            {
                alunos = new List<AlunoDTO>();
                dto = new AlunoDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                dto.NomeCompleto = dto.MensagemErro;
                alunos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
                 
            }

            return alunos;
        }

        public void AlterarFiliacao(AlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ALTERAR_FILIACAO";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ENCARREGADO", dto.Encarregado);
                BaseDados.AddParameter("@TEL_ENCARREGADO", dto.TelEncarregado);
                BaseDados.AddParameter("@ENC_EMAIL", dto.EncEmail);
                BaseDados.AddParameter("@ENC_ALTERNATIVO", dto.EncTelAlternativo);
                BaseDados.AddParameter("@PARENTESCO", dto.Parentesco);
                BaseDados.AddParameter("@PAI", dto.NomePai);
                BaseDados.AddParameter("@MAE", dto.NomeMae);
                BaseDados.AddParameter("@ENC_DOC", dto.TituloDocumento);

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

        public AlunoDTO AlterarContactos(AlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ALTERAR_CONTACTO";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", dto.MunicipioMorada); // MUNICIPIO DE MORADA

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", DBNull.Value);

                }
                BaseDados.AddParameter("@BAIRRO", dto.Bairro);
                BaseDados.AddParameter("@RUA", dto.Rua);
                BaseDados.AddParameter("@TELEFONE", dto.Telefone);
                BaseDados.AddParameter("@TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("@FAX", dto.TelefoneFax);

                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@WEBSITE", dto.WebSite);
                BaseDados.AddParameter("@DISTRITO", dto.Morada);
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

        public AlunoDTO AlterarDadosAcademicos(AlunoDTO dto)
        {
            //stp_ACA_ALUNO_UPDATE_ID
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ALTERAR_INSCRICAO";


                BaseDados.AddParameter("@MATRICULA", dto.Matricula);

                if (dto.Classe > 0)
                {
                    BaseDados.AddParameter("@CLASSE", dto.Classe);
                }
                else
                {
                    BaseDados.AddParameter("@CLASSE", DBNull.Value);
                }

                if (dto.Turma > 0)
                {
                    BaseDados.AddParameter("@TURMA", dto.Turma);
                }
                else
                {
                    BaseDados.AddParameter("@TURMA", DBNull.Value);
                }
                if (int.Parse(dto.Filial)>0)
                    BaseDados.AddParameter("@FILIAL", dto.Filial);
                else
                    BaseDados.AddParameter("@FILIAL", DBNull.Value);
                    BaseDados.AddParameter("@TURNO", dto.Turno);
                    BaseDados.AddParameter("@LINGUA", dto.OpcaoLingua);
                    BaseDados.AddParameter("@INSTITUICAO", dto.EscolaProveniencia);
                    BaseDados.AddParameter("@SAIDA", dto.SaidaProveniencia);
                    BaseDados.AddParameter("@SITUACAO_PROVENIENCIA", dto.SituacaoProveniencia);
                    BaseDados.AddParameter("@CURSO_PROVENIENCIA", dto.CursoProveniencia);
                    BaseDados.AddParameter("@PROVENIENCIA_CLASSE", dto.ClasseProveniencia);

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

        public void Apagar(AlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_EXCLUIR";


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


        }

        public AlunoDTO ObterPorPK(AlunoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERPORPK";

                string operacao = dto.DescricaoConvenio;

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DADOS", dto.DescricaoConvenio);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AlunoDTO();

                if (dr.Read())
                { 
                    dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString());
                    dto.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.Sexo = dr["PES_SEXO"].ToString() ?? string.Empty;

                    if (operacao.Equals("E"))
                    {
                        dto.Encarregado = dr["ALU_ENCARREGADO"].ToString();
                        dto.EncEmail = dr["ALU_ENCARREGADO_EMAIL"].ToString();
                        dto.EncTelAlternativo = dr["ALU_ENCARREGADO_TEL_ALTERNATIVO"].ToString();
                        dto.TelEncarregado = dr["ALU_ENCARREGADO_TELEFONE"].ToString();
                        dto.Parentesco = dr["ALU_ENC_PARENTESCO"].ToString();
                        dto.NomePai = dr["ALU_NOME_PAI"].ToString();
                        dto.NomeMae = dr["ALU_NOME_MAE"].ToString();
                        dto.TituloDocumento = dr["ALU_ENCARREGADO_IDENTIFICACAO"].ToString();
                    }
                    else if (operacao.Equals("C"))
                    {
                        dto.Rua = dr["ENT_RUA"].ToString();
                        dto.Bairro = dr["ENT_BAIRRO"].ToString();
                        dto.Telefone = dr["ENT_TELEFONE"].ToString();
                        dto.TelefoneAlt = dr["ENT_TELEFONE_ALTERNATIVO"].ToString();
                        dto.Email = dr["ENT_EMAIL"].ToString();
                        dto.TelefoneFax = dr["ENT_TELEFONE_FAX"].ToString();
                        dto.WebSite = dr["ENT_WEBSITE"].ToString();
                        dto.Municipio = dr["MORADA"].ToString();
                        dto.Morada = dr["ENT_MORADA_DISTRITO"].ToString();

                    }
                    else if (operacao.Equals("A"))
                    {
                        dto.Matricula = int.Parse(dr["MAT_CODIGO"].ToString());
                        dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                        dto.TituloDocumento = dr["ANO_ANO_LECTIVO"].ToString();
                        dto.Classe = int.Parse(dr["MAT_CODIGO_PLANO_CURRICULAR"].ToString());
                        dto.StatusMatricula = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                       
                        dto.SituacaoFinal = dr["MAT_SITUACAO_FINAL"].ToString();

                        if (dr["SITUACAO_FINAL"].ToString() != null && dr["SITUACAO_FINAL"].ToString() != "")
                        {
                            dto.Situacao = dr["SITUACAO_FINAL"].ToString();
                        }
                        else
                        {
                            dto.Situacao = dr["SITUACAO_INICIAL"].ToString();
                        }

                        if (dr["MAT_CODIGO_TURMA"].ToString() != null && dr["MAT_CODIGO_TURMA"].ToString() != "")
                        {
                            dto.Turma = int.Parse(dr["MAT_CODIGO_TURMA"].ToString());
                        }
                        dto.CursoID = dr["PLAN_CODIGO_RAMO"].ToString();

                        dto.Turno = dr["TUR_TURNO"].ToString();

                        dto.DescricaoEstado = dr["EST_DESCRICAO"].ToString();

                        dto.DataNascimento = DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString());                      

                        dto.Habilitacoes = dr["CUR_ESPECIFICACAO"].ToString();

                        if (dto.Habilitacoes.Equals("F") || dto.Habilitacoes.Equals("1") || dto.Habilitacoes.Equals("2"))
                        {
                            dto.Habilitacoes = "F";
                        }
                        else if (dto.Habilitacoes.Equals("M") || dto.Habilitacoes.Equals("MT"))
                        {
                            dto.Habilitacoes = "M";
                        }

                        if (dr["MAT_CODIGO_FILIAL"].ToString() != null && dr["MAT_CODIGO_FILIAL"].ToString() != "")
                        {
                            dto.Filial = dr["MAT_CODIGO_FILIAL"].ToString();
                        }

                        dto.Curso = dr["CURSO"].ToString();
                        dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                        dto.OpcaoLingua = dr["MAT_OPCAO"].ToString();
                        dto.EscolaProveniencia = dr["ALU_PROVENIENCIA_NOME"].ToString();
                        dto.CursoProveniencia = dr["ALU_PROVENIENCIA_CURSO"].ToString();
                        dto.ClasseProveniencia = dr["ALU_PROVENIENCIA_CLASSE"].ToString();
                        dto.SituacaoProveniencia = dr["ALU_PROVENIENCIA_SITUACAO"].ToString();
                        dto.SaidaProveniencia = dr["ALU_PROVENIENCIA_SAIDA"].ToString();

                    }
                    else if (operacao.Equals("G"))
                    {

                        dto.Status = int.Parse(dr["ALU_ESTADO"].ToString());
                        dto.DataInscricao = DateTime.Parse(dr["ALU_DATA_INSCRICAO"].ToString());
                        dto.DataNascimento = DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString());

                        dto.MunicipioNascimento= int.Parse(dr["ENT_LOCAL_NASCIMENTO"].ToString());
                        dto.PaisNascimento = int.Parse(dr["ENT_CODIGO_PAIS"].ToString());
                        dto.Nacionalidade = int.Parse(dr["ENT_NACIONALIDADE"].ToString());
                        dto.NroExterno = dr["ALU_NUMERO_MANUAL"].ToString() == "-1" ? "" : dr["ALU_NUMERO_MANUAL"].ToString();
                        dto.Nome = dr["PES_NOME"].ToString();
                        dto.SobreNome = dr["PES_SOBRENOME"].ToString();
                        dto.EstadoCivil = dr["PES_ESTADO_CIVIL"].ToString()==null ? "S" : dr["PES_ESTADO_CIVIL"].ToString();
                         
                        dto.LocalNascimento = int.Parse(dr["PES_NATURALIDADE"].ToString());
                        dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                        dto.Documento = int.Parse(dr["ENT_DOC_CODIGO_DOCUMENTO"].ToString());
                        dto.LocalEmissao = dr["ENT_DOC_LOCAL_EMISSAO"].ToString();
                        dto.Emissao = DateTime.Parse(dr["ENT_DOC_EMISSAO"].ToString() == null || dr["ENT_DOC_EMISSAO"].ToString() == "" ? DateTime.MinValue.ToString() : dr["ENT_DOC_EMISSAO"].ToString());

                    }
                    else if (operacao.Equals("I"))
                    {                         
                        dto.DataInscricao = DateTime.Parse(dr["ALU_DATA_INSCRICAO"].ToString()); 
                        dto.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                        dto.Curso = dr["CURSO"].ToString();
                        dto.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                        dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                        dto.Situacao = dr["ALU_CLASSE"].ToString();

                    }
                    else
                    {
                        dto.Encarregado = dr["ALU_ENCARREGADO"].ToString();
                        dto.DataInscricao = Convert.ToDateTime(dr["ALU_DATA_INSCRICAO"].ToString());
                        dto.EncEmail = dr["ALU_ENCARREGADO_EMAIL"].ToString();
                        dto.EncTelAlternativo = dr["ALU_ENCARREGADO_TEL_ALTERNATIVO"].ToString();
                        dto.TelEncarregado = dr["ALU_ENCARREGADO_TELEFONE"].ToString();
                        dto.Parentesco = dr["ALU_ENC_PARENTESCO"].ToString();
                        dto.NomePai = dr["ALU_NOME_PAI"].ToString();
                        dto.NomeMae = dr["ALU_NOME_MAE"].ToString();
                        dto.TituloDocumento = dr["ALU_ENCARREGADO_IDENTIFICACAO"].ToString();

                        dto.Rua = dr["ENT_RUA"].ToString();
                        dto.Bairro = dr["ENT_BAIRRO"].ToString();
                        dto.Telefone = dr["ENT_TELEFONE"].ToString();
                        dto.TelefoneAlt = dr["ENT_TELEFONE_ALTERNATIVO"].ToString();
                        dto.Email = dr["ENT_EMAIL"].ToString();
                        dto.TelefoneFax = dr["ENT_TELEFONE_FAX"].ToString();
                        dto.WebSite = dr["ENT_WEBSITE"].ToString();
                        dto.Municipio = dr["MORADA"].ToString();

                        dto.Matricula = int.Parse(dr["MAT_CODIGO"].ToString());
                        dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                        dto.AnoCivil = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                        dto.Classe = int.Parse(dr["MAT_CODIGO_PLANO_CURRICULAR"].ToString());
                        dto.StatusMatricula = int.Parse(dr["MAT_CODIGO_STATUS"].ToString());
                        dto.SituacaoFinal = dr["MAT_STATUS_INICIAL"].ToString();
                        dto.SituacaoInicial = dr["MAT_STATUS_FINAL"].ToString();
                        if (dr["MAT_TURMA"].ToString() != null && dr["MAT_TURMA"].ToString() != "")
                        {
                            dto.Turma = int.Parse(dr["MAT_TURMA"].ToString());
                        }

                        dto.Status = int.Parse(dr["ALU_ESTADO"].ToString());
                        dto.DataInscricao = DateTime.Parse(dr["ALU_DATA_INSCRICAO"].ToString());
                        dto.DataNascimento = DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString());

                        dto.MunicipioNascimento = int.Parse(dr["ENT_LOCAL_NASCIMENTO"].ToString());
                        dto.PaisNascimento = int.Parse(dr["ENT_CODIGO_PAIS"].ToString());
                        dto.Nacionalidade = int.Parse(dr["ENT_NACIONALIDADE"].ToString());

                        dto.Nome = dr["PES_NOME"].ToString();
                        dto.SobreNome = dr["PES_SOBRENOME"].ToString();
                        dto.EstadoCivil = dr["PES_ESTADO_CIVIL"].ToString();

                        dto.LocalNascimento = int.Parse(dr["PES_NATURALIDADE"].ToString());
                        dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                        dto.Documento = int.Parse(dr["ENT_DOC_CODIGO_DOCUMENTO"].ToString());
                        if (dr["MAT_CODIGO_FILIAL"].ToString() != null && dr["MAT_CODIGO_FILIAL"].ToString() != "")
                        {
                            dto.Filial = dr["MAT_CODIGO_FILIAL"].ToString();
                        }

                        dto.OpcaoLingua = dr["MAT_OPCAO"].ToString();
                        dto.EscolaProveniencia = dr["ALU_PROVENIENCIA_NOME"].ToString();
                        dto.CursoProveniencia = dr["ALU_PROVENIENCIA_CURSO"].ToString();
                        dto.ClasseProveniencia = dr["ALU_PROVENIENCIA_CLASSE"].ToString();
                        dto.SituacaoProveniencia = dr["ALU_PROVENIENCIA_SITUACAO"].ToString();
                        dto.SaidaProveniencia = dr["ALU_PROVENIENCIA_SAIDA"].ToString(); 
                        dto.LocalEmissao = dr["ENT_DOC_LOCAL_EMISSAO"].ToString();
                        dto.Emissao = DateTime.Parse(dr["ENT_DOC_EMISSAO"].ToString() == "" ? DateTime.MinValue.ToString() : dr["ENT_DOC_EMISSAO"].ToString());

                        dto.CursoID = dr["PLAN_CODIGO_RAMO"].ToString(); 
                        dto.Turno = dr["TUR_TURNO"].ToString(); 
                        dto.DescricaoEstado = dr["EST_DESCRICAO"].ToString(); 
                        dto.Habilitacoes = dr["CUR_ESPECIFICACAO"].ToString(); 
                        if (dto.Habilitacoes.Equals("F") || dto.Habilitacoes.Equals("1") || dto.Habilitacoes.Equals("2"))
                        {
                            dto.Habilitacoes = "F";
                        }
                        else if (dto.Habilitacoes.Equals("M") || dto.Habilitacoes.Equals("MT"))
                        {
                            dto.Habilitacoes = "M";
                        }
                    }

                    if (!string.IsNullOrEmpty(dr["PES_FOTOGRAFIA"].ToString()))
                    
                    dto.ExtensaoFoto = dr["PES_FOTO_EXTENSAO"].ToString();
                    if (dr["PES_FOTO_PATH"].ToString()!=null && dr["PES_FOTO_PATH"].ToString() != "")
                    {
                        dto.PathFoto = dr["PES_FOTO_PATH"].ToString();
                        dto.ExtensaoFoto = dr["PES_FOTO_EXTENSAO"].ToString();
                    }
                    else
                    {
                        if (dto.Sexo.Equals("F"))
                        {
                            dto.PathFoto = "~/imagens/128x128/logoAluna.png";
                            dto.ExtensaoFoto = ".png";
                        }
                        else
                        {
                            dto.PathFoto = "~/imagens/128x128/logoAluno.png";
                            dto.ExtensaoFoto = ".png";
                        }
                    }

                    dto.NroExterno = dr["ALU_NUMERO_MANUAL"].ToString() == "-1" ? dto.Inscricao : dr["ALU_NUMERO_MANUAL"].ToString();
                    dto.AcessoPortal = dr["ALU_PORTAL_ACESSO_STATUS"].ToString() == "1" ? true : false;
                   
                }
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

        public List<AlunoDTO> ObterPorFiltro(AlunoDTO dto)
        {

            List<AlunoDTO> alunos;
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERPORFILTRO";

                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@MATRICULA", dto.Inscricao);
                if (dto.NascimentoIni != null && dto.NascimentoIni != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@DT_NASCIMENTO_INI", dto.NascimentoIni);
                }
                else
                {
                    BaseDados.AddParameter("@DT_NASCIMENTO_INI", DBNull.Value);
                }
                if (dto.NascimentoTerm != null && dto.NascimentoTerm != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@DT_NASCIMENTO_TERM", dto.NascimentoTerm);
                }
                else
                {
                    BaseDados.AddParameter("@DT_NASCIMENTO_TERM", DBNull.Value);
                }
                BaseDados.AddParameter("@SEXO", dto.Sexo);

                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);
                
                if (dto.InscricaoIni != null && dto.InscricaoIni != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@DT_INSCRICAO_INI", dto.InscricaoIni);
                }
                else
                {
                    BaseDados.AddParameter("@DT_INSCRICAO_INI", DBNull.Value);
                }

                if (dto.InscricaoTerm != null && dto.InscricaoTerm != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@DT_INSCRICAO_TERM", dto.InscricaoTerm);
                }
                else
                {
                    BaseDados.AddParameter("@DT_INSCRICAO_TERM", DBNull.Value);
                }
                BaseDados.AddParameter("@ESTADO", dto.Status);                
                BaseDados.AddParameter("@ENCARREGADO", dto.Encarregado);
                BaseDados.AddParameter("@RESPONSAVEL", dto.Responsavel);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@DOC_ENCARREGADO", dto.TituloDocumento);
               
                alunos = new List<AlunoDTO>();
                 MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AlunoDTO();
                    if (!alunos.Exists(t => t.Codigo == int.Parse(dr["ALU_CODIGO"].ToString())))
                    {
                        dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString());
                        dto.DataInscricao = DateTime.Parse(dr["ALU_DATA_INSCRICAO"].ToString());
                        dto.DataNascimento = dr["ENT_DATA_NASCIMENTO"].ToString()!=null ? DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString()) : DateTime.Today;
                        dto.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString();
                        dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                        dto.Sexo = dr["PES_SEXO"].ToString();
                        dto.TituloDocumento = dr["DOC_DESCRICAO"].ToString();

                        if (dr["SITUACAO_FINAL"].ToString() != null && dr["SITUACAO_FINAL"].ToString() != "")
                        {
                            dto.Situacao = dr["SITUACAO_FINAL"].ToString() + " - " + dr["EST_DESCRICAO"].ToString();
                        }
                        else
                        {
                            dto.Situacao = " TURMA/SALA: " + dr["TURMA"].ToString() +" - "+ dr["SITUACAO_INICIAL"].ToString() + " - " + dr["EST_DESCRICAO"].ToString();
                        }

                        //dto.Fotografia = (byte[])dr["PES_FOTO_FOTOGRAFIA"].ToString();
                        dto.ExtensaoFoto = dr["PES_FOTO_EXTENSAO"].ToString();
                        if (dr["PES_FOTO_PATH"].ToString()!=null && dr["PES_FOTO_PATH"].ToString() != "")
                        {

                            dto.PathFoto = dr["PES_FOTO_PATH"].ToString();
                            dto.ExtensaoFoto = dr["PES_FOTO_EXTENSAO"].ToString();
                        }
                        else
                        {
                            if (dto.Sexo.Equals("F"))
                            {
                                dto.PathFoto = "~/imagens/128x128/logoAluna.png";
                                dto.ExtensaoFoto = ".png";
                            }
                            else
                            {
                                dto.PathFoto = "~/imagens/128x128/logoAluno.png";
                                dto.ExtensaoFoto = ".png";
                            }
                        }

                        dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();

                        dto.Curso = dr["CURSO"].ToString();

                        if (dto.Sexo.Equals("F"))
                        {
                            if (dr["ALU_ESTADO"].ToString() != null && dr["ALU_ESTADO"].ToString()!="1")
                            {
                                dto.DescricaoEstado = "BLOQUEADA";
                            }
                            else
                            {
                                dto.DescricaoEstado = "ACTIVA";
                            }
                        }
                        else
                        {
                            if (dr["ALU_ESTADO"].ToString() != null && dr["ALU_ESTADO"].ToString() != "1")
                            {
                                dto.DescricaoEstado = "BLOQUEADO";
                            }
                            else
                            {
                                dto.DescricaoEstado = "ACTIVO";
                            }
                        }


                        dto.Matricula = int.Parse(dr["MAT_CODIGO"].ToString());

                        dto.Documento = int.Parse(dr["ENT_DOC_CODIGO_DOCUMENTO"].ToString());

                        dto.SituacaoAcademica = dr["EST_DESCRICAO"].ToString();
                        dto.Turma = int.Parse(dr["TURMA_ALUNO"].ToString());
                        dto.NroExterno = dr["ALU_NUMERO_MANUAL"].ToString() == "-1" ? "" : dr["ALU_NUMERO_MANUAL"].ToString();
                        dto.Filial = dr["MAT_CODIGO_FILIAL"].ToString();
                        dto.Encarregado = dr["ALU_ENCARREGADO"].ToString();
                        dto.NomePai = dr["ALU_NOME_PAI"].ToString();
                        dto.NomeMae = dr["ALU_NOME_MAE"].ToString();
                        dto.CursoID = dr["CUR_CODIGO"].ToString();
                        dto.Classe = int.Parse(dr["PLAN_CODIGO"].ToString());
                        dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                        dto.Utilizador = dr["ALU_CADASTRO"].ToString();
                        dto.Responsavel = dr["ALU_ENCARREGADO_IDENTIFICACAO"].ToString();

                        dto.Telefone = dr["ENT_TELEFONE"].ToString();
                        if(dr["ALU_ENCARREGADO_TELEFONE"].ToString()!=null && dr["ALU_ENCARREGADO_TELEFONE"].ToString() != "")
                        {
                            if(dto.TelEncarregado!=dto.Telefone && !string.IsNullOrEmpty(dto.TelEncarregado))
                              dto.Telefone += "/" + dto.TelEncarregado;
                        }

                        dto.SiglaTurma = dr["TURMA"].ToString();
                        alunos.Add(dto);
                    }    
                }
            }
            catch (Exception ex)
            {
                alunos = new List<AlunoDTO>();
                dto = new AlunoDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                dto.NomeCompleto = dto.MensagemErro;
                alunos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return alunos;

        }
        

        public Boolean JaExiste(AlunoDTO dto)
        {
            
            bool conf = false;

            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERPORBI";

                


                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@BI", dto.Identificacao.Trim());
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                MySqlDataReader dr = BaseDados.ExecuteReader();


                while (dr.Read())
                {
                    if (int.Parse(dr["ALU_IDENTIFICACAO"].ToString()).Equals(dto.Identificacao.Trim()))
                    {
                        conf = true;
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

            return conf;

        }

        public Boolean IsProcessoExistente(AlunoDTO dto)
        {
            

            BaseDados.ComandText = "stp_ACA_ALUNO_OBTERNUMEROPROCESSO";

            bool conf = false;


            BaseDados.AddParameter("@INSCRICAO", dto.NroExterno);
            BaseDados.AddParameter("@FILIAL", dto.Filial);
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();


                while (dr.Read())
                {
                    if (int.Parse(dr["ALU_NUMERO_MANUAL"].ToString()).Equals(dto.NroExterno.Trim()))
                    {
                        conf = true;
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

            return conf;

        }


        public List<RelatorioAlunoDTO> ObterMatriculados(RelatorioAlunoDTO dto)
        {

            
            List<RelatorioAlunoDTO> coleccao;
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_LISTAGEM";



                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                /*AND MAT_DATA_PAGAMENTO IS NOT NULL*/
                BaseDados.AddParameter("@ESTADO", "AND MAT_CODIGO_STATUS=4");
                BaseDados.AddParameter("@FILIAL", dto.Codigo);
                BaseDados.AddParameter("@ANO_CURRICULAR", -1);
                BaseDados.AddParameter("@GERAL", dto.Sucesso == true ? 1 : 0);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<RelatorioAlunoDTO>();
                while (dr.Read())
                {
                    
                    dto = new RelatorioAlunoDTO();
                    dto.Inscricao = dr["INSCRICAO"].ToString();
                    dto.Nome = dr["NOME"].ToString().ToUpper();
                    dto.Data = dr["MAT_DATA"].ToString();
                    dto.Turma = dr["TUR_ABREVIATURA"].ToString();
                    dto.Classe = dr["CLASSE"].ToString();
                    dto.Curso = dr["CUR_NOME"].ToString();
                    dto.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dto.Turno = dr["TURNO"].ToString();
                    
                    dto.Sala = dr["TUR_SALA"].ToString();
                    if (dr["SITUACAO_FINAL"].ToString() != null && dr["SITUACAO_FINAL"].ToString() !="")
                    {
                        dto.Situacao = dr["SITUACAO_FINAL"].ToString() + " - " + dr["EST_DESCRICAO"].ToString();
                    }
                    else
                    {
                        dto.Situacao = dr["SITUACAO_INICIAL"].ToString() + " - " + dr["EST_DESCRICAO"].ToString();
                    }
                    dto.CodigoTurma = int.Parse(dr["TUR_ALU_CODIGO_TURMA"].ToString());
                    dto.Codigo = int.Parse(dr["ANO_CODIGO"].ToString());
                    dto.Ramo = dr["PLAN_CODIGO"].ToString();
                    dto.Matricula = dr["MAT_CODIGO"].ToString();
                    dto.Aluno = dr["MAT_CODIGO_ALUNO"].ToString();
                    dto.CompanyID = dr["ENT_DATA_NASCIMENTO"].ToString();
                    dto.Processo = dr["ALU_NUMERO_MANUAL"].ToString() == "-1" ? "" : dr["ALU_NUMERO_MANUAL"].ToString(); 
                    dto.Sexo = dr["PES_SEXO"].ToString();

                    dto.Inscricao = dto.Processo == "" ? dto.Inscricao : dto.Processo;
                    

                    coleccao.Add(dto);

                }

            }
            catch (Exception ex)
            {
                coleccao = new List<RelatorioAlunoDTO>(); 
                dto.Sucesso = false;
                dto.MensagemErro = dto.Inscricao+" "+dto.Nome+" - "+ex.Message.Replace("'", "");
                dto = new RelatorioAlunoDTO();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return coleccao;
        }

        public List<RelatorioAlunoDTO> Listagens(RelatorioAlunoDTO dto)
        {
            
            List<RelatorioAlunoDTO> coleccao;
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_LISTAGEM";
                
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@ESTADO", dto.FiltroStatus == null ? "" : dto.FiltroStatus);
                BaseDados.AddParameter("@FILIAL", dto.Codigo);
                BaseDados.AddParameter("@ANO_CURRICULAR", dto.AnoCurricular);
                BaseDados.AddParameter("@GERAL", dto.Sucesso == true ? 1 : 0);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<RelatorioAlunoDTO>();
                int ordem = 0;
                while (dr.Read())
                {
                    dto = new RelatorioAlunoDTO();
                    ordem++;
                    dto.Codigo = ordem;
                    dto.Inscricao = dr["INSCRICAO"].ToString();
                    dto.Nome = dr["NOME"].ToString().ToUpper();
                    dto.Data = dr["MAT_DATA"].ToString();
                    dto.Turma = dr["TUR_ABREVIATURA"].ToString();
                    dto.Classe = dr["CLASSE"].ToString();
                    dto.Curso = dr["CUR_NOME"].ToString();
                    dto.AnoLectivo = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dto.Turno = dr["TURNO"].ToString();

                    dto.Sala = dr["TUR_SALA"].ToString();
                    /*if (dr["SITUACAO_FINAL"].ToString() != null && dr["SITUACAO_FINAL"].ToString() != "")
                    {
                        dto.Situacao = dr["SITUACAO_FINAL"].ToString() + " - " + dr["EST_DESCRICAO"].ToString();
                    }
                    else*/
                    {
                        dto.Situacao = dr["SITUACAO_INICIAL"].ToString() + " - " + dr["EST_DESCRICAO"].ToString();
                    }
                    dto.CodigoTurma = int.Parse(dr["TUR_ALU_CODIGO_TURMA"].ToString()); 
                    dto.Ramo = dr["PLAN_CODIGO"].ToString();
                    dto.Matricula = dr["MAT_CODIGO"].ToString();
                    dto.Aluno = dr["MAT_CODIGO_ALUNO"].ToString();
                    dto.CompanyID = dr["ENT_DATA_NASCIMENTO"].ToString();
                    dto.Processo = dr["ALU_NUMERO_MANUAL"].ToString() == "-1" ? "" : dr["ALU_NUMERO_MANUAL"].ToString();
                    dto.Sexo = dr["PES_SEXO"].ToString();

                    dto.Inscricao = dto.Processo == "" ? dto.Inscricao : dto.Processo;
                    dto.Aluno = dr["ALU_CODIGO"].ToString();
                    dto.TotalGeral = dr["TUR_LOTACAO"].ToString();
                    coleccao.Add(dto);

                }

            }
            catch (Exception ex)
            {
                coleccao = new List<RelatorioAlunoDTO>();
                dto = new RelatorioAlunoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return coleccao;
        }

       

        public List<RelatorioAlunoDTO> ListaInadimplencia(RelatorioAlunoDTO dto)
        {

            
            List<RelatorioAlunoDTO> coleccao;
            try
            {
                BaseDados.ComandText = "stp_FIN_LISTA_INADIMPLENCIA"; 
               
                BaseDados.AddParameter("@CURSO", dto.Curso?? "-1");
                BaseDados.AddParameter("@CLASSE", dto.Classe ?? "-1");
                BaseDados.AddParameter("@TURMA", dto.Turma ?? "-1");
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@PERIODO", dto.Periodo ?? string.Empty);
                BaseDados.AddParameter("@RELATORIO", dto.Relatorio ?? string.Empty);
                BaseDados.AddParameter("@FILIAL", dto.CompanyID);
                BaseDados.AddParameter("@ITEM", dto.ItemID);
                BaseDados.AddParameter("@CATEGORIA", dto.ItemCategory);
                BaseDados.AddParameter("@MONTHLY_TYPE", dto.MontlyCategoryItem ?? string.Empty);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<RelatorioAlunoDTO>();
                while (dr.Read())
                {
                    dto = new RelatorioAlunoDTO
                    {
                        Codigo = int.Parse(dr["ALU_CODIGO"].ToString()),
                        Inscricao = dr["ALU_INSCRICAO"].ToString(),
                        Nome = dr["ENT_NOME_COMPLETO"].ToString().ToUpper().Trim(), 
                        Classe = dr["PLAN_DESCRICAO"].ToString(),
                        Curso = dr["CUR_NOME"].ToString(),
                        Turma = dr["TUR_ABREVIATURA"].ToString(),
                        ValorGlobal = decimal.Parse(dr["DIVIDA"].ToString()?? "0"),
                        Multa = decimal.Parse(dr["MULTAS"].ToString())  

                    };
                    coleccao.Add(dto);

                }

            }
            catch (Exception ex)
            {
                coleccao = new List<RelatorioAlunoDTO>();
                dto = new RelatorioAlunoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return coleccao;
        }

        public List<RelatorioAlunoDTO> GetInadimplenciaGeral(RelatorioAlunoDTO dto)
        {


            List<RelatorioAlunoDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_FIN_INADIMPLENCIA_GERAL";

                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo); 
                BaseDados.AddParameter("@FILIAL", dto.CompanyID);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<RelatorioAlunoDTO>();
                while (dr.Read())
                {
                    dto = new RelatorioAlunoDTO();
                    dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString());
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.Nome = dr["ENT_NOME_COMPLETO"].ToString().ToUpper().Trim();

                    dto.Classe = dr["PLAN_DESCRICAO"].ToString();
                    dto.Curso = dr["CUR_NOME"].ToString();
                    dto.Turma = dr["TUR_ABREVIATURA"].ToString();
                    dto.ValorGlobal = decimal.Parse(dr["MENS_ALU_VALOR"].ToString());
                    dto.Multa = decimal.Parse(dr["MULTA"].ToString());
                    dto.ItemDesignation = dr["ITEM_DESCRICAO"].ToString();
                    dto.Periodo = dr["MENS_PAR_MES"].ToString();
                    dto.AnoCivil = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                lista = new List<RelatorioAlunoDTO>();
                dto = new RelatorioAlunoDTO();
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

        public List<RelatorioAlunoDTO> CartaCobranca(RelatorioAlunoDTO dto)
        {

            
            List<RelatorioAlunoDTO> coleccao;
            try
            {
                BaseDados.ComandText = "stp_FIN_CARTA_COBRANCA";

                BaseDados.AddParameter("@MATRICULA", dto.Codigo);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@FILIAL", dto.Status);
                

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<RelatorioAlunoDTO>();
                while (dr.Read())
                {
                    dto = new RelatorioAlunoDTO();

                    dto.Ramo = dr["ALU_ENCARREGADO"].ToString();
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.Data = dr["PERIODO"].ToString();
                    dto.Nome = dr["ENT_NOME_COMPLETO"].ToString().ToUpper();
                    dto.Periodo = dr["COMPETENCIA"].ToString();
                    dto.Mensalidade = decimal.Parse(dr["ITEM_PRE_PRECO"].ToString());
                    dto.Multa = decimal.Parse(dr["MULTA"].ToString());
                    dto.Curso = dr["CURSO"].ToString();
                    dto.Turma = dr["TURMA"].ToString();
                    dto.Situacao = dr["SITUACAO"].ToString();
                    
                    coleccao.Add(dto);

                }

            }
            catch (Exception ex)
            {
                coleccao = new List<RelatorioAlunoDTO>();
                dto = new RelatorioAlunoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return coleccao.OrderBy(t=>t.Nome).ToList();
        }

        public bool FotoExiste(AlunoDTO dto) 
        {
            bool Existe = false;
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERFOTO";
                BaseDados.AddParameter("@FOTO", dto.PathFoto);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    Existe = true;
                    break;
                } 
            }
            catch (Exception) 
            {
                
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return Existe;
        }

        public AlunoDTO AdicionarPreInscricao(AlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_PRE_INSCRICAO_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@NOME", dto.NomeCompleto);
                BaseDados.AddParameter("@DATA_NASCIMENTO", dto.DataNascimento);
                if (dto.FormacaoProfissional > 0)
                {
                    BaseDados.AddParameter("@CURSO", dto.FormacaoProfissional);
                }
                else 
                {
                    BaseDados.AddParameter("@CURSO", DBNull.Value);
                }

                if (dto.Profissao > 0)
                {
                    BaseDados.AddParameter("@OPCAO", dto.Profissao);
                }
                else
                {
                    BaseDados.AddParameter("@OPCAO", DBNull.Value);
                }
                BaseDados.AddParameter("@OUTRO", dto.Curso.ToUpper());
                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@TELEFONE", dto.Telefone);
                BaseDados.AddParameter("@TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("@SUGESTOES", dto.DescricaoEstado);
                BaseDados.AddParameter("@TURNO", dto.Turno);
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

         
         
       
        public void AdicionarPessoaContacto(ContactoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_PESSOA_CONTACTO_ADICIONAR";


                BaseDados.AddParameter("@ALUNO", dto.Pessoa);
                BaseDados.AddParameter("@PESSOA", dto.NomePessoa);
                BaseDados.AddParameter("@CONTACTO", dto.Contacto);
                BaseDados.AddParameter("@GRAU", dto.Tipo);
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

        public void ExcluirPessoasContacto(ContactoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_PESSOA_CONTACTO_EXCLUIR";


                BaseDados.AddParameter("@ALUNO", dto.Pessoa);  

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

        public List<ContactoDTO> ListaPessoasContacto(ContactoDTO dto)
        {

            List<ContactoDTO> alunos;
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_PESSOA_CONTACTO_OBTER";

                BaseDados.AddParameter("@ALUNO", dto.Pessoa);

                alunos = new List<ContactoDTO>();
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ContactoDTO();

                    dto.Pessoa = int.Parse(dr["ALU_CONT_CODIGO_ALUNO"].ToString());
                    dto.NomePessoa = dr["ALU_CONT_NOME"].ToString();
                    dto.Contacto = dr["ALU_CONT_TELEFONE"].ToString();
                    dto.Tipo = dr["ALU_CONT_GRAU"].ToString(); 

                    alunos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                alunos = new List<ContactoDTO>();
                dto = new ContactoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return alunos;

        }

        public AlunoDTO ObterUltimaMatricula(AlunoDTO dto)
        {
            

            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_ULTIMA_MATRICULA";

                string operacao = dto.DescricaoConvenio;

                BaseDados.AddParameter("@ALUNO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AlunoDTO();

                while (dr.Read())
                {
                    dto.Matricula = int.Parse(dr["MATRICULA"].ToString());
                    dto.AnoLectivo = int.Parse(dr["ANO_LECTIVO"].ToString()); 
                }
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


        public AlunoDTO GetID(string pProcessoManual)
        {
            AlunoDTO dto = new AlunoDTO();
            
            try
            {

                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERPORPROCESSOMANUAL"; 

                BaseDados.AddParameter("@PROCESSO", pProcessoManual); 

                MySqlDataReader dr = BaseDados.ExecuteReader(); 

                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString() == "" ? "-1" : dr["ALU_CODIGO"].ToString());
                }
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

        public List<AlunoDTO> ObterDevedores(AlunoDTO dto)
        {

            List<AlunoDTO> alunos;
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERDEVEDORES";

                 
                BaseDados.AddParameter("@FILIAL", dto.Filial); 
                alunos = new List<AlunoDTO>();
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AlunoDTO();

                    dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString()); 
                    dto.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString(); 
                    

                    alunos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                alunos = new List<AlunoDTO>();
                dto = new AlunoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return alunos;

        }

        public List<AlunoDTO> ObterFicha(AlunoDTO dto)
        {

            List<AlunoDTO> alunos;
            
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_OBTERFICHA";

                BaseDados.AddParameter("@ALUNO_ID", dto.Codigo);
                 

                alunos = new List<AlunoDTO>();
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AlunoDTO();

                    dto.Codigo = int.Parse(dr["ALU_CODIGO"].ToString()); 
                    dto.Inscricao = dr["ALU_INSCRICAO"].ToString();
                    dto.NomeCompleto = dr["ENT_NOME_COMPLETO"].ToString(); 
                    dto.Sexo = dr["PES_SEXO"].ToString() == "F" ? "FEMININO" : "MASCULINO";
                    dto.DataNascimento = DateTime.Parse(dr["ENT_DATA_NASCIMENTO"].ToString());  
                    dto.Curso = dr["CUR_NOME"].ToString(); 
                    dto.Habilitacoes = dr["PLAN_DESCRICAO"].ToString();
                    dto.SiglaTurma = dr["TUR_ABREVIATURA"].ToString();
                    dto.Turno = dr["TURNO"].ToString();
                    if (dr["PES_FOTO_PATH"].ToString() != null && dr["PES_FOTO_PATH"].ToString() != "")
                    {
                        dto.PathFoto = dr["PES_FOTO_PATH"].ToString();
                    }
                    else
                    {
                        if (dr["PES_SEXO"].ToString().Equals("F"))
                        {
                            dto.PathFoto = "~/imagens/128x128/logoAluna.png";
                            dto.ExtensaoFoto = ".png";
                        }
                        else
                        {
                            dto.PathFoto = "~/imagens/128x128/logoAluno.png";
                            dto.ExtensaoFoto = ".png";
                        }
                    }
                    dto.AnoCivil = int.Parse(dr["ANO_ANO_LECTIVO"].ToString());
                    dto.AnoLectivo = int.Parse(dr["ANO_CODIGO"].ToString());
                    dto.NomePai = dr["ALU_NOME_PAI"].ToString();
                    dto.NomeMae = dr["ALU_NOME_MAE"].ToString(); 
                    dto.Encarregado = dr["ALU_ENCARREGADO"].ToString();
                    dto.Parentesco = dr["ALU_ENC_PARENTESCO"].ToString() == "-1" ? "NÃO INFORMADO" : dr["ALU_ENC_PARENTESCO"].ToString(); 
                    dto.TelEncarregado = dr["ALU_ENCARREGADO_TELEFONE"].ToString();
                    dto.EncEmail = dr["ALU_ENCARREGADO_EMAIL"].ToString();
                    dto.Responsavel = dr["ALU_ENCARREGADO_IDENTIFICACAO"].ToString();
                    dto.Morada = dr["ENT_MORADA_DISTRITO"].ToString(); 
                    dto.Telefone = dr["ENT_TELEFONE"].ToString(); 
                    dto.NomeLocalNascimento = dr["ENT_LOCAL_NASCIMENTO"].ToString();
                    dto.Naturalidade = dr["PES_NATURALIDADE"].ToString();
                    dto.Nome = dr["ENT_CODIGO_PAIS"].ToString() ?? "-1";
                    dto.SocialName = dr["ENT_NACIONALIDADE"].ToString(); 
                    dto.EstadoCivil = dr["PES_ESTADO_CIVIL"].ToString();
                    dto.DataInscricao = DateTime.Parse(dr["ENT_DATA_INCLUSAO"].ToString());
                    dto.TituloDocumento = dr["DOCUMENTO"].ToString();
                    dto.Identificacao = dr["ENT_IDENTIFICACAO"].ToString();
                    dto.Rua = dr["ENT_RUA"].ToString();
                    dto.Bairro = dr["ENT_BAIRRO"].ToString(); 
                    dto.NomeProvinciaMorada = dr["PROVINCIA_MORADA"].ToString(); 
                    dto.Email = dr["ENT_EMAIL"].ToString(); 
                    dto.NomeMunicipio = dr["MUNICIPIO_MORADA"].ToString();
                    dto.OpcaoLingua = dr["MAT_OPCAO"].ToString();
                    dto.EscolaProveniencia = dr["ALU_PROVENIENCIA_NOME"].ToString(); 
                    dto.SaidaProveniencia = dr["ALU_PROVENIENCIA_SAIDA"].ToString();
                    dto.SituacaoProveniencia = dr["ALU_PROVENIENCIA_SITUACAO"].ToString();
                    dto.CursoProveniencia = dr["ALU_PROVENIENCIA_CURSO"].ToString();
                    dto.ClasseProveniencia = dr["ALU_PROVENIENCIA_CLASSE"].ToString();

                    alunos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                alunos = new List<AlunoDTO>();
                dto = new AlunoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return alunos;

        }

        public AlunoDTO GetCrendencialForPortalAluno(AcessoDTO dto)
        {
             
            
            AlunoDTO studentSession = new AlunoDTO();
            try
            {
                BaseDados.ComandText = "stp_SIS_PORTAL_ALUNO_ACESSO";

                BaseDados.AddParameter("@STUDENT_ID", dto.SessionID);


                

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    if (dto.SessionID == int.Parse(dr["ALU_CODIGO"].ToString()) && dto.Senha == dr["ALU_PASSWORD"].ToString())
                    {
                        if(dr["ALU_PORTAL_ACESSO_STATUS"].ToString()!=null && int.Parse(dr["ALU_PORTAL_ACESSO_STATUS"].ToString()) == 1)
                        {
                            studentSession = new AlunoDTO
                            {
                                Codigo = int.Parse(dr["ALU_CODIGO"].ToString()),
                                SocialName = dr["ENT_NOME_COMPLETO"].ToString(),
                                Senha = dr["ALU_PASSWORD"].ToString(),
                                Sexo = dr["PES_SEXO"].ToString(),
                                PathFoto = dr["PES_FOTO_PATH"].ToString(),
                                AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                                Filial = dr["MAT_CODIGO_FILIAL"].ToString(),
                                NivelEnsino = dr["FIL_CATEGORIA"].ToString() == "3" ? "S" : "G",
                                EscolaProveniencia = dr["COMPANY_NAME"].ToString()
                            };
                            studentSession.Sucesso = true;
                            studentSession.MensagemErro = string.Empty;
                        }
                        else
                        {
                            studentSession.Sucesso = false;
                            studentSession.MensagemErro = "alert('Acesso Bloqueado ao Portal do Aluno(a), Por favor contacte de forma presencial a secretaria geral ou serviços pedagógicos para reposição do acesso.');";
                        }
                        
                    }
                    else if (dto.Senha != dr["ALU_PASSWORD"].ToString())
                    {
                        studentSession.MensagemErro = "alert('Senha Incorrecta');";
                        studentSession.Sucesso = false;
                    }
                    else
                    {
                        studentSession.Sucesso = false;
                        studentSession.MensagemErro = "alert(''Ops! Não tem permissão para aceder ao Portal');";
                    }
                    
                } 
                
                
            }
            catch (Exception ex)
            {
                studentSession.Sucesso = false;
                studentSession.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return studentSession; 
        }

        public AcessoDTO GetCredenicialPortalEncarregado(AcessoDTO dto)
        {

            
            try
            {
                BaseDados.ComandText = "stp_SIS_PORTAL_ENCARREGADO_ACESSO"; 

                BaseDados.AddParameter("@ENCARREGADO_ID", dto.Email);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AcessoDTO
                    { 
                        SocialName = dr["ALU_ENCARREGADO"].ToString(),
                        Senha = dr["ALU_ENCARREGADO_PORTAL_PASSWORD"].ToString()
                    };
                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto = new AcessoDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dto;

        }


        public void GrantStudentPortalAccess(AlunoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_SIS_PORTAL_ALUNO_ADICIONAR";
                BaseDados.AddParameter("@PASSWORD_ID", dto.Senha);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@ACESSO", dto.Status);

                if (dto.DescricaoConvenio == "E")
                {
                    _commandText = "stp_SIS_PORTAL_ENCARREGADO_ADICIONAR";
                    BaseDados.AddParameter("@EMAIL", dto.Email);
                }


                BaseDados.AddParameter("@CODIGO", dto.Codigo);




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
