using DataAccessLayer.Comercial.SAFT;
using Dominio.Comercial.SAFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.SAFT
{
    public class SaftRN
    {
        private static SaftRN _instancia;
        private SaftDAO daoSaft;
        private  AuditFileDAO daoAuditFile;
        private BillingAuditFileDAO daoBillingAuditFile;

        public SaftRN()
        {
            daoSaft = new SaftDAO();
            daoAuditFile = new AuditFileDAO();
            daoBillingAuditFile = new BillingAuditFileDAO();
        }

        public static SaftRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new SaftRN();
            }

            return _instancia;
        }

        public AuditFile AuditeFile(SaftDTO dto)
        {
            HashRN hashRN = new HashRN();
            if (dto.FileType == "F")
            {
                var BillingAuditFile = GetSaftBillingAuditFile(dto);
                hashRN.GenerateInvoicesHash(BillingAuditFile, dto.PrivateKey);
                hashRN.GenerateWorkingDocumentsHash(BillingAuditFile, dto.PrivateKey);

                return BillingAuditFile;
            }
            else
                return new AuditFile();
        }


        private AuditFileBilling GetSaftBillingAuditFile(SaftDTO saft)
        {
            AuditFileBilling dto;
            try
            {
                dto = new AuditFileBilling()
                {
                    Header = daoAuditFile.GetHeaderInfo(saft),
                    MasterFiles = new MasterFiles/// MasterFilesBilling
                    {
                        Customer = daoAuditFile.GetCustomersList(saft),
                        Product = daoAuditFile.GetProductList(saft),
                        TaxTable = new TaxTable
                        {
                            TaxTableEntry = daoAuditFile.GetTaxTableEntriesList(saft)
                        },
                    },
                    SourceDocuments = new SourceDocuments //SourceDocumentsBilling
                    {

                        SalesInvoices = daoBillingAuditFile.GetBillingDocuments(saft),
                        WorkingDocuments = daoBillingAuditFile.GetWorkingDocuments(saft),
                        Payments = daoBillingAuditFile.GetPayment(saft)
                    },
                };
            }catch(Exception ex)
            {
                dto = new AuditFileBilling();
                string erro = ex.Message;
            }

            return dto;

        }


        public void Gravar(SaftDTO dto)
        {
            daoSaft.Adicionar(dto);
        }

        public void Validar(SaftDTO dto)
        {
            daoSaft.Validar(dto);
        }

        public List<SaftDTO> ObterPorFiltro(SaftDTO dto)
        {
            return daoSaft.ObterPorFiltro(dto);
        }

        public SaftDTO ObterPorPK(SaftDTO dto)
        {
            return ObterPorFiltro(dto).SingleOrDefault();
        }   

    }
}
