using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogicLayer.Comercial;
using DataAccessLayer.Geral;
using Dominio.Comercial;
using Dominio.Geral;
using Dominio.Tesouraria;

namespace BusinessLogicLayer.Geral
{
    public class EntidadeRN
    {
        private static EntidadeRN _instancia; 
        private EntidadeDAO daoEntidade;

        GenericRN _genericClass = new GenericRN();

        public EntidadeRN()
        {
            daoEntidade = new EntidadeDAO();
        }

        public static EntidadeRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new EntidadeRN();
            }

            return _instancia;
        }

        public EntidadeDTO Salvar(EntidadeDTO dto)
        {
            dto = daoEntidade.Adicionar(dto);

            if(dto.MensagemErro == string.Empty)
            {
                if (dto.IsTerceiro)
                {
                    daoEntidade.AddCustomerSupplierData(dto);
                    dto.MensagemErro = _genericClass.SuccessMessage();
                }
                else
                {
                    dto.MensagemErro = string.Empty;
                }

                dto.Sucesso = true;
                    
            }
            else
            {
                dto.MensagemErro = _genericClass.ErrorMessage(dto.MensagemErro);
            }

            return dto;

        }

        public EntidadeDTO Eliminar(EntidadeDTO dto)
        {
            return daoEntidade.Excluir(dto);
        }

        public List<EntidadeDTO> ObterPorFiltro(EntidadeDTO dto)
        {
            return daoEntidade.ObterPorFiltro(dto).Where(t => t.Filial == "-1" || t.Filial == dto.Filial).ToList();
        }

        public List<EntidadeDTO> GetCustomerList(EntidadeDTO dto)
        { 
            return daoEntidade.ObterCustomerPorFiltro(dto).Where(t => t.Filial == "-1" || t.Filial == dto.Filial).ToList();
        }

        public List<EntidadeDTO> GetSupplierList(EntidadeDTO dto)
        {
            return daoEntidade.ObterSupplierPorFiltro(dto).Where(t=>t.Filial == dto.Filial || t.Filial == "-1").ToList();
        }

        public EntidadeDTO ObterPorPK(EntidadeDTO dto)
        {
            return daoEntidade.ObterEntidadePorPK(dto);
        }

        public EntidadeDTO GetByPK(EntidadeDTO dto)
        {
            return daoEntidade.ObterCustomerSupplierPorPK(dto);
        }


        public List<string> GetEntityLessInfo(string pEntityName, string pFilial)
        {
            List<string> lista = new List<string>();
            
            foreach (var entidade in ObterPorFiltro(new EntidadeDTO(pEntityName, pFilial))) 
            {
                lista.Add(entidade.Codigo + "¥" + entidade.NomeCompleto + "¥" + entidade.Identificacao + "¥" + entidade.Morada + "¥" + entidade.Desconto + "¥" + entidade.Saldo+entidade.Email+"¥"+entidade.Telefone);
            }

            return lista;
        }

        public List<EntidadeDTO> GetCustomerDevedoresList(EntidadeDTO dto)
        {
            return GetCustomerList(dto).Where(t => decimal.Parse(t.Saldo) > 0).OrderByDescending(t => decimal.Parse(t.Saldo)).ToList();
        }

        public List<EntidadeDTO> GetCustomerForDropDowList(EntidadeDTO dto)
        {
            var lista = GetCustomerList(dto);

            lista.Insert(0, new EntidadeDTO(-1, "-SELECCIONE-"));

            return lista;
        } 
        
         

        public List<string> GetCustomerLessInfo(string pEntityName, string pFilial)
        {
            List<string> lista = new List<string>();

            foreach (var entidade in GetCustomerList(new EntidadeDTO(pEntityName, pFilial)))
            { 
                lista.Add(entidade.Codigo + "¥" + entidade.NomeCompleto + "¥" + entidade.Identificacao + "¥" + entidade.Morada + "¥" + entidade.Desconto + "¥" + SaldoCliente(entidade) + "¥" +
                          entidade.Email + "¥" + entidade.Telefone + "¥" + entidade.PaymentDays + "¥" + entidade.PaymentMethod + "¥" + entidade.PaymentTerms + "¥" +
                         entidade.ModoExpedicao + "¥" + entidade.TablePriceID + "¥" + entidade.DescontoLinha + "¥" + entidade.LimiteCredito + "¥" + entidade.Currency + "¥" + 
                         entidade.RetencaoID + "¥" + entidade.CustomerFiscalCodeID);
            }

            return lista;
        }

        public List<EntidadeDTO> ObterWithConvenio(EntidadeDTO dto)
        {
            return daoEntidade.ObterPorConvenio(dto);
        }

        public List<EntidadeDTO> GetCustomerAllForDropDowList(EntidadeDTO dto)
        {
            var lista = GetCustomerList(dto);

            lista.Insert(0, new EntidadeDTO(-1, "-SELECCIONE-"));

            return lista;
        }

        public List<string> GetSupplierLessInfo(string pEntityName, string pFilial)
        {
            List<string> lista = new List<string>();

            foreach (var entidade in GetSupplierList(new EntidadeDTO(pEntityName, pFilial)))
            {
                lista.Add(entidade.Codigo + "¥" + entidade.NomeCompleto + "¥" + entidade.Identificacao + "¥" + entidade.Morada + "¥" + entidade.Desconto + "¥" + entidade.Saldo + "¥" + entidade.Email + "¥" + entidade.Telefone +
                         "¥" + entidade.PaymentDays + "¥" + entidade.PaymentMethod + "¥" + entidade.PaymentTerms + "¥" + entidade.ModoExpedicao + "¥" + entidade.TablePriceID + "¥" + entidade.CustomerFiscalCodeID+ "¥" +
                         SaldoCliente(entidade) + "¥" +entidade.SupplierFiscalCodeID);
            }

            return lista;
        }  

        public List<EntidadeDTO> GetEntityForDropDown(string pFilial)
        {
            var entityList = ObterPorFiltro(new EntidadeDTO(string.Empty, pFilial));
            foreach(var entity in entityList)
            {
                entity.NomeCompleto = entity.Entidade + " - " + entity.NomeCompleto;
            }
            entityList.Insert(0, new EntidadeDTO(-1, "000 - NÃO IDENTIFICADO"));

            return entityList;
        }

        public List<EntidadeDTO> GetCompanyInsuranceList(string pFilial)
        {
            var list = daoEntidade.ObterTerceirosPorFiltro(new EntidadeDTO { NomeCompleto = "" }).Where(t => t.IsCompanyInsurance && (t.Filial == pFilial || t.Filial == "-1")).ToList();
            list.Insert(0, new EntidadeDTO { Codigo = -1, NomeCompleto = "-SELECCIONE-" });
            return list;
        }

        decimal SaldoCliente(EntidadeDTO dto)
        {
            var ExtractList = FaturaClienteRN.GetInstance().GetCustomerExtractList(new FaturaDTO
            {
                Entidade = dto.Codigo 
            });

            return ExtractList.Sum(t => t.Saldo);
        }
    }
}
