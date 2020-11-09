using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccessLayer.Geral
{
    public class MoedaDAO 
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public MoedaDTO Adicionar(MoedaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_FIN_MOEDA_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao.ToUpper());
                BaseDados.AddParameter("SIGLA", dto.Sigla.ToUpper());

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

        public MoedaDTO Alterar(MoedaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_FIN_MOEDA_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao.ToUpper());
                BaseDados.AddParameter("SIGLA", dto.Sigla.ToUpper());
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

        public bool Eliminar(MoedaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_FIN_MOEDA_EXCLUIR";


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

        public List<MoedaDTO> ObterPorFiltro(MoedaDTO dto)
        {
            List<MoedaDTO> Moedas = new List<MoedaDTO>();
            try
            {
                BaseDados.ComandText = "stp_FIN_MOEDA_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);


                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MoedaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[2].ToString().ToUpper() + " - " + dr[1].ToString().ToUpper();
                    dto.Sigla = dr[2].ToString();



                    Moedas.Add(dto);
                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto = new MoedaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                Moedas = new List<MoedaDTO>();
                Moedas.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return Moedas;
        }

        public MoedaDTO ObterPorPK(MoedaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_FIN_MOEDA_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                if (dr.Read())
                {
                    dto = new MoedaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString().ToUpper();
                    dto.Sigla = dr[2].ToString().ToUpper();
                    dto.Sucesso = true;
                }

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
