using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio.Comercial.SAFT
{
    /*
    [XmlRoot(ElementName = "Header")]
    public class Header
    {
        [XmlElement(ElementName = "AuditFileVersion")]
        public string AuditFileVersion { get; set; }
        [XmlElement(ElementName = "CompanyID")]
        public string CompanyID { get; set; }
        [XmlElement(ElementName = "TaxRegistrationNumber")]
        public string TaxRegistrationNumber { get; set; }
        [XmlElement(ElementName = "TaxAccountingBasis")]
        public string TaxAccountingBasis { get; set; }
        [XmlElement(ElementName = "CompanyName")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "BusinessName")]
        public string BusinessName { get; set; }
        [XmlElement(ElementName = "CompanyAddress")]
        public CompanyAddress CompanyAddress { get; set; }
        [XmlElement(ElementName = "FiscalYear")]
        public string FiscalYear { get; set; }
        [XmlElement(ElementName = "StartDate")]
        public string StartDate { get; set; }
        [XmlElement(ElementName = "EndDate")]
        public string EndDate { get; set; }
        [XmlElement(ElementName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
        [XmlElement(ElementName = "DateCreated")]
        public string DateCreated { get; set; }
        [XmlElement(ElementName = "TaxEntity")]
        public string TaxEntity { get; set; }
        [XmlElement(ElementName = "ProductCompanyTaxID")]
        public string ProductCompanyTaxID { get; set; }
        [XmlElement(ElementName = "SoftwareValidationNumber")]
        public string SoftwareValidationNumber { get; set; }
        [XmlElement(ElementName = "ProductID")]
        public string ProductID { get; set; }
        [XmlElement(ElementName = "ProductVersion")]
        public string ProductVersion { get; set; }
        [XmlElement(ElementName = "HeaderComment")]
        public string HeaderComment { get; set; }
        [XmlElement(ElementName = "Telephone")]
        public string Telephone { get; set; }
        [XmlElement(ElementName = "Fax")]
        public string Fax { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Website")]
        public string Website { get; set; }
         
        public List<string> HeaderValidation()
        {
            List<string> ErrorMessageList = new List<string>();
            if (string.IsNullOrEmpty(CompanyID))
            {
                ErrorMessageList.Add("O Campo Registo Comercial da Empresa está vázio");
            }else if (string.IsNullOrEmpty(TaxRegistrationNumber))
            {
                ErrorMessageList.Add("O Campo NIF da empresa está vázio na Ficha de cadastro da empresa");
            }
            else if (string.IsNullOrEmpty(TaxAccountingBasis))
            {
                ErrorMessageList.Add("O Campo NIF da empresa está vázio na Ficha de cadastro da empresa");
            }
            return ErrorMessageList;
        }
    }*/

    [XmlRoot(ElementName = "Header", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
    public class Header
    {
        [XmlElement(ElementName = "AuditFileVersion", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string AuditFileVersion { get; set; }
        [XmlElement(ElementName = "CompanyID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CompanyID { get; set; }
        [XmlElement(ElementName = "TaxRegistrationNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxRegistrationNumber { get; set; }
        [XmlElement(ElementName = "TaxAccountingBasis", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxAccountingBasis { get; set; }
        [XmlElement(ElementName = "CompanyName", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "BusinessName", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string BusinessName { get; set; }
        [XmlElement(ElementName = "CompanyAddress", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public CompanyAddress CompanyAddress { get; set; }
        [XmlElement(ElementName = "FiscalYear", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string FiscalYear { get; set; }
        [XmlElement(ElementName = "StartDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string StartDate { get; set; }
        [XmlElement(ElementName = "EndDate", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string EndDate { get; set; }
        [XmlElement(ElementName = "CurrencyCode", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string CurrencyCode { get; set; }
        [XmlElement(ElementName = "DateCreated", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string DateCreated { get; set; }
        [XmlElement(ElementName = "TaxEntity", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string TaxEntity { get; set; }
        [XmlElement(ElementName = "ProductCompanyTaxID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductCompanyTaxID { get; set; }
        [XmlElement(ElementName = "SoftwareValidationNumber", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string SoftwareValidationNumber { get; set; }
        [XmlElement(ElementName = "ProductID", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductID { get; set; }
        [XmlElement(ElementName = "ProductVersion", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string ProductVersion { get; set; }
        [XmlElement(ElementName = "HeaderComment", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string HeaderComment { get; set; }
        [XmlElement(ElementName = "Telephone", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Telephone { get; set; }
        [XmlElement(ElementName = "Fax", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Fax { get; set; }
        [XmlElement(ElementName = "Email", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Email { get; set; }
        [XmlElement(ElementName = "Website", Namespace = "urn:OECD:StandardAuditFile-Tax:AO_1.01_01")]
        public string Website { get; set; }
    }
}
