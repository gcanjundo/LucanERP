using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class CategoriaVeiculoDAO:ConexaoDB 
    {
         

        public CategoriaDTO Adicionar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_CATEGORIA_ADICIONAR";
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

        public CategoriaDTO Alterar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_CATEGORIA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador);
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

        public CategoriaDTO Eliminar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_CATEGORIA_EXCLUIR";
                 
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

        public List<CategoriaDTO> ObterPorFiltro(CategoriaDTO dto)
        {
            List<CategoriaDTO> listaDepartamentos;
            try
            {
                ComandText = "stp_AUTO_CATEGORIA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                listaDepartamentos = new List<CategoriaDTO>();

                while(dr.Read())
                {
                   dto = new CategoriaDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaDepartamentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new CategoriaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDepartamentos = new List<CategoriaDTO>();
                listaDepartamentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaDepartamentos;
        }

        public CategoriaDTO ObterPorPK(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_AUTO_CATEGORIA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new CategoriaDTO();

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
