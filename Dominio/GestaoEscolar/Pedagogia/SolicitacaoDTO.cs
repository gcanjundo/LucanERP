using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class SolicitacaoDTO:RetornoDTO
    {
        private Int32 _codigo = 0;

        public Int32 Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private String _documento = "";

        public String Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }
        private AlunoDTO _solicitante;

        public AlunoDTO Solicitante
        {
            get { return _solicitante; }
            set { _solicitante = value; }
        }
        

        private String _status = "";

        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
        private String _emissor = "";

        public String Emissor
        {
            get { return _emissor; }
            set { _emissor = value; }
        }

        private String _recibo = ""; 

        public String Recibo
        {
            get { return _recibo; }
            set { _recibo = value; }
        } 

        public SolicitacaoDTO() 
        {
        
        }

        public SolicitacaoDTO(string pCodigo)
        {
            // TODO: Complete member initialization
            this.Codigo = Convert.ToInt32(pCodigo);
        }


        public string De { get; set; }

        public string Ate { get; set; }

        public string Numero { get; set; }

        
        public string Tipo { get; set; }

        public string Motivo { get; set; }

        public string Disciplina { get; set; }

        public List<NotaDTO> Notas { get; set; }

        public string Instituicao { get; set; }

        public string CursoDestino { get; set; }

        public DateTime DataDeferimento { get; set; }

        public string Observacoes { get; set; }

        public string ParecerPedagogia { get; set; }

        public int IsUrgente { get; set; }

        public string ResponsavelDeferimento { get; set; }

        public string MotivoDeferimento { get; set; }

        public decimal Nota { get; set; }

        public string DocFile { get; set; }
    }

}
