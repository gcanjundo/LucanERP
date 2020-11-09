using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using MySql.Data.MySqlClient;
using Dominio.Clinica;
using DataAccessLayer.Geral;

namespace DataAccessLayer.Clinica
{
    public class LaboratorioRequisicaoExameDAO : ConexaoDB
    {


        public LaboratorioRequisicaoExameDTO Adicionar(LaboratorioRequisicaoExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_REQUISICAO_EXAME_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@ORDER_ID", dto.CustomerOrderID <= 0 ? (object)DBNull.Value : dto.CustomerOrderID);
                AddParameter("@PACIENTE_ID", dto.PacientID);
                AddParameter("@REQUESTER_ID", dto.ProfissionalSolicitanteID <= 0 ? (object)DBNull.Value : dto.ProfissionalSolicitanteID);
                AddParameter("@REQUESTER_DETAILS", dto.OtherRequesterDetails);
                AddParameter("@REQUEST_DATE", dto.DataRequisicao);
                AddParameter("@REQUEST_STATUS", dto.Status);
                AddParameter("@REQUESTER_NOTES", dto.Observacoes);
                AddParameter("@CONVENIO_ID", dto.ConvenioID);
                AddParameter("@BENEFICIARIO", dto.NroBeneficiario);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@TOTAL_EXAMES", dto.TotalExames);
                AddParameter("@TOTAL_DESCONTO", dto.TotalDesconto);
                AddParameter("@TOTAL_UTENTE", dto.TotalUtente);
                AddParameter("@TOTAL_ENTIDADE", dto.TotalEntidade);
                AddParameter("@TOTAL_GERAL", dto.TotalGeral);
                AddParameter("@DELIVERY_PREVISION", dto.DataPrevisaoEntrega); 
                AddParameter("@PRIORIDADE", dto.Priority);
                AddParameter("@COMPANY_ID", dto.Filial);


                dto.Codigo = ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
                if (dto.Codigo > 0 && dto.Sucesso)
                {
                    foreach (var exam in dto.RequisicaoExamesList)
                    {
                        exam.RequisicaoID = dto.Codigo;
                        AddRequestItem(exam);
                    }
                }
            }

            return dto;
        }

