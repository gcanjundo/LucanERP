using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient; 
using Dominio.Seguranca;
using MySql.Data.MySqlClient;
using Dominio.Comercial;
using System.IO;

namespace DataAccessLayer.Seguranca
{
    public class ConfiguracaoDAO
    {
        private readonly ConexaoDB DBConexao;
        public ConfiguracaoDAO()
        {
            DBConexao = new ConexaoDB();
        }
        public ConfiguracaoDTO SysConfigAdd(ConfiguracaoDTO dto) 
        {
            try
            {
                DBConexao.ComandText = "stp_SIS_CONFIGURACAO_ADICIONAR";

                DBConexao.AddParameter("@FILIAL", dto.Filial);
                DBConexao.AddParameter("@IDIOMA", dto.Idioma);
                DBConexao.AddParameter("@COUNTRY", dto.Country);
                DBConexao.AddParameter("@CURRENCY", dto.Currency);
                DBConexao.AddParameter("@ENDERECO", dto.DefaultAddressOnSalesDocuments);
                DBConexao.AddParameter("@WAREHOUSE", dto.DefaultWarehouse);
                DBConexao.AddParameter("@ACCOUNT", dto.PosDefaultAccount);
                DBConexao.AddParameter("@DOCUMENT", dto.PosSalesDefaultDocument);
                DBConexao.AddParameter("@PAYMENT", dto.PosDefaultPaymentDeadLine);
                DBConexao.AddParameter("@EXPEDITION", dto.PosDefaultExpedition);
                DBConexao.AddParameter("@CUSTOMER", dto.PosDefaultCustomer);
                DBConexao.AddParameter("@DOCUMENT_STATUS", dto.PosDocumentStatus);
                DBConexao.AddParameter("@AFTER_SALE", dto.PosPageAfterSale);
                DBConexao.AddParameter("@KEYBOARD", dto.PosShowkeyBoard);
                DBConexao.AddParameter("@CATEGORY", dto.PosDefaultProductCategory);
                DBConexao.AddParameter("@LISTED", dto.PosNumberListedProduct);
                DBConexao.AddParameter("@PAYMENT_STATUS", dto.PaymentStatus);
                DBConexao.AddParameter("@OPERATION", dto.POSOpenClosureMode);
                DBConexao.AddParameter("@CONTROL", dto.PosControlMode);
                DBConexao.AddParameter("@CODE_CONFIRMATION", dto.PosAllowCodeConfirmation);
                DBConexao.AddParameter("@MULTIPLES_CLOSES", dto.PosAllowMultipleCloses);
                DBConexao.AddParameter("@PRINTER_NAME", dto.PosPrinterName);
                DBConexao.AddParameter("@KITCHEN_MONITOR", dto.KitchenMonitorActive);
                DBConexao.AddParameter("@PRINTER_KITCHEN_NAME", dto.RestKitchenPrinterName);
                DBConexao.AddParameter("@OPENING_DOCUMENT_ID", dto.DefaultPOSOpenDocumentID);
                DBConexao.AddParameter("@CLOSURE_DOCUMENT_ID", dto.DefaultPosClosureDocumentID);
                DBConexao.AddParameter("@DEFAULT_IN_DOCUMENT_ID", dto.DefaultPOSIncomeDocumentID);
                DBConexao.AddParameter("@DEFAULT_OUT_DOCUMENT_ID", dto.DefaultPOsOutDocumentID);
                DBConexao.AddParameter("@ALLOW_CHANGE_PVP", dto.AllowAutomaticChangePVP == true ? 1 : 0);
                DBConexao.AddParameter("@RETENTION_TAX", dto.TaxaNormalRetencao);
                DBConexao.AddParameter("@INVOCE_LOGO", dto.InvoiceWithLogo);
                DBConexao.AddParameter("@INVOCE_ACUMULA_QUANTIDADE", dto.InvoiceAcumulaQuantidade);

                DBConexao.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally 
            {
                DBConexao.FecharConexao();
            }

            return dto;
        
        } 
        

        public ConfiguracaoDTO ObterConfiguracaoActiva(EmpresaDTO pFilial) 
        {

            ConfiguracaoDTO dto = new ConfiguracaoDTO();

            try
            {

                DBConexao.ComandText = "stp_SIS_CONFIGURACAO_ACTIVA";
                DBConexao.AddParameter("@CODIGO", pFilial.Codigo);
                MySqlDataReader dr = DBConexao.ExecuteReader();

                if (dr.Read())
                {
                    dto.Filial = dr["SIS_CODIGO_FILIAL"].ToString();
                    dto.Idioma = int.Parse(dr["SIS_CODIGO_IDIOMA"].ToString());
                    dto.Country = int.Parse(dr["SIS_PAIS_PADRAO"].ToString());
                    dto.Currency = int.Parse(dr["SIS_MOEDA_PADRAO"].ToString());
                    dto.DefaultAddressOnSalesDocuments = dr["SIS_ENDERECO_RECIBO"].ToString();
                    dto.DefaultWarehouse = int.Parse(dr["SIS_WAREHOUSE_DEFAULT"].ToString());
                    dto.DefaultPaymentMethodID = int.Parse(dr["SIS_DEFAULT_PAYMENT_METHOD"].ToString());
                    dto.StockMode = dr["SIS_STOCK_LOTE_MODE"].ToString() == string.Empty ? "NA" : dr["SIS_STOCK_LOTE_MODE"].ToString();
                    dto.NotifyStockLevel = dr["SIS_STOCK_LEVEL_NOTIFICATION"].ToString() == "1" ? true : false;
                    dto.ExpirationDateNotify = dr["SIS_STOCK_EXPIRE_DATE_NOTIFICATION"].ToString() == "1" ? true : false;
                    dto.StartExpireNotification = int.Parse(dr["SIS_STOCK_EXPIRE_DATE_NOTIFICATION"].ToString() == "" ? "30" : dr["SIS_STOCK_EXPIRE_DATE_NOTIFICATION"].ToString());
                    dto.AllowSalesOnlyValidLote = dr["SIS_STOCK_EXPIRE_DATE_NOTIFICATION"].ToString() == "1" ? true : false;
                    dto.PosDefaultAccount = dr["POS_CONFIG_ACCOUNT"].ToString();
                    dto.PosSalesDefaultDocument = int.Parse(dr["POS_CONFIG_DOCUMENT"].ToString());
                    dto.PosDefaultPaymentDeadLine = int.Parse(dr["POS_CONFIG_PAYMENT"].ToString());
                     
                    dto.PosDefaultExpedition = int.Parse(dr["POS_CONFIG_EXPEDITION"].ToString());
                    dto.PosDefaultCustomer = int.Parse(dr["POS_CONFIG_CUSTOMER"].ToString());
                    dto.PosDocumentStatus = int.Parse(dr["POS_CONFIG_DOCUMENT_STATUS"].ToString());
                    dto.PosPageAfterSale = dr["POS_CONFIG_PAGE_AFTER_SALE"].ToString(); 
                    dto.PosShowkeyBoard = dr["POS_CONFIG_KEYBOARD"].ToString() == "1" ? true: false;

                    dto.PosDefaultProductCategory = int.Parse(dr["POS_CONFIG_CATEGORY"].ToString());
                    dto.PosNumberListedProduct = int.Parse(dr["POS_CONFIG_TOTAL_PRODUCT"].ToString());
                    dto.PosDefaultWarehouse = int.Parse(dr["POS_CONFIG_WAREHOUSE"].ToString());
                    dto.PaymentStatus = int.Parse(dr["POS_CONFIG_PAYMENT_STATUS"].ToString());
                    dto.POSOpenClosureMode = dr["POS_CONFIG_OPERATION_MODE"].ToString(); // Modo de Abertura ou Fecho dos POS ( C - Centralizado ou D - Distribuido)
                    dto.PosControlMode = dr["POS_CONFIG_CONTROL_MODE"].ToString(); // Modo do Controlo dos POS U-Por Utilizador;  C-Pelo Nome do POS ; I-Pelo IP da Máquina
                    dto.PosAllowCodeConfirmation = int.Parse(dr["POS_CONFIG_CODE_CONFIRMATION"].ToString());
                    dto.PosAllowMultipleCloses = int.Parse(dr["POS_CONFIG_MULTIPLES_CLOSES"].ToString());
                    dto.PosPrinterName = dr["POS_CONFIG_PRINTER_NAME"].ToString();
                    dto.KitchenMonitorActive = dr["POS_KITECHEN_MONITOR"].ToString() == "1" ? true : false;
                    dto.RestKitchenPrinterName = dr["POS_KITECHEN_PRINTER_NAME"].ToString();
                    EmpresaDTO objFilial = new EmpresaDTO
                    {
                        NomeCompleto = dr["FIL_NOME_COMERCIAL"].ToString(),
                        NomeComercial = dr["FIL_RAZAO_SOCIAL"].ToString(),
                        PathFoto = dr["ENT_FOTOGRAFIA_PATH"].ToString(),
                        Categoria = dr["FIL_CATEGORIA"].ToString(),
                        Identificacao = dr["ENT_IDENTIFICACAO"].ToString(),
                        Rua = dr["ENT_RUA"].ToString(),
                        Bairro = dr["ENT_BAIRRO"].ToString(),
                        Telefone = dr["ENT_TELEFONE"].ToString(),
                        TelefoneAlt = dr["ENT_TELEFONE_ALTERNATIVO"].ToString(),
                        Email = dr["ENT_EMAIL"].ToString(),
                        WebSite = dr["ENT_WEBSITE"].ToString(),
                        Municipio = dr["MUN_DESCRICAO"].ToString(),
                        Provincia = dr["PROV_DESCRICAO"].ToString(),
                        ProvinciaMorada = int.Parse(dr["PROV_CODIGO"].ToString()),
                        CustomerFiscalCodeID = dr["FIL_SUJEITO_PASSIVO"].ToString(),
                        CompanyLogo = dr["ENT_FOTOGRAFIA_PATH"].ToString(),
                    };
                    dto.CurrencySimbol = dr["PAI_MOEDA"].ToString(); 
                    dto.TituloDocumento = dr["DOC_DESCRICAO"].ToString();
                    
                    dto.BranchDetails = objFilial;
                    dto.CompanyName = dr["FIL_RAZAO_SOCIAL"].ToString();
                    dto.CompanyCity = dr["PROV_DESCRICAO"].ToString();
                    dto.DefaultPOSOpenDocumentID = dr["POS_OPENING_DOCUMENT_ID"].ToString();
                    dto.DefaultPosClosureDocumentID = dr["POS_CLOSURE_DOCUMENT_ID"].ToString();
                    dto.DefaultPOSIncomeDocumentID = dr["POS_DEFAULT_IN_DOCUMENT_ID"].ToString();
                    dto.DefaultPOsOutDocumentID = dr["POS_DEFAULT_OUT_DOCUMENT_ID"].ToString();
                    dto.RestModalidade = dr["SIS_REST_MODALIDADE"].ToString();
                    dto.StockIncomeSerieID = dr["SIS_STOCK_INCOME_SERIE_ID"].ToString()!="" ? int.Parse(dr["SIS_STOCK_INCOME_SERIE_ID"].ToString()) :-1;
                    dto.StockOutcomeSerieID = dr["SIS_STOCK_OUTCOME_SERIE_ID"].ToString()!="" ? int.Parse(dr["SIS_STOCK_OUTCOME_SERIE_ID"].ToString()) : -1;
                    dto.AllowAutomaticChangePVP = dr["SIS_CHANGE_PVP_ONPURCHAGE"].ToString() == "1" ? true : false;
                    dto.TaxaNormalRetencao = dr["SIS_RETENTION_TAX"].ToString() != "" ? decimal.Parse(dr["SIS_RETENTION_TAX"].ToString()) : 0;
                    dto.InvoiceWithLogo = dr["SIS_INVOICE_WITH_LOGO"].ToString() != "1" ? false : true;
                    dto.BackupPath = dr["SIS_BACK_PATH"].ToString() == "" ? "C:\\data": dr["SIS_BACK_PATH"].ToString();
                    dto.InvoiceAcumulaQuantidade = dr["SIS_ACUMULA_QUANTIDADE"].ToString() !="1" ? false : true;
                    dto.MargemLucroPadrao = dr["SIS_MARGEM_LUCRO_PADRAO"].ToString() != "" ? decimal.Parse(dr["SIS_MARGEM_LUCRO_PADRAO"].ToString()) : 0;
                    dto.Sucesso = true;
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                DBConexao.FecharConexao();
            }
            return dto;
        }

        public ConfiguracaoDTO SavePosSettings(ConfiguracaoDTO dto)
        {
            try
            {
                DBConexao.ComandText = "stp_SIS_CONFIGURACAO_ADICIONAR";

                DBConexao.AddParameter("@FILIAL", dto.Filial);
                DBConexao.AddParameter("@IDIOMA", dto.Idioma);
                DBConexao.AddParameter("@COUNTRY", dto.Country);
                DBConexao.AddParameter("@CURRENCY", dto.Currency);
                DBConexao.AddParameter("@ENDERECO", dto.DefaultAddressOnSalesDocuments);
                DBConexao.AddParameter("@WAREHOUSE", dto.DefaultWarehouse);
                DBConexao.AddParameter("@ACCOUNT", dto.PosDefaultAccount);
                DBConexao.AddParameter("@DOCUMENT", dto.PosSalesDefaultDocument);
                DBConexao.AddParameter("@PAYMENT", dto.PosDefaultPaymentDeadLine);
                DBConexao.AddParameter("@EXPEDITION", dto.PosDefaultExpedition);
                DBConexao.AddParameter("@CUSTOMER", dto.PosDefaultCustomer);
                DBConexao.AddParameter("@DOCUMENT_STATUS", dto.PosDocumentStatus);
                DBConexao.AddParameter("@AFTER_SALE", dto.PosPageAfterSale);
                DBConexao.AddParameter("@KEYBOARD", dto.PosShowkeyBoard);
                DBConexao.AddParameter("@CATEGORY", dto.PosDefaultProductCategory);
                DBConexao.AddParameter("@LISTED", dto.PosNumberListedProduct);
                DBConexao.AddParameter("@PAYMENT_STATUS", dto.PaymentStatus);
                DBConexao.AddParameter("@OPERATION", dto.POSOpenClosureMode);
                DBConexao.AddParameter("@CONTROL", dto.PosControlMode);
                DBConexao.AddParameter("@CODE_CONFIRMATION", dto.PosAllowCodeConfirmation);
                DBConexao.AddParameter("@MULTIPLES_CLOSES", dto.PosAllowMultipleCloses);
                DBConexao.AddParameter("@PRINTER_NAME", dto.PosPrinterName);
                DBConexao.AddParameter("@KITCHEN_MONITOR", dto.KitchenMonitorActive);
                DBConexao.AddParameter("@PRINTER_KITCHEN_NAME", dto.RestKitchenPrinterName);
                DBConexao.AddParameter("@OPENING_DOCUMENT_ID", dto.DefaultPOSOpenDocumentID);
                DBConexao.AddParameter("@CLOSURE_DOCUMENT_ID", dto.DefaultPosClosureDocumentID);
                DBConexao.AddParameter("@DEFAULT_IN_DOCUMENT_ID", dto.DefaultPOSIncomeDocumentID);
                DBConexao.AddParameter("@DEFAULT_OUT_DOCUMENT_ID", dto.DefaultPOsOutDocumentID);
                DBConexao.AddParameter("@DEFAULT_IN_STOCK_SERIE_ID", dto.StockIncomeSerieID);
                DBConexao.AddParameter("@DEFAULT_OUT_STOCK_SERIE_ID", dto.StockOutcomeSerieID);

                DBConexao.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                DBConexao.FecharConexao();
            }

            return dto;

        }
        
        public Tuple<bool, string> ExecuteBackup(ConfiguracaoDTO dto) 
        {
            string errorMessage = "";
            bool sucesso = true;
            try
            {


                if (!Directory.Exists(dto.BackupPath))
                    Directory.CreateDirectory(dto.BackupPath);

                dto.BackupPath = dto.BackupPath + "\\BACKUP" + DateTime.Today.AddDays(-1).Day + "" + DateTime.Today.Month + "" + DateTime.Today.Year.ToString() + ".sql";
                if (!File.Exists(dto.BackupPath))
                {
                    using (MySqlConnection conn = new MySqlConnection(DBConexao.StringConnection))
                    {
                        
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            /*
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportToFile(dto.BackupPath);
                                conn.Close();
                            }*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                errorMessage = ex.Message.Replace("'", "");
                sucesso = false;
            }
            finally
            {
                DBConexao.FecharConexao();
            }

            return new Tuple<bool, string>(sucesso, errorMessage);
        }

        public ConfiguracaoDTO SaveClinicalSettings(ConfiguracaoDTO dto)
        {
            try
            {
                DBConexao.ComandText = "stp_CLI_CONFIGURACAO_ADICIONAR";

                DBConexao.AddParameter("@FILIAL", dto.Filial); 

                DBConexao.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                DBConexao.FecharConexao();
            }

            return dto;

        }

        public AcessoDTO ObterConfiguracaoClinica(AcessoDTO dto)
        { 
            try
            {

                DBConexao.ComandText = "stp_CLI_CONFIGURACAO_OBTERPORFILTRO";
                
                DBConexao.AddParameter("@COMPANY_ID", dto.Filial);

                MySqlDataReader dr = DBConexao.ExecuteReader();

                if (dr.Read())
                {
                    dto.Filial = dr["CLI_FILIAL_ID"].ToString();
                    dto.Settings.HorarioInicioP1 = DateTime.Parse(dr["CLI_HORA_INICIO_P1"].ToString());
                    dto.Settings.HorarioTerminoP1 = DateTime.Parse(dr["CLI_HORA_TERMINO_P1"].ToString());
                    dto.Settings.DuracaoAtendimento = int.Parse(dr["CLI_DURACAO_ATENDIMENTO"].ToString());
                    dto.Settings.ValorActoProfissional = decimal.Parse(dr["CLI_VALOR_PROFISSIONAL"].ToString());
                    dto.Settings.PercentagemActoProfissional = decimal.Parse(dr["CLI_VALOR_CLINICA"].ToString());
                    dto.Settings.DiaInicial = int.Parse(dr["CLI_DIA_INICIAL"].ToString());
                    dto.Settings.DiaFinal = int.Parse(dr["CLI_DIA_FINAL"].ToString());
                    dto.Settings.HorarioInicioP2 = DateTime.Parse(dr["CLI_HORA_INICIO_P2"].ToString());
                    dto.Settings.HorarioTerminoP2 = DateTime.Parse(dr["CLI_HORA_TERMINO_P2"].ToString());
                    dto.Settings.HorarioInicioP3 = DateTime.Parse(dr["CLI_HORA_INICIO_P3"].ToString());
                    dto.Settings.HorarioTerminoP3 = DateTime.Parse(dr["CLI_HORA_TERMINO_P3"].ToString());
                    dto.Settings.HorarioInicioP4 = DateTime.Parse(dr["CLI_HORA_INICIO_P4"].ToString());
                    dto.Settings.HorarioTerminoP4 = DateTime.Parse(dr["CLI_HORA_TERMINO_P4"].ToString());
                }

            }
            catch (Exception ex)
            {
                dto.Settings.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                DBConexao.FecharConexao();
            }
            return dto;
        }

    }
}
