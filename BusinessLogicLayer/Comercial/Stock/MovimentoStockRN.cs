using BusinessLogicLayer.Geral;
using DataAccessLayer.Comercial;
using DataAccessLayer.Comercial.Stock;
using Dominio.Comercial; 
using Dominio.Comercial.Stock;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial.Stock
{
  public class MovimentoStockRN
    {
      private static MovimentoStockRN _instancia;
      private MovimentoStockDAO dao;
      private StockDAO daoStock;

      public MovimentoStockRN()
      {
          dao = new MovimentoStockDAO();
          daoStock = new StockDAO();
      }

      public static MovimentoStockRN GetInstance()
      {
          if (_instancia == null)
          {
              _instancia = new MovimentoStockRN();
          }
          return _instancia;
      }

      public MovimentoStockDTO Gravar(MovimentoStockDTO dto, bool isTransfer)
      {

            if (isTransfer)
            { 
                dto.ArmazemID = dto.ArmazemTo;
            }

          MovimentoStockDTO movimento = dao.Adicionar(dto);

          if (movimento.Codigo > 0)
          {
              foreach (var item in dto.ListaArtigo)
              {
                  item.Movimento = movimento.Codigo;
                  item.Utilizador = item.Utilizador == null || item.Utilizador == string.Empty ? dto.Utilizador : item.Utilizador;
                  item.Filial = item.Filial == null || item.Filial == string.Empty ? dto.Filial : item.Filial;
                  item.ArmazemOrigem = item.ArmazemOrigem <= 0 ? dto.ArmazemFrom : item.ArmazemOrigem;
                  item.AramzemDestino = item.AramzemDestino <=0  ? dto.ArmazemTo : item.AramzemDestino;

                     


                  item.Operacao = dto.Operacao;
                  item.Existencia = item.Existencia <=0 || isTransfer ? GetInicialStock(item.ArtigoID, isTransfer ? item.AramzemDestino : item.ArmazemOrigem) : item.Existencia;
                    item.Quantidade = item.ContagemFisica != 0 && item.Quantidade == 0 ? item.Acrto : item.Quantidade;
                  

                  if ((item.Operacao == 2 && item.Quantidade > 0) || (item.Operacao == 1 && item.Quantidade < 0))
                  {    
                      item.Quantidade *= -1;
                  }

                     
                    
                     
                  dao.AdicionarArtigo(item);
                  
              }
              return ObterPorPk(movimento);
          }
          else
          {
              return dto;
          }
      }

      public MovimentoStockDTO Excluir(MovimentoStockDTO dto)
      {
          return dao.Excluir(dto);
      }

      public SerieDTO ObterSerieStock(SerieDTO dto)
      {
          return dao.ObterSerieStock(dto);
      }

       

      public MovimentoStockDTO TransferirStock( MovimentoStockDTO dto)
      {
            if (dto.ListaArtigo.Count > 0)
            {
                dto = dao.Adicionar(dto);

                if (dto.Codigo > 0)
                {
                    int TransferID = dto.Codigo;
                    MovimentoStockDTO Saida = dto;
                    Saida.Operacao = 2;
                    Saida.TranferenciaID = TransferID;
                    Saida.Documento = 12;
                    Saida.Serie = ObterSerieStock(new SerieDTO(Saida.Documento)).Codigo;
                    Saida.Codigo = -1;

                    if (Gravar(Saida, false).Codigo > 0)
                    {
                        MovimentoStockDTO Entrada = dto;
                        Entrada.Operacao = 1;
                        Entrada.Codigo = -1;
                        Entrada.TranferenciaID = TransferID;
                        Entrada.ArmazemID = dto.ArmazemTo;
                        Entrada.Documento = 11;
                        Entrada.Serie = ObterSerieStock(new SerieDTO(Entrada.Documento)).Codigo;
                        Entrada = Gravar(Entrada, true);
                        dto = ObterPorPk(new MovimentoStockDTO(TransferID));
                    }
                    else
                    {
                        dto = Saida;
                    }
                }
            }
            else
            {
                dto.MensagemErro = "Não foram seleccionados Artigos para transferência";
            }

          return dto;
      }

        



        public MovimentoStockDTO ObterPorPk(MovimentoStockDTO dto)
      {
          return dao.ObterPorPK(dto);
      }

      public List<MovimentoStockDTO> ObterMovimentos(MovimentoStockDTO dto)
      {
          return dao.ObterPorFiltro(dto);
      }

      private decimal GetInicialStock(int pProductID, int pWarehouseID)
      {
          return daoStock.StockActualArmazem(pProductID, pWarehouseID, DateTime.MaxValue, DateTime.MaxValue).Actual;
      }

      public void AddStockInicial(ItemMovimentoStockDTO dto)
      {
          daoStock.AddStockInicial(dto);
      }


      public InventarioDTO SaveInventory(InventarioDTO dto)
      {
          return daoStock.SaveInventory(dto);
      }

      public List<ItemMovimentoStockDTO> GetInventoryProductListByReference(ItemMovimentoStockDTO dto)
      {
        return  daoStock.GetInventoryProductList(dto);
      }

        public List<InventarioDTO> GetInventoryHistoryList(InventarioDTO dto)
        {
            return daoStock.GetInventoryByReference(dto);
        }

        public List<string> InventoryListForAutoComplete(InventarioDTO dto)
        {
            List<string> lista = new List<string>();

            foreach (var inventory in GetInventoryHistoryList(dto))
            {
                lista.Add(inventory.InventoryID.ToString()+";"+inventory.InventoryDate.ToString() + ";" + inventory.Reference + ";" + inventory.Localizacao + ";" + inventory.FuncionarioID + ";" + inventory.Status);
            }

            return lista;
        }

      private void GenerateInicialStock(List<ItemFaturacaoDTO> lista)
      {

      }

        public void GenerateStockMovimentFromSalesDocument(List<ItemMovimentoStockDTO> pLista, FaturaDTO pDocument, string pMovimentCode)
        {
            if (pLista.Count > 0)
            {

                int doc = 12;
                SerieDTO _serie = ObterSerieStock(new SerieDTO(doc));
                MovimentoStockDTO dto = new MovimentoStockDTO
                {
                    Codigo = -1,
                    Documento = doc,
                    ArmazemFrom = pDocument.Armazem,
                    DataStock = DateTime.Today,
                    Lancamento = DateTime.Now,
                    Operacao = pMovimentCode == "E" ? 1 : 2,
                    Serie = _serie.Codigo,
                    Referencia = string.Empty,
                    Utilizador = pDocument.Utilizador,
                    Numeracao = 0,
                    ArmazemTo = pDocument.Armazem,
                    FuncionarioID = pDocument.VendedorID <= 0 ? "-1" : pDocument.VendedorID.ToString(),
                    ListaArtigo = pLista,
                    DocumentoFrom = pDocument.Documento,
                    DocumentID = pDocument.Codigo,
                    Filial = pDocument.Filial,
                    ArmazemID = pDocument.Armazem,
                    DocumentTypeFromID = pDocument.Documento
                };

                Gravar(dto, false);
            }

        }

        public List<ItemMovimentoStockDTO> GetStockProductExtractList(ItemMovimentoStockDTO dto)
        {
            return dao.ObterItemsList(dto);
        }

        public List<ItemMovimentoStockDTO> GetStockProductListResume(List<ItemMovimentoStockDTO> productList)
        {
            List<ItemMovimentoStockDTO> lista = new List<ItemMovimentoStockDTO>();

            foreach(var product in productList.ToList())
            {
                var productExtract = productList.Where(t => t.ArtigoID == product.ArtigoID).ToList();  

                if(productExtract.Count > 0)
                {
                    product.Quantidade = productExtract.Sum(t => t.Quantidade);
                    product.PrecoUnitario = product.PrecoUnitario > 0 ? (productExtract.Sum(t => t.PrecoUnitario) / productExtract.Count) : 0;
                    product.ValorTotal = productExtract.Sum(t => t.ValorTotal);
                    lista.Add(product);
                    productList.RemoveAll(t => t.ArtigoID == product.ArtigoID);
                }
            }
            return lista;
        }

        public void RecalculaStocks()
        {
            dao.RecalculaStocks();
        }

        public void GenerateStockMovimentFromPurchaseDocument(List<ItemMovimentoStockDTO> pLista, FaturaDTO pDocument, string pMovimentCode)
        {
            if (pLista.Count > 0)
            {

                int doc = 12;
                SerieDTO _serie = ObterSerieStock(new SerieDTO(doc));
                MovimentoStockDTO dto = new MovimentoStockDTO
                {
                    Codigo = -1,
                    Documento = doc,
                    ArmazemFrom = pDocument.Armazem,
                    DataStock = DateTime.Today,
                    Lancamento = DateTime.Now,
                    Operacao = pMovimentCode == "E" ? 1 : 2,
                    Serie = _serie.Codigo,
                    Referencia = string.Empty,
                    Utilizador = pDocument.Utilizador,
                    Numeracao = 0,
                    ArmazemTo = pDocument.Armazem,
                    FuncionarioID = pDocument.VendedorID <= 0 ? "-1" : pDocument.VendedorID.ToString(),
                    ListaArtigo = pLista,
                    DocumentoFrom = pDocument.Documento,
                    DocumentID = pDocument.Codigo,
                    Filial = pDocument.Filial,
                    ArmazemID = pDocument.Armazem,
                    DocumentTypeFromID = pDocument.Documento
                };

                Gravar(dto, false);
            }

        }

        public List<ItemMovimentoStockDTO> ProductExtractList(ItemMovimentoStockDTO dto)
        {
            return daoStock.ObterProductExtract(dto);
        }

        public void AcertoAutomaticoStock(ArtigoDTO produto)
        {
            ItemMovimentoStockDTO dto = new ItemMovimentoStockDTO();
            dto.Filial = "-1";
            dto.LookupDate1 = DateTime.MinValue;
            dto.LookupDate2 = DateTime.MinValue;
            dto.ArtigoID = produto.Codigo;
            dto.Armazem = produto.ArmazemID.ToString();

            foreach(var item in ProductExtractList(dto))
            {
                produto.Quantidade = item.TotalLiquido;
            }

            daoStock.ActualizaStock(produto);
        }

        public void CancelarContagem(InventarioDTO inventarioDTO)
        {
            daoStock.CancelInventory(inventarioDTO);
        }

        public Tuple<bool, string> CheckInCounting(ItemMovimentoStockDTO dto)
        {
            return daoStock.GetInCounting(dto);
        }
    }
}
