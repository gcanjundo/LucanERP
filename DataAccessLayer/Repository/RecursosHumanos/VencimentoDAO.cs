using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class VencimentoDAO 
    {
        readonly ConexaoDB BaseDados;

        public VencimentoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public ModoProcessamentoDTO Adicionar(ModoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VENCIMENTO_ADICIONAR";

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

        public ModoProcessamentoDTO Alterar(ModoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VENCIMENTO_ALTERAR";
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

        public ModoProcessamentoDTO Eliminar(ModoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VENCIMENTO_EXCLUIR";
                 
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

        public List<ModoProcessamentoDTO> ObterPorFiltro(ModoProcessamentoDTO dto)
        {
            List<ModoProcessamentoDTO> listaDepartamentos = new List<ModoProcessamentoDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_VENCIMENTO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new ModoProcessamentoDTO();

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

        public ModoProcessamentoDTO ObterPorPK(ModoProcessamentoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_VENCIMENTO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new ModoProcessamentoDTO();

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
