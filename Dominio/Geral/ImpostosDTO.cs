﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class ImpostosDTO : TabelaGeral
    {
        public string Valorizacao { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string SaftTaxLine { get; set; }
        public string ZonaFiscal { get; set; }
        public ImpostosDTO()
        {

        }

        public ImpostosDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public ImpostosDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public ImpostosDTO(int pCodigo, string pDescricao, string pSigla)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
        }

        public ImpostosDTO(int pCodigo, string pDescricao, string pSigla, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
        }

        public ImpostosDTO(int pCodigo, string pDescricao, string pSigla, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Sigla = pSigla;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        }


        public decimal Valor { get; set; }
        public string Notes { get; set; }
        public string InternalCode { get; set; }
    }

}
