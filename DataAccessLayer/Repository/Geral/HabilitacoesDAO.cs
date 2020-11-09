using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class HabilitacoesDAO: IAcessoBD<HabilitacoesDTO>
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public HabilitacoesDTO Adicionar(HabilitacoesDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_HABILITACOES_ADICIONAR";

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

        public HabilitacoesDTO Alterar(HabilitacoesDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_HABILITACOES_ALTERAR";

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

        public bool Eliminar(HabilitacoesDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_HABILITACOES_EXCLUIR";

                
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

        public List<HabilitacoesDTO> ObterPorFiltro(HabilitacoesDTO dto)
        {
            List<HabilitacoesDTO> listaHabilitacoes; 
            try
            {
                
                BaseDados.ComandText = "stp_GER_HABILITACOES_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaHabilitacoes = new List<HabilitacoesDTO>();
                while (dr.Read()) 
                {
                    dto = new HabilitacoesDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();

                    dto.Estado = int.Parse(dr[3].ToString());

                    listaHabilitacoes.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new HabilitacoesDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaHabilitacoes = new List<HabilitacoesDTO>();
                listaHabilitacoes.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaHabilitacoes;
        }

        public HabilitacoesDTO ObterPorPK(HabilitacoesDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_GER_HABILITACOES_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new HabilitacoesDTO();
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
