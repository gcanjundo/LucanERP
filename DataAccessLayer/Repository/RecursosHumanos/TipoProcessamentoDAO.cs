using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class TipoProcessamentoDAO 
    {
        readonly ConexaoDB BaseDados;

        public TipoProcessamentoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public TipoProcessamentoDTO Adicionar(TipoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_TIPO_PROCESSAMENTO_ADICIONAR";

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

        
        public TipoProcessamentoDTO Eliminar(TipoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_TIPO_PROCESSAMENTO_EXCLUIR";
                 
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = BaseDados.ExecuteNonQuery();
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

        public List<TipoProcessamentoDTO> ObterPorFiltro(TipoProcessamentoDTO dto)
        {
            List<TipoProcessamentoDTO> listaDepartamentos = new List<TipoProcessamentoDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_TIPO_PROCESSAMENTO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new TipoProcessamentoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaDepartamentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDepartamentos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaDepartamentos;
        }

        public TipoProcessamentoDTO ObterPorPK(TipoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_TIPO_PROCESSAMENTO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new TipoProcessamentoDTO();

                if(dr.Read())
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
