using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.Seguranca;

namespace DataAccessLayer.Seguranca
{
    public class EmailMonitorDAO 
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public EmailMonitorDTO Adicionar(EmailMonitorDTO dto) 
        {
           
            try
            {
                 

                BaseDados.ComandText = "stp_SIS_EMAIL_MONITOR_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@REMETENTE", dto.Remetente);
                BaseDados.AddParameter("@ENDERECO", dto.Endereco);
                BaseDados.AddParameter("@SERVIDOR", dto.Servidor);
                BaseDados.AddParameter("@USUARIO", dto.Usuario);
                BaseDados.AddParameter("@SENHA", dto.CurrentPassword);
                BaseDados.AddParameter("@PORTA", dto.Porta);
                BaseDados.AddParameter("@ENABLE_SSL", dto.AtivaSSL ? 1 : 0);
                BaseDados.AddParameter("@DEFAULT_CREDENTIALS", dto.UseDefaultCredencial ? 1 : 0);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.ExecuteNonQuery();
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

        public EmailMonitorDTO Apagar(EmailMonitorDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_SIS_EMAIL_MONITOR_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                BaseDados.ExecuteNonQuery();
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

        public EmailMonitorDTO ObterPorCodigo(EmailMonitorDTO dto) 
        {
           
            try
            {
                BaseDados.ComandText = "stp_SIS_EMAIL_MONITOR_OBTERPORPK";

                BaseDados.AddParameter("@CODIGO", dto.Codigo); 
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new EmailMonitorDTO();
                if (dr.Read()) 
                {
                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.Servidor = dr[1].ToString();
                    dto.Remetente = dr[2].ToString();
                    dto.Endereco = dr[3].ToString();
                    dto.Usuario = dr[4].ToString();
                    dto.CurrentPassword = dr[5].ToString();
                    dto.Porta = dr[6].ToString() != string.Empty ? int.Parse(dr[6].ToString()) : 0;
                    dto.AtivaSSL = dr[7].ToString() != "1" ? false : true;
                    dto.UseDefaultCredencial = dr[8].ToString() != "1" ? false : true;
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

        public List<EmailMonitorDTO> ObterPorFiltro(EmailMonitorDTO dto)
        {
            List<EmailMonitorDTO> lista;
            
            try
            {
                BaseDados.ComandText = "stp_SIS_EMAIL_MONITOR_OBTERPORFILTRO";


                BaseDados.AddParameter("@REMETENTE", dto.Remetente);
                BaseDados.AddParameter("@ENDERECO", dto.Endereco);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<EmailMonitorDTO>();
                while (dr.Read())
                {
                    dto = new EmailMonitorDTO();

                    dto.Codigo = int.Parse(dr[0].ToString()); 
                    dto.Servidor = dr[1].ToString();
                    dto.Remetente = dr[2].ToString();
                    dto.Endereco = dr[3].ToString();
                    dto.Usuario = dr[4].ToString();
                    dto.CurrentPassword = dr[5].ToString();
                    dto.Porta = dr[6].ToString() != string.Empty ? int.Parse(dr[6].ToString()) : 0;
                    dto.AtivaSSL = dr[7].ToString() != "1" ? false : true;
                    dto.UseDefaultCredencial = dr[8].ToString() != "1" ? false : true; 
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new EmailMonitorDTO();
                lista = new List<EmailMonitorDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Remetente = dto.MensagemErro;
                lista.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
                
            }
            return lista;

        }


    }
}
