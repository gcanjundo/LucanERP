using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class LaboratorioCategoriaExameDAO: ConexaoDB 
    {
         

        public CategoriaDTO Adicionar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_CATEGORIA_EXAME_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@CATEGORIA", dto.Categoria);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@FILIAL", dto.Filial);

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

         

        public CategoriaDTO Eliminar(CategoriaDTO dto)
        {
            try
            {
                ComandText = "stp_GER_CATEGORIA_EXCLUIR";
                 
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
            List<CategoriaDTO> listaCategorias = new List<CategoriaDTO>();
            try
            {
                ComandText = "stp_CLI_LABORATORIO_CATEGORIA_EXAME_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);
                AddParameter("@SIGLA", dto.Sigla ?? string.Empty); 
                if (!string.IsNullOrEmpty(dto.Categoria))
                {
                    AddParameter("@CATEGORIA", dto.Categoria);
                }
                else
                {
                    AddParameter("@CATEGORIA", -1);
                }
                AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
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
            }
            finally
            {
                FecharConexao();
            }

            return listaCategorias;
        }

        
    }
}
