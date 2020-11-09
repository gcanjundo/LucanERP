using Dominio.Clinica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Clinica
{
    public class LaboratorioAmostraExameDAO : ConexaoDB
    {
        public LaboratorioAmostraExameDTO Adicionar(LaboratorioAmostraExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_AMOSTRA_ADICIONAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("UTILIZADOR", dto.Utilizador);

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

        public LaboratorioAmostraExameDTO Alterar(LaboratorioAmostraExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_AMOSTRA_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo); 
                AddParameter("UTILIZADOR", dto.Utilizador);

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

        public LaboratorioAmostraExameDTO Eliminar(LaboratorioAmostraExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_AMOSTRA_EXCLUIR";

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

        public List<LaboratorioAmostraExameDTO> ObterPorFiltro(LaboratorioAmostraExameDTO dto)
        {
            List<LaboratorioAmostraExameDTO> lista = new List<LaboratorioAmostraExameDTO>();
            try
            {
                ComandText = "stp_CLI_LABORATORIO_AMOSTRA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao); 

                MySqlDataReader dr = ExecuteReader();
                int totalRegistos = 0;
                while (dr.Read())
                {
                    dto = new LaboratorioAmostraExameDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                    totalRegistos++;
                    lista.Add(dto);
                }

                lista[0].RegistosPorPagina = totalRegistos;
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

            return lista;
        }

        public LaboratorioAmostraExameDTO ObterPorPK(LaboratorioAmostraExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_AMOSTRA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo); 

                MySqlDataReader dr = ExecuteReader();

                dto = new LaboratorioAmostraExameDTO();

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
