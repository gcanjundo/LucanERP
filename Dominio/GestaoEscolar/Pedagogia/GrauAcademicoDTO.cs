using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class GrauAcademicoDTO:TabelaGeral
    {
         
        public GrauAcademicoDTO()
        {

        }

        public GrauAcademicoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public GrauAcademicoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public GrauAcademicoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public GrauAcademicoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pNivelEnsino)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            NivelEnsino = pNivelEnsino;
        }

        public GrauAcademicoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
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
