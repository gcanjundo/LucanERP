using BusinessLogicLayer.Geral;
using DataAccessLayer.Comercial;
using DataAccessLayer.Comercial.Stock;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{

    public class ProductPriceListRN
    {
        private static ProductPriceListRN _instancia;

        private ProductPriceListDAO dao;

        public ProductPriceListRN()
        {
          dao = new ProductPriceListDAO();
        }

        public static ProductPriceListRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ProductPriceListRN();
            }

            return _instancia;
        }

        public void Gravar(List<ProductPriceListDTO>  pLista, ArtigoDTO pProduct)
        {
            foreach (var dto in pLista)
            {
                dto.Codigo = pProduct.Codigo;
                dao.Adicionar(dto);
            }
            
        }

        public List<ProductPriceListDTO> ObterPorFitro(ProductPriceListDTO dto)
        {
            var _productPricesList = dto.Codigo >=0 ? dao.ObterPorFiltro(dto) : new List<ProductPriceListDTO>();
            foreach (var price in TabelaPrecoRN.GetInstance().ObterPorFiltro(new TabelaPrecoDTO(-1, "")))
            {
                if(!_productPricesList.Exists(t=>t.PriceTableID == price.Codigo))
                {
                    var product = new ProductPriceListDTO
                    {
                        Codigo = dto.Codigo,
                        PriceTableID = price.Codigo,
                        PrecoVenda = 0,
                        ImpostoID = -1,
                        PercentualImposto = 0,
                        ImpostoIncluido = (short)0,
                        ImpostoLiquido = 0,
                        UnidadeVenda = "-1",
                        QtdUndVenda = 1,
                        Utilizador = dto.Utilizador,
                        TablePriceDesignation = price.Descricao.ToUpper(),
                        CurtaDescricao = price.Sigla,
                        
                    };
                    _productPricesList.Add(product);
                }
            }
            
            return _productPricesList;
        }

        public ProductPriceListDTO ObterPorPK(ProductPriceListDTO dto)
        {
            var productList = dao.ObterPorFiltro(dto);
            if(productList.Count > 0)
            {
               return productList[0];
            }
            
            return new ProductPriceListDTO();
        }

        public ProductPriceListDTO Excluir(ProductPriceListDTO dto)
        {
            return dao.Excluir(dto);
        }



    }
}
