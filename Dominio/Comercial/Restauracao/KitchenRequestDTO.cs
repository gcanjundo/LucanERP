using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Restauracao
{
    public class KitchenRequestDTO:RetornoDTO
    {
        
        public int Ordem { get; set; }
        public int Numeracao { get; set; }
        public int Atendimento { get; set; }

        public string Referencia { get; set; }
        public DateTime Data { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public string Responsavel { get; set; }
        public string Obs { get; set; }
        public object Artigo { get; set; }
        public object PedidoDia { get; set; }

    }
}
