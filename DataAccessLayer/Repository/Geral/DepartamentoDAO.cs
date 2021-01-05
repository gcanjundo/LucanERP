using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class DepartamentoDAO:ConexaoDB 
    {
         

        public DepartamentoDTO Adicionar(DepartamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DEPARTAMENTO_ADICIONAR";
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

        public DepartamentoDTO Alterar(DepartamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DEPARTAMENTO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("UTILIZADOR", dto.Utilizador);
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

        public DepartamentoDTO Eliminar(DepartamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DEPARTAMENTO_EXCLUIR";
                 
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

        public List<DepartamentoDTO> ObterPorFiltro(DepartamentoDTO dto)
        {
            List<DepartamentoDTO> listaDepartamentos;
            try
            {
                ComandText = "stp_GER_DEPARTAMENTO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = ExecuteReader();

                listaDepartamentos = new List<DepartamentoDTO>();

                while(dr.Read())
                {
                   dto = new DepartamentoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString());

                   listaDepartamentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DepartamentoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDepartamentos = new List<DepartamentoDTO>();
                listaDepartamentos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaDepartamentos;
        }

        public DepartamentoDTO ObterPorPK(DepartamentoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_DEPARTAMENTO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new DepartamentoDTO();

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
