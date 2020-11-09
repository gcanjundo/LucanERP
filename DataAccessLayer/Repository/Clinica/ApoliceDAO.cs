using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Clinica
{
    public class ApoliceDAO : ConexaoDB
    {
       

        public ApoliceDTO Adicionar(ApoliceDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_APOLICE_ADICIONAR";

                AddParameter("PACIENTE", dto.Beneficiario); 
                AddParameter("CONVENIO", dto.Convenio); 
                if (dto.Emissao != DateTime.MinValue)
                {
                    AddParameter("EMISSAO", dto.Emissao);
                }
                else 
                {
                    AddParameter("EMISSAO", DBNull.Value);
                }

                if (dto.Validade != DateTime.MinValue)
                {
                    AddParameter("VALIDADE", dto.Validade);
                }
                else
                {
                    AddParameter("VALIDADE", DBNull.Value);
                }
                AddParameter("APOLICE", dto.NumeroBeneficiario);
                AddParameter("NUM_PS", dto.NumPS);
                AddParameter("TITULAR", dto.IsVitalicio); 

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

        public ApoliceDTO Eliminar(ApoliceDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_APOLICE_EXCLUIR";

                AddParameter("PACIENTE", dto.Beneficiario);
                AddParameter("CONVENIO", dto.Convenio);
                 

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

        public List<ApoliceDTO> ObterPorFiltro(ApoliceDTO dto) 
        {
            List<ApoliceDTO> lista;
            try
            {
                ComandText = "stp_CLI_APOLICE_OBTERPORFILTRO";

                AddParameter("PACIENTE", dto.Beneficiario);
                AddParameter("CONVENIO", dto.Convenio);

                MySqlDataReader dr = ExecuteReader();
                lista = new List<ApoliceDTO>();
                while (dr.Read()) 
                {
                    dto = new ApoliceDTO();

                    dto.Convenio = int.Parse(dr[0].ToString());
                    dto.Beneficiario = int.Parse(dr[1].ToString());
                    dto.NumeroBeneficiario = dr[2].ToString();
                    if (dr[2].ToString() != null && !dr[2].ToString().Equals(""))
                    {
                        dto.Emissao = Convert.ToDateTime(dr[2].ToString());
                    }

                    if (dr[3].ToString() != null && !dr[3].ToString().Equals(""))
                    {
                        dto.Validade = Convert.ToDateTime(dr[3].ToString());
                    }
                     
                    dto.NumPS = dr[4].ToString(); 
                    if (dr[5].ToString() != null && dr[5].ToString() =="1") 
                    {
                        dto.IsVitalicio = true;
                    }

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new ApoliceDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", ""); 
                lista = new List<ApoliceDTO>();
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
