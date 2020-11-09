using Dominio.Geral;
using Dominio.Oficina;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Oficina
{
    public class MarcacaoDAO : ConexaoDB
    {
        public MarcacaoDTO Adicionar(MarcacaoDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_MARCACAO_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@CHECKIN_DATE", dto.CheckInDate);
                AddParameter("CAR_ID", dto.VehicleID);
                AddParameter("DOCUMENT_ID", dto.DocumentID);
                AddParameter("WORK_ID", dto.WorkOrderID);
                AddParameter("@ENTITY_ID", dto.Entidade);
                AddParameter("@FREE_NOTES", dto.FreeNotes);
                AddParameter("@CANCEL_NOTES", dto.CancelNotes);
                AddParameter("@ESTADO", dto.Status);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@CHECKIN_BRANCH_ID", dto.Filial);
                AddParameter("@BOOKING_BRANCH_ID", dto.SerieID);

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


        public MarcacaoDTO Excluir(MarcacaoDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_MARCACAO_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo); 
                AddParameter("@CANCEL_NOTES", dto.CancelNotes);
                AddParameter("@ESTADO", dto.Status);
                AddParameter("@UTILIZADOR", dto.Utilizador);

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
            }

            return dto;
        }

        public List<MarcacaoDTO> ObterPorFiltro(MarcacaoDTO dto)
        {
            List<MarcacaoDTO> lista = new List<MarcacaoDTO>();
            try
            {

                ComandText = "stp_GER_AUTO_MARCACAO_OBTERPORFILTRO";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("DE", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value: dto.LookupDate1);
                AddParameter("ATE", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2);
                AddParameter("@MATRICULA", dto.Matricula);
                AddParameter("@CAR_ID", dto.VehicleID); 
                AddParameter("@ENTIDADE", dto.DesignacaoEntidade== null ? string.Empty : dto.DesignacaoEntidade);
                AddParameter("@ENTIDADE_ID", dto.Entidade);
                AddParameter("@PHONE_NUMBER", dto.CompanyPhone);
                AddParameter("@ESTADO", dto.Status);
                AddParameter("@DOCUMENT_ID", dto.DocumentID);
                AddParameter("@NUMBER", dto.DocumentNumber ?? "-1");
                AddParameter("@UTILIZADOR", dto.Utilizador);


                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new MarcacaoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.CheckInDate = DateTime.Parse(dr[1].ToString());
                    dto.VehicleID = int.Parse(dr[2].ToString());
                    dto.DocumentID = int.Parse(dr[3].ToString());
                    dto.WorkOrderID = int.Parse(dr[4].ToString());
                    dto.BillingEntityID = int.Parse(dr[5].ToString());
                    dto.FreeNotes = dr[6].ToString();
                    dto.CancelNotes = dr[7].ToString();
                    dto.Status = int.Parse(dr[8].ToString());
                    dto.CreatedBy = dr[11].ToString();
                    dto.CreatedDate = DateTime.Parse(dr[12].ToString());
                    dto.UpdatedBy = dr[13].ToString();
                    dto.UpdatedDate = DateTime.Parse(dr[14].ToString());
                    dto.CancelledBy = dr[15].ToString();
                    dto.CancelledDate = dr[16].ToString()!="" ? DateTime.Parse(dr[16].ToString()) : DateTime.MinValue; 
                    dto.BillingEntityDesignation = dr[17].ToString();
                    dto.WorkDesgination = dr[18].ToString();
                    dto.Veiculo = new VeiculoDTO(dr[19].ToString());
                    dto.DesignacaoEntidade = dr[20].ToString();
                    dto.CompanyPhone = dr[21].ToString();
                    dto.Email = dr[22].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MarcacaoDTO();
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
