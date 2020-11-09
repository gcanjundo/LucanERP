using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class PeriodoLectivoDAO 
    {
        readonly ConexaoDB BaseDados;

        public PeriodoLectivoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PeriodoLectivoDTO Inserir(PeriodoLectivoDTO dto)
        {
            
            try
            {

                BaseDados.ComandText = "stp_ACA_PERIODO_LECTIVO_ADICIONAR";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@PERIODO", dto.Descricao);

                if (dto.Inicio != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@INICIO", dto.Inicio);
                }
                else
                {
                    BaseDados.AddParameter("@INICIO", DBNull.Value);
                }

                if (dto.Termino != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@TERMINO", dto.Termino);
                }
                else
                {
                    BaseDados.AddParameter("@TERMINO", DBNull.Value);
                }
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

        public PeriodoLectivoDTO Alterar(PeriodoLectivoDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_ACA_PERIODO_LECTIVO_ALTERAR";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@PERIODO", dto.Descricao);

                if (dto.Inicio != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@INICIO", dto.Inicio);
                }
                else
                {
                    BaseDados.AddParameter("@INICIO", DBNull.Value);
                }

                if (dto.Termino != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@TERMINO", dto.Termino);
                }
                else
                {
                    BaseDados.AddParameter("@TERMINO", DBNull.Value);
                }
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                
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

        public void Apagar(PeriodoLectivoDTO dto)
        {
          
            try
            {
                BaseDados.ComandText = "stp_ACA_PERIODO_LECTIVO_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
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

        }

        public PeriodoLectivoDTO ObterPorPK(PeriodoLectivoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_PERIODO_LECTIVO_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                if (dto.AnoLectivo > 0)
                {
                    BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                    BaseDados.AddParameter("@PERIODO", dto.Descricao);
                }
                else
                {
                    BaseDados.AddParameter("@ANO", -1);
                    BaseDados.AddParameter("@PERIODO", "");

                } 

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new PeriodoLectivoDTO();
                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["PER_CODIGO"].ToString());

                    dto.AnoLectivo = Int32.Parse(dr["PER_CODIGO_ANO_LECTIVO"].ToString());

                   dto.Descricao = dr["PER_DESCRICAO"].ToString();


                    dto.Inicio = DateTime.Parse(dr["PER_INICIO"].ToString());
                    dto.Termino = DateTime.Parse(dr["PER_TERMINO"].ToString());

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

        public List<PeriodoLectivoDTO> ObterPorFiltro(PeriodoLectivoDTO dto)
        {
            
            List<PeriodoLectivoDTO> lista = new List<PeriodoLectivoDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_PERIODO_LECTIVO_OBTERPORFILTRO";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@PERIODO", dto.Descricao);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PeriodoLectivoDTO();
                    dto.Codigo = Int32.Parse(dr["PER_CODIGO"].ToString());

                    dto.AnoLectivo = Int32.Parse(dr["PER_CODIGO_ANO_LECTIVO"].ToString());

                    dto.Descricao = dr["PER_DESCRICAO"].ToString();


                    dto.Inicio = DateTime.Parse(dr["PER_INICIO"].ToString());
                    dto.Termino = DateTime.Parse(dr["PER_TERMINO"].ToString());

                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<PeriodoLectivoDTO>(); 
                dto = new PeriodoLectivoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
