using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Comercial;
using Dominio.Comercial;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Comercial
{
    public class MetodoPagamentoRN
    {
        private static MetodoPagamentoRN _instancia;

        private MetodoPagamentoDAO dao;

        public MetodoPagamentoRN()
        {
          dao = new MetodoPagamentoDAO();
        }

        public static MetodoPagamentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MetodoPagamentoRN();
            }

            return _instancia;
        }

        public MetodoPagamentoDTO Salvar(MetodoPagamentoDTO dto) 
        {
            if (dto.Codigo > 0)
            {
                return dao.Alterar(dto);
            }
            else 
            {
                return dao.Adicionar(dto);
            }
        }

        public MetodoPagamentoDTO Excluir(MetodoPagamentoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<MetodoPagamentoDTO> ObterPorFiltro(MetodoPagamentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<MetodoPagamentoDTO> ObterResumoEntradas(FaturaDTO dto, bool IsAllowed)
        {
            var PaymentMethodList = ObterPorFiltro(new MetodoPagamentoDTO(0, ""));

            PaymentMethodList = dto.PrazoPagto > 0 ? PaymentMethodList.Where(t => t.Codigo == dto.PrazoPagto).ToList() : PaymentMethodList;
            List<MetodoPagamentoDTO> lista = new List<MetodoPagamentoDTO>();

            foreach (var payment in PaymentMethodList)
            {
                dto.PrazoPagto = payment.Codigo;
                payment.TotalLinha = IsAllowed ? new FaturaDAO().TotalEntradas(dto) : 0;
                lista.Add(payment);
            }

            return lista;
        }

        public List<MetodoPagamentoDTO> ObterResumoSaida(FaturaDTO dto, bool IsAllowed)
        {
            var PaymentMethodList = ObterPorFiltro(new MetodoPagamentoDTO(0, ""));

            PaymentMethodList = dto.PrazoPagto > 0 ? PaymentMethodList.Where(t => t.Codigo == dto.PrazoPagto).ToList() : PaymentMethodList;
            List<MetodoPagamentoDTO> lista = new List<MetodoPagamentoDTO>();

            foreach (var payment in PaymentMethodList)
            {
                dto.PrazoPagto = payment.Codigo;
                payment.TotalLinha = IsAllowed ? new FaturaDAO().TotalSaidas(dto) : 0;
                lista.Add(payment);
            }

            return lista;
        }

        public List<MetodoPagamentoDTO> ListaMetodoPagamento(string pDescricao)
        {
            if (pDescricao == null)
            {
                pDescricao = "";
            }
            return dao.ObterPorFiltro(new MetodoPagamentoDTO(0, pDescricao));
        }

        public MetodoPagamentoDTO ObterPorPK(MetodoPagamentoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

        public List<MetodoPagamentoDTO> ddlMetodosPagamento()
        {
            List<MetodoPagamentoDTO> lista = ObterPorFiltro(new MetodoPagamentoDTO(-1, ""));
            lista.Insert(0, new MetodoPagamentoDTO(-1, "-SELECCIONE-")); 
            return lista; 
        }

        public List<string> GetPaymentMethods()
        {
            List<string> lista = new List<string>();
            foreach(var metodo in ListaMetodoPagamento(""))
            {
                lista.Add(metodo.Codigo.ToString()+"_"+metodo.Descricao.ToUpper()+"_"+metodo.Sigla.ToUpper()+"_0_" + metodo.Icon);
            }
            return lista;
        }

        public List<string> GetPaymentMethodsForPOS()
        {
            List<string> lista = new List<string>();
            foreach (var metodo in ListaMetodoPagamento("").Where(t=>t.POSVisible == 1).ToList())
            {
                lista.Add(metodo.Codigo.ToString() + "_" + metodo.Descricao.ToUpper() + "_" + metodo.Sigla.ToUpper() + "_0_"+metodo.Icon);
            }
            return lista;
        }
    }
}
