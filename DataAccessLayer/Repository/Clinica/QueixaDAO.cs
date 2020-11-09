using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class QueixaDAO: ConexaoDB 
    {
         

        public QueixaDTO Adicionar(QueixaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_QUEIXA_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);

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

        public QueixaDTO Alterar(QueixaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_QUEIXA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
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

        public QueixaDTO Eliminar(QueixaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_QUEIXA_EXCLUIR";
                 
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

        public List<QueixaDTO> ObterPorFiltro(QueixaDTO dto)
        {
            List<QueixaDTO> listaQueixas;
            try
            {
                ComandText = "stp_CLI_QUEIXA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                listaQueixas = new List<QueixaDTO>();

                while(dr.Read())
                {
                   dto = new QueixaDTO();

                   dto.Sigla = dr[0].ToString();
                   dto.Designacao = dr[1].ToString();
                   dto.CID = dr[1].ToString();
                   dto.Descricao = dr[1].ToString();
                   dto.Inclusao = dr[2].ToString();
                   dto.Exclusao = dr[1].ToString(); 

                   listaQueixas.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new QueixaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaQueixas = new List<QueixaDTO>();
                listaQueixas.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaQueixas;
        }

        public QueixaDTO ObterPorPK(QueixaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_QUEIXA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new QueixaDTO();

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
