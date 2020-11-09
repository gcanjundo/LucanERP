using DataAccessLayer;
using Dominio.Comercial.Lavandaria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace DataAccessLayer.Comercial.Lavandaria
{
    public class RoupaCorDAO : ConexaoDB
    {
       
            public RoupaCorDTO Adicionar(RoupaCorDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_COR_ADICIONAR";

                AddParameter("ITEM_COR_ID", dto.Cor_Codigo);
                AddParameter("OBSERVACOES", dto.Observacao);
                AddParameter("CREATED_BY", dto.CreatedBy);
                AddParameter("CREATED_DATE", dto.CreatedDate);
                

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
                }

                return dto;
            }

            public RoupaCorDTO Alterar(RoupaCorDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_COR_ALTERAR";

                AddParameter("ITEM_COR_ID", dto.Cor_Codigo);
                AddParameter("OBSERVACOES", dto.Observacao);
                AddParameter("UPDATED_BY", dto.UpdatedBy);
                AddParameter("UPDATED_DATE", dto.UpdatedDate);

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

            public RoupaCorDTO Eliminar(RoupaCorDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_COR_EXCLUIR";

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

            public List<RoupaCorDTO> ObterPorFiltro(RoupaCorDTO dto)
            {
                List<RoupaCorDTO> lista;
                try
                {
                    ComandText = "stp_ROUPA_CORES_OBTERPORFILTRO";

                    AddParameter("OBSERVACOES", dto.Observacao);

                    MySqlDataReader dr = ExecuteReader();

                    lista = new List<RoupaCorDTO>();

                    while (dr.Read())
                    {
                        dto = new RoupaCorDTO();

                        dto.Codigo = int.Parse(dr[0].ToString());
                        dto.Cor_Codigo = int.Parse(dr[1].ToString());
                        dto.Observacao = dr[2].ToString();
                        
                    lista.Add(dto);
                    }

                }
                catch (Exception ex)
                {
                    dto = new RoupaCorDTO();
                    dto.Sucesso = false;
                    dto.MensagemErro = ex.Message.Replace("'", "");
                    lista = new List<RoupaCorDTO>();
                    lista.Add(dto);
                }
                finally
                {
                    FecharConexao();
                }

                return lista;
            }

            public RoupaCorDTO ObterPorPK(RoupaCorDTO dto)
            {
                try
                {
                    ComandText = "stp_ROUPA_CORES_OBTERPORPK";

                    AddParameter("CODIGO", dto.Codigo);

                    MySqlDataReader dr = ExecuteReader();

                    dto = new RoupaCorDTO();

                    if (dr.Read())
                    {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Cor_Codigo = int.Parse(dr[1].ToString());
                    dto.Observacao = dr[2].ToString();
                   


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
