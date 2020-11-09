using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class DoencaDAO: ConexaoDB 
    {
         

        public DoencaDTO Adicionar(DoencaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DOENCA_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);

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

        public DoencaDTO Alterar(DoencaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DOENCA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
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

        public DoencaDTO Eliminar(DoencaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DOENCA_EXCLUIR";
                 
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

        public List<DoencaDTO> ObterPorFiltro(DoencaDTO dto)
        {
            List<DoencaDTO> listaDoencas = new List<DoencaDTO>();
            try
            {
                ComandText = "stp_CLI_DOENCA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("PageIndex", dto.IndicePagina);
                AddParameter("PageSize", dto.RegistosPorPagina);
                ParametroRetorno();
                
                MySqlDataReader dr = ExecuteReader();
                int totalRegistos = 0;
                while(dr.Read())
                {
                   dto = new DoencaDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());
                   totalRegistos++;
                   listaDoencas.Add(dto);
                }

                listaDoencas[0].RegistosPorPagina = totalRegistos;
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

            return listaDoencas;
        }

        public DoencaDTO ObterPorPK(DoencaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DOENCA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("CID", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

                dto = new DoencaDTO();

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
