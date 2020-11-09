using Dominio.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Faturacao
{
    public class BolsaDTO : PromocaoDTO 
    { 
        public bool RemovedIfLate { get; set; }
        public string Criterio { get; set; }
        public decimal Quantidade { get; set; }
    }
}
