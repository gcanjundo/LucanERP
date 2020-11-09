using Dominio.Geral;
using Dominio.RecursosHumanos;

namespace Dominio.Clinica
{
    public class ProfissionaDTO: PessoaDTO
    {
        public int Especialidade { get; set; } 
        public string CedulaProfissional { get; set; } 
        public string AreaProfissional { get; set; }  
        public string Registo { get; set; }
        public int ProfissionalID { get; set; }
        public int UserID { get; set; }
        public string Tratamento { get; set; }
    }
}
