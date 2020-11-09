using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class PrescricaoItemDTO:RetornoDTO
    {
        
        public int Prescricao { get; set; }
        public int Medicamento { get; set; }

        public string Quantidade { get; set; }
        public string Duracao { get; set; }
        public string Instucoes { get; set; }
        public string  ContraIndicacoes { get; set; }

        public PrescricaoItemDTO()
        {
            Codigo = 0;
            Prescricao = 0;
            Medicamento = 0;
            Quantidade = "";
            Duracao = "";
            Instucoes = "";
            ContraIndicacoes = "";

        }

        public PrescricaoItemDTO(int pCodigo)
        {
            Codigo = pCodigo;
            Prescricao = 0;
            Medicamento = 0;
            Quantidade = "";
            Duracao = "";
            Instucoes = "";
            ContraIndicacoes = "";

        }

        public PrescricaoItemDTO(int pCodigo, int pPrescricao, int pMedicamento, string pQuantidade, string pDuracao, string pInstrucoes, string pContraIndicacoes)
        {
            Codigo = pCodigo;
            Prescricao = pPrescricao;
            Medicamento = pMedicamento;
            Quantidade = pQuantidade;
            Duracao = pDuracao;
            Instucoes = pInstrucoes;
            ContraIndicacoes = pContraIndicacoes;
        }
    }
}
