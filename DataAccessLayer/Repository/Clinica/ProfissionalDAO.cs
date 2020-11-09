using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dominio.Geral;
using Dominio.Clinica;

namespace DataAccessLayer.Clinica
{
    public class ProfissionalDAO
    {
        static ConexaoDB BaseDados = new ConexaoDB();
     

        public ProfissionaDTO Adicionar(ProfissionaDTO dto) 
        {
             
            try
            {

                BaseDados.ComandText = "stp_CLI_PROFISSIONAL_ADICIONAR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                BaseDados.AddParameter("NOME_COMPLETO", dto.NomeCompleto.ToUpper());

                if (dto.DataNascimento.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", dto.DataNascimento);
                }

                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", dto.MunicipioNascimento);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);
                }

                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }


                
                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);

                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("LOCAL_MORADA", dto.MunicipioMorada);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_MORADA", DBNull.Value);
                }
                BaseDados.AddParameter("RUA", dto.Rua);
                BaseDados.AddParameter("BAIRRO", dto.Bairro);
                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("FAX", dto.TelefoneFax);
                BaseDados.AddParameter("EMAIL", dto.Email);

                BaseDados.AddParameter("SEXO", dto.Sexo);
                BaseDados.AddParameter("ESTADO_CIVIL", dto.EstadoCivil); 

                if (dto.Documento > 0)
                {
                    BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                }
                else
                {
                    BaseDados.AddParameter("DOCUMENTO", DBNull.Value);
                } 

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento);
                }
                else
                {
                     BaseDados.AddParameter("@NATURALIDADE", DBNull.Value);
                } 

                if (dto.PaisNascimento > 0)
                {
                    BaseDados.AddParameter("@PAIS", dto.PaisNascimento);
                }
                else
                {
                    BaseDados.AddParameter("@PAIS", DBNull.Value);
                }
                 

                BaseDados.AddParameter("@ESPECIALIDADE", dto.Especialidade);
                BaseDados.AddParameter("@NUM_ORDEM", dto.CedulaProfissional);

                if (dto.Fotografia != null)
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", dto.Fotografia);
                }
                else
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", MySqlDbType.VarBinary);
                }

                BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);

                BaseDados.AddParameter("@PATH", dto.PathFoto);

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                BaseDados.AddParameter("@SENHA", dto.CurrentPassword);

                BaseDados.AddParameter("@PROFISSAO", dto.AreaProfissional);

                BaseDados.AddParameter("@FILIAL", dto.Filial);

                dto.Codigo = BaseDados.ExecuteInsert();

                if (dto.Codigo == -2)
                {
                    dto.Sucesso = false;
                    dto.MensagemErro = "Já Existe um Médico Cadastro este Nº do Documento";

                } if (dto.Codigo == -3)
                {
                    dto.Sucesso = false;
                    dto.MensagemErro = "Já Existe um Médico Cadastro este Nº de Cédula da Ordem dos Médicos";

                }
                else 
                {
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

        public ProfissionaDTO Alterar(ProfissionaDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_CLI_PROFISSIONAL_ALTERAR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                BaseDados.AddParameter("NOME_COMPLETO", dto.NomeCompleto.ToUpper());

                if (dto.DataNascimento.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", dto.DataNascimento);
                }

                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", dto.MunicipioNascimento);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);
                }

                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }



                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);

                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("LOCAL_MORADA", dto.MunicipioMorada);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_MORADA", DBNull.Value);
                }
                BaseDados.AddParameter("RUA", dto.Rua);
                BaseDados.AddParameter("BAIRRO", dto.Bairro);
                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("FAX", dto.TelefoneFax);
                BaseDados.AddParameter("EMAIL", dto.Email);

                BaseDados.AddParameter("SEXO", dto.Sexo);
                BaseDados.AddParameter("ESTADO_CIVIL", dto.EstadoCivil);


                if (dto.Documento > 0)
                {
                    BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                }
                else
                {
                    BaseDados.AddParameter("DOCUMENTO", DBNull.Value);
                }



                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento);
                }
                else
                {
                    BaseDados.AddParameter("@NATURALIDADE", DBNull.Value);
                }



                if (dto.PaisNascimento > 0)
                {
                    BaseDados.AddParameter("@PAIS", dto.PaisNascimento);
                }
                else
                {
                    BaseDados.AddParameter("@PAIS", DBNull.Value);
                }


                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.AddParameter("@ESPECIALIDADE", dto.Especialidade);
                BaseDados.AddParameter("@NUM_ORDEM", dto.CedulaProfissional);

                if (dto.Fotografia != null)
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", dto.Fotografia);
                }
                else
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", MySqlDbType.VarBinary);
                }

                BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto); 
                BaseDados.AddParameter("@PATH", dto.PathFoto);  
                BaseDados.AddParameter("@PROFISSAO", dto.AreaProfissional);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador); 

                dto.Codigo = BaseDados.ExecuteInsert();

                if (dto.Codigo == -2)
                {
                    dto.Sucesso = false;
                    dto.MensagemErro = "Já Existe um Médico Cadastro este Nº do Documento";

                } if (dto.Codigo == -3)
                {
                    dto.Sucesso = false;
                    dto.MensagemErro = "Já Existe um Médico Cadastro este Nº de Cédula da Ordem dos Médicos";

                }
                else
                {
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

        public ProfissionaDTO Excluir(ProfissionaDTO dto)
        {
             
            try
            {
                BaseDados.ComandText = "stp_CLI_PROFISSIONAL_EXCLUIR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

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

        public List<ProfissionaDTO> ObterPorFiltro(ProfissionaDTO dto) 
        {
            List<ProfissionaDTO> es;

            try
            {
                BaseDados.ComandText = "stp_CLI_PROFISSIONAL_OBTERPORFILTRO";

                BaseDados.AddParameter("PROFISSIONAL_ID", dto.Codigo);
                BaseDados.AddParameter("NOME", dto.NomeCompleto); 
                BaseDados.AddParameter("CATEGORIA", dto.AreaProfissional);
                BaseDados.AddParameter("ESPECIALIDADE", dto.Especialidade); 

                MySqlDataReader dr = BaseDados.ExecuteReader();
                es = new List<ProfissionaDTO>();
                while (dr.Read())
                {
                    dto = new ProfissionaDTO();


                    dto.Codigo = int.Parse(dr[0].ToString());

                    dto.NomeCompleto = dr[1].ToString();
                    
                    if (dr[2].ToString() != null && dr[2].ToString().Equals("M"))
                    {
                        dto.NomeCompleto = "Dr. "+dto.NomeCompleto;
                    }
                    else if (dr[2].ToString() != null && dr[2].ToString().Equals("F"))
                    {
                        dto.NomeCompleto = "Dra. " + dto.NomeCompleto;
                    }


                    dto.NomeFormacao = dr[2].ToString();
                    dto.CedulaProfissional = dr[3].ToString();
                    dto.DesignacaoNacionalidade = dr[4].ToString();
                    dto.Telefone = dr[5].ToString();
                    dto.Registo = NumeroProcesso(dto.Codigo);
                    dto.AreaProfissional = dr[8].ToString();

                    es.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto = new ProfissionaDTO();
                dto.Sucesso = false;
               dto.MensagemErro = ex.Message.Replace("'", "");

                es = new List<ProfissionaDTO>();
                es.Add(dto);

            }
            finally {

                BaseDados.FecharConexao();
            }

            return es;
        }

        public ProfissionaDTO ObterPorPK(ProfissionaDTO dto) 
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_PROFISSIONAL_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new ProfissionaDTO();
                if (dr.Read()) 
                {

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.NomeCompleto = dr[1].ToString();
                    dto.DataNascimento = Convert.ToDateTime(dr[2].ToString());
                    dto.Sexo = dr[3].ToString();
                    dto.EstadoCivil = dr[4].ToString();
                    dto.Nacionalidade = int.Parse(dr[5].ToString());
                    dto.PaisNascimento = int.Parse(dr[6].ToString());
                    dto.LocalNascimento = int.Parse(dr[7].ToString());
                    dto.MunicipioNascimento = int.Parse(dr[8].ToString());
                    dto.Documento = int.Parse(dr[9].ToString());
                    dto.Identificacao = dr[10].ToString();
                    dto.CedulaProfissional =dr[11].ToString().ToUpper();
                    dto.NomeFormacao =  dr[12].ToString().ToUpper();

                    dto.Rua = dr[13].ToString();
                    dto.Bairro = dr[14].ToString();
                    dto.Provincia = dr[15].ToString();
                    dto.MunicipioMorada = int.Parse(dr[16].ToString().ToUpper());

                    dto.Telefone = dr[17].ToString();
                    dto.TelefoneAlt = dr[18].ToString();
                    dto.TelefoneFax = dr[19].ToString();
                    dto.Email = dr[20].ToString(); 
                    dto.PathFoto = dr[21].ToString();
                    dto.AreaProfissional = dr[22].ToString();
                    dto.Tratamento = dr[23].ToString();
                    dto.ProfissionalID = int.Parse(dr[24].ToString());
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

        public String NumeroProcesso(int codigo)
        {
            String matricula = "";

            if (codigo < 10)
            {
                matricula = "00000";
            }
            else if (codigo < 100)
            {
                matricula = "0000";
            }
            else if (codigo < 1000)
            {
                matricula = "000";
            }
            else if (codigo < 10000)
            {
                matricula = "00";
            }
            else if (codigo < 100000)
            {
                matricula = "0";
            }

            return matricula + codigo.ToString();

        } 


    }
}
