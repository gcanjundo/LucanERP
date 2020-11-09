using System;
using System.Collections.Generic;

using Dominio.Comercial;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Comercial
{
    public class CondicaoPagamentoDAO 
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public CondicaoPagamentoDTO Adicionar(CondicaoPagamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_CONDICAO_PAGAMENTO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@CONDICAO", dto.Pagamento);
                BaseDados.AddParameter("@ENTRADA", dto.EntradaInicial);
                BaseDados.AddParameter("@PARCELAS", dto.NroPrestacoes);
                BaseDados.AddParameter("@PERIODICIDADE", dto.Periodicidade);
                BaseDados.AddParameter("@DESCONTO", dto.DescontoFinaceiro);
                BaseDados.AddParameter("@VENCIMENTO", dto.Vencimento);

                dto.Codigo = BaseDados.ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally 
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public CondicaoPagamentoDTO Alterar(CondicaoPagamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_CONDICAO_PAGAMENTO_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla); 
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@CONDICAO", dto.Pagamento);
                BaseDados.AddParameter("@ENTRADA", dto.EntradaInicial);
                BaseDados.AddParameter("@PARCELAS", dto.NroPrestacoes);
                BaseDados.AddParameter("@PERIODICIDADE", dto.Periodicidade);
                BaseDados.AddParameter("@DESCONTO", dto.DescontoFinaceiro);
                BaseDados.AddParameter("@VENCIMENTO", dto.Vencimento);

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public bool Eliminar(CondicaoPagamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_CONDICAO_PAGAMENTO_EXCLUIR";

                
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto.Sucesso;
        }

        public List<CondicaoPagamentoDTO> ObterPorFiltro(CondicaoPagamentoDTO dto)
        {
            List<CondicaoPagamentoDTO> listaCondicaoPagamento; 
            try
            {
                
                BaseDados.ComandText = "stp_COM_CONDICAO_PAGAMENTO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaCondicaoPagamento = new List<CondicaoPagamentoDTO>();
                while (dr.Read()) 
                {
                    dto = new CondicaoPagamentoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Pagamento = dr[4].ToString() == "" ? "AP" : dr[4].ToString();
                    dto.Vencimento = int.Parse(dr[5].ToString() == "" ? "0" : dr[5].ToString());
                    dto.EntradaInicial = decimal.Parse(dr[6].ToString() == "" ? "0" : dr[6].ToString());
                    dto.NroPrestacoes = decimal.Parse(dr[7].ToString() == "" ? "0" : dr[7].ToString());
                    dto.Periodicidade = int.Parse(dr[8].ToString() == "" ? "0" : dr[8].ToString());
                    dto.DescontoFinaceiro = decimal.Parse(dr[9].ToString() == "" ? "0" : dr[9].ToString());
                    dto.Status = dto.Estado;
                    listaCondicaoPagamento.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new CondicaoPagamentoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaCondicaoPagamento = new List<CondicaoPagamentoDTO>();
                listaCondicaoPagamento.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaCondicaoPagamento;
        }

        public CondicaoPagamentoDTO ObterPorPK(CondicaoPagamentoDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_COM_CONDICAO_PAGAMENTO_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new CondicaoPagamentoDTO();
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

                if (dr.Read())
                {
                    
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Pagamento = dr[4].ToString() == "" ? "AP" : dr[4].ToString();
                    dto.Vencimento = int.Parse(dr[5].ToString() == "" ? "0" : dr[5].ToString());
                    dto.EntradaInicial = decimal.Parse(dr[6].ToString() == "" ? "0" : dr[6].ToString());
                    dto.NroPrestacoes = decimal.Parse(dr[7].ToString() == "" ? "0" : dr[7].ToString());
                    dto.Periodicidade = int.Parse(dr[8].ToString() == "" ? "0" : dr[8].ToString());
                    dto.DescontoFinaceiro = decimal.Parse(dr[9].ToString() == "" ? "0" : dr[9].ToString());
                }
            }
            catch (Exception ex)
            {
              
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }


    }
}
