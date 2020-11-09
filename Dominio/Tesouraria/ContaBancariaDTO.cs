using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Tesouraria
{
    public class ContaBancariaDTO:RetornoDTO
    {
   
        public int Titular
        {
            get;
            set;
        }
        
        public string NomeTitular
        {
            get;
            set;
        }

        public int Banco
        {
            get;
            set;
        }

        public string NomeBanco
        {
            get;
            set;
        }

        public string NumeroConta
        {
            get;
            set;
        }

        public int Moeda
        {
            get;
            set;
        }


        public string SiglaMoeda
        {
            get;
            set;
        }
        

        public string IBAN
        {
            get;
            set;
        }



        public string NIB
        {
            get;
            set;
        }
        
        public string Swift
        {
            get;
            set;
        }
        
        public string AccountType
        {
            get;
            set;
        }


        public decimal Saldo
        {
            get;
            set;
        }
         
         
        
        public string Tipo { get; set; }
        public  string Beneficiario { get; set; }

        public ContaBancariaDTO()
        {
            NumeroConta = string.Empty;
            Banco = -1;
            Moeda = -1;
            NomeBanco = string.Empty;
            Filial = "-1";
        }

         

        public ContaBancariaDTO(string pNumero)
        {
            NumeroConta = pNumero;
            Codigo = int.Parse(pNumero == string.Empty ? "-1" : pNumero);
            Banco = -1;
            Moeda = -1;
            NomeBanco = string.Empty;
            Filial = "-1";
        }

        public ContaBancariaDTO(string pConta, string pUnidade)
        {
             NumeroConta = pConta;
             Descricao = pUnidade;
             
        }

        public ContaBancariaDTO(string pConta, int pBanco, string pFilial, int pMoeda, string pNomeBanco)
        {
            Banco = pBanco;
            Filial = pFilial;
            Moeda = pMoeda;
            NomeBanco = pNomeBanco;
            NumeroConta = pConta;
            Descricao = pNomeBanco;
            AccountType = pNomeBanco;
        }


    }
}
