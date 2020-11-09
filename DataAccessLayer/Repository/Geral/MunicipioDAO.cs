using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccessLayer.Geral
{
    public class MunicipioDAO:IAcessoBD<MunicipioDTO>
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public MunicipioDTO Adicionar(MunicipioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MUNICIPIO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("PROVINCIA", dto.Provincia);
                BaseDados.AddParameter("SITUACAO", 1);
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

        public MunicipioDTO Alterar(MunicipioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MUNICIPIO_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("PROVINCIA", dto.Provincia);

                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }

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

        public bool Eliminar(MunicipioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MUNICIPIO_EXCLUIR";


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

        public List<MunicipioDTO> ObterPorFiltro(MunicipioDTO dto)
        {
            List<MunicipioDTO> Municipios;
            try
            {
                BaseDados.ComandText = "stp_GER_MUNICIPIO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("PROVINCIA", dto.Provincia);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                Municipios = new List<MunicipioDTO>();
                while (dr.Read())
                {
                    dto = new MunicipioDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Provincia = int.Parse(dr[1].ToString());
                    dto.Descricao = dr[2].ToString();
                     dto.Estado = int.Parse(dr[3].ToString()); 
                    dto.NomeProvincia = dr[4].ToString();

                    Municipios.Add(dto);

                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                Municipios = new List<MunicipioDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                Municipios.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return Municipios;
        }

        public MunicipioDTO ObterPorPK(MunicipioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MUNICIPIO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                if (dr.Read())
                {
                    dto = new MunicipioDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Provincia = int.Parse(dr[1].ToString());
                    dto.Descricao = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.NomeProvincia = dr[4].ToString();
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
