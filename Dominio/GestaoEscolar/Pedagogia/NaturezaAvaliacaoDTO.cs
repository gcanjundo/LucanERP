using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class NaturezaAvaliacaoDTO: TabelaGeral
    {
        public NaturezaAvaliacaoDTO()
        {

        }


        public NaturezaAvaliacaoDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public NaturezaAvaliacaoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public NaturezaAvaliacaoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public NaturezaAvaliacaoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public NaturezaAvaliacaoDTO(int pCodigo, string pDescricao, string pSigla, string pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla; 
        }

        public string Tipo { get; set; }
    }
}
