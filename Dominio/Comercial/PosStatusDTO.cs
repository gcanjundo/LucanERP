using Dominio.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class PosStatusDTO: RetornoDTO
    { 
        public int POS { get; set; }
        public string DescricaoPos { get; set; }
        public DateTime Data { get; set; }
        public decimal SaldoInicial { get; set; }
        public int Turno { get; set; }
        public DateTime Abertura { get; set; }
        public decimal SaldoFinal { get; set; }
        public DateTime Fecho { get; set; }
        public string IP { get; set; }
        public int DocumentID { get; set; }
        public object DefaultAccount { get; set; }
        public MovimentoDTO PosTransaction { get; set; }
        public decimal ValorSessao { get; set; }
        public DateTime TurnoBegin { get; set; }
        public DateTime TurnoEnd { get; set; }

        public PosStatusDTO()
        {

        }

        public PosStatusDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

    }
}
