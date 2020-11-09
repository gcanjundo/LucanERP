using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using Dominio.GestaoEscolar.Faturacao;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Faturacao
{
    public class ParcelaMensalidadeDAO 
    {
        readonly ConexaoDB BaseDados;

        public ParcelaMensalidadeDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public int Inserir(ParcelaMensalidadeDTO dto)
        {
            int codigo = 0;
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_PARCELA_ADICIONAR"; 
            
            try
            {

                BaseDados.AddParameter("@MENSALIDADE", dto.Mensalidade.Codigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@MES", dto.Mes);
                BaseDados.AddParameter("@PERIODO", dto.Data);
                BaseDados.AddParameter("@VALOR_MENSAL", dto.ValorUnitario);
                BaseDados.AddParameter("@COBRA_MULTA", dto.CobraMulta == true ? 1 : 0); 
                BaseDados.AddParameter("@DATA_PAGAMENTO", dto.DataLimite);
                BaseDados.AddParameter("@ACTIVA", dto.Activa == true ? 1 : 0);

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
            return codigo;
        }

        public int Alterar(ParcelaMensalidadeDTO dto)
        {
            int codigo = 0;
            
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_PARCELA_ALTERAR";

                BaseDados.AddParameter("@MENSALIDADE", dto.Mensalidade.Codigo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@MES", dto.Mes);
                BaseDados.AddParameter("@PERIODO", dto.Data);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@VALOR_MENSAL", dto.ValorUnitario);
                BaseDados.AddParameter("@COBRA_MULTA", dto.CobraMulta == true ? 1 : 0);
                BaseDados.AddParameter("@DATA_PAGAMENTO", dto.DataLimite);
                BaseDados.AddParameter("@ACTIVA", dto.Activa == true ? 1 : 0);

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
            return codigo;
        }

        public int Excluir(ParcelaMensalidadeDTO dto)
        {
            int codigo = 0;
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_PARCELA_EXCLUIR";
            

            
            
            BaseDados.AddParameter("@CODIGO", dto.Codigo);

            try
            {
                
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
            return codigo;
        }


        public ParcelaMensalidadeDTO ObterPorPK(ParcelaMensalidadeDTO dto)
        {
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_PARCELA_OBTERPORPK"; 
            
            BaseDados.AddParameter("@CODIGO", dto.Codigo);
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new ParcelaMensalidadeDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["MENS_PAR_CODIGO"].ToString());
                    dto.Descricao = dr["MENS_PAR_DESCRICAO"].ToString();
                    dto.Data = dr["MENS_PAR_DATA"].ToString();
                    MensalidadeDTO mensalidade = new MensalidadeDTO();
                    mensalidade.Codigo = int.Parse(dr["MENS_PAR_CODIGO_MENSALIDADE"].ToString());
                    MensalidadeDAO daoItem = new MensalidadeDAO();
                    mensalidade = daoItem.ObterPorPK(mensalidade);

                    if (!dr["MENS_PAR_MES"].ToString().Equals(DBNull.Value)) 
                    {
                        dto.Mes = int.Parse(dr["MENS_PAR_MES"].ToString());
                    }

                    if (!dr["MENS_PAR_VALOR_MENSAL"].ToString().Equals(DBNull.Value))
                    {
                        dto.ValorUnitario = decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString());
                    }

                    dto.CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1" ? true : false;

                    dto.Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1" ? true : false;

                    dto.Mensalidade=mensalidade;


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

        public Boolean JaExiste(ParcelaMensalidadeDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_PARCELA_OBTERDUPLICADA";


                BaseDados.AddParameter("@MENSALIDADE", dto.Mensalidade.Codigo);
                BaseDados.AddParameter("@DATA", dto.Data);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new ParcelaMensalidadeDTO();
                while (dr.Read())
                {
                    dto.Sucesso = true;
                    break;

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

            return dto.Sucesso;




        }

        public ListaParcelasMensalidadesDTO ObterPorFiltro(ParcelaMensalidadeDTO dto)
        {
           
            ListaParcelasMensalidadesDTO lista = new ListaParcelasMensalidadesDTO();
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_PARCELA_OBTERPORFILTRO";

                BaseDados.AddParameter("@MENSALIDADE", dto.Mensalidade.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new ParcelaMensalidadeDTO();
                    dto.Codigo = int.Parse(dr["MENS_PAR_CODIGO"].ToString());
                    dto.Descricao = dr["MENS_PAR_DESCRICAO"].ToString();
                    dto.Data = dr["MENS_PAR_DATA"].ToString();

                    MensalidadeDTO mensalidade = new MensalidadeDTO();
                    mensalidade.Codigo = int.Parse(dr["MENS_PAR_CODIGO_MENSALIDADE"].ToString());
                    MensalidadeDAO daoItem = new MensalidadeDAO();
                    mensalidade = daoItem.ObterPorPK(mensalidade);
                    dto.Mensalidade=mensalidade;
                    if (!dr["MENS_PAR_MES"].ToString().Equals(DBNull.Value))
                    {
                        dto.Mes = int.Parse(dr["MENS_PAR_MES"].ToString());
                    }

                    if (!dr["MENS_PAR_VALOR_MENSAL"].ToString().Equals(DBNull.Value))
                    {
                        dto.ValorUnitario = decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString());
                    }

                    dto.CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1" ? true : false;

                    dto.Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1" ? true : false;

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
    }
}
