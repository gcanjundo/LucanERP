using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Lavandaria
{
    public class RoupaDefeitoDTO : RetornoDTO
    {
        
        public int Defeito_Codigo { get; set; } 
        public RoupaDefeitoDTO()
        {

        }
        public RoupaDefeitoDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public RoupaDefeitoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public RoupaDefeitoDTO(int pCodigo,int pDefeitoCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Defeito_Codigo = pDefeitoCodigo;
            Descricao = pDescricao;
           
        }

    }
}
