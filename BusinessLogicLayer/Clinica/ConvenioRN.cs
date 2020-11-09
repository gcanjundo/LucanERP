using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class ConvenioRN
    {
        private static ConvenioRN _instancia;

        private ConvenioDAO dao;
        private ConvenioCoberturaDAO daoItem;

        public ConvenioRN()
        {
            dao = new ConvenioDAO();
            daoItem = new ConvenioCoberturaDAO();
        }

        public static ConvenioRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ConvenioRN();
            }

            return _instancia;
        }

        public ConvenioDTO Salvar(ConvenioDTO dto) 
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

        public ConvenioDTO Excluir(ConvenioDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<ConvenioDTO> ObterPorFiltro(ConvenioDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<ConvenioDTO> ListaConvenios(string pDescription, int pCompanyInsuranceID)
        {
            if (pDescription==null) 
            {
                pDescription = "";
            }
            return dao.ObterPorFiltro(new ConvenioDTO(pDescription, pCompanyInsuranceID));
        }

       

        public ConvenioDTO ObterPorPK(ConvenioDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }



        public List<ConvenioDTO> GetForDropDownList(string pCompanyInsuranceID)
        {
            var list = ListaConvenios(string.Empty, int.Parse(pCompanyInsuranceID==""?"-1": pCompanyInsuranceID)); 
            list.Insert(0, new ConvenioDTO { Codigo = -1, Descricao = "PARTICULAR" });

            return list;
        }

        public ConvenioDTO AddCoberturaItem(List<ConvenioCoberturaItemDTO> itemList)
        {
            var dto = Salvar(itemList[0].Convenio);
            if (dto.Sucesso)
            {
                foreach (var item in itemList)
                {
                    item.ConvenioID = dto.Codigo;
                    dto.MensagemErro += daoItem.Adicionar(item).MensagemErro;
                    if (dto.MensagemErro != "")
                    {
                        break;
                    }
                }
            }

            return dto;
        }

        public List<ConvenioCoberturaItemDTO> ObterListaItemConvenio(ConvenioCoberturaItemDTO dto)
        {
            return daoItem.ObterPorFiltro(dto);
        }
    }
}
