using System;
using System.Collections.Generic;

using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.RecursosHumanos
{
    public class TipoAlteracaoPeriodicaDAO :ConexaoDB
    {


        public TipoDTO Adicionar(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@TIPO", dto.Operacao); 
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

        public TipoDTO Alterar(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo); 
                AddParameter("@TIPO", dto.Operacao);

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

        public TipoDTO Eliminar(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_EXCLUIR";

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

        public List<TipoDTO> ObterPorFiltro(TipoDTO dto)
        {
            List<TipoDTO> lista;
            try
            {
                ComandText = "stp_GER_TIPO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<TipoDTO>();

                while (dr.Read())
                {
                    dto = new TipoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Operacao = dr[4].ToString();
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new TipoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<TipoDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public TipoDTO ObterPorPK(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new TipoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Operacao = dr[4].ToString();


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
