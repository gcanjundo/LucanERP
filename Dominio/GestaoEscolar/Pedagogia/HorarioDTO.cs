using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class HorarioDTO:RetornoDTO
    {
        private int _horCodigo=0;

        public int HorCodigo
        {
            get { return _horCodigo; }
            set { _horCodigo = value; }
        }
        private DocenteDTO _horDocente = new DocenteDTO();

        public DocenteDTO HorDocente
        {
            get { return _horDocente; }
            set { _horDocente = value; }
        }
        private int _horDiaSemana = -1;

        public int HorDiaSemana
        {
            get { return _horDiaSemana; }
            set { _horDiaSemana = value; }
        }
        private int _horInicio = 0;

        public int HorInicio
        {
            get { return _horInicio; }
            set { _horInicio = value; }
        }
        private int _horTermino = 0;

        public int HorTermino
        {
            get { return _horTermino; }
            set { _horTermino = value; }
        }
        private string _horSala="";

        public string HorSala
        {
            get { return _horSala; }
            set { _horSala = value; }
        }
        private String _horRegime="";

        public String HorRegime
        {
            get { return _horRegime; }
            set { _horRegime = value; }
        }
        private TurmaDTO _horTurma;

        public TurmaDTO HorTurma
        {
            get { return _horTurma; }
            set { _horTurma = value; }
        }
        private UnidadeCurricularDTO _horDisiciplina = new UnidadeCurricularDTO();

        public UnidadeCurricularDTO HorDisiciplina
        {
            get { return _horDisiciplina; }
            set { _horDisiciplina = value; }
        }

        private AnoLectivoDTO _horAnoLectivo;

        public AnoLectivoDTO HorAnoLectivo
        {
            get { return _horAnoLectivo; }
            set { _horAnoLectivo = value; }
        }

        public Decimal Duracao { get; set; }
        public string Turno { get; set; }

        public string SegundaFeira { get; set; }

        public string TercaFeira { get; set; }

        public string QuartaFeira { get; set; }

        public string QuintaFeira { get; set; }

        public string SextaFeira { get; set; }

        public string Sabado { get; set; }

        public DateTime Validade { get; set; }

        public string Curso { get; set; }

        public string Departamento { get; set; }

        public string HorPeriodo { get; set; }

        public string Horario { get; set; }

        public DateTime PeriodoFrom { get; set; }

        public DateTime PeriodoTerm { get; set; }
    }

}
