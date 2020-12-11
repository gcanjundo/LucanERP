using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class ImpostosDAO :ConexaoDB
    {


        public ImpostosDTO Adicionar(ImpostosDTO dto)
        {
            try
            {
                ComandText = "stp_GER_IMPOSTOS_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@VALOR", dto.Valor);
                AddParameter("@TIPO", dto.Tipo);
                AddParameter("@CATEGORIA", dto.Categoria);
                AddParameter("@VALORIZACAO", dto.Valorizacao);
                AddParameter("@NOTAS", dto.Notes);
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
       

        public ImpostosDTO Eliminar(ImpostosDTO dto)
        {
            try
            {
                ComandText = "stp_GER_IMPOSTOS_EXCLUIR";

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

        public List<ImpostosDTO> ObterPorFiltro(ImpostosDTO dto)
        {
            List<ImpostosDTO> lista;
            try
            {
                ComandText = "stp_GER_IMPOSTOS_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty); 

                MySqlDataReader dr = ExecuteReader();

                lista = new List<ImpostosDTO>();

                while (dr.Read())
                {
                    dto = new ImpostosDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Valor = decimal.Parse(dr[4].ToString());
                    dto.Descricao += " (" + dto.Valor + "%)";
                    dto.Valorizacao = dr[5].ToString();
                    dto.ZonaFiscal = dr[6].ToString();
                    dto.Tipo = dr[7].ToString();
                    dto.Categoria = dr[8].ToString();
                    dto.SaftTaxLine = dr[9].ToString();
                    dto.Notes = dr[10].ToString();
                    dto.InternalCode = dr[11].ToString();
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ImpostosDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<ImpostosDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public ImpostosDTO ObterPorPK(ImpostosDTO dto)
        {
            try
            {
                ComandText = "stp_GER_IMPOSTOS_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new ImpostosDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Valor = decimal.Parse(dr[4].ToString());
                    dto.Valorizacao = dr[5].ToString();
                    dto.ZonaFiscal = dr[6].ToString();
                    dto.Tipo = dr[7].ToString();
                    dto.Categoria = dr[8].ToString();
                    dto.SaftTaxLine = dr[9].ToString();
                    dto.Notes = dr[10].ToString();
                    dto.InternalCode = dr[11].ToString();
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
