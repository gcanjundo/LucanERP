using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Tesouraria
{
    public class BancoDTO: EntidadeDTO
    {
        public int Estado { get; set; }
        public BancoDTO() 
        {
        
        }

        public BancoDTO(int codigo, string descricao, string sigla) 
        {
            Codigo = codigo;
            NomeCompleto = descricao;
            NomeComercial = sigla;
            
        }

        public BancoDTO(string pDescricao, string pFilial)
        {
            NomeCompleto = pDescricao;
            Filial = pFilial;
        }

        public BancoDTO(int pCodigo, string pDesignacao, string pSigla, string pFilial, int pStatus)
        {
            Codigo = pCodigo;
            NomeCompleto = pDesignacao;
            NomeComercial = pSigla;
            Filial = pFilial;
            Estado = pStatus;
            Tipo = "B";
            Categoria = "-1";
            Identificacao = string.Empty;
            Nacionalidade = -1;
            Rua = string.Empty;
            Bairro = string.Empty;
            MunicipioMorada = -1;
            Telefone = string.Empty;
            TelefoneAlt = string.Empty;
            TelefoneFax = string.Empty;
            Email = string.Empty;
            WebSite = string.Empty;
            Desconto = 0;
            LimiteCredito = 0;
            Morada = string.Empty;

        }
    }

}
