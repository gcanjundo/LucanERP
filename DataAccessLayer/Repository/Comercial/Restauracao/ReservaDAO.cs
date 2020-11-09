
using Dominio.Comercial.Restauracao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial.Restauracao
{
    public class ReservaDAO : ConexaoDB
    {

        public ReservaDTO Reservar(ReservaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_RESERVA_ADICIONAR";

                AddParameter("MESA_ID", dto.Mesa);
                AddParameter("CLIENTE", dto.DesignacaoEntidade); 
                AddParameter("CHECKIN", dto.DataInicio);
                AddParameter("PHONE", dto.CompanyPhone);
                AddParameter("EMAIL", dto.Email);
                AddParameter("OBS", dto.Notas);
                AddParameter("UTILIZADOR", dto.Utilizador);
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

        public void ActualizarReserva(ReservaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_RESERVA_ACTUALIZAR";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("STATUS_ID", dto.BookingStatus);
                AddParameter("UTILIZADOR", dto.Utilizador);

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

        public List<ReservaDTO> ObterReservasPorFiltro(ReservaDTO dto)
        {
            List<ReservaDTO> lista = new List<ReservaDTO>();
            try
            {

                ComandText = "stp_REST_RESERVA_OBTERPORFILTRO";

                AddParameter("CUSTOMER", dto.Descricao);
                AddParameter("DATA_INI", dto.LookupDate1);
                AddParameter("DATA_TERM", dto.LookupDate2);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ReservaDTO
                    {     
                        Codigo = int.Parse(dr[1].ToString()),
                        Mesa = dr[1].ToString(),
                        DesignacaoEntidade = dr[2].ToString(),
                        Descricao = dr[3].ToString(),
                        DataInicio = DateTime.Parse(dr[4].ToString()), 
                        Situacao = SetBookingStatus(dr[5].ToString()),
                        CompanyPhone = dr[6].ToString(),
                        Notas = dr[7].ToString(),
                        Email = dr[8].ToString(),
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

        private string SetBookingStatus(string pStatus)
        {
            if (pStatus == "E")
            {
                return "CANCELADA";
            }
            else if (pStatus == "A")
            {
                return "CONFIRMADA";
            }
            else if (pStatus == "F")
            {
                return "FECHADA";
            }
            else
            {
                return "";
            }
        }
    }
}
