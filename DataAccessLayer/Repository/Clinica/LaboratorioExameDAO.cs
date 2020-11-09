using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dominio.Clinica;


namespace DataAccessLayer.Clinica
{
    public class LaboratorioExameDAO: ConexaoDB 
    { 
        public LaboratorioExameDTO Adicionar(LaboratorioExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_ADICIONAR";

                AddParameter("@CODIGO", dto.ExameArtigoID);
                AddParameter("DESCRICAO", dto.Designacao);
                AddParameter("@CATEGORIA", dto.Categoria);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@AMOSTRA_ID", dto.AmostraID <= 0 ? (object)DBNull.Value : dto.AmostraID);
                AddParameter("@DURACAO", dto.DelieveryDeadLine);
                AddParameter("@ARTIGO_ID", dto.Codigo <= 0 ? (object)DBNull.Value : dto.Codigo);
                AddParameter("@CLINICAL_APPLICATION", dto.AplicacaoClinica);
                AddParameter("@UNIDADE", dto.UnidadeVenda);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@NOTAS_INTERPRETACAO", dto.Notes);
                AddParameter("@CUIDADOS_COLECTA", dto.CuidadosColecta);
                AddParameter("@NOTAS_PACIENTE", dto.NotasPaciente);
                AddParameter("@TRANSPORTE_ARMAZENAMENTO", dto.TransporteArmazenamento);
                AddParameter("@METODOS_LAB", dto.MetodosLaboratoriais);
                AddParameter("@COMENTARIO_PATOLOGISTA", dto.CometarioPatologistaClinico);

                dto.Codigo = ExecuteInsert();
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
                if (dto.Codigo > 0 && dto.Sucesso)
                {
                    foreach (var item in dto.ValoresReferencia)
                    {
                        item.ExameID = dto.Codigo;
                        AddReferenceValues(item);
                    }

                    if(dto.ChildrenComposeList.Count > 0)
                    {
                        foreach(var exame in dto.ChildrenComposeList)
                        {
                            exame.MasterID = dto.Codigo;
                            AddChildrenCompose(exame);
                        }
                    }
                }

                 
            }

            return dto;
        }



        public LaboratorioExameDTO Excluir(LaboratorioExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo); 
                AddParameter("@UTILIZADOR", dto.Utilizador); 

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

        public List<LaboratorioExameDTO> ObterPorFiltro(LaboratorioExameDTO dto)
        {
            List<LaboratorioExameDTO> listaExames = new List<LaboratorioExameDTO>();
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Designacao); 
                AddParameter("@CATEGORIA", dto.Categoria);
                AddParameter("@AMOSTRA_ID", dto.AmostraID);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new LaboratorioExameDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Designacao = dr[1].ToString(),
                        Referencia = dr[2].ToString(),
                        Categoria = dr[3].ToString(),
                        Notes = dr[4].ToString(), // Tipo de Amostra
                        Status = int.Parse(dr[5].ToString()),
                        UnidadeVenda = dr[6].ToString(),
                        Filial = dr[7].ToString()
                    };

                    listaExames.Add(dto);
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

            return listaExames;
        }

        public LaboratorioExameDTO ObterPorPK(LaboratorioExameDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_OBTERPORPK";

                AddParameter("@CODIGO", dto.Codigo); 

                MySqlDataReader dr = ExecuteReader();

                dto = new LaboratorioExameDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Designacao = dr[1].ToString();
                    dto.Categoria = dr[2].ToString();
                    dto.Referencia = dr[3].ToString();
                    dto.AmostraID = int.Parse(dr[4].ToString() == "" ? "-1": dr[4].ToString());
                    dto.DelieveryDeadLine = int.Parse(dr[5].ToString());
                    dto.ExameArtigoID = int.Parse(dr[6].ToString() == "" ? "-1" : dr[6].ToString());
                    dto.UnidadeID = int.Parse(dr[7].ToString());
                    dto.Notes = dr[8].ToString();
                    dto.AplicacaoClinica = dr[9].ToString();
                    dto.Status = int.Parse(dr[10].ToString());
                    dto.CuidadosColecta = dr[17].ToString();
                    dto.NotasPaciente = dr[18].ToString();
                    dto.TransporteArmazenamento = dr[19].ToString();
                    dto.MetodosLaboratoriais = dr[20].ToString();
                    dto.CometarioPatologistaClinico = dr[21].ToString();
                    dto.PrecoVenda = decimal.Parse(dr[22].ToString());
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
                if(dto.Codigo > 0)
                {
                    dto.ValoresReferencia = ObterValoresReferencia(dto);
                    dto.ChildrenComposeList = ObterChidren(dto);
                }
            }

            return dto;
        }
        
        
        void AddReferenceValues(LaboratorioExameValoresReferenciaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_VALORES_REFERENCIA_ADICIONAR"; 
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@EXAME_ID", dto.ExameID);
                AddParameter("@FAIXA_ETARIA", dto.FaixaEtariaID);
                AddParameter("@SINAL", dto.Sinal);
                AddParameter("@VALOR_INICIAL", dto.ValorInicial);
                AddParameter("@VALOR_FINAL", dto.ValorFinal);
                AddParameter("@VALOR_REFERENCIA", dto.ValorReferencia);
                AddParameter("@UTILIZADOR", dto.Utilizador); 

                ExecuteNonQuery();
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
        }

        List<LaboratorioExameValoresReferenciaDTO> ObterValoresReferencia(LaboratorioExameDTO dto)
        {
            var lista = new List<LaboratorioExameValoresReferenciaDTO>();
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_VALORES_REFERENCIA_OBTERPORFILTRO";

                AddParameter("@EXAME_ID", dto.Codigo);

                MySqlDataReader dr = ExecuteReader(); 

                while (dr.Read())
                {
                    lista.Add(new LaboratorioExameValoresReferenciaDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        ExameID = int.Parse(dr[1].ToString()),
                        FaixaEtariaID = int.Parse(dr[2].ToString()),
                        Sinal = dr[3].ToString(),
                        ValorInicial = decimal.Parse(dr[4].ToString()),
                        ValorFinal = decimal.Parse(dr[5].ToString()),
                        ValorReferencia = dr[6].ToString(),
                        Descricao = dr[11].ToString()
                    });
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

            return lista;
        }

        void AddChildrenCompose(LaboratorioExameDTO dto)
        {
            try
            {
                ComandText = "CLI_LABORATORIO_EXAME_COMPOSTO_ADICIONAR";
                AddParameter("@EXA_MASTER", dto.MasterID);
                AddParameter("@EXA_CHILDREN", dto.Codigo);
                ExecuteNonQuery();
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
        }

        public List<LaboratorioExameDTO> ObterChidren(LaboratorioExameDTO dto)
        {
            List<LaboratorioExameDTO> listaExames = new List<LaboratorioExameDTO>();
            try
            {
                ComandText = "CLI_LABORATORIO_EXAME_COMPOSTO_OBTERPORMASTER";

                AddParameter("EXA_MASTER", dto.Codigo); 

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new LaboratorioExameDTO
                    {
                        Codigo = int.Parse(dr[1].ToString()),
                        Designacao = dr[3].ToString(), 
                    };

                    listaExames.Add(dto);
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

            return listaExames;
        }
    }
}
