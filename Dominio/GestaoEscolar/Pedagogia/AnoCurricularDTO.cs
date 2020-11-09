using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class AnoCurricularDTO:RetornoDTO
    {
        public AnoCurricularDTO(int pCodigo, RamoDTO pRamo, int pAnoCurricular, string pDescricao, string pTipo)
        {
            this.AnoCurricular = pAnoCurricular;
            this.Ramo = pRamo;
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Tipo =pTipo;
        }

        public AnoCurricularDTO() 
        {
        
        }

        public AnoCurricularDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
        }

        public AnoCurricularDTO(int pCodigo, string pDescricao)
        {
            // TODO: Complete member initialization
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

         
        public RamoDTO Ramo { get; set; }
        public string Tipo { get; set; }
        public int AnoCurricular { get; set; } 
        public string DataLimite { get; set; } 
        public int Inicio { get; set; } 
        public int Final { get; set; }
        public int TurmaID { get; set; }
        public int Idade { get; set; }
        public bool IsNotaEstagioRequired { get; set; }
    }

}
