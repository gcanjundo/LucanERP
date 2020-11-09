using Dominio.Comercial;
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial
{
    public class PromocaoDAO
    {
        readonly ConexaoDB BaseDados;

        public PromocaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PromocaoDTO Adicionar(PromocaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_PROMOCAO_ADICIONAR";


                BaseDados.AddParameter("@PROMO_ID", dto.Codigo);
                BaseDados.AddParameter("@DESIGNACAO", dto.Descricao);
                BaseDados.AddParameter("@VALIDADE_INI", dto.ValidationStartDate);
                BaseDados.AddParameter("@VALIDADE_TERM", dto.ValidationEndDate);
                BaseDados.AddParameter("@RECORRENCIA", dto.Recorrencia);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@ALLPRODUCT", dto.AllProducts == true ? 1 : 0);
                BaseDados.AddParameter("@QUANTIDADE", dto.QuatidadeCompra);
                BaseDados.AddParameter("@PRECO_REDUZIDO", dto.MontanteMinimo);
                BaseDados.AddParameter("@PRECO_FIXO", dto.ValorMonetarioFixo);
                BaseDados.AddParameter("@DESCONTO", dto.Valor);
                BaseDados.AddParameter("@UNIDADE_REDUCAO", dto.Unidade);
                BaseDados.AddParameter("@UNIDADE_PRECO", dto.Unidade);
                BaseDados.AddParameter("@UNIDADE_DESCONTO", dto.Unidade);
                BaseDados.AddParameter("@PROMO_STATUS", dto.Status);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador); 
                BaseDados.AddParameter("@INICIO", dto.Begin);
                BaseDados.AddParameter("@TERMINO", dto.End); 
                BaseDados.AddParameter("@LIMITE", dto.LimiteUtilizacaoDate);
                BaseDados.AddParameter("@TABLE_PRICE", dto.TablePriceID <=0 ? (object)DBNull.Value : dto.TablePriceID);
                BaseDados.AddParameter("@CUSTOMER_ID", dto.ForCustomer == "-1" ? (object)DBNull.Value : dto.ForCustomer);
                BaseDados.AddParameter("@CATEGORIA_ID", dto.CategoriaID<=0 ? (object)DBNull.Value : dto.CategoriaID);
                BaseDados.AddParameter("@WAREHOUSE_ID", dto.WareHouseName== "-1" ? (object)DBNull.Value: dto.WareHouseName);
                BaseDados.AddParameter("@ALLSALES", dto.AllSalesValues == true ? 1 : 0);
                BaseDados.AddParameter("@MARK_ID", dto.MarcaID <= 0 ? (object)DBNull.Value : dto.MarcaID);
                BaseDados.AddParameter("@PRODUCT_ID", dto.ProductID <=0 ? (object)DBNull.Value : dto.ProductID);

                dto.Codigo = BaseDados.ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public PromocaoDTO Excluir(PromocaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_PROMOCAO_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo); 
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador); 


                dto.Codigo = BaseDados.ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public PromocaoDTO ObterPorPK(PromocaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_PROMOCAO_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new PromocaoDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.ValidationStartDate = DateTime.Parse(dr[2].ToString());
                    dto.ValidationEndDate = DateTime.Parse(dr[3].ToString());
                    dto.Recorrencia = dr[4].ToString();
                    dto.Tipo = dr[5].ToString();
                    dto.AllProducts = dr[6].ToString() != "1" ? false : true;
                    dto.QuatidadeCompra = decimal.Parse(dr[7].ToString());
                    dto.MontanteMinimo = decimal.Parse(dr[8].ToString());
                    dto.ValorMonetarioFixo = decimal.Parse(dr[9].ToString());
                    dto.Valor = decimal.Parse(dr[10].ToString());
                    dto.Unidade = dr[11].ToString();
                    dto.Status = int.Parse(dr[14].ToString());
                    dto.Begin = dr[15].ToString() != "" ? DateTime.Parse(dr[15].ToString()) : DateTime.MinValue;
                    dto.End = dr[16].ToString() != "" ? DateTime.Parse(dr[16].ToString()) : DateTime.MinValue;
                    dto.LimiteUtilizacaoDate = dto.Begin = dr[17].ToString() != "" ? DateTime.Parse(dr[17].ToString()) : DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public List<PromocaoDTO> ObterPorFiltro(PromocaoDTO dto)
        {
            var lista = new List<PromocaoDTO>();
            try
            {
                BaseDados.ComandText = "stp_COM_PROMOCAO_OBTERPORFILTRO";

                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                
                while (dr.Read())
                {
                    dto = new PromocaoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.ValidationStartDate = DateTime.Parse(dr[2].ToString());
                    dto.ValidationEndDate = DateTime.Parse(dr[3].ToString());
                    dto.Recorrencia = dr[4].ToString();
                    dto.Tipo = dr[5].ToString();
                    dto.AllProducts = dr[6].ToString()!="1" ? false: true;
                    dto.QuatidadeCompra = decimal.Parse(dr[7].ToString());
                    dto.MontanteMinimo = decimal.Parse(dr[8].ToString());
                    dto.ValorMonetarioFixo = decimal.Parse(dr[9].ToString());
                    dto.Valor = decimal.Parse(dr[10].ToString());
                    dto.Unidade = dr[11].ToString();
                    dto.Status = int.Parse(dr[14].ToString());
                    dto.Begin = dr[15].ToString() != "" ? DateTime.Parse(dr[15].ToString()) : DateTime.MinValue;
                    dto.End = dr[16].ToString() != "" ? DateTime.Parse(dr[16].ToString()) : DateTime.MinValue;
                    dto.LimiteUtilizacaoDate = dto.Begin = dr[17].ToString() != "" ? DateTime.Parse(dr[17].ToString()) : DateTime.MinValue;
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public void AddProduct(PromocaoDTO dto, List<ArtigoDTO> pList)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_PROMOCAO_OBTERPORPK";

                BaseDados.AddParameter("@PROMOCAO_ID", dto.Codigo);
                 
                foreach (var product in pList)
                {
                    BaseDados.AddParameter("@PRODUCT_ID", product.Codigo);
                    BaseDados.AddParameter("@DESCONTO", product.Desconto);
                    BaseDados.AddParameter("@VALOR", product.PrecoVenda); 
                    BaseDados.AddParameter("@DELETED", product.Status);
                    BaseDados.AddParameter("@UTILIZADOR", product.Utilizador); 

                    BaseDados.ExecuteNonQuery(); 
                    dto.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
        }
    }
}
