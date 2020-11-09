using System;
using System.Collections.Generic;
using Dominio.GestaoEscolar.Faturacao;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Faturacao
{
    public class BolsaDAO
    {
        readonly ConexaoDB BaseDados;

        public BolsaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public BolsaDTO Adicionar(BolsaDTO dto)
        {
            BaseDados.ComandText = "stp_FIN_BOLSA_ADICIONAR";

            try
            {


                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@ENTIDADE", dto.Entidade);
                BaseDados.AddParameter("@INICIO", dto.ValidationStartDate);

                if (dto.ValidationEndDate != null && dto.ValidationEndDate != DateTime.MinValue)
                {
                    BaseDados.AddParameter("@TERMINO", dto.ValidationEndDate);
                }
                else
                {
                    BaseDados.AddParameter("@TERMINO", DBNull.Value);
                }
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.AddParameter("@CRITERIO", string.IsNullOrEmpty(dto.Criterio) || dto.Criterio == "-1" ? "OT" : dto.Criterio);
                BaseDados.AddParameter("ANO", dto.AnoLectivo);
                BaseDados.AddParameter("QUANTIDADE", dto.Quantidade);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("REMOVED", dto.RemovedIfLate == true ? 1 : 0);

                dto.Codigo = BaseDados.ExecuteInsert();

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

        public BolsaDTO Excluir(BolsaDTO dto)
        {
            BaseDados.ComandText = "stp_FIN_BOLSA_EXCLUIR";

            try
            {

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();

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

        public BolsaDTO ObterPorPK(BolsaDTO dto)
        {

           

            try
            {
                BaseDados.ComandText = "stp_FIN_BOLSA_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);


                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new BolsaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString(); 
                    dto.Entidade = int.Parse(dr[3].ToString());
                    dto.ValidationStartDate = Convert.ToDateTime(dr[4].ToString());
                    if (dr[5].ToString() != "")
                    {
                        dto.ValidationStartDate = Convert.ToDateTime(dr[5]);
                    }
                    else
                    {
                        dto.ValidationStartDate = DateTime.MaxValue;
                    }
                    dto.DesignacaoEntidade = dr[3].ToString() + " - " + dr[6].ToString();
                    dto.Criterio = dr[7].ToString();
                    dto.AnoLectivo = int.Parse(dr[8].ToString());
                    dto.RemovedIfLate = dr[9].ToString() != "1" ? false : true;
                    
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

        public List<BolsaDTO> ObterPorFiltro(BolsaDTO dto)
        {
            List<BolsaDTO> lista = new List<BolsaDTO>();
            try
            {
                BaseDados.ComandText = "stp_FIN_BOLSA_OBTERPORFILTRO";

                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new BolsaDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.DesignacaoEntidade = dr[2].ToString(); 
                    dto.Situacao = dr[6].ToString();
                    dto.RemovedIfLate = dr[7].ToString() != "1" ? false : true;
                    dto.AnoLectivo = int.Parse(dr[8].ToString());
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
                BaseDados.FecharConexao();
            }
            return lista;
        }

        public List<AlunoDTO> ObterListaBolseiros(BolsaDTO bolsa)
        {
            List<AlunoDTO> lista = new List<AlunoDTO>();
            AlunoDTO dto = new AlunoDTO();
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_BOLSEIROS";

                BaseDados.AddParameter("@ANO", bolsa.AnoLectivo);
                BaseDados.AddParameter("@TIPO", bolsa.Tipo);
                BaseDados.AddParameter("@FILIAL", bolsa.Filial);
                BaseDados.AddParameter("@ENTIDADE", bolsa.Entidade);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new AlunoDTO
                    {
                        Inscricao = dr[0].ToString(),
                        NomeCompleto = dr[1].ToString(),
                        Descricao = dr[2].ToString(),
                        Curso = dr[4].ToString(),
                        ClasseProveniencia = dr[5].ToString(),
                        Classe = int.Parse(dr[6].ToString()),
                        Matricula = int.Parse(dr[7].ToString()),
                        Codigo = int.Parse(dr[8].ToString()),
                        SaldoCorrente = decimal.Parse(dr[9].ToString())
                    };
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                bolsa.Sucesso = false;
                bolsa.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
