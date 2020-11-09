using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using MySql.Data.MySqlClient;
using Dominio.Comercial;


namespace DataAccessLayer.Comercial
{
    public class MetodoPagamentoDAO: ConexaoDB 
    {
         

        public MetodoPagamentoDTO Adicionar(MetodoPagamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_PAGAMENTO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("INCIDENCIA", dto.Sigla);
                AddParameter("POS_VISIBLE", dto.POSVisible);
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

        public MetodoPagamentoDTO Alterar(MetodoPagamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_PAGAMENTO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("INCIDENCIA", dto.Sigla);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("POS_VISIBLE", dto.POSVisible);

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

        public MetodoPagamentoDTO Eliminar(MetodoPagamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_PAGAMENTO_EXCLUIR";
                 
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

        public List<MetodoPagamentoDTO> ObterPorFiltro(MetodoPagamentoDTO dto)
        {
            List<MetodoPagamentoDTO> listaMetodos;
            try
            {
                ComandText = "stp_GER_FORMA_PAGAMENTO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                listaMetodos = new List<MetodoPagamentoDTO>();

                while(dr.Read())
                {
                   dto = new MetodoPagamentoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.POSVisible = int.Parse(dr[4].ToString());
                   dto.DescricaoPagamento = dr[5].ToString();
                   dto.Icon = dr[12].ToString();
                   dto.PaymentMode = dr[14].ToString(); 
                   listaMetodos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new MetodoPagamentoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaMetodos = new List<MetodoPagamentoDTO>();
                listaMetodos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaMetodos;
        } 

        public MetodoPagamentoDTO ObterPorPK(MetodoPagamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_PAGAMENTO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new MetodoPagamentoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.POSVisible = int.Parse(dr[4].ToString());
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
