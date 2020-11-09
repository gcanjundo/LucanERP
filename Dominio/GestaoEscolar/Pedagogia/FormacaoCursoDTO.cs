using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class FormacaoCursoDTO :TabelaGeral
    {

        public Tuple<int, string> GrauAcademico { get; set; }
        public Tuple<int, bool> Curricular { get; set; }
        public Tuple<int, bool> CurtaDuracao { get; set; }

        public FormacaoCursoDTO ()
        {

        }

        public FormacaoCursoDTO (int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public FormacaoCursoDTO (int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            GrauAcademico = new Tuple<int, string>(-1, "");
        }

        public FormacaoCursoDTO (int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public FormacaoCursoDTO (int pCodigo, string pDescricao, string pSigla, int pEstado, Tuple<int, string>pGrau, Tuple<int, bool> pFormacaoCurricular, Tuple<int, bool> pDuracao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            GrauAcademico = pGrau;
            Curricular = pFormacaoCurricular;
            CurtaDuracao = pDuracao;
        }

        public FormacaoCursoDTO (int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }
    }
}
