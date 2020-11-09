using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class DiagnosticoDAO: ConexaoDB 
    {
         

        public DiagnosticoDTO Adicionar(DiagnosticoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DIAGNOSTICO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("ATENDIMENTO", dto.Atendimento);
                AddParameter("DOENCA", dto.Doenca);
                AddParameter("ICPC", dto.Problema);
                if (dto.Inicio != DateTime.MinValue.ToShortDateString())
                {
                    AddParameter("INICIO", Convert.ToDateTime(dto.Inicio));
                }
                else
                {
                    AddParameter("INICIO", DBNull.Value);
                }
                AddParameter("IDADE", dto.IdadeInicio);
                AddParameter("MESES", dto.MesesInicio);
                AddParameter("SITUACAO", dto.Situacao);
                if (dto.Termino != DateTime.MinValue.ToShortDateString())
                {
                    AddParameter("TERMINO", Convert.ToDateTime(dto.Termino));
                }
                else
                {
                    AddParameter("TERMINO", DBNull.Value);
                }
                AddParameter("IDADE_TERMINO", dto.IdadeTermino);
                AddParameter("MESES_TERMINO", dto.MesesTermino);

                ExecuteNonQuery();

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

        public DiagnosticoDTO Alterar(DiagnosticoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DIAGNOSTICO_ALTERAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("ATENDIMENTO", dto.Atendimento);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("DOENCA", dto.Doenca);
                AddParameter("ICPC", dto.Problema);
                if (dto.Inicio != DateTime.MinValue.ToShortDateString())
                {
                    AddParameter("INICIO", Convert.ToDateTime(dto.Inicio));
                }
                else
                {
                    AddParameter("INICIO", DBNull.Value);
                }
                AddParameter("IDADE", dto.IdadeInicio);
                AddParameter("MESES", dto.MesesInicio);
                AddParameter("SITUACAO", dto.Situacao);
                if (dto.Termino != DateTime.MinValue.ToShortDateString())
                {
                    AddParameter("TERMINO", Convert.ToDateTime(dto.Termino));
                }
                else
                {
                    AddParameter("TERMINO", DBNull.Value);
                }
                AddParameter("IDADE_TERMINO", dto.IdadeTermino);
                AddParameter("MESES_TERMINO", dto.MesesTermino);

                ExecuteNonQuery();
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

        public DiagnosticoDTO Eliminar(DiagnosticoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DIAGNOSTICO_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = ExecuteNonQuery();
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

        public List<DiagnosticoDTO> ObterPorFiltro(DiagnosticoDTO dto)
        {
            List<DiagnosticoDTO> listaDiagnosticos;
            try
            {
                ComandText = "stp_CLI_DIAGNOSTICO_OBTERPORFILTRO";

                AddParameter("ATENDIMENTO", dto.Atendimento);
                AddParameter("TIPO", dto.Sigla);
                AddParameter("PACIENTE", dto.Paciente);
                AddParameter("SITUACAO", dto.Situacao);

                MySqlDataReader dr = ExecuteReader();

                listaDiagnosticos = new List<DiagnosticoDTO>();

                while(dr.Read())
                {
                   dto = new DiagnosticoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Atendimento = dr[1].ToString();
                   dto.Doenca = dr[2].ToString();
                   dto.Descricao = dr[3].ToString();
                   dto.Sigla = dr[4].ToString();
                  

                   listaDiagnosticos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new DiagnosticoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDiagnosticos = new List<DiagnosticoDTO>();
                listaDiagnosticos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaDiagnosticos;
        }

        public DiagnosticoDTO ObterPorPK(DiagnosticoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_DIAGNOSTICO_OBTERPORPK"; 

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new DiagnosticoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Atendimento = dr[1].ToString();
                    dto.Doenca = dr[2].ToString();
                    dto.Descricao = dr[3].ToString();
                    dto.Sigla = dr[4].ToString();
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
