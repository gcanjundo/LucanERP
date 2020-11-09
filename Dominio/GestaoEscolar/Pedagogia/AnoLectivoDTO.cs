using Dominio.GestaoEscolar.Faturacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class AnoLectivoDTO:RetornoDTO  
    {
        public AnoLectivoDTO(int codigo, String ano, String descricao, DateTime inicio, DateTime termino, 
            DateTime inicioMatricula, DateTime terminoMatricula, string status) 
        {
            this.AnoAno = ano;
            this.AnoCodigo = codigo;
            this.AnoInicio = inicio;
            this.AnoInicioMatricula = inicioMatricula;
            this.AnoDescricao = descricao;
            this.AnoTermino = termino;
            this.AnoTerminoMatricula = terminoMatricula;
            AnoStatus = status;
        }


        public AnoLectivoDTO(int pCodigo, String pDsAno, string pEnsino, string pFilial)
        {
            this.AnoAno = pDsAno;
            this.AnoCodigo = pCodigo;
            Filial = pFilial;
            NivelEnsino = pEnsino;
        }
        public AnoLectivoDTO() 
        {
        
        }

        public AnoLectivoDTO(string pCodigo)
        {
            this.AnoCodigo = int.Parse(pCodigo);
        }

        public AnoLectivoDTO(int pCodigo)
        {
            this.AnoCodigo = pCodigo;
        }

        public AnoLectivoDTO(int pCodigo, string pNivelEnsino, string pFilial)
        {
            this.AnoCodigo = pCodigo;
            NivelEnsino = pNivelEnsino;
            Filial = pFilial;
        }


        private int _anoCodigo=0;

        public int AnoCodigo
        {
            get { return _anoCodigo; }
            set { _anoCodigo = value; }
        }
        private String _anoAno="";

        public String AnoAno
        {
            get { return _anoAno; }
            set { _anoAno = value; }
        }
        private String _anoDescricao="";

        public String AnoDescricao
        {
            get { return _anoDescricao; }
            set { _anoDescricao = value; }
        }
        private DateTime _anoInicio;

        public DateTime AnoInicio
        {
            get { return _anoInicio; }
            set { _anoInicio = value; }
        }
        private DateTime _anoTermino;

        public DateTime AnoTermino
        {
            get { return _anoTermino; }
            set { _anoTermino = value; }
        }
        private DateTime _anoInicioMatricula;

        public DateTime AnoInicioMatricula
        {
            get { return _anoInicioMatricula; }
            set { _anoInicioMatricula = value; }
        }
        private DateTime _anoTerminoMatricula;

        public DateTime AnoTerminoMatricula
        {
            get { return _anoTerminoMatricula; }
            set { _anoTerminoMatricula = value; }
        }

        private string _anoStatus;

        public string AnoStatus
        {
            get { return _anoStatus; }
            set { _anoStatus = value; }
        }
         
        public string TaxaInscricao { get; set; }

        public string MultaMatricula { get; set; }

        public List<PeriodoLectivoDTO> PeriodosLectivosList { get; set; }
        
    }

   
}
