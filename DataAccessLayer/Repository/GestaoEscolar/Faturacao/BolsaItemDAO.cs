using System;
using System.Collections.Generic;
using Dominio.GestaoEscolar.Faturacao;

namespace DataAccessLayer.GestaoEscolar.Faturacao
{
    public class BolsaItemDAO
    {
        readonly ConexaoDB BaseDados;

        public BolsaItemDAO()
        {
            BaseDados = new ConexaoDB();

        }
        public BolsaItemDTO Salvar(BolsaItemDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_FIN_BOLSA_ITEM_COBRANCA_ADICIONAR";
                BaseDados.AddParameter("@BOLSA", dto.Bolsa);
                BaseDados.AddParameter("@ITEM", dto.ItemCobranca);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@PERCENTAGEM", dto.Valor);
                BaseDados.AddParameter("@VALOR", dto.Valor);
                BaseDados.AddParameter("@MULTA", dto.Multa);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.ExecuteNonQuery();
 
            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public BolsaItemDTO Remover(BolsaItemDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_FIN_BOLSA_ITEM_COBRANCA_EXCLUIR";

                BaseDados.AddParameter("@BOLSA", dto.Bolsa);
                BaseDados.AddParameter("@ITEM", dto.ItemCobranca);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public List<BolsaItemDTO> ObterPorFiltro(BolsaItemDTO dto)
        {
            List<BolsaItemDTO> lista = new List<BolsaItemDTO>();

            try
            {
                BaseDados.ComandText = "stp_FIN_BOLSA_ITEM_COBRANCA_OBTERPORFILTRO";

                BaseDados.AddParameter("@BOLSA", dto.Bolsa);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read()) {
                    dto = new BolsaItemDTO();
                    dto.Bolsa = dr[0];
                    dto.ItemCobranca = dr[1] + " - "+dr[3].ToUpper();
                    dto.Percentagem = decimal.Parse(dr[4]);
                    dto.Valor = decimal.Parse(dr[5]);
                    dto.Multa = dr[6];
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;

        }

        public BolsaItemDTO ObterPorValor(BolsaItemDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_FIN_BOLSA_ITEM_COBRANCA_OBTERVALOR";

                BaseDados.AddParameter("@ALUNO", dto.Bolsa);
                BaseDados.AddParameter("@ITEM", dto.ItemCobranca);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new BolsaItemDTO();

                while (dr.Read())
                {
                    
                    
                    dto.Valor = decimal.Parse(dr[0]);
                    dto.Multa = dr[1];
                   
                }

            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;

        }

        public List<BolsaItemDTO> ObterItensActivos(BolsaItemDTO dto)
        {
            List<BolsaItemDTO> lista = new List<BolsaItemDTO>();

            try
            {
                BaseDados.ComandText = "stp_FIN_BOLSA_ITEM_COBRANCA_OBTERACTIVOS";

                BaseDados.AddParameter("@ALUNO", dto.Bolsa);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new BolsaItemDTO();
                    dto.Bolsa = dr[0];
                    dto.ItemCobranca = dr[1];
                    dto.Valor = decimal.Parse(dr[3]);
                    dto.Multa = dr[6];
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;

        }


        public List<BolsaItemDTO> BolsaItem(BolseiroDTO dto)
        {
            List<BolsaItemDTO> lista = new List<BolsaItemDTO>();
            BolsaItemDTO item;
            try
            {
                BaseDados.ComandText = "stp_ACA_BOLSA_ITEM_OBTERBOLSA";

                BaseDados.AddParameter("@ALUNO", dto.Aluno);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    item = new BolsaItemDTO();
                    item.Bolsa = dr[0];
                    item.ItemCobranca = dr[1];
                    item.Valor = decimal.Parse(dr[2]);
                    item.Multa = dr[3];
                    item.AnoLectivo = int.Parse(dr[4]);
                    item.CategoryID = int.Parse(dr[6]);
                    item.Sucesso = dr[7] != "1" ? true : false;
                    item.ItemDesignacao = dr[8];
                    lista.Add(item);
                }

            }
            catch (Exception ex)
            {
                item = new BolsaItemDTO();
                item.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
