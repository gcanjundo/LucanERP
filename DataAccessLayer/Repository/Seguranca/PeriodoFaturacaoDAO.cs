using Dominio.Seguranca;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace DataAccessLayer.Seguranca
{
    public class PeriodoFaturacaoDAO:ConexaoDB
    {
        public AnoFaturacaoDTO Adicionar(AnoFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_ANO_FATURACAO_ADICIONAR";
                AddParameter("ANO", dto.Ano);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("INICIO", dto.Inicio);
                AddParameter("TERMINO", dto.Termino);
                AddParameter("SITUACAO", dto.Actived == true ? 1 : 0);
                AddParameter("@UTILIZADOR", dto.Utilizador);

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

        
        public AnoFaturacaoDTO Eliminar(AnoFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_ANO_FATURACAO_EXCLUIR";
                 
                AddParameter("ANO", dto.Ano);
                AddParameter("FILIAL", dto.Filial);

                  
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

        public List<AnoFaturacaoDTO> ObterPorFiltro(AnoFaturacaoDTO dto)
        {
            List<AnoFaturacaoDTO> lista;
             
            try
            {
                ComandText = "stp_SIS_ANO_FATURACAO_OBTERPORFILTRO";

                AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<AnoFaturacaoDTO>();

                while (dr.Read())
                {
                    dto = new AnoFaturacaoDTO();

                    dto.Ano = int.Parse(dr[0].ToString());
                    dto.Filial = dr[1].ToString();
                    dto.Descricao = dr[0].ToString();
                    dto.Inicio = DateTime.Parse(dr[3].ToString());
                    dto.Termino = DateTime.Parse(dr[4].ToString());
                    dto.Actived = dr[6].ToString() == "1" ? true : false;

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new AnoFaturacaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<AnoFaturacaoDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public AnoFaturacaoDTO ObterPorPK(AnoFaturacaoDTO dto)
        {
            try
            {
                ComandText = "stp_SIS_ANO_FATURACAO_OBTERPORPK";

                AddParameter("ANO", dto.Ano);
                AddParameter("FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                dto = new AnoFaturacaoDTO();

                if (dr.Read())
                {
                    dto.Ano = int.Parse(dr[0].ToString());
                    dto.Filial = dr[1].ToString();
                    dto.Descricao = dr[2].ToString();
                    dto.Inicio = DateTime.Parse(dr[3].ToString());
                    dto.Termino = DateTime.Parse(dr[4].ToString());
                    dto.Actived = dr[6].ToString() == "1" ? true : false;  
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
