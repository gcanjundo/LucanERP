using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccessLayer.Geral
{
    public class PaisDAO:IAcessoBD<PaisDTO>
    {
        static ConexaoDB BaseDados = new ConexaoDB();

        public PaisDTO Adicionar(PaisDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PAIS_ADICIONAR";
                
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);

                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SIGLA", string.Empty);
                BaseDados.AddParameter("@MOEDA", -1);
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

        public PaisDTO Alterar(PaisDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PAIS_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade); 
                BaseDados.AddParameter("SITUACAO", dto.Estado); 
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@SIGLA", string.Empty);
                BaseDados.AddParameter("@MOEDA", -1);

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

        public bool Eliminar(PaisDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PAIS_EXCLUIR";

                 
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

        public List<PaisDTO> ObterPorFiltro(PaisDTO dto)
        {
            List<PaisDTO> Paises = new List<PaisDTO>();
            try
            {
                BaseDados.ComandText = "stp_GER_PAIS_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao ?? String.Empty);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read()) 
                {
                    dto = new PaisDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Nacionalidade = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Sigla = dr[4].ToString();
                    dto.Moeda = dr[5].ToString();

                    Paises.Add(dto);

                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                Paises.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return Paises;
        }

        public PaisDTO ObterPorPK(PaisDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PAIS_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                if (dr.Read())
                {
                    dto = new PaisDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Nacionalidade = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    //dto.Sigla = dr[4].ToString();
                    //dto.Moeda = dr[5].ToString();
                     

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
