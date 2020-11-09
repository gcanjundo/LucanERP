using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class ConvenioDTO:TabelaGeral
    {
        private int _instituicao = 0;

        public int Instituicao
        {
            get { return _instituicao; }
            set { _instituicao = value; }
        }
        private string _nomeInstituicao = "";

        public string NomeInstituicao
        {
            get { return _nomeInstituicao; }
            set { _nomeInstituicao = value; }
        }
        private string _tipo = "";

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        private DateTime _inicio = DateTime.MinValue;

        public DateTime Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }
        private DateTime _termino = DateTime.MinValue;

        public DateTime Termino
        {
            get { return _termino; }
            set { _termino = value; }
        }
    }
}
