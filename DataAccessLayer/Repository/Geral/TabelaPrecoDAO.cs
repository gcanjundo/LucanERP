using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class TabelaPrecoDAO :ConexaoDB
    {


        public TabelaPrecoDTO Adicionar(TabelaPrecoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_TABELA_PRECO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador);
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

        public TabelaPrecoDTO Alterar(TabelaPrecoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_TABELA_PRECO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
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

        public TabelaPrecoDTO Eliminar(TabelaPrecoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_TABELA_PRECO_EXCLUIR";

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

        public List<TabelaPrecoDTO> ObterPorFiltro(TabelaPrecoDTO dto)
        {
            List<TabelaPrecoDTO> listaTipoContactos;
            try
            {
                ComandText = "stp_COM_TABELA_PRECO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

                listaTipoContactos = new List<TabelaPrecoDTO>();

                while (dr.Read())
                {
                    dto = new TabelaPrecoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Sigla = dr[2].ToString(),
                        Estado = int.Parse(dr[3].ToString())
                    };

                    listaTipoContactos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new TabelaPrecoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaTipoContactos = new List<TabelaPrecoDTO>();
                listaTipoContactos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaTipoContactos;
        }

        public TabelaPrecoDTO ObterPorPK(TabelaPrecoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_TABELA_PRECO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new TabelaPrecoDTO();

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
