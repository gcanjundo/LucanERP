using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.RecursosHumanos
{
    public class CargoDAO 
    {
        readonly ConexaoDB BaseDados;

        public CargoDAO()
        {
            BaseDados = new ConexaoDB();
        }


        public CargoDTO Adicionar(CargoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_CARGO_ADICIONAR";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado); 
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                dto.Codigo = BaseDados.ExecuteInsert();
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

        public CargoDTO Alterar(CargoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_CARGO_ALTERAR";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado); 
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);
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

        public CargoDTO Eliminar(CargoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_CARGO_EXCLUIR";
                 
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

        public List<CargoDTO> ObterPorFiltro(CargoDTO dto)
        {
            List<CargoDTO> listaDepartamentos = new List<CargoDTO>();
            try
            {
                BaseDados.ComandText = "stp_RH_CARGO_OBTERPORFILTRO";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();

               

                while(dr.Read())
                {
                   dto = new CargoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Descricao = dr[1].ToString();
                   dto.Sigla = dr[2].ToString();
                   dto.Estado = int.Parse(dr[3].ToString()); 
                   listaDepartamentos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaDepartamentos.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaDepartamentos;
        }

        public CargoDTO ObterPorPK(CargoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_CARGO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new CargoDTO();

                while(dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString()); 

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
