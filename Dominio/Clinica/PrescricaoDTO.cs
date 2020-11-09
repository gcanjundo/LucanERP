using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class PrescricaoDTO:RetornoDTO
    {
        
        public int Atendimento { get; set; }
        public DateTime DataEmissao { get; set; }

        public string Profissional { get; set; }

        public PrescricaoDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public PrescricaoDTO(int pCodigo, int pAtendimento)
        {
            Codigo = pCodigo;
            Atendimento = pAtendimento;
        }


    }
}
