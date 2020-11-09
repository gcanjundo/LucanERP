using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class VinculoDAO 
    {
        readonly ConexaoDB BaseDados;

        public VinculoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public VinculoDTO Adicionar(VinculoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VINCULO_ADICIONAR";
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

        public VinculoDTO Alterar(VinculoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VINCULO_ALTERAR";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
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

        public VinculoDTO Eliminar(VinculoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VINCULO_EXCLUIR";
                 
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

        public List<VinculoDTO> ObterPorFiltro(VinculoDTO dto)
        {
            List<VinculoDTO> listaDepartamentos = new List<VinculoDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_VINCULO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new VinculoDTO();

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

        public VinculoDTO ObterPorPK(VinculoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VINCULO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new VinculoDTO();

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
