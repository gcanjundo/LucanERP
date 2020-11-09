using DataAccessLayer.Comercial;
using Dominio.Comercial;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Comercial
{
    public class GuiaEntregaRN
    {
        private static GuiaEntregaRN _instancia;

        private GuiaEntregaDAO dao;

        public GuiaEntregaRN()
        {
            dao = new GuiaEntregaDAO();
        }

        public static GuiaEntregaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new GuiaEntregaRN();
            }

            return _instancia;
        }

        public GuiaEntregaDTO Adicionar(GuiaEntregaDTO dto)
        {
            GuiaEntregaDTO savedGuide = dto;
            if (dto.Serie <= 0)
            {
                savedGuide.MensagemErro = "alert('Seleccione a Série');";
            }else if(dto.FuncionarioID == "-1")
            {
                savedGuide.MensagemErro = "alert('Seleccione o Responsável pela Entrega');";
            }else if(dto.ReceptorCarga == "")
            {
                savedGuide.MensagemErro = "alert('Informe o nome de quem da parte do cliente recebeu a mercadoria');";
            }else if(dto.ListaArtigos.Count == 0)
            {
                savedGuide.MensagemErro = "alert('Digite a quantidade entregue de mercadorias');";
            }
            else
            {
                var itemsList = dto.ListaArtigos; 
                savedGuide = dao.Adicionar(dto);

                if (savedGuide.Codigo > 0)
                {
                    foreach (var item in itemsList)
                    {
                        item.Codigo = savedGuide.Codigo;
                        dao.SalvarItem(item);
                    }
                    savedGuide.MensagemErro = "alert('Guia de Entrega Criada com Sucesso.'); window.close(); window.open('../Stock/Relatorios/GuiaEntrega?pID=" + savedGuide.Codigo + "')";
                }
                else
                {
                    savedGuide.MensagemErro = "alert('Erro ao Gravar a Guia: "+savedGuide.MensagemErro+"')";
                }

                
            }
            

            return savedGuide;
        }

        public List<GuiaItemDTO> GetDeliveryData(GuiaEntregaDTO dto)
        {
            return dao.ObterGuiaItemsList(dto);
        }

        public List<GuiaItemDTO> CustomerItemsDeliveredExtractList(GuiaItemDTO dto)
        {
            return dao.ObterItemDeliveryExtractList(dto);
             
        }

        public List<GuiaEntregaDTO>ObterPorFiltro(GuiaEntregaDTO dto)
        {
            return dao.ObterGuiasPorFiltro(dto);
        }

        public void Excluir(GuiaEntregaDTO dto)
        {
            dao.Anular(dto);
        }

        public List<GuiaItemDTO> ObterItemPorFiltro(GuiaEntregaDTO dto)
        {
            return dao.ObterGuiaItemsList(dto);
        }

        public GuiaEntregaDTO LaundryDelivery(GuiaEntregaDTO dto)
        {
            GuiaEntregaDTO savedGuide = dto;

            if (dto.ListaArtigos.Count == 0)
            {
                savedGuide.MensagemErro = "alert('Digite a quantidade entregue de mercadorias');";
            }
            else
            {
                var itemsList = dto.ListaArtigos;
                savedGuide = dao.Adicionar(dto);

                if (savedGuide.Codigo > 0)
                {
                    foreach (var item in itemsList)
                    {
                        item.Fatura = savedGuide.DocumentoOrigem; // TALAO DE RECOLHA ID
                        item.DocOrigemID = savedGuide.Codigo; // GUIA ID 
                        dao.SalvarItems(item);
                    }
                    savedGuide.MensagemErro = "window.open('../Lavandaria/Reports/TalaoEntrega?pDocID=" + savedGuide.Codigo + "'); location.reload();";
                    savedGuide.Sucesso = true;
                }
                else
                {
                    savedGuide.MensagemErro = "alert('Erro ao Gravar a Guia: " + savedGuide.MensagemErro + "')";
                }


            }


            return savedGuide;
        }
    }
}
