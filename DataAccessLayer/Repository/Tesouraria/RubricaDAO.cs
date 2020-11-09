
using Dominio.Comercial;
using Dominio.Contabilidade;
using Dominio.Tesouraria; 
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic; 

namespace DataAccessLayer.Tesouraria
{
    public class RubricaDAO : ConexaoDB
    {

        public RubricaDTO Gravar(RubricaDTO dto, List<MovimentoPlanoContaDTO> pPlanAccountList, List<DocumentoComercialDTO> pDocumentsList)
        {
            try
            {
                ComandText = "stp_FIN_CONTAS_TESOURARIA_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@RUBRICA_ID", dto.RubricaID);
                AddParameter("@CLASSIFICACAO", dto.Classificacao);
                AddParameter("@DESCRICAO", dto.Designacao);
                AddParameter("@NATUREZA", dto.Natureza);
                AddParameter("@PLANO_CONTAS", dto.PlanGeralContaID);
                AddParameter("@MOVIMENTO", dto.Movimento);
                AddParameter("@AGRUPAMENTO", dto.Agrupamento);
                AddParameter("@DRE", dto.IncideDRE);
                AddParameter("@DESTINO", dto.Destino);
                AddParameter("@ISFIXA", dto.IsFixa);
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

                foreach(var account in pPlanAccountList)
                {

                }
            }

            return dto;
        }

        public RubricaDTO Excluir(RubricaDTO dto)
        {
            try
            {
                ComandText = "stp_FIN_CONTAS_TESOURARIA_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo); 
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

        

        public List<RubricaDTO> ObterPorFiltro()
        {
            List<RubricaDTO> lista = new List<RubricaDTO>();
            RubricaDTO dto;
            try
            {
                ComandText = "stp_FIN_CONTAS_TESOURARIA_OBTERPORFILTRO";
                 

                MySqlDataReader dr = ExecuteReader();  

                while (dr.Read())
                {
                    dto = new RubricaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.RubricaID = int.Parse(dr[1].ToString() == string.Empty ? "-1" : dr[1].ToString());
                    dto.Classificacao = dr[2].ToString();
                    dto.Designacao = dr[3].ToString();
                    dto.Natureza = dr[4].ToString();
                    dto.PlanGeralContaID = int.Parse(dr[5].ToString() == "" ? "-1" : dr[5].ToString());
                    dto.Movimento = dr[6].ToString();
                    dto.Agrupamento = int.Parse(dr[7].ToString() == "" ? "-1" : dr[7].ToString());
                    dto.IncideDRE = dr[8].ToString() != "1" ? false : true;
                    dto.Destino = dr[9].ToString();
                    dto.IsFixa = dr[10].ToString() != "1" ? false : true;
                    dto.DesignacaoEntidade = dr[15].ToString();
                    dto.LookupField1 = dto.Classificacao + " - " + dto.Designacao;
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new RubricaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public MovimentoPlanoContaDTO AddAccount(MovimentoPlanoContaDTO dto)
        {
            try
            {
                ComandText = "stp_TES_FLUXO_INTEGRACAO_CONTABILIDADE_ADICIONAR";

                AddParameter("@FLUXO_ID", dto.FluxoCaixaID);
                AddParameter("@PLANO_CONTA_ID", dto.PlanoContaID);
                AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public List<MovimentoPlanoContaDTO> ObterPorFiltro(RubricaDTO pRubrica)
        {
            List<MovimentoPlanoContaDTO> lista = new List<MovimentoPlanoContaDTO>();
            MovimentoPlanoContaDTO dto;
            try
            {
                ComandText = "stp_TES_FLUXO_INTEGRACAO_OBTERPORFILTRO";


                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new MovimentoPlanoContaDTO();
                    dto.FluxoCaixaID = int.Parse(dr[0].ToString());
                    dto.PlanoContaID = int.Parse(dr[1].ToString());
                    dto.PlanoConta = new PlanoContaDTO
                    {
                        Codigo = int.Parse(dr[1].ToString()),
                        Conta = dr[2].ToString(),
                        Descricao = dr[3].ToString()

                    };
                    dto.LookupField1 = dr[2].ToString()+" - "+ dr[3].ToString();
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new MovimentoPlanoContaDTO();
                dto.Sucesso = false;
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
