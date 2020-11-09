using System;
using System.Collections.Generic;
using Dominio.Comercial;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Comercial
{
    public class ComissaoDAO : ConexaoDB
    {


        public ComissaoDTO Adicionar(ComissaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_COMISSAO_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@VALOR", dto.Valor);

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
       

        public ComissaoDTO Eliminar(ComissaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_COMISSAO_EXCLUIR";

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

        public List<ComissaoDTO> ObterPorFiltro(ComissaoDTO dto)
        {
            List<ComissaoDTO> lista;
            try
            {
                ComandText = "stp_COM_COMISSAO_OBTERPORFILTRO";

                

                MySqlDataReader dr = ExecuteReader();

                lista = new List<ComissaoDTO>();

                while (dr.Read())
                {
                    dto = new ComissaoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Valor = Convert.ToDecimal(dr[2].ToString());
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());
                     
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ComissaoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<ComissaoDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public ComissaoDTO ObterPorPK(ComissaoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_COMISSAO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new ComissaoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Valor = Convert.ToDecimal(dr[2].ToString());
                    dto.Estado = int.Parse(dr[3].ToString()); 
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
