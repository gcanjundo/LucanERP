using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class PessoaDTO: EntidadeDTO
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Sexo { get; set; } 
        public string EstadoCivil { get; set; }   
        public string Habilitacoes { get; set; }
        public int PaiID { get; set; }
        public int MaeID { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public int GrupoSanguineo { get; set; }
        public decimal Altura { get; set; } 
        public decimal Peso { get; set; }
        public IdadeDTO Idade { get; set; }
        public string Naturalidade { get; set; }


    }
}
