using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class MunicipioDTO: TabelaGeral
    {
        public int Provincia { get; set; }
        public string NomeProvincia { get; set; }

        public MunicipioDTO() 
        {
        
        }

        public MunicipioDTO(int pCodigo)
        {
            Codigo = pCodigo;
            Descricao = "";
            
        }

        public MunicipioDTO(string pDescricao)
        {   Codigo = 0;
            Descricao = pDescricao;
            Provincia = 0;
        }

        public MunicipioDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public MunicipioDTO(string pDescricao, int pProvincia)
        {
            Provincia = pProvincia;
            Descricao = pDescricao;
        }

        public MunicipioDTO(int pCodigo, string pDescricao, int pProvincia, string pSigla, int pStatus, string pNomeProvincia)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Provincia = pProvincia;
            Sigla = pSigla;
            Estado = pStatus;
            NomeProvincia = pNomeProvincia;
        }


    }
}
