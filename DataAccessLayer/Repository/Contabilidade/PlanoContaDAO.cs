using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Contabilidade;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Contabilidade
{
    public class PlanoContaDAO : ConexaoDB
    {
         

        public PlanoContaDTO Inserir(PlanoContaDTO dto)
        {
            
            try
            {

                ComandText = "stp_CONTB_PLANO_CONTA_ADICIONAR"; 
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@NUMERO", dto.Conta);
                AddParameter("@DESCRICAO", dto.Descricao);
                if (dto.ContaPai.Equals(0) || dto.ContaPai.Equals(-1))
                {
                    AddParameter("@PAI", DBNull.Value);
                }
                else 
                {
                    AddParameter("@PAI", dto.ContaPai);
                }
                AddParameter("@ESTADO", dto.Estado);
                AddParameter("@NATUREZA", dto.Natureza);
                AddParameter("@CLASSE", dto.Classe); 
                AddParameter("@ANO", dto.AnoExercicio);

                dto.Codigo = ExecuteInsert();
                 
            }
            catch (Exception ex)
            {

                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }
            return dto;
        }

        
        public PlanoContaDTO ObterPorPK(PlanoContaDTO dto)
        {
            ComandText = "stp_CONTB_PLANO_CONTA_OBTERPORPK";
            
            
            AddParameter("@CODIGO", dto.Codigo);
            try
            {
                dto = new PlanoContaDTO();
                MySqlDataReader dr = ExecuteReader();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Conta = dr[1].ToString();
                    dto.Descricao = dr[2].ToString();
                    dto.ContaPai = int.Parse(dr[3].ToString());
                    dto.Estado = int.Parse(dr[4].ToString()); 
                    dto.Natureza = dr[5].ToString();
                    dto.Classe = dr[6].ToString();
                    dto.AnoExercicio = int.Parse(dr[7].ToString());
                    dto.RubricaPai = dr[8].ToString();
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

        public List<PlanoContaDTO> ObterPorFiltro(PlanoContaDTO dto)
        {
            
            List<PlanoContaDTO> lista = new List<PlanoContaDTO>();
            try
            {
               ComandText = "stp_CONTB_PLANO_CONTA_OBTERPORFILTRO";

                
               AddParameter("@NUMERO", dto.Conta);
               AddParameter("@DESCRICAO", dto.Descricao);
               AddParameter("@CLASSE", dto.Classe=="-1" ? string.Empty : dto.Classe);
               AddParameter("@NATUREZA", dto.Natureza == "-1" ? string.Empty : dto.Natureza);

                MySqlDataReader dr = ExecuteReader();
                while (dr.Read())
                {
                    dto = new PlanoContaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Conta = dr[1].ToString();
                    dto.Descricao = dr[2].ToString();
                    dto.ContaPai = int.Parse(dr[3].ToString());
                    dto.Estado = int.Parse(dr[4].ToString());
                    dto.Natureza = dr[5].ToString();
                    dto.Classe = dr[6].ToString();
                    dto.AnoExercicio = int.Parse(dr[7].ToString());
                    dto.RubricaPai = dr[8].ToString();

                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                 
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                FecharConexao();
            }
            return lista;
        } 
    }
}
