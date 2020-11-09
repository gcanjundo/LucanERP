using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial.Stock
{
    public class DimensaoDAO:ConexaoDB
    {
        public DimensaoDTO Adicionar(DimensaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_DIMENSOES_ADICIONAR";
                
                AddParameter("@ARTIGO", dto.Codigo);
                AddParameter("@TAMANHO", dto.Tamanho);
                AddParameter("@COR", dto.Cor);
                AddParameter("@REFERENCIA", dto.Referencia); 
                AddParameter("@EXISTENCIA", dto.Quantidade);
                AddParameter("@VALIDADE", dto.DataValidade);
                AddParameter("@LIMITE_FATURACAO", dto.DataLimiteFaturacao);                
                AddParameter("@PRECO_CUSTO", dto.PrecoCusto);
                AddParameter("@FORNECEDOR", dto.Fornecedor);
                AddParameter("@CODIGO_BARRAS", dto.CodigoBarras);
                AddParameter("@UTILIZADOR", dto.Utilizador); 
                AddParameter("@ESTADO", dto.ProductStatus);
                AddParameter("@ARMAZEM", dto.WareHouseName);
                AddParameter("@ARTIGO_BASE", dto.ProductID);

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro += ex.Message.Replace("'", string.Empty);
                dto.Sucesso = false;
            }
            finally
            {
                FecharConexao();
            }
            return dto;
        }

        public List<DimensaoDTO> ObterPorFiltro(DimensaoDTO dto)
        {
            int codArmazem = dto.WareHouseName == null ? 1 : int.Parse(dto.WareHouseName);

            List<DimensaoDTO> lista = new List<DimensaoDTO>();
            try
            {
                ComandText = "stp_GER_ARTIGO_DIMENSOES_OBTERPORFILTRO";

                AddParameter("@ARTIGO", dto.Codigo);
                AddParameter("@TAMANHO", dto.Tamanho);
                AddParameter("@COR", dto.Cor);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@CODIGO_BARRAS", dto.CodigoBarras);
                AddParameter("@DESIGNACAO", dto.Designacao); 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new DimensaoDTO();
                    dto.Codigo = int.Parse(dr["DIM_CODIGO_ARTIGO"].ToString());
                    dto.CodigoBarras = dr["DIM_CODIGO_BARRAS"].ToString();
                    dto.Referencia = dr["DIM_REFERENCIA"].ToString();
                    //dto.Quantidade = new StockDAO().StockActual(dto.Codigo);
                    StockInfoDTO stockInfo = new StockDAO().StockActualArmazem(dto.Codigo, codArmazem, DateTime.MaxValue, DateTime.MaxValue);
                    dto.Quantidade = stockInfo.Actual;  
                    dto.Designacao = dr["ART_DESIGNACAO"].ToString().ToUpper();
                    dto.Categoria = dr["CAT_DESCRICAO"].ToString();

                    dto.PrecoVenda = Convert.ToDecimal(dr["ART_PRECO_VENDA"].ToString() ?? "0");

                    dto.UnidadeVenda = dr["UNI_SIGLA"].ToString();
                    dto.FotoArtigo = dr["ART_IMAGEM"].ToString();

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }
            return lista;
        }
    }
}
