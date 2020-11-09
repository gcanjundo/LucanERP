using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class TransferenciaDTO
    {
        private Int32 _transCodigo = 0;

        public Int32 TransCodigo
        {
            get { return _transCodigo; }
            set { _transCodigo = value; }
        }
        private Int32 _transAluno = 0;

        public Int32 TransAluno
        {
            get { return _transAluno; }
            set { _transAluno = value; }
        }

        private Int32 _transInstituicao = 0;

        public Int32 TransInstituicao
        {
            get { return _transInstituicao; }
            set { _transInstituicao = value; }
        }
        private String _transDsInstituicao = "";

        public String TransDsInstituicao
        {
            get { return _transDsInstituicao; }
            set { _transDsInstituicao = value; }
        }
        private String _transMotivo = "";

        public String TransMotivo
        {
            get { return _transMotivo; }
            set { _transMotivo = value; }
        }
    }
    public class ListaTranferenciasDTO : List<TransferenciaDTO> 
    {
    
    }
}
