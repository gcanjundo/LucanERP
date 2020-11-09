using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;

namespace Dominio.Clinica
{
    public class ConvenioDTO:TabelaGeral
    {
         
        public decimal ValorParceiro { get; set; }
        public decimal ValorUtente { get; set; }
        public DateTime Validade { get; set; }
        public decimal PrecoProposto { get; set; } 
        public decimal ValorAcordado { get; set; }
        public decimal PercentualProposto { get; set; }
        public List<ConvenioCoberturaItemDTO> ItensCobertos { get; set; }  
        public ConvenioDTO()
        {

        }

        public ConvenioDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public ConvenioDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public ConvenioDTO(string pDescricao, int pEntidade)
        {
            this.Descricao = pDescricao;
            this.Entidade = pEntidade;
        } 


    }

    public class ConvenioCoberturaItemDTO : RetornoDTO
    {
        public int Artigo { get; set; }
        public int ConvenioID { get; set; }
        public decimal ValorParceiro { get; set; }
        public decimal ValorUtente { get; set; } 
        public string ItemDesignation { get; set; }
        public string AggrementDesignation { get; set; }
        public decimal PrecoProposto { get; set; }
        public decimal PrecoAcordado { get; set; }
        public decimal PrecoVendaPublico { get; set; }

        public ConvenioDTO Convenio { get; set; }
    }
}
