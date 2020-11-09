using Dominio.Tesouraria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Tesouraria
{
    public class ContaPagarReceberDAO : ConexaoDB
    {
        public ContaPagarReceberDTO Inserir(ContaPagarReceberDTO dto)
        { 
            try
            {
                ComandText = "stp_FIN_CONTA_PAGAR_RECEBER_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@ENTIDADE", dto.Entidade); 
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@REFERENCIA", dto.Referencia); 
                AddParameter("@EMISSAO", dto.Emissao);
                AddParameter("@VENCIMENTO", dto.Vencimento); 
                AddParameter("@PARCELA_ID", dto.Parcela);
                AddParameter("@ESTADO", dto.Status);
                AddParameter("@TITULO", dto.TituloDocumento);
                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@NATUREZA", dto.Natureza);
                AddParameter("@RUBRICA_ID", dto.RubricaID);
                AddParameter("@VALOR", dto.Valor * dto.Cambio);
                AddParameter("@UTILIZADOR", dto.Utilizador);  
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@CAMBIO", dto.Cambio);
                AddParameter("@VALOR_INCIDENCIA", dto.ValorIncidencia);
                AddParameter("@VALOR_DESCONTO", dto.ValorDesconto);
                AddParameter("@VALOR_IVA", dto.ValorIVA);
                AddParameter("@RETENCAO", dto.ValorRetencao);
                AddParameter("@ISREAL", dto.IsReal);
                ExecuteNonQuery();
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

        public ContaPagarReceberDTO Excluir(ContaPagarReceberDTO dto)
        {
            try
            {
                ComandText = "stp_FIN_CONTA_PAGAR_RECEBER_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo); 

                ExecuteNonQuery();
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

        public Tuple<int, int> ImportarPendentesComercial(ContaPagarReceberDTO dto)
        {
            int totalCustomerInvoices=0, totalSupplierInvoices=0;
            try
            {
                ComandText = "stp_FIN_CONTA_PAGAR_RECEBER_IMPORTAR_PENDENTES";

                AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();
                if (dr.Read())
                {
                    totalCustomerInvoices = int.Parse(dr[0].ToString());
                    totalSupplierInvoices = int.Parse(dr[1].ToString());
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

            return new Tuple<int, int>(totalCustomerInvoices, totalSupplierInvoices);
        }

        public List<ContaPagarReceberDTO> ObterPorFiltro(ContaPagarReceberDTO dto)
        {
            List<ContaPagarReceberDTO> lista = new List<ContaPagarReceberDTO>();

            try
            {
                ComandText = "stp_FIN_CONTA_PAGAR_RECEBER_OBTERPORFILTRO";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@DOCUMENTO", dto.Documento);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@ENTIDADE", dto.Entidade);
                AddParameter("@RUBRICA", dto.RubricaID);
                AddParameter("@EMISSAO_INI", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("@EMISSAO_TERM", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);
                AddParameter("@VENCIMENTO_INI", dto.LookupDate3 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate3);
                AddParameter("@VENCIMENTO_TERM", dto.LookupDate4 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate4);
                AddParameter("@FILIAL", dto.Filial);

                decimal Saldo = 0;
                MySqlDataReader dr = ExecuteReader(); 
                while (dr.Read())
                {
                    dto = new ContaPagarReceberDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Filial = dr[1].ToString(),
                        Entidade = int.Parse(dr[2].ToString()),
                        Documento = int.Parse(dr[3].ToString() == "" ? "-1" : dr[3].ToString()),
                        Referencia = dr[4].ToString(),
                        Emissao = DateTime.Parse(dr[5].ToString()),
                        Vencimento = DateTime.Parse(dr[6].ToString()),
                        Parcela = int.Parse(dr[7].ToString()),
                        Status = int.Parse(dr[8].ToString()),
                        TituloDocumento = dr[9].ToString(),
                        Descricao = dr[10].ToString(),
                        Natureza = dr[11].ToString(),
                        RubricaID = int.Parse(dr[12].ToString() == "" ? "-1" : dr[12].ToString()),
                        Valor = decimal.Parse(dr[13].ToString()),
                        Pendente = decimal.Parse(string.IsNullOrEmpty(dr[14].ToString()) ? "0" : dr[14].ToString()),
                        Moeda = int.Parse(dr[19].ToString()),
                        Cambio = decimal.Parse(dr[20].ToString()),
                        ValorIncidencia = decimal.Parse(string.IsNullOrEmpty(dr[21].ToString()) ? "0" : dr[21].ToString()),
                        ValorDesconto = decimal.Parse(string.IsNullOrEmpty(dr[22].ToString()) ? "0" : dr[22].ToString()),
                        ValorIVA = decimal.Parse(string.IsNullOrEmpty(dr[23].ToString()) ? "0" : dr[23].ToString()),
                        ValorRetencao = decimal.Parse(string.IsNullOrEmpty(dr[24].ToString()) ? "0" : dr[24].ToString()),
                        IsReal = int.Parse(string.IsNullOrEmpty(dr[25].ToString()) ? "0" : dr[25].ToString()),
                        DesignacaoEntidade = dr[26].ToString(),
                        LookupField1 = dr[27].ToString(), // Descricao da Rubrica
                        LookupField2 = dr[28].ToString(), // Nome do Documento
                        LookupField3 = dr[29].ToString(), // Sigla da Moeda
                        Credito = dr[11].ToString() == "R" ? decimal.Parse(dr[13].ToString()) : 0,
                        Debito = dr[11].ToString() == "P" ? decimal.Parse(dr[13].ToString()) : 0,
                    };
                    Saldo = Saldo + (dto.Credito - dto.Debito);
                    dto.Saldo = Saldo;
                    dto.LookupField4 = dto.Status == 1 ? "ABERTA " : " LIQUIDADA";
                    dto.LookupField4 += dto.IsReal == 1 ? " - REAL" : " PREVISTA";
                    dto.Dias = (int)DateTime.Today.Subtract(dto.Vencimento).TotalDays;
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
