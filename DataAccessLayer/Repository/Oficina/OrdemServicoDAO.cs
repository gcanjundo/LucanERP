using Dominio.Oficina;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Oficina
{
    public class OrdemServicoDAO : ConexaoDB
    {
        public OrdemServicoDTO Add(OrdemServicoDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_ADICIONAR";

                AddParameter("@OS_ID", dto.WorkOrderID);
                AddParameter("@DOCUMENT", dto.DocumentID);
                AddParameter("@SERIE", dto.SerieID);
                AddParameter("@OS_TYPE", dto.OsType);
                AddParameter("@REFERENCE", dto.Reference);
                AddParameter("@NUMBER", dto.NroOrdenacao);
                AddParameter("@DOC_STATE", dto.DocumentStatus);
                AddParameter("@TECHNICIAN_ID", dto.TechnicianID <= 0 ? (object)DBNull.Value : dto.TechnicianID);
                AddParameter("@CAR", dto.VehicleID);
                AddParameter("@KM", dto.InitialKM);
                AddParameter("@OS_STATUS", dto.OsStatus);
                AddParameter("@BOOKER_USER", dto.BookingUser);
                AddParameter("@BOOKING_DATE", (dto.BookingDate == DateTime.MinValue ? (object)DBNull.Value : dto.BookingDate));
                AddParameter("@CHECKIN_TECHNICIAN", dto.CheckInTechnicianID <= 0 ? (object)DBNull.Value : dto.CheckInTechnicianID);
                AddParameter("@CHECKIN", (dto.CheckInDate == DateTime.MinValue ? (object)DBNull.Value : dto.CheckInDate));
                AddParameter("@DRIVER", dto.DriverPerson);
                AddParameter("@CONTACT", dto.DriverContact);
                AddParameter("@STARTED", (dto.OsBeginDate == DateTime.MinValue ? (object)DBNull.Value : dto.OsBeginDate));
                AddParameter("@FINISHED", (dto.OsEndDate == DateTime.MinValue ? (object)DBNull.Value : dto.OsEndDate));
                AddParameter("@CHECKOUT_TECHNICIAN", dto.CheckOutTechnicianID <= 0 ? (object)DBNull.Value : dto.CheckOutTechnicianID);
                AddParameter("@CHECKOUT", (dto.CheckOutDate == DateTime.MinValue ? (object)DBNull.Value : dto.CheckOutDate));
                AddParameter("@FLAG", dto.Actived == false ? 0 : 1);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@COMPANY_ID", dto.Filial);
                AddParameter("@PREDICTION_DATE", (dto.PredictionClosing == DateTime.MinValue ? (object)DBNull.Value : dto.PredictionClosing));
                AddParameter("@FREE_NOTES", dto.FreeNotes);
                AddParameter("@ENTITY_ID", dto.Entidade); 
                dto.WorkOrderID = ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if (dto.WorkOrderID > 0)
                {
                    foreach (var detail in dto.OrderDetailsList)
                    {
                        detail.OrderID = dto.WorkOrderID;
                        SaveDetail(detail);
                    }
                    dto.CheckListOrder.OrderServiceID = dto.WorkOrderID;
                    SaveChekList(dto.CheckListOrder);
                }
            }

            return dto;
        }

        public void AddInvoice(OrdemServicoDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_ADICIONAR_FATURA";

                AddParameter("@INVOICE", dto.InvoiceID);
                AddParameter("@WORK_ORDER", dto.WorkOrderID);

                ExecuteNonQuery();

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if (dto.WorkOrderID > 0)
                {
                    dto = ObterPorPK(dto);
                }
            }
        }

        public OrdemServicoDTO Excluir(OrdemServicoDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_EXCLUIR";

                AddParameter("@OS_ID", dto.WorkOrderID);
                 
                ExecuteNonQuery();

                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if (dto.WorkOrderID > 0)
                {
                    dto = ObterPorPK(dto);
                }
            }

            return dto;
        }

        public OrdemServicoDTO ObterPorPK(OrdemServicoDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_OBTERPORPK";
                AddParameter("@OS_ID", dto.WorkOrderID);

                MySqlDataReader dr = ExecuteReader();
                dto = new OrdemServicoDTO();
                if (dr.Read())
                {
                    dto.WorkOrderID = int.Parse(dr[0].ToString());
                    dto.DocumentID = int.Parse(dr[1].ToString());
                    dto.SerieID = int.Parse(dr[2].ToString()== string.Empty ? "-1" : dr[2].ToString());
                    dto.OsType = int.Parse(dr[3].ToString());
                    dto.Reference = dr[4].ToString();
                    dto.DocumentNumber = dr[5].ToString();
                    dto.DocumentStatus = int.Parse(dr[6].ToString());
                    dto.TechnicianID = int.Parse(dr[37].ToString()== string.Empty ? "-1" : dr[37].ToString());
                    dto.VehicleID = int.Parse(dr[8].ToString());
                    dto.InitialKM = decimal.Parse(dr[9].ToString());
                    dto.OsStatus = int.Parse(dr[10].ToString());
                    dto.BookingUser = dr[11].ToString();
                    dto.BookingDate = dr[12].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[12].ToString());
                    dto.CheckInTechnicianID = int.Parse(dr[38].ToString() == string.Empty ? "-1" :  dr[38].ToString());
                    dto.CheckInDate = dr[14].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[14].ToString());
                    dto.DriverPerson = dr[15].ToString();
                    dto.DriverContact = dr[16].ToString();
                    dto.OsBeginDate = dr[17].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[17].ToString());
                    dto.OsEndDate = dr[18].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[18].ToString());
                    dto.CheckOutTechnicianID = int.Parse(dr[39].ToString() == string.Empty ? "-1" : dr[39].ToString());
                    dto.CheckOutDate = dr[20].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[20].ToString());
                    dto.FreeNotes = dr[21].ToString();
                    dto.Actived = dr[22].ToString() == "1" ? true : false;
                    dto.CreatedBy = dr[23].ToString();
                    dto.CreatedDate = dr[24].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[24].ToString());
                    dto.UpdatedBy = dr[25].ToString();
                    dto.UpdatedDate = dr[26].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[26].ToString());
                    dto.Filial = dr[27].ToString();
                    dto.PredictionClosing = dr[28].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[28].ToString());  
                    dto.BillingEntityID = int.Parse(dr[30].ToString());
                    dto.WorkOrderTechnicianName = dr[32].ToString(); // NOME DO TECNICO RESPONSAVEL
                    dto.BillingEntityDesignation = dr[33].ToString();  // NOME DO CLIENTE A FACTURAR
                    dto.CheckInTechnicianName = dr[34].ToString();   // NOME DO RECEPCIONISTA
                    dto.CheckOutTechnicianName = dr[35].ToString(); // NOME DO RESPONSÁVEL PELA SAÍDA 
                    dto.DesignacaoEntidade = dr[36].ToString(); // NOME DO PROPRIETARIO
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
                if(dto.WorkOrderID > 0)
                {
                    var _oDetail = new OrderDetailDTO();
                    _oDetail.OrderID = dto.WorkOrderID;
                    _oDetail.TechnicianID = -1;
                    dto.OrderDetailsList = ObterDetalhesPorFiltro(_oDetail);
                    dto.CheckListOrder = ObterCheckList(new CheckListDTO(dto.WorkOrderID));

                }
            }

            return dto;
        }

        public List<OrdemServicoDTO> ObterPorFiltro(OrdemServicoDTO dto)
        {

            var lista = new List<OrdemServicoDTO>();
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_OBTERPORFILTRO";
                AddParameter("@FILTER", dto.LookupField1);
                AddParameter("@MATRICULA_ID", dto.Matricula);
                AddParameter("@CUSTOMER_ID", dto.Entidade);
                AddParameter("@REFERENCE", dto.Reference);
                AddParameter("@OS_TYPE", dto.OsType < 0 ? 1 : dto.OsType);
                AddParameter("@OS_STATUS", dto.Status);
                AddParameter("@CREATED_BY", dto.CreatedBy);
                AddParameter("@TECHNICIAN", dto.TechnicianID);
                AddParameter("@PRODUCT_ID", dto.WorkOrderID); // PRODUCT_ID
                AddParameter("@CHECKIN_FROM", (dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1));
                AddParameter("@CHECKIN_UNTIL", (dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2));
                AddParameter("@BOOKING_FROM", (dto.LookupDate3 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate3));
                AddParameter("@BOOKING_UNTIL", (dto.LookupDate4 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate4));
                AddParameter("@CHECKOUT_FROM", (dto.LookupDate5 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate5));
                AddParameter("@CHECKOUT_UNTIL", (dto.LookupDate6 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate6));
                AddParameter("@WORK_FROM", (dto.LookupDate7 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate7));
                AddParameter("@WORK_UNTIL", (dto.LookupDate8 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate8));
                AddParameter("@CLOSED_FROM", (dto.LookupDate9 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate9));
                AddParameter("@CLOSED_UNTIL", (dto.LookupDate10 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate10));
                AddParameter("@PREVISION_FROM", (dto.LookupDate11 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate11));
                AddParameter("@PREVISION_UNTIL", (dto.LookupDate12 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate12));
                AddParameter("@COMPANY_ID", dto.Filial);
                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new OrdemServicoDTO
                    {
                        WorkOrderID = int.Parse(dr[0].ToString()),
                        Reference = dr[1].ToString(),
                        DriverPerson = dr[2].ToString(),
                        Matricula = dr[3].ToString(),
                        Status = int.Parse(dr[4].ToString()),
                        BookingDate = dr[5].ToString() == null || dr[5].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[5].ToString()),
                        CheckInDate = dr[6].ToString() == null || dr[6].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[6].ToString()),
                        PredictionClosing = dr[7].ToString() == null || dr[7].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[7].ToString()),
                        CheckOutDate = dr[8].ToString() == null || dr[8].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[8].ToString()),
                        FuncionarioID = dr[9].ToString(),
                        OsBeginDate = dr[10].ToString() == null || dr[10].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[10].ToString()),
                        OsEndDate = dr[11].ToString() == null || dr[11].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[11].ToString()),
                        OsType = int.Parse(dr[12].ToString()),
                        ProductListSubTotal = dr[13].ToString() == null || dr[13].ToString() == string.Empty ? 0 : decimal.Parse(dr[13].ToString()),
                        ServiceListSubTotal = dr[14].ToString() == null || dr[14].ToString() == string.Empty ? 0 : decimal.Parse(dr[14].ToString()),
                        DesignacaoEntidade = dr[15].ToString(),// Owner
                        DriverContact = dr[16].ToString(),
                        CompanyPhone = dr[17].ToString(),
                        InvoiceID = dr[18].ToString() != "" ? int.Parse(dr[18].ToString()) : 0
                        
                    };
                    dto.OrderSubTotal = dto.ProductListSubTotal + dto.ServiceListSubTotal;
                    lista.Add(dto);
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


        

        public void SaveDetail(OrderDetailDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_DETALHES_ADICIONAR";
                AddParameter("@DETAIL_CODE", dto.DetailID);
                AddParameter("@ORDER_NUMBER", dto.NroOrdenacao);
                AddParameter("@ORDER_ID", dto.OrderID); 
                AddParameter("@ITEM_ID", dto.ItemID);
                AddParameter("@QUANTIDADE", dto.Quantity); 
                AddParameter("@PRECO", dto.UnitPrice);
                AddParameter("@DESCONTO", dto.Discount);
                AddParameter("@IMPOSTO", dto.Tax); 
                AddParameter("@TOTAL_VALUE", dto.SubTotal);
                AddParameter("@TECHNICIAN", dto.TechnicianID <=0 ? (object)DBNull.Value : dto.TechnicianID);
                AddParameter("@BEGIN_TASK", (dto.ActionStart == DateTime.MinValue ? (object)DBNull.Value : dto.ActionStart));
                AddParameter("@END_TASK", (dto.ActionEnd == DateTime.MinValue ? (object)DBNull.Value : dto.ActionEnd));
                AddParameter("@PREVISION", (dto.EndPrevision == DateTime.MinValue ? (object)DBNull.Value : dto.EndPrevision));
                AddParameter("@DETAIL_STATUS", dto.Status <=0 ? (object)DBNull.Value : dto.Status);
                AddParameter("@COMENTARIOS", dto.Notes);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@WAREHOUSE", dto.WareHouseID);
                AddParameter("@LOCATION", dto.WorshopLocationID);

                ExecuteNonQuery();

                dto.Sucesso = true;

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

        public List<OrderDetailDTO> ObterDetalhesPorFiltro(OrderDetailDTO dto)
        {

            var lista = new List<OrderDetailDTO>();
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_DETALHES_OBTERPORFILTRO";


                AddParameter("@ORDER_ID", dto.OrderID);
                AddParameter("@TECHNICIAN", dto.TechnicianID);
                AddParameter("@COMPANY_ID", dto.Filial);
                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new OrderDetailDTO
                    {
                        DetailID = int.Parse(dr[0].ToString()),
                        NroOrdenacao = int.Parse(dr[1].ToString()),
                        OrderID = int.Parse(dr[2].ToString()),
                        ItemID = int.Parse(dr[3].ToString()),
                        Quantity = decimal.Parse(dr[4].ToString()),
                        UnitPrice = decimal.Parse(dr[5].ToString()),
                        Discount = decimal.Parse(dr[6].ToString()),
                        Tax = decimal.Parse(dr[7].ToString()),
                        SubTotal = decimal.Parse(dr[8].ToString()),
                        TechnicianID = int.Parse(dr[9].ToString() == string.Empty ? "-1" : dr[9].ToString()),
                        ActionStart = dr[10].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[10].ToString()),
                        ActionEnd = dr[11].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[11].ToString()), 
                        EndPrevision = dr[12].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[12].ToString()),
                        Status = int.Parse(dr[13].ToString()),
                        Notes = dr[14].ToString(),
                        CreatedBy = dr[16].ToString(),
                        CreatedDate = dr[17].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[17].ToString()),
                        UpdatedBy = dr[18].ToString(),
                        UpdatedDate = dr[19].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[19].ToString()),
                        WareHouseID = int.Parse(dr[20].ToString()== string.Empty ? "-1" : dr[20].ToString()),
                        WorshopLocationID = int.Parse(dr[21].ToString() == string.Empty ? "-1" : dr[21].ToString()),
                        InvoiceID = int.Parse(dr[23].ToString() == string.Empty ? "0" : dr[23].ToString()),
                        TituloDocumento = dr[24].ToString(),
                        SocialName = dr[25].ToString(),
                        LookupNumericField1 = decimal.Parse(dr[26].ToString() == string.Empty ? "0" : dr[26].ToString()),
                        Designacao = dr[27].ToString(),
                        LookupField1 = dr[28].ToString(),
                        ItemType = dr[29].ToString()
                    };
                    lista.Add(dto);
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

        private void SaveChekList(CheckListDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_CHECKLIST_ADICIONAR";
                AddParameter("@ORDER_ID", dto.OrderServiceID);
                AddParameter("@TECHNICIAN", dto.FuncionarioID);
                AddParameter("@HAS_MACACO", dto.HasMacaco == true ? 1 : 0);
                AddParameter("@HAS_CHAVE", dto.HasChaveRoda == true ? 1 : 0);
                AddParameter("@HAS_MANIVELA", dto.HasManivela == true ? 1 : 0);
                AddParameter("@HAS_PNEU", dto.HasSocorro == true ? 1 : 0);
                AddParameter("@HAS_PISFLE", dto.HasPiscaFrontalLE == true ? 1 : 0);
                AddParameter("@HAS_PISFLD", dto.HasPiscaFrontalLD == true ? 1 : 0);
                AddParameter("@HAS_PISTLE", dto.HasPiscaTraseiroLE == true ? 1 : 0);
                AddParameter("@HAS_PISTLD", dto.HasPiscaTraseiroLD == true ? 1 : 0);
                AddParameter("@HAS_STOPLE", dto.HasStopLE == true ? 1 : 0);
                AddParameter("@HAS_STOPLD", dto.HasStopLD == true ? 1 : 0);
                AddParameter("@HAS_FAROLLE", dto.HasFarolLE == true ? 1 : 0);
                AddParameter("@HAS_FAROLLD", dto.HasFarolLD == true ? 1 : 0);
                AddParameter("@HAS_SYMBOLF", dto.HasSimboloFrontal == true ? 1 : 0);
                AddParameter("@HAS_SYMBOLT", dto.HasSimboloTraseiro == true ? 1 : 0);
                AddParameter("@HAS_RETRO_LE", dto.HasRetrovisorLE == true ? 1 : 0);
                AddParameter("@HAS_RETRO_LD", dto.HasRetrovisorLD == true ? 1 : 0);
                AddParameter("@HAS_RETRO_IN", dto.HasRetrovisorInterior == true ? 1 : 0);
                AddParameter("@HAS_RADIO", dto.HasRadio == true ? 1 : 0);
                AddParameter("@HAS_TAPFLE", dto.HasTapeteFrontalLE == true ? 1 : 0);
                AddParameter("@HAS_TAPFLD", dto.HasTapeteFrontalLD == true ? 1 : 0);
                AddParameter("@HAS_TAPTLE", dto.HasTapeteTraseiroLE == true ? 1 : 0);
                AddParameter("@HAS_TAPTLD", dto.HasTapeteTraseiroLD == true ? 1 : 0);
                AddParameter("@HAS_TAPTMEI", dto.HasTapeteTraseiroMeio == true ? 1 : 0);
                AddParameter("@HAS_ISQUEIRO", dto.HasIsqueiro == true ? 1 : 0);
                AddParameter("@HAS_ELEVADORFLE", dto.HasElevadorFrontalLE == true ? 1 : 0);
                AddParameter("@HAS_ELEVADORFLD", dto.HasElevadorFrontalLD == true ? 1 : 0);
                AddParameter("@HAS_ELEVADORTLE", dto.HasElevadorTraseiroLE == true ? 1 : 0);
                AddParameter("@HAS_ELEVADORTLD", dto.HasElevadorTraseiroLD == true ? 1 : 0);
                AddParameter("@HAS_COLETE", dto.HasColete == true ? 1 : 0);
                AddParameter("@HAS_TRIANGULO", dto.HasTriangulo == true ? 1 : 0);
                AddParameter("@FLUEL_LEVEL", dto.NivelCombustivel);
                AddParameter("@UTILIZADOR", dto.Utilizador);

                ExecuteNonQuery();

                dto.Sucesso = true;

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

        public CheckListDTO ObterCheckList(CheckListDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_ORDEM_SERVICO_CHECKLIST_OBTERPORPK";

                AddParameter("@OS_ID", dto.OrderServiceID);

                MySqlDataReader dr = ExecuteReader();
                dto = new CheckListDTO();
                if (dr.Read())
                {
                    dto.OrderServiceID = int.Parse(dr[0].ToString());
                    dto.FuncionarioID = dr[1].ToString();
                    dto.HasMacaco = dr[2].ToString() == "1" ? true : false;
                    dto.HasChaveRoda = dr[3].ToString() == "1" ? true : false;
                    dto.HasManivela = dr[4].ToString() == "1" ? true : false;
                    dto.HasSocorro = dr[5].ToString() == "1" ? true : false;
                    dto.HasPiscaFrontalLE = dr[6].ToString() == "1" ? true : false;
                    dto.HasPiscaFrontalLD = dr[7].ToString() == "1" ? true : false;
                    dto.HasPiscaTraseiroLE = dr[8].ToString() == "1" ? true : false;
                    dto.HasPiscaTraseiroLD = dr[9].ToString() == "1" ? true : false;
                    dto.HasStopLE = dr[10].ToString() == "1" ? true : false;
                    dto.HasStopLD = dr[11].ToString() == "1" ? true : false;
                    dto.HasFarolLE = dr[12].ToString() == "1" ? true : false;
                    dto.HasFarolLD = dr[13].ToString() == "1" ? true : false;
                    dto.HasSimboloFrontal = dr[14].ToString() == "1" ? true : false;
                    dto.HasSimboloTraseiro = dr[15].ToString() == "1" ? true : false;
                    dto.HasRetrovisorLE = dr[16].ToString() == "1" ? true : false;
                    dto.HasRetrovisorLD = dr[17].ToString() == "1" ? true : false;
                    dto.HasRadio = dr[18].ToString() == "1" ? true : false;
                    dto.HasTapeteFrontalLE = dr[19].ToString() == "1" ? true : false;
                    dto.HasTapeteFrontalLD = dr[20].ToString() == "1" ? true : false;
                    dto.HasTapeteTraseiroLE = dr[21].ToString() == "1" ? true : false;
                    dto.HasTapeteTraseiroLD = dr[23].ToString() == "1" ? true : false;
                    dto.HasTapeteTraseiroMeio = dr[24].ToString() == "1" ? true : false;
                    dto.HasIsqueiro = dr[25].ToString() == "1" ? true : false;
                    dto.HasElevadorFrontalLE = dr[26].ToString() == "1" ? true : false;
                    dto.HasElevadorFrontalLD = dr[27].ToString() == "1" ? true : false;
                    dto.HasElevadorTraseiroLE = dr[28].ToString() == "1" ? true : false;
                    dto.HasElevadorTraseiroLD = dr[29].ToString() == "1" ? true : false;
                    dto.HasColete = dr[30].ToString() == "1" ? true : false;
                    dto.HasTriangulo = dr[31].ToString() == "1" ? true : false;
                    dto.NivelCombustivel = dr[32].ToString();
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

            return dto;
        }



    }
}
