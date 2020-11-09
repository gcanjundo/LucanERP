using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class GrupoSalarialDAO 
    {
        readonly ConexaoDB BaseDados;

        public GrupoSalarialDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public GrupoSalarialDTO Adicionar(GrupoSalarialDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_GRUPO_SALARIAL_ADICIONAR";
                
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("MOEDA", dto.Moeda);
                BaseDados.AddParameter("SALARIO", dto.SalarioBase);

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

        public GrupoSalarialDTO Alterar(GrupoSalarialDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_GRUPO_SALARIAL_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("MOEDA", dto.Moeda);
                BaseDados.AddParameter("SALARIO", dto.SalarioBase);
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = BaseDados.ExecuteNonQuery();
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

        public GrupoSalarialDTO Eliminar(GrupoSalarialDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_GRUPO_SALARIAL_EXCLUIR";
                 
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = BaseDados.ExecuteNonQuery();
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

        public List<GrupoSalarialDTO> ObterPorFiltro(GrupoSalarialDTO dto)
        {
            List<GrupoSalarialDTO> listaGrupos = new List<GrupoSalarialDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_GRUPO_SALARIAL_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@MOEDA", dto.Moeda);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                 

                while(dr.Read())
                {
                   dto = new GrupoSalarialDTO();


                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Moeda = int.Parse(dr[4].ToString());
                   dto.SalarioBase = decimal.Parse(dr[5].ToString());
                   if (String.IsNullOrEmpty(dr[3].ToString()))
                   {
                       dto.Estado = 1;
                   }
                   else
                   {
                       dto.Estado = 0;
                   }

                   dto.SiglaMoeda = dr[6].ToString();
                    
                   listaGrupos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaGrupos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaGrupos;
        }

        public GrupoSalarialDTO ObterPorPK(GrupoSalarialDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_GRUPO_SALARIAL_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new GrupoSalarialDTO();

                if(dr.Read())
                { 
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    
                    dto.Moeda = int.Parse(dr[4].ToString());
                    dto.SalarioBase = decimal.Parse(dr[5].ToString());
                    if (String.IsNullOrEmpty(dr[3].ToString()))
                    {
                        dto.Estado = 1;
                    }
                    else
                    {
                        dto.Estado = 0;
                    }

                    dto.SiglaMoeda = dr[6].ToString();
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
