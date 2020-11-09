using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class CursoDAO
    {
        readonly ConexaoDB BaseDados;

        public CursoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public CursoDTO Inserir(CursoDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_ADICIONAR";

                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@SIGLA", dto.Abreviatura);
                BaseDados.AddParameter("@DURACAO", dto.Duracao);
                BaseDados.AddParameter("@ESPECIFICACAO", dto.Especificacao);                 
                BaseDados.AddParameter("@SITUACAO", dto.Estado);
                BaseDados.AddParameter("@TEMPO", dto.Tempo);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@CURSO", dto.Ramo);
                BaseDados.AddParameter("@FORMACAO", dto.Formacao);
                BaseDados.AddParameter("@AREA", dto.Area);
                BaseDados.AddParameter("@COORDENADOR", dto.Coordenador);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@MODELO_PAUTA", dto.FormatoPauta);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                dto.Codigo = BaseDados.ExecuteInsert();
                 if (dto.Codigo > 0) 
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

        public CursoDTO Alterar(CursoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_ALTERAR";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@NOME", dto.Nome);
                BaseDados.AddParameter("@SIGLA", dto.Abreviatura);
                BaseDados.AddParameter("@DURACAO", dto.Duracao);
                BaseDados.AddParameter("@ESPECIFICACAO", dto.Especificacao);
                BaseDados.AddParameter("@SITUACAO", dto.Estado);
                BaseDados.AddParameter("@TEMPO", dto.Tempo); 
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@CURSO", dto.Ramo);
                BaseDados.AddParameter("@FORMACAO", dto.Formacao);
                BaseDados.AddParameter("@AREA", dto.Area);
                BaseDados.AddParameter("@COORDENADOR", dto.Coordenador);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@MODELO_PAUTA", dto.FormatoPauta);
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

        public void Apagar(CursoDTO dto)
        {

            
            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_EXCLUIR";
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

        public CursoDTO ObterPorPK(CursoDTO dto)
        {

            
            try
            {
               BaseDados.ComandText = "stp_ACA_CURSO_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new CursoDTO();
                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["CUR_CODIGO"].ToString());
                    dto.Nome = dr["CUR_NOME"].ToString();
                    dto.Abreviatura = dr["CUR_ABREVIACAO"].ToString();
                    dto.Duracao = int.Parse(dr["CUR_DURACAO"].ToString());
                    dto.Estado = int.Parse(dr["CUR_STATUS"].ToString());
                    dto.Especificacao = dr["CUR_ESPECIFICACAO"].ToString();
                    dto.Tempo = dr["CUR_TEMPO"].ToString();
                    dto.Inicio = int.Parse(dr["CUR_INICIO"].ToString());
                    dto.Termino = int.Parse(dr["CUR_TERMINO"].ToString());
                    dto.Ramo = dr["CUR_CODIGO_CURSO"].ToString();
                    dto.Formacao = dr["CUR_FORMACAO"].ToString();
                    dto.Area = dr["CUR_AREA_FORMACAO"].ToString();
                    dto.FormatoPauta = dr["CUR_MODELO_PAUTA"].ToString();
                    dto.NivelEnsino = dr["FORM_GRAU_FORMACAO_ID"].ToString();
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

         
        public List<CursoDTO> ObterPorFiltro(CursoDTO dto)
        {
            
            List<CursoDTO> lista = new List<CursoDTO>();
            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_OBTERPORFILTRO";
                BaseDados.AddParameter("@CURSO", dto.Nome);
                BaseDados.AddParameter("@SIGLA", dto.Abreviatura);
                BaseDados.AddParameter("@ESPECIFICACAO", dto.Especificacao);
                BaseDados.AddParameter("@SITUACAO", dto.Estado);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@UNIDADE", dto.Ensino);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<CursoDTO>();
                while (dr.Read())
                {
                    dto = new CursoDTO();
                    dto.Codigo = Int32.Parse(dr["CUR_CODIGO"].ToString());
                    dto.Nome = dr["CUR_NOME"].ToString();
                    dto.Abreviatura = dr["CUR_ABREVIACAO"].ToString();
                    dto.Duracao = int.Parse(dr["CUR_DURACAO"].ToString());
                    dto.Estado = int.Parse(dr["CUR_STATUS"].ToString());
                    dto.Especificacao = dr["CUR_ESPECIFICACAO"].ToString();
                    dto.Tempo = dr["CUR_TEMPO"].ToString();
                    dto.Inicio = int.Parse(dr["CUR_INICIO"].ToString());
                    dto.Termino = int.Parse(dr["CUR_TERMINO"].ToString());
                    dto.Formacao = dr["CUR_FORMACAO"].ToString();
                    dto.FormatoPauta = dr["CUR_MODELO_PAUTA"].ToString();
                    dto.Ramo = dr["CUR_CODIGO_CURSO"].ToString();
                    lista.Add(dto);

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

        public List<CursoDTO> PosicaoIntegrada(RelatorioAlunoDTO unidade)
        {
            
            List<CursoDTO> lista = new List<CursoDTO>();
            CursoDTO dto = null;
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_POSICAO_INTEGRADA";
                BaseDados.AddParameter("@ANO", unidade.AnoLectivo);
                BaseDados.AddParameter("@FILIAL", unidade.Codigo);
                BaseDados.AddParameter("@ESPECIFICACAO", unidade.Situacao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<CursoDTO>();
                
                while (dr.Read())
                {
                    dto = new CursoDTO();
                    dto.Codigo = Int32.Parse(dr["CUR_CODIGO"].ToString());
                    dto.Nome = dr["CUR_NOME"].ToString();
                    dto.Inicio = int.Parse(dr["INSCRITOS"].ToString());
                    dto.Duracao = int.Parse(dr["TOTAL_MATRICULADOS"].ToString());
                    dto.Termino = int.Parse(dr["CONFIRMADOS"].ToString());
                    
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {

                dto = new CursoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");  
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public List<CursoDTO> ObterAreasCientifica(string pEnsino)
        {

            
            CursoDTO dto = null;

            List<CursoDTO> lista =null;
            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_AREA_CIENTIFICA";

                BaseDados.AddParameter("ENSINO", pEnsino);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<CursoDTO>();
                
                while (dr.Read())
                {
                    dto = new CursoDTO();
                    dto.Codigo = Int32.Parse(dr["CUR_CODIGO"].ToString());
                    dto.Nome = dr["CUR_NOME"].ToString();
                    
                    lista.Add(dto);

                }
                lista.Insert(0,new CursoDTO(-1, "SELECCIONE"));

            }
            catch (Exception ex)
            {
                dto = new CursoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public CursoDTO isAutorizado(string pUtilizador, int pCurso)
        {

            
            List<CursoDTO> lista = null;
            CursoDTO dto = new CursoDTO();
            try
            {

                BaseDados.ComandText = "stp_ACA_NOTA_VALIDACAO_OBTER_PERMISSAO";
                BaseDados.AddParameter("@UTILIZADOR", pUtilizador);
                BaseDados.AddParameter("@CURSO", pCurso);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<CursoDTO>();

                
                while (dr.Read())
                {
                    dto = new CursoDTO
                    {
                        Codigo = int.Parse(dr["VAL_PERM_CURSO"].ToString()),
                        Sucesso = true
                    };
                    break;

                } 
            }
            catch (Exception ex)
            {
                dto = new CursoDTO
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

        public string SalvarPermissaoValidacao(CursoDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_VALIDACAO_PERMISSAO_ADICIONAR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@CURSO", dto.Codigo);
                BaseDados.AddParameter("@USER_SESSION", dto.CreatedBy);

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

            return dto.MensagemErro;
        }
    }
}
