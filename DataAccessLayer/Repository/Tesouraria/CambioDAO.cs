using DataAccessLayer.Geral;
using Dominio.Tesouraria;
using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Tesouraria
{
    public class CambioDAO:ConexaoDB
    {
        public CambioDTO Inserir(CambioDTO dto)
        { 

            try
            {
                ComandText = "stp_FIN_CAMBIO_ADICIONAR";

                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@CAMBIO", dto.CambioCompra);
                AddParameter("@INICIO", dto.Inicio);
                if (dto.Termino != DateTime.MinValue)
                {
                    AddParameter("@TERMINO", dto.Termino);
                }
                else
                {
                    AddParameter("@TERMINO", DBNull.Value);
                }

                AddParameter("@FILIAL", dto.Filial); 


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

        public CambioDTO Alterar(CambioDTO dto)
        {


           
            try
            {

                ComandText = "stp_FIN_CAMBIO_ALTERAR";


                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@CAMBIO", dto.CambioCompra);
                AddParameter("@INICIO", dto.Inicio);
                if (dto.Termino != DateTime.MinValue)
                {
                    AddParameter("@TERMINO", dto.Termino);
                }
                else
                {
                    AddParameter("@TERMINO", DBNull.Value);
                }
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

        public CambioDTO Excluir(CambioDTO dto)
        {

           

            try
            {
                ComandText = "stp_FIN_CAMBIO_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo);

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

        public CambioDTO ObterPorPK(CambioDTO dto)
        {
          
            try
            {
                ComandText = "stp_FIN_CAMBIO_OBTERPORPK";


                AddParameter("@CODIGO", dto.Codigo);
                MySqlDataReader dr = ExecuteReader();
                dto = new CambioDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["CAM_CODIGO"].ToString());
                    dto.Moeda = dr["CAM_CODIGO_MOEDA"].ToString();
                    dto.CambioCompra = Convert.ToDecimal(dr["CAM_CAMBIO"].ToString());
                    dto.Inicio = Convert.ToDateTime(dr["CAM_INICIO"].ToString());
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

        public CambioDTO ObterCambioActual(CambioDTO dto)
        {
            try
            {

                ComandText = "stp_FIN_CAMBIO_OBTERACTUAL";

                AddParameter("@MOEDA", dto.Moeda);
                AddParameter("@FILIAL", dto.Filial);
                MySqlDataReader dr = ExecuteReader();
                dto = new CambioDTO();
                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["CAM_CODIGO"].ToString());
                    dto.Moeda = dr["CAM_CODIGO_MOEDA"].ToString();
                    dto.CambioCompra = Convert.ToDecimal(dr["CAM_CAMBIO"].ToString());
                    dto.Inicio = Convert.ToDateTime(dr["CAM_INICIO"].ToString());
                    dto.Descricao = dr["MOE_DESCRICAO"].ToString().ToUpper() + " " + dr["MOE_SIGLA"].ToString().ToUpper();

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

        public List<CambioDTO> ObterPorFiltro(CambioDTO dto)
        {

            List<CambioDTO> lista = new List<CambioDTO>();
            try
            {
                ComandText = "stp_FIN_CAMBIO_OBTERPORFILTRO";
                AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new CambioDTO();
                    dto.Codigo = int.Parse(dr["CAM_CODIGO"].ToString());
                    dto.Moeda = dr["CAM_CODIGO_MOEDA"].ToString();
                    dto.CambioCompra = Convert.ToDecimal(dr["CAM_CAMBIO"].ToString());
                    dto.Inicio = Convert.ToDateTime(dr["CAM_INICIO"].ToString());
                    dto.Descricao = dr["MOE_DESCRICAO"].ToString().ToUpper() + " " + dr["MOE_SIGLA"].ToString().ToUpper();
                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                lista = new List<CambioDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista.Add(dto);

            }
            finally
            {
                FecharConexao();
            }
            return lista;
        }
    }
}
