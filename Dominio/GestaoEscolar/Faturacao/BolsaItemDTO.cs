using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Faturacao
{
    public class BolsaItemDTO:RetornoDTO
    {
         
        public string ItemCobranca { get; set; } 
        public string Bolsa { get; set; } 
        public DateTime Inicio { get; set; } 
        public DateTime Validade { get; set; } 
        public Decimal Valor { get; set; } 
        public Decimal Percentagem { get; set; }       
        public string Multa { get; set; } 

        public BolsaItemDTO()
        {
            Bolsa = "";
            ItemCobranca = "";
            Filial = "-1";
            Valor = 0;
            Inicio = DateTime.MinValue;
            Validade = DateTime.MinValue;
            Multa = "N";

        }

        public BolsaItemDTO(string pBolsa)
        {
            // TODO: Complete member initialization
            Bolsa = pBolsa;
        }


       

        public BolsaItemDTO(string pBolsa, string pFilial, string pItem, string pMulta)
        {
            Bolsa = pBolsa;
            Filial = pFilial;
            ItemCobranca = pItem;
            Multa = pMulta;
        }




        public int CategoryID { get; set; }
    }
}
