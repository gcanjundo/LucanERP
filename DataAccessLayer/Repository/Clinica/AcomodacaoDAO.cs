using System;
using System.Collections.Generic;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class AcomodacaoDAO: ConexaoDB 
    {
         

        public AcomodacaoDTO Adicionar(AcomodacaoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ACOMODACAO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("EXTENSAO", dto.Extensao);
                AddParameter("TIPO", dto.Tipo);

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

        public AcomodacaoDTO Alterar(AcomodacaoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ACOMODACAO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("EXTENSAO", dto.Extensao);
                AddParameter("TIPO", dto.Tipo);
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

        public AcomodacaoDTO Eliminar(AcomodacaoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ACOMODACAO_EXCLUIR";
                 
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

        public List<AcomodacaoDTO> ObterPorFiltro(AcomodacaoDTO dto)
        {
            List<AcomodacaoDTO> listaAcomodacaos = new List<AcomodacaoDTO>();
            try
            {
                ComandText = "stp_CLI_ACOMODACAO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new AcomodacaoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());
                   dto.Tipo = int.Parse(dr[4].ToString());
                   dto.DescricaoTipo = dr[6].ToString();
                   dto.Extensao = dr[5].ToString();

                   listaAcomodacaos.Add(dto);
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

            return listaAcomodacaos;
        }

        public AcomodacaoDTO ObterPorPK(AcomodacaoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ACOMODACAO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new AcomodacaoDTO();

                if (dr.Read())
                {

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Tipo = int.Parse(dr[4].ToString());
                    dto.Extensao = dr[5].ToString();
                    dto.DescricaoTipo = dr[6].ToString();

                   
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
