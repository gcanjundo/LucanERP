using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class EstagioDAO
    {
        readonly ConexaoDB BaseDados;

        public EstagioDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public EstagioDTO Adicionar(EstagioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_ADICIONAR";


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@DESCRICAO", dto.Designacao);
                BaseDados.AddParameter("@DOCENTE", dto.Docente.Codigo);
                BaseDados.AddParameter("@ANO_CURRICULAR", dto.Ano.Codigo);
                BaseDados.AddParameter("@PAID", dto.IsPago);

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

        public void Excluir(EstagioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_EXCLUIR";


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
        }

        public List<EstagioDTO> ObterPorFiltro(EstagioDTO dto)
        {

            var lista = new List<EstagioDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ESTAGIO_OBTERPORFILTRO";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                foreach (var dr in reader)
                {
                    dto = new EstagioDTO();
                    dto.Codigo = int.Parse(dr["EST_CODIGO"].ToString());
                    dto.AnoLectivo = int.Parse(dr["EST_ANO_LECTIVO"].ToString());
                    dto.Filial = dr["EST_CODIGO_FILIAL"].ToString();
                    dto.Entidade = int.Parse(dr["EST_ENTIDADE_ID"].ToString());
                    dto.Inicio = DateTime.Parse(dr["EST_INICIO"].ToString());
                    dto.Termino = DateTime.Parse(dr["EST_TERMINO"].ToString());
                    dto.DeletedBy = dr["EST_DELETED_BY"].ToString();
                    dto.DeletedDate = dr["EST_DELETED_DATE"].ToString() == null ? DateTime.MinValue : DateTime.Parse(dr["EST_DELETED_DATE"].ToString());
                    dto.SocialName = dr["ENT_NOME_COMPLETO"].ToString();
                    dto.IsPago = dr["EST_PAID"].ToString() == "1" ? true : false;
                    dto.Ano = new AnoCurricularDTO(int.Parse(dr["EST_ANO_CURRICULAR_ID"].ToString()), new RamoDTO(int.Parse(dr["PLAN_CODIGO_RAMO"].ToString())), -1, "", "");
                    dto.Designacao = dr["EST_DESCRICAO"].ToString();
                    dto.Docente = new DocenteDTO(int.Parse(dr["EST_DOCENTE_RESPONSAVEL_ID"].ToString()));

                    lista.Add(dto);

                }

            }catch (Exception ex)
            {
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
