using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class FiliacaoDTO:RetornoDTO
    {
        private Int32 _filCodigo = 0;

        public Int32 FilCodigo
        {
            get { return _filCodigo; }
            set { _filCodigo = value; }
        }
        private String _filNome = "";

        public String FilNome
        {
            get { return _filNome; }
            set { _filNome = value; }
        }
        private String _filIdentificacao = "";

        public String FilIdentificacao
        {
            get { return _filIdentificacao; }
            set { _filIdentificacao = value; }
        }

        private DateTime _filDtNascimento;

        public DateTime FilDtNascimento
        {
            get { return _filDtNascimento; }
            set { _filDtNascimento = value; }
        }
        private Int32 _filHabilitacoesID = 0;

        public Int32 FilHabilitacoesID
        {
            get { return _filHabilitacoesID; }
            set { _filHabilitacoesID = value; }
        }
        private String _filHabilitacoesDS = "";

        public String FilHabilitacoesDS
        {
            get { return _filHabilitacoesDS; }
            set { _filHabilitacoesDS = value; }
        }
        private Int32 _filProfissaoID = 0;

        public Int32 FilProfissaoID
        {
            get { return _filProfissaoID; }
            set { _filProfissaoID = value; }
        }
        private String _filProfissaoDS = "";

        public String FilProfissaoDS
        {
            get { return _filProfissaoDS; }
            set { _filProfissaoDS = value; }
        }
        private Int32 _filInstituicaoID = 0;

        public Int32 FilInstituicaoID
        {
            get { return _filInstituicaoID; }
            set { _filInstituicaoID = value; }
        }
        private String _filInstituicaoDS = "";

        public String FilInstituicaoDS
        {
            get { return _filInstituicaoDS; }
            set { _filInstituicaoDS = value; }
        }
        private String _filTelefone = "";

        public String FilTelefone
        {
            get { return _filTelefone; }
            set { _filTelefone = value; }
        }
        private String _fiLTelAlternativo = "";

        public String FiLTelAlternativo
        {
            get { return _fiLTelAlternativo; }
            set { _fiLTelAlternativo = value; }
        }
        private String _filEmail = "";

        public String FilEmail
        {
            get { return _filEmail; }
            set { _filEmail = value; }
        }

        private AlunoFiliacaoDTO _filAlunoFiliacao;

        public AlunoFiliacaoDTO FilAlunoFiliacao
        {
            get { return _filAlunoFiliacao; }
            set { _filAlunoFiliacao = value; }
        }

        private ListaAlunoFiliacaoDTO _Pais;

        public ListaAlunoFiliacaoDTO Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }

        
        
    }

    public class ListaFiliacaoDTO : List<FiliacaoDTO>
    {

    }
}
