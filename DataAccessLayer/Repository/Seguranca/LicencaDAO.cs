using Dominio.Seguranca;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace DataAccessLayer.Seguranca
{
    public class LicencaDAO
    {
        readonly ConexaoDB BaseDados;
        public LicencaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public LicencaDTO Inserir(LicencaDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_SIS_LICENCA_NOVO";

                BaseDados.AddParameter("@CODIGO", dto.LicenseID);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@VALIDATE", dto.ValidateDate);
                BaseDados.AddParameter("@FISCAL_YEAR", dto.FiscalYear);
                BaseDados.AddParameter("@GENERATION", dto.GenereatedDate);
                BaseDados.AddParameter("@HOSTNAME", dto.HostName);
                BaseDados.AddParameter("@MACADDRESS", dto.HostMacAddress);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@DESCRIPTION", dto.LookupNumericField1);
                BaseDados.AddParameter("@LICENSE_TYPE", dto.LicType);

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;

        }

        public LicencaDTO ObterPorLicencaValida(LicencaDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_SIS_LICENCA_OBTERVALIDA";

                BaseDados.AddParameter("@SERVER_NAME", dto.HostName);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new LicencaDTO();
                if (dr.Read())
                {
                    dto.LicenseID = dr[0].ToString();
                    dto.Filial = dr[1].ToString();
                    dto.ValidateDate = dr[2].ToString();
                    dto.FiscalYear = dr[3].ToString();
                    dto.GenereatedDate = dr[4].ToString();
                    dto.HostName = dr[5].ToString();
                    dto.HostMacAddress = dr[6].ToString();
                    dto.Status = int.Parse(dr[7].ToString());
                    dto.MensagemErro = dr[8].ToString();
                    dto.LicType = dr[10].ToString();
                    dto.Sucesso = true;
                }
                else
                {
                    dto.Sucesso = false;
                    dto.MensagemErro = "Licença Inválida";
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;
        }
    }
}
