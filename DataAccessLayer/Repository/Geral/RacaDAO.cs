using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class RacaDAO:ConexaoDB 
    {
         

        public RacaDTO Adicionar(RacaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RACA_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
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

        public RacaDTO Alterar(RacaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RACA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado); 
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("CODIGO", dto.Codigo);
               
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

        public RacaDTO Eliminar(RacaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RACA_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = ExecuteNonQuery();
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

        public List<RacaDTO> ObterPorFiltro(RacaDTO dto)
        {
            List<RacaDTO> listaRacas;
            try
            {
                ComandText = "stp_GER_RACA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                listaRacas = new List<RacaDTO>();

                while(dr.Read())
                {
                   dto = new RacaDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaRacas.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new RacaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaRacas = new List<RacaDTO>();
                listaRacas.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaRacas;
        }

        public RacaDTO ObterPorPK(RacaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RACA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new RacaDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                   
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
    }
}
