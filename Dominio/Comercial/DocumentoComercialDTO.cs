using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial
{
    public class DocumentoComercialDTO:TabelaGeral
    {
        public string ComentarioRodape { get; set; }
        public string Tipo { get; set; }
        public string Stock { get; set; }
        public string ContaCorrente { get; set; }
        public string Caixa { get; set; }
        public string Formato { get; set; }
        public string Categoria { get; set; }
        public int AllowInsert { get; set; }
        public int AllowDelete { get; set; }
        public int AllowSelect { get; set; }
        public int AllowUpdate { get; set; } 
        public DocumentoComercialDTO()
        {

        }

        public DocumentoComercialDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public DocumentoComercialDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public DocumentoComercialDTO(int pCodigo, string pDescricao, string pSigla, string pCategoria)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Categoria = pCategoria;
        }

        public DocumentoComercialDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public DocumentoComercialDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, string pStock, string pCC, string pCaixa, string pFormato)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            Stock = pStock;
            ContaCorrente = pCC;
            Caixa = pCaixa;
            Formato = pFormato;
        }

        public Nullable <int> ParentID { get; set; }

        public string Link { get; set; }

        public string Template { get; set; }
    }
}
