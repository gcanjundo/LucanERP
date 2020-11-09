using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Seguranca
{
    public class IdiomaDAO:ConexaoDB 
    {
         

        public IdiomaDTO Adicionar(IdiomaDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_IDIOMA_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                //AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public IdiomaDTO Alterar(IdiomaDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_IDIOMA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                //AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = ExecuteNonQuery();
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

        public IdiomaDTO Eliminar(IdiomaDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_IDIOMA_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = ExecuteNonQuery();
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

        public List<IdiomaDTO> ObterPorFiltro(IdiomaDTO dto)
        {
            List<IdiomaDTO> lista;
            try
            {
                ComandText = "stp_SIS_IDIOMA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<IdiomaDTO>();

                while(dr.Read())
                {
                   dto = new IdiomaDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new IdiomaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<IdiomaDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public IdiomaDTO ObterPorPK(IdiomaDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_IDIOMA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new IdiomaDTO();

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
