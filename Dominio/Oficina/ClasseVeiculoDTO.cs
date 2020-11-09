using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Oficina
{
    public class ClasseVeiculoDTO:TabelaGeral
    {
        public ClasseVeiculoDTO()
        {

        }

        public ClasseVeiculoDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public ClasseVeiculoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public ClasseVeiculoDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla; 
        }

        public ClasseVeiculoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public ClasseVeiculoDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }
    }
}
