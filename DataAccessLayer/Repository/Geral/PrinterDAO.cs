using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class PrinterDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public PrinterDTO Adicionar(PrinterDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_PRINTERS_ADICIONAR";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@NOME", dto.Sigla);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao); 
                BaseDados.AddParameter("@IP", dto.AddressIP);
                BaseDados.AddParameter("@PATH", dto.NetworkPath);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally 
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }
         

        public List<PrinterDTO> ObterPorFiltro()
        {
            List<PrinterDTO> lista = new List<PrinterDTO>();
            try
            {
                
                BaseDados.ComandText = "stp_SIS_PRINTERS_OBTERTODAS"; 


                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read()) 
                {
                    PrinterDTO dto = new PrinterDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Sigla = dr[1].ToString(),
                        Descricao = dr[2].ToString(),
                        AddressIP = dr[3].ToString(),
                       NetworkPath = dr[2].ToString()
                    };

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
               var orint = new PrinterDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

         
        public ProductPrinterDTO AddProductPrinter(ProductPrinterDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_ARTIGO_PRINTERS_ADICIONAR";


                BaseDados.AddParameter("@ARTIGO", dto.ProductID);
                BaseDados.AddParameter("@IMPRESSORA", dto.PrinterID);
                BaseDados.AddParameter("@VIA", dto.CopyNumber);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        } 

        public ProductPrinterDTO DeleteProductPrinter(ProductPrinterDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_ARTIGO_PRINTERS_EXCLUIR";


                BaseDados.AddParameter("@ARTIGO", dto.ProductID); 
                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }



        public List<ProductPrinterDTO> ObterProductPrinterList(ProductPrinterDTO dto)
        {
            var lista = new List<ProductPrinterDTO>();
            try
            {
                BaseDados.ComandText = "stp_COM_ARTIGO_PRINTERS_OBTERPORFILTRO";

                BaseDados.AddParameter("@ARTIGO", dto.ProductID);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ProductPrinterDTO
                    {
                        ProductID = int.Parse(dr[0].ToString()),
                        PrinterID = int.Parse(dr[1].ToString()),
                        PrinterName = dr[2].ToString(),
                        CopyNumber = int.Parse(dr[3].ToString())
                    };

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
