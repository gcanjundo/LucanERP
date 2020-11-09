using Dominio.Comercial; 
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial
{
    public class ReciboDAO
    {
        readonly ConexaoDB BaseDados;

        public ReciboDAO()
        {
            BaseDados = new ConexaoDB();
        }
        public ReciboDTO Adicionar(ReciboDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_RECIBO_CLIENTE_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@SERIE", dto.Serie);
                BaseDados.AddParameter("@CLIENTE", dto.Entidade);
                BaseDados.AddParameter("@EMISSAO", dto.Emissao); 
                BaseDados.AddParameter("@NUMERACAO", dto.Numeracao);
                BaseDados.AddParameter("@REFERENCIA", dto.Referencia);
                BaseDados.AddParameter("@MOEDA", dto.Moeda);
                BaseDados.AddParameter("@CAMBIO", dto.Cambio); 
                BaseDados.AddParameter("@VALOR_TOTAL", dto.ValorDocumento);
                BaseDados.AddParameter("@VALOR_PAGO", dto.ValorPago); 
                BaseDados.AddParameter("@NOME_CLIENTE", dto.CustomerName);
                BaseDados.AddParameter("@CONTRIBUINTE", dto.CustomerVAT); 
                BaseDados.AddParameter("@OBSERVACOES", dto.Observacoes);
                BaseDados.AddParameter("@ANULADO", dto.DocumentStatus);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@DOCUMENT_FROM_ID", dto.DocFromNumber <= 0 ? (object)DBNull.Value : dto.DocFromNumber);
                BaseDados.AddParameter("@DESCONTO", dto.DescontoComercial);
                BaseDados.AddParameter("@DESCONTO_NUMERARIO", dto.DescontoNumerario);
                BaseDados.AddParameter("@TAX_ID", dto.TaxID <=0 ? (object)DBNull.Value : dto.TaxID);
                BaseDados.AddParameter("@VALOR_EXCESSO", dto.ValorExcesso);

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
                if(dto.Codigo > 0)
                {
                    dto = ObterPorPK(dto);
                }
            }

            return dto;
        }

        public ReciboDTO Excluir(ReciboDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_RECIBO_CLIENTE_ANULAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@MOTIVO", dto.Observacoes);

                BaseDados.ExecuteNonQuery();
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

        public ReciboDTO ObterPorPK(ReciboDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_RECIBO_CLIENTE_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new ReciboDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Filial = dr[1].ToString();
                    dto.Documento = int.Parse(dr[2].ToString());
                    dto.Serie = int.Parse(dr[3].ToString() == null ? "-1" : dr[3].ToString());
                    dto.Entidade = int.Parse(dr[4].ToString());
                    dto.Emissao = DateTime.Parse(dr[5].ToString()); 
                    dto.Numeracao = int.Parse(dr[6].ToString() == string.Empty ? "0": dr[6].ToString());
                    dto.Referencia = dr[7].ToString();
                    dto.Moeda = dr[8].ToString();
                    dto.Cambio = decimal.Parse(dr[9].ToString());
                    dto.ValorDocumento = decimal.Parse(dr[10].ToString());
                    dto.ValorPago = decimal.Parse(dr[11].ToString());   
                    dto.CustomerName = dr[12].ToString();
                    dto.CustomerVAT = dr[13].ToString();
                    dto.Observacoes = dr[14].ToString();
                    dto.DocumentStatus = dr[15].ToString() == "" ? short.Parse("1") : short.Parse(dr[15].ToString());
                    dto.MotivoAnulacao = dr[16].ToString();
                    dto.DescontoComercial = decimal.Parse(dr[25].ToString() == string.Empty ? "0" : dr[25].ToString());
                    dto.DescontoNumerario = decimal.Parse(dr[26].ToString() == string.Empty ? "0" : dr[26].ToString());
                    dto.TaxID = int.Parse(dr[27].ToString() == string.Empty ? "0" : dr[27].ToString());
                    dto.ValorExcesso = decimal.Parse(dr[28].ToString() == string.Empty ? "0" : dr[28].ToString());
                    dto.TituloDocumento = dr["DOC_DESCRICAO"].ToString();
                    dto.CompanyAddress = dr["MORADA"].ToString()+"TEL.: "+ dr["ENT_TELEFONE"].ToString()+"<br/>NIF: "+ dr["ENT_IDENTIFICACAO"].ToString();
                    dto.OriginalDocumnetReference = dr["ORGINAL_DOC_REFERENCE"].ToString();

                   

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
                ReciboDocumentosDTO documento = new ReciboDocumentosDTO();
                documento.ReciboID = dto.Codigo;
                var DocumentsList = ObterDocumentos(documento);
                dto.DocumentosLiquidados = DocumentsList.Item1;
                dto.FaturasLiquidadas = DocumentsList.Item2;
            }

            return dto;
        }

         

        public List<ReciboDTO> ObterPorFiltro(ReciboDTO dto)
        {
            List<ReciboDTO> lista = new List<ReciboDTO>();
            try
            {
                
                BaseDados.ComandText = "stp_COM_RECIBO_CLIENTE_OBTERPORFILTRO";

                BaseDados.AddParameter("@FILIAL", dto.Filial);

                if (dto.EmissaoIni == DateTime.MinValue)
                    BaseDados.AddParameter("@EMISSAO_INI", DBNull.Value);
                else
                   BaseDados.AddParameter("@EMISSAO_INI", dto.EmissaoIni);


                if (dto.EmissaoTerm == DateTime.MinValue)
                    BaseDados.AddParameter("@EMISSAO_TERM", DBNull.Value);
                else
                    BaseDados.AddParameter("@EMISSAO_TERM", dto.EmissaoTerm);
                
                   BaseDados.AddParameter("@REFERENCIA", dto.Referencia);
                   BaseDados.AddParameter("@CLIENTE", dto.CustomerName);
                   BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                   BaseDados.AddParameter("@SITUACAO", dto.Status);
                   BaseDados.AddParameter("@ENTIDADE", dto.Entidade);
                   BaseDados.AddParameter("@TIPO", "A");
                   BaseDados.AddParameter("@SERIE", -1);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ReciboDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Referencia = dr[1].ToString();
                    dto.Emissao = DateTime.Parse(dr[2].ToString());
                    dto.TituloDocumento = dr[3].ToString();
                    dto.CustomerName = dr[4].ToString();
                    dto.ValorDocumento = decimal.Parse(dr[5].ToString());
                    dto.ValorPago = decimal.Parse(dr[6].ToString()) == 0 ? decimal.Parse(dr[7].ToString()) : decimal.Parse(dr[6].ToString());                    

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



        public ReciboDocumentosDTO LiquidarDocumento(ReciboDocumentosDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_RECIBO_CLIENTE_DOCUMENTO_LIQUIDAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ORDEM", dto.Ordem);
                BaseDados.AddParameter("@RECIBO", dto.ReciboID);
                BaseDados.AddParameter("@DOCUMENTO_TYPE", dto.Documento);
                BaseDados.AddParameter("@DOCUMENTO_ID", dto.DocumentID); 
                BaseDados.AddParameter("@NUMERACAO", dto.Numeracao);
                BaseDados.AddParameter("@REFERENCIA", dto.Referencia);
                BaseDados.AddParameter("@VALOR", dto.ValorTotal);
                BaseDados.AddParameter("@LIQUIDADO", dto.ValorPago);
                BaseDados.AddParameter("@PENDENTE", dto.Saldo);
                BaseDados.AddParameter("@DESCONTO", dto.DescontoEntidade);
                BaseDados.AddParameter("@JUROS", dto.Troco);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@DOCUMENT_STATUS", dto.Activo == true ? 1 : 0);

                BaseDados.ExecuteNonQuery();
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

        public Tuple<List<ReciboDocumentosDTO>, List<FaturaDTO>> ObterDocumentos(ReciboDocumentosDTO pRecibo)
        {
            List<ReciboDocumentosDTO> documentosLiquidados = new List<ReciboDocumentosDTO>();
            List<FaturaDTO> faturasLiquidadas = new List<FaturaDTO>();

            ReciboDocumentosDTO dto;
            try
            {

                BaseDados.ComandText = "stp_COM_CLIENTE_DOCUMENTOS_OBTERPORFILTRO";
                BaseDados.AddParameter("@DOCUMENTO", pRecibo.ReciboID);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    var _fatura = new FaturaDTO();
                    dto = new ReciboDocumentosDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Referencia = dr[1].ToString();
                    dto.Emissao = DateTime.Parse(dr[2].ToString());
                    dto.DescricaoDocumento = dr[3].ToString();
                    dto.ValorTotal = decimal.Parse(dr[4].ToString());
                    dto.ValorPago = decimal.Parse(dr[5].ToString());
                    dto.Saldo = decimal.Parse(dr[6].ToString());
                    dto.StatusDocumento = int.Parse(dr[7].ToString());
                    dto.StatusPagamento = dr[8].ToString();
                    dto.Saldo = dto.ValorTotal - dto.ValorPago;
                    dto.ValorRetencao = decimal.Parse(dr[9].ToString());
                    dto.TotalImpostos = decimal.Parse(dr[10].ToString());

                    documentosLiquidados.Add(dto);

                    _fatura.Codigo = dto.Codigo;
                    _fatura.Referencia = dto.Referencia;
                    _fatura.Emissao = dto.Emissao;
                    _fatura.DescricaoDocumento = dto.DescricaoDocumento;
                    _fatura.ValorTotal = dto.ValorTotal;
                    _fatura.ValorPago = dto.ValorTotal;
                    _fatura.ValorFaturado = dto.ValorPago;
                    _fatura.Saldo = dto.Saldo;
                    _fatura.StatusDocumento = dto.StatusDocumento;
                    _fatura.StatusPagamento = dto.StatusPagamento;
                    if (dto.Saldo == 0)
                    {
                        _fatura.Saldo = dto.ValorTotal - dto.ValorPago;
                    }
                    _fatura.ValorRetencao = dto.ValorRetencao;
                    _fatura.TotalImpostos =  dto.TotalImpostos;

                    faturasLiquidadas.Add(_fatura);

                }

            }
            catch (Exception ex)
            {
                dto = new ReciboDocumentosDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return new Tuple<List<ReciboDocumentosDTO>, List<FaturaDTO>>(documentosLiquidados, faturasLiquidadas);
        }

       
    }
}
