using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccessLayer.Clinica
{
    public class PrioridadeDAO: IAcessoBD<PrioridadeDTO>
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public PrioridadeDTO Adicionar(PrioridadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_PRIORIDADE_ADICIONAR";

                BaseDados.AddParameter("TEMPO", dto.TempoEspera);

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                BaseDados.AddParameter("SIGLA", dto.Sigla);
               
                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }

                
                
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

        public PrioridadeDTO Alterar(PrioridadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_PRIORIDADE_ALTERAR";

                BaseDados.AddParameter("TEMPO", dto.TempoEspera);

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                BaseDados.AddParameter("SIGLA", dto.Sigla);

                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }

                

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
            return dto;
        }

        public bool Eliminar(PrioridadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_PRIORIDADE_EXCLUIR";


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

        public List<PrioridadeDTO> ObterPorFiltro(PrioridadeDTO dto)
        {
            List<PrioridadeDTO> Prioridades;
            try
            {
                BaseDados.ComandText = "stp_CLI_PRIORIDADE_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao); 

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                Prioridades = new List<PrioridadeDTO>();
                while (dr.Read())
                {
                    dto = new PrioridadeDTO();
                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.TempoEspera = int.Parse(dr[3].ToString());
                    dto.Estado = int.Parse(dr[4].ToString());
                    

                    Prioridades.Add(dto);

                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                Prioridades = new List<PrioridadeDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                Prioridades.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return Prioridades;
        }

        public PrioridadeDTO ObterPorPK(PrioridadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_PRIORIDADE_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                if (dr.Read())
                {
                    dto = new PrioridadeDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.TempoEspera = int.Parse(dr[3].ToString());
                    dto.Estado = int.Parse(dr[4].ToString());
                }
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
    }
}
