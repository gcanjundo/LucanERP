using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class TriagemDTO:RetornoDTO
    {
        public int Atendimento { get; set; } 
        public string Pulso { get; set; }

        public string Tensao { get; set; }

        public double Peso { get; set; }

        public double Altura { get; set; }

        public string Temperatura { get; set; }
        public string Saturacao { get; set; } 
        public string Respiracao { get; set; }  
        public string GrupoSanguineo { get; set; }

        public string FrequenciaCardiaca { get; set; }

        public string  PerimetroCefalico { get; set; }

        public string Glicemia { get; set; }

        public string Colheita { get; set; }

        public DateTime Data { get; set; }
        public TriagemDTO(int pAtendimento)
        {
            // TODO: Complete member initialization
           Atendimento = pAtendimento;
        }

        public TriagemDTO()
        {
            // TODO: Complete member initialization
        }

       
    }
}
