using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class CoordenacaoDAO 
    {
        readonly ConexaoDB BaseDados;

        public CoordenacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public CoordenacaoDTO Adicionar(CoordenacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_COORDENACAO_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("NIVEL", dto.NivelEnsino);
                BaseDados.AddParameter("FILIAL", dto.Filial);

                BaseDados.ExecuteNonQuery();

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
        public bool Eliminar(CoordenacaoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_COORDENACAO_EXCLUIR";

                
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

        public List<CoordenacaoDTO> ObterPorFiltro(CoordenacaoDTO dto)
        {
            List<CoordenacaoDTO> listaCoordenacao; 
            try
            {
                
                BaseDados.ComandText = "stp_ACA_COORDENACAO_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("NIVEL", dto.Sigla);
                BaseDados.AddParameter("FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                listaCoordenacao = new List<CoordenacaoDTO>();
                while (dr.Read()) 
                {
                    dto = new CoordenacaoDTO
                    {
                        Codigo = int.Parse(dr[0]),
                        Descricao = dr[1],
                        Sigla = dr[2],

                        Estado = int.Parse(dr[3])
                    };

                    listaCoordenacao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new CoordenacaoDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                listaCoordenacao = new List<CoordenacaoDTO>
                {
                    dto
                };

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaCoordenacao;
        }

        public CoordenacaoDTO ObterPorPK(CoordenacaoDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_ACA_COORDENACAO_OBTERPORPK";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                dto = new CoordenacaoDTO();
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
