using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;

namespace Dominio.RecursosHumanos
{
    public class FuncionarioDTO:PessoaDTO
    {
        public string NumeroMecanografico { get; set; }
        public string NomeAbreviado { get; set; }
        public string PrimeiroNome { get; set; }
        public string PrimeiroApelido { get; set; }
        public string SegundoApelido { get; set; }
        public string SobreNomeSolteiro { get; set; }
        public string SobreNomeConjuge { get; set; }
        public int TipoRendimentoID { get; set; }
        public decimal TaxaFixaIRT { get; set; }
        public bool PertenceOrgaosSociais { get; set; }
        public int ModoProcessamentoSalarial { get; set; }
        public string TipoSubsidioAlimentacao { get; set; } // DP - DIAS PROCESSADOS; VF - VALOR FIXO
        public string Cargo { get; set; }
        public string ContratoTrabalhoID { get; set; }
        public string Departamento { get; set; }
        public string Turno { get; set; }
        public string CategoriaSalarial { get; set; }
        public decimal Salario { get; set; }

        public string MotivoDemissao { get; set; }

        public string DataDemissao { get; set; }

        public DateTime DataObtencaoFormacao { get; set; }

       
        

        public int Unidade { get; set; }

        public string TipoSalario { get; set; }

        public string CargaHorariaSemanal { get; set; }

        public string CargaHorariaMensal { get; set; }

        public string Vinculo { get; set; }

        public DateTime DataInicioContrato { get; set; }

        public DateTime DataTerminoContrato { get; set; }

        public string VigenciaContrato { get; set; }

        public string PeriodoExperimental { get; set; }

        public DateTime DataAvisoPrevio { get; set; }  
        public DateTime DataAdmissao { get; set; }
        public int FormacaoProfissional { get; set; }
        public int Profissao { get; set; }
        public object ContaBancaria { get; set; }
        public object NIB { get; set; }
        public string RegimeLaboral { get; set; }
        public string Tratamento { get; set; }
        public string MotivoAdmissao { get; set; }
        public bool IsDocente { get; set; }
        public decimal PercentagemIncapacidade { get; set; } 
        public List<AbonoDescontoDTO> AbonosDecontos { get; set; }
    }
     
}
