using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class GrauAcademicoDAO 
    {

        readonly ConexaoDB BaseDados;

        public GrauAcademicoDAO()
        {
            BaseDados = new ConexaoDB();
        }
        public GrauAcademicoDTO Adicionar(GrauAcademicoDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_CURSO_GRAU_ACADEMICO_ADICIONAR";
                 BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                 BaseDados.AddParameter("SIGLA", dto.Sigla); 
                 BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("COORDENACAO", dto.NivelEnsino);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                

                dto.Codigo =  BaseDados.ExecuteInsert();
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

        public GrauAcademicoDTO Alterar(GrauAcademicoDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_CURSO_GRAU_ACADEMICO_ALTERAR";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("COORDENACAO", dto.NivelEnsino);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("CODIGO", dto.Codigo); ;

                dto.Codigo =  BaseDados.ExecuteNonQuery();
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

        public GrauAcademicoDTO Eliminar(GrauAcademicoDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_CURSO_GRAU_ACADEMICO_EXCLUIR";
                 
                 BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto.Codigo =  BaseDados.ExecuteNonQuery();
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

        public List<GrauAcademicoDTO> ObterPorFiltro(GrauAcademicoDTO dto)
        {
            List<GrauAcademicoDTO> lista;
            try
            {
                 BaseDados.ComandText = "stp_ACA_CURSO_GRAU_ACADEMICO_OBTERPORFILTRO";

                  
                MySqlDataReader dr =  BaseDados.ExecuteReader();

                lista = new List<GrauAcademicoDTO>();

                foreach (var dr in reader)
                {
                    dto = new GrauAcademicoDTO();

                    dto.Codigo = int.Parse(dr[0]);
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];
                    dto.Estado = int.Parse(dr[3]);
                    dto.NivelEnsino = dr[4];
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new GrauAcademicoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<GrauAcademicoDTO>
                {
                    dto
                };
            }
            finally
            {
                 BaseDados.FecharConexao();
            }

            return lista;
        }

        public GrauAcademicoDTO ObterPorPK(GrauAcademicoDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_CURSO_GRAU_ACADEMICO_OBTERPORPK";

                 BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr =  BaseDados.ExecuteReader();

                dto = new GrauAcademicoDTO();

                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];
                    dto.Estado = int.Parse(dr[3]);
                    dto.NivelEnsino = dr[4]; 
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

        public List<FormacaoCursoDTO > ObterFormacao()
        {
            List<FormacaoCursoDTO > lista;
            FormacaoCursoDTO  dto;
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_FORMACAO_OBTERPORFILTRO";

                 
                MySqlDataReader dr = BaseDados.ExecuteReader();

                lista = new List<FormacaoCursoDTO >();

                foreach (var dr in reader)
                {
                    dto = new FormacaoCursoDTO 
                    {
                        Codigo = int.Parse(dr[0]),
                        Descricao = dr[1],
                        GrauAcademico = new Tuple<int, string>(int.Parse(dr[2]), ""),
                        Sigla = dr[3],
                        Estado = int.Parse(dr[4]),
                        
                    };
                    bool boolStatus = dr[9] == null || dr[9] != "1" ? false : true;
                    dto.Curricular = new Tuple<int, bool>(int.Parse(dr[9]), boolStatus);
                    boolStatus = dr[10] == null || dr[10] != "1" ? false : true;
                    dto.CurtaDuracao = new Tuple<int, bool>(int.Parse(dr[10]), boolStatus);
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new FormacaoCursoDTO 
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                lista = new List<FormacaoCursoDTO >
                {
                    dto
                };
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
