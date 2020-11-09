using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.RecursosHumanos
{
    public class GrupoSalarialDTO:TabelaGeral
    {
         
        public int Moeda { get; set; } 
        public string SiglaMoeda { get; set; }
        public decimal SalarioBase { get; set; }

        public GrupoSalarialDTO()
        {

        }

        public GrupoSalarialDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public GrupoSalarialDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public GrupoSalarialDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public GrupoSalarialDTO(int pCodigo, int pMoeda) 
        {
            Codigo = pCodigo;
            Moeda = pMoeda;
        }

        public GrupoSalarialDTO(string pDescricao, int pMoeda)
        {
            Descricao = pDescricao;
            Moeda = pMoeda;
        }


        public GrupoSalarialDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public GrupoSalarialDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, decimal pSalario, int pMoeda)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            SalarioBase = pSalario;
            Moeda = pMoeda;
        }

        public GrupoSalarialDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, int pMoeda, string pSiglaMoeda, decimal pSalario)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Moeda = pMoeda;
            SiglaMoeda = pSiglaMoeda;
            SalarioBase = pSalario;
        }
    }
}
