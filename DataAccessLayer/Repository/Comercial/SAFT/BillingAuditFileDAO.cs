using Dominio.Comercial.SAFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Comercial.SAFT
{

    public class BillingAuditFileDAO
    {
        ConexaoDB bdContext;
        public BillingAuditFileDAO()
        {
            bdContext = new ConexaoDB();
        }

         
        

        public SalesInvoices GetBillingDocuments(SaftDTO pFilter)
        {
            
            SalesInvoices salesInvoicesResume = new SalesInvoices(); 

            try
            {
                bdContext.ComandText = "stp_SAFT_SALES_INVOICES_TOTAL_ENTRIES_DEBITS_CREDITS";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader(); 
                if (dr.Read())
                {
                    salesInvoicesResume = new SalesInvoices
                    {
                        NumberOfEntries = dr[0].ToString(),
                        TotalDebit = dr[1].ToString() != "" ? dr[1].ToString().Replace(",", ".").Replace("-", string.Empty) : "0.00",
                        TotalCredit = dr[2].ToString()!="" ? dr[2].ToString().Replace(",", ".").Replace("-", string.Empty) : "0.00"
                    };
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
            }

            salesInvoicesResume.Invoice = GetSalesInvoiceList(pFilter);

            if(salesInvoicesResume.Invoice.Count == 0)
            {
                salesInvoicesResume = new SalesInvoices
                {
                    NumberOfEntries = "0",
                    TotalDebit = "0.00",
                    TotalCredit = "0.00"
                };
            }




            return salesInvoicesResume;
        }

        

        private List<CustomerInvoice> GetSalesInvoiceList(SaftDTO pFilter)
        {
            List<CustomerInvoice> lista = new List<CustomerInvoice>();
            try
            {
                bdContext.ComandText = "stp_SAFT_SALES_INVOICES";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    var invoice =
                    new CustomerInvoice
                    {
                        InvoiceNo = dr[0].ToString(),
                        DocumentStatus = new DocumentStatus
                        {
                            InvoiceStatus = dr[1].ToString(),
                            InvoiceStatusDate = DateTime.Parse(dr[2].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                            //Reason = dr[3].ToString(),
                            SourceID = dr[4].ToString(),
                            SourceBilling = dr[5].ToString()
                        },
                        Hash = dr[6].ToString(),
                        HashControl = dr[7].ToString(),
                        Period = dr[8].ToString(),
                        InvoiceDate = DateTime.Parse(dr[9].ToString()).ToString("yyyy-MM-dd"),
                        InvoiceType = dr[10].ToString(),
                        SpecialRegimes = new SpecialRegimes
                        {
                            SelfBillingIndicator = dr[11].ToString(),
                            CashVATSchemeIndicator = dr[12].ToString(),
                            ThirdPartiesBillingIndicator = dr[13].ToString()
                        },
                        SourceID = dr[14].ToString(),
                        EACCode = dr[15].ToString() == "" ? "000" : dr[15].ToString(),
                        SystemEntryDate = DateTime.Parse(dr[16].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        TransactionID = pFilter.FileType == "F" ? null : dr[17].ToString(),
                        CustomerID = dr[18].ToString(),
                        ShipTo = null/* new ShipTo
                        {
                            DeliveryID = dr[19].ToString(),
                            DeliveryDate = DateTime.Parse(dr[20].ToString() == "" ? dr[16].ToString() : dr[20].ToString()).ToString("yyyy-MM-dd"),
                            WarehouseID = dr[21].ToString(),
                            LocationID = dr[22].ToString(),
                            Address = new Address
                            {
                                BuildingNumber = dr[23].ToString(),
                                StreetName = dr[24].ToString(),
                                AddressDetail = dr[25].ToString() == "" ? "N/A" : dr[25].ToString(),
                                City = dr[26].ToString(),
                                PostalCode = dr[27].ToString(),
                                Province = dr[28].ToString(),
                                Country = dr[29].ToString()
                            }
                        }*/,

                        ShipFrom = null /*new ShipFrom
                        {
                            DeliveryID = dr[30].ToString(),
                            DeliveryDate = DateTime.Parse(dr[31].ToString() == "" ? dr[16].ToString() : dr[31].ToString()).ToString("yyyy-MM-dd"),
                            WarehouseID = dr[32].ToString(),
                            LocationID = dr[33].ToString(),
                            Address = new Address
                            {
                                BuildingNumber = dr[34].ToString(),
                                StreetName = dr[35].ToString(),
                                AddressDetail = dr[36].ToString() == "" ? "N/A" : dr[36].ToString(),
                                City = dr[37].ToString(),
                                PostalCode = dr[38].ToString(),
                                Province = dr[39].ToString(),
                                Country = dr[40].ToString()
                            }
                        }*/,
                        MovementEndTime = DateTime.Parse(dr[41].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        MovementStartTime = DateTime.Parse(dr[42].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        InvoiceID = int.Parse(dr[43].ToString()),
                        DocumentTotals = new CustomerInvoiceDocumentTotals
                        {
                            TaxPayable = dr[44].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                            NetTotal = dr[45].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                            GrossTotal = dr[46].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),

                            Settlement = new Settlement
                            {
                                SettlementDiscount = "0",
                                SettlementAmount = "0",
                                SettlementDate = null,// DateTime.MinValue.ToString("yyyy-MM-dd"),
                                PaymentTerms = ""
                            },

                        },

                        WithholdingTax =null /*new WithholdingTax
                        {
                            WithholdingTaxType = "IS",
                            WithholdingTaxDescription = "",
                            WithholdingTaxAmount = "0",
                        }*/,
                       

                       

                    };

                    if (dr[47].ToString()!="AOA")
                    {
                        invoice.DocumentTotals.Currency = new Currency
                        {
                            CurrencyCode = dr[47].ToString(),
                            CurrencyAmount = dr[48].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                            ExchageRate = dr[49].ToString() == "" || dr[49].ToString() == "0" ? "1" : dr[49].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty)

                        };
                    }
                    if(dr[61].ToString()!= "0,00")
                    {
                        string WithholdingType = "IS";
                        if (dr[62].ToString() == "6,50000")
                        {
                            WithholdingType = "II";
                        }else if (dr[62].ToString() == "10,50000")
                        {
                            WithholdingType = "IRT";
                        }

                        invoice.WithholdingTax = new WithholdingTax
                        {
                            WithholdingTaxType =  WithholdingType,
                            WithholdingTaxDescription = dr[62].ToString(),
                            WithholdingTaxAmount = dr[61].ToString().Replace(",", "."),
                        };
                    }
                    lista.Add(invoice); 
                }
            }
            catch (Exception ex)
            {
                string erroEx = ex.Message;
            }
            finally
            {
                bdContext.FecharConexao();
                for(int i =0; i<lista.Count; i++)
                {
                    var Line = GetCustomesInvoiceLines(lista[i]);
                    lista[i].Line = Line; /*
                    var taxBase = Line.Sum(t => decimal.Parse(t.TaxBase.Replace(".", ",")));

                    decimal TotalNet = decimal.Parse(lista[i].DocumentTotals.NetTotal.Replace(".", ",")), TotalGross = decimal.Parse(lista[i].DocumentTotals.GrossTotal.Replace(".", ",")),
                        TotalTax = decimal.Parse(lista[i].DocumentTotals.TaxPayable.Replace(".", ","));
                    TotalGross -= TotalTax;

                    if ( taxBase == 0)
                    {
                        
                        
                        lista[i].DocumentTotals = new CustomerInvoiceDocumentTotals
                        {
                            TaxPayable = taxBase.ToString(),
                            NetTotal = TotalNet.ToString().Replace(",", "."),
                            GrossTotal = TotalGross.ToString().Replace(",", "."),
                        };  
                    }*/

                    lista[i].DocumentTotals.Payment = GetSalesInvoicePaymentMethods(lista[i]).ToArray();
                }

            }



            return lista;
        }

        private List<Line> GetCustomesInvoiceLines(CustomerInvoice pInvoiceHeader)
        {
            List<Line> lista = new List<Line>();
            try
            {
                bdContext.ComandText = "stp_SAFT_SALES_INVOICES_LINES";
                bdContext.AddParameter("@INVOICE_ID", pInvoiceHeader.InvoiceID);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    var item = new Line
                    {
                        LineNumber = dr[0].ToString(),
                        /*OrderReferences = new OrderReferences
                        {
                            OriginatingON = dr[1].ToString(),
                            OrderDate = dr[2].ToString() == string.Empty ? DateTime.MinValue.ToString("yyyy-MM-dd") : DateTime.Parse(dr[2].ToString()).ToString("yyyy-MM-dd"),
                        },*/
                        ProductCode = dr[3].ToString(),
                        ProductDescription = dr[4].ToString(),
                        Quantity = dr[5].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                        UnitPrice = dr[6].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty), 
                        UnitOfMeasure = dr[7].ToString(),
                        TaxBase =null, //decimal.Parse(dr[6].ToString()) > 0 ? null : Math.Round(decimal.Parse(dr[8].ToString()), 2).ToString().Replace(",", ".").Replace("-", string.Empty),
                        TaxPointDate = dr[9].ToString() == string.Empty ? DateTime.MinValue.ToString("yyyy-MM-dd") : DateTime.Parse(dr[9].ToString()).ToString("yyyy-MM-dd"),
                        
                        Description = dr[4].ToString(),
                        
                        ProductSerialNumber = null,
                        DebitAmount = (pInvoiceHeader.InvoiceType == "FT" || pInvoiceHeader.InvoiceType == "FR" || pInvoiceHeader.InvoiceType == "ND") ? null : dr[14].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                        CreditAmount = (pInvoiceHeader.InvoiceType == "NC") ? null : Math.Round(decimal.Parse(dr[14].ToString()), 2).ToString().Replace(",", ".").Replace("-", string.Empty),

                        Tax = new Tax
                        {
                            TaxType = dr[16].ToString(),
                            TaxCountryRegion = dr[17].ToString() == "" ? "AO" : dr[17].ToString(),
                            TaxCode = dr[18].ToString(),
                            TaxPercentage = dr[19].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                            TaxAmount = null, //dr[20].ToString().Replace(",", ".").Replace("-", string.Empty)  
                        },
                        
                        /*
                        TaxExemptions = new TaxExemptions
                        {
                            TaxExemptionReason = dr[21].ToString(),
                            TaxExemptionCode = dr[22].ToString()
                        },*/
                        TaxExemptionReason = dr[21].ToString(),
                        TaxExemptionCode = dr[22].ToString(),

                        SettlementAmount = dr[23].ToString().Replace(",", ".").Replace("-", string.Empty),
                        CustomsInformation =null/* new CustomsInformation
                        {
                            ARCNo = dr[24].ToString(),
                            IECAmount = dr[25].ToString(),
                        }*/,

                    };
                    if(item.TaxExemptionCode == "")
                    {
                        item.TaxExemptionCode = null;
                        item.TaxExemptionReason = null;
                    }
                    lista.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
                if(pInvoiceHeader.InvoiceType == "NC" || pInvoiceHeader.InvoiceType == "ND")
                {

                }
            }

            return lista;
        }

        private List<References> GetRetifiedInvoicesReferences(CustomerInvoice pInvoice)
        {
            List<References> referencesList = new List<References>();
            try
            {
                bdContext.ComandText = "stp_SAFT_SALES_INVOICES_RETIFIED_DOCUMENTS_REFERENCES";

                bdContext.AddParameter("@INVOICE_ID", pInvoice.InvoiceID);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    referencesList.Add(
                        new References
                        {
                            Reference = dr[0].ToString(),
                            Reason = dr[1].ToString(),
                        });
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
            }

            return referencesList;
        }

        private List<CustomerInvoicePayment> GetSalesInvoicePaymentMethods(CustomerInvoice pInvoiceHeader)
        {
            List<CustomerInvoicePayment> lista = new List<CustomerInvoicePayment>();
            try
            {
                bdContext.ComandText = "stp_SAFT_SALES_INVOICES_PAYMENTS";
                bdContext.AddParameter("@INVOICE_ID", pInvoiceHeader.InvoiceID);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new CustomerInvoicePayment
                    {
                        PaymentMechanism = dr[0].ToString(),
                        PaymentAmount = dr[1].ToString().Replace(",", ".").Replace("-", string.Empty),
                        PaymentDate = DateTime.Parse(dr[2].ToString()).ToString("yyyy-MM-dd")
                    });;
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

        public WorkingDocuments GetWorkingDocuments(SaftDTO pFilter)
        {
            WorkingDocuments workingDocuments = new WorkingDocuments();
            try
            {
                bdContext.ComandText = "stp_SAFT_WORKING_DOCUMENTS_TOTAL_ENTRIES_DEBITS_CREDITS";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();
                if (dr.Read())
                {
                    workingDocuments = new WorkingDocuments
                    {
                        NumberOfEntries = dr[0].ToString(),
                        TotalDebit = dr[1].ToString().Replace(",", ".").Replace("-", string.Empty),
                        TotalCredit = dr[2].ToString().Replace(",", ".").Replace("-", string.Empty)
                    };
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
                workingDocuments.WorkDocument = GetWorkDocumentList(pFilter);
                if(workingDocuments.NumberOfEntries ==null && workingDocuments.WorkDocument.Length == 0)
                {
                     
                    workingDocuments = new WorkingDocuments
                    {
                        NumberOfEntries = "0",
                        TotalDebit = "0.00",//dr[1].ToString().Replace(",", ".").Replace("-", string.Empty),
                        TotalCredit = "0.00"//dr[2].ToString().Replace(",", ".").Replace("-", string.Empty)
                    };
                }
            }


            return workingDocuments;
        }

        private WorkDocument[] GetWorkDocumentList(SaftDTO pFilter)
        {
            List<WorkDocument> lista = new List<WorkDocument>();
            try
            {
                bdContext.ComandText = "stp_SAFT_WORKING_DOCUMENTS";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    var workDocument = new WorkDocument
                    {
                        DocumentNumber = dr[0].ToString(),
                        DocumentStatus = new WorkingDocumentStatus
                        {
                            WorkStatus = dr[1].ToString(),
                            WorkStatusDate = DateTime.Parse(dr[2].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                            //Reason = dr[3].ToString(),
                            SourceID = dr[4].ToString(),
                            SourceBilling = dr[5].ToString()
                        },
                        Hash = dr[6].ToString(),
                        HashControl = dr[7].ToString(),
                        Period = dr[8].ToString(),
                        WorkDate = DateTime.Parse(dr[9].ToString()).ToString("yyyy-MM-dd"),
                        WorkType = dr[10].ToString().Replace("FP", "PP").Replace("NP", "PP").Replace("ORT", "OR").Replace("EC", "NE").Replace("ECL", "NE"),

                        SourceID = dr[11].ToString(),
                        EACCode = null,//dr[12].ToString(),
                        SystemEntryDate = DateTime.Parse(dr[13].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        TransactionID = pFilter.FileType == "F" ? null : dr[14].ToString(),
                        CustomerID = dr[15].ToString(),
                        WorkID = int.Parse(dr[16].ToString()),
                        DocumentTotals = new WorkingDocumentsTotals
                        {
                            TaxPayable = dr[17].ToString().Replace(",", ".").Replace("-", string.Empty),
                            NetTotal = dr[18].ToString().Replace(",", ".").Replace("-", string.Empty),
                            GrossTotal = dr[19].ToString().Replace(",", ".").Replace("-", string.Empty),
                            Currency = new Currency
                            {
                                CurrencyCode = dr[20].ToString(),
                                CurrencyAmount = Math.Round(decimal.Parse(dr[21].ToString()), 2).ToString().Replace(",", ".").Replace("-", string.Empty),
                                ExchageRate = dr[22].ToString().Replace(",", ".").Replace("-", string.Empty)

                            },
                        },

                    };

                    if (dr[20].ToString() == "AOA")
                        workDocument.DocumentTotals.Currency = null;

                    lista.Add(workDocument);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
                for (int i = 0; i < lista.Count; i++)
                {
                    lista[i].Line = GetWorkDocumentListLines(lista[i]);//.ToArray(); 
                }

            }



            return lista.ToArray();
        }


        private List<Line> GetWorkDocumentListLines(WorkDocument pWorkDocument)
        {
            List<Line> lista = new List<Line>();
            try
            {
                bdContext.ComandText = "stp_SAFT_WORKING_DOCUMENTS_LINES";
                bdContext.AddParameter("@INVOICE_ID", pWorkDocument.WorkID);

                MySqlDataReader dr = bdContext.ExecuteReader(); 
                while (dr.Read())
                {
                    
                    var item = new Line
                    {
                        LineNumber = dr[0].ToString(),
                        /*OrderReferences = new OrderReferences
                        {
                            OriginatingON = dr[1].ToString(),
                            OrderDate = dr[2].ToString() == string.Empty ? DateTime.MinValue.ToString("yyyy-MM-dd") : DateTime.Parse(dr[2].ToString()).ToString("yyyy-MM-dd"),
                        },*/
                        ProductCode = dr[3].ToString(),
                        ProductDescription = dr[4].ToString(),
                        Quantity = dr[5].ToString().Replace(",", ".").Replace("-", string.Empty),
                        UnitPrice = dr[6].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                        UnitOfMeasure = dr[7].ToString(),
                        //TaxBase = null,// Math.Round(decimal.Parse(dr[8].ToString()), 2).ToString().Replace(",", ".").ToString().Replace("-", string.Empty),
                        TaxPointDate = dr[9].ToString() == string.Empty ? DateTime.MinValue.ToString("yyyy-MM-dd") : DateTime.Parse(dr[9].ToString()).ToString("yyyy-MM-dd"),
                        /*References = new References
                        {
                            Reference = dr[10].ToString(),
                            Reason = dr[11].ToString()
                        },*/
                        Description = dr[12].ToString(),

                        DebitAmount = null,//dr[14].ToString().Replace(",", ".").Replace("-", string.Empty),
                        CreditAmount = dr[15].ToString().Replace(",", ".").Replace("-", string.Empty),
                        Tax = new Tax
                        {
                            TaxType = dr[16].ToString(),
                            TaxCountryRegion = dr[17].ToString() == "" ? "AO" : dr[17].ToString(),
                            TaxCode = dr[18].ToString(),
                            TaxPercentage = dr[19].ToString().Replace(",", ".").Replace("-", string.Empty).Replace("-", string.Empty),
                            TaxAmount = null, //dr[20].ToString().Replace(",", ".").Replace("-", string.Empty)


                        },
                        ProductSerialNumber = null,
                        /*
                        TaxExemptions = new TaxExemptions
                        {
                            TaxExemptionReason = dr[21].ToString(),
                            TaxExemptionCode = dr[22].ToString()
                        },*/
                        TaxExemptionReason = dr[21].ToString(),
                        TaxExemptionCode = dr[22].ToString(),
                        SettlementAmount = dr[23].ToString().Replace(",", ".").Replace("-", string.Empty)

                    };

                    if (item.TaxExemptionCode == "")
                    {
                        item.TaxExemptionCode = null;
                        item.TaxExemptionReason = null;
                    }
                    //decimal debitAmount = decimal.Parse(item.DebitAmount), creditAmount = decimal.Parse(item.CreditAmount);

                    lista.Add(item);
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


        public Payments GetPayment(SaftDTO pFilter)
        {
            List<Payment> receiptsList = new List<Payment>();
            var Payments = new Payments();
            try
            {
                bdContext.ComandText = "stp_SAFT_RECEIPTS";
                bdContext.AddParameter("@COMPANY_ID", pFilter.Filial);
                bdContext.AddParameter("@FILE_TYPE", pFilter.FileType);
                bdContext.AddParameter("@DATE_INI", pFilter.DateFrom);
                bdContext.AddParameter("@DATE_TERM", pFilter.DateUntil);
                bdContext.AddParameter("@TRANSACTION_ID", pFilter.SaftID);
                bdContext.AddParameter("@FISCAL_YEAR", pFilter.FiscalYear);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                {
                    if (Payments.NumberOfEntries == 0)
                    {
                        Payments = new Payments
                        {
                            NumberOfEntries = int.Parse(dr[0].ToString()),
                            TotalDebit = int.Parse(dr[0].ToString()) > 0 ? dr[1].ToString().Replace(",", ".").Replace("-", string.Empty) : "0.00",
                            TotalCredit = int.Parse(dr[0].ToString()) > 0 ? dr[2].ToString().Replace(",", ".").Replace("-", string.Empty) : "0.00"
                        };
                    }

                    receiptsList.Add(ReceiptDetails(dr, pFilter));
                }

                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bdContext.FecharConexao();
                if(receiptsList.Count == 0)
                {
                    Payments = new Payments
                    {
                        TotalCredit = "0.00",
                        TotalDebit = "0.00",
                    };
                }
                else
                {
                    for (int i = 0; i < receiptsList.Count; i++)
                    {
                        receiptsList[i].PaymentMethod = SetReceiptPaymentMethods(receiptsList[i], pFilter.DateFrom, pFilter.DateUntil);
                        receiptsList[i].Line = SetPaymentLines(receiptsList[i], pFilter);//.ToArray();
                    }
                }
                

                Payments.Payment = receiptsList;//.ToArray();
            }

            return Payments;
        }

        private PaymentsPaymentMethod SetReceiptPaymentMethods(Payment pPayment, DateTime pFrom, DateTime pUntil)
        {
            List<PaymentsPaymentMethod> lista = new List<PaymentsPaymentMethod>();
            var PaymentsPaymentMethod = new PaymentsPaymentMethod();
            try
            {
                bdContext.ComandText = "stp_SAFT_RECEIPTS_PAYMENTS_METHOD";
                bdContext.AddParameter("@PAYMENT_ID", pPayment.PaymentID);/*
                bdContext.AddParameter("@DATE_INI", pFrom);
                bdContext.AddParameter("@DATE_TERM", pUntil);*/

                MySqlDataReader dr = bdContext.ExecuteReader();
                
                while (dr.Read())
                {
                    lista.Add(new PaymentsPaymentMethod
                    {
                        //PaymentMechanism = dr[0].ToString(),
                        PaymentAmount = dr[2].ToString().Replace(",", ".").Replace("-", string.Empty),
                        PaymentDate = DateTime.Parse(dr[3].ToString()).ToString("yyyy-MM-dd")
                    });
                }

            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
            }
            finally
            {
                bdContext.FecharConexao();
            }
            if (lista != null && lista.Count > 0)
            {
                return new PaymentsPaymentMethod
                {
                    PaymentAmount = lista.Sum(t => decimal.Parse(t.PaymentAmount.Replace(".", ","))).ToString().Replace(",", "."),
                    PaymentDate = lista.Max(t => DateTime.Parse(t.PaymentDate)).ToString("yyyy-MM-dd")
                };
            }
            else
            {
                return new PaymentsPaymentMethod();
            }
            
        }

        private static Payment ReceiptDetails(MySqlDataReader dr, SaftDTO pFilter)
        {
             
            var payment = new Payment
            {
                PaymentRefNo = dr[3].ToString(),
                Period = dr[4].ToString(),
                TransactionID = pFilter.FileType == "F" ? null : dr[5].ToString(),
                TransactionDate = DateTime.Parse(dr[6].ToString()).ToString("yyyy-MM-dd"),
                PaymentType = dr[7].ToString().Replace("RE", "RC"),
                Description = dr[8].ToString(),
                SystemID = dr[9].ToString(),
                DocumentStatus = new PaymentsDocumentStatus
                {
                    PaymentStatus = dr[10].ToString(),
                    PaymentStatusDate = DateTime.Parse(dr[11].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                    Reason = dr[12].ToString(),
                    SourceID = dr[13].ToString(),
                    SourcePayment = dr[14].ToString()
                },
                DocumentTotals = new PaymentsDocumentTotals
                {
                    TaxPayable = dr[15].ToString().Replace(",", ".").Replace("-", string.Empty),
                    NetTotal = dr[16].ToString().Replace(",", ".").Replace("-", string.Empty),
                    GrossTotal = dr[17].ToString().Replace(",", ".").Replace("-", string.Empty),
                    Settlement = new PaymentsDocumentTotalsSettlement
                    {
                        SettlementAmount = dr[18].ToString().Replace(",", ".").Replace("-", string.Empty)
                    }, 
                },
                /*WithholdingTax = null new WithholdingTax
                {
                    WithholdingTaxType = "II",
                    WithholdingTaxDescription = "N/A",
                    WithholdingTaxAmount = "0.00"
                },*/
                PaymentID = int.Parse(dr[22].ToString()),
                SourceID = dr[23].ToString(),
                SystemEntryDate = DateTime.Parse(dr[24].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                CustomerID = dr[25].ToString()

            };

            if (dr[19].ToString() != "AOA")
            {
                payment.DocumentTotals.Currency = new Currency
                {
                    CurrencyCode = dr[19].ToString(),
                    CurrencyAmount = dr[20].ToString().Replace(",", ".").Replace("-", string.Empty),
                    ExchageRate = dr[21].ToString().Replace(",", ".").Replace("-", string.Empty)
                };
            }

            return payment; 
        } 

        private List<PaymentsLines> SetPaymentLines(Payment dto, SaftDTO pSaft)
        {
            List<PaymentsLines> lista = new List<PaymentsLines>();
            try
            {
                bdContext.ComandText = "stp_SAFT_RECEIPTS_LINES";
                bdContext.AddParameter("@PAYMENT_ID", dto.PaymentID);

                MySqlDataReader dr = bdContext.ExecuteReader();
                while (dr.Read())
                { 
                    lista.Add(new PaymentsLines
                    {
                        LineNumber = dr[0].ToString(),
                        SourceDocumentID = new SourceDocumentID
                        {
                            OriginatingON = dr[1].ToString(),
                            InvoiceDate = DateTime.Parse(dr[2].ToString()).ToString("yyyy-MM-dd"),
                            //Description = dr[3].ToString(),
                        },
                        SettlementAmount = dr[4].ToString().Replace(",", ""),
                        DebitAmount = pSaft.FileType == "F" ? null : dr[5].ToString().Replace(",", ""),
                        CreditAmount = dr[6].ToString().Replace(",", ""), 
                        Tax = new Tax
                        {
                            TaxType = dr[7].ToString(),
                            TaxCountryRegion = dr[8].ToString(),
                            TaxCode = dr[9].ToString(),
                            TaxPercentage = dr[10].ToString().Replace(",", "."),
                            TaxAmount = null, // dr[11].ToString().Replace(",", ".")
                        },
                        TaxExemptionReason = dr[12].ToString(), 
                        TaxExemptionCode = dr[13].ToString(), 

                    });
                }
            }
            catch (Exception ex)
            {
                string ErroText = ex.Message;
            }
            finally
            {
                bdContext.FecharConexao();
            }

            return lista;
        }
    }
}
