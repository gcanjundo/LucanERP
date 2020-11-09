using System;
using System.Collections.Generic;
using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class MotivoAusenciaDAO:ConexaoDB 
    {
         

        public MotivoDTO Adicionar(MotivoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_MOTIVO_ADMISSAO_DEMISSAO_ADICIONAR";
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

        public MotivoDTO Alterar(MotivoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_MOTIVO_ADMISSAO_DEMISSAO_ALTERAR";
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

        public MotivoDTO Eliminar(MotivoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_MOTIVO_ADMISSAO_DEMISSAO_EXCLUIR";
                 
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

        public List<MotivoDTO> ObterPorFiltro(MotivoDTO dto)
        {
            List<MotivoDTO> listaMotivos = new List<MotivoDTO>();
            try
            {
                ComandText = "stp_RH_MOTIVO_ADMISSAO_DEMISSAO_OBTERPORFILTRO";

                if (dto.Descricao == null)
                {
                    AddParameter("@DESCRICAO", String.Empty);
                }
                else 
                {
                    AddParameter("@DESCRICAO", dto.Descricao); 
                }
                AddParameter("@SIGLA", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new MotivoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaMotivos.Add(dto);
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

            return listaMotivos;
        }

        public MotivoDTO ObterPorPK(MotivoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_MOTIVO_ADMISSAO_DEMISSAO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new MotivoDTO();

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
