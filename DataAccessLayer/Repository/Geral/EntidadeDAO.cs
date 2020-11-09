using System;
using System.Collections.Generic;
using System.Linq;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dominio.Geral;
using Dominio.Tesouraria;
using Dominio.Comercial.Restauracao;
using DataAccessLayer.Clinica;

namespace DataAccessLayer.Geral
{
    public class EntidadeDAO
    {
        static ConexaoDB BaseDados = new ConexaoDB();


        public EntidadeDTO Adicionar(EntidadeDTO dto)
        {


            try
            {

                BaseDados.ComandText = "stp_GER_ENTIDADE_ADICIONAR";
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("RAZAO", dto.NomeCompleto);
                BaseDados.AddParameter("FANTASIA", dto.NomeComercial);

                if (!string.IsNullOrEmpty(dto.Categoria) && dto.Categoria!="-1")
                {
                    BaseDados.AddParameter("CATEGORIA", dto.Categoria);
                }
                else
                {
                    BaseDados.AddParameter("CATEGORIA", DBNull.Value);
                }
                    
                BaseDados.AddParameter("ENTIDADE", dto.Tipo);

                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);


                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("PAIS", dto.Nacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("PAIS", DBNull.Value);
                }
                BaseDados.AddParameter("RUA", dto.Rua);

                BaseDados.AddParameter("BAIRRO", dto.Bairro);

                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("MORADA", dto.MunicipioMorada);
                }
                else
                {
                    BaseDados.AddParameter("MORADA", DBNull.Value);
                }

                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("FAX", dto.TelefoneFax);
                BaseDados.AddParameter("EMAIL", dto.Email);
                BaseDados.AddParameter("WEBSITE", dto.WebSite);
                BaseDados.AddParameter("DESCONTO", dto.Desconto);
                BaseDados.AddParameter("LIMITE_CREDITO", dto.LimiteCredito);
                BaseDados.AddParameter("FILIAL", dto.Filial =="-1" ? (object)DBNull.Value : dto.Filial);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@DISTRITO", dto.Morada);
                BaseDados.AddParameter("@CAIXA_POSTAL", dto.CaixaPostal);

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


        public EntidadeDTO Excluir(EntidadeDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_GER_ENTIDADE_EXCLUIR";

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

            return dto;
        }

