using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class FormaFarmaceuticaDAO :ConexaoDB
    {


        public FormaFarmaceuticaDTO Adicionar(FormaFarmaceuticaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_FARMACEUTICA_ADICIONAR";
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

        public FormaFarmaceuticaDTO Alterar(FormaFarmaceuticaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_FARMACEUTICA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
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

        public FormaFarmaceuticaDTO Eliminar(FormaFarmaceuticaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_FARMACEUTICA_EXCLUIR";

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

        public List<FormaFarmaceuticaDTO> ObterPorFiltro(FormaFarmaceuticaDTO dto)
        {
            List<FormaFarmaceuticaDTO> listaFormaFarmaceuticas;
            try
            {
                ComandText = "stp_GER_FORMA_FARMACEUTICA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                listaFormaFarmaceuticas = new List<FormaFarmaceuticaDTO>();

                while (dr.Read())
                {
                    dto = new FormaFarmaceuticaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    listaFormaFarmaceuticas.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new FormaFarmaceuticaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaFormaFarmaceuticas = new List<FormaFarmaceuticaDTO>();
                listaFormaFarmaceuticas.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaFormaFarmaceuticas;
        }

        public FormaFarmaceuticaDTO ObterPorPK(FormaFarmaceuticaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_FORMA_FARMACEUTICA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new FormaFarmaceuticaDTO();

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


