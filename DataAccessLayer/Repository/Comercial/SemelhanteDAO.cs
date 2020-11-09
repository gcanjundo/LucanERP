
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Comercial
{
    public class SemelhanteDAO: ConexaoDB
    {
        public SemelhanteDTO Adicionar(SemelhanteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_SEMELHANTE_ADICIONAR";

                AddParameter("ARTIGO", dto.ProductID);
                AddParameter("COMPONENTE", dto.Codigo); 
                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<SemelhanteDTO> ObterPorFiltro(ArtigoDTO pArtigo)
        {
            List<SemelhanteDTO> lista = new List<SemelhanteDTO>();
            SemelhanteDTO dto;
            try
            {
                ComandText = "stp_GER_ARTIGO_SEMELHANTE_OBTERPORFILTRO";

                AddParameter("ARTIGO", pArtigo.Codigo);

                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new SemelhanteDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Designacao = dr[1].ToString(); 
                    dto.PrecoVenda = Convert.ToDecimal(dr[2].ToString() ?? "0");
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new SemelhanteDTO();
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public SemelhanteDTO Excluir(SemelhanteDTO dto)
        {
            try
            {
                ComandText = "stp_GER_ARTIGO_SEMELHANTE_EXCLUIR";

                AddParameter("@ARTIGO", dto.Codigo);

                ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }
    }
}
