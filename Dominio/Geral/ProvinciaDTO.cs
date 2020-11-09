using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class ProvinciaDTO: TabelaGeral
    {
        public int PaisId { get; set; }
        public string CountryName { get; set; }

        public ProvinciaDTO()
        {
            Descricao = "";
        }

        public ProvinciaDTO(int pCodigo)
        {
            Codigo = pCodigo;
            Descricao ="";
        }

        public ProvinciaDTO(string pDescricao)
        {
            Codigo = 0;
            Descricao = pDescricao;
        }

        public ProvinciaDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public ProvinciaDTO(string pDescricao, int pPaisId)
        {
            PaisId = pPaisId;
            Descricao = pDescricao;
        }

        public ProvinciaDTO(int pCodigo, string pDescricao, int pPaisId, int pStatus)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            PaisId = pPaisId;
            Estado = pStatus;
        }

        public ProvinciaDTO(int pCodigo, string pDescricao, string pSigla, int pPaisId, int pStatus)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            PaisId = pPaisId;
            Estado = pStatus;
            Sigla = pSigla;
        }
    }
}
