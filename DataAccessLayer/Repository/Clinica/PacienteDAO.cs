using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dominio.Geral;
using Dominio.Clinica;

namespace DataAccessLayer.Clinica
{
    public class PacienteDAO
    {
        static ConexaoDB BaseDados = new ConexaoDB();
     

        public PacienteDTO Adicionar(PacienteDTO dto) 
        {
             
            try
            {

                BaseDados.ComandText = "stp_CLI_PACIENTE_ADICIONAR";

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

                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", dto.MunicipioNascimento);
                }
                else 
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);
                }

                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);
                }
                else 
                {
                    BaseDados.AddParameter("NACIONALIDADE",DBNull.Value);
                }

                if (dto.PaisNascimento > 0)
                {
                    BaseDados.AddParameter("PAIS", dto.PaisNascimento);
                }
                else
                {
                    BaseDados.AddParameter("PAIS", DBNull.Value);
                }
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
                BaseDados.AddParameter("SEXO", dto.Sexo);
                BaseDados.AddParameter("CIVIL", dto.EstadoCivil);

                BaseDados.AddParameter("PAI", dto.NomePai);
                BaseDados.AddParameter("MAE", dto.NomeMae);

                if (dto.IsActivo)
                {
                    BaseDados.AddParameter("ESTADO", 1);
                }
                else
                {
                    BaseDados.AddParameter("ESTADO", 0);
                } 

                if (dto.Fotografia != null)
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", dto.Fotografia);
                }
                else
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", MySqlDbType.VarBinary);
                }

                BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);
                BaseDados.AddParameter("@PATH", dto.PathFoto);

                if (dto.Documento > 0)
                {
                    BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                    BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);
                }
                else
                {
                    BaseDados.AddParameter("DOCUMENTO", DBNull.Value);
                    BaseDados.AddParameter("IDENTIFICACAO", DBNull.Value);
                }


                if (dto.GrupoSanguineo > 0)
                {
                    BaseDados.AddParameter("@GRUPO", dto.GrupoSanguineo);
                }
                else 
                {
                    BaseDados.AddParameter("@GRUPO", DBNull.Value);
                }

                if (dto.Raca > 0)
                {
                    BaseDados.AddParameter("@RACA", dto.Raca);
                }
                else 
                {
                    BaseDados.AddParameter("@RACA", DBNull.Value);
                }


                if (dto.Religiao > 0)
                {
                    BaseDados.AddParameter("@RELIGIAO", dto.Religiao);
                }
                else 
                {
                    BaseDados.AddParameter("@RELIGIAO", DBNull.Value);
                }

                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento);
                BaseDados.AddParameter("@SEGURADORA", dto.Seguradora);
                BaseDados.AddParameter("@CONVENIO", dto.PlanoSeguro);
                BaseDados.AddParameter("@APOLICE", dto.Apolice);
                if (dto.Validade.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("VALIDADE", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("VALIDADE", dto.Validade);
                }

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@DEFICIENCIA", dto.DeficienciaFisica);
                BaseDados.AddParameter("@DISTRITO", dto.Distrito);
                /*
                BaseDados.AddParameter("@CONTRATO", dto.Contrato);
                BaseDados.AddParameter("@CARGO", dto.Cargo);
                BaseDados.AddParameter("@MATRICULA", dto.Matricula);
                BaseDados.AddParameter("@AREA", dto.Area);
                BaseDados.AddParameter("@ALOJAMENTO", dto.Alojamento);
                */
                
                

                dto.Codigo = BaseDados.ExecuteInsert();

                if (dto.Codigo == -2)
                {
                    dto.Sucesso = false;
                    dto.MensagemErro = "Já Existe um Utente Cadastrado com este Nº do Documento";

                }
                else 
                {
                    dto.Sucesso = true;
                    dto.MensagemErro = "";
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

        public PacienteDTO Alterar(PacienteDTO dto)
        {
            try
            {

                BaseDados.ComandText = "stp_CLI_PACIENTE_ALTERAR";

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

                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", dto.MunicipioNascimento);
                }
                else
                {
                    BaseDados.AddParameter("LOCAL_NASCIMENTO", DBNull.Value);
                }

                if (dto.Nacionalidade > 0)
                {
                    BaseDados.AddParameter("NACIONALIDADE", dto.Nacionalidade);
                }
                else
                {
                    BaseDados.AddParameter("NACIONALIDADE", DBNull.Value);
                }

                if (dto.PaisNascimento > 0)
                {
                    BaseDados.AddParameter("PAIS", dto.PaisNascimento);
                }
                else
                {
                    BaseDados.AddParameter("PAIS", DBNull.Value);
                }
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
                BaseDados.AddParameter("SEXO", dto.Sexo);
                BaseDados.AddParameter("CIVIL", dto.EstadoCivil);

                BaseDados.AddParameter("PAI", dto.NomePai);
                BaseDados.AddParameter("MAE", dto.NomeMae);
                 

                if (dto.IsActivo)
                {
                    BaseDados.AddParameter("ESTADO", "A");
                }
                else
                {
                    BaseDados.AddParameter("ESTADO", "I");
                }

                if (dto.Fotografia != null)
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", dto.Fotografia);
                }
                else
                {
                    BaseDados.AddParameter("@FOTOGRAFIA", MySqlDbType.VarBinary);
                }

                BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);
                BaseDados.AddParameter("@PATH", dto.PathFoto);

                if (dto.Documento > 0)
                {
                    BaseDados.AddParameter("DOCUMENTO", dto.Documento);
                    BaseDados.AddParameter("IDENTIFICACAO", dto.Identificacao);
                }
                else
                {
                    BaseDados.AddParameter("DOCUMENTO", DBNull.Value);
                    BaseDados.AddParameter("IDENTIFICACAO", DBNull.Value);
                }


                if (dto.GrupoSanguineo > 0)
                {
                    BaseDados.AddParameter("@GRUPO", dto.GrupoSanguineo);
                }
                else
                {
                    BaseDados.AddParameter("@GRUPO", DBNull.Value);
                }

                if (dto.Raca > 0)
                {
                    BaseDados.AddParameter("@RACA", dto.Raca);
                }
                else
                {
                    BaseDados.AddParameter("@RACA", DBNull.Value);
                }


                if (dto.Religiao > 0)
                {
                    BaseDados.AddParameter("@RELIGIAO", dto.Religiao);
                }
                else
                {
                    BaseDados.AddParameter("@RELIGIAO", DBNull.Value);
                }

                BaseDados.AddParameter("@FILIAL", dto.Filial);

                BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento);
                BaseDados.AddParameter("@SEGURADORA", dto.Seguradora);
                BaseDados.AddParameter("@CONVENIO", dto.PlanoSeguro);
                BaseDados.AddParameter("@APOLICE", dto.Apolice);
                if (dto.Validade.Equals(DateTime.MinValue))
                {
                    BaseDados.AddParameter("VALIDADE", DBNull.Value);
                }
                else
                {
                    BaseDados.AddParameter("VALIDADE", dto.Validade);
                }

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                if(dto.DeficienciaFisica!="" && dto.DeficienciaFisica!="-1")
                 BaseDados.AddParameter("@DEFICIENCIA", dto.DeficienciaFisica);
                else
                    BaseDados.AddParameter("DEFICIENCIA", DBNull.Value);

                BaseDados.AddParameter("@DISTRITO", dto.Distrito);
                /*
                BaseDados.AddParameter("@CONTRATO", dto.Contrato);
                BaseDados.AddParameter("@CARGO", dto.Cargo);
                BaseDados.AddParameter("@MATRICULA", dto.Matricula);
                BaseDados.AddParameter("@AREA", dto.Area);
                BaseDados.AddParameter("@ALOJAMENTO", dto.Alojamento);
                 */

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

        public PacienteDTO Excluir(PacienteDTO dto)
        {
             
            try
            {
                BaseDados.ComandText = "stp_CLI_PACIENTE_EXCLUIR";

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

        public List<PacienteDTO> ObterPorFiltro(PacienteDTO dto) 
        {
            List<PacienteDTO> es;

            try
            {
                BaseDados.ComandText = "stp_CLI_PACIENTE_OBTERPORFILTRO";

                BaseDados.AddParameter("NOME", dto.NomeCompleto); 
                BaseDados.AddParameter("DATA_NASCIMENTO", dto.DataNascimento == DateTime.MinValue ? (object)DBNull.Value : dto.DataNascimento); 

                MySqlDataReader dr = BaseDados.ExecuteReader();
                es = new List<PacienteDTO>();
                while (dr.Read())
                {
                    dto = new PacienteDTO();


                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.CreatedDate = Convert.ToDateTime(dr[1].ToString());
                    dto.NomeCompleto = dr[2].ToString();
                    dto.DataNascimento = dr[3].ToString()!="" ? Convert.ToDateTime(dr[3].ToString()) : DateTime.MinValue;
                    dto.Sexo = dr[4].ToString();
                    dto.EstadoCivil = dr[5].ToString();
                    dto.Identificacao = dr[6].ToString();
                    dto.Idade = new IdadeDTO { Extenso = CalcularIdade(dto.DataNascimento) };
                    dto.PacienteID = dr["UTE_CODIGO"].ToString();
                    dto.Telefone = dr[7].ToString();
                    dto.TelefoneAlt = dr[8].ToString();
                    dto.Email = dr[9].ToString();
                    dto.DesignacaoEntidade = dto.PacienteID + "-" + dto.NomeCompleto;
                    dto.PathFoto = dr[10].ToString();
                    if (dto.PathFoto.Equals(string.Empty))
                    {
                        if (dto.Sexo.Equals("FEMININO"))
                        {
                            dto.PathFoto = "~/imagens/128x128/People-Patient-Female-icon.png";

                        }
                        else
                        {
                            dto.PathFoto = "~/imagens/128x128/People-Patient-Male-icon.png";
                        }
                    }

                   

                    if (dto.TelefoneAlt != "")
                    {
                        dto.Telefone += "/" + dto.TelefoneAlt;
                    }

                    dto.PlanoSeguro = "PARTICULAR";
                    es.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto = new PacienteDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                es = new List<PacienteDTO>();
                es.Add(dto);

            }
            finally {

                BaseDados.FecharConexao();
            }

            return es;
        }

        public PacienteDTO ObterPorPK(PacienteDTO dto) 
        {
            try
            {
                BaseDados.ComandText = "stp_CLI_PACIENTE_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new PacienteDTO();
                if (dr.Read()) 
                {
                   
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.NomeCompleto = dr[1].ToString();
                    dto.DataNascimento = Convert.ToDateTime(dr[2].ToString()=="" ? DateTime.MinValue.ToString() : dr[2].ToString());
                    dto.Idade = new IdadeDTO { Extenso = CalcularIdade(dto.DataNascimento) };
                    dto.MunicipioNascimento = int.Parse(dr[3].ToString()== "" ? "-1" : dr[3].ToString());
                    dto.PaisNascimento = int.Parse(dr[4].ToString() == "" ? "-1" : dr[4].ToString());
                    dto.Documento = int.Parse(dr[5].ToString() == string.Empty ? "-1" : dr[5].ToString());
                    dto.MunicipioMorada = int.Parse(dr[6].ToString() == string.Empty ? "-1" : dr[6].ToString());

                    dto.Rua = dr[7].ToString();
                    dto.Bairro = dr[8].ToString();

                    
                    dto.Telefone = dr[9].ToString();
                    dto.TelefoneAlt = dr[10].ToString();
                    dto.TelefoneFax = dr[11].ToString();
                    dto.Email = dr[12].ToString();

                    dto.Identificacao = dr[16].ToString();
                    dto.Nacionalidade = int.Parse(dr[17].ToString() == "" ? "-1" : dr[17].ToString());
                    dto.PathFoto = dr[19].ToString();
                    dto.Distrito = dr[20].ToString(); 
                    dto.Sexo = dr[28].ToString();
                    dto.EstadoCivil = dr[29].ToString();


                    dto.GrupoSanguineo = int.Parse(dr[36].ToString() == "" ? "-1" : dr[36].ToString());
                    dto.NomePai = dr[37].ToString();
                    dto.NomeMae = dr[38].ToString();
                    
                    //dto.Nacionalidade = int.Parse(dr[5].ToString());

                    dto.LocalNascimento = int.Parse(dr[41].ToString() == "" ? "-1" : dr[41].ToString());
                    dto.Religiao = int.Parse(dr[42].ToString().ToUpper() == "" ? "-1" : dr[42].ToString());
                    dto.Raca = int.Parse(dr[43].ToString().ToUpper() == "" ? "-1" : dr[43].ToString());
                    dto.PacienteID = dr[44].ToString();
                    dto.DeficienciaFisica = dr[45].ToString().ToUpper() == "" ? "-1" : dr[45].ToString();
                    dto.Documento = int.Parse(dr[46].ToString().ToUpper() == "" ? "-1" : dr[46].ToString());
                    dto.Provincia = dr[47].ToString().ToUpper() == "" ? "-1" : dr[47].ToString();


                    dto.Seguradora = dr[48].ToString() == "" ? "-1" : dr[48].ToString();
                    dto.PlanoSeguro = dr[49].ToString() == "" ? "-1" : dr[49].ToString();
                    dto.Apolice = dr[50].ToString();
                    if (dr[50].ToString() != "")
                    {
                        dto.Validade = DateTime.Parse(dr[50].ToString());
                    }
                }
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

        public String NumeroProcesso(int codigo)
        {
            String matricula = "";

            if (codigo < 10)
            {
                matricula = "00000";
            }
            else if (codigo < 100)
            {
                matricula = "0000";
            }
            else if (codigo < 1000)
            {
                matricula = "000";
            }
            else if (codigo < 10000)
            {
                matricula = "00";
            }
            else if (codigo < 100000)
            {
                matricula = "0";
            }

            return matricula + codigo.ToString();

        }

        public String CalcularIdade(DateTime dNascimento)
        {
            int idDias = 0, idMeses = 0, idAnos = 0; // (id = idade)

            string ta = "", tm = "", td = "";
            DateTime dAtual = DateTime.Today;
            if (dAtual < dNascimento)
            {

                return "Data de nascimento inválida ";

            }

            if (dAtual.Day < dNascimento.Day)
            {
                int vMes = dAtual.Month - 1;
                if(dAtual.Month == 1){
                    vMes = dAtual.Month;
                }
                idDias = (DateTime.DaysInMonth(dAtual.Year, vMes));

                idMeses = -1;

                if (idDias == 28 && dNascimento.Day == 29)

                    idDias = 29;

            }

            if (dAtual.Month < dNascimento.Month)
            {

                idMeses = idMeses + 12;

                idAnos = -1;

            }

            idDias = dAtual.Day - dNascimento.Day + idDias;

            idMeses = dAtual.Month - dNascimento.Month + idMeses;

            idAnos = dAtual.Year - dNascimento.Year + idAnos;

            if (idAnos > 1)

                ta = idAnos + " anos ";

            else if (idAnos == 1)

                ta = idAnos + "ano";

            if (idMeses > 1)

                tm = idMeses + " meses ";

            else if (idMeses == 1)

                tm = idMeses + " mês ";

            if (idDias > 1)

                td = idDias + " dias ";

            else if (idDias == 1)

                td = idDias + " dia ";

            return ta + tm + td;

        }

        
    }
}
