using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class EstagioDTO:RetornoDTO
    { 
        public DocenteDTO Docente { get; set; }
        public AnoCurricularDTO Ano { get; set; } 
        public bool IsPago { get; set; }
    }
}
