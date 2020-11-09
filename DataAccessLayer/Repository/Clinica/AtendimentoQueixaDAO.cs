using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class AtendimentoQueixasDAO: ConexaoDB 
    {
         

        public AtendimentoQueixasDTO Adicionar(AtendimentoQueixasDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ATENDIMENTO_QUEIXAS_ADICIONAR";

                AddParameter("ATENDIMENTO", dto.Atendimento);
                AddParameter("QUEIXA", dto.Queixa);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("TEMPO", dto.Tempo);
                AddParameter("UTILIZADOR", dto.Utilizador);

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

        

        public AtendimentoQueixasDTO Eliminar(AtendimentoQueixasDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ATENDIMENTO_QUEIXAS_EXCLUIR";
                 
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

            return dto;
        }

        public List<AtendimentoQueixasDTO> ObterPorFiltro(AtendimentoQueixasDTO dto)
        {
            List<AtendimentoQueixasDTO> listaAtendimentoQueixass = new List<AtendimentoQueixasDTO>();
            try
            {
                ComandText = "stp_CLI_ATENDIMENTO_QUEIXAS_OBTERPORFILTRO";

                AddParameter("ATENDIMENTO", dto.Atendimento); 

                MySqlDataReader dr = ExecuteReader();

                while(dr.Read())
                {
                   dto = new AtendimentoQueixasDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Atendimento = int.Parse(dr[1].ToString());
                   dto.Queixa = dr[2].ToString();
                   dto.Descricao = dr[3].ToString();
                   dto.Tempo = dr[4].ToString();

                   listaAtendimentoQueixass.Add(dto);
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

            return listaAtendimentoQueixass;
        }

        public AtendimentoQueixasDTO ObterPorPK(AtendimentoQueixasDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_ATENDIMENTO_QUEIXAS_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new AtendimentoQueixasDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Atendimento = int.Parse(dr[1].ToString());
                    dto.Queixa = dr[2].ToString();
                    dto.Descricao = dr[3].ToString();
                    dto.Tempo = dr[4].ToString();
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
