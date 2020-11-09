using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;

namespace Dominio.Clinica
{
    public class AtendimentoDTO:RetornoDTO
    {
        
        public int PacienteID { get; set; }
        public string Paciente { get; set; } 
        public string Profissional { get; set; } 
        public string Especialidade { get; set; }  
        public DateTime DataRegisto { get; set; }  
        public string Ambulatorio { get; set; }    
        public string ActoPrincipal { get; set; } 
        public object PlanoSaude { get; set; } 
        public string Registo { get; set; } 
        public string Obs { get; set; } 
        public string Alergia { get; set; } 
        public string Advertencias { get; set; } 
        public string HistoricoFamiliar { get; set; } 
        public string HistoricoSocial { get; set; } 
        public string HistoricoMedico { get; set; } 
        public string HistoricoPessoal { get; set; } 
        public string Prioridade { get; set; } 
        public string Membros { get; set; } 
        public string SistemaNervoso { get; set; } 
        public string Abdomen { get; set; } 
        public string ObsTorax { get; set; } 
        public string AuscultacaoPulmonar { get; set; } 
        public string AuscultacaoCardiaca { get; set; } 
        public string Cabeca { get; set; } 
        public string HistoriaDoencaActual { get; set; } 
        public string Desfecho { get; set; } 
        public string Filtro { get; set; } 
        public string Fotografia { get; set; } 
        public string Departamento { get; set; } 
        public string NumeroBeneficiario { get; set; }
        public string AcompanhanteID { get; set; }
        public string RelacaoUtente { get; set; }
        public string Contacto { get; set; }
        public string Proveniencia { get; set; }
        public string Transporte { get; set; }
        public DateTime DataChegada { get; set; }
        public string Hora { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime BookedTime { get; set; }
        public int ProfissionalID { get; set; }
        public int EspecialidadeID { get; set; }

        public AtendimentoDTO()
        {

        }

        public AtendimentoDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
        } 
         
    }
}