        public void AddCustomerSupplierData(EntidadeDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_GER_TERCEIRO_ADICIONAR";

                BaseDados.AddParameter("ENTIDADE", dto.Codigo);

                if (!string.IsNullOrEmpty(dto.Categoria) && dto.Categoria != "-1")
                {
                    BaseDados.AddParameter("CATEGORIA", dto.Categoria);
                }
                else
                {
                    BaseDados.AddParameter("CATEGORIA", DBNull.Value);
                }

                BaseDados.AddParameter("FILIAL", dto.Filial == "-1" ? (object)DBNull.Value : dto.Filial);
                BaseDados.AddParameter("LIMITE_CREDITO", dto.LimiteCredito); 
                BaseDados.AddParameter("SALDO", 0); 
                BaseDados.AddParameter("DESCONTO", dto.Desconto); 
                BaseDados.AddParameter("PAYMENT_METHOD", dto.PaymentMethod == "-1" ? (object)DBNull.Value : dto.PaymentMethod);
                BaseDados.AddParameter("PAYMENT_TERMS", dto.PaymentTerms == "-1" ? (object)DBNull.Value : dto.PaymentTerms);
                BaseDados.AddParameter("LINE_DISCOUNT", dto.DescontoLinha);
                BaseDados.AddParameter("VENCIMENTO", dto.DiasVencimento);
                BaseDados.AddParameter("PRAZO_LIMITE", dto.PaymentDays);

                BaseDados.AddParameter("CURRENCY", dto.Currency == "-1" ? (object)DBNull.Value : dto.Currency);
                BaseDados.AddParameter("SALEMAN_ID", dto.AccountManager == "-1" ? (object)DBNull.Value : dto.AccountManager);
                BaseDados.AddParameter("CHARGESMAN_ID", dto.ChargesManager == "-1" ? (object)DBNull.Value : dto.ChargesManager);
                BaseDados.AddParameter("BANK_ID", dto.Banco == "-1" ? (object)DBNull.Value : dto.Banco); 
                BaseDados.AddParameter("ACCOUNT_ID", dto.AccountBank);
                BaseDados.AddParameter("IBAN", dto.IBAN);
                BaseDados.AddParameter("SWIFT", dto.Swift); 
                BaseDados.AddParameter("TABLE_PRICE_ID", dto.TablePriceID == "-1" ? (object)DBNull.Value : dto.TablePriceID);

                BaseDados.AddParameter("@COMERCIAL_PEOPLE", dto.ComercialContactName);
                BaseDados.AddParameter("@COMERCIAL_FUNCTION", dto.ComercialContactFunction);
                BaseDados.AddParameter("@COMERCIAL_PHONE", dto.ComercialContactPhoneNumber);
                BaseDados.AddParameter("@COMERCIAL_EMAIL", dto.ComercialContactEmail);

                BaseDados.AddParameter("@CHARGES_PEOPLE", dto.ChargesContactName);
                BaseDados.AddParameter("@CHARGES_FUNCTION", dto.ChargesContactFunction);
                BaseDados.AddParameter("@CHARGES_PHONE", dto.ChargesContactPhoneNumber);
                BaseDados.AddParameter("@CHARGES_EMAIL", dto.ChargesContactEmail);

                BaseDados.AddParameter("@ADMINISTRATIVE_PEOPLE", dto.AdministrativeContactName);
                BaseDados.AddParameter("@ADMINISTRATIVE_FUNCTION", dto.AdministrativeContactFunction);
                BaseDados.AddParameter("@ADMINISTRATIVE_PHONE", dto.AdministrativeContactPhoneNumber);
                BaseDados.AddParameter("@ADMINISTRATIVE_EMAIL", dto.AdministrativeContactEmail);
                BaseDados.AddParameter("@EXPEDITION_ID", dto.ModoExpedicao);
                BaseDados.AddParameter("@ALLOW_ALERT", dto.AllowAlert ? 1 : 0);
                BaseDados.AddParameter("@ALERT_DAYS", dto.AlertDays);
                BaseDados.AddParameter("@PEDING_VALUE_ALERT", dto.AlertPendingValue ? 1 : 0); 
                BaseDados.AddParameter("@CUSTOMER_IVA_CODE", dto.CustomerFiscalCodeID == "-1" ? (object)DBNull.Value : dto.CustomerFiscalCodeID);
                BaseDados.AddParameter("@ANGARIADOR_ID", dto.AngariadorID <=0 ?(object)DBNull.Value : dto.AngariadorID); 
                BaseDados.AddParameter("@ALLOW_RETENTION", dto.RetencaoID);  
                BaseDados.AddParameter("@CUSTOMER_ACCOUNT_PLAN_ID", dto.CustomerAccountPlanID <= 0 ? (object)DBNull.Value : dto.CustomerAccountPlanID);
                BaseDados.AddParameter("@SUPPLIER_ACCOUNT_PLAN_ID", dto.SupplierAccountPlanID <= 0 ? (object)DBNull.Value : dto.SupplierAccountPlanID); 
                BaseDados.AddParameter("@SELFBILLING", dto.AllowSefBilling ? 1 : 0);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@ISCOMPANY_INSURANCE", dto.IsCompanyInsurance);
                BaseDados.AddParameter("@PREFERENCIAL_CONTACT", dto.CanalContactoPreferencial);
                BaseDados.AddParameter("@HOW_FINDING_US", dto.CanalAngariacao);
                BaseDados.AddParameter("@BIRTH_DATE", dto.BirthDate);
                BaseDados.AddParameter("@IS_ACTIVO", dto.Status);
                BaseDados.AddParameter("@SUPPLIER_IVA_CODE", dto.SupplierFiscalCodeID == "-1" ? (object)DBNull.Value : dto.SupplierFiscalCodeID);

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
                
                if(dto.ApoliceSeguro!=null && dto.ApoliceSeguro.Convenio > 0)
                {
                    dto.ApoliceSeguro.Beneficiario = dto.Codigo;
                    new ApoliceDAO().Adicionar(dto.ApoliceSeguro); 
                }
            }

 
        }

