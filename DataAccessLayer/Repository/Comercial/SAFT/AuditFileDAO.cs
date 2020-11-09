using Dominio.Comercial.SAFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Comercial.SAFT
{

    public class AuditFileDAO
    {
        private readonly ConexaoDB bdContext;
        public AuditFileDAO()
        {
            bdContext = new ConexaoDB();
        }

        public Header GetHeaderInfo(SaftDTO pFilter)
        {
            Header header = new Header();
            try
            {
                bdContext.ComandText = "stp_SAFT_HEADER";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();

                if (dr.Read())
                {
                    header.AuditFileVersion = dr[1].ToString();
                    header.CompanyID = dr[2].ToString();
                    header.TaxRegistrationNumber = dr[3].ToString();
                    header.TaxAccountingBasis = dr[4].ToString();
                    header.CompanyName = dr[5].ToString();
                    header.BusinessName = dr[6].ToString();
                    header.CompanyAddress = new CompanyAddress
                    {
                        BuildingNumber = dr[7].ToString() == "" ? null : dr[7].ToString(),
                        StreetName = dr[8].ToString() == "" ? null : dr[8].ToString(),
                        AddressDetail = dr[9].ToString(),
                        City = dr[10].ToString(),
                        PostalCode = dr[11].ToString() == "" ? null : dr[11].ToString(),
                        Province = dr[12].ToString() == "" ? null : dr[12].ToString(),
                        Country = dr[13].ToString()
                    };
                    header.FiscalYear = dr[14].ToString();
                    header.StartDate = DateTime.Parse(dr[15].ToString()).ToString("yyyy-MM-dd");
                    header.EndDate = DateTime.Parse(dr[16].ToString()).ToString("yyyy-MM-dd");
                    header.CurrencyCode = dr[17].ToString();
                    header.DateCreated = DateTime.Parse(dr[18].ToString()).ToString("yyyy-MM-dd");
                    header.TaxEntity = dr[19].ToString();
                    header.ProductCompanyTaxID = dr[20].ToString();
                    header.SoftwareValidationNumber = "143/AGT/2019"; //dr[21].ToString()== "" ? "143/AGT/2019" : dr[21].ToString();
                    header.ProductID = "KitandaSoft GE/GC LUCAN - PRESTACAO DE SERVICOS (SU), LDA"; //dr[22].ToString() == "" ? "KITANDASOFT GE" : dr[22].ToString();
                    header.ProductVersion = dr[23].ToString();
                    header.HeaderComment = dr[24].ToString();
                    header.Telephone = dr[25].ToString();
                    header.Fax = dr[26].ToString();
                    header.Email = dr[27].ToString();
                    header.Website = dr[28].ToString();
                }
            }
            catch (Exception ex)
            {
                //header.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }

            return header;
        }

        public List<Customer> GetCustomersList(SaftDTO pFilter)
        {
            List<Customer> lista = new List<Customer>();

            try
            {
                bdContext.ComandText = "stp_SAFT_CUSTOMERS";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    var customer = new Customer
                    {
                        CustomerID = dr[0].ToString(),
                        AccountID = dr[1].ToString(),
                        CustomerTaxID = dr[2].ToString(),
                        CompanyName = dr[3].ToString(),
                        Contact = dr[4].ToString() == "" ? null : dr[4].ToString(),
                        BillingAddress = new BillingAddress
                        {
                            BuildingNumber = dr[5].ToString() == "" ? null : dr[5].ToString(),
                            StreetName = dr[6].ToString() == "" ? null : dr[6].ToString(),
                            AddressDetail = dr[7].ToString() == "" ? "Desconhecido" : dr[7].ToString(),
                            City = dr[8].ToString() == "" ? "Desconhecido" : dr[8].ToString(),
                            PostalCode = dr[9].ToString() == "" ? null : dr[9].ToString(),
                            Province = dr[10].ToString() == "" ? null : dr[10].ToString(),
                            Country = dr[11].ToString()
                        },

                        ShipToAddress = new ShipToAddress
                        {
                            //BuildingNumber = dr[5].ToString() == "" ? "0" : dr[5].ToString(),
                            //StreetName = dr[6].ToString(),
                            AddressDetail = dr[7].ToString() == "" ? dr[6].ToString() : dr[7].ToString(),
                            City = dr[8].ToString() == "" ? "Desconhecido" : dr[8].ToString(),
                            //PostalCode = dr[9].ToString() == "" ? "0" : dr[9].ToString(),
                            //Province = dr[10].ToString(),
                            Country = dr[11].ToString()
                        },

                        Telephone = dr[12].ToString(),
                        Fax = dr[13].ToString(),
                        Email = dr[14].ToString(),
                        Website = dr[15].ToString(),
                        SelfBillingIndicator = dr[16].ToString()
                    };

                    lista.Add(customer);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
            }

            return lista;
        }


        public List<Product> GetProductList(SaftDTO pFilter)
        {
            List<Product> lista = new List<Product>();
            try
            {
                bdContext.ComandText = "stp_SAFT_PRODUCTS";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    var product = new Product
                    {
                        ProductType = dr[0].ToString(),
                        ProductCode = dr[1].ToString(),
                        ProductGroup = dr[2].ToString(),
                        ProductDescription = dr[3].ToString(),
                        ProductNumberCode = dr[4].ToString(),
                        CustomsDetails = null,//dr[5].ToString(),
                        //UNNumber = null,//dr[6].ToString()
                    };

                    lista.Add(product);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
            }

            return lista;
        }

        public List<TaxTableEntry> GetTaxTableEntriesList(SaftDTO pFilter)
        {
            List<TaxTableEntry> lista = new List<TaxTableEntry>();
            try
            {
                bdContext.ComandText = "stp_SAFT_TAX_TABLE_ENTRY";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    var tax = new TaxTableEntry
                    {
                        TaxType = dr[0].ToString(),
                        TaxCountryRegion = dr[1].ToString(),
                        TaxCode = dr[2].ToString(),
                        Description = dr[3].ToString(),
                        TaxExpirationDate = null,// dr[4].ToString(),
                        TaxPercentage = dr[5].ToString().Replace(",", "."),
                        TaxAmount =null// dr[6].ToString().Replace(",", ".")
                    };

                    lista.Add(tax);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
            }

            return lista;
        }


    }
}
