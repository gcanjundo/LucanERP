using DataAccessLayer.Geral;
using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;



namespace DataAccessLayer.Comercial
{
    public class CompraDAO : ConexaoDB
    {
        public FaturaDTO Adicionar(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@ARMAZEM", dto.Armazem);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SERIE", dto.Serie);
                AddParameter("@FORNECEDOR", dto.Entidade);
                AddParameter("@NOME_FORNECEDOR", dto.NomeEntidade);
                AddParameter("@EMISSAO", dto.Emissao);
                AddParameter("@VALIDADE", dto.Validade);
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@CAMBIO", dto.Cambio);
                AddParameter("@PRAZO", dto.PrazoPagto);
                AddParameter("@EXPEDICAO", dto.Expedicao);
                AddParameter("@DESCONTO", dto.Desconto);
                AddParameter("@STATUS_DOCUMENT", dto.StatusDocumento);
                AddParameter("@NUMERACAO", dto.Numeracao == null ? 0 : dto.Numeracao);
                AddParameter("@REFERENCIA", dto.Referencia == null ? string.Empty : dto.Referencia);
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
                AddParameter("@DESCONTO_FORNECEDOR", dto.DescontoEntidade);

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
                AddParameter("@REFERENCIA_FORNECEDOR", dto.DocReferenciaExterna);
                AddParameter("@VALOR_FRETE", dto.ValorFrete);
                AddParameter("@VALOR_SEGURO", dto.ValorSeguro);
                AddParameter("@IVA_IMPORTACAO", dto.ValorIVAImportacao);
                AddParameter("@VALOR_ADUANEIRO", dto.ValorAduaneiro);
                AddParameter("@HONORARIO_DESPACHANTE", dto.HonorarioDespachante);
                AddParameter("@TRANSPORTACAO_LOCAL", dto.TransportacaoLocal);
                AddParameter("@OUTROS_ENCARGOS", dto.OutrosEncargos);
                AddParameter("@REQUISICAO", dto.Requisicao);
                AddParameter("@MARCA", dto.Marca);
                AddParameter("@MODELO", dto.Modelo);
                AddParameter("@CHASSI", dto.Chassi);
                AddParameter("@ANO_FABRICO", string.IsNullOrEmpty(dto.AnoFabrico) ? (object)DBNull.Value : dto.AnoFabrico);

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
                    dto = ObterPorPK(dto);
                    dto.Sucesso = true;
                }
            }

            return dto;
        }

        public FaturaDTO Excluir(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_EXCLUIR";

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
                ComandText = "stp_COM_FATURA_FORNECEDOR_CANCELAR";

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
            catch
            {

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
            catch
            {

                total = 0;
            }
            finally
            {
                FecharConexao();
            }
            return total;
        }
        public FaturaDTO ObterPorPK(FaturaDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_OBTERPORPK";

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
                    dto.PrazoPagto = int.Parse(dr[10].ToString() ?? "-1");
                    dto.Expedicao = int.Parse(dr[11].ToString() ?? "-1");
                    dto.Desconto = decimal.Parse(dr[12].ToString());
                    dto.Numeracao = int.Parse(dr[13].ToString() == string.Empty ? "0" : dr[13].ToString());
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
                    dto.Activo = dr[36].ToString() == "0" ? false : true;
                    dto.TituloDocumento = dr[38].ToString();
                    dto.StatusDocumento = int.Parse(dr[39].ToString());
                    dto.Parcela = int.Parse(dr[40].ToString());
                    dto.NotasComerciais = dr[41].ToString();
                    dto.ResponsavelCarregamento = dr[42].ToString() == "-1" ? "" : dr[42].ToString();
                    dto.DeliveryMan = dr[43].ToString();
                    dto.Saldo = dto.ValorTotal - dto.ValorPago;
                    dto.Destinatario = dr[44].ToString();
                    dto.ContactoDestinatario = dr[45].ToString();
                    dto.LookupField1 = dr[46].ToString();
                    dto.EntityBillingID = int.Parse(dr[47].ToString() == string.Empty ? "1" : dr[47].ToString());
                    dto.Matricula = dr[48].ToString();
                    dto.DocumentBarcode = dr[49].ToString();
                    dto.VendedorID = int.Parse(dr[50].ToString() == "" ? "-1" : dr[50].ToString());
                    dto.FuncionarioID = dr[51].ToString();
                    dto.SocialName = dr[52].ToString();
                    dto.CreatedDate = DateTime.Parse(dr[53].ToString());
                    dto.ValorFrete = dr["FAT_VALOR_FRETE"].ToString() != "" ? decimal.Parse(dr["FAT_VALOR_FRETE"].ToString()) : 0;
                    dto.ValorSeguro = dr["FAT_VALOR_SEGURO"].ToString() != "" ? decimal.Parse(dr["FAT_VALOR_SEGURO"].ToString()) : 0;
                    dto.ValorIVAImportacao = dr["FAT_IVA_IMPORTACAO"].ToString() != "" ? decimal.Parse(dr["FAT_IVA_IMPORTACAO"].ToString()) : 0;
                    dto.ValorAduaneiro = dr["FAT_VALOR_ADUANEIRA"].ToString() != "" ? decimal.Parse(dr["FAT_VALOR_ADUANEIRA"].ToString()) : 0;
                    dto.HonorarioDespachante = dr["FAT_VALOR_HONORARIO_DESPACHANTE"].ToString() != "" ? decimal.Parse(dr["FAT_VALOR_HONORARIO_DESPACHANTE"].ToString()) : 0;
                    dto.TransportacaoLocal = dr["FAT_VALOR_TRANSPORTACAO_LOCAL"].ToString() != "" ? decimal.Parse(dr["FAT_VALOR_TRANSPORTACAO_LOCAL"].ToString()) : 0;
                    dto.OutrosEncargos = dr["FAT_OUTROS_ENCARGOS"].ToString() != "" ? decimal.Parse(dr["FAT_OUTROS_ENCARGOS"].ToString()) : 0;
                    dto.TotalEncargos = dr["FAT_TOTAL_ENCARGOS"].ToString() != "" ? decimal.Parse(dr["FAT_TOTAL_ENCARGOS"].ToString()) : 0;
                    dto.NIF = dr["ENT_IDENTIFICACAO"].ToString();
                    dto.Requisicao = dr["FAT_REQUISICAO"].ToString();
                    dto.Marca = dr["FAT_MARCA"].ToString();
                    dto.Modelo = dr["FAT_MODELO"].ToString();
                    dto.Chassi = dr["FAT_CHASSI"].ToString();
                    dto.AnoFabrico = dr["FAT_ANO_FABRICO"].ToString();
                    dto.CompanyAddress = dr["MORADA"].ToString();
                    dto.CompanyPhone = dr["ENT_TELEFONE"].ToString();
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                ItemFaturacaoDTO item = new ItemFaturacaoDTO();
                item.Fatura = dto.Codigo;
                dto.ListaArtigos = new ItemCompraDAO().ObterPorFiltro(item);
                dto.Parcelas = ObterParcelas(dto);
            }

            return dto;
        }


        public List<FaturaDTO> ObterParcelas(FaturaDTO dto)
        {
            var lista = new List<FaturaDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_FORNECEDOR_OBTERPARCELAS";

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


                ComandText = "stp_COM_FATURA_FORNECEDOR_OBTERPORFILTRO";

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
                AddParameter("@FORNECEDOR", dto.NomeEntidade == null ? string.Empty : dto.NomeEntidade);
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
                AddParameter("REFERENCIA_FORNECEDOR", dto.DocReferenciaExterna ?? String.Empty);
                AddParameter("ANO", dto.LookupNumericField1);


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
                    dto.Entidade = int.Parse(dr["FAT_CODIGO_FORNECEDOR"].ToString());
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
                    dto.DocReferenciaExterna = dr["FAT_REFERENCIA_FORNECEDOR"].ToString();

                     
                    if (dto.TituloDocumento == "SUPPLIER_V" || dto.TituloDocumento == "SUPPLIER_I")
                    {
                        dto.StatusPagamento = "<span class='label label-success'>" + dto.StatusPagamento + "</span>";
                        int percentagemPaga = (int)Math.Round((dto.ValorPago * 100) / dto.ValorTotal);
                        //((int)dto.ValorPago >= (int)dto.ValorTotal)
                        if ((percentagemPaga > 0 && percentagemPaga < 100))
                        {
                            if (dto.DiasAtrasado <= 0)
                                dto.StatusPagamento = "<span class='badge bg-yellow'>" + new StatusDAO().PaymentStatusList().Where(t => t.Codigo == 2).SingleOrDefault().Descricao + " (" + percentagemPaga + "%)</span>";
                            else
                                dto.StatusPagamento = "<span class='badge bg-red'>" + new StatusDAO().PaymentStatusList().Where(t => t.Codigo == 5).SingleOrDefault().Descricao + " (" + percentagemPaga + "%)</span></div></div>";
                        }
                        else if (percentagemPaga == 0)
                        {
                            if (dto.DiasAtrasado <= 0)
                                dto.StatusPagamento = "<span class='label label-warning'>PENDENTE</span>";
                            else
                                dto.StatusPagamento = "<span class='label label-danger'>" + new StatusDAO().PaymentStatusList().Where(t => t.Codigo == 4).SingleOrDefault().Descricao + "</span>";
                        }
                        else if (dto.ValorPago >= dto.ValorTotal)
                        {
                            dto.StatusPagamento = "<span class='label label-success'>LIQUIDADO(A)</span>";
                        }
                    } 

                    if (dr["FAT_STATUS"].ToString() == "0")
                    {
                        dto.LookupField11 = "<span class='label label-danger'>ANULADO</span>";
                        dto.StatusPagamento = dto.LookupField11;
                    }
                    else
                    {
                        dto.LookupField11 = string.Empty;
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


                ComandText = "stp_COM_FATURA_FORNECEDOR_OBTERPENDENTES";

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
                AddParameter("@FORNECEDOR", dto.NomeEntidade);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@SITUACAO", dto.Activo == true ? 1 : 0);
                AddParameter("@ENTIDADE", dto.Entidade);
                AddParameter("@DOCUMENT_STATUS", dto.StatusDocumento);
                AddParameter("@PAYMENT_STATUS", dto.StatusPagamento);
                AddParameter("@SERIE", dto.Serie == null ? -1 : dto.Serie);
                AddParameter("@WAREHOUSE_ID", dto.Armazem);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@REFERENCIA_FORNECEDOR", dto.DocReferenciaExterna ?? String.Empty);
                AddParameter("ANO", dto.LookupNumericField1);

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

                    dto.Lucro = 0;//new ItemCompraDAO().ObterLucro(dto.Codigo);
                    dto.Validade = DateTime.Parse(dr[10].ToString());
                    dto.Saldo = decimal.Parse(dr[11].ToString());
                    dto.StatusDocumento = int.Parse(dr[12].ToString());
                    dto.StatusPagamento = dr[13].ToString();


                    dto.Utilizador = dr["FAT_CREATED_BY"].ToString();
                    dto.TituloDocumento = dr["DOC_FORMATO"].ToString();
                    dto.LookupField1 = dr[14].ToString();
                    dto.LookupField2 = dr["MOE_SIGLA"].ToString();
                    dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString());
                    dto.Entidade = int.Parse(dr["FAT_CODIGO_FORNECEDOR"].ToString());
                    dto.Parcela = int.Parse(dr["FAT_PARCELA"].ToString() == String.Empty ? "1" : dr["FAT_PARCELA"].ToString());
                    dto.ValorPago = dto.ValorPago - dto.Troco;
                    dto.Saldo = dto.ValorTotal - dto.ValorPago + dto.Troco;
                    dto.DiasAtrasado = DiasPendentes(dto.Validade);
                    dto.DocReferenciaExterna = dr["FAT_REFERENCIA_FORNECEDOR"].ToString();
                    if (dto.ValorPago <= dto.ValorTotal)
                    {
                        _acumulado += dto.Saldo;
                        dto.Lucro = _acumulado;
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

       
        public List<FaturaDTO> ObterSupplierExtract(FaturaDTO dto)
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

                    if (dr["DOC_FORMATO"].ToString() != "INVOICE_E" && dr["DOC_FORMATO"].ToString() != "INVOICE_O")
                    {

                        dto.Codigo = int.Parse(dr[0].ToString());
                        dto.Emissao = DateTime.Parse(dr[1].ToString());
                        dto.LookupField1 = dr[2].ToString();
                        dto.Referencia = dr[3].ToString();
                        dto.DescricaoDocumento = dr[4].ToString();
                        dto.NomeEntidade = dr[5].ToString();
                        dto.ValorTotal = decimal.Parse(dr[6].ToString());
                        dto.ValorPago = decimal.Parse(dr[7].ToString()); // DEBITO
                        dto.Saldo = dto.ValorTotal - dto.ValorPago; // CREDTIO 

                        if (_acumulado == 0)
                        {
                            dto.LookupNumericField1 = dto.ValorPago - dto.Saldo;
                            _acumulado = dto.LookupNumericField1;
                        }
                        else if (dto.ValorPago < dto.Saldo)
                        {
                            dto.LookupNumericField1 = _acumulado - dto.Saldo;
                            _acumulado -= dto.Saldo;
                        }
                        else if (dto.ValorPago > dto.Saldo)
                        {
                            dto.LookupNumericField1 = _acumulado + dto.ValorPago;
                            _acumulado += dto.ValorPago;
                        }



                        dto.Saldo = dto.Saldo > 0 ? -dto.Saldo : 0;
                        dto.TituloDocumento = dr["DOC_FORMATO"].ToString();
                        dto.LookupDate1 = DateTime.Parse(dr["FAT_CREATED_DATE"].ToString());
                        dto.Documento = int.Parse(dr["DOC_CODIGO"].ToString());
                        dto.LookupField2 = dr["MOE_SIGLA"].ToString();
                        dto.Cambio = decimal.Parse(dr["FAT_CAMBIO"].ToString() == string.Empty ? "1" : dr["FAT_CAMBIO"].ToString());
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
                ComandText = "stp_COM_FATURA_FORNECEDOR_GETLASTONE";

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

          

    }
}
