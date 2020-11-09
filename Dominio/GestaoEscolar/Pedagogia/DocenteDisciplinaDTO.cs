using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class DocenteDisciplinaDTO:RetornoDTO
    {
        public string DocenteName { get; set; }
        public string DisciplinaDesgination { get; set; }
        public DocenteDTO Docente { get; set; }
        public UnidadeCurricularDTO Disciplina { get; set; }
         

    }
}
