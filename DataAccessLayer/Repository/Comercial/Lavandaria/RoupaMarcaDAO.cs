using DataAccessLayer;
using Dominio.Comercial.Lavandaria;
using MySql.Data.MySqlClient;
using System;


namespace DataAccessLayer.Comercial.Lavandaria
{
    public class RoupaMarcaDAO : ConexaoDB
    {
     
            public RoupaMarcaDTO Adicionar(RoupaMarcaDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_MARCA_ADICIONAR";

                    AddParameter("MARCA_ID", dto.MarcaId);
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

            public RoupaMarcaDTO Alterar(RoupaMarcaDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_MARCA_ALTERAR";

                    AddParameter("MARCA_ID", dto.MarcaId);
                    AddParameter("UPDATED_BY", dto.CreatedBy);
                    AddParameter("UPDATED_DATE", dto.CreatedDate);

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

            public RoupaMarcaDTO Eliminar(RoupaMarcaDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_MARCA_EXCLUIR";

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

            
            public RoupaMarcaDTO ObterPorPK(RoupaMarcaDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_ROUPA_MARCA_OBTERPORPK";

                    AddParameter("CODIGO", dto.Codigo);

                    MySqlDataReader dr = ExecuteReader();

                    dto = new RoupaMarcaDTO();

                    if (dr.Read())
                    {
                        dto.Codigo = int.Parse(dr[0].ToString());
                        dto.MarcaId = int.Parse(dr[1].ToString());
                        dto.CreatedBy = dr[3].ToString();
                        dto.CreatedDate = Convert.ToDateTime(dr[4].ToString());
                        dto.UpdatedBy = dr[5].ToString();
                        dto.UpdatedDate = Convert.ToDateTime(dr[6].ToString());
                     
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
