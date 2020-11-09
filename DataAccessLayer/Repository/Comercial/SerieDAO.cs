using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dominio.Comercial;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Comercial
{
    public class SerieDAO 
    {
        readonly ConexaoDB BaseDados;

        public SerieDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public SerieDTO Adicionar(SerieDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_SERIE_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("ANO", dto.Ano);
                BaseDados.AddParameter("FILIAL", dto.Filial);
                BaseDados.AddParameter("INICIO", dto.Inicio);
                BaseDados.AddParameter("TERMINO", dto.Termino);
                BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("NUMERACAO", dto.Numeracao);
                BaseDados.AddParameter("NRO_COPIAS", dto.Copias);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("FREE_LANCAMENTO", dto.AllowFreeDocument);

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

        public SerieDTO Alterar(SerieDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_SERIE_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("ANO", dto.Ano);
                BaseDados.AddParameter("INICIO", dto.Inicio);
                BaseDados.AddParameter("TERMINO", dto.Termino);
                BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("NUMERACAO", dto.Numeracao);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("NRO_COPIAS", dto.Copias);
                BaseDados.AddParameter("FILIAL", dto.Filial);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("FREE_LANCAMENTO", dto.AllowFreeDocument);

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

        public bool Eliminar(SerieDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_COM_SERIE_EXCLUIR";

                
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

        public List<SerieDTO> ObterPorFiltro(SerieDTO dto)
        {
            List<SerieDTO> series; 
            try
            {
                
                BaseDados.ComandText = "stp_COM_SERIE_OBTERPORFILTRO";
                
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@ESTADO", dto.Estado);
                BaseDados.AddParameter("@ANO", dto.Ano);
                BaseDados.AddParameter("@FILIAL", dto.Filial==null ? "-1" : dto.Filial);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                series = new List<SerieDTO>();
                while (dr.Read()) 
                {
                    dto = new SerieDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Numeracao = int.Parse(dr[2].ToString());
                    dto.Termino = DateTime.Parse(dr[3].ToString());
                    dto.Documento = int.Parse(dr[4].ToString());
                    dto.Filial = dr[5].ToString();
                    dto.Entidade = int.Parse(dr[6].ToString());
                    dto.Ano = int.Parse(dr[7].ToString());
                    dto.TituloDocumento = dr[8].ToString();
                    dto.Status = int.Parse(dr[9].ToString()); 
                    dto.AllowFreeDocument = /*dr[10].ToString() != "1" ? false :*/ true;
                    dto.Inicio = DateTime.Parse(dr[10].ToString());
                    series.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new SerieDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                series = new List<SerieDTO>();
                series.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return series;
        }

        public SerieDTO ObterPorPK(SerieDTO dto)
        {
            try
            {  
                BaseDados.ComandText = "stp_COM_SERIE_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DOCUMENTO", dto.Documento);
                BaseDados.AddParameter("@ANO", dto.Ano);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                dto = new SerieDTO();
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

                if (dr.Read())
                {

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Ano = int.Parse(dr[1].ToString());
                    dto.Descricao = dr[4].ToString();
                    dto.Inicio = DateTime.Parse(dr[5].ToString());
                    dto.Termino = DateTime.Parse(dr[6].ToString());
                    dto.Documento = int.Parse(dr[7].ToString());
                    dto.Numeracao = int.Parse(dr[8].ToString());
                    dto.Copias = int.Parse(dr[11].ToString());
                    dto.AllowFreeDocument = dr[18].ToString() != "1" ? false : true;

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
