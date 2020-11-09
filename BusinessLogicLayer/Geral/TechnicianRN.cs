using BusinessLogicLayer.Geral;
using DataAccessLayer.Geral;
using Dominio.Geral;
using Dominio.Comercial.Restauracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{
    public class TechnicianRN
    {
        private static TechnicianRN _instancia;

        private TechnicianDAO dao;

        public TechnicianRN()
        {
          dao = new TechnicianDAO();
        }

        public static TechnicianRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TechnicianRN();
            }

            return _instancia;
        }


        public TechnicianDTO Salvar(TechnicianDTO pTecnico)
        {
           pTecnico.Entity = EntidadeRN.GetInstance().Salvar(pTecnico.Entity);

            if (pTecnico.Entity.MensagemErro.Equals(string.Empty) || pTecnico.Entity.Sucesso == true)
            {    
                 dao.Adicionar(pTecnico);
            }

            return pTecnico;
        }

        public TechnicianDTO ObterFuncionario(TechnicianDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<TechnicianDTO> ObterPorFiltro(TechnicianDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<string> GetInfoOffices(string pNome,string pFilial)
        {
            List<string> lista=new List<string>();
            foreach (var saleman in ObterPorFiltro(new TechnicianDTO(pNome, pFilial)))
            {
                lista.Add(saleman.Entity.Codigo + "¥" + saleman.Entity.NomeCompleto + "¥" + saleman.ValorComissao+ "¥" + saleman.FuncionarioID);
            }
            return lista;
        }


        public List<TechnicianDTO> GetForDropDownList()
        {
            var lista = ObterPorFiltro(new TechnicianDTO
            {
                Entity = new EntidadeDTO { NomeCompleto =""},
                Filial = "-1"
            });

            lista.Insert(0, new TechnicianDTO { FuncionarioID = "-1", DesignacaoEntidade = "-SELECCIONE-" });
            return lista;
        }



    }
}
