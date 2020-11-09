using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using Dominio.GestaoEscolar.Faturacao;
using DataAccessLayer.GestaoEscolar.Faturacao;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class PlanoCurricularDAO
    {
        readonly ConexaoDB BaseDados;

        public PlanoCurricularDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public AnoCurricularDTO Inserir(AnoCurricularDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_ADICIONAR";

                BaseDados.AddParameter("@ANO", dto.AnoCurricular);
                BaseDados.AddParameter("@RAMO", dto.Ramo.RamCodigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@IDADE", dto.Idade);
                BaseDados.AddParameter("LIMITE", dto.DataLimite);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@NOTA_ESTAGIO", dto.IsNotaEstagioRequired == true ? 1 : 0);

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

        public AnoCurricularDTO Alterar(AnoCurricularDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_ALTERAR";

                BaseDados.AddParameter("@ANO", dto.AnoCurricular);
                BaseDados.AddParameter("@RAMO", dto.Ramo.RamCodigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@TIPO", dto.Tipo); ;
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@IDADE", dto.Idade);
                BaseDados.AddParameter("LIMITE", dto.DataLimite);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@NOTA_ESTAGIO", dto.IsNotaEstagioRequired == true ? 1 : 0);

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

        public void Apagar(AnoCurricularDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_EXCLUIR";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                BaseDados.ExecuteNonQuery();

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

        public AnoCurricularDTO ObterPorPK(AnoCurricularDTO dto)
        {
          
            try
            {
                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_OBTERPORPK"; 

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                BaseDados.AddParameter("@CLASSE", dto.AnoCurricular);
                if (dto.Ramo != null)
                {
                    BaseDados.AddParameter("@RAMO", dto.Ramo.RamCodigo);
                }
                else
                {
                    BaseDados.AddParameter("@RAMO", -1);
                }


                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AnoCurricularDTO();
                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["PLAN_CODIGO"].ToString());
                    
                     
                    
                    dto.AnoCurricular = Int32.Parse(dr["PLAN_ANO_CURRICULAR"].ToString());

                    dto.Descricao = dr["PLAN_DESCRICAO"].ToString();
                    dto.Tipo = dr["PLAN_TIPO"].ToString(); 

                    dto.Ramo = new RamoDTO(Int32.Parse(dr["PLAN_CODIGO_RAMO"].ToString()), dr["CUR_NOME"].ToString(), null, dr["CUR_ABREVIACAO"].ToString(), 0, 0, 1, "");
                    dto.Idade = int.Parse(dr["PLAN_IDADE"].ToString() == null || dr["PLAN_IDADE"].ToString() =="" ? "-1" : dr["PLAN_IDADE"].ToString());
                    dto.DataLimite = dr["PLAN_DATA"].ToString();
                    dto.IsNotaEstagioRequired = dr["PLAN_NOTA_ESTAGIO"].ToString() == null || dr["PLAN_NOTA_ESTAGIO"].ToString() == "" || dr["PLAN_NOTA_ESTAGIO"].ToString() != "1" ? false : true;


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

        public AnoCurricularDTO ObterQuantidade()
        {
           

            AnoCurricularDTO dto = new AnoCurricularDTO();
            try
            {

                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_QUANTIDADE";
                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                    dto.Codigo = Int32.Parse(dr["TOTAL"].ToString());

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

        public AnoCurricularDTO ObterAnoSeguinte(AnoCurricularDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_SEGUINTE";

                BaseDados.AddParameter("@CLASSE", dto.Codigo);

                int classeActual = dto.Codigo;

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AnoCurricularDTO();
                while (dr.Read())
                {
                    if (Int32.Parse(dr["PLAN_CODIGO"].ToString())==classeActual)
                    {
                        classeActual = -1;
                    }
                    else if (classeActual == -1)
                    {
                        dto.Codigo = Int32.Parse(dr["PLAN_CODIGO"].ToString());

                        dto.AnoCurricular = Int32.Parse(dr["PLAN_ANO_CURRICULAR"].ToString());

                        dto.Descricao = dr["PLAN_DESCRICAO"].ToString();
                    
                        break;
                    }

                    
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

        public List<AnoCurricularDTO> ObterPorFiltro(AnoCurricularDTO dto)
        {
           
            List<AnoCurricularDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_OBTERPORFILTRO";




                if (dto.AnoCurricular > 0)
                {
                    BaseDados.AddParameter("@ANO", dto.AnoCurricular);
                }
                else
                {
                    BaseDados.AddParameter("@ANO", -1);
                }
                if (dto.Ramo != null)
                {
                    BaseDados.AddParameter("@RAMO", dto.Ramo.RamCodigo);
                }
                else
                {
                    BaseDados.AddParameter("@RAMO", -1);
                }


                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<AnoCurricularDTO>();
                while (dr.Read())
                {
                    dto = new AnoCurricularDTO();
                    dto.Codigo = Int32.Parse(dr["PLAN_CODIGO"].ToString());

                    RamoDTO dtoRamo = new RamoDTO();
                    dtoRamo.RamCodigo = Int32.Parse(dr["PLAN_CODIGO_RAMO"].ToString());


                    dto.AnoCurricular = Int32.Parse(dr["PLAN_ANO_CURRICULAR"].ToString());

                    

                    dto.Descricao = dr["PLAN_DESCRICAO"].ToString();
                    dto.Tipo = dr["PLAN_TIPO"].ToString();

                    dto.Ramo = new RamoDTO(Int32.Parse(dr["PLAN_CODIGO_RAMO"].ToString()), dr["CUR_NOME"].ToString(), null, dr["CUR_ABREVIACAO"].ToString(), 0, 0, 1, "");
                    dto.Idade = int.Parse(dr["PLAN_IDADE"].ToString() == null || dr["PLAN_IDADE"].ToString() == "" ? "-1" : dr["PLAN_IDADE"].ToString());
                    dto.DataLimite = dr["PLAN_DATA"].ToString();
                    dto.IsNotaEstagioRequired = dr["PLAN_NOTA_ESTAGIO"].ToString() == null || dr["PLAN_NOTA_ESTAGIO"].ToString() == "" || dr["PLAN_NOTA_ESTAGIO"].ToString() != "1" ? false : true;
                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<AnoCurricularDTO>();
                dto = new AnoCurricularDTO();
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


        public List<AnoCurricularDTO> ObterTodas(AnoCurricularDTO dto)
        {

            List<AnoCurricularDTO> lista;
            try
            {
                BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_OBTERTODOS";

                BaseDados.AddParameter("@FILIAL", dto.Filial);
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<AnoCurricularDTO>();
                while (dr.Read())
                {
                    dto = new AnoCurricularDTO();
                    dto.Codigo = Int32.Parse(dr["PLAN_ANO_CURRICULAR"].ToString());
                    dto.Descricao = dr["PLAN_DESCRICAO"].ToString();
                    dto.IsNotaEstagioRequired = dr["PLAN_NOTA_ESTAGIO"].ToString() == null || dr["PLAN_NOTA_ESTAGIO"].ToString() == "" || dr["PLAN_NOTA_ESTAGIO"].ToString() != "1" ? false : true;
                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<AnoCurricularDTO>();
                dto = new AnoCurricularDTO();
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

        public Int32 UltimoAno(AnoCurricularDTO dto)
        {
            var _lastYear = -1;
            try
            {
                BaseDados.ComandText = "stp_ACA_CURSO_ULTIMO_ANO";
                BaseDados.AddParameter("@PLANO", dto.Codigo);
            
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AnoCurricularDTO();
                if (dr.Read())
                {
                    _lastYear = int.Parse(dr[2]);
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

            return _lastYear;
        }

        public List<AnoCurricularDTO> ObterClasseEstudadas(AlunoDTO dtoAluno) 
        {
           
            List<AnoCurricularDTO> lista;

            AnoCurricularDTO dto;

            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_PLANOS_FREQUENTADOS";
                BaseDados.AddParameter("@ALUNO", dtoAluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<AnoCurricularDTO>();
                while (dr.Read())
                {
                    dto = new AnoCurricularDTO();

                    dto.Codigo = Int32.Parse(dr["PLAN_CODIGO"].ToString());

                    dto.AnoCurricular = Int32.Parse(dr["PLAN_ANO_CURRICULAR"].ToString());
                    dto.Ramo = new RamoDTO(Int32.Parse(dr["PLAN_CODIGO_RAMO"].ToString()), dr["CUR_NOME"].ToString(), null, dr["CUR_ABREVIACAO"].ToString(), 0, 0, 1, "");
                    dto.Descricao = dr["PLAN_DESCRICAO"].ToString();
                    dto.Tipo = dr["PLAN_TIPO"].ToString();
                    dto.AnoLectivo = Int32.Parse(dr["MAT_CODIGO_ANO"].ToString());
                    dto.TurmaID = int.Parse(dr["TUR_CODIGO"].ToString()); 

                    lista.Add(dto);

                }

            }
            catch (Exception ex)
            {
                lista = new List<AnoCurricularDTO>();
                dto = new AnoCurricularDTO();
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

        public MensalidadeDTO ObterMensalidadeEscolar(MatriculaDTO dto)
        {
            BaseDados.ComandText = "stp_ACA_PLANO_CURRICULAR_OBTERMENSALIDADE";

            
            var mensalidade = new MensalidadeDTO();
            try
            {
                BaseDados.AddParameter("@PLANO", dto.AnoCurricular);
                BaseDados.AddParameter("@MATRICULA", dto.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {   
                    mensalidade.Codigo = int.Parse(dr[1]);
                    break;
                     
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
                mensalidade = new MensalidadeDAO().ObterPorPK(mensalidade);
            }

            return mensalidade;
        }

      
    }
}