        public List<EntidadeDTO> ObterPorFiltro(EntidadeDTO dto)
        {
            List<EntidadeDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_GER_ENTIDADE_OBTERPORFILTRO";

                BaseDados.AddParameter("NOME", dto.NomeCompleto);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EntidadeDTO>();
                while (dr.Read())
                {
                    dto = new EntidadeDTO();


                    dto.Codigo =int.Parse(dr[0].ToString());
                    dto.Identificacao = dr[1].ToString();
                    dto.NomeCompleto = dr[2].ToString();
                    dto.Morada = dr[3].ToString().ToUpper();
                    dto.Telefone = dr[4].ToString();
                    dto.Email = dr[5].ToString();
                    dto.WebSite = dr[6].ToString();
                    dto.TelefoneAlt = dr[7].ToString();
                    dto.Tipo = dr[8].ToString();
                    dto.Categoria = dr[9].ToString();
                    decimal _saldo = decimal.Parse(dr[10].ToString().Replace(".", ","));
                    
                    dto.Saldo = String.Format("{0:N2}", _saldo);

                    dto.EntityType = new TipoDAO().GetEntityTypeList().Where(t => t.Sigla == (dr[8].ToString() == "" ? "A" : dr[8].ToString())).SingleOrDefault();
                    dto.Tipo = dto.EntityType.Sigla;
                    dto.SocialName = dto.EntityType.Descricao;

                    dto.Entidade = int.Parse(dr[11].ToString());
                    dto.Desconto = dr[12].ToString() != "" ? decimal.Parse(dr[12].ToString()) : 0;
                    dto.PaymentMethod = dr[13].ToString() != "" ? dr[13].ToString() : "-1";
                    dto.PaymentTerms = dr[14].ToString() != "" ? dr[14].ToString() : "-1";
                    dto.DescontoLinha = dr[15].ToString() != "" ? decimal.Parse(dr[15].ToString()) : 0; 
                    dto.LimiteCredito = dr[16].ToString() != "" ? decimal.Parse(dr[16].ToString()) : 0;
                    dto.TablePriceID = dr[17].ToString() != "" ? dr[17].ToString() : "-1";
                    dto.CreatedDate = dr[18].ToString() != "" ? DateTime.Parse(dr[18].ToString()) : DateTime.MinValue; 
                    dto.Filial = dr[19].ToString() != "" ? dr[19].ToString() : "-1";
                    dto.IsCompanyInsurance = dr["TER_COMPANY_INSURANCE"].ToString() != "1" ? false : true;
                   

                    lista.Add(dto);
                }  
            }
            catch (Exception ex)
            {
                dto = new EntidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<EntidadeDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }

