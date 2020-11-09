using DataAccessLayer.Tesouraria;
using Dominio.Comercial;
using Dominio.Tesouraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tesouraria
{
    public class RubricaRN
    {
        private static RubricaRN _instancia;
        private RubricaDAO dao;
        private GenericRN _genericClass;
        public RubricaRN()
        {
            dao = new RubricaDAO();
            _genericClass = new GenericRN();
        }

        public static RubricaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new RubricaRN();
            }

            return _instancia;
        }

        public RubricaDTO Adicionar(RubricaDTO dto,List<MovimentoPlanoContaDTO> pPlanAccountList, List<DocumentoComercialDTO> pDocumentsList)
        { 
            return dao.Gravar(dto, pPlanAccountList, pDocumentsList);
        }
        public List<RubricaDTO> GetAllList()
        {
            return dao.ObterPorFiltro();
        }
        public List<RubricaDTO> ObterPorFiltro(RubricaDTO dto)
        {
            var lista = dao.ObterPorFiltro();

            if (dto.Codigo > 0)
            {
                lista = lista.Where(t => t.Codigo == dto.Codigo).ToList();
            }
            else
            {
                dto.Designacao = dto.Designacao == null ? string.Empty : dto.Designacao;
                dto.Classificacao = dto.Classificacao == null ? string.Empty : dto.Classificacao;

                lista = lista.Where(t => t.Designacao.Contains(dto.Designacao) || t.Classificacao == dto.Classificacao).ToList();

                if (!string.IsNullOrEmpty(dto.Natureza) && dto.Natureza != "-1")
                {
                     
                    lista = lista.Where(t => t.Movimento == (dto.Natureza == "E" || dto.Natureza =="R" ? "R" : "D")).ToList();
                }
                    

                if(dto.RubricaID > 0)
                    lista = lista.Where(t => t.RubricaID == dto.RubricaID).ToList();
            }
                


            return lista;
        }

        public List<RubricaDTO> ObterTodas(RubricaDTO dto)
        {
            var lista = ObterPorFiltro(dto); 
            var childrenList = lista.Where(t => t.RubricaID > 0).ToList();
            var OrderedList = new List<RubricaDTO>();
            lista = lista.OrderBy(t => t.Codigo).ToList();

            foreach (var item in lista.Where(t => t.RubricaID<=0).ToList())
            {
                item.Classificacao = "<b>" + item.Classificacao + "</b>";
                item.Designacao = "<b>" + item.Designacao + "</b>";
                item.Movimento = "<b>" + item.Movimento + "</b>";
                OrderedList.Add(item);
                foreach(var child in childrenList.Where(t=>t.RubricaID == item.Codigo).ToList())
                {
                    child.Classificacao = "<i>" + child.Classificacao + "</i>";
                    child.Designacao = "<i>" + child.Designacao + "</i>";
                    child.Movimento = "<i>" + child.Movimento + "</i>";
                    OrderedList.Add(child);
                }
            }
             
            return OrderedList;
        }

        public RubricaDTO ObterPorPK(RubricaDTO dto)
        {
            return ObterPorFiltro(dto).SingleOrDefault();
        }

        public List<RubricaDTO> GetForDropDownList(string pNatureza)
        {
            RubricaDTO dto = new RubricaDTO();
            dto.Natureza = pNatureza;
            var lista = dto.Natureza !="-1" ? ObterPorFiltro(dto).ToList() : new List<RubricaDTO>();
            dto.Codigo = -1;
            dto.Designacao = "-SELECCIONE-";
            dto.LookupField1 = "-SELECCIONE-";
            lista.Insert(0, dto);  
            return lista; 
        }

        public List<RubricaDTO> GetSubGroupForDropDownList(int pGroupID, string pNatureza)
        {
            RubricaDTO dto = new RubricaDTO();
            dto.Natureza = pNatureza;
            var lista = dto.Natureza != "-1" ? ObterPorFiltro(dto).ToList() : new List<RubricaDTO>();
            dto.Codigo = -1;
            dto.Designacao = "-SELECCIONE-";
            dto.LookupField1 = "-SELECCIONE-";

            var Subgroup = lista.Where(t => t.RubricaID == pGroupID).ToList();
            var OrderList = new List<RubricaDTO>();
            foreach (var item in Subgroup)
            {
                foreach(var subItem in lista.Where(t=>t.RubricaID == item.Codigo).ToList())
                {
                    if(!OrderList.Exists(t=>t.Codigo == item.Codigo))
                    {
                        OrderList.Add(item);
                    } 
                    OrderList.Add(subItem);
                }
            }
            OrderList.Insert(0, dto);

            return OrderList;
        }
    }
}
