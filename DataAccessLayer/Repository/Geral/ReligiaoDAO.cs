using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class ReligiaoDAO:ConexaoDB 
    {
         

        public ReligiaoDTO Adicionar(ReligiaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RELIGIAO_ADICIONAR";
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
                dto.MensagemErro =ex.Message.Replace("'", "");
            }
            finally 
            {
                FecharConexao();
            }

            return dto;
        }

        public ReligiaoDTO Alterar(ReligiaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RELIGIAO_ALTERAR";
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
                dto.MensagemErro =ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public ReligiaoDTO Eliminar(ReligiaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RELIGIAO_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro =ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<ReligiaoDTO> ObterPorFiltro(ReligiaoDTO dto)
        {
            List<ReligiaoDTO> listaReligiaos;
            try
            {
                ComandText = "stp_GER_RELIGIAO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = ExecuteReader();

                listaReligiaos = new List<ReligiaoDTO>();

                while(dr.Read())
                {
                   dto = new ReligiaoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaReligiaos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ReligiaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro =ex.Message.Replace("'", "");
                listaReligiaos = new List<ReligiaoDTO>();
                listaReligiaos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaReligiaos;
        }

        public ReligiaoDTO ObterPorPK(ReligiaoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_RELIGIAO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new ReligiaoDTO();

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
                dto.MensagemErro =ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }
    }
}
