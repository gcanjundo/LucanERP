using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class GrauParentescoDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public GrauParentescoDTO Adicionar(GrauParentescoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_GRAU_PARENTECO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", 0);
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

        public GrauParentescoDTO Alterar(GrauParentescoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_GRAU_PARENTECO_ALTERAR";

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

        public bool Eliminar(GrauParentescoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_GRAU_PARENTECO_EXCLUIR";

                
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

        public List<GrauParentescoDTO> ObterPorFiltro(GrauParentescoDTO dto)
        {
            List<GrauParentescoDTO> listaGrauParentesco; 
            try
            {
                
                BaseDados.ComandText = "stp_GER_GRAU_PARENTECO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaGrauParentesco = new List<GrauParentescoDTO>();
                while (dr.Read()) 
                {
                    dto = new GrauParentescoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();

                    dto.Estado = int.Parse(dr[3].ToString());

                    listaGrauParentesco.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new GrauParentescoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaGrauParentesco = new List<GrauParentescoDTO>();
                listaGrauParentesco.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaGrauParentesco;
        }

        public GrauParentescoDTO ObterPorPK(GrauParentescoDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_GER_GRAU_PARENTECO_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new GrauParentescoDTO();
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

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
                BaseDados.FecharConexao();
            }

            return dto;
        }
    }
}
