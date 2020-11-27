using System;
using System.Collections.Generic;
using System.Configuration;
using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class ArmazemDAO
    {
        private readonly ConexaoDB conexao; 
        public ArmazemDAO()
        {
            conexao = new ConexaoDB();
        }
        public ArmazemDTO Adicionar(ArmazemDTO dto)
        {
            try
            {
               conexao.ComandText = "stp_GER_ARMAZEM_ADICIONAR";
                conexao.AddParameter("@CODIGO", dto.Codigo);
                conexao.AddParameter("@DESCRICAO", dto.Descricao);
                conexao.AddParameter("@SIGLA", dto.Sigla);
                conexao.AddParameter("@SITUACAO", dto.Status);
                conexao.AddParameter("@FILIAL", dto.Filial);
                conexao.AddParameter("@UTILIZADOR", dto.Utilizador);
                conexao.AddParameter("@ASM", dto.AlertaStockMinimo == true ? 1 : 0);
                conexao.AddParameter("@PSN", dto.PermiteStockNegativo == true ? 1 : 0);
                conexao.AddParameter("@ASN", dto.AlertaStockNegativo == true ? 1 : 0);
                conexao.AddParameter("@TIPO", dto.Tipo);
                conexao.AddParameter("@POS", dto.EnablePOS == true ? 1  : 0);
                conexao.AddParameter("@INCOME", dto.AllowIncome == true ? 1 : 0);
                conexao.AddParameter("@OUTCOME", dto.AllowOutcome == true ? 1 : 0);
                conexao.AddParameter("@IS_REST", dto.IsForRest == true ? 1 : 0);
                conexao.AddParameter("@TABLE_PRICE", dto.TablePriceID);

                dto.Codigo = conexao.ExecuteInsert();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
               conexao.FecharConexao();
            }

            return dto;
        }

    
        public ArmazemDTO Eliminar(ArmazemDTO dto)
        {
            try
            {
               conexao.ComandText = "stp_GER_ARMAZEM_EXCLUIR";

                conexao.AddParameter("@CODIGO", dto.Codigo);

                dto.Codigo = conexao.ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
               conexao.FecharConexao();
            }

            return dto;
        }

        public List<ArmazemDTO> ObterPorFiltro(ArmazemDTO dto)
        {
            List<ArmazemDTO> lista;
            try
            {
               conexao.ComandText = "stp_GER_ARMAZEM_OBTERPORFILTRO";

                conexao.AddParameter("@DESCRICAO", dto.Descricao ?? string.Empty);
                conexao.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = conexao.ExecuteReader();

                lista = new List<ArmazemDTO>();

                while (dr.Read())
                {
                    dto = new ArmazemDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Filial = dr[2].ToString(),
                        Sigla = dr[3].ToString(),
                        Estado = int.Parse(dr[4].ToString() == "" ? "0" : dr[4].ToString()),
                        AlertaStockMinimo = dr[5].ToString() == "1",
                        PermiteStockNegativo = dr[6].ToString() == "1",
                        AlertaStockNegativo = dr[7].ToString() == "1",
                        Tipo = dr[8].ToString(),
                        EnablePOS = dr[9].ToString() == "1",
                        AllowIncome = dr[10].ToString() == "1",
                        AllowOutcome = dr[11].ToString() == "1",
                        IsForRest = dr[18].ToString() == "1",
                        TablePriceID = int.Parse(dr[19].ToString() == "" ? "-1" : dr[19].ToString()),
                    };

                    dto.Status = dto.Estado;

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ArmazemDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<ArmazemDTO>();
                lista.Add(dto);
            }
            finally
            {
               conexao.FecharConexao();
            }

            return lista;
        }

        public ArmazemDTO ObterForRest(ArmazemDTO dto)
        {     
            try
            {
               conexao.ComandText = "stp_GER_ARMAZEM_ONLYFOR_REST";


                conexao.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = conexao.ExecuteReader();
                 
                while (dr.Read())
                {
                    dto = new ArmazemDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Descricao = dr[1].ToString(),
                        Filial = dr[2].ToString(),
                        Sigla = dr[3].ToString(),
                        Estado = int.Parse(dr[4].ToString() == "" ? "0" : dr[4].ToString()),
                        AlertaStockMinimo = dr[5].ToString() == "1" ? true : false,
                        PermiteStockNegativo = dr[6].ToString() == "1" ? true : false,
                        AlertaStockNegativo = dr[7].ToString() == "1" ? true : false,
                        Tipo = dr[8].ToString(),
                        EnablePOS = dr[9].ToString() != "1" ? false : true,
                        AllowIncome = dr[10].ToString() != "1" ? false : true,
                        AllowOutcome = dr[11].ToString() != "1" ? false : true,
                        IsForRest = dr[18].ToString() != "1" ? false : true,
                        TablePriceID = int.Parse(dr[19].ToString() == "" ? "-1" : dr[19].ToString()),
                    }; 
                    dto.Status = dto.Estado;
                    break;
                }

            }
            catch (Exception ex)
            {
                dto = new ArmazemDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", ""); 
            }
            finally
            {
               conexao.FecharConexao();
            }

            return dto;
        }



        public void AddPermissao(ArmazemDTO dto)
        {
            try
            {
               conexao.ComandText = "stp_GER_ARMAZEM_PERMISSOES_ADICIONAR";
                conexao.AddParameter("@ARMAZEM_ID", dto.Codigo);
                conexao.AddParameter("@UTILIZADOR_ID", dto.UtilizadoID <=0 ? (Object)DBNull.Value : dto.UtilizadoID);
                conexao.AddParameter("@PERFIL_ID", dto.PerfilID <= 0 ? (Object)DBNull.Value : dto.PerfilID);
                conexao.AddParameter("@CADASTRAR", dto.AllowInsert);
                conexao.AddParameter("@ALTERAR", dto.AllowUpdate);
                conexao.AddParameter("@CONSULTAR", dto.AllowSelect);
                conexao.AddParameter("@IMPRIMIR", dto.AllowPrint);
                conexao.AddParameter("@EXCLUIR", dto.AllowDelete);
                conexao.AddParameter("@PERMISSAO", dto.Status);
                conexao.AddParameter("@UTILIZADOR", dto.Utilizador); 

                conexao.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
               conexao.FecharConexao();
            }
        }


        public List<ArmazemDTO> ObterPermissoesPorFiltro(ArmazemDTO dto)
        {
            List<ArmazemDTO> lista;
            try
            {
               conexao.ComandText = "stp_GER_ARMAZEM_OBTERPERMISSOES";

                conexao.AddParameter("@UTILIZADOR", dto.Utilizador);
                conexao.AddParameter("@PERFIL_ID", dto.PerfilID);

                MySqlDataReader dr = conexao.ExecuteReader();

                lista = new List<ArmazemDTO>();

                while (dr.Read())
                {
                    dto = new ArmazemDTO();

                    dto.Codigo = int.Parse(dr[1].ToString()); 
                    dto.AllowInsert = int.Parse(dr[4].ToString()); 
                    dto.AllowUpdate = int.Parse(dr[5].ToString()); 
                    dto.AllowSelect = int.Parse(dr[6].ToString()); 
                    dto.AllowPrint = int.Parse(dr[7].ToString());
                    dto.AllowDelete = int.Parse(dr[8].ToString()); 
                    dto.Status = int.Parse(dr[9].ToString());
                    dto.Descricao = dr[14].ToString();
                    dto.Filial = dr[15].ToString();
                    dto.EnablePOS = dr[16].ToString() == "1" ? true : false;
                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ArmazemDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", "")
                };
                lista = new List<ArmazemDTO>
                {
                    dto
                };
            }
            finally
            {
               conexao.FecharConexao();
            }

            return lista;
        }
    }
}
