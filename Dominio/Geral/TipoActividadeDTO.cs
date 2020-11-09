using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class TipoActividadeDTO : TabelaGeral
    {
        public TipoActividadeDTO()
        {

        }

        public TipoActividadeDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public TipoActividadeDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public TipoActividadeDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public TipoActividadeDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public TipoActividadeDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }

        enum TaskType
        {
            Telefonema,
            ApresentacaoProposta,
            Compromisso,
            EnvioCarta,
            EnvioEmail,
            EnvioProposta,
            Reuniao,
            Tarefa,
            Cobranca,
            ActoMedico,
            OrdemServiço
        }
    }
} 

