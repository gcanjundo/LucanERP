using DataAccessLayer.Geral;
using Dominio.Comercial;
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Comercial
{
    public class FaturaDAO : ConexaoDB
    {
        public FaturaDTO Adicionar(FaturaDTO dto)
        {
            try
            { 

                ComandText = "stp_COM_FATURA_CLIENTE_ADICIONAR";  
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@ARMAZEM", dto.Armazem);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SERIE", dto.Serie);
                AddParameter("@CLIENTE", dto.Entidade);
                AddParameter("@NOME_CLIENTE", dto.NomeEntidade);
                AddParameter("@EMISSAO", dto.Emissao == DateTime.MinValue ? DateTime.Today : dto.Emissao);
                AddParameter("@VALIDADE", dto.Validade);
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@CAMBIO", dto.Cambio);
                AddParameter("@PRAZO", dto.PrazoPagto);
                AddParameter("@EXPEDICAO", dto.Expedicao);
                AddParameter("@DESCONTO", dto.DescontoFinanceiro); // Valor Percentual Descontos Financeiros
                AddParameter("@STATUS_DOCUMENT", dto.StatusDocumento);
                AddParameter("@NUMERACAO", dto.Numeracao == null ? 0 : dto.Numeracao);
                AddParameter("@REFERENCIA", dto.Referencia == null? string.Empty : dto.Referencia);
                AddParameter("@VALOR_BRUTO", dto.TotalIliquido);
                AddParameter("@TOTAL_DESCONTOS", dto.TotalDescontos);
                AddParameter("@TOTAL_LIQUIDO", dto.TotalLiquido);
                AddParameter("@TOTAL_IMPOSTOS", dto.TotalImpostos);
                AddParameter("@VALOR_TOTAL", dto.ValorTotal);
                AddParameter("@STATUS_PAGTO", dto.StatusPagamento);
                AddParameter("@OBSERVACOES", dto.Observacoes);
                AddParameter("@DOCUMENTO_ORIGEM", dto.DocumentoOrigem);
                AddParameter("@NUMERO_DOC_ORIGEM", dto.NumeroDocOrigem == null || dto.NumeroDocOrigem == "" ? "0" : dto.NumeroDocOrigem);
                AddParameter("@VALOR_PAGO", dto.ValorPago);
                AddParameter("@SALDO", dto.Saldo);
                AddParameter("@TROCO", dto.Troco);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@DOCUMENTO_ACTIVO", dto.Activo == true ? 1 : 0);
                AddParameter("@DESCONTO_CLIENTE", dto.DescontoEntidade); // Valor Percentual Descontos de Clientes

                if (dto.DataCarga == DateTime.MinValue)
                    AddParameter("@DATA_CARGA", DBNull.Value);
                else
                    AddParameter("@DATA_CARGA", dto.DataCarga);
                AddParameter("@ENDERECO_CARGA", dto.EnderecoCarga);
                AddParameter("@LOCAL_CARGA", dto.LocalCarga);
                if (dto.DataDescarga == DateTime.MinValue)
                    AddParameter("@DATA_DESCARGA", DBNull.Value);
                else
                    AddParameter("@DATA_DESCARGA", dto.DataDescarga);
                AddParameter("@ENDERECO_DESCARGA", dto.EnderecoDescarga);
                AddParameter("@LOCAL_DESCARGA", dto.LocalDescarga);
                AddParameter("@NOTAS_INTERNAS", dto.NotasInternas); 
                AddParameter("@MOTIVO_ANULACAO", dto.MotivoAnulacao);
                AddParameter("@PARCELA", dto.Parcela == 0 ? 1 : dto.Parcela);
                if (dto.VendedorID > 0)
                {
                    AddParameter("@VENDEDOR", dto.VendedorID);
                }
                else
                {
                    AddParameter("@VENDEDOR", DBNull.Value);
                }
                AddParameter("@COMISSAO", dto.Comissao);
                AddParameter("@BARCODE", dto.DocumentBarcode);
                AddParameter("@PREVISAO_ENTREGA", dto.PrevisaoEntrega == null || (DateTime)dto.PrevisaoEntrega == DateTime.MinValue ? (object)DBNull.Value : dto.PrevisaoEntrega);
                AddParameter("@DATA_LIQUIDACAO", dto.DataLiquidacao == null || (DateTime)dto.PrevisaoEntrega == DateTime.MinValue ? (object)DBNull.Value : dto.DataLiquidacao);
                AddParameter("@LOADER_MAN", dto.ResponsavelCarregamento == string.Empty ? "-1" : dto.ResponsavelCarregamento);
                AddParameter("@RECEPTOR", dto.Destinatario);
                AddParameter("@DELIVERY_MAN", dto.DeliveryMan == string.Empty ? "-1" : dto.DeliveryMan);
                AddParameter("@RETENCAO", dto.ValorRetencao); 
                AddParameter("@BILLING_ID", dto.EntityBillingID == 0 ? (object)DBNull.Value : dto.EntityBillingID);
                AddParameter("@NOTAS_COMERCIAIS", dto.NotasComerciais);
                AddParameter("@TEL_DESTINO_1", dto.ContactoDestinatario);
                AddParameter("@TEL_DESTINO_2", dto.LookupField1);
                AddParameter("@MATRICULA", dto.Matricula);
                AddParameter("@CUSTOMER_CREDIT", 0);
                AddParameter("@TITULO", dto.HeaderNotes);
                AddParameter("@VALOR_UTENTE", dto.TotalValorUtente);
                AddParameter("@VALOR_ENTIDADE", dto.TotalValorEntidade);

                dto.Codigo = ExecuteInsert();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if (dto.Codigo > 0)
                {
                    AddOtherInfo(dto);
                   dto = ObterPorPK(dto);
                }
                else
                {
                    dto.Sucesso = false;
                }
            }

            return dto;
        }

        public FaturaDTO SetOrderNumber(FaturaDTO dto)
        {
            try
            {

                ComandText = "stp_COM_FATURA_CLIENTE_SETREFERENCE";

                AddParameter("@CODIGO", dto.Codigo); 
                AddParameter("@NUMERO_DOC_ORIGEM", dto.NumeroDocOrigem == null || dto.NumeroDocOrigem == "" ? "0" : dto.NumeroDocOrigem);
                AddParameter("@MOTIVO_ANULACAO", dto.MotivoAnulacao);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@VALOR_TOTAL", dto.ValorTotal);

                ExecuteNonQuery();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);

            }
            finally
            {
                FecharConexao();
                dto = ObterPorPK(dto);
            }

            return dto;
        }

        public void SaveHashAssign(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_SAVEHASH";

                AddParameter("@DOCUMENT_ID", dto.Codigo); 
                AddParameter("@HASH_ID", dto.Hash);
                AddParameter("@PRIOR_HASH", dto.PriorHash);

                ExecuteNonQuery();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
        }

        public FaturaDTO Excluir(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@MOTIVO", dto.MotivoAnulacao);
                ExecuteNonQuery();
                dto.Sucesso = true;

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

        public FaturaDTO Abortar(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_CANCELAR";

                AddParameter("@CODIGO", dto.Codigo); 
                ExecuteNonQuery();
                dto.Sucesso = true;

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

       

        public decimal TotalEntradas(FaturaDTO dto)
        {
            decimal total = 0;
            try
            {
                ComandText = "stp_FIN_RESUMO_CAIXA_ENTRADA";
                AddParameter("@MEIO_PAGAMENTO", dto.PrazoPagto);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@FILIAL", dto.Filial);
                if(dto.Codigo > 0)
                {
                    AddParameter("@INICIO", DBNull.Value);
                    AddParameter("@TERMINO", DBNull.Value);
                    AddParameter("@SESSAO", dto.Codigo);
                }
                else
                {
                    AddParameter("@INICIO", dto.Inicio);
                    AddParameter("@TERMINO", dto.Termino);
                    AddParameter("@SESSAO", -1);
                }

                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    total = Convert.ToDecimal(dr[0].ToString());

                }
                else
                {
                    total = 0;
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;

                total = 0;
            }
            finally
            {
                FecharConexao();
            }
            return total;
        }

        public decimal TotalSaidas(FaturaDTO dto)
        {
            decimal total = 0;
            try
            {
                ComandText = "stp_FIN_RESUMO_CAIXA_SAIDA";
                AddParameter("@MEIO_PAGAMENTO", dto.PrazoPagto);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@FILIAL", dto.Filial);
                if (dto.Codigo > 0)
                {
                    AddParameter("@INICIO", DBNull.Value);
                    AddParameter("@TERMINO", DBNull.Value);
                    AddParameter("@SESSAO", dto.Codigo);
                }
                else
                {
                    AddParameter("@INICIO", dto.Inicio);
                    AddParameter("@TERMINO", dto.Termino);
                    AddParameter("@SESSAO", -1);
                }

                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    total = Convert.ToDecimal(dr[0].ToString());
                }
                else
                {
                    total = 0;
                }

            }
            catch(Exception ex)  
            {
                dto.MensagemErro = ex.Message;
                total = 0;
            }
            finally
            {
                FecharConexao();
            }
            return total;
        }

        public List<FaturaDTO> MapaGlobalIVA(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_IVA_GLOBAL";

                AddParameter("@DATA_INI", dto.EmissaoIni);
                AddParameter("@DATA_TERM", dto.EmissaoTerm);
                AddParameter("@COMPANY_ID", dto.Filial); 
                AddParameter("@FISCAL_YEAR", dto.FiscalYear);


                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new FaturaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.Referencia = dr[1].ToString();
                    dto.ValorRetencao = decimal.Parse(dr[2].ToString());
                    dto.ValorIncidencia = decimal.Parse(dr[3].ToString());
                    dto.TotalImpostos = decimal.Parse(dr[4].ToString());

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public FaturaDTO ObterPorPK(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_OBTERPORPK";

                AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new FaturaDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Filial = dr[1].ToString();
                    dto.Armazem = int.Parse(dr[2].ToString() ?? "-1");
                    dto.Documento = int.Parse(dr[3].ToString());
                    dto.Serie = int.Parse(dr[4].ToString() ?? "-1");
                    dto.Entidade = int.Parse(dr[5].ToString());
                    dto.Emissao = DateTime.Parse(dr[6].ToString());
                    dto.Validade = DateTime.Parse(dr[7].ToString());
                    dto.Moeda = int.Parse(dr[8].ToString());
                    dto.Cambio = decimal.Parse(dr[9].ToString());
                    dto.PrazoPagto = int.Parse(string.IsNullOrEmpty(dr[10].ToString()) ? "-1": dr[10].ToString());
                    dto.Expedicao = int.Parse(string.IsNullOrEmpty(dr[11].ToString()) ? "-1" : dr[11].ToString());
                    dto.Desconto = decimal.Parse(dr[12].ToString());
                    dto.Numeracao = int.Parse(dr[13].ToString() == string.Empty ? "0": dr[13].ToString());
                    dto.Referencia = dr[14].ToString();
                    dto.StatusDocumento = int.Parse(dr[15].ToString());
                    dto.TotalIliquido = decimal.Parse(dr[16].ToString()); // Valor Bruto
                    dto.TotalDescontos = decimal.Parse(dr[17].ToString());
                    dto.TotalImpostos = decimal.Parse(dr[18].ToString());
                    dto.ValorTotal = decimal.Parse(dr[19].ToString());
                    dto.StatusPagamento = dr[20].ToString();
                    dto.Observacoes = dr[21].ToString();
                    dto.DocumentoOrigem = int.Parse(dr[22].ToString() == string.Empty ? "-1" : dr[22].ToString());
                    dto.NumeroDocOrigem = dr[23].ToString();
                    dto.ValorPago = decimal.Parse(dr[24].ToString());
                    dto.Saldo = decimal.Parse(dr[25].ToString());
                    dto.Troco = decimal.Parse(dr[26].ToString());
                    dto.NomeEntidade = dr[27].ToString();
                    dto.DescontoEntidade = decimal.Parse(dr[28].ToString() == string.Empty ? "0" : dr[28].ToString());
                    dto.DataCarga = DateTime.Parse(dr[29].ToString() == string.Empty || dr[29].ToString() == null ? DateTime.MinValue.ToString() : dr[29].ToString());
                    dto.EnderecoCarga = dr[30].ToString();
                    dto.LocalCarga = dr[31].ToString();
                    dto.DataDescarga = DateTime.Parse(dr[32].ToString() == string.Empty || dr[32].ToString() == null ? DateTime.MinValue.ToString() : dr[32].ToString());
                    dto.EnderecoDescarga = dr[33].ToString();
                    dto.LocalDescarga = dr[34].ToString();
                    dto.EnderecoDescarga = dr[35].ToString();
                    dto.MotivoAnulacao = dr[36].ToString();
                    dto.Activo = dr[37].ToString() == "0" ? false : true;
                    dto.TituloDocumento = dr[38].ToString();
                    dto.StatusDocumento = int.Parse(dr[39].ToString());
                    dto.Parcela = int.Parse(dr[40].ToString());
                    dto.NotasComerciais = dr[41].ToString();
                    dto.ResponsavelCarregamento = dr[42].ToString() == "-1" ? "" : dr[42].ToString();
                    dto.DeliveryMan = dr[43].ToString();
                    dto.Destinatario = dr[44].ToString();
                    dto.ContactoDestinatario = dr[45].ToString();
                    dto.LookupField1 = dr[46].ToString();
                    dto.EntityBillingID = int.Parse(dr[47].ToString() == string.Empty ? "1" : dr[47].ToString());
                    dto.Matricula = dr[48].ToString();
                    dto.DocumentBarcode = dr[49].ToString();
                    dto.VendedorID = int.Parse(dr[50].ToString() =="" ? "-1" : dr[50].ToString());
                    dto.FuncionarioID = dr[51].ToString();
                    dto.SocialName = dr[52].ToString();
                    dto.CreatedDate = DateTime.Parse(dr[53].ToString() == "" ? dr[6].ToString() : dr[53].ToString());
                    dto.NIF = dr["ENT_IDENTIFICACAO"].ToString(); 
                    dto.LookupField2= dr["FAT_CUSTOMER_SENT"].ToString() == "1" ? "2ª VIA" : "ORIGINAL";
                    dto.Hash = dr["FAT_HASH"].ToString();
                    dto.PriorHash = dr["FAT_PRIOR_HASH"].ToString();
                    if (!string.IsNullOrEmpty(dto.Hash))
                        dto.HasFields = dto.Hash[0].ToString() + dto.Hash[10].ToString() + dto.Hash[20].ToString() + dto.Hash[30].ToString() + "-";
                    dto.ValorRetencao = decimal.Parse(dr["FAT_VALOR_RETENCAO"].ToString() != "" ? dr["FAT_VALOR_RETENCAO"].ToString() : "0"); 
                    dto.Saldo = (dto.ValorTotal - dto.ValorRetencao) - (dto.ValorPago - dto.Troco);
                    dto.LookupField3 = dr["MOE_SIGLA"].ToString();
                    dto.FooterDocumentNotes = dr["DOC_COMENTARIO_FIXO"].ToString();
                    dto.HeaderNotes = dr["FAT_TITLE"].ToString();
                    dto.TipoDocumento = dr["DOC_FORMATO"].ToString();
                    dto.TotalValorUtente = decimal.Parse(dr["FAT_VALOR_TOTAL_UTENTE"].ToString() != "" ? dr["FAT_VALOR_TOTAL_UTENTE"].ToString() : "0");
                    dto.TotalValorEntidade = decimal.Parse(dr["FAT_VALOR_TOTAL_ENTIDADE"].ToString() != "" ? dr["FAT_VALOR_TOTAL_ENTIDADE"].ToString() : "0"); 
                    dto.Sucesso = true;
                    dto.IsPaid = dto.StatusPagamento == "3" || (dto.ValorTotal - dto.ValorRetencao) == (dto.ValorPago - dto.Troco) || dto.TipoDocumento == "INVOICE_R" ? true : false;
                    dto.Email = dr["ENT_EMAIL"].ToString();
                    dto.MobileNumber = dr["ENT_TELEFONE"].ToString();
                    dto.CarMark = dr["FAT_MARCA"].ToString();
                    dto.CarModel = dr["FAT_MODELO"].ToString();
                    dto.CarRegistrationID = dr["FAT_MATRICULA"].ToString();
                    dto.CarYear = dr["FAT_ANO"].ToString();
                    dto.CarKM = dr["FAT_LOOKUPFIELD7"].ToString();
                    dto.CarColor = dr["FAT_LOOKUPFIELD8"].ToString();
                    dto.ContractReference = dr["FAT_CONTRATO"].ToString();
                    dto.SESNumber = dr["FAT_SESNUMBER"].ToString();
                    dto.ShipName = dr["FAT_LOOKUPFIELD9"].ToString();
                    dto.ValorFaturado = decimal.Parse(dr["FAT_VALOR_DOCUMENTO_ORIGINAL"].ToString());
                    
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
                dto.Sucesso = false;
            }
            finally
            {
                FecharConexao();
                if (dto.Sucesso || dto.Codigo > 0)
                {
                    dto.CustomerAddress = CustomerInvoiceAddress(dto);
                    ItemFaturacaoDTO item = new ItemFaturacaoDTO();
                    item.Fatura = dto.Codigo;
                    dto.ListaArtigos = new ItemFaturacaoDAO().ObterPorFiltro(item);
                    dto.Parcelas = ObterParcelas(dto);
                    dto.Sucesso = true;
                }
                
            }

            return dto;
        }

         
        public List<FaturaDTO> ObterParcelas(FaturaDTO dto)
        {
            var lista = new List<FaturaDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_OBTERPARCELAS";

                AddParameter("@REFERENCE", dto.Referencia);

                MySqlDataReader dr = ExecuteReader();

                
                while (dr.Read())
                {
                    dto = new FaturaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Parcela = int.Parse(dr[1].ToString());
                    dto.Validade = DateTime.Parse(dr[2].ToString()); 
                    dto.TotalIliquido = decimal.Parse(dr[3].ToString()); // Valor Bruto
                    dto.TotalDescontos = decimal.Parse(dr[4].ToString());
                    dto.TotalImpostos = decimal.Parse(dr[5].ToString());
                    dto.ValorTotal = decimal.Parse(dr[6].ToString());
                    dto.ValorRetencao = decimal.Parse(dr[7].ToString());
                    dto.ParcelaID = dto.Codigo;
                    
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
                dto.Referencia = dto.MensagemErro;
                lista = new List<FaturaDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
                
            }

            return lista;
        }

        public List<FaturaDTO> ObterPorFiltro(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {


                ComandText = "stp_COM_FATURA_CLIENTE_OBTERPORFILTRO";

                AddParameter("@FILIAL", dto.Filial);

                if (dto.EmissaoIni == DateTime.MinValue)
                    AddParameter("@EMISSAO_INI", DBNull.Value);
                else
                    AddParameter("@EMISSAO_INI", DateTime.Parse(dto.EmissaoIni.ToString()));


                if (dto.EmissaoTerm == DateTime.MinValue)
                    AddParameter("@EMISSAO_TERM", DBNull.Value);
                else
                    AddParameter("@EMISSAO_TERM", DateTime.Parse(dto.EmissaoTerm.ToString()));



                if (dto.ValidadeIni == DateTime.MinValue)
                    AddParameter("@VALIDADE_INI", DBNull.Value);
                else
                    AddParameter("@VALIDADE_INI", dto.ValidadeIni);


                if (dto.ValidadeTerm == DateTime.MinValue)
                    AddParameter("@VALIDADE_TERM", DBNull.Value);
                else
                    AddParameter("@VALIDADE_TERM", dto.ValidadeTerm);

                AddParameter("@REFERENCIA", dto.Referencia == null ? string.Empty : dto.Referencia);
                AddParameter("@CLIENTE", dto.NomeEntidade == null ? string.Empty : dto.NomeEntidade);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SITUACAO", dto.Activo == true ? 1 : 0);
                AddParameter("@ENTIDADE", dto.Entidade);
                AddParameter("@DOCUMENT_STATUS", dto.StatusDocumento);
                AddParameter("@PAYMENT_STATUS", dto.StatusPagamento);
                AddParameter("@SERIE", dto.Serie == null ? -1 : dto.Serie);
                AddParameter("@WAREHOUSE_ID", dto.Armazem);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@PRODUCT_ID", dto.LookupNumericField1);
                AddParameter("@PRODUCT_NAME", dto.LookupField1 ?? string.Empty);
                AddParameter("@CATEGORY_ID", dto.LookupNumericField2);
                AddParameter("DOCUMENT_TYPE", dto.TituloDocumento);
                AddParameter("FISCAL_YEAR", dto.FiscalYear);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new FaturaDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Referencia = dr[1].ToString(),
                        Emissao = DateTime.Parse(dr[2].ToString()),
                        DescricaoDocumento = dr[3].ToString(),
                        NomeEntidade = dr[4].ToString(),
                        TotalDescontos = decimal.Parse(dr[5].ToString()),
                        TotalImpostos = decimal.Parse(dr[6].ToString()),
                        ValorTotal = decimal.Parse(dr[7].ToString()),
                        TotalIliquido = decimal.Parse(dr["FAT_VALOR_BRUTO"].ToString()),
                        ValorPago = decimal.Parse(dr[8].ToString()),
                        Troco = decimal.Parse(dr[9].ToString())
                    };
                    dto.ValorPago = dto.ValorPago - dto.Troco;

                    dto.Validade = DateTime.Parse(dr[10].ToString()); 
                    dto.StatusDocumento = int.Parse(dr[12].ToString());
                   

                    dto.DiasAtrasado = DiasPendentes(dto.Validade);
                    dto.Utilizador = dr["FAT_CREATED_BY"].ToString();
                    dto.TituloDocumento = dr["DOC_FORMATO"].ToString();
                    dto.Documento = int.Parse(dr["DOC_CODIGO"].ToString());

                    dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString());
                    dto.Entidade = int.Parse(dr["FAT_CODIGO_CLIENTE"].ToString());
                    dto.Parcela = int.Parse(dr["FAT_PARCELA"].ToString() == String.Empty ? "1" : dr["FAT_PARCELA"].ToString());
                    dto.LocalDescarga = dr["FAT_LOCAL_DESCARGA"].ToString();
                    dto.EnderecoDescarga = dr["FAT_MORADA_DESCARGA"].ToString();
                    dto.DataDescarga = DateTime.Parse(dr["FAT_DATA_ENTREGA"].ToString() != "" ? dr["FAT_DATA_ENTREGA"].ToString() : DateTime.MinValue.ToString());
                    dto.Destinatario = dr["FAT_RECEPCAO"].ToString();
                    dto.DeliveryMan = dr["FAT_RESPONSAVEL_ENTREGA"].ToString();
                    
                    dto.LocalDescarga += dto.EnderecoDescarga;
                    dto.LookupField11 = new StatusDAO().DocumentStatusList().Where(t => t.Codigo == dto.StatusDocumento).SingleOrDefault().Descricao; 
                    dto.NumeroDocOrigem = dr["DOCUMENTO_ORIGINAL"].ToString();
                    dto.StatusPagamento = dr[13].ToString() != "" ? new StatusDAO().PaymentStatusList().Where(t => t.Codigo == int.Parse(dr[13].ToString())).SingleOrDefault().Descricao : dr[13].ToString();
                     
                    dto.ValorRetencao = decimal.Parse(dr["FAT_VALOR_RETENCAO"].ToString() != "" ? dr["FAT_VALOR_RETENCAO"].ToString() : "0");
                    dto.Saldo = dto.ValorPago > 0 ? ((dto.ValorTotal - dto.ValorRetencao) - dto.ValorPago) : decimal.Parse(dr[11].ToString());
                    dto.Saldo = dto.TituloDocumento == "INVOICE" ? (dto.Saldo > 0 ? dto.Saldo : (dto.ValorTotal - dto.ValorRetencao) - (dto.ValorPago - dto.Troco)) : 0;
                    if (dto.TituloDocumento == "INVOICE" || dto.TituloDocumento=="INVOICE_R")
                    {
                        if(dto.TituloDocumento == "INVOICE_R")
                        {
                            dto.ValorPago = (dto.ValorTotal - dto.ValorRetencao);
                            dto.Saldo = 0;
                        }

                        dto.StatusPagamento = " <span class='label label-success'>" + dto.StatusPagamento + "</span>";
                        if (dto.StatusDocumento != 1)
                        {
                            int percentagemPaga = dto.ValorPago > 0 ? (int)Math.Round((dto.ValorPago * 100) / (dto.ValorTotal - dto.ValorRetencao)) : 0;
                            //((int)dto.ValorPago >= (int)dto.ValorTotal)
                            if ((percentagemPaga > 0 && percentagemPaga < 100))
                            {
                                if (dto.DiasAtrasado <= 0)
                                {
                                    dto.StatusPagamento = " <span class='badge bg-yellow'>" + new StatusDAO().PaymentStatusList().Where(t => t.Codigo == 2).SingleOrDefault().Descricao + " (" + percentagemPaga + "%)</span>";
                                    dto.LookupField6 = "2";
                                }
                                    
                                else
                                {
                                    percentagemPaga = 100 - percentagemPaga;
                                    dto.StatusPagamento = " <span class='badge bg-red'>"+ percentagemPaga + "% DO PAGAMENTO "+ new StatusDAO().PaymentStatusList().Where(t => t.Codigo == 5).SingleOrDefault().Descricao + " </span></div></div>";
                                    dto.LookupField6 = "5";
                                }
                                    
                            }
                            else if (percentagemPaga == 0)
                            {
                                if (dto.DiasAtrasado <= 0)
                                {
                                    dto.StatusPagamento = " <span class='label label-warning'>PENDENTE</span>";
                                    dto.LookupField6 = "1";
                                }
                                else
                                {
                                    dto.StatusPagamento = " <span class='label label-danger'>" + new StatusDAO().PaymentStatusList().Where(t => t.Codigo == 4).SingleOrDefault().Descricao + "</span>";
                                    dto.LookupField6 = "4";
                                }
                                    
                            }
                            else if ((dto.ValorPago) >= (dto.ValorTotal - dto.ValorRetencao))
                            {
                                dto.StatusPagamento = " <span class='label label-success'>LIQUIDADO(A)</span>";
                                dto.LookupField6 = "3";
                            }
                        }
                        else
                        {
                            dto.StatusPagamento = " <span class='label label-info'> EM FATURAÇÃO</span>";
                            dto.LookupField6 = "1";
                        }
                        
                    }


                    dto.Activo = true;
                    if (dr["FAT_STATUS"].ToString() =="0")
                    {    
                        dto.StatusPagamento = "<span class='label label-danger'>DOCUMENTO ANULADO</span>";
                        dto.Activo = false;
                        dto.LookupField10 = "(Anulado)";
                        dto.LookupField5 = "<span class='label label-danger'>DOCUMENTO ANULADO</span>";
                    }
                    else
                    { 
                        dto.LookupField5 = new StatusDAO().DocumentStatusList().Where(t => t.Codigo == dto.StatusDocumento).SingleOrDefault().Descricao;
                        dto.LookupField5 = " <span class='label label-success'>" + dto.LookupField5 + "</span>";
                    }

                    if (dto.StatusDocumento == 1)
                    {
                        dto.LookupField11 = " <span class='label label-info'>DOCUMENTO EM RASCUNHO</span>";
                        dto.LookupField10 = string.Empty;
                        dto.LookupField5 = " <span class='label label-info'>DOCUMENTO EM RASCUNHO</span>";
                    }
                    else
                    {
                         
                        dto.LookupField11 = string.Empty;
                        dto.LookupField10 = string.Empty;
                        
                    }
                     

                    dto.CreatedBy = dr["FAT_CREATED_BY"].ToString();
                    dto.CreatedDate = DateTime.Parse(dr["FAT_CREATED_DATE"].ToString());
                    dto.NIF = dr["ENT_IDENTIFICACAO"].ToString();
                    dto.LookupField2 = dr["SER_DESCRICAO"].ToString();
                    dto.Numeracao = int.Parse(dr["FAT_NUMERACAO"].ToString()); 

                    lista.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        private int DiasPendentes(DateTime pDueDate)
        { 
            return (int)DateTime.Today.Subtract(pDueDate).TotalDays;
        }

        public List<FaturaDTO> ObterPendentes(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {


                ComandText = "stp_COM_FATURA_CLIENTE_OBTERPENDENTES";

                AddParameter("@FILIAL", dto.Filial);

                if (dto.EmissaoIni == DateTime.MinValue)
                    AddParameter("@EMISSAO_INI", DBNull.Value);
                else
                    AddParameter("@EMISSAO_INI", DateTime.Parse(dto.EmissaoIni.ToString()));


                if (dto.EmissaoTerm == DateTime.MinValue)
                    AddParameter("@EMISSAO_TERM", DBNull.Value);
                else
                    AddParameter("@EMISSAO_TERM", DateTime.Parse(dto.EmissaoTerm.ToString()));



                if (dto.ValidadeIni == DateTime.MinValue)
                    AddParameter("@VALIDADE_INI", DBNull.Value);
                else
                    AddParameter("@VALIDADE_INI", dto.ValidadeIni);


                if (dto.ValidadeTerm == DateTime.MinValue)
                    AddParameter("@VALIDADE_TERM", DBNull.Value);
                else
                    AddParameter("@VALIDADE_TERM", dto.ValidadeTerm);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@CLIENTE", dto.NomeEntidade);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SITUACAO", dto.Activo == true ? 1 : 0);
                AddParameter("@ENTIDADE", dto.Entidade);
                AddParameter("@DOCUMENT_STATUS", dto.StatusDocumento);
                AddParameter("@PAYMENT_STATUS", dto.StatusPagamento);
                AddParameter("@SERIE", dto.Serie == null ? -1 : dto.Serie);
                AddParameter("@WAREHOUSE_ID", dto.Armazem);
                AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = ExecuteReader();
                decimal _acumulado = 0;
                while (dr.Read())
                {
                    dto = new FaturaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Referencia = dr[1].ToString();
                    dto.Emissao = DateTime.Parse(dr[2].ToString());
                    dto.DescricaoDocumento = dr[3].ToString();
                    dto.NomeEntidade = dr[4].ToString();
                    dto.TotalDescontos = decimal.Parse(dr[5].ToString());
                    dto.TotalImpostos = decimal.Parse(dr[6].ToString());
                    dto.ValorTotal = decimal.Parse(dr[7].ToString());
                    dto.TotalIliquido = decimal.Parse(dr["FAT_VALOR_BRUTO"].ToString());
                    dto.ValorPago = decimal.Parse(dr[8].ToString());
                    dto.Troco = decimal.Parse(dr[9].ToString());
                    
                    dto.Lucro = 0;//new ItemFaturacaoDAO().ObterLucro(dto.Codigo);
                    dto.Validade = DateTime.Parse(dr[10].ToString());
                    dto.Saldo = decimal.Parse(dr[11].ToString());
                    dto.StatusDocumento = int.Parse(dr[12].ToString());
                    dto.StatusPagamento = dr[13].ToString();
                    
                    
                    dto.Utilizador = dr["FAT_CREATED_BY"].ToString();
                    dto.TituloDocumento = dr["DOC_FORMATO"].ToString();
                    dto.LookupField1 = dr[14].ToString();
                    dto.LookupField2 = dr["MOE_SIGLA"].ToString();
                    dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString());
                    dto.Entidade = int.Parse(dr["FAT_CODIGO_CLIENTE"].ToString());
                    dto.Parcela = int.Parse(dr["FAT_PARCELA"].ToString() == String.Empty ? "1" : dr["FAT_PARCELA"].ToString());
                    dto.ValorRetencao = decimal.Parse(dr["FAT_VALOR_RETENCAO"].ToString() != "" ? dr["FAT_VALOR_RETENCAO"].ToString() : "0");
                    dto.ValorPago = dto.ValorPago - dto.Troco;
                    dto.ValorTotal -= dto.ValorRetencao;
                    dto.Saldo = (dto.ValorTotal) - (dto.ValorPago + dto.Troco);
                    
                    dto.DiasAtrasado = DiasPendentes(dto.Validade);
                    dto.DocumentoOrigem = dr["FAT_NUMERO_DOC_ORIGEM"].ToString() != "" ? int.Parse(dr["FAT_NUMERO_DOC_ORIGEM"].ToString()) : 0;
                    dto.Activo = true;
                     
                    if (dto.StatusPagamento != "3")
                    {
                        if (dto.TituloDocumento == "INVOICE_D" || dto.TituloDocumento=="INVOICE_A")
                        {
                            if(lista.Exists(t => t.Codigo == dto.DocumentoOrigem) || dto.TituloDocumento == "INVOICE_A")
                            {
                                dto.TotalIliquido = dto.TotalIliquido > 0 ? dto.TotalIliquido *= (-1) : dto.TotalIliquido;
                                dto.TotalDescontos = dto.TotalDescontos < 0 ? dto.TotalDescontos *= (-1) : dto.TotalDescontos;
                                dto.TotalImpostos = dto.TotalImpostos > 0 ? dto.TotalImpostos *= (-1) : dto.TotalImpostos;
                                dto.Saldo = dto.Saldo > 0 ? dto.Saldo *= (-1) : dto.Saldo;
                                dto.ValorTotal = dto.ValorTotal < 0 ? dto.ValorTotal *= (-1) : dto.ValorTotal;
                                //dto.ValorPago = dto.ValorPago == 0 ? dto.ValorTotal : dto.ValorPago;
                                if (dto.ValorPago >= 0 || (dto.ValorPago < 0 && dto.ValorTotal != dto.ValorPago))
                                {
                                    dto.ValorTotal *= -1;
                                    _acumulado += dto.Saldo;
                                    dto.Lucro = _acumulado;
                                    lista.Add(dto);
                                }
                            }  
                        }
                        else
                        {
                            if (dto.ValorPago < dto.ValorTotal)
                            {
                                _acumulado += dto.Saldo;
                                dto.Lucro = _acumulado;
                                lista.Add(dto);
                            }
                        }
                    }
                    else
                    {
                        if(dto.TituloDocumento == "INVOICE_A")
                        {

                        }

                        dto.IsPaid = dto.StatusPagamento == "3" || (dto.ValorTotal - dto.ValorRetencao) == (dto.ValorPago - dto.Troco) || dto.TipoDocumento == "INVOICE_R" ? true : false;
                    }

                    
                    
                }


            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<FaturaDTO> ObterVendasDiarias(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_OBTERVENDASDIARIAS";

                AddParameter("@FILIAL", dto.Filial);

                if (dto.EmissaoIni == DateTime.MinValue)
                    AddParameter("@INICIO", DBNull.Value);
                else
                    AddParameter("@INICIO", dto.EmissaoIni);


                if (dto.EmissaoTerm == DateTime.MinValue)
                    AddParameter("@TERMINO", DBNull.Value);
                else
                    AddParameter("@TERMINO", dto.EmissaoTerm);
                AddParameter("@WAREHOUSE_ID", dto.Armazem);
                AddParameter("@UTILIZADOR", dto.Utilizador); 


                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new FaturaDTO
                    {
                        Emissao = DateTime.Parse(dr[0].ToString()),
                        ValorTotal = decimal.Parse(dr[1].ToString()),
                        TotalImpostos = decimal.Parse(dr[2].ToString()),
                        ValorFaturado = decimal.Parse(dr[1].ToString()),
                        Lucro =0,
                        TotalIliquido = decimal.Parse(dr[3].ToString()),
                        TotalDescontos = decimal.Parse(dr[4].ToString()),
                        ValorPago = decimal.Parse(dr[5].ToString()),
                        ValorRetencao = decimal.Parse(dr[6].ToString()),
                    }; 

                    if(lista.Exists(t=>t.Emissao == dto.Emissao))
                    {
                        var doc = lista.Where(t => t.Emissao == dto.Emissao).ToList().SingleOrDefault();
                        lista.Remove(doc);
                        dto.ValorTotal += doc.ValorTotal;
                        dto.TotalImpostos += doc.TotalImpostos;
                        dto.ValorFaturado += doc.ValorFaturado;
                        dto.TotalIliquido += doc.TotalIliquido;
                        dto.TotalDescontos += doc.TotalDescontos;
                        dto.ValorPago += doc.ValorPago;
                        dto.ValorRetencao += doc.ValorRetencao; 
                    }
                    
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista.OrderBy(t=>t.Emissao).ToList();
        }

        public List<FaturaDTO> ObterMovimentoCaixaDia(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {
                ComandText = "stp_COM_OBTERMOVIMENTODIACAIXA";

                AddParameter("@EMISSAO", dto.Emissao);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@FILIAL", dto.Filial);


                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new FaturaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Emissao = DateTime.Parse(dr[1].ToString());
                    dto.Referencia = dr[2].ToString();
                    dto.ValorTotal = decimal.Parse(dr[4].ToString());
                    dto.ValorPago = decimal.Parse(dr[3].ToString());
                    dto.Troco = decimal.Parse(dr[5].ToString());

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public void GravarParcela(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_PARCELA_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@CODIGO", dto.Parcela);
                AddParameter("@VALIDADE", dto.Validade); 
                AddParameter("@VALOR_TOTAL", dto.ValorTotal); 

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<ItemFaturacaoDTO> ObterListaDeArtigosVendidosEntreDatas(FaturaDTO dto)
        {
            List<ItemFaturacaoDTO> lista = new List<ItemFaturacaoDTO>();
            ItemFaturacaoDTO item;
            try
            {
                ComandText = "stp_COM_OBTERARTIGOS_VENDIDOS_ENTRE_DATAS";
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@DATA_INI", dto.Emissao);
                AddParameter("@DATA_FIN", dto.Validade);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    item = new ItemFaturacaoDTO();

                    item.Designacao = dr[0].ToString();
                    item.Quantidade = decimal.Parse(dr[1].ToString());
                    item.PrecoUnitario = decimal.Parse(dr[2].ToString());
                    item.ValorDesconto = decimal.Parse(dr[3].ToString());
                    item.ValorImposto = decimal.Parse(dr[4].ToString());
                    item.TotalLiquido = decimal.Parse(dr[5].ToString());

                    lista.Add(item);
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
            return lista;
        }

        public List<MetodoPagamentoDTO> ObterPagamentos(FaturaDTO dto)
        {
            List<MetodoPagamentoDTO> lista = new List<MetodoPagamentoDTO>();
            try
            {
                ComandText = "stp_FIN_FATURA_OBTERPAGAMENTOS";
                AddParameter("@FATURA", dto.Codigo); 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    MetodoPagamentoDTO pagto = new MetodoPagamentoDTO();

                    pagto.Descricao = dr[0].ToString();
                    pagto.TotalLinha = decimal.Parse(dr[1].ToString());

                    lista.Add(pagto);
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
            return lista;
        }

        public List<ReceiptDTO> ObterDadosImpressao( FaturaDTO dto)
        {

            List<ReceiptDTO> lista = new List<ReceiptDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_IMPRESSAO";


                AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();


                while (dr.Read())
                {
                    ReceiptDTO invoice = new ReceiptDTO();

                    invoice.DocumentType = dr[0].ToString();
                    invoice.DocumentReference = dr[1].ToString();
                    invoice.SalesDate = dr[2].ToString();
                    invoice.PaymentMethod = dr[3].ToString();
                    invoice.TotalToPay = dr[4].ToString();
                    invoice.Customer = dr[5].ToString();
                    invoice.CustomerPhoneNumber = dr[6].ToString();
                    invoice.CustomerPhoneAlternate = dr[7].ToString();
                    invoice.CustomerEmail = dr[8].ToString(); 
                    invoice.CustomerAddressLine1 = dr[9].ToString();
                    invoice.CustomerAddressLine2 = dr[10].ToString();
                    invoice.DocumentCurrency = dr[11].ToString();
                    invoice.DocumentRate = dr[12].ToString();
                    invoice.CustomerDiscount = dr[13].ToString();
                    invoice.DocumentDiscount = dr[14].ToString();//FinancialDiscount
                    invoice.DocumentDueDate = dr[15].ToString();
                    invoice.ProductCode = dr[16].ToString();
                    invoice.ProductBarcode = dr[17].ToString();
                    invoice.ProductDesignation = dr[18].ToString();
                    invoice.ProductQuantity = dr[19].ToString();
                    invoice.ProductPrice = dr[20].ToString();
                    invoice.ProductDiscount = dr[21].ToString();
                    invoice.ProductTax = dr[22].ToString();
                    invoice.ProductTotal = dr[23].ToString();
                    invoice.Subtotal = dr[24].ToString();
                    invoice.DocumentDiscountValue = dr[25].ToString();//Desconto total do documento
                    invoice.DocumentTaxValue = dr[26].ToString();
                    invoice.CustomerNIF = dr[27].ToString();
                    invoice.TituloDocumento = dr[28].ToString();
                    invoice.ComercialNotes = dr[29].ToString(); 
                    invoice.ProductNotes = dr[31].ToString().Replace("\n", "<br/>"); //Observaçoes da Linha(ItemNotes)
                    invoice.Hour = DateTime.Parse(dr[32].ToString()).ToShortTimeString();
                    invoice.ExpeditionDate = dr[33].ToString() != "" ? dr[33].ToString() : string.Empty;
                    invoice.Matricula = dr[34].ToString();
                    invoice.ExpeditionPlace = dr[35].ToString();
                    invoice.DeliveryAddress = dr[36].ToString();
                    invoice.DeliveryDate = dr[37].ToString();
                    invoice.ReceiverPeople = dr[38].ToString();
                    invoice.DeliveryPeople = dr[39].ToString() == "-1" ? "" : dr[39].ToString();
                    invoice.TaxID = int.Parse(dr[40].ToString() == "" ? "-1" : dr[40].ToString());
                    invoice.DocumentNotes = dr[41].ToString()+dr["FAT_MOTIVO_ANULACAO"].ToString() + "<br/>";
                    invoice.Status = dr["FAT_STATUS"].ToString() != "" ? int.Parse(dr["FAT_STATUS"].ToString()) : 1;
                    invoice.DocumentBarcode = dr["FAT_BARCODE"].ToString();
                    invoice.SocialName = dr["VENDEDOR"].ToString();
                    invoice.Utilizador = dr["FAT_CREATED_BY"].ToString();
                    invoice.ProductUnity = dr["UNI_SIGLA"].ToString();
                    invoice.Paid = dr["FAT_VALOR_PAGO"].ToString();

                    invoice.FinanceDiscountValue = decimal.Parse(invoice.Subtotal) * (decimal.Parse(invoice.DocumentDiscount)/100);
                    invoice.CustomerDiscountValue = decimal.Parse(invoice.Subtotal) * (decimal.Parse(invoice.CustomerDiscount)/100);

                    invoice.ProductDiscountValue = (decimal.Parse(invoice.ProductQuantity) * decimal.Parse(invoice.ProductPrice)) * (decimal.Parse(invoice.ProductDiscount)/100);
                    //DocInfo.DocumentDiscountValue = (DocInfo.ProductDiscountValue + DocInfo.FinanceDiscountValue + DocInfo.CustomerDiscountValue).ToString();

                    if (invoice.Status == 0)
                    {
                        invoice.DocumentStatus = "<span style='text-align:right; color:red'>Documento Anulado</span>";
                    }
                    else
                    {
                        invoice.DocumentStatus = string.Empty;
                    }

                    invoice.LookupField1 = dr["FAT_CUSTOMER_SENT"].ToString() == "1" ? "2ª Via em conformidade com o original" : "ORIGINAL";
                    if(!string.IsNullOrEmpty(dr["FAT_HASH"].ToString()))
                        invoice.HashTag = dr["FAT_HASH"].ToString()[0].ToString()+dr["FAT_HASH"].ToString()[10].ToString() + dr["FAT_HASH"].ToString()[20].ToString() + dr["FAT_HASH"].ToString()[30].ToString() + "-";
                    invoice.RetencaoLinha = decimal.Parse(dr["FAT_ITEM_RETENCAO"].ToString());
                    invoice.RetencaoDocumento = dr["FAT_VALOR_RETENCAO"].ToString();
                    invoice.FooterDocumentNotes = dr["DOC_COMENTARIO_FIXO"].ToString();
                    invoice.HeaderNotes = dr["FAT_TITLE"].ToString();
                    invoice.LookupField3 = dr["CAT_DESCRICAO"].ToString();
                    invoice.NroOrdenacao = int.Parse(dr["FAT_ORDEM"].ToString());
                    invoice.FuncionarioID = dr["NOME_TECNICO_EXECUTOR"].ToString();
                    invoice.ValorUtente = dr["FAT_ITEM_VALOR_UTENTE"].ToString();
                    invoice.ValorEntidade = dr["FAT_ITEM_VALOR_ENTIDADE"].ToString();
                    invoice.TituloLinha = dr["FAT_ITEM_TITULO"].ToString();
                    invoice.Email = dr["ENT_EMAIL"].ToString();
                    invoice.MobileNumber = dr["ENT_TELEFONE"].ToString();
                    invoice.CarMark = dr["FAT_MARCA"].ToString();
                    invoice.CarModel = dr["FAT_MODELO"].ToString();
                    invoice.CarRegistrationID = dr["FAT_MATRICULA"].ToString();
                    invoice.CarYear = dr["FAT_ANO"].ToString();
                    invoice.CarKM = dr["FAT_LOOKUPFIELD7"].ToString();
                    invoice.CarColor = dr["FAT_LOOKUPFIELD8"].ToString();
                    invoice.ContractReference = dr["FAT_CONTRATO"].ToString();
                    invoice.SESNumber = dr["FAT_SESNUMBER"].ToString();
                    invoice.ShipName = dr["FAT_LOOKUPFIELD9"].ToString();
                    invoice.CaixaPostal = dr["ENT_CAIXA_POSTAL"].ToString();
                    invoice.DistritoUrbano = dr["ENT_DISTRITO"].ToString();
                    invoice.CustomerCurrencyID = int.Parse(dr["TER_CURRENCY_ID"].ToString());


                    lista.Add(invoice);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<FaturaDTO> ObterCustomerExtract(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {


                ComandText = "stp_COM_CONTA_CORRENTE_CLIENTE_EXTRATO";

                AddParameter("@ENTITY_ID", dto.Entidade); 

                MySqlDataReader dr = ExecuteReader();
                decimal _acumulado = 0;
                while (dr.Read())
                {
                    dto = new FaturaDTO();

                    if (dr["DOC_FORMATO"].ToString() == "INVOICE_R" || dr["DOC_FORMATO"].ToString() == "INVOICE" || 
                        dr["DOC_FORMATO"].ToString() == "INVOICE_D" || dr["DOC_FORMATO"].ToString() == "INVOICE_A"
                        || dr["DOC_FORMATO"].ToString() == "RECEIPT_P" || dr["DOC_FORMATO"].ToString() == "RECEIPT_R" || dr["DOC_FORMATO"].ToString() == "RECEIPT_A")
                    {
                        
                        dto.Codigo = int.Parse(dr[0].ToString());
                        dto.Emissao = DateTime.Parse(dr[1].ToString());
                        dto.LookupField1 = dr[2].ToString();
                        dto.Referencia = dr[3].ToString();
                        dto.DescricaoDocumento = dr[4].ToString();
                        dto.NomeEntidade = dr[5].ToString();
                        dto.TituloDocumento = dr["DOC_FORMATO"].ToString();
                        dto.ValorTotal = decimal.Parse(dr[6].ToString());

                        if (dto.TituloDocumento != "INVOICE_D")
                        {
                            dto.Debito = dto.ValorTotal;
                            dto.ValorPago = decimal.Parse(dr[7].ToString());  
                            
                            dto.Credito = dto.ValorPago;

                            if (dto.TituloDocumento == "INVOICE")
                            {
                                dto.ValorPago = 0;
                                dto.Credito = 0;
                            }else if (dto.TituloDocumento == "RECEIPT_R")
                            {
                                dto.ValorTotal = 0;
                                dto.Debito = 0;
                            } 
                        } 
                        else
                        {
                            dto.ValorTotal = decimal.Parse(dr[7].ToString()); // DEBITO
                            dto.ValorPago = decimal.Parse(dr[6].ToString()); // CREDITO
                            dto.Debito = 0;
                            dto.Credito = dto.ValorPago;
                        }

                        if (dto.TituloDocumento == "INVOICE_A" || dto.TituloDocumento == "RECEIPT_A")
                        {
                            dto.Adiantamento = decimal.Parse(dr[8].ToString());

                            if (dto.Adiantamento == dto.Credito)
                                dto.Credito = 0;

                            dto.Saldo = dto.Debito > 0 ? dto.Debito - dto.Credito : 0;  

                        }
                        else
                        {
                            dto.Saldo = dto.ValorTotal - dto.ValorPago;
                        }    

                        _acumulado += dto.Saldo;
                        dto.LookupNumericField1 = _acumulado;



                        dto.Saldo = dto.Saldo < 0 ? (-1)*dto.Saldo : dto.Saldo;
                        dto.Saldo = dto.Saldo - dto.ValorPago;
                        dto.LookupDate1 = DateTime.Parse(dr["FAT_CREATED_DATE"].ToString()); 
                        dto.Documento = int.Parse(dr["DOC_CODIGO"].ToString());
                        dto.LookupField2 = dr["MOE_SIGLA"].ToString();
                        dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString() == string.Empty ? "1" : dr["FAT_CAMBIO"].ToString());
                        dto.Parcela = int.Parse(dr["FAT_PARCELA"].ToString());
                        dto.Entidade = int.Parse(dr["FAT_CODIGO_CLIENTE"].ToString());
                        dto.NumeroDocOrigem = dr["FAT_NUMERO_DOC_ORIGEM"].ToString();
                        dto.Email = dr["ENT_EMAIL"].ToString();
                        lista.Add(dto);
                    }
                }


            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public FaturaDTO GetLastInserted(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_GETLASTONE";

                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@SERIE_ID", dto.Serie);

                MySqlDataReader dr = ExecuteReader();

                dto = new FaturaDTO();
                if (dr.Read())
                {
                    dto.Numeracao = int.Parse(dr[0].ToString() == string.Empty ? "0" : dr[0].ToString());
                    dto.Emissao = DateTime.Parse(dr[1].ToString());
                     
                    
                     
                }

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

        public ReceiptDTO GetConsultaMesa(ReceiptDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_CONSULTA_CONTA_ADICIONAR";

                AddParameter("@ATENDIMENTO_ID", dto.DocumentID);
                AddParameter("@UTILIZADOR", dto.SocialName);

                MySqlDataReader dr = ExecuteReader();

                dto = new ReceiptDTO();
                if (dr.Read())
                {
                    dto.DocumentType = dr["DOC_DESCRICAO"].ToString();
                    dto.DocumentReference = dr["CONS_REFERENCE"].ToString();
                    dto.Utilizador = dr["UTI_NOME"].ToString();
                }

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

        public List<FaturaDTO> GetPosExtractList(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {


                ComandText = "stp_COM_POS_STATUS_EXTRACT";

                AddParameter("@POS_ID", dto.Codigo);

                 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new FaturaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.DescricaoDocumento = dr[1].ToString();
                    dto.Referencia = dr[2].ToString() == String.Empty ? dr[3].ToString() : dr[2].ToString();
                    dto.ValorTotal = decimal.Parse(dr[4].ToString());
                    dto.Emissao = DateTime.Parse(dr[5].ToString());
                    dto.NomeEntidade = dr[6].ToString();
                    dto.Url = dr[7].ToString();
                    dto.LookupField11 = dr[9].ToString();
                    dto.Saldo = decimal.Parse(dr[10].ToString());
                    dto.LookupField2 = dr[11].ToString();/*
                    if(lista.Exists(t=>t.Referencia == dto.Referencia))
                    {
                        var document = lista.Where(t => t.Referencia == dto.Referencia).SingleOrDefault(); 
                        lista = lista.Where(t => t.Referencia != dto.Referencia).ToList();
                        document.ValorTotal += dto.ValorTotal;
                        dto = document;
                    }*/
                    lista.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public void ExcluirItem(ItemFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_EXCLUIR";

                
                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@FATURA", dto.Fatura);

                ExecuteNonQuery();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            } 
        }

        public List<FaturaDTO> ObterPorVeiculos(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {


                ComandText = "stp_COM_EXTRATO_FATURACAO_VIATURA";

                AddParameter("@FILIAL", dto.Filial);

                if (dto.EmissaoIni == DateTime.MinValue)
                    AddParameter("@EMISSAO_INI", DBNull.Value);
                else
                    AddParameter("@EMISSAO_INI", DateTime.Parse(dto.EmissaoIni.ToString()));


                if (dto.EmissaoTerm == DateTime.MinValue)
                    AddParameter("@EMISSAO_TERM", DBNull.Value);
                else
                    AddParameter("@EMISSAO_TERM", DateTime.Parse(dto.EmissaoTerm.ToString()));



                if (dto.ValidadeIni == DateTime.MinValue)
                    AddParameter("@VALIDADE_INI", DBNull.Value);
                else
                    AddParameter("@VALIDADE_INI", dto.ValidadeIni);


                if (dto.ValidadeTerm == DateTime.MinValue)
                    AddParameter("@VALIDADE_TERM", DBNull.Value);
                else
                    AddParameter("@VALIDADE_TERM", dto.ValidadeTerm);

                AddParameter("@REFERENCIA", dto.Referencia == null ? string.Empty : dto.Referencia);
                AddParameter("@CLIENTE", dto.NomeEntidade == null ? string.Empty : dto.NomeEntidade);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SITUACAO", dto.Activo == true ? 1 : 0);
                AddParameter("@ENTIDADE", dto.Entidade);
                AddParameter("@DOCUMENT_STATUS", dto.StatusDocumento);
                AddParameter("@PAYMENT_STATUS", dto.StatusPagamento);
                AddParameter("@SERIE", dto.Serie == null ? -1 : dto.Serie);
                AddParameter("@WAREHOUSE_ID", dto.Armazem);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@PRODUCT_ID", dto.LookupNumericField1);
                AddParameter("@PRODUCT_NAME", dto.LookupField1 ?? string.Empty);
                AddParameter("@CATEGORY_ID", dto.LookupNumericField2);
                AddParameter("DOCUMENT_TYPE", dto.TituloDocumento);
                AddParameter("VEHICLE_ID", dto.Matricula);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new FaturaDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Referencia = dr[1].ToString(),
                        Emissao = DateTime.Parse(dr[2].ToString()),
                        DescricaoDocumento = dr[3].ToString(),
                        NomeEntidade = dr[4].ToString(),
                        TotalDescontos = decimal.Parse(dr[5].ToString()),
                        TotalImpostos = decimal.Parse(dr[6].ToString()),
                        ValorTotal = decimal.Parse(dr[7].ToString()),
                        TotalIliquido = decimal.Parse(dr["FAT_VALOR_BRUTO"].ToString()),
                        ValorPago = decimal.Parse(dr[8].ToString()),
                        Troco = decimal.Parse(dr[9].ToString())
                    };
                    dto.ValorPago = dto.ValorPago - dto.Troco;

                    dto.Validade = DateTime.Parse(dr[10].ToString());
                    dto.Saldo = dto.ValorPago > 0 ? (dto.ValorTotal - dto.ValorPago) : decimal.Parse(dr[11].ToString());
                    dto.StatusDocumento = int.Parse(dr[12].ToString());
                    dto.StatusPagamento = dr[13].ToString() != "" ? new StatusDAO().PaymentStatusList().Where(t => t.Codigo == int.Parse(dr[13].ToString())).SingleOrDefault().Descricao : "";


                    dto.DiasAtrasado = DiasPendentes(dto.Validade);
                    dto.Utilizador = dr["FAT_CREATED_BY"].ToString();
                    dto.TituloDocumento = dr["DOC_FORMATO"].ToString();
                    dto.Documento = int.Parse(dr["DOC_CODIGO"].ToString());
                    dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString());
                    dto.Entidade = int.Parse(dr["FAT_CODIGO_CLIENTE"].ToString());
                    dto.Parcela = int.Parse(dr["FAT_PARCELA"].ToString() == String.Empty ? "1" : dr["FAT_PARCELA"].ToString());
                    dto.LocalDescarga = dr["FAT_LOCAL_DESCARGA"].ToString();
                    dto.EnderecoDescarga = dr["FAT_MORADA_DESCARGA"].ToString();
                    dto.DataDescarga = DateTime.Parse(dr["FAT_DATA_ENTREGA"].ToString() != "" ? dr["FAT_DATA_ENTREGA"].ToString() : DateTime.MinValue.ToString());
                    dto.Destinatario = dr["FAT_RECEPCAO"].ToString();
                    dto.DeliveryMan = dr["FAT_RESPONSAVEL_ENTREGA"].ToString();
                    dto.Saldo = dto.TituloDocumento == "INVOICE" ? (dto.Saldo > 0 ? dto.Saldo : dto.ValorTotal - (dto.ValorPago - dto.Troco)) : 0;
                    dto.LocalDescarga += dto.EnderecoDescarga;
                    dto.LookupField11 = dto.StatusDocumento != 8 ? new StatusDAO().DocumentStatusList().Where(t => t.Codigo == dto.StatusDocumento).SingleOrDefault().Descricao : "";
                    dto.NumeroDocOrigem = dr["DOCUMENTO_ORIGINAL"].ToString();
                    lista.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public MoradaDTO CustomerInvoiceAddress(FaturaDTO dto)
        {
            var address = new MoradaDTO();
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_MORADA_OBTER";

                AddParameter("@INVOICE", dto.Codigo); 

                MySqlDataReader dr = ExecuteReader();

                 
                if (dr.Read())
                {
                    address.BuildingNumber = dr[1].ToString();
                    address.StreetName = dr[2].ToString();
                    address.AddressDetail = dr[3].ToString();
                    address.City = dr[4].ToString();
                    address.Province = dr[6].ToString();
                    address.Country = dr[7].ToString();
                    address.PhoneNumber = dr[8].ToString();
                    address.Email = dr[9].ToString();
                }

            }
            catch (Exception ex)
            {
                address.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
            return address;
        }

        public Tuple<bool, string> SaveCustomerOrderPayment(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_CUSTOMER_ORDER_UPDATE_PAYMENT";

                AddParameter("@ORDER_ID", dto.NumeroDocOrigem); 
                AddParameter("@PAID_VALUE", dto.ValorPago);
                ExecuteNonQuery();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return new Tuple<bool, string>(dto.Sucesso, dto.MensagemErro);
        }

        public List<FaturaDTO> ObterImpostoSelo(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {


                ComandText = "stp_COM_MAPA_IMPOSTO_SELO";

                AddParameter("@EMISSAO_INI", dto.EmissaoIni);
                AddParameter("@EMISSAO_TERM", dto.EmissaoTerm);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Emissao = DateTime.Parse(dr[1].ToString()); 
                    dto.Referencia = dr[2].ToString(); 
                    dto.Entidade = int.Parse(dr[4].ToString());
                    dto.NomeEntidade = dr[5].ToString();
                    dto.ValorTotal = decimal.Parse(dr[6].ToString());
                    dto.ValorPago = decimal.Parse(dr[6].ToString());
                    dto.TituloDocumento = dr["DOC_FORMATO"].ToString();  
                    dto.Documento = int.Parse(dr["DOC_CODIGO"].ToString());
                    dto.LookupField2 = dr["MOE_SIGLA"].ToString();
                    dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString() == string.Empty ? "1" : dr["FAT_CAMBIO"].ToString());
                    dto.Parcela = int.Parse(dr["FAT_PARCELA"].ToString()); 
                    dto.TotalDescontos = decimal.Parse(dr[14].ToString());
                    dto.ValorRetencao = decimal.Parse(dr[15].ToString());
                    dto.TotalImpostos = dto.ValorPago * (1 / 100);
                    lista.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public void CorrigeDocumentosAnulados(FaturaDTO dto)
        {
            try
            {

                ComandText = "stp_sys_retifica_notas_credito";

                AddParameter("@CODIGO", dto.Codigo); 

                ExecuteNonQuery();
                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);

            }
            finally
            {
                FecharConexao(); 
            }
        }

        public List<FaturaDTO> GetCustomerOrderPayments(FaturaDTO dto)
        {
            List<FaturaDTO> lista = new List<FaturaDTO>();
            try
            {
                ComandText = "stp_COM_CUSTOMER_ORDER_BILLING";

                AddParameter("@ORDER_ID", dto.Codigo);

                MySqlDataReader dr = ExecuteReader(); 
                

                while (dr.Read())
                {
                    lista.Add(dto = new FaturaDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Documento = int.Parse(dr[1].ToString()),
                        Numeracao = int.Parse(dr[2].ToString()),
                        Referencia = dr[3].ToString(),
                        ValorTotal = decimal.Parse(dr[4].ToString()),
                        ValorPago = decimal.Parse(dr[5].ToString()),
                        Saldo = decimal.Parse(dr[4].ToString()) - decimal.Parse(dr[5].ToString())
                    });
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        FaturaDTO AddOtherInfo(FaturaDTO dto)
        {
            try
            {

                ComandText = "stp_COM_FATURA_CLIENTE_OTHERINFO_ADICIONAR";

                AddParameter("@FTM_NUMBER", dto.ManualInvoiceNumber);
                AddParameter("@FTM_REFERENCE", dto.ManualInvoiceReference);
                AddParameter("@FTM_DATA", dto.ManualInvoiceDate);
                AddParameter("@CAR_REGISTRATION", dto.CarRegistrationID);
                AddParameter("@CAR_MARK", dto.CarMark);
                AddParameter("@CAR_MODEL", dto.CarModel);
                AddParameter("@CAR_YEAR", dto.CarYear == string.Empty ? "0" : dto.CarYear);
                AddParameter("@CONTRACT_REFERENCE", dto.ContractReference);
                AddParameter("@SESNUMBER", dto.SESNumber);
                AddParameter("@COMPANY_BILLING", dto.IssuingBranchID);
                AddParameter("@LOOKUP1", dto.CarKM); //CAR KM
                AddParameter("@LOOKUP2", dto.CarColor); // CAR COLOR
                AddParameter("@LOOKUP3", dto.ShipName); // NAVIO
                AddParameter("@LOOKUP4", dto.LookupField4); // FREE
                AddParameter("@CODIGO", dto.Codigo);

                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
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
