using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class EmpresaDTO: EntidadeDTO
    {
        public EmpresaDTO()
        {

        }
        public EmpresaDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public EmpresaDTO(string pCodigo)
        {
            Codigo = Convert.ToInt32(pCodigo);
        }

        public EmpresaDTO(int pCodigo, string pDescricao)
        {
            Codigo = Convert.ToInt32(pCodigo);
            base.NomeCompleto = pDescricao;
        }    
        public int EmpresaSede { get; set; } 
        public string Responsavel { get; set; }
        public int ProvinciaMorada { get; set; }
        public string NomeLocalNascimento { get; set; }
        public bool IsColectiveCompany { get; set; }
        public int DefaultWithHoldingID { get; set; }
        public bool IsDefault { get; set; }
    }
}
