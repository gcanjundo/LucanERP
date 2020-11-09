using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class RamoDTO:RetornoDTO
    {
        public RamoDTO(int pCodigo, String pDescricao, CursoDTO pCurso, string pDesignacao, int pInicio, int pTermino, int pEstado, string pAreaFormacao) 
        {
            this.RamCodigo = pCodigo;
            this.RamCurso = pCurso;
            this.RamDescricao = pDescricao;
            RamDesignacao = pDesignacao;
            this.RamInicio = pInicio;
            this.RamTermino = pTermino;
            RamStatus = 1;
            AreaFormacao = pAreaFormacao;

        }

        public RamoDTO() 
        {
        
        }

        public RamoDTO(string pCodigo) 
        {
            this.RamCodigo = Convert.ToInt32(pCodigo);
        }

        public RamoDTO(CursoDTO pCurso)
        {
            RamCurso = pCurso;
        }

        public RamoDTO(int pID, string pDesignation)
        {
            RamCodigo = pID;
            RamDescricao = pDesignation;
        }

        public RamoDTO(int pCodigo)
        {
            this.RamCodigo = pCodigo;
        }

        private int _ramCodigo= 0;

        public int RamCodigo
        {
            get { return _ramCodigo; }
            set { _ramCodigo = value; }
        }
        private String ramDescricao="";

        public String RamDescricao
        {
            get { return ramDescricao; }
            set { ramDescricao = value; }
        }
        private CursoDTO ramCurso;

        public CursoDTO RamCurso
        {
            get { return ramCurso; }
            set { ramCurso = value; }
        }

        private String _ramDesignacao="";

        public String RamDesignacao
        {
            get { return _ramDesignacao; }
            set { _ramDesignacao = value; }
        }

        private int _ramInicio = 0;

        public int RamInicio
        {
            get { return _ramInicio; }
            set { _ramInicio = value; }
        }

        private int _ramTermino = 0;

        public int RamTermino
        {
            get { return _ramTermino; }
            set { _ramTermino = value; }
        }
        private int _ramStatus = 0;

        public int RamStatus
        {
            get { return _ramStatus; }
            set { _ramStatus = value; }
        }
        private string _erro = "";

        public string Erro
        {
            get { return _erro; }
            set { _erro = value; }
        }

        private String _dsInicio = "";

        public String DsInicio
        {
            get { return _dsInicio; }
            set { _dsInicio = value; }
        }
        private String _dsTermino = "";

        public String DsTermino
        {
            get { return _dsTermino; }
            set { _dsTermino = value; }
        }

        public string AreaFormacao { get; set; }
    }

}
