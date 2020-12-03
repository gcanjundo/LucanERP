using System;
using System.Collections.Generic;

using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class CategoriaDAO
    {
        private readonly ConexaoDB dataBase;
        public CategoriaDAO()
        {
            dataBase = new ConexaoDB();
        }
         

        public CategoriaDTO Adicionar(CategoriaDTO dto)
        {
            try
            {
                dataBase.ComandText = "stp_GER_CATEGORIA_ADICIONAR";

                dataBase.AddParameter("@CODIGO", dto.Codigo);
                dataBase.AddParameter("DESCRICAO", dto.Descricao);
                dataBase.AddParameter("SIGLA", dto.Sigla);
                dataBase.AddParameter("SITUACAO", dto.Estado);
                dataBase.AddParameter("@CATEGORIA", dto.Categoria);
                dataBase.AddParameter("@UTILIZADOR", dto.Utilizador);
                dataBase.AddParameter("@FILIAL", dto.Filial);

                dto.Codigo = dataBase.ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                dataBase.FecharConexao();
            }

            return dto;
        }

         

        public CategoriaDTO Eliminar(CategoriaDTO dto)
        {
            try
            {
                dataBase.ComandText = "stp_GER_CATEGORIA_EXCLUIR";
                 
                dataBase.AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = dataBase.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                dataBase.FecharConexao();
            }

            return dto;
        }

        public List<CategoriaDTO> ObterPorFiltro(CategoriaDTO dto)
        {
            List<CategoriaDTO> listaCategorias = new List<CategoriaDTO>();
            try
            {
                dataBase.ComandText = "stp_GER_CATEGORIA_OBTERPORFILTRO";

                dataBase.AddParameter("@DESCRICAO", dto.Descricao ?? string.Empty);
                dataBase.AddParameter("@SIGLA", dto.Sigla ?? string.Empty); 
                if (!string.IsNullOrEmpty(dto.Categoria))
                {
                    dataBase.AddParameter("@CATEGORIA", dto.Categoria);
                }
                else
                {
                    dataBase.AddParameter("@CATEGORIA", -1);
                }
                dataBase.AddParameter("@FILIAL", dto.Filial?? "2");

                MySqlDataReader dr = dataBase.ExecuteReader(); 

                while(dr.Read())
                {
                   dto = new CategoriaDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());
                   dto.Categoria = dr[4].ToString();
                   listaCategorias.Add(dto);
                }

            }
            catch (Exception ex)
            { 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaCategorias.Add(dto);
            }
            finally
            {
                dataBase.FecharConexao();
            }

            return listaCategorias;
        }

        public List<CategoriaDTO> ObterCategoriaClinicoProfissional()
        {
            List<CategoriaDTO> listaCategorias = new List<CategoriaDTO>();
            CategoriaDTO dto = new CategoriaDTO {Codigo = -1, Descricao = "-TODOS-", Sigla = "" };
            try
            {
                dataBase.ComandText = "stp_CLI_CATEGORIA_PROFISSIONAL_OBTERPORFILTRO";

                MySqlDataReader dr = dataBase.ExecuteReader();
                listaCategorias.Add(dto);
                while (dr.Read())
                {
                    dto = new CategoriaDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Sigla = dr[2].ToString(), 
                    };
                     
                        listaCategorias.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new CategoriaDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
            }
            finally
            {
                dataBase.FecharConexao();
            }

            return listaCategorias;
        }

        public List<CategoriaDTO> ObterComProdutos(bool onlyRest)
        {
            List<CategoriaDTO> listaCategorias = new List<CategoriaDTO>();
            CategoriaDTO dto =null;
            try
            {
                dataBase.ComandText = "stp_GER_CATEGORIA_OBTERCOMPRODUTOS";
                dataBase.AddParameter("ONLY_REST", onlyRest ? 1 : 0);
                MySqlDataReader dr = dataBase.ExecuteReader();

                while (dr.Read())
                {
                    dto = new CategoriaDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Sigla = dr[2].ToString(),
                        Estado = int.Parse(dr[3].ToString()),
                        Categoria = dr[4].ToString(),
                        Operacao = dr[5].ToString(),
                        IsArtigoRest = dr[6].ToString() == "1" ? true : false
                    };

                    if (!listaCategorias.Exists(t => t.Codigo == dto.Codigo))
                        listaCategorias.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new CategoriaDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
            }
            finally
            {
                dataBase.FecharConexao();
            }

            return listaCategorias;
        }

        public CategoriaDTO ObterPorPK(CategoriaDTO dto)
        {
            try
            {
                dataBase.ComandText = "stp_GER_CATEGORIA_OBTERPORPK";

                dataBase.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = dataBase.ExecuteReader();

                dto = new CategoriaDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Categoria = dr[4].ToString();
                   
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                dataBase.FecharConexao();
            }

            return dto;
        }
    }
}
