
using DataAccessLayer;
using Dominio.Comercial.Lavandaria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial.Lavandaria
{
    public class GeneroVestuarioDAO : ConexaoDB
    {

        public GeneroVestuarioDTO Adicionar(GeneroVestuarioDTO dto)
        {
            try
            {
                ComandText = "stp_LAV_GENERO_VESTUARIO_ADICIONAR";
                AddParameter("DESIGNACAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("UTILIZADOR", dto.Utilizador);
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

        public GeneroVestuarioDTO Alterar(GeneroVestuarioDTO dto)
        {
            try
            {
                ComandText = "stp_LAV_GENERO_VESTUARIO_ALTERAR";
                AddParameter("DESIGNACAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("UTILIZADOR", dto.Utilizador);
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

        public GeneroVestuarioDTO Eliminar(GeneroVestuarioDTO dto)
        {
            try
            {
                ComandText = "stp_LAV_GENERO_VESTUARIO_EXCLUIR";

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

        public List<GeneroVestuarioDTO> ObterPorFiltro(GeneroVestuarioDTO dto)
        {
            List<GeneroVestuarioDTO> listaReligiaos;
            try
            {
                ComandText = "stp_LAV_GENERO_VESTUARIO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = ExecuteReader();

                listaReligiaos = new List<GeneroVestuarioDTO>();

                while (dr.Read())
                {
                    dto = new GeneroVestuarioDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    listaReligiaos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new GeneroVestuarioDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaReligiaos = new List<GeneroVestuarioDTO>();
                listaReligiaos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaReligiaos;
        }

        public GeneroVestuarioDTO ObterPorPK(GeneroVestuarioDTO dto)
        {
            try
            {
                ComandText = "stp_LAV_GENERO_VESTUARIO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new GeneroVestuarioDTO();

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
