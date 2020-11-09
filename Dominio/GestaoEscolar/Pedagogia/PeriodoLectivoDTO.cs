using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class PeriodoLectivoDTO:RetornoDTO
    {
          

        public string DescricaoAno { get; set; } 
        public int Estado { get; set; }
        public string Sigla { get; set; }
        public PeriodoLectivoDTO()
        {
            // TODO: Complete member initialization
            Codigo = -1;
            AnoLectivo = 0;
            DataInicio = DateTime.MinValue;
            DataTermino = DateTime.MinValue;
            Descricao = "";
            
        }

        public PeriodoLectivoDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public PeriodoLectivoDTO(int pCodigo, int pAno)
        {
            Codigo = pCodigo;
            AnoLectivo = pAno;
        }

        public PeriodoLectivoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public PeriodoLectivoDTO(int pCodigo, string pDescricao,  int pAno, int pEstado)
        {
            Codigo = pCodigo;
            AnoLectivo = pAno;
            Descricao = pDescricao;
            Estado = pEstado;
        }

        public PeriodoLectivoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, int pAnoLectivo, DateTime pInicio, DateTime pTermino, bool pSucesso, string pMensagem)
        {
            // TODO: Complete member initialization
            Codigo = pCodigo;
            AnoLectivo = pAnoLectivo;
            DataInicio = pInicio;
            DataTermino = pTermino;
            Descricao = pDescricao;
            Sigla = pSigla;
            
        }
    }
}
