using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class RamoDAO
    {
        readonly ConexaoDB BaseDados;

        public RamoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public RamoDTO Inserir(RamoDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_ACA_CURSO_RAMO_ADICIONAR";

                BaseDados.AddParameter("@DESCRICAO", dto.RamDescricao);
                BaseDados.AddParameter("@DESIGNACAO", dto.RamDesignacao);
                BaseDados.AddParameter("@CURSO", dto.RamCurso.Codigo);
                BaseDados.AddParameter("@INICIO", dto.RamInicio);
                BaseDados.AddParameter("@TERMINO", dto.RamTermino);
                BaseDados.AddParameter("@ESTADO", dto.RamStatus);

                dto.RamCodigo = BaseDados.ExecuteInsert();
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

        public RamoDTO Alterar(RamoDTO dto)
        {
            string _commandText= "stp_ACA_CURSO_RAMO_ALTERAR";

            try
            {

                BaseDados.AddParameter("@DESCRICAO", dto.RamDescricao);
                BaseDados.AddParameter("@DESIGNACAO", dto.RamDesignacao);
                BaseDados.AddParameter("@CURSO", dto.RamCurso.Codigo);
                BaseDados.AddParameter("@INICIO", dto.RamInicio);
                BaseDados.AddParameter("@CODIGO", dto.RamCodigo);
                BaseDados.AddParameter("@TERMINO", dto.RamTermino);
                BaseDados.AddParameter("@ESTADO", dto.RamStatus);

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

        public void Apagar(RamoDTO dto)
        {
           

            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_RAMO_EXCLUIR";
                BaseDados.AddParameter("@CODIGO", dto.RamCodigo);

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

        public RamoDTO ObterPorPK(RamoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_RAMO_OBTERPORPK";

            
            
           
            BaseDados.AddParameter("@CODIGO", dto.RamCodigo);
            if (dto.RamDesignacao != null)
            {
                BaseDados.AddParameter("@DESIGNACAO", dto.RamDesignacao.Trim());
            }
            else
            {
                BaseDados.AddParameter("@DESIGNACAO", String.Empty); 
            }

            if (dto.RamCurso != null)
            {
                BaseDados.AddParameter("@CURSO", dto.RamCurso.Codigo);
            }
            else 
            {
                BaseDados.AddParameter("@CURSO", -1);
            }
               
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new RamoDTO();
                while (dr.Read())
                {
                    dto.RamCodigo = Int32.Parse(dr["RAM_CODIGO"].ToString());
                    dto.RamDescricao = dr["RAM_DESCRICAO"].ToString();
                    CursoDTO dtoCurso = new CursoDTO(Int32.Parse(dr["RAM_CODIGO_CURSO"].ToString()));
                    
                    dto.RamDesignacao = dr["RAM_DESIGNACAO"].ToString();
                    dto.RamInicio = int.Parse(dr["RAM_INICIO"].ToString());
                    dto.RamTermino = int.Parse(dr["RAM_TERMINO"].ToString());
                    dto.RamStatus = int.Parse(dr["RAM_STATUS"].ToString());
                    CursoDAO daoCurso = new CursoDAO();
                    dtoCurso = daoCurso.ObterPorPK(dtoCurso);

                    if (dto.RamInicio > 0)
                    {
                        dto.DsInicio = dto.RamInicio.ToString() + "ª Classe";
                    }
                    else if(dto.RamInicio == 0)
                    {
                        dto.DsInicio = "Pré-Escolar";
                    }

                    if (dto.RamTermino > 0)
                    {
                        dto.DsTermino = dto.RamTermino.ToString() + "ª Classe";
                    }
                    else if (dto.RamTermino == 0)
                    {
                        dto.DsTermino = "Pré-Escolar";
                    }

                    dto.RamCurso = dtoCurso;
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

        public List<RamoDTO> ObterPorFiltro(RamoDTO dto)
        {


            List<RamoDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_RAMO_OBTERPORFILTRO";


                BaseDados.AddParameter("@DESCRICAO", dto.RamDescricao);
                BaseDados.AddParameter("@CURSO", dto.RamCurso.Codigo);
               
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<RamoDTO>();
                while (dr.Read())
                {
                    dto = new RamoDTO();
                    dto.RamCodigo = Int32.Parse(dr["RAM_CODIGO"].ToString());
                    dto.RamDescricao = dr["RAM_DESCRICAO"].ToString();
                    CursoDTO dtoCurso = new CursoDTO(Int32.Parse(dr["RAM_CODIGO_CURSO"].ToString()));

                    dto.RamDesignacao = dr["RAM_DESIGNACAO"].ToString();
                    dto.RamInicio = int.Parse(dr["RAM_INICIO"].ToString());
                    dto.RamTermino = int.Parse(dr["RAM_TERMINO"].ToString());
                    dto.RamStatus = int.Parse(dr["RAM_STATUS"].ToString());
                    CursoDAO daoCurso = new CursoDAO();
                    dtoCurso = daoCurso.ObterPorPK(dtoCurso);
                    dto.RamCurso = dtoCurso;

                    if (dto.RamInicio > 0)
                    {
                        dto.DsInicio = dto.RamInicio.ToString() + "ª Classe";
                    }
                    else if (dto.RamInicio == 0)
                    {
                        dto.DsInicio = "Pré-Escolar";
                    }

                    if (dto.RamTermino > 0)
                    {
                        dto.DsTermino = dto.RamTermino.ToString() + "ª Classe";
                    }
                    else if (dto.RamTermino == 0)
                    {
                        dto.DsTermino = "Pré-Escolar";
                    }

                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<RamoDTO>();
                dto = new RamoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
 

    }
}
