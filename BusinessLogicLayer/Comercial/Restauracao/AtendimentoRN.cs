using BusinessLogicLayer.Comercial;
using DataAccessLayer.Comercial;
using DataAccessLayer.Comercial.Restauracao;
using Dominio.Comercial;
using Dominio.Comercial.Stock;
using Dominio.Geral;
using Dominio.Comercial.Restauracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Comercial.Stock;

namespace BusinessLogicLayer.Comercial.Restauracao
{
    public class AtendimentoRN
    {
         private static AtendimentoRN _instancia;

        private readonly AtendimentoDAO dao; 

        public AtendimentoRN()
        {
            dao = new AtendimentoDAO(); 
        }

        public static AtendimentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AtendimentoRN();
            }

            return _instancia;
        }

        public AtendimentoDTO Salvar(AtendimentoDTO dto)
        {  
            return dao.Adicionar(dto);
        }

        public AtendimentoDTO EncerrarAtendimento(AtendimentoDTO dto)
        {
            dto = dao.Fechar(dto);
            if (dto.Sucesso)
            { 
                MesaRN.GetInstance().Desocupar(new MesaDTO(int.Parse(dto.Mesa)));
            }
            return dto;
        }

        public List<AtendimentoDTO> ObterPorFiltro(AtendimentoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public AtendimentoDTO ObterPorPK(AtendimentoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        

        public List<AtendimentoItemDTO> ObterItensAtendimento(AtendimentoDTO dto)
        {
            return dao.ObterItens(dto);
        }

        public void RemoveTodosItens(AtendimentoDTO dto)
        {
            ReporStock(dto);
            dao.ExcluirItens(dto);
        }

        public void GravarItens(List<AtendimentoItemDTO> pItemList, int pStockWareHouseID)
        {
            List<ItemMovimentoStockDTO> StockProductsList = new List<ItemMovimentoStockDTO>();
             
             
            foreach (var item in pItemList)
            {
                var orderItem = dao.AdicionarItem(item); 
                if (orderItem.Sucesso && item.MoveStock && (!item.Saved || (item.Saved && item.Deleted)))
                {
                    var ExitenciaInicial = new StockDAO().StockActual(item.Artigo, pStockWareHouseID);
                    ItemMovimentoStockDTO product = new ItemMovimentoStockDTO
                    {
                        ArtigoID = item.Artigo,
                        Designacao = item.Designacao,
                        Existencia = ExitenciaInicial,
                        PrecoUnitario = item.Preco,
                        TotalLiquido = item.Preco * item.Quantidade,
                        Quantidade = !item.Deleted ? -item.Quantidade : item.Quantidade,
                        ValorTotal = (ExitenciaInicial - item.Quantidade) * item.Preco,
                        Operacao = 2,
                        ArmazemOrigem = pStockWareHouseID,
                        AramzemDestino = pStockWareHouseID
                    };
                    StockProductsList.Add(product);
                } 
            }
            AbateStock(StockProductsList); 
            
        }

        private void AbateStock(List<ItemMovimentoStockDTO> StockProductsList)
        {
             
            
        }

        public List<AtendimentoItemDTO> ObterConsultaMesa(AtendimentoDTO dto)
        {
            return dao.ObterConsulta(dto);
        }

        public void Cancelar(AtendimentoDTO dto)
        {
            dao.Excluir(dto);
            ReporStock(dto);
        }

        private void ReporStock(AtendimentoDTO dto)
        {
            foreach (var item in ObterItensAtendimento(dto))
            {
                ArtigoDTO artigo = ArtigoRN.GetInstance().ObterPorPK(new ArtigoDTO(item.Artigo));
                if (artigo.MovimentaStock)
                {
                    ItemFaturacaoDTO stock = new ItemFaturacaoDTO();
                    stock.Quantidade = item.Quantidade;
                    stock.Artigo = item.Artigo;
                    new ItemFaturacaoDAO().ReposicaoStockArtigo(stock);
                }
            }
        }

        public AtendimentoDTO GravarPedido(AtendimentoDTO dto)
        {
            return dao.GravarPedido(dto);
        }

        public void TrocarMesa(AtendimentoDTO dto)
        {
            dao.TrocarMesa(dto);
        }

        public void SaveCustomerInfo(AtendimentoDTO dto)
        {
            dao.CustomerUpdate(dto);
        }
    }
}
