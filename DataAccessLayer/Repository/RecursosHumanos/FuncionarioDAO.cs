using Dominio.RecursosHumanos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccessLayer.RecursosHumanos
{
    public class FuncionarioDAO
    {
        readonly ConexaoDB BaseDados;

        public FuncionarioDAO()
        {
            BaseDados = new ConexaoDB();
        }


        public FuncionarioDTO Adicionar(FuncionarioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_FUNCIONARIO_ADICIONAR"; 

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@NOME_COMPLETO", dto.NomeCompleto);
                BaseDados.AddParameter("@DATA_NASCIMENTO", dto.DataNascimento);
                BaseDados.AddParameter("@NACIONALIDADE", dto.Nacionalidade);
                if (dto.MunicipioNascimento > 0)
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", dto.MunicipioNascimento); // MUNICIPIO DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_NASCIMENTO", DBNull.Value);

                }
                if (dto.MunicipioMorada > 0)
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", dto.MunicipioMorada); // MUNICIPIO DE MORADA

                }
                else
                {
                    BaseDados.AddParameter("@LOCAL_MORADA", DBNull.Value);

                }

                BaseDados.AddParameter("@RUA", dto.Rua);
                BaseDados.AddParameter("@BAIRRO", dto.Bairro);
                BaseDados.AddParameter("@TELEFONE", dto.Telefone);
                BaseDados.AddParameter("@TELF_ALT", dto.TelefoneAlt);
                BaseDados.AddParameter("@TELF_OUTRO", dto.TelefoneFax);
                BaseDados.AddParameter("@EMAIL", dto.Email);

                BaseDados.AddParameter("@SEXO", dto.Sexo);

                if (dto.EstadoCivil.Equals("-1"))
                {
                    dto.EstadoCivil = string.Empty;
                }
                BaseDados.AddParameter("@CIVIL", dto.EstadoCivil);
                BaseDados.AddParameter("@PAI", dto.NomePai);
                BaseDados.AddParameter("@MAE", dto.NomeMae);

                BaseDados.AddParameter("@MOTIVO_ADMISSAO", DBNull.Value);


                BaseDados.AddParameter("@ADMISSAO", dto.DataAdmissao);

                BaseDados.AddParameter("@DOCUMENTO", dto.Descricao);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);

                BaseDados.AddParameter("@ESTADO", dto.Situacao);

                if (!string.IsNullOrEmpty(dto.Cargo) && dto.Cargo != "-1")
                    BaseDados.AddParameter("@CARGO", dto.Cargo);
                else
                    BaseDados.AddParameter("@CARGO", DBNull.Value);
                

                if (!string.IsNullOrEmpty(dto.Departamento) && dto.Departamento != "-1")
                    BaseDados.AddParameter("@DEPARTAMENTO", dto.Departamento);
                else
                    BaseDados.AddParameter("@DEPARTAMENTO", DBNull.Value);

                BaseDados.AddParameter("@EMAIL_INSTITUCIONAL", dto.WebSite);
                BaseDados.AddParameter("@EXTENSAO", string.Empty);

                if (!string.IsNullOrEmpty(dto.TipoSalario) && dto.TipoSalario != "-1")
                    BaseDados.AddParameter("@VENCIMENTO", dto.TipoSalario);
                else
                    BaseDados.AddParameter("@VENCIMENTO", DBNull.Value);

                BaseDados.AddParameter("@CATEGORIA", dto.CategoriaSalarial);
                BaseDados.AddParameter("@SALARIO", dto.Salario);
                BaseDados.AddParameter("@TURNO", dto.Turno);

                BaseDados.AddParameter("@CARGA_SEMANAL", dto.CargaHorariaSemanal);
                BaseDados.AddParameter("@CARGA_MENSAL", dto.CargaHorariaMensal);
                
               


                if (!string.IsNullOrEmpty(dto.Habilitacoes))
                    BaseDados.AddParameter("@HABILITACOES", dto.Habilitacoes);
                else
                    BaseDados.AddParameter("@HABILITACOES", DBNull.Value);

                if (dto.FormacaoProfissional > 0)
                    BaseDados.AddParameter("@FORMACAO", dto.FormacaoProfissional);
                else
                    BaseDados.AddParameter("@FORMACAO", DBNull.Value);

                if (dto.Profissao > 0)
                    BaseDados.AddParameter("@PROFISSAO", dto.Profissao);
                else
                    BaseDados.AddParameter("@PROFISSAO", DBNull.Value);

                BaseDados.AddParameter("@PAIS", dto.PaisNascimento);

                if (dto.DataObtencaoFormacao != DateTime.MinValue)

                    BaseDados.AddParameter("@OBTENCAO", dto.DataObtencaoFormacao);
                else
                    BaseDados.AddParameter("@OBTENCAO", DBNull.Value);

                if (dto.LocalNascimento > 0)
                {
                    BaseDados.AddParameter("@NATURALIDADE", dto.LocalNascimento); //PROVINCIA DE NASCIMENTO

                }
                else
                {
                    BaseDados.AddParameter("@NATURALIDADE", DBNull.Value);

                }

                if (!string.IsNullOrEmpty(dto.Banco) && dto.Banco != "-1")
                {
                    BaseDados.AddParameter("@BANCO", dto.Banco);

                }
                else
                {
                    BaseDados.AddParameter("@BANCO", DBNull.Value);

                }

                BaseDados.AddParameter("@CONTA", dto.ContaBancaria);
                BaseDados.AddParameter("@IBAN", dto.IBAN);
                BaseDados.AddParameter("@NIB", dto.NIB);
                BaseDados.AddParameter("@SWIFT", dto.Swift);

                if (!string.IsNullOrEmpty(dto.Vinculo) && dto.Vinculo != "-1")
                    BaseDados.AddParameter("@VINCULO", dto.Vinculo);
                else
                    BaseDados.AddParameter("@VINCULO", DBNull.Value);


                if (!string.IsNullOrEmpty(dto.RegimeLaboral) && dto.RegimeLaboral != "-1")
                    BaseDados.AddParameter("@REGIME", dto.RegimeLaboral);
                else
                    BaseDados.AddParameter("@REGIME", DBNull.Value);

                if (dto.DataInicioContrato.Equals(DateTime.MinValue))
                    BaseDados.AddParameter("@INICIO_CONTRATO", DBNull.Value);
                else
                BaseDados.AddParameter("@INICIO_CONTRATO", dto.DataInicioContrato);

                if (dto.DataTerminoContrato.Equals(DateTime.MinValue))
                    BaseDados.AddParameter("@TERMINO_CONTRATO", DBNull.Value);
                else
                BaseDados.AddParameter("@TERMINO_CONTRATO", dto.DataTerminoContrato);

                BaseDados.AddParameter("@VIGENCIA", dto.VigenciaContrato);
                BaseDados.AddParameter("@PERIODO_EXPERIMENTAL", dto.PeriodoExperimental);

                if (dto.DataAvisoPrevio.Equals(DateTime.MinValue))

                    BaseDados.AddParameter("@AVISO_PREVIO", DBNull.Value);
                else

                BaseDados.AddParameter("@AVISO_PREVIO", dto.DataAvisoPrevio);

                if (!string.IsNullOrEmpty(dto.MotivoDemissao) && dto.MotivoDemissao != "-1")
                {
                    BaseDados.AddParameter("@MOTIVO_DEMISSAO", dto.MotivoDemissao);
                    BaseDados.AddParameter("@DATA_DEMISSAO", dto.DataDemissao);
                }
                else
                {
                    BaseDados.AddParameter("@MOTIVO_DEMISSAO", DBNull.Value);
                    BaseDados.AddParameter("@DATA_DEMISSAO", DBNull.Value);
                }

                BaseDados.AddParameter("@DOCENTE", dto.IsDocente ? 1 : 0);

                if ((int.Parse(dto.Filial)>0))
                    BaseDados.AddParameter("@FILIAL", dto.Filial);
                else
                    BaseDados.AddParameter("@FILIAL", DBNull.Value);

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                if (dto.PathFoto != null && !dto.PathFoto.Equals(""))
                {
                    BaseDados.AddParameter("@PATH", dto.PathFoto);
                    BaseDados.AddParameter("@EXTENSAO", dto.ExtensaoFoto);

                }
                else
                {
                    BaseDados.AddParameter("@PATH", DBNull.Value);
                    BaseDados.AddParameter("@ARQUIVO", DBNull.Value);
                }

                dto.Codigo = BaseDados.ExecuteInsert();
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

        public FuncionarioDTO Excluir(FuncionarioDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_RH_FUNCIONARIO_EXCLUIR";
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

        public List<FuncionarioDTO> ObterPorFiltro(FuncionarioDTO dto)
        {
            List<FuncionarioDTO> lista = null;
            try
            {
                 
                BaseDados.ComandText = "stp_RH_FUNCIONARIO_OBTERPORFILTRO";

                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@NOME", dto.NomeCompleto.ToUpper());
                if (dto.DataInicio.Equals(DateTime.MinValue))

                    BaseDados.AddParameter("@INICIO", DBNull.Value);
                else

                    BaseDados.AddParameter("@INICIO", dto.DataInicio);

                if (dto.DataTermino.Equals(DateTime.MinValue))

                    BaseDados.AddParameter("@TERMINO", DBNull.Value);
                else

                    BaseDados.AddParameter("@TERMINO", dto.DataTermino);

                BaseDados.AddParameter("@FILTRO", string.Empty);
                BaseDados.AddParameter("@IDENTIFICACAO", dto.Identificacao);
                BaseDados.AddParameter("@SITUACAO", dto.Situacao);
                BaseDados.AddParameter("@ORDEM", -1);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<FuncionarioDTO>();
                while(dr.Read())
                {
                    dto = new FuncionarioDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());

                    if (string.IsNullOrEmpty(dr[1].ToString()))
                    {
                        if (dr[13].Equals("F"))
                        {
                            dto.PathFoto = "~/RecursosHumanos/fotosFuncionarios/Funcionaria.png";
                        }
                        else
                        {
                            dto.PathFoto = "~/RecursosHumanos/fotosFuncionarios/Funcionario.png";
                        }
                    }
                    else
                    {
                        dto.PathFoto = dr[1].ToString();
                    }
                    
                    dto.NomeCompleto = dr[2].ToString().ToUpper();
                    dto.Cargo = dr[3].ToString();
                    dto.DataAdmissao = Convert.ToDateTime(dr[4].ToString());
                    dto.Situacao = dr[5].ToString();
                    if (dto.Situacao.Equals("1"))
                    {
                        dto.Situacao = "ACTIVO";
                    }
                    else
                    {
                        dto.Situacao = "INACTIVO";
                    }
                    dto.Filial = dr[6].ToString();
                    dto.Vinculo = dr[7].ToString();
                    dto.Email = dr[8].ToString();
                    dto.Telefone = dr[9].ToString();
                    dto.Departamento = dr[12].ToString();

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<FuncionarioDTO>();
                dto.NomeCompleto = dto.MensagemErro;
                lista.Add(dto);

            }
            finally
            {
                BaseDados.FecharConexao();

            }

            return lista;
        }

        public FuncionarioDTO ObterPorPK(FuncionarioDTO dto)
        {
            try
            {
               

                BaseDados.ComandText = "stp_RH_FUNCIONARIO_OBTERPORPK";

                BaseDados.AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new FuncionarioDTO();

                while(dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Filial = dr[1].ToString();
                    dto.Situacao = dr[2].ToString();
                    dto.NomeCompleto = dr[3].ToString().ToUpper();
                    dto.DataNascimento = Convert.ToDateTime(dr[4].ToString());
                    dto.Sexo = dr[5].ToString();
                    dto.EstadoCivil = dr[6].ToString();
                    dto.NomePai = dr[7].ToString();
                    dto.NomeMae = dr[8].ToString();
                    dto.Nacionalidade = int.Parse(dr[9].ToString());
                    dto.PaisNascimento = int.Parse(dr[10].ToString());
                    dto.Naturalidade = dr[11].ToString();
                    dto.LocalNascimento = int.Parse(dr[12].ToString());
                    dto.Rua = dr[13].ToString();
                    dto.Bairro = dr[14].ToString();
                    dto.Morada = dr[15].ToString(); 
                    dto.MunicipioMorada = int.Parse(dr[16].ToString());
                    dto.Telefone = dr[17].ToString();
                    dto.TelefoneAlt = dr[18].ToString();
                    dto.TelefoneFax = dr[19].ToString();
                    dto.Email = dr[20].ToString();
                    dto.DataAdmissao = Convert.ToDateTime(dr[21].ToString());
                    dto.MotivoAdmissao = dr[22].ToString();
                    dto.Cargo = dr[23].ToString();
                    dto.Departamento = dr[24].ToString(); 
                    dto.WebSite = dr[25].ToString(); // Email Empresa
                    dto.Tratamento = dr[26].ToString(); // Extensao
                    dto.TipoSalario = dr[27].ToString();
                    dto.CategoriaSalarial = dr[28].ToString();
                    dto.Salario = decimal.Parse(dr[29].ToString());
                    dto.Turno = dr[30].ToString();
                    dto.CargaHorariaSemanal = dr[31].ToString();
                    dto.CargaHorariaMensal = dr[32].ToString();
                    dto.Vinculo = dr[33].ToString();
                    dto.RegimeLaboral = dr[34].ToString();
                    if (dr[35] != null)
                    {
                        dto.DataInicioContrato = Convert.ToDateTime(dr[35].ToString());
                    }

                    if (dr[36] != null)
                    {
                        dto.DataTerminoContrato = Convert.ToDateTime(dr[36].ToString());
                    }

                    dto.VigenciaContrato = dr[37].ToString();
                    dto.PeriodoExperimental = dr[38].ToString();

                    if (dr[39] != null)
                    {
                        dto.DataAvisoPrevio = Convert.ToDateTime(dr[439].ToString());
                    }

                    if (dr[40] != null)
                    {
                        dto.DataDemissao = Convert.ToDateTime(dr[40].ToString()).ToShortDateString();
                    }

                    dto.MotivoDemissao = dr[41].ToString();
                    dto.Habilitacoes = dr[42].ToString();
                    dto.FormacaoProfissional = int.Parse(dr[43].ToString());

                    if (dr[44] != null)
                    {
                        dto.DataObtencaoFormacao = Convert.ToDateTime(dr[44].ToString());
                    }

                    dto.Profissao = int.Parse(dr[45].ToString());
                    dto.Banco = dr[46].ToString();
                    dto.ContaBancaria = dr[47].ToString();
                    dto.IBAN = dr[48].ToString();
                    dto.NIB = dr[49].ToString();
                    dto.Swift = dr[50].ToString();

                    if (dr[51].ToString() == null) 
                    {
                        if (dto.Sexo.Equals("F"))
                        {
                            dto.PathFoto = "~/RecursosHumanos/fotosFuncionarios/Funcionaria.png";
                        }
                        else
                        {
                            dto.PathFoto = "~/RecursosHumanos/fotosFuncionarios/Funcionario.png";
                        }
                    }
                    else
                    {
                        dto.PathFoto = dr[51].ToString();
                        
                    }

                    dto.TaxaFixaIRT = decimal.Parse(dr[52].ToString());
                    dto.PertenceOrgaosSociais = dr[53].ToString() != "1" ? false : true;
                    dto.ModoProcessamentoSalarial = int.Parse(dr[54].ToString());
                    dto.TipoSubsidioAlimentacao = dr[55].ToString();
                    dto.NumeroMecanografico = dr[56].ToString();
                    dto.NomeAbreviado = dr[57].ToString();
                    dto.PercentagemIncapacidade = decimal.Parse(dr[58].ToString());

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
