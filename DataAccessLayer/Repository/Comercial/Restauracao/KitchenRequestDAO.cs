
using Dominio.Comercial.Restauracao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;  

namespace DataAccessLayer.Comercial.Restauracao
{
    public class KitchenRequestDAO : ConexaoDB
    {

        public KitchenRequestDTO Adicionar(KitchenRequestDTO dto)
        {
            try
            {
                ComandText = "stp_REST_PEDIDO_COZINHA_ADICIONAR";

                // CODIGO, ATENDENTE, CLIENTE, INICIO, TERMINO, FILIAL, DESTINO, OUT LSTID
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@NUMERACAO", dto.Numeracao);
                AddParameter("@REFERENCIA", dto.Referencia);
                AddParameter("@ATENDIMENTO", dto.Atendimento);
                AddParameter("@ARTIGO", dto.Artigo); 
                AddParameter("@DATA_PEDIDO", dto.Data);
                AddParameter("@INICIO", dto.Inicio);
                AddParameter("@TERMINO", dto.Termino);
                AddParameter("@ATENDENTE", dto.Responsavel);
                AddParameter("@OBSERVACOES", dto.Obs);
                AddParameter("@NUMERO_DIA", dto.PedidoDia);
                AddParameter("@SITUACAO", dto.Status);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                

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

        public KitchenRequestDTO Fechar(KitchenRequestDTO dto)
        {
            try
            {
                ComandText = "stp_REST_PEDIDO_COZINHA_FECHAR";
                // CODIGO, ATENDENTE, CLIENTE, INICIO, TERMINO, FILIAL, DESTINO, OUT LSTID
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@ATENDENTE", dto.Responsavel);  
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@SITUACAO", dto.Status);
                AddParameter("@NOTAS", dto.Obs);

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

        public KitchenRequestDTO Eliminar(KitchenRequestDTO dto)
        {
            try
            {
                ComandText = "stp_REST_PEDIDO_COZINHA_EXCLUIR";

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

        public List<KitchenRequestDTO> ObterPorFiltro(KitchenRequestDTO dto)
        {
            List<KitchenRequestDTO> lista;
            try
            {
                ComandText = "stp_REST_PEDIDO_COZINHA_OBTERPORFILTRO";

                // ATENDENTE, CLIENTE, FILIAL
                AddParameter("@ATENDENTE", dto.Responsavel);
                AddParameter("@ATENDIMENTO", dto.Atendimento);
                AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                lista = new List<KitchenRequestDTO>();

                while (dr.Read())
                {
                    dto = new KitchenRequestDTO();

                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.Artigo = dr[1].ToString(); 
                    dto.Inicio = DateTime.Parse(dr[2].ToString());
                    dto.Termino = DateTime.Parse(dr[3].ToString() == "" ? DateTime.MinValue.ToShortDateString() : dr[3].ToString());
                    dto.Atendimento = int.Parse(dr[4].ToString());
                    dto.Responsavel = dr[5].ToString();
                    dto.Situacao = dr[6].ToString();
                    dto.Obs = dr[7].ToString();
                    dto.Utilizador = dr[9].ToString();

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new KitchenRequestDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<KitchenRequestDTO>();
                lista.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

         
 
        public void Excluir(KitchenRequestDTO dto)
        {
            try
            {
                ComandText = "stp_REST_PEDIDO_COZINHA_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);

                ExecuteNonQuery();

                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            } 
        }
    }
}
