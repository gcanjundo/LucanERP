using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    
    public class AlunoDTO:PessoaDTO
    {
        public int AlunoID { get; set; } 
        public string Inscricao { get; set; }  
        public DateTime DataInscricao { get; set; }  
        public string Curso { get; set; }
        public int Classe { get; set; } 
        public int Turma { get; set; }  
        public string SituacaoInicial { get; set; } 
        public string SituacaoFinal { get; set; } 
        public string Encarregado { get; set; }
        public string TelEncarregado { get; set; }
        public string EncEmail { get; set; } 
        public string EncTelAlternativo { get; set; } 
        public string Parentesco { get; set; } 
        public int Matricula { get; set; } 
        public string Turno { get; set; }



        public AlunoDTO() 
        {
        
        }

        public AlunoDTO(int pCodigo)
        {
            this.Codigo = pCodigo;
        }

        public AlunoDTO(int pCodigo, int pAnoLectivo)
        {
            // TODO: Complete member initialization
            Codigo = pCodigo;
            AnoLectivo = pAnoLectivo;
        }

        
        public AlunoDTO(int pCodigo, string pFiltro)
        {
            Codigo = pCodigo;
            DescricaoConvenio = pFiltro;
        }

        public AlunoDTO(string pNome)
        {
            NomeCompleto = pNome;
        }

        public AlunoDTO(string pCodigo, string pNome)
        {
            Codigo = int.Parse(pCodigo);
            NomeCompleto = pNome;
        }
        public AlunoDTO(int pCodigo, int pAnoLectivo, string pFiltro)
        {
            Codigo = pCodigo;
            AnoLectivo = pAnoLectivo;
            DescricaoConvenio = pFiltro;
        }
        

        public AlunoDTO(int pCodigo, decimal pSaldo)
        {
            this.Codigo = pCodigo;
            SaldoCorrente = pSaldo;
        }

        public AlunoDTO(string pCodigo, string pInscricao, string pNome)
        {
            Codigo = int.Parse(pCodigo);
            NomeCompleto = pNome;
            Inscricao = pInscricao;
        }

        public decimal SaldoCorrente { get; set; } 
        public string Disciplina { get; set; } 
        public string SituacaoAcademica { get; set; } 
        public string CursoID { get; set; }  
        public string OpcaoLingua { get; set; }

        public string EscolaProveniencia { get; set; }

        public string SaidaProveniencia { get; set; }

        public string SituacaoProveniencia { get; set; }

        public string CursoProveniencia { get; set; }

        public string ClasseProveniencia { get; set; }

        public string NroExterno { get; set; }

        public string SiglaTurma { get; set; }
        public bool AcessoPortal { get; set; }
        public bool IsExterno { get; set; } 
        public string DescricaoConvenio { get; }
        public int StatusMatricula { get; set; }
        public string DescricaoEstado { get; set; }
    }
}
