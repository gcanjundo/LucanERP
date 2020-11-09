using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using MySql.Data.MySqlClient; 

namespace DataAccessLayer.Seguranca
{
    public class EmpresaDAO
    {

        private readonly ConexaoDB BaseDados;

        public EmpresaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public EmpresaDTO Adicionar(EmpresaDTO dto)
        {


            try
            {

                BaseDados.ComandText = "stp_SIS_EMPRESA_ADICIONAR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("NOME_COMPLETO", dto.NomeCompleto);
                if (dto.DataNascimento.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", dto.DataNascimento);
                }
                BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);

                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }
                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("LOCAL_MORADA", dto.MunicipioMorada);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_MORADA", DBNull.Value);
                }
                BaseDados.AddParameter("RUA", dto.Rua);
                BaseDados.AddParameter("BAIRRO", dto.Bairro);
                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("FAX", dto.TelefoneFax);
                BaseDados.AddParameter("EMAIL", dto.Email);
                BaseDados.AddParameter("WEBSITE", dto.WebSite);
                if (dto.IsActivo)
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }
                BaseDados.AddParameter("INCLUSAO", DateTime.Now);

                BaseDados.AddParameter("RAZAO", dto.NomeCompleto);

                BaseDados.AddParameter("FANTASIA", dto.NomeComercial);
                if (dto.EmpresaSede > 0)
                {
                    BaseDados.AddParameter("EMPRESA", dto.EmpresaSede);
                }
                else
                {
                    BaseDados.AddParameter("EMPRESA", DBNull.Value);
                }

                if (dto.Fotografia != null && dto.PathFoto != null && dto.PathFoto.Equals(""))
                {
                    BaseDados.AddParameter("LOGOTIPO", dto.Fotografia);

                }
                else
                {
                    BaseDados.AddParameter("LOGOTIPO", DBNull.Value);
                }

                BaseDados.AddParameter("PATH", dto.PathFoto);

                BaseDados.AddParameter("EMISSAO", dto.Emissao);
                BaseDados.AddParameter("VALIDADE", dto.Validade);
                BaseDados.AddParameter("LOCAL_EMISSAO", dto.LocalEmissao);
                BaseDados.AddParameter("CATEGORIA", dto.Categoria);
                BaseDados.AddParameter("INICIAIS", dto.DesignacaoEntidade);
                BaseDados.AddParameter("@ISPASSIVO", dto.CustomerFiscalCodeID);
                BaseDados.AddParameter("@RETENCAO", dto.DefaultWithHoldingID);


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

        public EmpresaDTO ObterEmpresaSistema()
        {
            List<EmpresaDTO> empresas;
            EmpresaDTO dto = new EmpresaDTO();
            try
            {
                BaseDados.ComandText = "stp_SIS_EMPRESA_OBTERPRINCIPAL";


                MySqlDataReader dr = BaseDados.ExecuteReader();
                empresas = new List<EmpresaDTO>();

                if (dr.Read())
                {


                    dto.Codigo = int.Parse(dr[0].ToString());

                    if (dr[1].ToString() != null && dr[1].ToString().Equals("0001-01-01"))
                    {
                        dto.DataNascimento = Convert.ToDateTime(dr[1].ToString());
                    }

                    dto.NomeCompleto = dr[2].ToString();



                    dto.Identificacao = dr[4].ToString();

                    if (dr[5].ToString() != null && !dr[5].ToString().Equals(""))
                    {
                        dto.MunicipioMorada = int.Parse(dr[5].ToString());
                        dto.Morada = dr[6].ToString().ToUpper();
                    }


                    dto.Bairro = dr[7].ToString().ToUpper();
                    dto.Rua = dr[8].ToString().ToUpper();
                    dto.Telefone = dr[9].ToString();
                    dto.TelefoneAlt = dr[10].ToString();
                    dto.TelefoneFax = dr[11].ToString();
                    dto.Email = dr[12].ToString();
                    dto.WebSite = dr[13].ToString();
                    dto.PathFoto = dr[14].ToString();
                    dto.ProvinciaMorada = int.Parse(dr[15].ToString());
                    dto.Categoria = dr[16].ToString();


                }
            }
            catch (Exception ex)
            {
                dto = new EmpresaDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto;

        }

        public EmpresaDTO Alterar(EmpresaDTO dto)
        {

            try
            {

                BaseDados.ComandText = "stp_SIS_EMPRESA_ALTERAR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("NOME_COMPLETO", dto.NomeCompleto);
                if (dto.DataNascimento.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("DATA_NASCIMENTO", dto.DataNascimento);
                }
                BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);

                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }
                BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("LOCAL_MORADA", dto.MunicipioMorada);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_MORADA", DBNull.Value);
                }
                BaseDados.AddParameter("RUA", dto.Rua);
                BaseDados.AddParameter("BAIRRO", dto.Bairro);
                BaseDados.AddParameter("TELEFONE", dto.Telefone);
                BaseDados.AddParameter("TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("FAX", dto.TelefoneFax);
                BaseDados.AddParameter("EMAIL", dto.Email);
                BaseDados.AddParameter("WEBSITE", dto.WebSite);
                if (dto.IsActivo)
                {
                    BaseDados.AddParameter("SITUACAO", 1);
                }
                else
                {
                    BaseDados.AddParameter("SITUACAO", 0);
                }
                BaseDados.AddParameter("INCLUSAO", DateTime.Now);

                BaseDados.AddParameter("RAZAO", dto.NomeCompleto);

                BaseDados.AddParameter("FANTASIA", dto.NomeComercial);
                if (dto.EmpresaSede > 0)
                {
                    BaseDados.AddParameter("EMPRESA", dto.EmpresaSede);
                }
                else
                {
                    BaseDados.AddParameter("EMPRESA", DBNull.Value);
                }

                if (dto.Fotografia != null && dto.PathFoto != null && dto.PathFoto.Equals(""))
                {
                    BaseDados.AddParameter("LOGOTIPO", dto.Fotografia);

                }
                else
                {
                    BaseDados.AddParameter("LOGOTIPO", DBNull.Value);
                }

                BaseDados.AddParameter("PATH", dto.PathFoto);

                BaseDados.AddParameter("EMISSAO", dto.Emissao);
                BaseDados.AddParameter("VALIDADE", dto.Validade);
                BaseDados.AddParameter("LOCAL_EMISSAO", dto.LocalEmissao);
                if (!string.IsNullOrEmpty(dto.Categoria))
                   BaseDados.AddParameter("CATEGORIA", dto.Categoria);
                else
                    BaseDados.AddParameter("CATEGORIA", DBNull.Value);
                BaseDados.AddParameter("INICIAIS", dto.DesignacaoEntidade);
                BaseDados.AddParameter("@ISPASSIVO", dto.CustomerFiscalCodeID);
                BaseDados.AddParameter("@RETENCAO", dto.DefaultWithHoldingID);

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

        public EmpresaDTO Excluir(EmpresaDTO dto)
        {

            try
            {
                BaseDados.ComandText = "stp_SIS_EMPRESA_EXCLUIR";

                BaseDados.AddParameter("CODIGO", dto.Codigo);
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
 
        public List<EmpresaDTO> ObterAcessoFiliais(UtilizadorDTO dto)
        {
            List<EmpresaDTO> empresas;
            EmpresaDTO filial = null;

            try
            {
                BaseDados.ComandText = "stp_SIS_EMPRESA_ACESSO";

                 
                BaseDados.AddParameter("UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                empresas = new List<EmpresaDTO>();

                while (dr.Read())
                {
                    filial = new EmpresaDTO();


                    filial.Codigo = int.Parse(dr[0].ToString());
                    filial.NomeComercial = dr[1].ToString();
                    filial.Morada = dr[2].ToString().ToUpper() + " " + dr[3].ToString().ToUpper();
                    filial.Telefone = dr[4].ToString();
                    filial.Email = dr[5].ToString();
                    filial.Categoria =  dr[6].ToString();
                    if (dr[6].ToString() == "1")
                    {
                        filial.IsDefault = true;
                    }
                    else
                    {
                        filial.IsDefault = false;
                    }

                    filial.CompanyVAT = dr[8].ToString();
                    filial.PathFoto = dr[9].ToString();
                    if (dr[10].ToString() == "T")
                        filial.CustomerFiscalCodeID = "Regime Transitório";
                    else if (dr[10].ToString() == "I")
                        filial.CustomerFiscalCodeID = "Regime de não Sujeição IVA";
                    else
                        filial.CustomerFiscalCodeID = "Regime Geral IVA";
                    empresas.Add(filial);
                }


            }
            catch (Exception ex)
            {
                filial = new EmpresaDTO();
                filial.Sucesso = false;
                filial.MensagemErro = ex.Message.Replace("'", "");

                empresas = new List<EmpresaDTO>();
                empresas.Add(filial);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return empresas;
        }

        public List<EmpresaDTO> ObterFiliais(EmpresaDTO empresa)
        {
            List<EmpresaDTO> empresas;

            try
            {
                BaseDados.ComandText = "stp_SIS_EMPRESA_OBTERPORFILTRO";

                BaseDados.AddParameter("EMPRESA", empresa.EmpresaSede);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                empresas = new List<EmpresaDTO>();
                while (dr.Read())
                {
                    EmpresaDTO dto = new EmpresaDTO();


                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.NomeComercial = dr[1].ToString();
                    dto.Morada = dr[2].ToString().ToUpper() + " " + dr[3].ToString().ToUpper();
                    dto.Bairro = dr[7].ToString().ToUpper();
                    dto.Rua = dr[6].ToString().ToUpper(); 
                    dto.Telefone = dr[4].ToString();
                    dto.Email = dr[5].ToString();
                    dto.Categoria = dr[8].ToString();
                    empresas.Add(dto);
                }


            }
            catch (Exception ex)
            {
                EmpresaDTO dto = new EmpresaDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                empresas = new List<EmpresaDTO>();
                empresas.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return empresas;
        }

        public void Incluir(UtilizadorDTO dto)
        {
            BaseDados.ComandText = "stp_SIS_UTILIZADOR_FILIAL_ADICIONAR";

            BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
            BaseDados.AddParameter("@FILIAL", dto.Filial);

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
        }

        public void Remover(UtilizadorDTO dto)
        {
            BaseDados.ComandText = "stp_SIS_UTILIZADOR_FILIAL_EXCLUIR";

            BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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
        }

        
    }
}
