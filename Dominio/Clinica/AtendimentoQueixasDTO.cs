using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class AtendimentoQueixasDTO:RetornoDTO
    {
        

        public int Atendimento { get; set; }

        public string Queixa { get; set; } 

        public string Tempo { get; set; }

        public AtendimentoQueixasDTO()
        {

        }

        public AtendimentoQueixasDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public AtendimentoQueixasDTO(int pAtendimento, string pQueixa)
        {
            this.Atendimento = pAtendimento;
            this.Queixa = pQueixa;
        }

        public AtendimentoQueixasDTO(int pAtendimento, string pDescricao, string pQueixa)
        {
            this.Atendimento = pAtendimento;
            this.Descricao = pDescricao;
            this.Queixa = pQueixa;
        }

        public AtendimentoQueixasDTO(string pDescricao, string pQueixa)
        {
            
            this.Descricao = pDescricao;
            this.Queixa = pQueixa; 
        }

        public AtendimentoQueixasDTO(int pCodigo, int pAtendimento, string pQueixa, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Queixa = pQueixa;
            Atendimento = pAtendimento;

        }


    }
}
