using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class TipoActividadeDAO :ConexaoDB
    {


        public TipoActividadeDTO Adicionar(TipoActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ACTIVIDADE_ADICIONAR";
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

        public TipoActividadeDTO Alterar(TipoActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ACTIVIDADE_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador); 

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

        public TipoActividadeDTO Eliminar(TipoActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ACTIVIDADE_EXCLUIR";

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

        public List<TipoActividadeDTO> ObterPorFiltro(TipoActividadeDTO dto)
        {
            List<TipoActividadeDTO> lista;
            try
            {
                ComandText = "stp_GER_TIPO_ACTIVIDADE_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty); 

                MySqlDataReader dr = ExecuteReader();

                lista = new List<TipoActividadeDTO>();

                while (dr.Read())
                {
                    dto = new TipoActividadeDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new TipoActividadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<TipoActividadeDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public TipoActividadeDTO ObterPorPK(TipoActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ACTIVIDADE_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new TipoActividadeDTO();

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
