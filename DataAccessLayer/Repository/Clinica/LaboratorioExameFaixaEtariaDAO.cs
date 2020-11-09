using Dominio.Clinica;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Clinica
{
    public class LaboratorioExameFaixaEtariaDAO : ConexaoDB
    {
        public LaboratorioExameFaixaEtariaDTO Adicionar(LaboratorioExameFaixaEtariaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_FAIXA_ETARIA_ADICIONAR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);
                AddParameter("@IDADE_INICIAL", dto.IdadeInicial);
                AddParameter("@IDADE_FINAL", dto.IdadeFinal);
                AddParameter("@UNIDADE", dto.UnidadeFaixa);
                AddParameter("@SEXO", dto.Sexo);
                AddParameter("@ESTADO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador ?? string.Empty);

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
 
        public LaboratorioExameFaixaEtariaDTO Eliminar(LaboratorioExameFaixaEtariaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_FAIXA_ETARIA_EXCLUIR";

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

        public List<LaboratorioExameFaixaEtariaDTO> ObterPorFiltro(LaboratorioExameFaixaEtariaDTO dto)
        {
            List<LaboratorioExameFaixaEtariaDTO> lista = new List<LaboratorioExameFaixaEtariaDTO>();
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_FAIXA_ETARIA_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao?? string.Empty); 

                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new LaboratorioExameFaixaEtariaDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.IdadeInicial = int.Parse(dr[3].ToString());
                    dto.IdadeFinal = int.Parse(dr[4].ToString());
                    dto.UnidadeFaixa = dr[5].ToString();
                    dto.Sexo = dr[6].ToString();
                    dto.Estado = int.Parse(dr[7].ToString());

                    dto.LookupField2 = FaixaIdade(dto.UnidadeFaixa);
                    dto.LookupField1 = dto.Descricao.ToUpper() + " DE " + dto.IdadeInicial.ToString() + " À " + dto.IdadeFinal + " " + dto.LookupField2;

                    lista.Add(dto);
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

            return lista;
        }

        public LaboratorioExameFaixaEtariaDTO ObterPorPK(LaboratorioExameFaixaEtariaDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_LABORATORIO_EXAME_FAIXA_ETARIA_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo); 

                MySqlDataReader dr = ExecuteReader();

                dto = new LaboratorioExameFaixaEtariaDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.IdadeInicial = int.Parse(dr[3].ToString());
                    dto.IdadeFinal = int.Parse(dr[4].ToString());
                    dto.UnidadeFaixa = dr[5].ToString();
                    dto.Sexo = dr[6].ToString();
                    dto.Estado = int.Parse(dr[7].ToString()); 
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

        string FaixaIdade(string pUnidade)
        {
            if(pUnidade == "M")
            {    
                return "MESES";
            }
            else if(pUnidade == "D")
            {
                return "DIAS";
            }else
            {
                return "ANO(S)";
            }
        }
    }
}
