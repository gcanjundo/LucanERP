using System;
using System.Collections.Generic;

using Dominio.Comercial.Restauracao;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Comercial.Restauracao
{
    public class MesaDAO: ConexaoDB
    {
        

        public MesaDTO Adicionar(MesaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_ADICIONAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("LOCALIZACAO", dto.Localizacao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("LUGARES", dto.Lugares);

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

        public MesaDTO Alterar(MesaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_ALTERAR";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("LOCALIZACAO", dto.Localizacao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("LUGARES", dto.Lugares);

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

        public bool Eliminar(MesaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_EXCLUIR";

                
                AddParameter("CODIGO", dto.Codigo);

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

            return dto.Sucesso;
        }

        public List<MesaDTO> ObterPorFiltro(MesaDTO dto)
        {
            List<MesaDTO> listaMesa; 
            try
            {
                
                ComandText = "stp_REST_MESA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("TIPO", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();
                listaMesa = new List<MesaDTO>();
                while (dr.Read()) 
                {
                    dto = new MesaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Localizacao = dr[1].ToString();
                    dto.Descricao = dr[2].ToString();
                    dto.Sigla = dr[3].ToString();
                    dto.Estado = int.Parse(dr[4].ToString());
                    dto.Lugares = int.Parse(dr[5].ToString() == null ? "1" : dr[5].ToString());
                    dto.DesignacaoEntidade = dr[6].ToString();
                    dto.BookingID = dr[7].ToString() == string.Empty ? 0 : int.Parse(dr[7].ToString());

                    listaMesa.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MesaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaMesa = new List<MesaDTO>();
                listaMesa.Add(dto);

            }
            finally
            {
                FecharConexao();
            }

            return listaMesa;
        }

        public MesaDTO ObterPorPK(MesaDTO dto)
        {
            try
            { 
                ComandText = "stp_REST_MESA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                dto = new MesaDTO();

                MySqlDataReader dr = ExecuteReader(); 

                if (dr.Read())
                {

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Localizacao = dr[1].ToString();
                    dto.Descricao = dr[2].ToString();
                    dto.Sigla = dr[3].ToString();
                    dto.Estado = int.Parse(dr[4].ToString());
                    dto.Lugares = int.Parse(dr[5].ToString() == null ? "1" : dr[5].ToString());  
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


        public void Ocupar(MesaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_OCUPAR";

                 
                AddParameter("CODIGO", dto.Codigo); 

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
        }

        public void Desocupar(MesaDTO dto)
        {
            try
            {
                ComandText = "stp_REST_MESA_DESOCUPAR";


                AddParameter("CODIGO", dto.Codigo);

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
        }

       
    }
}