        public void AddRequestItem(LaboratorioRequisicaoExameDetalhesDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_REQUISICAO_EXAME_DETAILS_ADICIONAR";

                AddParameter("@REQUEST_ID", dto.RequisicaoID);
                AddParameter("@EXAME_ID", dto.ExameID);
                AddParameter("@ORDER_ITEM_ID", dto.OrderItemID <= 0 ? (object)DBNull.Value : dto.OrderItemID);
                AddParameter("@DELIVERY_DATE", dto.PrevisionDeliveryDate);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@PRECO_UNITARIO", dto.PrecoUnitario);
                AddParameter("@DESCONTO", dto.Desconto);
                AddParameter("@VALOR_DESCONTO", dto.ValorDesconto);
                AddParameter("@VALOR_UTENTE", dto.ValorUtente);
                AddParameter("@VALOR_ENTIDADE", dto.ValorEntidade);
                AddParameter("@VALOR_TOTAL", dto.ValorTotal);
                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }
        }


        public LaboratorioRequisicaoExameDTO Excluir(LaboratorioRequisicaoExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_REQUISICAO_EXAME_EXCLUIR";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("MOTIVO", dto.Observacoes);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<LaboratorioRequisicaoExameDTO> ObterPorFiltro(LaboratorioRequisicaoExameDTO dto)
        {
            var lista = new List<LaboratorioRequisicaoExameDTO>();
            try
            {
                ComandText = "stp_CLI_LABORATORIO_REQUISICAO_EXAME_OBTERPORFILTRO";


                AddParameter("@REQUEST_ID", dto.Codigo);
                AddParameter("@PACIENTE_ID", dto.PacientID);
                AddParameter("@PROFESSIONAL_ID", dto.ProfissionalSolicitanteID);
                AddParameter("@REQUESTED_FROM", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("@REQUESTED_UNTIL", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);
                AddParameter("@CREATED_BY", dto.Utilizador);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new LaboratorioRequisicaoExameDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        PacientID = int.Parse(dr[1].ToString()),
                        ProfissionalSolicitanteID = dr[2].ToString() == "" ? -1 : int.Parse(dr[2].ToString()),
                        DataRequisicao = Convert.ToDateTime(dr[3].ToString()),
                        Status = int.Parse(dr[4].ToString()),
                        DataPrevisaoEntrega = Convert.ToDateTime(dr[5].ToString()),
                        DateEntrega = dr[6].ToString() != "" ? Convert.ToDateTime(dr[6].ToString()) : DateTime.MinValue,
                        SocialName = dr[7].ToString(),
                        OtherRequesterDetails = dr[8].ToString(),
                        Priority = int.Parse(dr[9].ToString()),
                        LookupNumericField1 = int.Parse(dr[10].ToString()),
                        LookupField1 = dr[11].ToString(),
                        LookupField2 = new StatusDAO().CustomerRequestServiceStatusList().Where(t => t.Codigo == int.Parse(dr[4].ToString())).SingleOrDefault().Descricao,
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
                FecharConexao();
            }

            return lista;
        }

        public LaboratorioRequisicaoExameDTO ObterPorPK(LaboratorioRequisicaoExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_REQUISICAO_EXAME_OBTERPORPK";


                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();
                dto = new LaboratorioRequisicaoExameDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.PacientID = int.Parse(dr[1].ToString());
                    dto.ProfissionalSolicitanteID = dr[2].ToString()!="" ? int.Parse(dr[2].ToString()) : -1;
                    dto.OtherRequesterDetails = dr[3].ToString();

                    if (dr[4].ToString() != null && dr[4].ToString() != "")
                        dto.DataRequisicao = Convert.ToDateTime(dr[4].ToString());

                    if (dr[5].ToString() != null && dr[5].ToString() != "")
                        dto.DataPrevisaoEntrega = Convert.ToDateTime(dr[5].ToString());

                    dto.Status = int.Parse(dr[6].ToString());
                    dto.Observacoes = dr[7].ToString();
                    dto.ConvenioID = dr[8].ToString()!="" ? int.Parse(dr[8].ToString()) : -1;
                    dto.NroBeneficiario = dr[9].ToString();
                    if (dr[10].ToString() != null && dr[10].ToString() != "")
                        dto.InicioProcessamento = Convert.ToDateTime(dr[10].ToString());
                    if (dr[11].ToString() != null && dr[11].ToString() != "")
                        dto.TerminoProcessamento = Convert.ToDateTime(dr[11].ToString());
                    if (dr[12].ToString() != null && dr[12].ToString() != "")
                        dto.DateEntrega = Convert.ToDateTime(dr[12].ToString());
                    dto.TotalExames = decimal.Parse(dr[19].ToString());
                    dto.TotalDesconto = decimal.Parse(dr[20].ToString());
                    dto.TotalUtente = decimal.Parse(dr[21].ToString());
                    dto.TotalEntidade = decimal.Parse(dr[22].ToString());
                    dto.TotalGeral = decimal.Parse(dr[23].ToString());
                    dto.TotalLquidado = dr[24].ToString()!="" ? decimal.Parse(dr[24].ToString()) : 0;
                    dto.Priority = int.Parse(dr[25].ToString());
                    dto.Filial = dr[26].ToString();
                    dto.LookupDate1 = Convert.ToDateTime(dr[27].ToString()); 
                    dto.LookupField1 = dr[28].ToString() == "F" ? "FEMININO" : "MASCULINO";
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
                dto.RequisicaoExamesList = GetRequestDetailsList(new LaboratorioRequisicaoExameDetalhesDTO { RequisicaoID = dto.Codigo, ExameID = -1 });
            }

            return dto;
        }

        public List<LaboratorioRequisicaoExameDetalhesDTO> GetRequestDetailsList(LaboratorioRequisicaoExameDetalhesDTO dto)
        {
            var lista = new List<LaboratorioRequisicaoExameDetalhesDTO>();

            try
            {
                ComandText = "stp_CLI_LABORATORIO_REQUISICAO_EXAME_DETAILS_OBTERPORFILTRO";


                AddParameter("REQUEST_ID", dto.RequisicaoID);
                AddParameter("EXAME_ID", dto.ExameID);
                MySqlDataReader dr = ExecuteReader();
                int ordem = 1;
                while (dr.Read())
                {
                    dto = new LaboratorioRequisicaoExameDetalhesDTO
                    {
                        NroOrdenacao = ordem,
                        RequisicaoID = int.Parse(dr[0].ToString()),
                        ExameID = int.Parse(dr[1].ToString()),
                        PrevisionDeliveryDate = DateTime.Parse(dr[2].ToString()),
                        ProcessBeginDate = dr[3].ToString() != "" ? DateTime.Parse(dr[3].ToString()) : DateTime.MinValue,
                        CollectionDate = dr[4].ToString() != "" ? DateTime.Parse(dr[4].ToString()) : DateTime.MinValue,
                        ProfessionalCollectionID = dr[5].ToString() != "" ? int.Parse(dr[5].ToString()) : -1,
                        IsSelfCollection = dr[6].ToString() != "1" ? false : true,
                        ProcessProfessionalID = dr[7].ToString() != "" ? int.Parse(dr[7].ToString()) : -1,
                        TechnicalNotes = dr[8].ToString(),
                        ResultReferenceValue = dr[9].ToString() != "" ? decimal.Parse(dr[9].ToString()) : 0,
                        Result = dr[10].ToString(),
                        ResultNotes = dr[11].ToString(),
                        ResultDate = dr[12].ToString() != "" ? DateTime.Parse(dr[12].ToString()) : DateTime.MinValue,
                        ProcessEndDate = dr[13].ToString() != "" ? DateTime.Parse(dr[13].ToString()) : DateTime.MinValue,
                        Status = dr[14].ToString()!="" ? int.Parse(dr[14].ToString()) : 1,
                        PrecoUnitario = decimal.Parse(dr[19].ToString()),
                        Desconto = decimal.Parse(dr[20].ToString()),
                        ValorDesconto = decimal.Parse(dr[21].ToString()),
                        ValorUtente = decimal.Parse(dr[22].ToString()),
                        ValorEntidade = decimal.Parse(dr[23].ToString()),
                        ValorTotal = decimal.Parse(dr[24].ToString()),
                        Descricao = dr[26].ToString(), 
                    };
                    dto.LookupField1 = new StatusDAO().CustomerRequestServicesItemStatusList().Where(t => t.Codigo == dto.Status).SingleOrDefault().Descricao;
                    lista.Add(dto);
                    ordem++;
                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }
    }
}
