using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class PeriodoLancamentoDAO 
    {
        readonly ConexaoDB BaseDados;

        public PeriodoLancamentoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PeriodoLancamentoDTO Adicionar(PeriodoLancamentoDTO dto) 
        {
           
            try
            {

                string _commandText= "spt_ACA_PERIODO_LANCAMENTO_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo); 
                BaseDados.AddParameter("@PERIODO", dto.PeriodoLectivoID);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@EXAME_ID", dto.ExameID);
                BaseDados.AddParameter("@EXTRA", dto.IsPeriodoExtra ? 1 : 0);
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

        
        public PeriodoLancamentoDTO Excluir(PeriodoLancamentoDTO dto)
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
            List<PeriodoLancamentoDTO> periodos;
            
            try
            {
                BaseDados.ComandText = "spt_ACA_PERIODO_LANCAMENTO_OBTERPORFILTRO";

                BaseDados.AddParameter("@ANO", dto.AnoLectivo); 
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                periodos = new List<PeriodoLancamentoDTO>();
                while (dr.Read())
                {
                    dto = new PeriodoLancamentoDTO
                    {
                        Codigo = int.Parse(dr[0]),
                        PeriodoLectivoID = dr[1] != null && dr[1] != "" ? int.Parse(dr[1]) : -1,
                        Descricao = dr[2],
                        Inicio = Convert.ToDateTime(dr[3]),
                        Termino = Convert.ToDateTime(dr[4]),
                        ExameID = dr[4] != null && dr[5] != "" ? int.Parse(dr[5]) : -1,
                        IsPeriodoExtra = dr[4] != "1" ? false : true,
                         
                    };

                    periodos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PeriodoLancamentoDTO();
                periodos = new List<PeriodoLancamentoDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                periodos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
                
            }
            return periodos;

        }


    }
}
