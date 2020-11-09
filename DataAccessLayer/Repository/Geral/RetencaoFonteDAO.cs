using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class RetencaoFonteDAO :ConexaoDB
    {


        public RetencaoFonteDTO Adicionar(RetencaoFonteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RETENCAO_FONTE_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@VALOR", dto.Valor);
                AddParameter("@TIPO", dto.Tipo); 
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
       

        public RetencaoFonteDTO Eliminar(RetencaoFonteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RETENCAO_FONTE_EXCLUIR";

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

        public List<RetencaoFonteDTO> ObterPorFiltro(RetencaoFonteDTO dto)
        {
            List<RetencaoFonteDTO> lista;
            try
            {
                ComandText = "stp_GER_RETENCAO_FONTE_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao); 

                MySqlDataReader dr = ExecuteReader();

                lista = new List<RetencaoFonteDTO>();

                while (dr.Read())
                {
                    dto = new RetencaoFonteDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Valor = decimal.Parse(dr[3].ToString());
                    dto.Valorizacao = dr[4].ToString();
                    dto.Estado = int.Parse(dr[5].ToString());
                    dto.Descricao += "(" + dto.Valor + "%)"; 
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new RetencaoFonteDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<RetencaoFonteDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public RetencaoFonteDTO ObterPorPK(RetencaoFonteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RETENCAO_FONTE_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new RetencaoFonteDTO();

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
