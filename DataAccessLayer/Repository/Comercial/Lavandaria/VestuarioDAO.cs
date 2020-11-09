using DataAccessLayer;
using Dominio.Comercial.Lavandaria;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace DataAccessLayer.Comercial.Lavandaria
{
    public class VestuarioDAO : ConexaoDB
    {
            public VestuarioDTO Adicionar(VestuarioDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_VESTUARIO_ADICIONAR";
                    AddParameter("DESIGNACAO", dto.Descricao);
                    AddParameter("SIGLA", dto.Sigla);
                    AddParameter("UNIDADE", dto.Unidade);
                    AddParameter("NUMBER_PECAS", dto.NroItems);
                    AddParameter("GENERO_ID", dto.GeneroID);
                    AddParameter("SITUACAO", dto.Situacao);
                    AddParameter("PHOTO_BLOB", dto.ImageBlob);
                    AddParameter("PHOTO_PATH", dto.ImagePath);
                    AddParameter("UTILIZADOR", dto.Utilizador);
                   
                    

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

            public VestuarioDTO Alterar(VestuarioDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_VESTUARIO_ALTERAR";
                    AddParameter("DESIGNACAO", dto.Descricao);
                    AddParameter("SIGLA", dto.Sigla);
                    AddParameter("UNIDADE", dto.Unidade);
                    AddParameter("NUMBER_PECAS", dto.NroItems);
                    AddParameter("GENERO_ID", dto.GeneroID);
                    AddParameter("STATUS", dto.Status);
                    AddParameter("PHOTO_BLOB", dto.ImageBlob);
                    AddParameter("PHOTO_PATH", dto.ImagePath);
                    AddParameter("UTILIZADOR", dto.Utilizador);

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

            public VestuarioDTO Eliminar(VestuarioDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_VESTUARIO_EXCLUIR";

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
        
            public List<VestuarioDTO> ObterPorFiltro(VestuarioDTO dto)
            {
                List<VestuarioDTO> lista;
            try
            {
                ComandText = "stp_LAV_VESTUARIO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao ?? string.Empty);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<VestuarioDTO>();

                while (dr.Read())
                {
                    dto = new VestuarioDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Sigla = dr[2].ToString(),
                        Unidade = dr[3].ToString(),
                        NroItems = int.Parse(dr[4].ToString()),
                        GeneroID = int.Parse(dr[5].ToString()),
                        ImageBlob = dr[6].ToString(),
                        ImagePath = dr[7].ToString()
                       
                    };

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new VestuarioDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<VestuarioDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

                return lista;
            }
        
        public VestuarioDTO ObterPorPK(VestuarioDTO dto)
            {
                try
                {
                    ComandText = "stp_LAV_VESTUARIO_OBTERPORPK";

                    AddParameter("CODIGO", dto.Codigo);

                    MySqlDataReader dr = ExecuteReader();

                    dto = new VestuarioDTO();

                    if (dr.Read())
                    {

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Unidade = dr[3].ToString();
                    dto.NroItems = int.Parse(dr[4].ToString());
                    dto.GeneroID = int.Parse(dr[5].ToString());
                    dto.ImageBlob = dr[6].ToString();
                    dto.ImagePath = dr[7].ToString();



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
