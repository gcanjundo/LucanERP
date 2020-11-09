using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class EspecialidadeDAO: ConexaoDB 
    {
         

        public EspecialidadeDTO Adicionar(EspecialidadeDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ESPECIALIDADE_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);

               dto.Codigo = ExecuteInsert();
               dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally 
            {
                FecharConexao();
            }

            return dto;
        }

        public EspecialidadeDTO Alterar(EspecialidadeDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ESPECIALIDADE_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public EspecialidadeDTO Eliminar(EspecialidadeDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ESPECIALIDADE_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<EspecialidadeDTO> ObterPorFiltro(EspecialidadeDTO dto)
        {
            List<EspecialidadeDTO> listaEspecialidades = new List<EspecialidadeDTO>();
            try
            {
                ComandText = "stp_CLI_ESPECIALIDADE_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla??"");
                AddParameter("DEPARTAMENTO_ID", dto.LookupField1);

                MySqlDataReader dr = ExecuteReader();

                while(dr.Read())
                {
                   dto = new EspecialidadeDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   if (dto.Sigla.Equals("MD"))
                   {
                       dto.Categoria = "MÉDICA";
                   }
                   else if (dto.Sigla.Equals("PD"))
                   {
                       dto.Categoria = "PEDIATRÍCA";
                   }
                   else if (dto.Sigla.Equals("GO"))
                   {
                       dto.Categoria = "GINECOLOGIA E OBSTETRÍCIA";
                   }
                   else if (dto.Sigla.Equals("CG"))
                   {
                       dto.Categoria = "CIRURGICA";
                   }

                   listaEspecialidades.Add(dto);
                }

            }
            catch (Exception ex)
            { 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", ""); 
            }
            finally
            {
                FecharConexao();
            }

            return listaEspecialidades;
        }

        public EspecialidadeDTO ObterPorPK(EspecialidadeDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ESPECIALIDADE_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new EspecialidadeDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }


        public void AddEspecialidadeProfissional(EspecialidadeProfissionalDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_PROFISSIONAL_ESPECIALIDADE_ADICIONAR";

                AddParameter("PROFISSIONAL_ID", dto.ProfissionalID);
                AddParameter("ESPECIALIDADE_ID", dto.EspecialidadeID);
                AddParameter("UTILIZADOR", dto.Utilizador); 
                AddParameter("STATUS_ID", dto.Estado);
                AddParameter("VALOR", dto.ValorActo);
                AddParameter("PERCENTAGEM", dto.Percentagem);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

        }

        public List<EspecialidadeProfissionalDTO> ObterGetByProfessional(EspecialidadeProfissionalDTO dto)
        {
            List<EspecialidadeProfissionalDTO> lista = new List<EspecialidadeProfissionalDTO>();
            try
            {
                ComandText = "stp_CLI_PROFISSIONAL_ESPECIALIDADE_OBTERFILTRO";
                 
                AddParameter("PROFISSIONAL", dto.ProfissionalID);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new EspecialidadeProfissionalDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.ProfissionalID = int.Parse(dr[1].ToString());
                    dto.EspecialidadeID = int.Parse(dr[2].ToString());
                    dto.Percentagem = decimal.Parse(dr[5].ToString());
                    dto.ValorActo = decimal.Parse(dr[6].ToString());
                    dto.Descricao = dr[7].ToString();
          
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
                FecharConexao();
            }

            return lista;
        }
    }
}
