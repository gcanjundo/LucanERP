using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class AnoLectivoDAO
    {
        readonly ConexaoDB BaseDados;

        public AnoLectivoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public AnoLectivoDTO Inserir(AnoLectivoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_ADICIONAR";

                BaseDados.AddParameter("@DESCRICAO", dto.AnoDescricao);
                BaseDados.AddParameter("@ANO", dto.AnoAno.Trim().ToString());
                BaseDados.AddParameter("@INICIO", dto.AnoInicio);
                BaseDados.AddParameter("@TERMINO", dto.AnoTermino);

                if (dto.AnoInicioMatricula != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@INICIO_MAT", dto.AnoInicioMatricula);

                }
                else
                {
                    BaseDados.AddParameter("@INICIO_MAT", DBNull.Value);

                }

                if (dto.AnoTerminoMatricula != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@TERMINO_MAT", dto.AnoTerminoMatricula);
                }
                else
                {
                    BaseDados.AddParameter("@TERMINO_MAT", DBNull.Value);
                }
                BaseDados.AddParameter("@ESTADO", dto.AnoStatus);

                BaseDados.AddParameter("@NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("@INSCRICAO", dto.TaxaInscricao);
                BaseDados.AddParameter("@MULTA_MAT", dto.MultaMatricula);
                BaseDados.AddParameter("FILIAL", dto.Filial);

                dto.AnoCodigo = BaseDados.ExecuteInsert();

                if (dto.AnoCodigo == -1) 
                {
                    dto.MensagemErro = "Já está cadastrada o Ano Lectivo de "+dto.AnoAno;
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

        public AnoLectivoDTO Alterar(AnoLectivoDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_ALTERAR";

                BaseDados.AddParameter("@DESCRICAO", dto.AnoDescricao);
                BaseDados.AddParameter("@ANO", dto.AnoAno.Trim().ToString());
                BaseDados.AddParameter("@INICIO", dto.AnoInicio);
                BaseDados.AddParameter("@TERMINO", dto.AnoTermino);

                if (dto.AnoInicioMatricula != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@INICIO_MAT", dto.AnoInicioMatricula);

                }
                else
                {
                    BaseDados.AddParameter("@INICIO_MAT", DBNull.Value);

                }

                if (dto.AnoTerminoMatricula != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@TERMINO_MAT", dto.AnoTerminoMatricula);
                }
                else
                {
                    BaseDados.AddParameter("@TERMINO_MAT", DBNull.Value);
                }
                BaseDados.AddParameter("@ESTADO", dto.AnoStatus);
                BaseDados.AddParameter("@NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("@INSCRICAO", dto.TaxaInscricao);
                BaseDados.AddParameter("@MULTA_MAT", dto.MultaMatricula);
                BaseDados.AddParameter("@CODIGO", dto.AnoCodigo);
                BaseDados.AddParameter("FILIAL", dto.Filial);

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

        public void Apagar(AnoLectivoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_EXCLUIR";
               
                BaseDados.AddParameter("@CODIGO", dto.AnoCodigo);

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

        public AnoLectivoDTO ObterPorPK(AnoLectivoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_OBTERPORPK";

             
                BaseDados.AddParameter("@CODIGO", dto.AnoCodigo);
                if (dto != null && dto.AnoAno.Trim() != null)
                {
                    BaseDados.AddParameter("@ANO", dto.AnoAno.Trim());
                    
                }
                else 
                {
                    BaseDados.AddParameter("@ANO", String.Empty);
                }
                BaseDados.AddParameter("FILIAL", dto.Filial);
                 
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AnoLectivoDTO();
                while (dr.Read())
                {
                    dto.AnoCodigo = Int32.Parse(dr["ANO_CODIGO"].ToString());
                    dto.AnoDescricao = dr["ANO_DESCRICAO"].ToString();
                    dto.AnoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.AnoInicio = DateTime.Parse(dr["ANO_INICIO"].ToString());
                    dto.AnoTermino = DateTime.Parse(dr["ANO_TERMINO"].ToString());
                    if (dr["ANO_INICIO_MATRICULA"].ToString() != null && !dr["ANO_INICIO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoInicioMatricula = DateTime.Parse(dr["ANO_INICIO_MATRICULA"].ToString());
                    }

                    if (dr["ANO_TERMINO_MATRICULA"].ToString() != null && !dr["ANO_TERMINO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoTerminoMatricula = DateTime.Parse(dr["ANO_TERMINO_MATRICULA"].ToString());
                    }
                    dto.AnoStatus = dr["ANO_STATUS"].ToString();
                    dto.NivelEnsino = dr["ANO_NIVEL"].ToString();
                    dto.TaxaInscricao = dr["ANO_TAXA_INSCRICAO"].ToString();
                    dto.MultaMatricula = dr["ANO_MULTA_MATRICULA"].ToString();
                    dto.Filial = dr["ANO_CODIGO_FILIAL"].ToString();
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

        public AnoLectivoDTO ObterActual(AnoLectivoDTO dto)
        {
            var anosList = new List<AnoLectivoDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_OBTERACTUAL"; 

                BaseDados.AddParameter("NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("FILIAL", dto.Filial);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new AnoLectivoDTO();
                    dto.AnoCodigo = Int32.Parse(dr["ANO_CODIGO"].ToString());
                    dto.AnoDescricao = dr["ANO_DESCRICAO"].ToString();
                    dto.AnoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.AnoInicio = DateTime.Parse(dr["ANO_INICIO"].ToString());
                    dto.AnoTermino = DateTime.Parse(dr["ANO_TERMINO"].ToString());
                    if (dr["ANO_INICIO_MATRICULA"].ToString() != null && !dr["ANO_INICIO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoInicioMatricula = DateTime.Parse(dr["ANO_INICIO_MATRICULA"].ToString());
                    }

                    if (dr["ANO_TERMINO_MATRICULA"].ToString() != null && !dr["ANO_TERMINO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoTerminoMatricula = DateTime.Parse(dr["ANO_TERMINO_MATRICULA"].ToString());
                    }
                    dto.AnoStatus = dr["ANO_STATUS"].ToString();
                    dto.NivelEnsino = dr["ANO_NIVEL"].ToString();
                    dto.TaxaInscricao = dr["ANO_TAXA_INSCRICAO"].ToString();
                    dto.MultaMatricula = dr["ANO_MULTA_MATRICULA"].ToString();
                    dto.Filial = dr["ANO_CODIGO_FILIAL"].ToString();
                    dto.Sucesso = true;
                    if(dto.AnoInicio <= DateTime.Today)
                     anosList.Add(dto);

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

            return anosList.Count > 1 ? anosList.Where(t=>t.AnoInicio <= DateTime.Today).ToList().SingleOrDefault() : dto;
        }

        public AnoLectivoDTO ObterAnterior(AnoLectivoDTO dto)
        {
              
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_ANTERIOR";

                BaseDados.AddParameter("NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto.AnoCodigo = Int32.Parse(dr["ANO_CODIGO"].ToString());
                    dto.AnoDescricao = dr["ANO_DESCRICAO"].ToString();
                    dto.AnoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.AnoInicio = DateTime.Parse(dr["ANO_INICIO"].ToString());
                    dto.AnoTermino = DateTime.Parse(dr["ANO_TERMINO"].ToString());
                    if (dr["ANO_INICIO_MATRICULA"].ToString() != null && !dr["ANO_INICIO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoInicioMatricula = DateTime.Parse(dr["ANO_INICIO_MATRICULA"].ToString());
                    }

                    if (dr["ANO_TERMINO_MATRICULA"].ToString() != null && !dr["ANO_TERMINO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoTerminoMatricula = DateTime.Parse(dr["ANO_TERMINO_MATRICULA"].ToString());
                    }
                    dto.AnoStatus = dr["ANO_STATUS"].ToString();
                    dto.TaxaInscricao = dr["ANO_TAXA_INSCRICAO"].ToString();
                    dto.MultaMatricula = dr["ANO_MULTA_MATRICULA"].ToString();

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

        public List<AnoLectivoDTO> ObterPorFiltro(AnoLectivoDTO dto)
        {
            List<AnoLectivoDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_OBTERPORFILTRO";
                BaseDados.AddParameter("@DESCRICAO", dto.AnoDescricao);
                BaseDados.AddParameter("@ANO", dto.AnoAno.Trim());
                BaseDados.AddParameter("@NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("FILIAL", dto.Filial);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<AnoLectivoDTO>();
                while (dr.Read())
                {
                    dto = new AnoLectivoDTO();
                    dto.AnoCodigo = Int32.Parse(dr["ANO_CODIGO"].ToString());
                    dto.AnoDescricao = dr["ANO_DESCRICAO"].ToString();
                    dto.AnoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    dto.AnoInicio = DateTime.Parse(dr["ANO_INICIO"].ToString());
                    dto.AnoTermino = DateTime.Parse(dr["ANO_TERMINO"].ToString());
                    if (dr["ANO_INICIO_MATRICULA"].ToString() != null && !dr["ANO_INICIO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoInicioMatricula = DateTime.Parse(dr["ANO_INICIO_MATRICULA"].ToString());
                    }

                    if (dr["ANO_TERMINO_MATRICULA"].ToString() != null && !dr["ANO_TERMINO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoTerminoMatricula = DateTime.Parse(dr["ANO_TERMINO_MATRICULA"].ToString());
                    }
                    dto.AnoStatus = dr["ANO_STATUS"].ToString();
                    dto.NivelEnsino = dr["ANO_NIVEL"].ToString();
                    dto.TaxaInscricao = dr["ANO_TAXA_INSCRICAO"].ToString();
                    dto.MultaMatricula = dr["ANO_MULTA_MATRICULA"].ToString();
                    dto.Filial = dr["ANO_CODIGO_FILIAL"].ToString();

                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<AnoLectivoDTO>();
                dto = new AnoLectivoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public List<AnoLectivoDTO> ObterPorPagar(MatriculaDTO matricula)
        {
            List<AnoLectivoDTO> lista;
            AnoLectivoDTO dto;
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_OBTERTAXAEMDIVIDA";

                 
                BaseDados.AddParameter("@ANO", matricula.AnoLectivo);
                BaseDados.AddParameter("@ALUNO", matricula.Aluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                lista = new List<AnoLectivoDTO>();
                while (dr.Read())
                {
                    dto = new AnoLectivoDTO();

                    dto.AnoCodigo = Int32.Parse(dr["ANO_CODIGO"].ToString());

                    dto.AnoDescricao = dr["ANO_DESCRICAO"].ToString();
                    
                    dto.AnoAno = dr["ANO_ANO_LECTIVO"].ToString();
                    
                    dto.AnoInicio = DateTime.Parse(dr["ANO_INICIO"].ToString());
                    
                    dto.AnoTermino = DateTime.Parse(dr["ANO_TERMINO"].ToString());
                    
                    if (dr["ANO_INICIO_MATRICULA"].ToString() != null && !dr["ANO_INICIO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoInicioMatricula = DateTime.Parse(dr["ANO_INICIO_MATRICULA"].ToString());
                    }

                    if (dr["ANO_TERMINO_MATRICULA"].ToString() != null && !dr["ANO_TERMINO_MATRICULA"].ToString().Equals(""))
                    {
                        dto.AnoTerminoMatricula = DateTime.Parse(dr["ANO_TERMINO_MATRICULA"].ToString());
                    }
                    dto.AnoStatus = dr["ANO_STATUS"].ToString();
                    dto.AnoCivil = int.Parse(dto.AnoAno);

                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<AnoLectivoDTO>();
                dto = new AnoLectivoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public AnoLectivoDTO ImportacaoNovoAnoLectivo()
        {
            AnoLectivoDTO dto = new AnoLectivoDTO();
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_IMPORTAR";
                
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


        
        
        public RelatorioAlunoDTO ObterEstatisticaResumida(AnoLectivoDTO dto)
        { 
            RelatorioAlunoDTO dados = new RelatorioAlunoDTO();
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_RESUMO_ESTATISTICO";


                BaseDados.AddParameter("@ANO", dto.AnoAno);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader(); 
                
                while (dr.Read())
                {
                    dados = new RelatorioAlunoDTO();
                    dados.AnoLectivo = int.Parse(dr[0]);
                    dados.TotalGeral = dr[1];
                    dados.Inscritos = dr[2];
                    dados.Matriculados = dr[3];
                    dados.Masculino = dr[4];
                    dados.Feminino = dr[5]; 
                }

            }
            catch (Exception ex)
            {
                
                dados.Sucesso = false;
                dados.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dados;
        }

        public List<RelatorioAlunoDTO> ObterEstatisticaDetalhada(AnoLectivoDTO dto)
        {
            List<RelatorioAlunoDTO> lista = new List<RelatorioAlunoDTO>();
            RelatorioAlunoDTO dados;
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_ESTATISTICA_DETALHADA";


                BaseDados.AddParameter("@ANO", dto.AnoAno);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dados = new RelatorioAlunoDTO();
                    dados.Classe = dr[0];
                    dados.Curso = dr[1];
                    dados.Inscritos = dr[2];
                    dados.Matriculados = dr[3];
                    dados.Masculino = dr[4];
                    dados.Feminino = dr[5];
                    dados.CompanyID = dr[6]; 

                    lista.Add(dados);
                }

            }
            catch (Exception ex)
            {
                dados = new RelatorioAlunoDTO();
                lista = new List<RelatorioAlunoDTO>();
                dados.Sucesso = false;
                dados.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(dados);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public RelatorioAlunoDTO DashBoard(AnoLectivoDTO dto)
        {
            List<RelatorioAlunoDTO> lista = new List<RelatorioAlunoDTO>();
            RelatorioAlunoDTO dadosPainel = new RelatorioAlunoDTO();
            try
            {
                BaseDados.ComandText = "stp_ACA_ANO_LECTIVO_POSICAO_INTEGRADA";


                BaseDados.AddParameter("@ANO", dto.AnoCodigo);
                BaseDados.AddParameter("@FILIAL", dto.AnoAno);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {

                    dadosPainel.AnoLectivo = int.Parse(dr[0]);
                    dadosPainel.Inscritos = dr[2];
                    dadosPainel.Matriculados = dr[3];
                    dadosPainel.Situacao = dr[4];
                    dadosPainel.Nome = dr[5];
                    dadosPainel.Inscricao = dr[6];
                    dadosPainel.Matriculados = dr[9];
                    dadosPainel.NovasInscricoes = dr[10];
                    dadosPainel.Desistentes = dr[11];


                }

            }
            catch (Exception ex)
            {

                dadosPainel.Sucesso = false;
                dadosPainel.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dadosPainel;
        }
         
    }
}
