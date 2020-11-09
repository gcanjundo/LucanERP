using System;
using System.Collections.Generic;
using Dominio.Comercial;
using MySql.Data.MySqlClient;



namespace DataAccessLayer.Comercial
{
    public class MeioExpedicaoDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public MeioExpedicaoDTO Adicionar(MeioExpedicaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_EXPEDICAO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
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

        public MeioExpedicaoDTO Alterar(MeioExpedicaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_EXPEDICAO_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);

                BaseDados.AddParameter("SITUACAO", dto.Estado);
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

        public bool Eliminar(MeioExpedicaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_EXPEDICAO_EXCLUIR";

                
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

        public List<MeioExpedicaoDTO> ObterPorFiltro(MeioExpedicaoDTO dto)
        {
            List<MeioExpedicaoDTO> listaCondicaoPagamento; 
            try
            {
                
                BaseDados.ComandText = "stp_COM_EXPEDICAO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaCondicaoPagamento = new List<MeioExpedicaoDTO>();
                while (dr.Read()) 
                {
                    dto = new MeioExpedicaoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString(); 
                    dto.Estado = int.Parse(dr[3].ToString());
                    dto.Status = dto.Estado;

                    listaCondicaoPagamento.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MeioExpedicaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaCondicaoPagamento = new List<MeioExpedicaoDTO>();
                listaCondicaoPagamento.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaCondicaoPagamento;
        }

        public MeioExpedicaoDTO ObterPorPK(MeioExpedicaoDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_COM_EXPEDICAO_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new MeioExpedicaoDTO();
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
