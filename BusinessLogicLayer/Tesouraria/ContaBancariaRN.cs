using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Tesouraria;
using Dominio.Seguranca;
using DataAccessLayer.Tesouraria;

namespace BusinessLogicLayer.Tesouraria
{
    public class ContaBancariaRN
    {
        private static ContaBancariaRN _instancia;

        private ContaBancariaDAO daoContaBancaria;

        public ContaBancariaRN()
        {
          daoContaBancaria= new ContaBancariaDAO();
        }

        public static ContaBancariaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ContaBancariaRN();
            }

            return _instancia;
        }

        public ContaBancariaDTO AdicionarContaBancaria(ContaBancariaDTO dto)
        {
            return daoContaBancaria.Inserir(dto);
        }
        public void AtualizarSaldo(ContaBancariaDTO dto) 
        {
            daoContaBancaria.Alterar(dto);
        } 

        public void ApagarContaBancaria(ContaBancariaDTO dto)
        {
            daoContaBancaria.Excluir(dto);
        }

        public ContaBancariaDTO ObterContaBancaria(ContaBancariaDTO dto)
        {
            return daoContaBancaria.ObterPorPK(dto);
        }

        public List<ContaBancariaDTO> ListaContaBancarias(ContaBancariaDTO dto)
        {
            return daoContaBancaria.ObterPorFiltro(dto);
        }

        public List<ContaBancariaDTO> ListaForPayments(string pType, string pFilial, int pMoeda)
        {
            
            ContaBancariaDTO dto = new ContaBancariaDTO("", -1, pFilial, pMoeda, "");

            List<ContaBancariaDTO> lista = ListaContaBancarias(dto).OrderBy(t => t.Banco).ToList();

            if (lista.Count > 1)
            {
                lista.Insert(0, new ContaBancariaDTO("-1", -1, "-1", pMoeda, "Seleccione"));
            }

            return lista;
        } 

    }
}
