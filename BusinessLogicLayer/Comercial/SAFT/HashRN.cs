using Dominio.Comercial;
using Dominio.Comercial.SAFT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.SAFT
{
    public class HashRN
    {

        public string SaftFileName { get; set; } 
         
         

        // <summary>
        /// Reads the public key file and returns the RSA public key
        /// </summary>
        /// <returns></returns>
        public string GetRSAPublicKey(string PublicKeyFileName)
        {
            if (File.Exists(PublicKeyFileName))
            {
                string publickey = File.ReadAllText(PublicKeyFileName).Trim();

                if (publickey.StartsWith(RSAKeys.PEM_PUB_HEADER) && publickey.EndsWith(RSAKeys.PEM_PUB_FOOTER))
                {
                    //this is a pem file
                    RSAKeys rsa = new RSAKeys();
                    rsa.DecodePEMKey(publickey);

                    return rsa.PublicKey;
                }
                else
                {
                    return publickey;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Reads the public key file and returns the RSA private key
        /// </summary>
        /// <returns></returns>
        public string GetRSAPrivateKey(string PrivateKeyFileName)
        {
            if (File.Exists(PrivateKeyFileName))
            {
                string privatekey = File.ReadAllText(PrivateKeyFileName).Trim();

                if (privatekey.StartsWith(RSAKeys.PEM_PRIV_HEADER) && privatekey.EndsWith(RSAKeys.PEM_PRIV_FOOTER))
                {
                    //this is a pem file
                    RSAKeys rsa = new RSAKeys();
                    rsa.DecodePEMKey(privatekey);

                    return rsa.PrivateKey;
                }
                else
                {
                    return privatekey;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Format the correct invoice fields to the specification of the hash field.
        /// </summary>
        /// <param name="toHash">StringBuilder that will contain the formated string.</param>
        /// <param name="invoice">The invoice.</param>
        /// <param name="hashAnterior">The string hash generated of the last invoice.</param>
        void FormatinvoicesStringToHash(ref StringBuilder toHash, CustomerInvoice invoice, string hashAnterior)
        {
            toHash.Clear();
            toHash.AppendFormat("{0};{1};{2};{3};{4}"
                        , invoice.InvoiceDate
                        , invoice.SystemEntryDate
                        , invoice.InvoiceNo
                        , invoice.DocumentTotals.GrossTotal
                        , hashAnterior);
        }


        public void GenerateInvoicesHash(AuditFileBilling saftfile, string PrivateKeyFileName)
        {
            if (saftfile == null || saftfile.SourceDocuments == null || saftfile.SourceDocuments.SalesInvoices == null || saftfile.SourceDocuments.SalesInvoices.Invoice == null)
                return;

                #pragma warning disable IDE0068 // Usar o padrão de descarte recomendado
                            object hasher = SHA1.Create();
                #pragma warning restore IDE0068 // Usar o padrão de descarte recomendado

            using (RSACryptoServiceProvider rsaCryptokey = new RSACryptoServiceProvider(1024))
            {
                rsaCryptokey.FromXmlString(GetRSAPrivateKey(PrivateKeyFileName));

                StringBuilder toHash = new StringBuilder();

                 
                for (int i = 0; i < saftfile.SourceDocuments.SalesInvoices.Invoice.Count; i++)
                {
                    var invoice = saftfile.SourceDocuments.SalesInvoices.Invoice[i];

                    bool usaHashAnterior = true;
                    if (i == 0 || invoice.InvoiceType != saftfile.SourceDocuments.SalesInvoices.Invoice[i - 1].InvoiceType || Convert.ToInt32(invoice.InvoiceNo.Split('/')[1]) != Convert.ToInt32(saftfile.SourceDocuments.SalesInvoices.Invoice[i - 1].InvoiceNo.Split('/')[1]) + 1)
                        usaHashAnterior = false;

                    
                    FormatinvoicesStringToHash(ref toHash, invoice, usaHashAnterior ? saftfile.SourceDocuments.SalesInvoices.Invoice[i - 1].Hash : "");

                    byte[] stringToHashBuffer = Encoding.UTF8.GetBytes(toHash.ToString());
                    byte[] r = rsaCryptokey.SignData(stringToHashBuffer, hasher);

                    invoice.Hash = Convert.ToBase64String(r);
                    invoice.HashControl = invoice.Hash[0].ToString() + invoice.Hash[10].ToString() + invoice.Hash[20].ToString() + invoice.Hash[30].ToString();
                    string PriorHash = usaHashAnterior ? saftfile.SourceDocuments.SalesInvoices.Invoice[i - 1].Hash : "";
                    FaturaClienteRN.GetInstance().SaveHash(new FaturaDTO {

                        Hash = invoice.Hash,
                        PriorHash = PriorHash,
                        Codigo = invoice.InvoiceID
                        
                    });
                }
            }
        }



        void FormatWorkingDocumentsStringToHash(ref StringBuilder toHash, WorkDocument doc, string hashAnterior)
        {
            toHash.Clear();
            toHash.AppendFormat("{0};{1};{2};{3};{4}"
                        , doc.WorkDate
                        , doc.SystemEntryDate
                        , doc.DocumentNumber
                        , doc.DocumentTotals.GrossTotal
                        , hashAnterior);
        }

        public void GenerateWorkingDocumentsHash(AuditFileBilling saftfile, string PrivateKeyFileName)
        {
            if (saftfile == null || saftfile.SourceDocuments == null || saftfile.SourceDocuments.WorkingDocuments == null || saftfile.SourceDocuments.WorkingDocuments.WorkDocument == null)
                return;

            object hasher = SHA1.Create();

            using (RSACryptoServiceProvider rsaCryptokey = new RSACryptoServiceProvider(1024))
            {
                rsaCryptokey.FromXmlString(GetRSAPrivateKey(PrivateKeyFileName));

                StringBuilder toHash = new StringBuilder();

                for (int i = 0; i < saftfile.SourceDocuments.WorkingDocuments.WorkDocument.Length; i++)
                {
                    var doc = saftfile.SourceDocuments.WorkingDocuments.WorkDocument[i];

                    bool usaHashAnterior = true;
                    if (i == 0 || doc.WorkType != saftfile.SourceDocuments.WorkingDocuments.WorkDocument[i - 1].WorkType || Convert.ToInt32(doc.DocumentNumber.Split('/')[1]) != Convert.ToInt32(saftfile.SourceDocuments.WorkingDocuments.WorkDocument[i - 1].DocumentNumber.Split('/')[1]) + 1)
                        usaHashAnterior = false;

                    FormatWorkingDocumentsStringToHash(ref toHash, doc, usaHashAnterior ? saftfile.SourceDocuments.WorkingDocuments.WorkDocument[i - 1].Hash : "");

                    byte[] stringToHashBuffer = Encoding.UTF8.GetBytes(toHash.ToString());
                    byte[] r = rsaCryptokey.SignData(stringToHashBuffer, hasher);

                    doc.Hash = Convert.ToBase64String(r);
                }
            }
        }
        /// <summary>
        /// Generate the hash filed for the Movement of goods, base in a AuditFile object, the hash will be stored in HashTest field.
        /// </summary>
        /*
         *void FormatStringToHash(ref StringBuilder toHash, SourceDocumentsMovementOfGoodsStockMovement doc, string hashAnterior)
         {
            toHash.Clear();
            toHash.AppendFormat("{0};{1};{2};{3};{4}"
                        , doc.MovementDate.ToString("yyyy-MM-dd")
                        , doc.SystemEntryDate.ToString("yyyy-MM-ddTHH:mm:ss")
                        , doc.DocumentNumber
                        , doc.DocumentTotals.GrossTotal.ToString("0.00", en)
                        , hashAnterior);
         }
        
        public void GenerateMovementOfGoodHash(AuditFile saftfile, string PrivateKeyFileName)
        {
            if (saftfile == null || saftfile.SourceDocuments == null || saftfile.SourceDocuments.MovementOfGoods == null || saftfile.SourceDocuments.MovementOfGoods.StockMovement == null)
                return;

            object hasher = SHA1.Create();

            using (RSACryptoServiceProvider rsaCryptokey = new RSACryptoServiceProvider(1024))
            {
                rsaCryptokey.FromXmlString(GetRSAPrivateKey());

                StringBuilder toHash = new StringBuilder();

                for (int i = 0; i < saftfile.SourceDocuments.MovementOfGoods.StockMovement.Length; i++)
                {
                    SourceDocumentsMovementOfGoodsStockMovement doc = saftfile.SourceDocuments.MovementOfGoods.StockMovement[i];

                    bool usaHashAnterior = true;
                    if (i == 0 || doc.MovementType != saftfile.SourceDocuments.MovementOfGoods.StockMovement[i - 1].MovementType || Convert.ToInt32(doc.DocumentNumber.Split('/')[1]) != Convert.ToInt32(saftfile.SourceDocuments.MovementOfGoods.StockMovement[i - 1].DocumentNumber.Split('/')[1]) + 1)
                        usaHashAnterior = false;

                    FormatStringToHash(ref toHash, doc, usaHashAnterior ? saftfile.SourceDocuments.MovementOfGoods.StockMovement[i - 1].Hash : "");

                    byte[] stringToHashBuffer = Encoding.UTF8.GetBytes(toHash.ToString());
                    byte[] r = rsaCryptokey.SignData(stringToHashBuffer, hasher);

                    doc.HashTest = Convert.ToBase64String(r);
                }
            }
        }
        */

    }
}
