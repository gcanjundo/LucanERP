using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class TurnoDAO: IAcessoBD<TurnoDTO>
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public TurnoDTO Adicionar(TurnoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_TURNO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("START_DATE", dto.StartTime);
                BaseDados.AddParameter("END_DATE", dto.EndTime);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                dto.Codigo = BaseDados.ExecuteInsert();
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

        public TurnoDTO Alterar(TurnoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_TURNO_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla); 
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("START_DATE", dto.StartTime);
                BaseDados.AddParameter("END_DATE", dto.EndTime);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

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

        public bool Eliminar(TurnoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_TURNO_EXCLUIR";

                
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

        public List<TurnoDTO> ObterPorFiltro(TurnoDTO dto)
        {
            List<TurnoDTO> listaTurno = new List<TurnoDTO>();
            try
            {
                
                BaseDados.ComandText = "stp_GER_TURNO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao  ?? string.Empty);


                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read()) 
                {
                    dto = new TurnoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Sigla = dr[2].ToString(),
                        Estado = int.Parse(dr[3].ToString()),
                        StartTime = DateTime.Parse(dr[4].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr[4].ToString()),
                        EndTime = DateTime.Parse(dr[5].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr[5].ToString()),
                        Status = int.Parse(dr[3].ToString())
                    };

                    listaTurno.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new TurnoDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaTurno;
        }

        public TurnoDTO ObterPorPK(TurnoDTO dto)
        {
            var turno = new TurnoDTO();
            try
            {


                BaseDados.ComandText = "stp_GER_TURNO_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

                if (dr.Read())
                {  
                    turno.Codigo = int.Parse(dr[0].ToString());
                    turno.Descricao = dr[1].ToString();
                    turno.Sigla = dr[2].ToString();
                    turno.Estado = int.Parse(dr[3].ToString());
                    turno.StartTime = DateTime.Parse(dr[4].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr[4].ToString());
                    turno.EndTime = DateTime.Parse(dr[5].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr[5].ToString());

                }
            }
            catch (Exception ex)
            {
              
                turno.Sucesso = false;
                turno.MensagemErro = ex.Message.Replace("'", "");
                
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }
    }
}
