using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class DocenteDisciplinaDAO
    {

        readonly ConexaoDB BaseDados;

        public DocenteDisciplinaDAO()
        {
            BaseDados = new ConexaoDB();

        }
        public DocenteDisciplinaDTO Adicionar(DocenteDisciplinaDTO dto) 
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_DISCIPLINA_ADICIONAR";


                BaseDados.AddParameter("DISCIPLINA", dto.Disciplina.Codigo);
                BaseDados.AddParameter("DOCENTE", dto.Docente.Codigo);

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

        public DocenteDisciplinaDTO Excluir(DocenteDisciplinaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_DOCENTE_DISCIPLINA_EXCLUIR";


                BaseDados.AddParameter("DISCIPLINA", dto.Disciplina.Codigo);
                BaseDados.AddParameter("DOCENTE", dto.Docente.Codigo);

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


        public List<DocenteDisciplinaDTO> ObterPorFiltro(DocenteDisciplinaDTO dto) 
        {
            List<DocenteDisciplinaDTO> lista;
            try
            {
                lista = new List<DocenteDisciplinaDTO>();
                BaseDados.ComandText = "stp_ACA_DOCENTE_DISCIPLINA_OBTERPORFILTRO";
                 
                
                BaseDados.AddParameter("ANO", dto.Disciplina.AnoLectivo);
                BaseDados.AddParameter("DOCENTE", dto.Docente.Codigo);
                BaseDados.AddParameter("DISCIPLINA", dto.Docente.Disciplina);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) 
                {
                    dto = new DocenteDisciplinaDTO();

                    dto.Docente = new DocenteDTO(int.Parse(dr["PREF_CODIGO_DOCENTE"].ToString()));
                    dto.DocenteName = dr["ENT_NOME_COMPLETO"].ToString();

                    dto.Disciplina = new UnidadeCurricularDTO(int.Parse(dr["PREF_CODIGO_DISCIPLINA"].ToString()));
                    dto.DisciplinaDesgination = dr["DIS_PLAN_DESIGNACAO"].ToString(); 
                    lista.Add(dto);
                } 
            }
            catch (Exception ex)
            {
                lista = new List<DocenteDisciplinaDTO>();
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
