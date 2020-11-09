using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class RetornoDTO
    {

        public int Codigo { get; set; }
        public string Designacao { get; set; }
        public string Descricao { get; set; }
        public bool Sucesso { get; set; }  
        public string MensagemErro { get; set; }
        public int OrderNumber { get; set; }
        public string Utilizador { get; set; }
        public string Filial { get; set; }   
        public int Entidade { get; set; } 
        public string DesignacaoEntidade { get; set; }   
        public string CurrentPassword { get; set; } 
        public string SenhaNova { get; set; }
        public string CompanyLogo { get; set; } 
        public string CompanyName { get; set; } 
        public string CompanyAddress { get; set; } 
        public string CompanyCity { get; set; } 
        public string CompanyVAT { get; set; } 
        public string CompanyPhone { get; set; } 
        public string Street { get; set; } 
        public string TituloDocumento { get; set; } 
        public int UserProfile { get; set; } 
        public int Status { get; set; } 
        public string Situacao { get; set; } 
        public string FuncionarioID { get; set; } 
        public string SocialName { get; set; }

        public int Supervisor { get; set; }

        public bool IsCashRegister { get; set; }

        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CaixaPostal { get; set; }

        public string WareHouseName { get; set; }
        public string NivelEnsino { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime LookupDate1 { get; set; }

        public DateTime LookupDate2 { get; set; }

        public DateTime LookupDate3 { get; set; }

        public DateTime LookupDate4 { get; set; }

        public DateTime LookupDate5 { get; set; }

        public DateTime LookupDate6 { get; set; }

        public DateTime LookupDate7 { get; set; }

        public DateTime LookupDate8 { get; set; }

        public DateTime LookupDate9 { get; set; }
        
        public DateTime LookupDate10 { get; set; }

        public DateTime LookupDate11 { get; set; }

        public DateTime LookupDate12 { get; set; } 

        public int NroOrdenacao { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string CancelledBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CancelledDate { get; set; }
        public string ShortName { get; set; }

        public RetornoDTO()
        {
            Sucesso = false;
            MensagemErro = string.Empty;
            Utilizador = string.Empty;
            CurrentPassword = string.Empty;
            SenhaNova = string.Empty;
            TituloDocumento = string.Empty;
        }

        public string LookupField1 { get; set; }
        public string LookupField2 { get; set; }
        public string LookupField3 { get; set; }
        public string LookupField4 { get; set; }
        public string LookupField5 { get; set; } 
        public string LookupField6 { get; set; }
        public string LookupField7 { get; set; }
        public string LookupField8 { get; set; }
        public string LookupField9 { get; set; }
        public string LookupField10 { get; set; }
        public string LookupField11 { get; set; }

        
        public decimal LookupNumericField1 { get; set; }
        public decimal LookupNumericField2 { get; set; }
        public decimal LookupNumericField3 { get; set; }
        public decimal LookupNumericField4 { get; set; }
        public decimal LookupNumericField5 { get; set; }
        public decimal LookupNumericField6 { get; set; }
        public decimal LookupNumericField7 { get; set; }
        public decimal LookupNumericField8 { get; set; }
        public decimal LookupNumericField9 { get; set; }
        public decimal LookupNumericField10 { get; set; }
        public decimal LookupNumericField11 { get; set; }

        public string Url { get; set; } 
        public int StockIncomeSerieID { get; set; }
        public int StockOutcomeSerieID { get; set; }
        public bool Cancelled { get; set; }
        public object Connection { get; set; }
        public int AnoLectivo { get; set; }
        public int AnoCivil { get; set; }
    }
}