        public EntidadeDTO ObterEntidadePorPK(EntidadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_ENTIDADE_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new EntidadeDTO();
                if (dr.Read())
                {

                    dto.Codigo =int.Parse(dr[0].ToString());
                    dto.NomeCompleto = dr[1].ToString();
                    dto.NomeComercial = dr[2].ToString();
                    dto.Categoria = dr[3].ToString();

                     
                    dto.Identificacao = dr[4].ToString();
                    if (!String.IsNullOrEmpty(dr[5].ToString()))
                        dto.Nacionalidade =int.Parse(dr[5].ToString());
                    else
                    dto.Nacionalidade = -1;
                    dto.Rua = dr[6].ToString();
                    dto.Bairro = dr[7].ToString();
                    dto.Provincia = dr[8].ToString();
                    dto.MunicipioMorada =int.Parse(dr[9].ToString());
                    dto.Telefone = dr[10].ToString();
                    dto.TelefoneAlt = dr[11].ToString();
                    dto.Email = dr[12].ToString();
                    dto.WebSite = dr[13].ToString();
                    dto.Tipo = dr[14].ToString();
                    dto.Desconto = decimal.Parse(dr[15].ToString());
                    dto.LimiteCredito = decimal.Parse(dr[16].ToString());
                    dto.Morada = dr[17].ToString();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public List<BancoDTO> ObterBancos(BancoDTO dto)
        {
            List<BancoDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_FIN_BANCO_OBTERPORFILTRO";

                BaseDados.AddParameter("@DESCRICAO", dto.NomeCompleto);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<BancoDTO>();
                while (dr.Read())
                {
                    dto = new BancoDTO();


                    dto.Codigo =int.Parse(dr[0].ToString());
                    dto.NomeComercial = dr[1].ToString().ToUpper(); 
                    dto.NomeCompleto = dr[2].ToString();
                    
                    lista.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto = new BancoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<BancoDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }


        public EntidadeDTO ObterCustomerSupplierPorPK(EntidadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_TERCEIRO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new EntidadeDTO();
                if (dr.Read())
                { 
                    dto.Codigo =int.Parse(dr[0].ToString());
                    dto.Entidade =int.Parse(dr[1].ToString());
                    dto.Categoria = dr[2].ToString() == "" ? "-1" : dr[2].ToString();
                    dto.Filial = dr[3].ToString() == "" ? "-1" : dr[3].ToString();
                    dto.Tipo = dr[4].ToString();
                    dto.NomeCompleto = dr[5].ToString();
                    dto.LimiteCredito = decimal.Parse(dr[6].ToString());
                    dto.Saldo = dr[7].ToString();
                    dto.Desconto = decimal.Parse(dr[8].ToString());
                    dto.PaymentMethod = dr[9].ToString() == "" ? "-1" : dr[9].ToString();
                    dto.PaymentTerms = dr[10].ToString() == "" ? "-1" : dr[10].ToString();
                    dto.DescontoLinha = decimal.Parse(dr[11].ToString() == "" ? "0" : dr[11].ToString());
                    dto.DiasVencimento = dr[12].ToString();
                    dto.PaymentDays = dr[13].ToString();
                    dto.Currency = dr[14].ToString() == "" ? "-1" : dr[14].ToString();
                    dto.AccountManager = dr[15].ToString() == "" ? "-1" : dr[15].ToString();
                    dto.ChargesManager = dr[16].ToString() == "" ? "-1" : dr[16].ToString();
                    dto.Banco = dr[17].ToString() == "" ? "-1" : dr[17].ToString();
                    dto.AccountBank = dr[18].ToString();
                    dto.IBAN = dr[19].ToString();
                    dto.Swift = dr[20].ToString();
                    dto.TablePriceID = dr[21].ToString() == "" ? "-1" : dr[3].ToString();
                    dto.ComercialContactName = dr[22].ToString();
                    dto.ComercialContactFunction = dr[23].ToString();
                    dto.ComercialContactPhoneNumber = dr[24].ToString();
                    dto.ComercialContactEmail = dr[25].ToString();
                    dto.ChargesContactName = dr[26].ToString();
                    dto.ChargesContactFunction = dr[27].ToString();
                    dto.ChargesContactPhoneNumber = dr[28].ToString();
                    dto.ChargesContactEmail = dr[29].ToString();
                    dto.AdministrativeContactName = dr[30].ToString();
                    dto.AdministrativeContactFunction = dr[31].ToString();
                    dto.AdministrativeContactPhoneNumber = dr[32].ToString();
                    dto.AdministrativeContactEmail = dr[33].ToString(); 
                    dto.ModoExpedicao = dr[34].ToString() == "" ? "-1" : dr[34].ToString();
                    dto.CustomerFiscalCodeID = dr["TER_CUSTOMER_IVA_ID"].ToString() == string.Empty ? "-1" : dr["TER_CUSTOMER_IVA_ID"].ToString();
                    dto.AngariadorID = dr[39].ToString() == "" ? -1 : int.Parse(dr[39].ToString()); 
                    dto.NomeComercial = dr[55].ToString();
                    dto.Categoria = dr[56].ToString();


                    dto.Identificacao = dr[57].ToString();
                    if (!String.IsNullOrEmpty(dr[58].ToString()))
                        dto.Nacionalidade =int.Parse(dr[58].ToString());
                    else
                        dto.Nacionalidade = -1;
                    dto.Rua = dr[59].ToString();
                    dto.Bairro = dr[60].ToString();
                    dto.Provincia = dr[61].ToString();
                    dto.MunicipioMorada =int.Parse(dr[62].ToString());
                    dto.Telefone = dr[63].ToString();
                    dto.TelefoneAlt = dr[64].ToString();
                    dto.Email = dr[65].ToString();
                    dto.WebSite = dr[66].ToString(); 
                    dto.Distrito = dr[67].ToString();
                    dto.LookupField1 = dr["ALLOWED_DELETE_NIF"].ToString();
                    dto.IsCompanyInsurance = dr["TER_COMPANY_INSURANCE"].ToString() != "1" ? false : true;
                    dto.CanalContactoPreferencial = dr["TER_PREFERENCIAL_CONTACT"].ToString();
                    dto.CanalAngariacao = dr["TER_HOW_FINDING_US"].ToString();
                    dto.BirthDate = dr["TER_BIRTH_DATE"].ToString();
                    dto.RetencaoID = int.Parse(dr["TER_RETENCAO"].ToString() == "" ? "-1" : dr["TER_RETENCAO"].ToString());
                    dto.RetencaoID = dto.RetencaoID == 0 ? -1 : dto.RetencaoID;
                    dto.NroOrdenacao = int.Parse(dr["TER_ORDER_NUMBER"].ToString() == "" ? "-1" : dr["TER_ORDER_NUMBER"].ToString());
                    dto.Status = int.Parse(dr["TER_STATUS"].ToString() != "0" ? "1" : "0");
                    dto.SupplierFiscalCodeID = dr["TER_SUPPLIER_IVA_ID"].ToString() == string.Empty ? "-1" : dr["TER_SUPPLIER_IVA_ID"].ToString();
                    dto.CaixaPostal = dr["ENT_CAIXA_POSTAL"].ToString();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return dto;
        }

        public List<EntidadeDTO> ObterCustomerPorFiltro(EntidadeDTO dto)
        {
            List<EntidadeDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_GER_TERCEIRO_GETCUSTOMERLIST";

                BaseDados.AddParameter("NOME", dto.NomeCompleto??string.Empty);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EntidadeDTO>();
                while (dr.Read())
                {
                    dto = new EntidadeDTO(); 

                    dto.Codigo =int.Parse(dr[0].ToString());
                    dto.Identificacao = dr[1].ToString();
                    dto.NomeCompleto = dr[2].ToString();
                    dto.Morada = dr[3].ToString().ToUpper();
                    dto.Telefone = dr[4].ToString();
                    dto.Email = dr[5].ToString();
                    dto.WebSite = dr[6].ToString();
                    dto.TelefoneAlt = dr[7].ToString();
                    dto.Tipo = dr[8].ToString();
                    dto.Categoria = dr[9].ToString();
                    decimal _saldo = decimal.Parse(dr[10].ToString().Replace(".", ","));

                    dto.Saldo = String.Format("{0:N2}", _saldo); 
                    
                    if (dto.Tipo == "C")
                    {
                        dto.Tipo = "Cliente";
                        dto.SocialName = "C";
                    }
                    else if (dto.Tipo == "F")
                    {
                        dto.Tipo = "Fonecedor";
                        dto.SocialName = "F";
                    }
                    else
                    {
                        dto.Tipo = "Cliente e Fornecedor";
                        dto.SocialName = "";
                    }
                    dto.Desconto = decimal.Parse(dr[12].ToString() == "" ? "0" : dr[12].ToString());
                    dto.PaymentMethod = dr[13].ToString();
                    dto.PaymentTerms = dr[14].ToString();
                    dto.DescontoLinha = decimal.Parse(dr[15].ToString() == "" ? "0" : dr[15].ToString());
                    dto.LimiteCredito = decimal.Parse(dr[16].ToString() =="" ?  "0" : dr[16].ToString());
                    dto.TablePriceID = dr[17].ToString();
                    dto.CreatedDate = DateTime.Parse(dr[18].ToString());
                    dto.Filial = dr[19].ToString() != "" ? dr[19].ToString() : "-1";
                    dto.Currency = dr[20].ToString() != "" ? dr[20].ToString() : "1";
                    dto.CanalContactoPreferencial = dr["TER_PREFERENCIAL_CONTACT"].ToString();
                    dto.CanalAngariacao = dr["TER_HOW_FINDING_US"].ToString();
                    dto.BirthDate = dr["TER_BIRTH_DATE"].ToString();
                    dto.Entidade = int.Parse(dr["TER_CODIGO"].ToString());
                    dto.RetencaoID = int.Parse(dr["TER_RETENCAO"].ToString() == "" ? "-1" : dr["TER_RETENCAO"].ToString());
                    dto.Status = int.Parse(dr["TER_STATUS"].ToString());
                    dto.CustomerFiscalCodeID = dr["TER_CUSTOMER_IVA_ID"].ToString() == string.Empty ? "-1" : dr["TER_CUSTOMER_IVA_ID"].ToString();

                    lista.Add(dto);
                } 

            }
            catch (Exception ex)
            {
                dto = new EntidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<EntidadeDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }

        public List<EntidadeDTO> ObterSupplierPorFiltro(EntidadeDTO dto)
        {
            List<EntidadeDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_COM_SUPPLIERS_OBTERPORFILTRO";

                BaseDados.AddParameter("NOME", dto.NomeCompleto);

               MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EntidadeDTO>();
                while (dr.Read())
                {
                    dto = new EntidadeDTO();


                    dto.Codigo =int.Parse(dr[0].ToString());
                    dto.Identificacao = dr[1].ToString();
                    dto.NomeCompleto = dr[2].ToString();
                    dto.Morada = dr[3].ToString().ToUpper();
                    dto.Telefone = dr[4].ToString();
                    dto.Email = dr[5].ToString();
                    dto.WebSite = dr[6].ToString();
                    dto.TelefoneAlt = dr[7].ToString();
                    dto.Tipo = dr[8].ToString();
                    dto.Categoria = dr[9].ToString();
                    decimal _saldo = decimal.Parse(dr[10].ToString().Replace(".", ","));

                    dto.Saldo = String.Format("{0:N2}", _saldo > 0 ? -_saldo : 0);


                    if (dto.Tipo == "F")
                    {
                        dto.Tipo = "Fonecedor";
                        dto.SocialName = "F";
                    }
                    else
                    {
                        dto.Tipo = "Cliente e Fornecedor";
                        dto.SocialName = "A";
                    }
                    
                    dto.Filial = dr[19].ToString() != "" ? dr[19].ToString() : "-1";

                    dto.SupplierFiscalCodeID = dr["TER_SUPPLIER_IVA_ID"].ToString() == string.Empty ? "-1" : dr["TER_SUPPLIER_IVA_ID"].ToString();
                    dto.IsCompanyInsurance = dr["TER_COMPANY_INSURANCE"].ToString() != "1" ? false : true;
                    dto.Currency = dr["TER_CURRENCY_ID"].ToString();
                    dto.RetencaoID = int.Parse(dr["TER_RETENCAO"].ToString() == "" ? "-1" : dr["TER_RETENCAO"].ToString());
                    dto.RetencaoID = dto.RetencaoID == 0 ? -1 : dto.RetencaoID; 
                    dto.Status = int.Parse(dr["TER_STATUS"].ToString() != "0" ? "1" : "0");

                    lista.Add(dto);
                } 
            }
            catch (Exception ex)
            {
                dto = new EntidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<EntidadeDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }


        public List<EntidadeDTO> ObterTerceirosPorFiltro(EntidadeDTO dto)
        {
            List<EntidadeDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_GER_TERCEIRO_OBTERPORFILTRO";

                BaseDados.AddParameter("NOME", dto.NomeCompleto);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EntidadeDTO>();
                while (dr.Read())
                {
                    dto = new EntidadeDTO();


                    dto.Codigo = int.Parse(dr[0].ToString()); // ID de Terceiro
                    dto.Identificacao = dr[1].ToString();
                    dto.NomeCompleto = dr[2].ToString();
                    dto.Morada = dr[3].ToString().ToUpper();
                    dto.Telefone = dr[4].ToString();
                    dto.Email = dr[5].ToString();
                    dto.WebSite = dr[6].ToString();
                    dto.TelefoneAlt = dr[7].ToString();
                    dto.Tipo = dr[8].ToString();
                    dto.Categoria = dr[9].ToString();
                    decimal _saldo = decimal.Parse(dr[10].ToString().Replace(".", ","));
                    dto.Entidade = int.Parse(dr[11].ToString());
                    dto.Saldo = String.Format("{0:N2}", _saldo > 0 ? -_saldo : 0);


                    if (dto.Tipo == "F")
                    {
                        dto.Tipo = "Fonecedor";
                        dto.SocialName = "F";
                    }
                    else
                    {
                        dto.Tipo = "Cliente e Fornecedor";
                        dto.SocialName = "";
                    }
                    
                    dto.Filial = dr[19].ToString() != "" ? dr[19].ToString() : "-1"; 
                    dto.IsCompanyInsurance = dr["TER_COMPANY_INSURANCE"].ToString() != "1" ? false : true; 
                    dto.CustomerFiscalCodeID = dr["TER_CUSTOMER_IVA_ID"].ToString() == string.Empty ? "-1" : dr["TER_CUSTOMER_IVA_ID"].ToString();
                    dto.SupplierFiscalCodeID = dr["TER_SUPPLIER_IVA_ID"].ToString() == string.Empty ? "-1" : dr["TER_SUPPLIER_IVA_ID"].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new EntidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<EntidadeDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }
        /*
        string TaxaIvaRegime(string pRegimeID)
        {
            if (pRegimeID == "G")
            {
                return "3";
            }
            else if (pRegimeID == "T")
            {
                return "44";
            }
            else if (pRegimeID == "C")
            {
                return "52";
            }
            else
            {
                return "5";
            }

        }*/

        public List<EntidadeDTO> ObterPorConvenio(EntidadeDTO dto)
        {
            List<EntidadeDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_GER_ENTIDADE_OBTER_COM_CONVENIO";

                BaseDados.AddParameter("FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EntidadeDTO>();
                while (dr.Read())
                {
                    dto = new EntidadeDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        NomeCompleto = dr[1].ToString(),
                        NomeComercial = dr[1].ToString()
                    };

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new EntidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<EntidadeDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }


    }
}
