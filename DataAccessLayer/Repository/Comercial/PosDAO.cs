using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial.POS
{
    public class PosDAO
    {

        ConexaoDB BaseDados = new ConexaoDB();

        public PosDTO Adicionar(PosDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_POS_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Situacao);
                BaseDados.AddParameter("DESIGNACAO", dto.DesignacaoEntidade);
                BaseDados.AddParameter("ARMAZEM_ID", dto.WarehouseID <= 0 ? (object)DBNull.Value : dto.WarehouseID);
                BaseDados.AddParameter("DOCUMENT_ID", dto.DocumentSerieID <= 0 ? (object)DBNull.Value : dto.DocumentSerieID);
                BaseDados.AddParameter("CUSTOMER_ID", dto.CustomerDefault <= 0 ? (object)DBNull.Value : dto.CustomerDefault);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("PAYMENT_ID", dto.PaymentCondition <=0 ? (object)DBNull.Value : dto.PaymentCondition);
                BaseDados.AddParameter("VENDEDOR_ID", dto.FuncionarioID==null || dto.FuncionarioID=="" ? (object)DBNull.Value : dto.FuncionarioID);
                BaseDados.AddParameter("PRICE_TABLE_ID", dto.PriceTableID <= 0 ? (object)DBNull.Value : dto.PriceTableID);
                BaseDados.AddParameter("FUNDO", dto.FundoManeio);
                BaseDados.AddParameter("CALENDARIO", dto.AllowCalendar!=true ? 0 : 1);
                BaseDados.AddParameter("PREVENT_CLOSE", dto.PreventCloseWithSuspendSale != true ? 0 : 1);
                BaseDados.AddParameter("CASH_SERIE_ID", dto.CashRefundSerieID <= 0 ? (object)DBNull.Value : dto.CashRefundSerieID);
                BaseDados.AddParameter("CREDIT_SERIE_ID", dto.CreditRefundSerieID <= 0 ? (object)DBNull.Value : dto.CreditRefundSerieID);
                BaseDados.AddParameter("PAYMENT_METHOD_ID", dto.PaymentMethodID <= 0 ? (object)DBNull.Value : dto.PaymentMethodID);
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
        public List<PosDTO> ObterCaixaFechadas(PosDTO dto)
        {
            List<PosDTO> lista = new List<PosDTO>();
            try
            {
                BaseDados.ComandText = "stp_COM_POS_OBTER_CAIXA_FECHADA";
                BaseDados.AddParameter("DATA_HOJE",dto.DataHoje);
                BaseDados.AddParameter("FILIAL",dto.Filial);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("ACCAO", dto.Accao);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                while (dr.Read())
                {
                    dto = new PosDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<PosDTO>(); lista.Add(dto);

            }
            finally
            {
              BaseDados.FecharConexao();
            }
            return lista;
        }

        public bool Eliminar(PosDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_POS_EXCLUIR";


                BaseDados.AddParameter("CODIGO", dto.Codigo);

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

            return dto.Sucesso;
        }

        public List<PosDTO> ObterPorFiltro(PosDTO dto)
        {
            List<PosDTO> lista;
             
            try
            {

                BaseDados.ComandText = "stp_COM_POS_OBTERPORFILTRO";

                BaseDados.AddParameter("@FILIAL", dto.Filial==null ? "-1" : dto.Filial);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<PosDTO>();
                while (dr.Read())
                {

                    dto = new PosDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Sigla = dr[2].ToString(),
                        Estado = int.Parse(dr[3].ToString()),
                        FuncionarioID = dr[4].ToString(),
                        WarehouseID = dr[5].ToString() != "" ? int.Parse(dr[5].ToString()) : -1,
                        DocumentSerieID = dr[6].ToString() != "" ? int.Parse(dr[6].ToString()) : -1,
                        CustomerDefault = dr[7].ToString() != "" ? int.Parse(dr[7].ToString()) : -1,
                        DesignacaoEntidade = dr[8].ToString(),
                        SocialName = dr[9].ToString(),
                        Utilizador = dr[10].ToString(),
                        DefaultDocument = dr[11].ToString() != "" ? int.Parse(dr[11].ToString()) : -1,
                        TituloDocumento = dr[12].ToString(),
                        Filial = dr[13].ToString(),
                        LookupField2 = dr[14].ToString() == string.Empty ? "TODOS" : dr[14].ToString(),
                        PaymentCondition = dr[15].ToString() != "" ? int.Parse(dr[15].ToString()) : -1,
                        PaymentMethodID = dr[16].ToString() != "" ? int.Parse(dr[16].ToString()) : -1,
                        PriceTableID = dr[18].ToString() != "" ? int.Parse(dr[1].ToString()) : -1,
                        FundoManeio = dr[19].ToString() != "" ? decimal.Parse(dr[19].ToString()) : 0,
                        AllowCalendar = dr[20].ToString() == "1" ? true : false,
                        PreventCloseWithSuspendSale = dr[21].ToString() == "1" ? true : false,
                        CashRefundSerieID = dr[22].ToString() != "" ? int.Parse(dr[22].ToString()) : -1,
                        CreditRefundSerieID = dr[23].ToString() != "" ? int.Parse(dr[23].ToString()) : -1,
                        CashRefundDocumentID = dr[24].ToString() != "" ? int.Parse(dr[24].ToString()) : -1,
                        CreditRefundDocumentID = dr[25].ToString() != "" ? int.Parse(dr[25].ToString()) : -1,
                        PinCode = dr[26].ToString(), 
                    };

                    dto.ForRest = dr[27].ToString() == "1" || dto.WarehouseID <= 0 ? true : false;

                    if (dto.Sigla == "")
                    {
                        dto.Sigla = "POS00_" + dto.Codigo.ToString();
                    }

                    if (dto.DesignacaoEntidade == "")
                    {
                        dto.DesignacaoEntidade = "POSTO DE VENDA 00_" + dto.Codigo.ToString() ;
                    }
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PosDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                lista = new List<PosDTO>
                {
                    dto
                };

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }

        public PosDTO ObterPorPK(PosDTO dto)
        {
            try
            { 
                dto = ObterPorFiltro(dto).Where(t => t.Codigo == dto.Codigo).SingleOrDefault();
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

        public void ChangePosPinCode(PosDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_POS_CHANGE_PINCODE";


                BaseDados.AddParameter("PINCODE", dto.PinCode);
                BaseDados.AddParameter("POS_ID", dto.Codigo);

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
        }


    }
}
