using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class ProfissaoDAO: IAcessoBD<ProfissaoDTO>
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public ProfissaoDTO Adicionar(ProfissaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROFISSAO_ADICIONAR";

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

        public ProfissaoDTO Alterar(ProfissaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROFISSAO_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);

                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
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

        public bool Eliminar(ProfissaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_PROFISSAO_EXCLUIR";

                
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

        public List<ProfissaoDTO> ObterPorFiltro(ProfissaoDTO dto)
        {
            List<ProfissaoDTO> listaProfissao; 
            try
            {
                
                BaseDados.ComandText = "stp_GER_PROFISSAO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaProfissao = new List<ProfissaoDTO>();
                while (dr.Read()) 
                {
                    dto = new ProfissaoDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();

                    dto.Estado = int.Parse(dr[3].ToString());

                    listaProfissao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new ProfissaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaProfissao = new List<ProfissaoDTO>();
                listaProfissao.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaProfissao;
        }

        public ProfissaoDTO ObterPorPK(ProfissaoDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_GER_PROFISSAO_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new ProfissaoDTO();
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
