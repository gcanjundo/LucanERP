using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class AbonoDescontoDAO:ConexaoDB
    {

        public AbonoDescontoDTO Adicionar(AbonoDescontoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_ABONOS_DESCONTOS_ADICIONAR";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("CATEGORIA", dto.Categoria); // A - ABONO; D - DESCONTO 
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("VALOR_FIXO", dto.Valor);
                AddParameter("TIPO_VALOR", dto.TipoValor);

                AddParameter("VALOR_CONTRIBUICAO_FUNCIONARIO", dto.ContribuicaoFuncionarioValor);
                AddParameter("PERCENTAGEM_CONTRIBUICAO_FUNCIONARIO", dto.ContribuicaoFuncionarioPercentagem); 
                AddParameter("CONTRIBUICAO_MIN_FUNCIONARIO", dto.ContribuicaoFuncionarioMinValor);
                AddParameter("CONTRIBUICAO_MAX_FUNCIONARIO", dto.ContribuicaoFuncionarioMaxValor);

                AddParameter("VALOR_CONTRIBUICAO_EMPREGADOR", dto.ContribuicaoEmpregadorValor);
                AddParameter("PERCENTAGEM_CONTRIBUICAO_EMPREGADOR", dto.ContribuicaoEmpregadorPercentagem);
                AddParameter("CONTRIBUICAO_MIN_EMPREGADOR", dto.ContribuicaoEmpregadorMinValor);
                AddParameter("CONTRIBUICAO_MAX_EMPREGADOR", dto.ContribuicaoEmpregadorMaxValor);

                AddParameter("UTILIZADOR", dto.Utilizador);


                AddParameter("IS_IRT", dto.IsIRT ? 1 : 0);
                AddParameter("IS_INSS", dto.IsINSS ? 1 : 0);
                AddParameter("IS_VENCIMENTO", dto.IsVencimento ? 1 : 0);
                AddParameter("IS_CALCULADO", dto.IsCalculado ? 1 : 0);
                AddParameter("IS_SUBSIDIO_NATAL", dto.IsSubsidioNatal ? 1 : 0);
                AddParameter("IS_SUBSIDIO_FERIAS", dto.IsSubsidioFerias ? 1 : 0);

                AddParameter("INCIDE_SALARIO_BASE", dto.IncideSalarioBase ? 1 : 0);
                AddParameter("DESCONTA_IRT", dto.DescontaIRT ? 1 : 0);
                 
                AddParameter("DESCONTA_INSS", dto.DescontaINSS ? 1 : 0);
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
         

        public AbonoDescontoDTO Eliminar(AbonoDescontoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_ABONOS_DESCONTOS_EXCLUIR";
                 
               AddParameter("CODIGO", dto.Codigo);
               AddParameter("UTILIZADOR", dto.Utilizador);

                dto.Codigo =ExecuteNonQuery();
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

        public List<AbonoDescontoDTO> ObterPorFiltro(AbonoDescontoDTO dto)
        {
            List<AbonoDescontoDTO> listaBeneficios = new List<AbonoDescontoDTO>();
            try
            {
                ComandText = "stp_RH_ABONOS_DESCONTOS_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("CATEGORIA", dto.Sigla); 
                AddParameter("PROCESSAMENTO_ID", dto.TipoProcessamentoId);

                MySqlDataReader dr = ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new AbonoDescontoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                  
                   if (dr[2].Equals("D"))
                   {
                       dto.Categoria = "DESCONTO";
                   }
                   else
                   {
                       dto.Categoria = "ABONO";
                   }

                   dto.Sigla = dr[3].ToString();
                   dto.Estado = int.Parse(dr[4].ToString());
                    dto.Valor = dr[5].ToString() != "" ? decimal.Parse(dr[5].ToString()) : 0;
                    dto.TipoValor = dr[6].ToString();
                    dto.ContribuicaoFuncionarioValor = dr[7].ToString() != "" ? decimal.Parse(dr[7].ToString()) : 0;
                    dto.ContribuicaoFuncionarioPercentagem = dr[8].ToString() != "" ? decimal.Parse(dr[8].ToString()) : 0;
                    dto.ContribuicaoFuncionarioMinValor = dr[9].ToString() != "" ? decimal.Parse(dr[10].ToString()) : 0;
                    dto.ContribuicaoFuncionarioMaxValor = dr[11].ToString() != "" ? decimal.Parse(dr[11].ToString()) : 0;

                    dto.ContribuicaoEmpregadorValor = dr[12].ToString() != "" ? decimal.Parse(dr[12].ToString()) : 0;
                    dto.ContribuicaoEmpregadorPercentagem = dr[13].ToString() != "" ? decimal.Parse(dr[13].ToString()) : 0;
                    dto.ContribuicaoEmpregadorMinValor = dr[14].ToString() != "" ? decimal.Parse(dr[14].ToString()) : 0;
                    dto.ContribuicaoEmpregadorMaxValor = dr[15].ToString() != "" ? decimal.Parse(dr[15].ToString()) : 0;

                    dto.IsIRT = dr[21].ToString() != "1" ? false : true;
                    dto.IsINSS = dr[22].ToString() != "1" ? false : true;
                    dto.IsVencimento = dr[23].ToString() != "1" ? false : true;
                    dto.IsCalculado = dr[24].ToString() != "1" ? false : true;
                    dto.IsSubsidioNatal = dr[25].ToString() != "1" ? false : true;
                    dto.IsSubsidioFerias = dr[26].ToString() != "1" ? false : true;
                    dto.IncideSalarioBase = dr[27].ToString() != "1" ? false : true;
                    dto.DescontaIRT = dr[28].ToString() != "1" ? false : true;
                    dto.DescontaINSS = dr[29].ToString() != "1" ? false : true;
                    dto.TipoProcessamentoId = int.Parse(dr[30].ToString());

                    listaBeneficios.Add(dto);
                }

            }

            catch (Exception ex)
            {
                 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaBeneficios.Add(dto);
            }
            finally
            {
               FecharConexao();
            }

            return listaBeneficios;
        }

         
        public AbonoDescontoDTO ObterPorPK(AbonoDescontoDTO dto)
        {
            try
            {
                ComandText = "stp_RH_ABONOS_DESCONTOS_OBTERPORPK";

               AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new AbonoDescontoDTO();

                while(dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Categoria = dr[2].ToString();
                    dto.Sigla = dr[3].ToString();
                    dto.Estado = int.Parse(dr[4].ToString());
                    
                   
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
