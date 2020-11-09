using DataAccessLayer;
using Dominio.Comercial.Lavandaria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace DataAccessLayer.Comercial.Lavandaria
{
    public class RoupaDefeitoDAO : ConexaoDB
    {
       
            public RoupaDefeitoDTO Adicionar(RoupaDefeitoDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_DEFEITO_ADICIONAR";

                    AddParameter("DESCRICAO", dto.Descricao);
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

            public RoupaDefeitoDTO Alterar(RoupaDefeitoDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_DEFEITO_ALTERAR";

                    AddParameter("DEFEITO_ID", dto.Defeito_Codigo);
                    AddParameter("DESCRICAO", dto.Descricao);
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

            public RoupaDefeitoDTO Eliminar(RoupaDefeitoDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_DEFEITO_EXCLUIR";

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

           

            public RoupaDefeitoDTO ObterPorPK(RoupaDefeitoDTO dto)
            {
                try
                {
                    ComandText = "stp_ROUPA_DEFEITO_OBTERPORPK";

                    AddParameter("CODIGO", dto.Codigo);

                    MySqlDataReader dr = ExecuteReader();

                    dto = new RoupaDefeitoDTO();

                    if (dr.Read())
                    {
                        dto.Codigo = int.Parse(dr[0].ToString());
                        dto.Defeito_Codigo = int.Parse(dr[1].ToString());
                        dto.Descricao = dr[2].ToString();
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
       
        public List<RoupaDefeitoDTO> ObterPorFiltro(RoupaDefeitoDTO dto)
        {
            List<RoupaDefeitoDTO> lista;
            try
            {
                ComandText = "stp_LAV_ROUPA_DEFEITO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
               

                MySqlDataReader dr = ExecuteReader();

                lista = new List<RoupaDefeitoDTO>();

                while (dr.Read())
                {
                    dto = new RoupaDefeitoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Defeito_Codigo = int.Parse(dr[1].ToString());
                    dto.Descricao = dr[2].ToString();

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new RoupaDefeitoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<RoupaDefeitoDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

       
        
    }
}
