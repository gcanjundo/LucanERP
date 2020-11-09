
using DataAccessLayer.Comercial.Stock;

using Dominio.Comercial.Stock;
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial.Stock
{
    public class StockDAO: ConexaoDB
    {
        public StockInfoDTO Adicionar(StockInfoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_STOCK_ADICIONAR";

                AddParameter("ARTIGO", dto.ArtigoID);
                AddParameter("ARMAZEM", dto.ArmazemID);
                AddParameter("QUANTIDADE", dto.Actual);
                AddParameter("CONTAGEM", dto.ContagemFisica);
                AddParameter("MAXIMA", dto.Maxima);
                AddParameter("MINIMO", dto.Minima);
                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<StockInfoDTO> ObterPorArtigo(ArtigoDTO pArtigo)
        {
            List<StockInfoDTO> lista = new List<StockInfoDTO>();
            StockInfoDTO dto;
            try
            {
                ComandText = "stp_COM_STOCK_OBTERPORARTIGO";

                AddParameter("ARTIGO", pArtigo.Codigo);

                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new StockInfoDTO();
                    dto.ArtigoID = int.Parse(dr[0].ToString());
                    dto.ArmazemID = int.Parse(dr[1].ToString());
                    dto.WareHouseName = dr[2].ToString();
                    dto.Actual = Convert.ToDecimal(dr[3].ToString() ?? "0");
                    dto.ContagemFisica = Convert.ToDecimal(dr[4].ToString() ?? "0");
                    dto.Minima = Convert.ToDecimal(dr[5].ToString() ?? "0");
                    dto.Maxima = Convert.ToDecimal(dr[6].ToString() ?? "0");
                    dto.IsWarehousePOS = dr["ARM_POS"].ToString() == "1" ? true : false;
                    dto.UltimaContagem = dr["STOCK_DATA_ULTIMA_CONTAGEM"].ToString() != "" ? DateTime.Parse(dr["STOCK_DATA_ULTIMA_CONTAGEM"].ToString()) : DateTime.MinValue;
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new StockInfoDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public StockInfoDTO StockActualArmazem(int pCodigo, int pArmazem, DateTime pInicio, DateTime pTermino)
        {
            StockInfoDTO dto = new StockInfoDTO();

            try
            {
                // ESTA MÉTODO DEVERÁ SER ALTERADO PARA PEGAR A INFORMAÇÃO NA TABELA DE MOVIMENTOS DIRECTOS DE STOCK
                
                ComandText = "stp_COM_STOCK_ACTUAL_ARMAZEM";
                AddParameter("@ARTIGO", pCodigo);
                AddParameter("@ARMAZEM", pArmazem);
                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    dto.Actual = decimal.Parse(dr[0].ToString());
                    dto.ArmazemID = int.Parse(dr[1].ToString());
                    dto.WareHouseName = dr[2].ToString();
                } 

            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public decimal StockActual(int pCodigo, int pArmazem)
        {
            try
            {
                ComandText = "stp_COM_STOCK_ACTUAL";
                AddParameter("@ARTIGO", pCodigo);
                AddParameter("@ARMAZEM", pArmazem);
                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    return Convert.ToDecimal(dr[0].ToString());
                }
                else
                {
                    return 0;
                }

            }
            catch  
            {

                return 0;
            }
            finally
            {
                FecharConexao();
            }
        }


        public List<ArtigoDTO> ObterPorFiltro(StockInfoDTO dto)
        {
            List<ArtigoDTO> lista = new List<ArtigoDTO>();
            ArtigoDTO objArtigo;
            try
            {
                ComandText = "stp_COM_STOCK_OBTERPORFILTRO";

                AddParameter("ARMAZEM", dto.ArmazemID);
                AddParameter("ARTIGO", dto.DesignacaoArtigo);
                AddParameter("@REFERENCIA", dto.Reference == null ? string.Empty : dto.Reference);
                AddParameter("@CODIGO_BARRAS", dto.BarCode == null ? string.Empty : dto.BarCode);
                AddParameter("FILIAL", dto.Filial);
                AddParameter("LOTE_ID", dto.LookupNumericField1);
                AddParameter("DIMENSAO_ID", dto.LookupNumericField2);
                AddParameter("SERIE_ID", dto.LookupNumericField3);
                AddParameter("SEMELHANTE_ID", dto.LookupNumericField4); 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    objArtigo = new ArtigoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Referencia = dr[1].ToString(),
                        Designacao = dr[2].ToString(),
                        Categoria = dr[3].ToString(),
                        Quantidade = Convert.ToDecimal(dr[4].ToString()),
                        PrecoCusto = Convert.ToDecimal(dr[5].ToString()),
                        PrecoVenda = Convert.ToDecimal(dr[6].ToString()),
                        WareHouseName = dr[7].ToString(),
                        FotoArtigo = dr[8].ToString(),
                        CodigoBarras = dr[9].ToString()
                    };
                    lista.Add(objArtigo);
                }
            }
            catch (Exception ex)
            {
                objArtigo = new ArtigoDTO();
                objArtigo.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<ItemMovimentoStockDTO> GetToInventory(StockInfoDTO dto)
        {
            List<ItemMovimentoStockDTO> lista = new List<ItemMovimentoStockDTO>();
            ItemMovimentoStockDTO _artigo;
            try
            {
                ComandText = "stp_COM_STOCK_OBTERPORFILTRO";

                AddParameter("WAREHOUSE_ID", dto.ArmazemID);
                AddParameter("ARTIGO", dto.DesignacaoArtigo);
                AddParameter("@REFERENCIA", dto.Reference == null ? string.Empty : dto.Reference);
                AddParameter("@CODIGO_BARRAS", dto.BarCode == null ? string.Empty : dto.BarCode); 
                AddParameter("FILIAL", dto.Filial);
                AddParameter("LOTE_ID", dto.Product.LoteID <=0 ?(object)DBNull.Value : dto.Product.LoteID);
                AddParameter("DIMENSAO_ID", dto.Product.DimesaoID <= 0 ? (object)DBNull.Value : dto.Product.DimesaoID);
                AddParameter("SERIAL_ID", dto.Product.SerialNumberID <= 0 ? (object)DBNull.Value : dto.Product.SerialNumberID);
                AddParameter("SEMELHANTE_ID", dto.Product.SemelhanteID <= 0 ? (object)DBNull.Value : dto.Product.SemelhanteID);
                AddParameter("FAMILIA_ID", dto.Product.Categoria == "-1" ? (object)DBNull.Value : dto.Product.Categoria);
                AddParameter("UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    _artigo = new ItemMovimentoStockDTO();
                    _artigo.ArtigoID = int.Parse(dr[0].ToString());
                    _artigo.Referencia = dr[1].ToString();
                    _artigo.Designacao = dr[2].ToString();
                    _artigo.Classificacao = dr[3].ToString();
                    _artigo.Quantidade = Convert.ToDecimal(dr[4].ToString());
                    _artigo.PrecoCusto = Convert.ToDecimal(dr[5].ToString());
                    _artigo.PrecoVenda = Convert.ToDecimal(dr[6].ToString());
                    _artigo.Armazem = dr[7].ToString();
                    _artigo.FotoArtigo = dr[8].ToString();
                    _artigo.BarCode = dr[9].ToString();
                    _artigo.Existencia = _artigo.Quantidade;
                    _artigo.ContagemFisica = -1;
                    _artigo.Unidade = dr[14].ToString();
                    _artigo.LoteID = int.Parse(dr[10].ToString());
                    _artigo.DimensaoID = int.Parse(dr[12].ToString());
                    _artigo.SerialNumberID = int.Parse(dr[15].ToString());
                    _artigo.Designacao = dr[11].ToString() != "" ? (_artigo.Designacao + "-" + dr[11].ToString()) : _artigo.Designacao;
                    _artigo.Designacao = dr[13].ToString() != "" ? (_artigo.Designacao + "-" + dr[13].ToString()) : _artigo.Designacao;
                    _artigo.Designacao = dr[16].ToString() != "" ? (_artigo.Designacao + "-" + dr[16].ToString()) : _artigo.Designacao;
                 
                    if(!GetInCounting(_artigo).Item1)
                       lista.Add(_artigo);
                }
            }
            catch (Exception ex)
            {
                _artigo = new ItemMovimentoStockDTO();
                _artigo.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }


        public void AddStockInicial(ItemMovimentoStockDTO dto)
        {
            try
            {
                ComandText = "stp_COM_ARTIGO_ADICIONAR_STOCK_INICIAL"; 

                AddParameter("QUANTIDADE", dto.Existencia);
                AddParameter("WAREHOUSE_ID", dto.Armazem);
                AddParameter("PRODUCT_ID", dto.ArtigoID);
                AddParameter("BARCODE", dto.BarCode); 
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("MOVIMENT_ID", dto.Movimento);
                AddParameter("UNIDADE_ID", dto.Unidade);
                AddParameter("VALIDADE", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("PRECO_COMPRA", dto.PrecoCompra);
                AddParameter("PRECO_VENDA", dto.PrecoVenda);
                AddParameter("DATA_SI", dto.LookupDate10); // Data de Lançamento do Stock Inicial

                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            } 
        }

        public void ActualizaStock(ArtigoDTO dto)
        {
            try
            {
                ComandText = "stp_STOCK_ACTUALIZAR_STOCK_PRODUTO";

                AddParameter("PRODUCT_ID", dto.Codigo);
                AddParameter("QUANTIDADE", dto.Quantidade); 
                AddParameter("ARMAZEM_ID", dto.ArmazemID);

                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }
        }

        public InventarioDTO SaveInventory(InventarioDTO dto)
        {
            try
            {
                ComandText = "stp_STOCK_INVENTORY_ADICIONAR";

                AddParameter("INVENTORY_ID", dto.InventoryID);
                AddParameter("REFERENCE", dto.Reference); 
                AddParameter("@WAREHOUSE_ID", dto.WarehouseID);
                AddParameter("INVENTORY_DATE", dto.InventoryDate);
                AddParameter("@INVENTORY_LAUCH", dto.RegistrationDate); 
                AddParameter("@EMPLOYEE_ID", dto.EmployeeID);
                AddParameter("@COMPANY_ID", dto.Filial);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@pSTATUS", dto.InventoryStatus);
                
                dto.InventoryID = ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.InventoryID = 0;
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if(dto.InventoryID > 0)
                {
                    foreach (var item in dto.InventoryItemsList)
                    {
                        item.Movimento = dto.InventoryID; 
                        AddInventoryItem(item);
                    }
                }
            }

            return dto;
        }

        public InventarioDTO CancelInventory(InventarioDTO dto)
        {
            try
            {
                ComandText = "stp_STOCK_INVENTORY_CANCELAR";

                AddParameter("INVENTORY_ID", dto.InventoryID);  

                dto.InventoryID = ExecuteInsert();

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.InventoryID = 0;
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if (dto.InventoryID > 0)
                {
                    foreach (var item in dto.InventoryItemsList)
                    {
                        item.Movimento = dto.InventoryID;
                        AddInventoryItem(item);
                    }
                }
            }

            return dto;
        }

        private void AddInventoryItem(ItemMovimentoStockDTO dto)
        {
            try
            {
                ComandText = "stp_STOCK_INVENTORY_ITEM_ADICIONAR";
                AddParameter("INVENTORY_ID", dto.Movimento);
                AddParameter("PRODUCT_ID", dto.ArtigoID); 
                AddParameter("EXISTENCIA", dto.Existencia);
                AddParameter("CONTAGEM", dto.ContagemFisica); 
                AddParameter("ACERTO", dto.Acrto);
                AddParameter("@LOTE_ID", dto.LoteID <= 0 ? (object)DBNull.Value : dto.LoteID);
                AddParameter("@SIZE_ID", dto.DimensaoID <= 0 ? (object)DBNull.Value : dto.DimensaoID);
                AddParameter("@SERIAL_ID", dto.SerialNumberID <= 0 ? (object)DBNull.Value : dto.SerialNumberID);
                AddParameter("PRECO_CUSTO", dto.PrecoCusto);
                AddParameter("PRECO_VENDA", dto.PrecoVenda);
                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            } 
        }

        public List<InventarioDTO> GetInventoryByReference(InventarioDTO dto)
        { 
            List<InventarioDTO> lista = new List<InventarioDTO>();

            try
            {
                ComandText = "stp_STOCK_INVENTORY_OBTERPORFILTRO";

                AddParameter("@REFERENCIA", dto.Reference == null ? string.Empty : dto.Reference);
                AddParameter("@ESTADO", dto.InventoryStatus == null ? "-1" : dto.InventoryStatus);
                AddParameter("@PERIODO", dto.InventoryDate == DateTime.MinValue ? (object)DBNull.Value : dto.InventoryDate);
                AddParameter("@WAREHOUSE_ID", dto.WarehouseID);
                AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new InventarioDTO();
                    dto.InventoryID = int.Parse(dr[0].ToString());
                    dto.InventoryDate = DateTime.Parse(dr[1].ToString());
                    dto.Reference = dr[2].ToString();
                    dto.Localizacao = dr[3].ToString();
                    dto.FuncionarioID = dr[4].ToString();
                    dto.InventoryStatus = GetStatusDescription(dr[5].ToString());
                    dto.EmployeeID = int.Parse(dr[6].ToString() == "" ? "-1" : dr[6].ToString());
                    dto.Situacao = dr[5].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new InventarioDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
                dto.Reference = dto.MensagemErro;
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        private string GetStatusDescription(string pStatusID)
        {
            if (pStatusID == "E")
            {
                return "EM PREPARAÇÃO";
            }
            if (pStatusID == "F")
            {
                return "FINALIZADA";
            }
            else if (pStatusID == "C")
            {
                return "EM CONTAGEM";
            }
            else
            {
                return "ANULADA";
            }
        }

        public List<ItemMovimentoStockDTO> GetInventoryProductList(ItemMovimentoStockDTO _artigo)
        {
            List<ItemMovimentoStockDTO> lista = new List<ItemMovimentoStockDTO>();
            
            try
            {
                ComandText = "stp_STOCK_INVENTORY_OBTERPRODUCTS";

                AddParameter("@INVENTORY_ID", _artigo.Movimento); 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    _artigo = new ItemMovimentoStockDTO();
                    _artigo.Movimento = int.Parse(dr[0].ToString());
                    _artigo.Armazem = dr[1].ToString();
                    _artigo.LookupDate1 = DateTime.Parse(dr[2].ToString());
                    _artigo.TituloDocumento = dr[3].ToString();
                    _artigo.LookupDate2 = DateTime.Parse(dr[4].ToString());
                    _artigo.FuncionarioID = dr[5].ToString();
                    _artigo.SocialName = dr[6].ToString();
                    _artigo.Situacao = dr[7].ToString(); //GetStatusDescription(dr[7].ToString()); 
                    _artigo.LookupDate3 = DateTime.Parse(dr[8].ToString());
                    _artigo.ArtigoID = int.Parse(dr[9].ToString()); 
                    _artigo.BarCode = dr[10].ToString();
                    _artigo.Referencia = dr[11].ToString();
                    _artigo.Designacao = dr[12].ToString();
                    _artigo.Classificacao = dr[13].ToString();
                    _artigo.Quantidade = Convert.ToDecimal(dr[14].ToString());
                    _artigo.PrecoCusto = Convert.ToDecimal(dr[15].ToString());
                    _artigo.PrecoVenda = Convert.ToDecimal(dr[16].ToString());  
                    _artigo.FotoArtigo = dr[18].ToString(); 
                    _artigo.Existencia = _artigo.Quantidade;
                    _artigo.ContagemFisica = decimal.Parse(dr[19].ToString());
                    _artigo.Acrto = decimal.Parse(dr[20].ToString());
                    _artigo.Unidade = dr[21].ToString();
                    _artigo.LoteID = int.Parse(dr[22].ToString());
                    _artigo.DimensaoID = int.Parse(dr[23].ToString());
                    _artigo.SerialNumberID = int.Parse(dr[24].ToString());
                    _artigo.Designacao = dr[25].ToString() != "" ? (_artigo.Designacao + "-" + dr[25].ToString()) : _artigo.Designacao;
                    _artigo.Designacao = dr[26].ToString() != "" ? (_artigo.Designacao + "-" + dr[26].ToString()) : _artigo.Designacao;
                    _artigo.Designacao = dr[27].ToString() != "" ? (_artigo.Designacao + "-" + dr[27].ToString()) : _artigo.Designacao;
                    lista.Add(_artigo);
                }
            }
            catch (Exception ex)
            {
                _artigo = new ItemMovimentoStockDTO();
                _artigo.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<ItemMovimentoStockDTO> ObterProductExtract(ItemMovimentoStockDTO dto)
        {

            List<ItemMovimentoStockDTO> lista = new List<ItemMovimentoStockDTO>();
            try
            {
                ComandText = "stp_STOCK_EXTRATO_ARTIGO";


                AddParameter("@ARTIGO_ID", dto.ArtigoID);
                AddParameter("@DATA_INI", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("@DATA_TERM", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);
                AddParameter("@DOCUMENT_ID", dto.SerieID);
                AddParameter("@WAREHOUSE_ID", dto.Armazem);
                AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                decimal Saldo = 0;
                while (dr.Read())
                {
                    dto = new ItemMovimentoStockDTO
                    {
                        Movimento= int.Parse(dr[0].ToString()),
                        ArtigoID = int.Parse(dr[1].ToString()),
                        CreatedDate = !string.IsNullOrEmpty(dr[2].ToString()) ? DateTime.Parse(dr[2].ToString()) : DateTime.MinValue, 
                        WareHouseName = dr[3].ToString(),
                        Designacao = dr[4].ToString(),
                        Operacao = int.Parse(dr[5].ToString()),
                        PrecoUnitario = decimal.Parse(dr[6].ToString()),
                        Existencia = decimal.Parse(dr[7].ToString()),
                        Quantidade = decimal.Parse(dr[8].ToString()),
                        Referencia = dr[9].ToString(),
                        PrecoCustoMedio_Actual = decimal.Parse(dr[10].ToString()), 
                        Unidade = dr[11].ToString(),
                        WarehouseFrom = dr[13].ToString(),
                        WarehouseDestiny = dr[14].ToString(),
                        TransferID = int.Parse(dr[15].ToString()),
                        Armazem = dr[15].ToString(),

                    };
                    dto.Referencia = dr[12].ToString()+"("+dto.Referencia+")";
                    /*
                    if(dto.TransferID > 0)
                    {
                        if(lista.Exists(t => t.Operacao == dto.Operacao && t.TransferID == dto.TransferID))
                        {
                            var item = lista.Where(t => t.Operacao == dto.Operacao && t.TransferID == dto.TransferID).SingleOrDefault();
                            if (dto.Operacao == 1 && item.WareHouseName == dto.WarehouseDestiny ||
                                dto.Operacao == 0 && item.WareHouseName == dto.WarehouseFrom)
                            {
                                dto = null;
                            }
                        }
                        else if(dto.Operacao == 1 && dto.WareHouseName == dto.WarehouseDestiny ||
                                dto.Operacao == 0 && dto.WareHouseName == dto.WarehouseFrom)
                        {
                            dto = null;
                        }
                    }*/
                    

                    if (dto != null)
                    {
                        Saldo = Saldo == 0 ? (dto.Existencia + dto.Quantidade) : Saldo + dto.Quantidade;
                        dto.TotalLiquido = Saldo;
                        lista.Add(dto);
                    }
                     
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public Tuple<bool, string> GetInCounting(ItemMovimentoStockDTO dto)
        { 
            try
            {
                ComandText = "stp_STOCK_INVENTORY_OBTERPRODUCT_INCOUNTING";

                AddParameter("@PRODUCT_ID", dto.ArtigoID);

                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    if(int.Parse(dr[9].ToString()) == dto.ArtigoID && dr[7].ToString()=="C")
                    {
                        dto = new ItemMovimentoStockDTO { Sucesso = true, MensagemErro = dr[12].ToString() + " consta de uma contagem anterior que não está finalizada" };
                    } 
                }
            }
            catch (Exception ex)
            {
                dto = new ItemMovimentoStockDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty); 
            }
            finally
            {
                FecharConexao();
            }

            return new Tuple<bool, string>(dto.Sucesso, dto.MensagemErro);
        }
    }
}
