using System;
using System.Collections.Generic;
using Dominio.Comercial.Restauracao;
using MySql.Data.MySqlClient;



namespace DataAccessLayer.Comercial.Restauracao
{
    public class LocalizacaoDAO: ConexaoDB 
    {
         

        public LocalizacaoDTO Adicionar(LocalizacaoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_LOCALIZACAO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                //AddParameter("SITUACAO", dto.Estado);
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

        public LocalizacaoDTO Alterar(LocalizacaoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_LOCALIZACAO_ALTERAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                //AddParameter("SITUACAO", dto.Estado);
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

        public LocalizacaoDTO Eliminar(LocalizacaoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_LOCALIZACAO_EXCLUIR";
                 
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

        public List<LocalizacaoDTO> ObterPorFiltro(LocalizacaoDTO dto)
        {
            List<LocalizacaoDTO> lista;
            try
            {
                ComandText = "stp_REST_LOCALIZACAO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<LocalizacaoDTO>();

                while(dr.Read())
                {
                   dto = new LocalizacaoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new LocalizacaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<LocalizacaoDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public LocalizacaoDTO ObterPorPK(LocalizacaoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_LOCALIZACAO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new LocalizacaoDTO();

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
