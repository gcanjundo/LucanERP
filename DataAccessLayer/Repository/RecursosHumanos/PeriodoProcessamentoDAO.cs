using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class PeriodoProcessamentoDAO 
    {
        readonly ConexaoDB BaseDados;

        public PeriodoProcessamentoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PeriodoProcessamentoDTO Adicionar(PeriodoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_PERIODO_PROCESSAMENTO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);

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

        public PeriodoProcessamentoDTO Alterar(PeriodoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_PERIODO_PROCESSAMENTO_ALTERAR";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
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

        public PeriodoProcessamentoDTO Eliminar(PeriodoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_PERIODO_PROCESSAMENTO_EXCLUIR";
                 
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

        public List<PeriodoProcessamentoDTO> ObterPorFiltro(PeriodoProcessamentoDTO dto)
        {
            List<PeriodoProcessamentoDTO> listaDepartamentos = new List<PeriodoProcessamentoDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_PERIODO_PROCESSAMENTO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                

                while(dr.Read())
                {
                   dto = new PeriodoProcessamentoDTO();

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

        public PeriodoProcessamentoDTO ObterPorPK(PeriodoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_PERIODO_PROCESSAMENTO_OBTERPORFILTRO";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new PeriodoProcessamentoDTO();

                while(dr.Read())
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
