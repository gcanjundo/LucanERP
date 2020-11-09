using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Tesouraria
{
    public class CambioDTO:RetornoDTO
    { 

        public CambioDTO()
        {

        }

        public CambioDTO(string pMoeda)
        {
            Moeda = pMoeda;
        }

        public CambioDTO(int pCodigo, string pMoeda, decimal cambio, DateTime pFrom, DateTime pUntil, string pDescricao)
        {
            Codigo = pCodigo;
            Moeda = pMoeda;
            CambioCompra = cambio;
            Inicio = pFrom;
            Termino = pUntil;
            Descricao = pDescricao;
        }

        public CambioDTO(string pMoeda, string pCompanyID)
        {
            // TODO: Complete member initialization
            Moeda = pMoeda;
            Filial = pCompanyID;
        }

        
        public string Moeda { get; set; }
        public decimal CambioCompra { get; set; }
        public decimal CambioVenda { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int BancoID { get; set; }
        public int MoedaBase { get; set; }
        public DateTime Data { get; set; }  
    }
}
