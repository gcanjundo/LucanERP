using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class StatusMatriculaDAO   
    {
        readonly ConexaoDB BaseDados;

        public StatusMatriculaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public StatusMatriculaDTO Adicionar(StatusMatriculaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_STATUS_ESCOLAR_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("MOVIMENTO", dto.Movimento);
                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }
                BaseDados.AddParameter("@TAXA", dto.Taxa);
                BaseDados.AddParameter("@MULTA", dto.Multa);
                
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

        public StatusMatriculaDTO Alterar(StatusMatriculaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_STATUS_ESCOLAR_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("MOVIMENTO", dto.Sigla);

                if (dto.Estado.Equals(true))
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@TAXA", dto.Taxa);
                BaseDados.AddParameter("@MULTA", dto.Multa);
                
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

        public bool Eliminar(StatusMatriculaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_STATUS_ESCOLAR_EXCLUIR";


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

        public List<StatusMatriculaDTO> ObterPorFiltro(StatusMatriculaDTO dto)
        {
            List<StatusMatriculaDTO> StatusMatriculas;
            try
            {
                BaseDados.ComandText = "stp_ACA_STATUS_ESCOLAR_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("MOVIMENTO", dto.Movimento);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                StatusMatriculas = new List<StatusMatriculaDTO>();
                while (dr.Read())
                {
                    dto = new StatusMatriculaDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Movimento = int.Parse(dr[1]);
                    dto.Descricao = dr[2];

                    dto.Estado = int.Parse(dr[3]);
                    dto.Taxa = int.Parse(dr[4]);
                    dto.Multa = int.Parse(dr[5]);
                    dto.NomeMovimento = dr[6];

                    StatusMatriculas.Add(dto);

                }
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                StatusMatriculas = new List<StatusMatriculaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                StatusMatriculas.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return StatusMatriculas;
        }

        public StatusMatriculaDTO ObterPorPK(StatusMatriculaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_STATUS_ESCOLAR_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new StatusMatriculaDTO();
                    
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Movimento = int.Parse(dr[1]);
                    dto.Descricao = dr[2];

                    dto.Estado = int.Parse(dr[3]);
                    dto.Taxa = int.Parse(dr[4]);
                    dto.Multa = int.Parse(dr[5]);
                    dto.NomeMovimento = dr[6];
                }
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

        public List<StatusMatriculaDTO> ObterSituacaoAcademica()
        {
            List<StatusMatriculaDTO> StatusAcademico;
            StatusMatriculaDTO dto;
            try
            {
                BaseDados.ComandText = "stp_ACA_SITUACAO_ACADEMICA_OBTERTODAS"; 


                MySqlDataReader dr = BaseDados.ExecuteReader();
                StatusAcademico = new List<StatusMatriculaDTO>();
                while (dr.Read())
                {
                    dto = new StatusMatriculaDTO();
                    dto.Codigo = int.Parse(dr[0]); 
                    dto.Descricao = dr[1];
                    dto.Sigla = dr[2];
                    dto.Estado = int.Parse(dr[3]);  
                    StatusAcademico.Add(dto); 
                } 
            }
            catch (Exception ex)
            {
                dto = new StatusMatriculaDTO();
                StatusAcademico = new List<StatusMatriculaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                StatusAcademico.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return StatusAcademico;

        }
    }
}
