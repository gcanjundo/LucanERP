using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class CondutaDAO : ConexaoDB
    {


        public ProcedimentoDTO Adicionar(ProcedimentoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONDUTA_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Tipo);
                AddParameter("ATENDIMENTO", dto.Atendimento);

                ExecuteNonQuery();

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

        public ProcedimentoDTO Alterar(ProcedimentoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONDUTA_ALTERAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Tipo);
                AddParameter("ATENDIMENTO", dto.Atendimento);
                AddParameter("CODIGO", dto.Codigo);

                ExecuteNonQuery();
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

        public ProcedimentoDTO Eliminar(ProcedimentoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONDUTA_EXCLUIR";

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

        public List<ProcedimentoDTO> ObterPorFiltro(ProcedimentoDTO dto)
        {
            List<ProcedimentoDTO> listaProcedimentos;
            try
            {
                ComandText = "stp_CLI_CONDUTA_OBTERPORFILTRO";

                AddParameter("ATENDIMENTO", dto.Atendimento);

                MySqlDataReader dr = ExecuteReader();

                listaProcedimentos = new List<ProcedimentoDTO>();

                while (dr.Read())
                {
                    dto = new ProcedimentoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Tipo = dr[2].ToString();
                    dto.Atendimento = dr[3].ToString();

                    listaProcedimentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ProcedimentoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaProcedimentos = new List<ProcedimentoDTO>();
                listaProcedimentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaProcedimentos;
        }

        public ProcedimentoDTO ObterPorPK(ProcedimentoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_CONDUTA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new ProcedimentoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Tipo = dr[2].ToString();
                    dto.Atendimento = dr[3].ToString();
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
