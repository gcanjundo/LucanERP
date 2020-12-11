using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class GrupoSanguineoDAO:ConexaoDB 
    {
         

        public GrupoSanguineoDTO Adicionar(GrupoSanguineoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_SANGUINEO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public GrupoSanguineoDTO Alterar(GrupoSanguineoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_SANGUINEO_ALTERAR";
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

        public GrupoSanguineoDTO Eliminar(GrupoSanguineoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_SANGUINEO_EXCLUIR";
                 
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

        public List<GrupoSanguineoDTO> ObterPorFiltro(GrupoSanguineoDTO dto)
        {
            List<GrupoSanguineoDTO> listaGrupoSanguineos;
            try
            {
                ComandText = "stp_GER_GRUPO_SANGUINEO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = ExecuteReader();

                listaGrupoSanguineos = new List<GrupoSanguineoDTO>();

                while(dr.Read())
                {
                   dto = new GrupoSanguineoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaGrupoSanguineos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new GrupoSanguineoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaGrupoSanguineos = new List<GrupoSanguineoDTO>();
                listaGrupoSanguineos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaGrupoSanguineos;
        }

        public GrupoSanguineoDTO ObterPorPK(GrupoSanguineoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_SANGUINEO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new GrupoSanguineoDTO();

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
    }
}
