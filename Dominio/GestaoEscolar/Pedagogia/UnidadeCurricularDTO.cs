using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class UnidadeCurricularDTO:RetornoDTO
    {
        public UnidadeCurricularDTO(int codigo, AnoCurricularDTO plano, PeriodoLectivoDTO periodo, DisciplinaDTO disciplina,
       String tipo, string classificacao, int cargaHoraria, String conteudo, int status) {

           CargaHoraria = cargaHoraria;
           Classificacao = classificacao;
           Codigo = codigo;
           Conteudo = conteudo;
           Disciplina = disciplina;
           PeriodoLectivo = periodo;
           PlanoCurricular = plano;
           Tipo = tipo;
           Status = status;
            
        }

        public UnidadeCurricularDTO() 
        {
        
        }

        public UnidadeCurricularDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
        }

        public UnidadeCurricularDTO(int pPlano, int pStatus)
        {
            this.PlanoCurricular = new AnoCurricularDTO(pPlano);
            this.Status = pStatus;
        }

        public UnidadeCurricularDTO(string pDisciplina)
        {
            this.NomeDisciplina = pDisciplina;
        }

        public UnidadeCurricularDTO(int pCodigo, string pDisciplina)
        {
            this.Codigo = pCodigo;
            this.NomeDisciplina = pDisciplina;
        }

        private int _codigo=-1;
         
        public AnoCurricularDTO PlanoCurricular { get; set; }
        
        private PeriodoLectivoDTO _periodoLectivo = new PeriodoLectivoDTO();

        public PeriodoLectivoDTO PeriodoLectivo { get; set; }

        public DisciplinaDTO Disciplina { get; set; }

        public String Tipo { get; set; }

        public string Classificacao { get; set; }

        public int CargaHoraria { get; set; }

        public String Conteudo { get; set; } 
        public String NomeDisciplina { get; set; }

        public string Turma { get; set; }

        public string Creditos { get; set; }
        public int Ordem { get; set; }
        public string DocumentDesignation { get; set; }
        public bool AllowPG { get; set; }
        public bool AllowEX { get; set; }
        public bool AllowRC { get; set; }
        public int CargaTeorica { get; set; }
        public int CargaTeoriaPratica { get; set; }
        public int CargaHorariaPeriodo { get; set; }
        public int PraticaLaboratorial { get; set; }
        public int PrecedenteID { get; set; }
        public string Nivel { get; set; }
    }

    
}
