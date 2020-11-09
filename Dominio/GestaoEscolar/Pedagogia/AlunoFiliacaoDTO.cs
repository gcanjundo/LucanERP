using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class AlunoFiliacaoDTO
    {
        private Int32 _aluFilCodigo = 0;

        public Int32 AluFilCodigo
        {
            get { return _aluFilCodigo; }
            set { _aluFilCodigo = value; }
        }
        private Int32 aluFiliacao = 0;

        public Int32 AluFiliacao
        {
            get { return aluFiliacao; }
            set { aluFiliacao = value; }
        }
        private String aluParentesco;

        public String AluParentesco
        {
            get { return aluParentesco; }
            set { aluParentesco = value; }
        }
    }

    public class ListaAlunoFiliacaoDTO : List<AlunoFiliacaoDTO> 
    {
    
    }
}
