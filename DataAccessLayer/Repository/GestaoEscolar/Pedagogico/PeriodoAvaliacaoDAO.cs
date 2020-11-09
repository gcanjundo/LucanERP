using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class PeriodoAvaliacaoDAO 
    {
        readonly ConexaoDB BaseDados;

        public PeriodoAvaliacaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PeriodoLancamentoDTO Adicionar(PeriodoLancamentoDTO dto) 
        {
           
            try
            {

                string _commandText= "spt_ACA_PERIODO_LANCAMENTO_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivoID);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);


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

         
        public PeriodoLancamentoDTO Apagar(PeriodoLancamentoDTO dto)
        {

            try
            {

                BaseDados.ComandText = "spt_ACA_PERIODO_LANCAMENTO_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        

        public List<PeriodoLancamentoDTO> ObterPorFiltro(PeriodoLancamentoDTO dto)
        {
            List<PeriodoLancamentoDTO> avaliacoes;
            
            try
            {
                BaseDados.ComandText = "spt_ACA_PERIODO_LANCAMENTO_OBTERPORFILTRO";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivoID);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                avaliacoes = new List<PeriodoLancamentoDTO>();
                while (dr.Read())
                {
                    dto = new PeriodoLancamentoDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.PeriodoLectivoID = int.Parse(dr[1]);
                    dto.Descricao = dr[2];
                    dto.Inicio = Convert.ToDateTime(dr[3]);
                    dto.Termino = Convert.ToDateTime(dr[4]);

                    avaliacoes.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PeriodoLancamentoDTO();
                avaliacoes = new List<PeriodoLancamentoDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                avaliacoes.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
                
            }
            return avaliacoes;

        }


    }
}
