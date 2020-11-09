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
    public class DocenteDAO
    {
        readonly ConexaoDB BaseDados;

        public DocenteDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public DocenteDTO Adicionar(DocenteDTO dto)
        {


            try
            {

                BaseDados.ComandText = "stp_ACA_DOCENTE_ADICIONAR";

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

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", dto.MunicipioNascimento);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);
                }

                if (dto.PaisNacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.PaisNacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }
                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);

               
                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);
                
                BaseDados.AddParameter("EMAIL", dto.Email); 

                BaseDados.AddParameter("INCLUSAO", DateTime.Now);

                BaseDados.AddParameter("NOME", dto.Nome);
                BaseDados.AddParameter("SOBRENOME", dto.SobreNome);
                BaseDados.AddParameter("SEXO", dto.Sexo);
                BaseDados.AddParameter("ESTADO_CIVIL", dto.EstadoCivil);
                BaseDados.AddParameter("TRATAMENTO", dto.Tratamento);
                
                if (dto.Documento > 0)
                {
                    BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                }
                else
                {
                    BaseDados.AddParameter("DOCUMENTO", DBNull.Value);
                }

                
                if (!dto.DataInclusao.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("ADMISSAO", dto.DataInclusao);
                }
                else
                {
                    BaseDados.AddParameter("ADMISSAO", DBNull.Value);
                }
                BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento);
                
                BaseDados.AddParameter("@ESTADO", 1);

                BaseDados.AddParameter("@PAIS", dto.PaisNascimento);

                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.AddParameter("@HABILITACOES", dto.Habilitacoes);

                BaseDados.AddParameter("@CATEGORIA", dto.NomeFormacao);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.Senha);
                BaseDados.AddParameter("NOME_PAI", dto.NomePai);
                BaseDados.AddParameter("NOME_MAE", dto.NomeMae);
                BaseDados.AddParameter("EMISSAO_DOCUMENTO", dto.Emissao == DateTime.MinValue ? (object)DBNull.Value : dto.Emissao);
                BaseDados.AddParameter("LOCAL_EMISSAO", dto.LocalEmissao);
                BaseDados.AddParameter("RUA", dto.MoradaRua);
                BaseDados.AddParameter("BAIRRO", dto.MoradaBairro);
                BaseDados.AddParameter("PROVINCIA", dto.ProvinciaMorada);
                BaseDados.AddParameter("MUNICIPIO", dto.MunicipioMorada <= 0 ? (object)DBNull.Value : dto.MunicipioMorada);

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

        public DocenteDTO Alterar(DocenteDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_ACA_DOCENTE_ALTERAR";


                BaseDados.AddParameter("CODIGO", dto.Codigo);

                BaseDados.AddParameter("NOME_COMPLETO", dto.NomeCompleto);

                if (dto.DataNascimento.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", dto.DataNascimento);
                }

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", dto.MunicipioNascimento);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);
                }

                if (dto.PaisNacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.PaisNacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }
                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);


                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);

                BaseDados.AddParameter("EMAIL", dto.Email);

                BaseDados.AddParameter("INCLUSAO", DateTime.Now);

                BaseDados.AddParameter("NOME", dto.Nome);
                BaseDados.AddParameter("SOBRENOME", dto.SobreNome);
                BaseDados.AddParameter("SEXO", dto.Sexo);
                BaseDados.AddParameter("ESTADO_CIVIL", dto.EstadoCivil);
                BaseDados.AddParameter("TRATAMENTO", dto.Tratamento);

                if (dto.Documento > 0)
                {
                    BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                }
                else
                {
                    BaseDados.AddParameter("DOCUMENTO", DBNull.Value);
                }


                if (!dto.DataInclusao.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("ADMISSAO", dto.DataInclusao);
                }
                else
                {
                    BaseDados.AddParameter("ADMISSAO", DBNull.Value);
                }
                BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento);

                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("ESTADO", 1);
                }
                else
                {
                    BaseDados.AddParameter("ESTADO", 0); 
                }

                BaseDados.AddParameter("@PAIS", dto.PaisNascimento);

                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.AddParameter("@HABILITACOES", dto.Habilitacoes);

                BaseDados.AddParameter("@CATEGORIA", dto.NomeFormacao);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SENHA", dto.Senha);
                BaseDados.AddParameter("NOME_PAI", dto.NomePai);
                BaseDados.AddParameter("NOME_MAE", dto.NomeMae);
                BaseDados.AddParameter("EMISSAO_DOCUMENTO", dto.Emissao == DateTime.MinValue ? (object)DBNull.Value : dto.Emissao);
                BaseDados.AddParameter("LOCAL_EMISSAO", dto.LocalEmissao);
                BaseDados.AddParameter("RUA", dto.MoradaRua);
                BaseDados.AddParameter("BAIRRO", dto.MoradaBairro);
                BaseDados.AddParameter("PROVINCIA", dto.ProvinciaMorada);
                BaseDados.AddParameter("MUNICIPIO", dto.MunicipioMorada <= 0 ? (object)DBNull.Value : dto.MunicipioMorada);

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

        public DocenteDTO Excluir(DocenteDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_EXCLUIR";

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

        public List<DocenteDTO> ObterPorFiltro(DocenteDTO dto) 
        {
            List<DocenteDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_OBTERPORFILTRO";

                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("@SEXO", dto.Sexo);
                BaseDados.AddParameter("@ESTADO", dto.Estado);
                BaseDados.AddParameter("@HABILITACOES", dto.Habilitacoes);
                BaseDados.AddParameter("@CATEGORIA", dto.NomeFormacao);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@STATUS_ID", dto.DescricaoEstado);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<DocenteDTO>();
                while (dr.Read())
                {
                    dto = new DocenteDTO();


                    dto.Codigo = int.Parse(dr[0]);
                    dto.NomeCompleto = dr[1].ToUpper();
                    dto.Sexo = dr[2];
                    dto.Nacionalidade = dr[3];
                    dto.Telefone = dr[4];
                    dto.Email = dr[5].ToLower();
                    dto.NomeHabilitacoes = dr[6];
                    dto.NomeFormacao = dr[7];

                    lista.Add(dto);
                }
                
            }
            catch (Exception ex)
            {
                dto = new DocenteDTO();
                dto.NomeCompleto = "Ocorreu um Erro: "+ex.Message.Replace("'", "");
                lista = new List<DocenteDTO>();
                lista.Add(dto);

            }
            finally 
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public DocenteDTO ObterPorPK(DocenteDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new DocenteDTO();
                while (dr.Read())
                {

                    dto.Codigo = int.Parse(dr[0]);

                    dto.Nome = dr[1].ToUpper();
                    dto.SobreNome = dr[2].ToUpper();
                    dto.DataNascimento = Convert.ToDateTime(dr[3]);
                    dto.Sexo = dr[4];
                    dto.EstadoCivil = dr[5];
                    dto.PaisNacionalidade = int.Parse(dr[6]);
                    dto.Documento = int.Parse(dr[7]);
                    dto.Identificacao = dr[8];
                    dto.PaisNascimento = int.Parse(dr[9]);
                    dto.LocalNascimento = int.Parse(dr[10]);

                    if (dr[11] != null && !dr[11].Equals(""))
                    {
                        dto.MunicipioNascimento = int.Parse(dr[11]);

                    }
                    else
                    {
                        dto.MunicipioNascimento = -1;
                    }

                    if (dr[12].Equals("1")) 
                    {
                        dto.Estado = true;
                    }

                    dto.Telefone = dr[13];
                    dto.TelefoneAlt = dr[14]; 
                    dto.Email = dr[15].ToLower();
                    dto.Habilitacoes = int.Parse(dr[16]);
                    dto.NomeFormacao = dr[17];
                    dto.Utilizador = dr[18];
                    dto.Senha = dr[19];
                    dto.MoradaRua = dr[20];
                    dto.MoradaBairro = dr[21];
                    dto.MunicipioMorada = int.Parse(dr[22]);
                    dto.ProvinciaMorada = int.Parse(dr[23]);
                    dto.NomePai = dr[24];
                    dto.NomeMae = dr[25];
                    dto.Emissao = dr[26] != null && dr[26] != "" ? DateTime.Parse(dr[26]) : DateTime.MinValue;
                    dto.LocalEmissao = dr[27];
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

        public List<CursoDTO> ObterMeusCursos(DocenteDTO dto)
        {
            List<CursoDTO> lista = new List<CursoDTO>();
             
            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_OBTERMEUSCURSOS";

                BaseDados.AddParameter("DOCENTE", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader(); 

                while (dr.Read())
                {
                    CursoDTO objCurso = new CursoDTO();

                    objCurso.Codigo = int.Parse(dr[0]);
                    objCurso.Nome = dr[1];

                    lista.Add(objCurso);
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

            lista.Insert(0, new CursoDTO(-1, "SELECCIONE"));

            return lista;
        }

        public List<AnoCurricularDTO> ObterMeusAnos(DocenteDTO dto)
        {
            List<AnoCurricularDTO> lista = new List<AnoCurricularDTO>();

            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_MEUSANOSCURRICULARES";

                BaseDados.AddParameter("DOCENTE", dto.Codigo);
                BaseDados.AddParameter("CURSO", dto.FormacaoProfissional);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    AnoCurricularDTO objAnoCurricular = new AnoCurricularDTO();

                    objAnoCurricular.Codigo = int.Parse(dr[0]);
                    objAnoCurricular.Descricao = dr[1];

                    lista.Add(objAnoCurricular);
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

            lista.Insert(0, new AnoCurricularDTO(-1, "SELECCIONE"));

            return lista;
        }

        public List<TurmaDTO> ObterMinhasTurmas(DocenteDTO dto)
        {
            List<TurmaDTO> lista = new List<TurmaDTO>();

            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_OBTERMINHASTURMAS";

                BaseDados.AddParameter("DOCENTE", dto.Codigo);
                BaseDados.AddParameter("ANO", dto.AnoLectivo);
                BaseDados.AddParameter("CLASSE", dto.AnoCurricular);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    TurmaDTO objTurma = new TurmaDTO();

                    objTurma.Codigo = int.Parse(dr[0]);
                    objTurma.Sigla = dr[1];

                    lista.Add(objTurma);
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

            lista.Insert(0, new TurmaDTO(-1, dto.AnoCurricular,"SELECCIONE", "SELECCIONE", -1, 1, "", "", "-1", -1));

            return lista;
        }

        public List<UnidadeCurricularDTO> ObterMinhaDisciplinas(DocenteDTO dto)
        {
            List<UnidadeCurricularDTO> lista = new List<UnidadeCurricularDTO>();

            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_OBTERMINHASDISCIPLINAS";

                BaseDados.AddParameter("DOCENTE", dto.Codigo);
                BaseDados.AddParameter("PERIODO", dto.AnoCurricular);
                BaseDados.AddParameter("TURMA", dto.Turma);
                

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    UnidadeCurricularDTO objUnidadeCurricular = new UnidadeCurricularDTO();

                    objUnidadeCurricular.Codigo = int.Parse(dr[0]);
                    objUnidadeCurricular.NomeDisciplina = dr[1];

                    lista.Add(objUnidadeCurricular);
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

            lista.Insert(0, new UnidadeCurricularDTO(-1, "SELECCIONE"));

            return lista;
        }



        public DocenteDTO ObterAcesso(DocenteDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_OBTERCREDENCIAIS";

                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new DocenteDTO();
                while (dr.Read())
                {

                    dto.Codigo = int.Parse(dr[0]); 
                    dto.Nome = dr[1].ToUpper();
                    dto.Utilizador = dr[2];
                    dto.Senha = dr[3];
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
    }
}
