using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class GrupoClientesDAO :ConexaoDB
    {


        public CategoriaDTO Adicionar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_CLIENTE_ADICIONAR";
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla?? string.Empty);
                AddParameter("@SITUACAO", dto.Estado);
                AddParameter("@TABELA_PRECO", dto.TablePriceID);
                AddParameter("@CONDICAO_PAGAMENTO", dto.PaymentTermsID);
                AddParameter("@LIMITE_CREDITO", dto.LimiteCredito);
                AddParameter("@PROMOCAO_ID", dto.PromocaoID);
                AddParameter("@VALOR_DESCONTO", dto.ValorDesconto);
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

      

        public CategoriaDTO Eliminar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_CLIENTE_EXCLUIR";

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

        public List<CategoriaDTO> ObterPorFiltro(CategoriaDTO dto)
        {
            List<CategoriaDTO> listaCategoriaContactos;
            try
            {
                ComandText = "stp_GER_GRUPO_CLIENTE_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao); 

                MySqlDataReader dr = ExecuteReader();

                listaCategoriaContactos = new List<CategoriaDTO>();

                while (dr.Read())
                {
                    dto = new CategoriaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.TablePriceID = int.Parse(dr[4].ToString() != "" ? dr[4].ToString() : "-1");
                    dto.PaymentTermsID = int.Parse(dr[5].ToString() != "" ? dr[5].ToString() : "-1");
                    dto.LimiteCredito = decimal.Parse(dr[6].ToString() != "" ? dr[6].ToString() : "0");
                    dto.PromocaoID = int.Parse(dr[7].ToString() != "" ? dr[7].ToString() : "-1");
                    dto.ValorDesconto = decimal.Parse(dr[8].ToString() != "" ? dr[8].ToString() : "0");

                    listaCategoriaContactos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new CategoriaDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                listaCategoriaContactos = new List<CategoriaDTO>();
                listaCategoriaContactos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaCategoriaContactos;
        }

        public CategoriaDTO ObterPorPK(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_GRUPO_CLIENTE_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new CategoriaDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.TablePriceID = int.Parse(dr[4].ToString()!="" ? dr[4].ToString() : "-1");
                    dto.PaymentTermsID = int.Parse(dr[5].ToString() != "" ? dr[5].ToString() : "-1");
                    dto.LimiteCredito = decimal.Parse(dr[6].ToString() != "" ? dr[6].ToString() : "0");
                    dto.PromocaoID = int.Parse(dr[7].ToString() != "" ? dr[7].ToString() : "-1");
                    dto.ValorDesconto = decimal.Parse(dr[8].ToString() != "" ? dr[8].ToString() : "0");

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
