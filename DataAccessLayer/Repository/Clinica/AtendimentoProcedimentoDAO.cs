using Dominio.Clinica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Clinica
{
    public class AtendimentoProcedimentoDAO:ConexaoDB
    {
        public AtendimentoProcedimentoDTO Adicionar(AtendimentoProcedimentoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_ADICIONAR";


                AddParameter("@ARTIGO", dto.Atendimento);
                AddParameter("@FATURA", dto.Procedimento);
                AddParameter("@PRECO", dto.PrecoUnitario);
                AddParameter("@QUANTIDADE", dto.Quantidade); 
                AddParameter("@TOTAL", dto.ValorTotal); 
                AddParameter("ACTO", dto.Status);
                AddParameter("ACTO", dto.Utilizador);

                ExecuteNonQuery();

                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        
        public AtendimentoProcedimentoDTO Excluir(AtendimentoProcedimentoDTO dto)
        {
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_EXCLUIR";

                AddParameter("@ARTIGO", dto.Procedimento);
                AddParameter("@FATURA", dto.Atendimento);

                ExecuteNonQuery();

                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<AtendimentoProcedimentoDTO> ObterPorFiltro(AtendimentoProcedimentoDTO dto)
        {

            List<AtendimentoProcedimentoDTO> lista = new List<AtendimentoProcedimentoDTO>();
            try
            {
                ComandText = "stp_COM_FATURA_CLIENTE_ITEM_OBTERPORFILTRO";


                AddParameter("@FATURA", dto.Atendimento);

                MySqlDataReader dr = ExecuteReader();


                while (dr.Read())
                {
                    dto = new AtendimentoProcedimentoDTO();

                    dto.Procedimento = int.Parse(dr[1].ToString());
                    dto.Atendimento = int.Parse(dr[2].ToString());
                    dto.Quantidade = decimal.Parse(dr[3].ToString());
                    dto.PrecoUnitario = decimal.Parse(dr[4].ToString());
                    dto.ValorTotal = decimal.Parse(dr[9].ToString());
                    dto.ItemDesignation = dr[14].ToString();
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message;
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }
    }
}
