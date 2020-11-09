using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class PrioridadeActividadeDAO :ConexaoDB
    {


        public PrioridadeActividadeDTO Adicionar(PrioridadeActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_PRIORIDADE_ACTIVIDADE_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador); 
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

        public PrioridadeActividadeDTO Alterar(PrioridadeActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_PRIORIDADE_ACTIVIDADE_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@TIPO", dto.Operacao);

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

        public PrioridadeActividadeDTO Eliminar(PrioridadeActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_PRIORIDADE_ACTIVIDADE_EXCLUIR";

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

        public List<PrioridadeActividadeDTO> ObterPorFiltro(PrioridadeActividadeDTO dto)
        {
            List<PrioridadeActividadeDTO> listaTipoContactos;
            try
            {
                ComandText = "stp_GER_PRIORIDADE_ACTIVIDADE_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

                listaTipoContactos = new List<PrioridadeActividadeDTO>();

                while (dr.Read())
                {
                    dto = new PrioridadeActividadeDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    listaTipoContactos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new PrioridadeActividadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaTipoContactos = new List<PrioridadeActividadeDTO>();
                listaTipoContactos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaTipoContactos;
        }

        public PrioridadeActividadeDTO ObterPorPK(PrioridadeActividadeDTO dto)
        {
            try
            {
                ComandText = "stp_GER_PRIORIDADE_ACTIVIDADE_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new PrioridadeActividadeDTO();

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
