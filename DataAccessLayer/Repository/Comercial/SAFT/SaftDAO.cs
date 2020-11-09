using Dominio.Comercial.SAFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Comercial.SAFT
{
    public class SaftDAO
    {
        ConexaoDB bdContext;

        public SaftDAO()
        {
            bdContext = new ConexaoDB();
        }

        public SaftDTO Adicionar(SaftDTO dto)
        {
            try
            {
                bdContext.ComandText = "stp_SYS_SAFT_EXPORTACAO_ADICIONAR";

                bdContext.AddParameter("@SAFT_ID", dto.SaftID);
                bdContext.AddParameter("@BEGIN_DATE", dto.DateFrom);
                bdContext.AddParameter("@END_DATE", dto.DateUntil);
                bdContext.AddParameter("@FILE_TYPE", dto.FileType);
                bdContext.AddParameter("@SAFT_STATUS", dto.Status);
                bdContext.AddParameter("@NOTES", dto.Notes);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);
                bdContext.AddParameter("@COMPANY_ID", dto.Filial);
                bdContext.AddParameter("@FISCAL_YEAR", dto.FiscalYear);

                bdContext.ExecuteNonQuery() ;
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao(); 
            }

            return dto;
        }

        public SaftDTO Validar(SaftDTO dto)
        {
            try
            {
                bdContext.ComandText = "stp_SYS_SAFT_EXPORTACAO_VALIDAR";

                bdContext.AddParameter("@SAFT_ID", dto.SaftID);
                bdContext.AddParameter("@BEGIN_DATE", dto.DateFrom);
                bdContext.AddParameter("@END_DATE", dto.DateUntil);
                bdContext.AddParameter("@FILE_TYPE", dto.FileType);
                bdContext.AddParameter("@SAFT_STATUS", dto.Status);
                bdContext.AddParameter("@NOTES", dto.Notes);
                bdContext.AddParameter("@UTILIZADOR", dto.Utilizador);
                bdContext.AddParameter("@COMPANY_ID", dto.Filial);
                bdContext.AddParameter("@FISCAL_YEAR", dto.FiscalYear);

                bdContext.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }

            return dto;
        }

        public List<SaftDTO> ObterPorFiltro(SaftDTO dto)
        { 
            List<SaftDTO> lista = new List<SaftDTO>();
            try
            {
                bdContext.ComandText = "stp_SYS_SAFT_EXPORTACAO_OBTERPORFILTRO";

                bdContext.AddParameter("@SAFT_ID", dto.SaftID);
                bdContext.AddParameter("@BEGIN_DATE", dto.DateFrom);
                bdContext.AddParameter("@END_DATE", dto.DateUntil); 
                bdContext.AddParameter("@COMPANY_ID", dto.Filial);
                bdContext.AddParameter("@FILE_TYPE", dto.FileType); 
                bdContext.AddParameter("@SAFT_STATUS", dto.Status);

                MySqlDataReader dr = bdContext.ExecuteReader();

                while (dr.Read())
                {
                    dto = new SaftDTO
                    {
                        SaftID = int.Parse(dr[0].ToString()),
                        DateFrom = DateTime.Parse(dr[1].ToString()),
                        DateUntil = DateTime.Parse(dr[2].ToString()),
                        FileType = dr[3].ToString().ToUpper(),
                        Status = int.Parse(dr[4].ToString()),
                        Notes = dr[5].ToString(),
                        CreatedBy = dr[6].ToString(),
                        CreatedDate =DateTime.Parse(dr[7].ToString()),
                        UpdatedBy = dr[8].ToString(),
                        UpdatedDate = DateTime.Parse(dr[9].ToString()),
                        Filial = dr[10].ToString(),
                        FiscalYear = int.Parse(dr[11].ToString()), 
                    };
                     
                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                bdContext.FecharConexao();
            }
            return lista;
        }
    }
}
