using System;

namespace Dominio.Clinica
{
    public class ApoliceDTO:RetornoDTO
    {
        
         
        public bool IsVitalicio { get; set; }
        public string NumeroBeneficiario { get; set; } 
        public int Convenio { get; set; } 
        public string NumPS { get; set; }
        public DateTime Emissao { get; set; } 
        public DateTime Validade { get; set; } 
        public int Beneficiario { get; set; } 
        public ApoliceDTO() {
        
        
        }

        public ApoliceDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
        }  

        public ApoliceDTO(int pConvenio, int pBeneficiario)
        {
            this.Convenio = pConvenio;
            this.Beneficiario = pBeneficiario;
        }
    }
}
