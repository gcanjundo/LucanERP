
using Dominio.Clinica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class EntidadeDTO: RetornoDTO
    {
        

        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Nacionalidade { get; set; }

        public int Documento { get; set; }

        public string Identificacao { get; set; }

        public string Morada { get; set; }

        public string Rua { get; set; }
        public string  Bairro { get; set; }

        public int MunicipioMorada { get; set; }

        public string Provincia { get; set; } 
        public string Municipio { get; set; }
        public string Telefone { get; set; }

        public string TelefoneAlt { get; set; }

        public string  TelefoneFax { get; set; } 

        public string WebSite { get; set; }

        public bool IsActivo { get; set; }

        public byte[] Fotografia
        { get; set; }

        public string ExtensaoFoto
        { get; set; }

        public string PathFoto
        { get; set; }

        public DateTime Emissao { get; set; }
        public DateTime Validade { get; set; }

        public string LocalEmissao { get; set; }

        public string Categoria { get; set; }

        public string NomeComercial
        {
            get;
            set;
        }

        public string Tipo { get; set; }
        public TipoDTO EntityType { get; set; }
        public string Saldo {get; set;}
        public decimal Desconto { get; set; }

        public decimal LimiteCredito { get; set; }
        public decimal DescontoLinha { get; set; }
        public string DiasVencimento { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTerms { get; set; }
        public string ModoExpedicao { get; set; }
        public string PaymentDays { get; set; }
        public string Currency { get; set; }
        public string AccountManager { get; set; }
        public string TablePriceID { get; set; }
        public string Banco { get; set; }
        public string AccountBank { get; set; }
        public string IBAN { get; set; }
        public string Swift { get; set; }
        public string ComercialContactName { get; set; }
        public string ComercialContactFunction { get; set; }
        public string ComercialContactPhoneNumber { get; set; }
        public string ComercialContactEmail { get; set; }
        public string AdministrativeContactName { get; set; }
        public string AdministrativeContactFunction { get; set; }
        public string AdministrativeContactPhoneNumber { get; set; }
        public string AdministrativeContactEmail { get; set; }
        public string ChargesContactName { get; set; }
        public string ChargesContactFunction { get; set; }
        public string ChargesContactPhoneNumber { get; set; }
        public string ChargesContactEmail { get; set; } 
        public List<EntidadeDTO> AssociedEntities { get; set; }
        public bool IsTerceiro { get; set; }
        public bool IsCompanyInsurance { get; set; }
        public string ChargesManager { get; set; }
        public int PaisNascimento { get; set; }
        public int MunicipioNascimento { get; set; }
        public int LocalNascimento { get; set; }
        public string NomeFormacao { get; set; }
        public string DesignacaoNacionalidade { get; set; }
        public string Distrito { get; set; }
        public string CustomerFiscalCodeID { get; set; }
        public bool AllowAlert { get; set; }
        public object AlertDays { get; set; }
        public bool AlertPendingValue { get; set; }
        public int AngariadorID { get; set; }
        public int RetencaoID { get; set; }
        public int CustomerAccountPlanID { get; set; }
        public int SupplierAccountPlanID { get; set; }
        public bool AllowSefBilling { get; set; }  
        public string BirthDate { get; set; }
        public string CanalContactoPreferencial { get; set; }
        public string CanalAngariacao { get; set; }
        public string SupplierFiscalCodeID { get; set; }
        public ApoliceDTO ApoliceSeguro { get; set; }

        public EntidadeDTO()
        {

        }

        public EntidadeDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public EntidadeDTO(int pCodigo, string pNome)
        {
            Codigo = pCodigo;
            NomeCompleto = pNome;
        }

        public EntidadeDTO(string pNome)
        {
            NomeCompleto = pNome;
        }

        public EntidadeDTO(string pNome, string pFilial)
        {
            NomeCompleto = pNome;
            Filial = pFilial;
        }


        
    }
}
