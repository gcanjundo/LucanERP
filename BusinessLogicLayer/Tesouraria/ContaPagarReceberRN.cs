using DataAccessLayer.Tesouraria;
using Dominio.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tesouraria
{
    public class ContaPagarReceberRN
    {
        private static ContaPagarReceberRN _instancia;

        private ContaPagarReceberDAO dao;

        public ContaPagarReceberRN()
        {
            dao = new ContaPagarReceberDAO();
        }

        public static ContaPagarReceberRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ContaPagarReceberRN();
            }

            return _instancia;
        }


        public ContaPagarReceberDTO Gravar(ContaPagarReceberDTO dto)
        {
            int parcelas = dto.Parcela;
            
            DateTime dataFinal = dto.Vencimento; 

            for(int i=1; i<=parcelas; i++)
            {
                if (parcelas > 1)
                {     
                    dto.Emissao = dto.Emissao.AddDays(dto.Periodicidade == -1 ? parcelas : dto.Periodicidade);
                    if(dto.Emissao > dataFinal)
                    {
                        dto.Emissao = dataFinal;
                    }
                    dto.Vencimento = dto.Emissao;
                    dto.Parcela = i;
                }
                dto = dao.Inserir(dto);
                if (dto.MensagemErro != "")
                {
                    break;
                }
            }

            return dto;
        }
        
        public ContaPagarReceberDTO Excluir(ContaPagarReceberDTO dto)
        {
            dto = dao.Excluir(dto);
            if(dto.Codigo > 0)
            {
                dto.MensagemErro =  "A Conta "+dto.Codigo+", já tem pagamentos associados, para ser removida, tem de remover primeiro os pagamentos";
            }
            else
            {
                dto.MensagemErro = "Conta Removida com Sucesso";
            }

            return dto;
        }

        public List<ContaPagarReceberDTO> ObterPorFiltro(ContaPagarReceberDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public ContaPagarReceberDTO ObterPorPK(ContaPagarReceberDTO dto)
        {
            return ObterPorFiltro(dto).SingleOrDefault();
        }

        public Tuple<int, int> ImportarPendentesComerciais(ContaPagarReceberDTO dto)
        {
           return dao.ImportarPendentesComercial(dto);
        }
    }
}
