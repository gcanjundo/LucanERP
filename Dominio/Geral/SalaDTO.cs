using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class SalaDTO
    {
        public int Codigo
        {
            get; set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public int Lotacao
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public SalaDTO()
        {
            Codigo = -1;
            Descricao=string.Empty;
            Lotacao =0;
            Estado ="A";
        }

        public SalaDTO(int pCodigo, string pDescricao, int pLotacao, string pEstado) 
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Lotacao = pLotacao;
            Estado = pEstado;
        }

        public SalaDTO(int pCodigo)
        {
            Codigo = pCodigo; 
        }

        public string MensagemErro { get; set; }
    }
}
