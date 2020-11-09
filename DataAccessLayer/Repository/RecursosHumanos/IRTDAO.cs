using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class IRTDAO 
    {

        readonly ConexaoDB BaseDados;

        public IRTDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public EscaloesIRTDTO Adicionar(EscaloesIRTDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_ESCALOES_IRT_ADICIONAR";
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("DE", dto.SalarioMinimo);
                BaseDados.AddParameter("ATE", dto.SalarioMaximo);
                BaseDados.AddParameter("VALOR", dto.ValorMinimoDesconto);
                BaseDados.AddParameter("PERCENTUAL", dto.PercentualDesconto);
                BaseDados.AddParameter("EXCESSO", dto.ValorExcesso);
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();
               dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally 
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

  
        public EscaloesIRTDTO Eliminar(EscaloesIRTDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_ESCALOES_IRT_EXCLUIR";
                 
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = BaseDados.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public List<EscaloesIRTDTO> ObterPorFiltro()
        {
            List<EscaloesIRTDTO> listaEscaloes = new List<EscaloesIRTDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_ESCALOES_IRT_OBTERPORFILTRO"; 

                MySqlDataReader dr = BaseDados.ExecuteReader();



                while(dr.Read())
                {
                    EscaloesIRTDTO dto = new EscaloesIRTDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.SalarioMinimo = decimal.Parse(dr[1].ToString());
                    dto.SalarioMaximo = decimal.Parse(dr[2].ToString());
                    dto.ValorMinimoDesconto = decimal.Parse(dr[3].ToString());
                    dto.PercentualDesconto = decimal.Parse(dr[4].ToString());
                    dto.ValorExcesso = decimal.Parse(dr[5].ToString());
                    dto.Descricao = dr[6].ToString();
                    listaEscaloes.Add(dto);
                }

            }
            catch (Exception ex)
            {
                EscaloesIRTDTO dto = new EscaloesIRTDTO(); 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaEscaloes.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaEscaloes;
        }

        public EscaloesIRTDTO ObterPorPK(EscaloesIRTDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_ESCALOES_IRT_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new EscaloesIRTDTO();

                while(dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.SalarioMinimo = decimal.Parse(dr[1].ToString());
                    dto.SalarioMaximo = decimal.Parse(dr[2].ToString());
                    dto.ValorMinimoDesconto = decimal.Parse(dr[3].ToString());
                    dto.PercentualDesconto = decimal.Parse(dr[4].ToString());
                    dto.ValorExcesso = decimal.Parse(dr[5].ToString());
                    dto.Descricao = dr[6].ToString();



                }

            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }
    }
}
