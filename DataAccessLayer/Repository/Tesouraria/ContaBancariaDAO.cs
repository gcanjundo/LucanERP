using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.Tesouraria;
using MySql.Data.MySqlClient;
using DataAccessLayer.Geral;
using Dominio.Geral;


namespace DataAccessLayer.Tesouraria
{
    public class ContaBancariaDAO: ConexaoDB
    {
        public ContaBancariaDTO Inserir(ContaBancariaDTO dto)
        {
           
            
            try
            {
                ComandText = "stp_FIN_CONTA_BANCARIA_ADICIONAR";

                AddParameter("@NUMERO", dto.NumeroConta);
                AddParameter("@CODIGO_BANCO", dto.Banco);
                AddParameter("@CODIGO_MOEDA", dto.Moeda);
                AddParameter("@ESTADO", dto.Situacao);
                AddParameter("@IBAN", dto.IBAN);
                AddParameter("@NIB", dto.NIB);
                AddParameter("@SALDO", dto.Saldo);
                AddParameter("@CONT_SWIFT", dto.Swift);
                AddParameter("@DESCRICAO", dto.Descricao);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@CORRENTE", "S");
                AddParameter("@TIPO", dto.Tipo);
                AddParameter("@ENTITY", dto.Entidade);
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@BENEFICIARIO", dto.Beneficiario);

                dto.MensagemErro = string.Empty;
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

        public ContaBancariaDTO Alterar(ContaBancariaDTO dto)
        {

            ComandText = "stp_FIN_CONTA_BANCARIA_ALTERAR";

            AddParameter("@CONTA", dto.NumeroConta); 
            AddParameter("@SALDO", dto.Saldo);
            AddParameter("@FILIAL", dto.Filial);
            AddParameter("@CONT_SWIFT", dto.Swift);
            AddParameter("@CONT_CODIGO", dto.Codigo);

            try
            {

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
        
        public ContaBancariaDTO Excluir(ContaBancariaDTO dto)
        {
             
            ComandText = "stp_FIN_CONTA_BANCARIA_EXCLUIR";
                        
            
            AddParameter("@NUMERO", dto.NumeroConta);
            AddParameter("@FILIAL", dto.Filial);
            try
            {
                
                ExecuteNonQuery();
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

        public ContaBancariaDTO ObterPorPK(ContaBancariaDTO dto)
        {
            ComandText = "stp_FIN_CONTA_BANCARIA_OBTERPORPK";
            
            AddParameter("@CODIGO", dto.Codigo);
            

            try
            {
                
                MySqlDataReader dr = ExecuteReader();
                
                while (dr.Read())
                {
                    dto = new ContaBancariaDTO();

                    dto.Codigo = int.Parse(dr["CONT_ID"].ToString());
                    dto.NumeroConta = dr["CONT_NUMERO"].ToString().TrimEnd();

                    BancoDTO banco = new BancoDTO(int.Parse(dr["CONT_CODIGO_BANCO"].ToString()), "", dr["BANC_SIGLA"].ToString());
                    dto.Banco = banco.Codigo;
                    dto.NomeBanco = banco.NomeComercial;

                    MoedaDTO moeda = new MoedaDTO(int.Parse(dr["CONT_CODIGO_MOEDA"].ToString()), dr["MOE_DESCRICAO"].ToString(), dr["MOE_SIGLA"].ToString());

                    dto.Moeda = moeda.Codigo;
                    dto.SiglaMoeda = moeda.Descricao.ToUpper() + " " + moeda.Sigla;
                    dto.IBAN = dr["CONT_IBAN"].ToString();
                    dto.NIB = dr["CONT_NIB"].ToString();
                    dto.Situacao = dr["CONT_STATUS"].ToString();
                    dto.Saldo = Convert.ToDecimal(dr["CONT_SALDO"].ToString());
                    dto.Descricao = dr["CONT_DESCRICAO"].ToString();
                    dto.Tipo = dr["CONT_TIPO"].ToString();
                    dto.Entidade = int.Parse(dr["CONT_ENTITY_ID"].ToString());
                    break;
                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = true;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;

        }

        public List<ContaBancariaDTO> ObterPorFiltro(ContaBancariaDTO dto)
        {
            ComandText = "stp_FIN_CONTA_BANCARIA_OBTERPORFILTRO"; 
            
            AddParameter("@NUMERO", dto.NumeroConta);
            AddParameter("@BANCO", dto.Banco);
            AddParameter("@MOEDA", dto.Moeda);
            AddParameter("@SIGLA", dto.NomeBanco);
            AddParameter("@FILIAL", dto.Filial);

            List<ContaBancariaDTO> lista = new List<ContaBancariaDTO>();
            try
            {
                
                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new ContaBancariaDTO();

                    dto.NumeroConta = dr["CONT_NUMERO"].ToString();
                    BancoDTO banco = new BancoDTO(int.Parse(dr["CONT_CODIGO_BANCO"].ToString()), "", dr["BANC_SIGLA"].ToString());
                    dto.Banco = banco.Codigo;
                    dto.NomeBanco = banco.NomeComercial;
                    
                     
                    MoedaDTO moeda = new MoedaDTO(int.Parse(dr["CONT_CODIGO_MOEDA"].ToString()), dr["MOE_DESCRICAO"].ToString(), dr["MOE_SIGLA"].ToString());

                    dto.Moeda = moeda.Codigo;
                    dto.SiglaMoeda = moeda.Sigla;
                    dto.IBAN = dr["CONT_IBAN"].ToString();
                    dto.NIB = dr["CONT_NIB"].ToString();
                    dto.Situacao = dr["CONT_STATUS"].ToString();
                    dto.Saldo = Convert.ToDecimal(dr["CONT_SALDO"].ToString());
                    dto.Descricao = dr["CONT_DESCRICAO"].ToString();
                    dto.Tipo = dr["CONT_TIPO"].ToString();
                    dto.Entidade = int.Parse(dr["CONT_ENTITY_ID"].ToString());
                    dto.Codigo = int.Parse(dr["CONT_ID"].ToString());
                    dto.Filial = dr["CONT_FILIAL"].ToString();
                    dto.AccountType = dr["DESCRICAO"].ToString();
                    dto.Beneficiario = dr["CONT_BENEFICIARIO"].ToString();
                    
                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = true;
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
