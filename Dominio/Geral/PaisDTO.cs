using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class PaisDTO:TabelaGeral
    {

        public string Nacionalidade { get; set; }
        public PaisDTO()
        {

        }

        public PaisDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public PaisDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public PaisDTO(int pCodigo, string pDescricao, string pNacionalidade)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pNacionalidade;
        }

        public PaisDTO(int pCodigo, string pDescricao, string pNacionalidade, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Nacionalidade = pNacionalidade;
            Estado = pEstado;
        }

        public PaisDTO(int pCodigo, string pDescricao, string pNacionalidade, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Nacionalidade = pNacionalidade;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }


        public string Moeda { get; set; }
    }
}
