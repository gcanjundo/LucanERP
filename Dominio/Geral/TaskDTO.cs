using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Geral
{
    public class TaskDTO:RetornoDTO
    {

        
        public TipoActividadeDTO Task { get; set; }
        public string Titulo { get; set; }
        public string UtilizadorProprietario { get; set; }
        public int ExecutorID { get; set; } 
        public DateTime ScheduleDate { get; set; }
        public string UtilizadorExecutor { get; set; }
        public int TargetID { get; set; } 
        public int PrioridadeID { get; set; }
        public DateTime BeginImplementationDate { get; set; }
        public string Details { get; set; }
        public string PessoaContacto { get; set; }
        public string ContactoPessoaContacto { get; set; }
        public bool IsClosed { get; set; }
        public bool AllDay { get; set; }
        public int Recorrencia { get; set; }
        public DateTime EndImplementationDate { get; set; } 
        public string TaskReport { get; set; }
        public string InternalNotes { get; set; }
        public DateTime ScheduleEndDate { get; set; }
        public string Color { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }

    }
}
