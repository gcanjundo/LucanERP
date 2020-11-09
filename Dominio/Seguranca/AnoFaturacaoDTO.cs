using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Seguranca
{
    public class AnoFaturacaoDTO:RetornoDTO
    {
        public int Ano { get; set; }  
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public bool Actived { get; set; }

        public AnoFaturacaoDTO()
        {

        }

        public AnoFaturacaoDTO(int pAno, string pDescricao, DateTime pFrom, DateTime pUntil, bool pStatus)
        {
            Ano = pAno;
            Descricao = pDescricao;
            Inicio = pFrom;
            Termino = pUntil;
            Actived = pStatus;
        }
    }
}
