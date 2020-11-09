using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class DisciplinaDTO:TabelaGeral
    {
       
        public DisciplinaDTO()
        {

        }

        public DisciplinaDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public DisciplinaDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao ?? string.Empty;
        }

        public DisciplinaDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public DisciplinaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public DisciplinaDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
        }

        public DisciplinaDTO(string pPeriodo, string pAnoLectivo)
        {
            // TODO: Complete member initialization
            this.Periodo = Convert.ToInt32(pPeriodo);
            this.AnoLectivo = Convert.ToInt32(pAnoLectivo);
        }


        public int Classe { get; set; }

        public int Periodo { get; set; }

        public int Plano { get; set; } 
    }
}
