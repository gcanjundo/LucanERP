using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Stock
{
   public class MovimentoStockDTO:RetornoDTO
    {
       
       public int ArmazemID { get; set; }
       public int ArmazemFrom { get; set; }
       public int ArmazemTo { get; set; }
       public int Documento { get; set; }
       public int DocumentoFrom { get; set; }
       public int Numeracao { get; set; }
       public DateTime DataStock { get; set; }
       public DateTime Lancamento { get; set; }       
       public string Referencia { get; set; }
       public int Operacao { get; set; }
       public List<ItemMovimentoStockDTO> ListaArtigo;

       public MovimentoStockDTO()
       {
           Codigo = -1;
           Referencia = string.Empty;
           ListaArtigo = new List<ItemMovimentoStockDTO>();
       }

       public MovimentoStockDTO(int pCodigo)
       {
           Codigo = pCodigo;
       }

       public int Serie { get; set; }

       public string NomeDocumento { get; set; }

       public string NomeArmazemFrom { get; set; }

       public string NomeArmazemTo { get; set; }

       public object DocumentID { get; set; }

       public object EntityID { get; set; }

       public object TranferenciaID { get; set; }

       public object DocumentTypeFromID { get; set; }
       public int MotivoID { get; set; } 
       public int Moeda { get; set; }
       public decimal Cambio { get; set; }
    }
}
