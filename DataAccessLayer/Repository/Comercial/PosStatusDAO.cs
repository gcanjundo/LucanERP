
using Dominio.Comercial;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial
{
    public class PosStatusDAO : ConexaoDB
    {
        public PosStatusDTO Adicionar(PosStatusDTO dto)
        {
            try
            {
                ComandText = "stp_COM_POS_STATUS_OPEN_CLOSE";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@POS", dto.POS);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@SALDO_INICIAL", dto.SaldoInicial);
                AddParameter("@TURNO", dto.Turno);
                AddParameter("@ABERTURA", dto.Abertura);
                AddParameter("@SALDO_FINAL", dto.SaldoFinal);
                AddParameter("@IP", dto.IP);
                AddParameter("@FILIAL", dto.Filial);
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


        public decimal SaldoDia(PosStatusDTO dto)
        {
            decimal saldo = 0;
            try
            {

                ComandText = "stp_COM_POS_SALDO_DIA";

                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@DATA_ANTERIOR", DateTime.Today);

                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    saldo = decimal.Parse(dr[0].ToString());
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

            return saldo;
        }
        public List<PosStatusDTO> ObterTodasSessoesPorFilial(PosStatusDTO dto)
        {
            List<PosStatusDTO> lista;
            try
            {
                ComandText = "stp_COM_POS_STATUS_OBTERABERTAS";
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                /*AddParameter("@POS_ID", dto.POS);
                AddParameter("@DATA_ABERTURA", dto.Data == DateTime.MinValue ?(object)DBNull.Value : dto.Data);
                */
                MySqlDataReader dr = ExecuteReader();

                lista = new List<PosStatusDTO>();
                
                while (dr.Read())
                {
                    dto = new PosStatusDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        POS = int.Parse(dr[1].ToString()),
                        DescricaoPos = dr[2].ToString(),
                        Utilizador = dr[3].ToString(),
                        Data = Convert.ToDateTime(dr[4].ToString()),
                        Abertura = Convert.ToDateTime(dr[5].ToString()),
                        SaldoInicial = decimal.Parse(dr[6].ToString()),
                        SaldoFinal = decimal.Parse(dr[7].ToString()),
                        Fecho = dr[8].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[8].ToString()),
                        IP = dr[9].ToString(),
                        ValorSessao = decimal.Parse(dr[10].ToString())
                    };

                    if (dr[8].ToString() == "")
                    {
                        dto.Situacao = "<span class='label-default label label-warning text-center'>ABERTO</label>";
                    }
                    else
                    {
                        dto.Situacao = "<span class='label-default label label-success text-center'>FECHADO</label>";
                    }
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new PosStatusDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<PosStatusDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<PosStatusDTO> GetSessionRegisterList(PosStatusDTO dto)
        {
            List<PosStatusDTO> lista;
            try
            {
                ComandText = "stp_COM_POS_STATUS_SESSOES";
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@SESSION_DATE", dto.Data == DateTime.MinValue ? (Object)DBNull.Value : dto.Data);
                AddParameter("@POS_ID", dto.POS);
                AddParameter("@UTILIZADOR", dto.Utilizador == null || dto.Utilizador==string.Empty ? (object)DBNull.Value : dto.Utilizador);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<PosStatusDTO>();

                while (dr.Read())
                {
                    dto = new PosStatusDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()), 
                        DescricaoPos = dr[1].ToString(), 
                        Data = Convert.ToDateTime(dr[2].ToString()),
                        Abertura = Convert.ToDateTime(dr[3].ToString()),
                        SaldoInicial = decimal.Parse(dr[4].ToString()),
                        ValorSessao = decimal.Parse(dr[6].ToString()),
                        SaldoFinal = decimal.Parse(dr[4].ToString()) + decimal.Parse(dr[6].ToString())

                    };

                    if (dr[5].ToString() == "")
                    {
                        dto.Situacao = "ABERTA";
                    }
                    else
                    {
                        dto.Situacao = "FECHADA";
                    }

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new PosStatusDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<PosStatusDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public List<PosStatusDTO> GetMyOpenSessionRegisterList(PosStatusDTO dto)
        {
            List<PosStatusDTO> lista;
            try
            {
                ComandText = "stp_COM_POS_MYOPENPOS";
                AddParameter("@FILIAL", dto.Filial); 
                AddParameter("@POS_ID", dto.POS);
                AddParameter("@UTILIZADOR", dto.Utilizador == null || dto.Utilizador == string.Empty ? (object)DBNull.Value : dto.Utilizador);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<PosStatusDTO>();

                while (dr.Read())
                {
                    dto = new PosStatusDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        DescricaoPos = dr[2].ToString(),
                        Data = Convert.ToDateTime(dr[4].ToString()),
                        Abertura = Convert.ToDateTime(dr[5].ToString()),
                        SaldoInicial = decimal.Parse(dr[6].ToString()),
                        TurnoBegin = DateTime.Parse(dr[11].ToString()),
                        TurnoEnd = DateTime.Parse(dr[12].ToString()),
                        Turno = int.Parse(dr[13].ToString())

                    };

                    dto.TurnoBegin = DateTime.Parse(dto.Data.ToShortDateString() +" "+ dto.TurnoBegin.ToShortTimeString());
                    dto.TurnoEnd = DateTime.Parse(dto.Data.ToShortDateString() +" "+ dto.TurnoEnd.ToShortTimeString());

                    if((dto.TurnoBegin.Date == dto.TurnoEnd.Date) && (dto.TurnoEnd.TimeOfDay < dto.TurnoBegin.TimeOfDay))
                    {
                        dto.TurnoEnd = dto.TurnoEnd.AddDays(1);
                    }

                    lista.Add(dto);
                    break;
                }

            }
            catch (Exception ex)
            {
                dto = new PosStatusDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<PosStatusDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public PosStatusDTO ObterPorPK(PosStatusDTO dto)
        {
             
            try
            {
                ComandText = "stp_COM_POS_STATUS_OBTERPORPK";

                AddParameter("@UTILIZADOR", dto.Utilizador); 


                MySqlDataReader dr = ExecuteReader();

                dto = new PosStatusDTO(); 

                if(dr.Read())
                {
                   
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.POS = int.Parse(dr[1].ToString());
                    dto.Utilizador = dr[2].ToString();
                    dto.Data = Convert.ToDateTime(dr[3].ToString());
                    dto.SaldoInicial = decimal.Parse(dr[4].ToString());
                    dto.Turno = int.Parse(dr[5].ToString());
                    dto.Abertura = Convert.ToDateTime(dr[6].ToString()); 
                    dto.SaldoFinal = decimal.Parse(dr[7].ToString() == "" ? "0" : dr[7].ToString());
                    dto.Fecho = dr[8].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dr[8].ToString());
                    dto.IP = dr[9].ToString();
                    dto.Filial = dr[10].ToString(); 
                    dto.DescricaoPos = dr[11].ToString();  
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

            return dto;
        }

        public List<PosStatusDTO> ObterPorFiltro(PosStatusDTO dto)
        {
            List<PosStatusDTO> lista;
            try
            {
                ComandText = "stp_COM_POS_STATUS_OBTERPORFILTRO";
                AddParameter("@FILIAL", dto.Filial); 

                MySqlDataReader dr = ExecuteReader();

                lista = new List<PosStatusDTO>();

                while (dr.Read())
                {
                    dto = new PosStatusDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.POS = int.Parse(dr[1].ToString());
                    dto.DescricaoPos = dr[2].ToString();
                    dto.Utilizador = dr[3].ToString();
                    dto.Data = Convert.ToDateTime(dr[4].ToString());
                    dto.Abertura = Convert.ToDateTime(dr[5].ToString());
                    dto.SaldoInicial = decimal.Parse(dr[6].ToString());
                    dto.SaldoFinal = decimal.Parse(dr[7].ToString());
                    dto.IP = dr[9].ToString();
                    if (dr["POS_STA_FECHO"].ToString() != null && dr["POS_STA_FECHO"].ToString() != "")
                    {
                        dto.Fecho = DateTime.Parse(dr["POS_STA_FECHO"].ToString());
                    }

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new PosStatusDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<PosStatusDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }
    }
}
