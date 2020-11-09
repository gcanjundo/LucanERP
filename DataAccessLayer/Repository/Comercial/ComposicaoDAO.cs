
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial
{
    public class ComposicaoDAO: ConexaoDB
    {
        public ComposicaoDTO Adicionar(ComposicaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_COMPOSICAO_ADICIONAR";

                AddParameter("ARTIGO", dto.ArtigoVirtualID);
                AddParameter("COMPONENTE", dto.Codigo);
                AddParameter("QUANTIDADE", dto.Quantidade);
                AddParameter("PRECO_UNITARIO", dto.PrecoVenda);
                AddParameter("VALOR", dto.PrecoCusto);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("PERCENTUAL", dto.Percentual);
                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<ComposicaoDTO> ObterPorFiltro(ArtigoDTO pArtigo)
        {
            List<ComposicaoDTO> lista = new List<ComposicaoDTO>();
            ComposicaoDTO dto;
            try
            {
                ComandText = "stp_GER_ARTIGO_COMPOSICAO_OBTERPORFILTRO";

                AddParameter("ARTIGO", pArtigo.Codigo);

                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new ComposicaoDTO();
                    dto.ArtigoVirtualID = int.Parse(dr[0].ToString());
                    dto.Codigo = int.Parse(dr[1].ToString());
                    dto.Designacao = dr[2].ToString();
                    dto.Quantidade = Convert.ToDecimal(dr[3].ToString() ?? "0");
                    dto.PrecoVenda = Convert.ToDecimal(dr[4].ToString() ?? "0");
                    dto.PrecoCusto = Convert.ToDecimal(dr[5].ToString() ?? "0");
                    dto.MovimentaStock = dr[6].ToString() !="1" ? false : true;
                    dto.Referencia = dr[7].ToString();
                    dto.Desconto = Convert.ToDecimal(dr[8].ToString());
                    dto.ImpostoLiquido = decimal.Parse(dr[9].ToString());
                    dto.WareHouseName = dr[10].ToString();
                    dto.Percentual = decimal.Parse(dr[11].ToString());
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new ComposicaoDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public ComposicaoDTO Excluir(ComposicaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_COMPOSICAO_EXCLUIR";

                AddParameter("@ARTIGO", dto.Codigo);

                ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }
    }
}
