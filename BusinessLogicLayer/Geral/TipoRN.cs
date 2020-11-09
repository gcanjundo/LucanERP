using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class TipoRN
    {
        private static TipoRN _instancia;

        private TipoDAO dao;

        public TipoRN()
        {
          dao = new TipoDAO();
        }

        public static TipoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TipoRN();
            }

            return _instancia;
        }

        public TipoDTO Salvar(TipoDTO dto)
        {
            if (dto.Codigo > 0)
                return dao.Alterar(dto);
            else
                return dao.Adicionar(dto);
        }

        

        public void Apagar(TipoDTO dto)
        {
            dao.Eliminar(dto);
        }

        public TipoDTO ObterPorPK(TipoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<TipoDTO> ObterPorFiltro(TipoDTO dto)
        {
              
            return dao.ObterPorFiltro(dto);
        }

        public List<TipoDTO> ProductTypesList()
        {
            List<TipoDTO> lista = new List<TipoDTO>();
            lista.Add(new TipoDTO(1, "PRODUTOS", "P"));
            lista.Add(new TipoDTO(2, "SERVIÇOS", "S"));
            lista.Add(new TipoDTO(3, "PORTES, ADIANTAMENTOS OU OUTROS", "O"));
            lista.Add(new TipoDTO(4, "IMPOSTOS ESPECIAIS DE CONSUMO", "E"));
            lista.Add(new TipoDTO(5, "IMPOSTOS, TAXAS E ENCARGOS PARAFISCAIS", "T")); 

            return lista;
        }

        public List<TipoDTO> EntityTypesList()
        {
            List<TipoDTO> lista = new List<TipoDTO>();
            lista.Add(new TipoDTO(1, "CLIENTE", "T"));
            lista.Add(new TipoDTO(2, "FORNECEDOR", "F"));
            lista.Add(new TipoDTO(3, "CLIENTE E FORNECEDOR", "G"));
            lista.Add(new TipoDTO(4, "OUTROS CREDORES", "C"));
            lista.Add(new TipoDTO(5, "OUTROS DEVEDORES", "D")); 
            return lista;
        }

        public List<TipoDTO> TaxTypesList()
        {
            List<TipoDTO> lista = new List<TipoDTO>();
            lista.Add(new TipoDTO(1, "IMPOSTO SOBRE O VALOR ACRESCENTADO", "IVA"));
            lista.Add(new TipoDTO(2, "IMPOSTO DE SELO", "15"));
            lista.Add(new TipoDTO(3, "NÃO SUJEITO A IVA OU IS", "NS")); 
            return lista;
        }

        public List<TipoDTO> SaftSourceBillingList()
        {
            List<TipoDTO> lista = new List<TipoDTO>();
            lista.Add(new TipoDTO(1, "DOCUMENTO PRODUZIDO NA APLICAÇÃO", "P"));
            lista.Add(new TipoDTO(2, "DOCUMENTO INTEGRADO E PRODUZIDO NOUTRA APLICAÇÃO", "I"));
            lista.Add(new TipoDTO(3, "RECUPERAÇÃO OU DE EMISSÃO MANUAL", "M"));
            return lista;
        }
    }
}
