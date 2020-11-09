
using Dominio.Comercial;
using Dominio.Tesouraria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Tesouraria
{
    public class MovimentoDAO : ConexaoDB
    {
        public MovimentoDTO Adicionar(MovimentoDTO dto)
        {
            
            try
            {
                ComandText = "stp_FIN_MOVIMENTO_ADICIONAR";  

                AddParameter("@DATA_MOVIMENTO", dto.DataLancamento);
                AddParameter("@DATA_TRANSACAO", dto.DataTransacao);
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@MOVIMENTO", dto.Movimento);
                AddParameter("@PAGAMENTO", dto.MetodoPagamento);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@DESCRITIVO", dto.Descritivo); 
                AddParameter("@FLUXO_CAIXA", dto.FluxoCaixa);
                AddParameter("@CONTA", dto.ContaCorrente);
                AddParameter("@ENTIDADE", dto.Entidade <=0 ? (object)DBNull.Value : dto.Entidade);
                AddParameter("@VALOR", dto.Valor);
                AddParameter("@OBS", dto.Observacoes == null ? string.Empty : dto.Observacoes);
                AddParameter("@FATURA", dto.Documento);
                AddParameter("@POS", dto.Terminal);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@DOC_PAGTO", dto.RefComprovantePagto);
                AddParameter("@DOCUMENT_TYPE", dto.DocumentType);
                AddParameter("@DOCUMENT_ID", dto.DocumentID);
                AddParameter("@SERIE_ID", dto.SerieID);
                AddParameter("@REALIZADO", dto.IsReal ? 1 : 0);
                AddParameter("@MOVIMENTO_PERIODICO_ID", dto.PeriodicoID);
                AddParameter("@ACCOUNT_ORIGEM_ID", dto.ContaTransferenciaOrigem?? (object)DBNull.Value);
                AddParameter("@ACCOUNT_DESTINO_ID", dto.ContaTransferenciaDestino ?? (object)DBNull.Value);


                ExecuteNonQuery();
                dto.Sucesso = true;
                dto.MensagemErro = string.Empty; 
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

        public MovimentoDTO Anular(MovimentoDTO dto)
        {

            try
            {
                ComandText = "stp_FIN_MOVIMENTO_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@MOTIVO", dto.Descritivo);
                ExecuteNonQuery();
                dto.Sucesso = true;
                dto.MensagemErro = "alert('Lançamento de Caixa Anulado com Sucesso')";
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



        public List<MovimentoDTO> ObterDadosPagamento(MovimentoDTO dto)
        {

            List<MovimentoDTO> lista = new List<MovimentoDTO>();
            try
            {
                ComandText = "stp_FIN_MOVIMENTO_GET_DOCUMENT_PAYMENTS";
                AddParameter("@DOC_ID", dto.Documento);
                AddParameter("@DOC_TYPE", dto.DocumentType);

                MySqlDataReader dr = ExecuteReader();
                while (dr.Read())
                {
                    dto = new MovimentoDTO
                    {
                        DataTransacao = DateTime.Parse(dr["MOV_DATA"].ToString()),
                        Descritivo = dr["PAG_DESCRICAO"].ToString(),
                        ContaCorrente = dr["BANC_SIGLA"].ToString().Replace("LOCAL", string.Empty),
                        RefComprovantePagto = dr["MOV_NUM_DOC_PAGTO"].ToString(),
                        Valor = decimal.Parse(dr["MOV_VALOR"].ToString())
                    };
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MovimentoDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public MovimentoDTO ObterPorPK(MovimentoDTO dto)
        { 

            try
            {
                ComandText = "stp_FIN_MOVIMENTO_CAIXA_OBTERPORPK";

                AddParameter("@CODIGO", dto.Codigo);  
                
                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.ContaCorrente = dr[1].ToString();
                    dto.DataTransacao = Convert.ToDateTime(dr[2].ToString());
                    dto.DataLancamento = Convert.ToDateTime(dr[3].ToString());
                    dto.Moeda = int.Parse(dr[4].ToString());
                    dto.Movimento = dr[5].ToString();
                    dto.MetodoPagamento = int.Parse(dr[6].ToString());
                    dto.Utilizador = dr[7].ToString();
                    dto.Filial = dr[8].ToString();
                    dto.Descritivo = dr[9].ToString();
                    dto.Rubrica = dr[10].ToString() == string.Empty ? "-1" : dr[10].ToString();
                    dto.Valor = decimal.Parse(dr[11].ToString());
                    dto.Entidade = int.Parse(dr[12].ToString() == string.Empty ? "-1" : dr[12].ToString());
                    dto.Observacoes = dr[13].ToString();
                    dto.RefComprovantePagto = dr[14].ToString();
                    dto.Status = int.Parse(dr[15].ToString());
                    dto.DocumentType = int.Parse(dr[16].ToString());
                    dto.DocumentID = int.Parse(dr[17].ToString());
                    dto.SerieID = int.Parse(dr[18].ToString());
                    dto.IsConciled = dr[19].ToString() != "1" ? false : true;
                    dto.DocumentReference = dr[21].ToString();
                    dto.NumeroMovimento = int.Parse(dr[22].ToString() == string.Empty ? "-1": dr[22].ToString());
                    dto.ContaTransferenciaOrigem = dr[30].ToString() == string.Empty ? "-1" : dr[30].ToString();
                    dto.ContaTransferenciaDestino = dr[31].ToString() == string.Empty ? "-1" : dr[31].ToString();
                    dto.FluxoCaixa = int.Parse(dr[32].ToString() == string.Empty ? "-1" : dr[32].ToString());
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

        public List<MovimentoDTO> ObterPorFiltro(MovimentoDTO dto)
        {
             
            ComandText = "stp_FIN_MOVIMENTO_OBTERPORFILTRO";


            List<MovimentoDTO> lista = new List<MovimentoDTO>();
            try
            {
             
                if (dto.DataIni != null && !dto.DataIni.Equals(""))
                    AddParameter("@DATA_MOV_INI", dto.DataIni);
                else
                    AddParameter("@DATA_MOV_INI", DBNull.Value);

                if (dto.DataTerm != null && !dto.DataTerm.Equals(""))
                    AddParameter("@DATA_MOV_TERM", dto.DataTerm);
                else
                    AddParameter("@DATA_MOV_TERM", DBNull.Value); 
                AddParameter("@NATUREZA", dto.Movimento == "-1" ? string.Empty : dto.Movimento);
                AddParameter("@DESCRICAO", dto.Descritivo);
                AddParameter("@COMPROVANTE", dto.RefComprovantePagto);
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@BANCO", dto.Entidade);
                AddParameter("@CONTA", dto.ContaCorrente);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@FILTRO", dto.Observacoes);
                 
                MySqlDataReader dr = ExecuteReader();
                decimal saldo = 0;
                while (dr.Read())
                {
                    dto = new MovimentoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.ContaCorrente = dr[1].ToString();
                    dto.DataTransacao = Convert.ToDateTime(dr[2].ToString());
                    dto.DataLancamento = Convert.ToDateTime(dr[3].ToString());
                    dto.Moeda = int.Parse(dr[4].ToString());
                    dto.Movimento = dr[5].ToString();
                    dto.MetodoPagamento = int.Parse(dr[6].ToString());
                    dto.Utilizador = dr[7].ToString();
                    dto.Filial = dr[8].ToString();
                    dto.Descritivo = dr[9].ToString();
                    dto.Rubrica = dr[10].ToString() == string.Empty ? "-1" : dr[10].ToString();
                    dto.Valor = dto.Movimento == "S" ? -decimal.Parse(dr[11].ToString()) : decimal.Parse(dr[11].ToString());
                    dto.Entidade = int.Parse(dr[12].ToString() == string.Empty ? "-1" : dr[12].ToString());
                    dto.Observacoes = dr[13].ToString();
                    dto.RefComprovantePagto = dr[14].ToString();
                    dto.Status = int.Parse(dr[15].ToString());
                    dto.DocumentType = int.Parse(dr[16].ToString());
                    dto.DocumentID = int.Parse(dr[17].ToString());
                    dto.SerieID = int.Parse(dr[18].ToString() == string.Empty ? "-1" : dr[18].ToString());
                    dto.IsConciled = dr[19].ToString() != "1" ? false : true;
                    dto.DocumentReference = dr[21].ToString();
                    dto.NumeroMovimento = int.Parse(dr[22].ToString() == string.Empty ? "-1" : dr[22].ToString());
                    dto.PaymentMethodDesignation = dr[26].ToString();
                    dto.TituloDocumento = dr[28].ToString();
                    dto.FluxoCaixa = int.Parse(dr[30].ToString() == string.Empty ? dto.Rubrica : dr[30].ToString());
                    dto.Rubrica = dto.FluxoCaixa > 0 && dto.FluxoCaixa == int.Parse(dto.Rubrica) ? "-1" : dto.Rubrica;

                    if (dto.FluxoCaixa == -1)
                        dto.FluxoCaixa = 1;

                    saldo += dto.Valor;
                    dto.Saldo = saldo;
                    
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<MovimentoDTO>();
                dto.Sucesso = false;
                dto.Descritivo = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

       

        public List<MovimentoDTO> ObterRecebimentoCliente(MovimentoDTO dto)
        { 

            ComandText = "stp_FIN_MOVIMENTO_CUSTOMER_RECEIPTS_RESUME";

            List<MovimentoDTO> lista = new List<MovimentoDTO>();
            try
            {
                AddParameter("YEAR_FROM", DateTime.Parse(dto.DataIni).Year);
                AddParameter("YEAR_UNTIL", DateTime.Parse(dto.DataTerm).Year);
                AddParameter("FILIAL", dto.Filial);
                AddParameter("ENTITY_ID", dto.Entidade);
                AddParameter("ACCOUNT_ID", dto.ContaCorrente);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new MovimentoDTO
                    {
                        NroOrdenacao = int.Parse(dr[0].ToString()),
                        LookupNumericField1 = int.Parse(dr[1].ToString()),
                        Valor = decimal.Parse(dr[2].ToString())
                    };
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<MovimentoDTO>();
                dto.Sucesso = false;
                dto.Descritivo = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista.OrderBy(t=>t.NroOrdenacao).ToList();
        }

        public List<MovimentoDTO> ObterRecebimentoDiario(MovimentoDTO dto)
        {
            
            ComandText = "stp_FIN_MOVIMENTO_CUSTOMER_RECEIPTS_DAILY";

            List<MovimentoDTO> lista = new List<MovimentoDTO>();
            try
            {
                AddParameter("PERIODO_INICIO", DateTime.Parse(dto.DataIni));
                AddParameter("PERIODO_FIM", DateTime.Parse(dto.DataTerm));
                AddParameter("FILTER", dto.Movimento ?? string.Empty);
                AddParameter("FILIAL", dto.Filial);
                AddParameter("ENTITY_ID", dto.Entidade);
                AddParameter("ACCOUNT_ID", dto.ContaCorrente);
                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new MovimentoDTO
                    { 
                        DataTransacao = DateTime.Parse(dr[0].ToString()),
                        Valor = decimal.Parse(dr[1].ToString())
                    };
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                lista = new List<MovimentoDTO>();
                dto.Sucesso = false;
                dto.Descritivo = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<MetodoPagamentoDTO> ObterResumoRecebimentoPorPaymentMethod(MovimentoDTO dto)
        {
            
            ComandText = "stp_FIN_MOVIMENTO_RESUMO_PORMEIOPAGAMENTO";

            List<MetodoPagamentoDTO> lista = new List<MetodoPagamentoDTO>();
            try
            {
                AddParameter("INICIO", DateTime.Parse(dto.DataIni));
                AddParameter("TERMINO", DateTime.Parse(dto.DataTerm));
                AddParameter("FILIAL", dto.Filial);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("WAREHOUSE_ID", dto.LookupNumericField1);
                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    MetodoPagamentoDTO PaymentMethod = new MetodoPagamentoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        TotalLinha = decimal.Parse(dr[2].ToString())
                    };
                    lista.Add(PaymentMethod);
                }
            }
            catch (Exception ex)
            {

                var PaymentMethod = new MetodoPagamentoDTO
                {
                    Sucesso = false,
                    Descricao = ex.Message.Replace("'", "")
                };


                lista = new List<MetodoPagamentoDTO>()
                {
                    PaymentMethod
                };
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }



        public MovimentoDTO AddMovimentoPeriodico(MovimentoDTO dto)
        {

            try
            {
                ComandText = "stp_FIN_MOVIMENTO_PERIODICO_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@DESCRICAO", dto.Descritivo);
                AddParameter("@INICIO", dto.DataLancamento);
                AddParameter("@TERMINO", dto.DataTransacao);
                AddParameter("@PERIODICIDADE", dto.Periodicidade);
                AddParameter("@DIAS", dto.Dias);
                AddParameter("@NATUREZA", dto.Movimento);
                AddParameter("@ENTIDADE", dto.Entidade <= 0 ? (object)DBNull.Value : dto.Entidade); 
                AddParameter("@RUBRICA", dto.FluxoCaixa);  
                AddParameter("@VALOR", dto.Valor); 
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@CAMBIO", dto.Cambio);
                AddParameter("@DOCUMENT_ID", dto.DocumentID);
                AddParameter("@ACCOUNT_ID", dto.ContaCorrente); 
                AddParameter("@ESTADO", dto.Status); 
                AddParameter("@UTILIZADOR", dto.Utilizador);

                ExecuteNonQuery();
                dto.Sucesso = true;
                dto.MensagemErro = string.Empty;
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

        public List<MovimentoDTO> ObterPeriodicos(MovimentoDTO dto)
        {
            List<MovimentoDTO> lista = new List<MovimentoDTO>();
            try
            {
                ComandText = "stp_FIN_MOVIMENTOS_PERIODICOS_OBTERPORFILTRO";
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@ENTIDADE", dto.Entidade);
                AddParameter("@DESCRICAO", dto.Descritivo);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new MovimentoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descritivo = dr[1].ToString();
                    dto.DataTransacao = Convert.ToDateTime(dr[2].ToString());
                    dto.DataLancamento = Convert.ToDateTime(dr[3].ToString());
                    dto.Periodicidade = int.Parse(dr[4].ToString());
                    dto.Dias = int.Parse(dr[5].ToString());
                    dto.Movimento = dr[6].ToString();
                    dto.Entidade = int.Parse(dr[7].ToString() == string.Empty ? "-1" : dr[7].ToString());
                    dto.Rubrica = dr[8].ToString();
                    dto.Valor = decimal.Parse(dr[9].ToString());
                    dto.Moeda = int.Parse(dr[10].ToString());
                    dto.Cambio = decimal.Parse(dr[11].ToString());
                    dto.DocumentID = int.Parse(dr[12].ToString());
                    dto.ContaCorrente = dr[13].ToString();
                    dto.Status = int.Parse(dr[14].ToString());
                    dto.CreatedBy = dr[15].ToString();
                    dto.CreatedDate = Convert.ToDateTime(dr[16].ToString());
                    dto.UpdatedBy = dr[17].ToString();
                    dto.UpdatedDate = Convert.ToDateTime(dr[18].ToString());
                    dto.DesignacaoEntidade = dr[21].ToString();

                    lista.Add(dto);
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
            return lista;
        }

    }
}
