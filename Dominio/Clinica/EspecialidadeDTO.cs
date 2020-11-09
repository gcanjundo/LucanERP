using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Clinica
{
    public class EspecialidadeDTO:TabelaGeral
    {
        public EspecialidadeDTO()
        {
            Codigo = 0;
            Descricao ="";
            Sigla = "";
            Estado = 1;
        }

        public EspecialidadeDTO(int pCodigo) 
        {
            this.Codigo = pCodigo;
        }

        public EspecialidadeDTO(int pCodigo, string pDescricao)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
        }

        public EspecialidadeDTO(int pCodigo, string pDescricao, string pSigla)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
        }

        public EspecialidadeDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
        }

        public EspecialidadeDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            this.Codigo = pCodigo;
            this.Descricao = pDescricao;
            this.Sigla = pSigla;
            this.Estado = pEstado;
            this.MensagemErro = pMensagem;
            this.Sucesso = pSucesso;
        }

        public string Categoria { get; set; }
    }

    public class EspecialidadeProfissionalDTO: EspecialidadeDTO
    {
        public int ProfissionalID { get; set; }
        public int EspecialidadeID { get; set; }
        public decimal ValorActo { get; set; }
        public decimal Percentagem { get; set; }
    }

    public class EspecialidadeDepartamentoDTO : EspecialidadeDTO
    {
        public int DepartamentoID { get; set; }
        public int EspecialidadeID { get; set; }
    }
}
