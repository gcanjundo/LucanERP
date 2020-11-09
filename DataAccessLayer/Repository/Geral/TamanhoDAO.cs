using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class TamanhoDAO:ConexaoDB 
    {
         

        public TamanhoDTO Adicionar(TamanhoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TAMANHO_ADICIONAR";
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

        public TamanhoDTO Alterar(TamanhoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TAMANHO_ALTERAR";
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

        public TamanhoDTO Eliminar(TamanhoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TAMANHO_EXCLUIR";
                 
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

        public List<TamanhoDTO> ObterPorFiltro(TamanhoDTO dto)
        {
            List<TamanhoDTO> lista;
            try
            {
                ComandText = "stp_GER_TAMANHO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<TamanhoDTO>();

                while(dr.Read())
                {
                   dto = new TamanhoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new TamanhoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<TamanhoDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public TamanhoDTO ObterPorPK(TamanhoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TAMANHO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new TamanhoDTO();

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
