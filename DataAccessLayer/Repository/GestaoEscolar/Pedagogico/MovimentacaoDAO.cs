using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class MovimentacaoDAO: IAcessoBD<MovimentacaoDTO>
    {
        readonly ConexaoDB BaseDados;

        public MovimentacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public MovimentacaoDTO Adicionar(MovimentacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_MOVIMENTO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", 0);

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

        public MovimentacaoDTO Alterar(MovimentacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_MOVIMENTO_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);

                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }
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

        public bool Eliminar(MovimentacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_MOVIMENTO_EXCLUIR";

                
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

        public List<MovimentacaoDTO> ObterPorFiltro(MovimentacaoDTO dto)
        {
            List<MovimentacaoDTO> listaStatus; 
            try
            {
                
                BaseDados.ComandText = "stp_ACA_MOVIMENTO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaStatus = new List<MovimentacaoDTO>();
                while (dr.Read()) 
                {
                    dto = new MovimentacaoDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];

                    dto.Estado = int.Parse(dr[3]);

                    listaStatus.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MovimentacaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaStatus = new List<MovimentacaoDTO>();
                listaStatus.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaStatus;
        }

        public MovimentacaoDTO ObterPorPK(MovimentacaoDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_ACA_MOVIMENTO_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new MovimentacaoDTO();
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

                while (dr.Read())
                {
                    
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];
                    dto.Estado = int.Parse(dr[3]);

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
