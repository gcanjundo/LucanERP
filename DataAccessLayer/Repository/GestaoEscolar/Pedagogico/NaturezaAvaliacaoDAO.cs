using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class NaturezaAvaliacaoDAO 
    {
        readonly ConexaoDB BaseDados;

        public NaturezaAvaliacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public NaturezaAvaliacaoDTO Adicionar(NaturezaAvaliacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_NATUREZA_AVALIACAO_ADICIONAR";
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

        public NaturezaAvaliacaoDTO Alterar(NaturezaAvaliacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_NATUREZA_AVALIACAO_ALTERAR";
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

        public NaturezaAvaliacaoDTO Eliminar(NaturezaAvaliacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_NATUREZA_AVALIACAO_EXCLUIR";
                 
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

        public List<NaturezaAvaliacaoDTO> ObterPorFiltro(NaturezaAvaliacaoDTO dto)
        {
            List<NaturezaAvaliacaoDTO> listaNaturezaAvaliacaos;
            try
            {
                BaseDados.ComandText = "stp_ACA_NATUREZA_AVALIACAO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("ABREVIATURA", dto.Sigla);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                listaNaturezaAvaliacaos = new List<NaturezaAvaliacaoDTO>();

                while (dr.Read())
                {
                   dto = new NaturezaAvaliacaoDTO();

                   dto.Codigo = int.Parse(dr[0]);
                   dto.Descricao = dr[1];
                   dto.Sigla = dr[3];
                   //dto.Estado = int.Parse(dr[3]);
                   dto.Tipo = dr[4];

                   listaNaturezaAvaliacaos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new NaturezaAvaliacaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaNaturezaAvaliacaos = new List<NaturezaAvaliacaoDTO>();
                listaNaturezaAvaliacaos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaNaturezaAvaliacaos;
        }

        public NaturezaAvaliacaoDTO ObterPorPK(NaturezaAvaliacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_NATUREZA_AVALIACAO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new NaturezaAvaliacaoDTO();

                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];
                    //dto.Estado = int.Parse(dr[3]);

                   
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
