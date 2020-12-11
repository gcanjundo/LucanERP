using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class MarcaDAO: IAcessoBD<MarcaDTO>
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public MarcaDTO Adicionar(MarcaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MARCA_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("@ROOT", dto.Localizacao);

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

        public MarcaDTO Alterar(MarcaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MARCA_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ROOT", dto.Localizacao);

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

        public bool Eliminar(MarcaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_MARCA_EXCLUIR";

                
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

        public List<MarcaDTO> ObterPorFiltro(MarcaDTO dto)
        {
            List<MarcaDTO> listaMarca; 
            try
            {
                
                BaseDados.ComandText = "stp_GER_MARCA_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaMarca = new List<MarcaDTO>();
                while (dr.Read()) 
                {
                    dto = new MarcaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();

                    dto.Estado = int.Parse(dr[3].ToString());

                    listaMarca.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MarcaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaMarca = new List<MarcaDTO>();
                listaMarca.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaMarca;
        }

        public MarcaDTO ObterPorPK(MarcaDTO dto)
        {
            try
            { 
                BaseDados.ComandText = "stp_GER_MARCA_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new MarcaDTO();
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

        public List<MarcaDTO> ObterModelos(MarcaDTO dto)
        {
            List<MarcaDTO> listaMarca;
            try
            {

                BaseDados.ComandText = "stp_GER_MARCA_OBTERMODELOS";

                BaseDados.AddParameter("MARCA", dto.Codigo);
                BaseDados.AddParameter("DESCRICAO", dto.Descricao?? string.Empty);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaMarca = new List<MarcaDTO>();
                while (dr.Read())
                {
                    dto = new MarcaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString(); 
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Localizacao = dr[4].ToString();
                    listaMarca.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MarcaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaMarca = new List<MarcaDTO>();
                listaMarca.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaMarca;
        }
    }
}
