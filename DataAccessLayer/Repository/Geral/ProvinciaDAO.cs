using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccessLayer.Geral
{
    public class ProvinciaDAO
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public ProvinciaDTO Adicionar(ProvinciaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROVINCIA_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("PaisId", dto.PaisId);
                BaseDados.AddParameter("SITUACAO", dto.Estado);

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                
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

        public ProvinciaDTO Alterar(ProvinciaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROVINCIA_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("PaisId", dto.PaisId); 
                BaseDados.AddParameter("SITUACAO", dto.Estado); 
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                
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

        public bool Eliminar(ProvinciaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROVINCIA_EXCLUIR";


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

        public List<ProvinciaDTO> ObterPorFiltro(ProvinciaDTO dto)
        {
            List<ProvinciaDTO> Provincias;
            try
            {
                BaseDados.ComandText = "stp_GER_PROVINCIA_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("PaisId", dto.PaisId);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                Provincias = new List<ProvinciaDTO>();
                while (dr.Read())
                {
                    dto = new ProvinciaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.PaisId = int.Parse(dr[1].ToString());
                    dto.Descricao = dr[2].ToString();

                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.CountryName = dr[4].ToString();

                    Provincias.Add(dto);

                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                Provincias = new List<ProvinciaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                Provincias.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return Provincias;
        }

        public ProvinciaDTO ObterPorPK(ProvinciaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROVINCIA_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                if (dr.Read())
                {
                    dto = new ProvinciaDTO();
                    
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.PaisId = int.Parse(dr[1].ToString());
                    dto.Descricao = dr[2].ToString();
                    
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.CountryName = dr[4].ToString();
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
