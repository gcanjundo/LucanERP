using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class DisciplinaDAO 
    {

        readonly ConexaoDB BaseDados;

        public DisciplinaDAO()
        {
            BaseDados = new ConexaoDB();
        }
        public DisciplinaDTO Adicionar(DisciplinaDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_DISCIPLINA_ADICIONAR";
                 BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                 BaseDados.AddParameter("SITUACAO", dto.Estado);
                 BaseDados.AddParameter("SIGLA", dto.Sigla);
                 BaseDados.AddParameter("FILIAL", dto.Filial);

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

        public DisciplinaDTO Alterar(DisciplinaDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_DISCIPLINA_ALTERAR";
                 BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                 BaseDados.AddParameter("SITUACAO", dto.Estado);
                 BaseDados.AddParameter("SIGLA", dto.Sigla);
                 BaseDados.AddParameter("CODIGO", dto.Codigo);
                 BaseDados.AddParameter("FILIAL", dto.Filial);

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

        public DisciplinaDTO Eliminar(DisciplinaDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_DISCIPLINA_EXCLUIR";
                 
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

        public List<DisciplinaDTO> ObterPorFiltro(DisciplinaDTO dto)
        {
            List<DisciplinaDTO> listaDisciplinas;
            try
            {
                 BaseDados.ComandText = "stp_ACA_DISCIPLINA_OBTERPORFILTRO";

                 BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                 BaseDados.AddParameter("FILIAL", dto.Estado);

                MySqlDataReader dr =  BaseDados.ExecuteReader();

                listaDisciplinas = new List<DisciplinaDTO>();

                while (dr.Read())
                {
                   dto = new DisciplinaDTO();

                   dto.Codigo = int.Parse(dr[0]);
                   dto.Descricao = dr[1];
                   dto.Sigla = dr[2];
                   dto.Estado = int.Parse(dr[3]);

                   listaDisciplinas.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DisciplinaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDisciplinas = new List<DisciplinaDTO>();
                listaDisciplinas.Add(dto);
            }
            finally
            {
                 BaseDados.FecharConexao();
            }

            return listaDisciplinas;
        }

        public DisciplinaDTO ObterPorPK(DisciplinaDTO dto)
        {
            try
            {
                 BaseDados.ComandText = "stp_ACA_DISCIPLINA_OBTERPORPK";

                 BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr =  BaseDados.ExecuteReader();

                dto = new DisciplinaDTO();

                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];
                    dto.Estado = int.Parse(dr[3]);

                   
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
