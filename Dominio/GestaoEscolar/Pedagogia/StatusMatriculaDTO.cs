using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class StatusMatriculaDTO: TabelaGeral
    {
        private int _movimento = 0;

        public int Movimento
        {
            get { return _movimento; }
            set { _movimento = value; }
        }
        private string _nomeMovimento = "";

        public string NomeMovimento
        {
            get { return _nomeMovimento; }
            set { _nomeMovimento = value; }
        }

        private int _taxa = 0;

        public int Taxa
        {
            get { return _taxa; }
            set { _taxa = value; }
        }
        private int _multa = 0; 

        public int Multa
        {
            get { return _multa; }
            set { _multa = value; }
        }

        public StatusMatriculaDTO()
        {
        
        }

        public StatusMatriculaDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
            this.Descricao ="";
        }

        public StatusMatriculaDTO(string pDescricao)
        {
            this.Codigo = 0;
            this.Descricao = pDescricao;
        }

        public StatusMatriculaDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public StatusMatriculaDTO(string pDescricao, int pmovimento)
        {
            this.Movimento = pmovimento;
            this.Descricao = pDescricao;
        }

        public StatusMatriculaDTO(int pCodigo, string pDescricao, int pmovimento, int pStatus)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Movimento = pmovimento;
            this.Estado = pStatus;
        }

        public StatusMatriculaDTO(string pDescricao, string pSigla)
        {
            // TODO: Complete member initialization
            Sigla = pSigla;
            Descricao = pDescricao;
        }

         


    }
}
