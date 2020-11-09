using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia 
{
    public class CursoDTO:RetornoDTO
    { 
        public int Duracao { get; set; } 
        public string Especificacao { get; set; } 

        public string Tempo { get; set; } 
        public int Inicio { get; set; } 
        public int Termino { get; set; } 
        public string Formacao { get; set; } 

        public CursoDTO() 
        {
        
        }

        public CursoDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public CursoDTO(int pCodigo, string pDesignacao)
        {
            this.Codigo = pCodigo;
            this.Designacao = pDesignacao;
        }

        public CursoDTO(int pCodigo, string pDesignacao, string pAbreviatura)
        {
            this.Codigo = pCodigo;
            this.Designacao = pDesignacao;
            this.ShortName = pAbreviatura;
        }

        public CursoDTO(int pCodigo, string pDesignacao, string pAbreviatura, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Designacao = pDesignacao;
            this.ShortName = pAbreviatura;
            this.Status = pEstado;
        }

        public CursoDTO(int pCodigo, string pDesignacao, string pAbreviatura, int pDuracao, string pEspecificacao, string pTempo, int pInicio, int pTermino, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Designacao = pDesignacao;
            this.ShortName = pAbreviatura;
            this.Duracao = pDuracao;
            this.Especificacao = pEspecificacao;
            this.Status = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
            this.Tempo = pTempo;
            this.Inicio = pInicio;
            this.Termino = pTermino;
        }

        public string Ensino { get; set; }
        public string Ramo { get; set; }
        public bool isAutorizado { get; set; } 
        public string Area { get; set; } 
        public string Coordenador { get; set; } 
        public string FormatoPauta { get; set; }
    }

   
}
