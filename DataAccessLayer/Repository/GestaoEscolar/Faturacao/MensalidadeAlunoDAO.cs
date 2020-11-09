using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using Dominio.GestaoEscolar.Faturacao;
using MySql.Data.MySqlClient;
using DataAccessLayer.GestaoEscolar.Pedagogia;
using Dominio.Comercial;
using DataAccessLayer.Comercial;

namespace DataAccessLayer.GestaoEscolar.Faturacao
{
    public class MensalidadeAlunoDAO
    {
        readonly ConexaoDB BaseDados;

        public MensalidadeAlunoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public MensalidadeAlunoDTO Inserir(MensalidadeAlunoDTO dto)
        {

            
            try
            {

                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_ADICIONAR";
                 
                BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("@PARCELA", dto.Parcela.Codigo);
                BaseDados.AddParameter("@ESTADO", string.IsNullOrEmpty(dto.Situacao) ? "A" : dto.Situacao);
                BaseDados.AddParameter("@COBRANCA", dto.DataCobranca);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@VALOR", dto.Valor); 

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

        public MensalidadeAlunoDTO Alterar(MensalidadeAlunoDTO dto)
        {

            
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_ALTERAR";
                 
                BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("@PARCELA", dto.Parcela.Codigo);
                BaseDados.AddParameter("@ESTADO", dto.Situacao);
                BaseDados.AddParameter("@COBRANCA", dto.DataCobranca);
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@VALOR", dto.Valor);
                BaseDados.AddParameter("@VALOR_LIQUIDADO", dto.ValorLiquidado);
                BaseDados.AddParameter("@VALOR_DESCONTO", dto.ValorDesconto);
                BaseDados.AddParameter("@VALOR_MULTA", dto.ValorMulta); 

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = "Erro ao Adicionar a actualizar o Status das Mensalidades: " + ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return dto;
        }

        public void IncluirDivida(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_DIVIDA_ACTUALIZAR";

            BaseDados.AddParameter("@CODIGO", dto.Codigo);
            BaseDados.AddParameter("@DESCRICAO", dto.Descricao); 
            BaseDados.AddParameter("@VALOR", dto.ValorMulta); 
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

        public int Excluir(MensalidadeAlunoDTO dto)
        {

            
            int codigo = 0;
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_EXCLUIR"; 
            
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

        public int RetirarMulta(MensalidadeAlunoDTO dto)
        {

            
            int codigo = 0;
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_RETIRARMULTA"; 
            BaseDados.AddParameter("@FATURA", dto.Fatura);

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

        public int RemoveParcelas(MensalidadeAlunoDTO dto)
        {

            

            int codigo = 0;
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_REMOVERPARCELAS"; 

            BaseDados.AddParameter("@MATRICULA", dto.Matricula.Codigo);

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
        public MensalidadeAlunoDTO ObterPorPK(MensalidadeAlunoDTO dto)
        {

            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_OBTERPORPK";
            
             
            BaseDados.AddParameter("@CODIGO", dto.Codigo);
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MensalidadeAlunoDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();
                    MatriculaDTO matricula = new MatriculaDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString())
                    };
                    MatriculaDAO daoMatricula = new MatriculaDAO();
                    matricula = daoMatricula.ObterPorPK(matricula);
                    dto.Matricula = matricula;
                    bool isItemFaturacaoExterna = dr["MENS_EXTERNAL_FEE"].ToString() != string.Empty && dr["MENS_EXTERNAL_FEE"].ToString() == "1";

                    dto.Parcela = new ParcelaMensalidadeDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                       Mensalidade = new MensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                            Designacao = "",
                            DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                            InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                            TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                            Tipo = dr["MENS_TIPO"].ToString(),
                            ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                            AnoLectivo = matricula.AnoLectivo,
                            Filial = dr["ITEM_CODIGO_FILIAL"].ToString(),
                            IsExternalFee = isItemFaturacaoExterna
                        },
                       Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                       Data = dr["MENS_PAR_DATA"].ToString(),
                       Mes = int.Parse((dr["MENS_PAR_MES"].ToString())) 
                    }; 
                    
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString();

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

        public List<MensalidadeAlunoDTO> ProximasParaLiquidar(MensalidadeAlunoDTO dto)
        {
            
            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_PAGAMENTOATRASADO";

                BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
                BaseDados.AddParameter("@TIPO", dto.Parcela!=null ? dto.Parcela.Mensalidade.Tipo : string.Empty);
                BaseDados.AddParameter("@MENSALIDADE_ID", dto.Parcela != null ? dto.Parcela.Mensalidade.Codigo : -1);
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();
                    
                    bool isItemFaturacaoExterna = dr["MENS_EXTERNAL_FEE"].ToString() != string.Empty && dr["MENS_EXTERNAL_FEE"].ToString() == "1";
                    
                     
                    dto.Matricula = new MatriculaDTO
                    {
                        Inscricao = dr["ALU_NUMERO_MANUAL"].ToString(),
                        NomeAluno = dr["ENT_NOME_COMPLETO"].ToString(),
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString()),
                        Aluno = new AlunoDTO(int.Parse(dr["ALU_CODIGO"].ToString())),
                        NomeTurma = dr["TUR_ABREVIATURA"].ToString(),
                        Plano = new AnoCurricularDTO(-1, null, -1, dr["PLAN_DESCRICAO"].ToString(), dr["CUR_NOME"].ToString()),
                        AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                        DataPagto = DateTime.Parse(dr["MAT_DATA_PAGAMENTO"].ToString() == null || dr["MAT_DATA_PAGAMENTO"].ToString() == string.Empty ? DateTime.MinValue.ToString() : dr["MAT_DATA_PAGAMENTO"].ToString())
                    };
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString();
                    dto.ValorDesconto = decimal.Parse(dr["DESCONTO"].ToString() == string.Empty ? "0" : dr["DESCONTO"].ToString());
                    dto.Valor = decimal.Parse(!string.IsNullOrEmpty(dr["MENS_ALU_VALOR"].ToString()) ? dr["MENS_ALU_VALOR"].ToString() : "0");
                    dto.Multa = dr["MULTA"].ToString();
                    if (dto.Matricula.Aluno.Codigo > 0)
                        dto.Classe = "MA";
                    else
                        dto.Classe = "MD";
                    dto.AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString());
                    dto.ReferenciaContabil = dr["ITEM_INTEGRACAO_PLANO_CONTA"].ToString();


                    dto.Parcela = new ParcelaMensalidadeDTO 
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                        Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                        Mensalidade = new MensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                            Descricao = dr["ITEM_DESCRICAO"].ToString(),
                            DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                            InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                            TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                            Tipo = dr["MENS_TIPO"].ToString(),
                            ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                            AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                            Filial = dr["ITEM_CODIGO_FILIAL"].ToString(),
                            IsExternalFee = isItemFaturacaoExterna,
                            ExternalEntity = int.Parse(dr["MENS_TITULAR_ENTITY_ID"].ToString())
                        },
                        Data = dr["MENS_PAR_DATA"].ToString(),
                        Mes = int.Parse((dr["MENS_PAR_MES"].ToString())),
                        ValorUnitario = !string.IsNullOrEmpty(dr["MENS_PAR_VALOR_MENSAL"].ToString()) ? decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString()) : 0,
                        CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1",
                        Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1",
                    };
                    dto.Multa = dr["MENS_PAR_MULTA"].ToString() == "1" ? "S" : "N";

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                lista = new List<MensalidadeAlunoDTO>
                {
                    dto
                };
            }
            finally
            {
                BaseDados.FecharConexao();
            }

                return lista;

        }

        public List<MensalidadeAlunoDTO> ObterPorFiltro(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_OBTERPORFILTRO";

            BaseDados.AddParameter("@ANO_LECTIVO", dto.Matricula.AnoLectivo);
            BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
            BaseDados.AddParameter("@ESTADO", dto.Situacao);
            BaseDados.AddParameter("@FATURA", dto.Fatura);
            BaseDados.AddParameter("@NOME", dto.Matricula.Aluno.NomeCompleto);
            BaseDados.AddParameter("@FILIAL", dto.Filial);
            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();
                    

                    bool isItemFaturacaoExterna = dr["MENS_EXTERNAL_FEE"].ToString() != string.Empty && dr["MENS_EXTERNAL_FEE"].ToString() == "1";


                    dto.Parcela = new ParcelaMensalidadeDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                        Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                        Mensalidade = new MensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                            Descricao = dr["ITEM_DESCRICAO"].ToString(),
                            DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                            InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                            TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                            Tipo = dr["MENS_TIPO"].ToString(),
                            ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                            AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                            Filial = dr["ITEM_CODIGO_FILIAL"].ToString(),
                            IsExternalFee = isItemFaturacaoExterna,
                            ExternalEntity = int.Parse(dr["MENS_TITULAR_ENTITY_ID"].ToString())
                        },
                        Data = dr["MENS_PAR_DATA"].ToString(),
                        Mes = int.Parse((dr["MENS_PAR_MES"].ToString())),
                        ValorUnitario = !string.IsNullOrEmpty(dr["MENS_PAR_VALOR_MENSAL"].ToString()) ? decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString()) : 0,
                        CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1",
                        Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1",
                    };

                    dto.Matricula = new MatriculaDTO(int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString()));
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString());

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
         
        public List<MensalidadeAlunoDTO> ObterPorMatricula(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_OBTERPORMATRICULA";  
            
            BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
            

            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();
                    


                    bool isItemFaturacaoExterna = dr["MENS_EXTERNAL_FEE"].ToString() == string.Empty || dr["MENS_EXTERNAL_FEE"].ToString() != "1" ? false : true;

                    dto.Parcela = new ParcelaMensalidadeDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                        Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                        Mensalidade = new MensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                            Descricao = dr["ITEM_DESCRICAO"].ToString(),
                            DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                            InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                            TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                            Tipo = dr["MENS_TIPO"].ToString(),
                            ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                            AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                            Filial = dr["ITEM_CODIGO_FILIAL"].ToString(),
                            IsExternalFee = isItemFaturacaoExterna,
                            ExternalEntity = int.Parse(dr["MENS_TITULAR_ENTITY_ID"].ToString())
                        },
                        Data = dr["MENS_PAR_DATA"].ToString(),
                        Mes = int.Parse((dr["MENS_PAR_MES"].ToString())),
                        ValorUnitario = !string.IsNullOrEmpty(dr["MENS_PAR_VALOR_MENSAL"].ToString()) ? decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString()) : 0,
                        CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1",
                        Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1",
                    };

                    dto.Matricula = new MatriculaDTO(int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString()));
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString()); 



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

        public List<MensalidadeAlunoDTO> ObterDividaAluno(MensalidadeAlunoDTO dto)
        {
            
            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_OBTERDIVIDA";

                BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
                BaseDados.AddParameter("@TIPO", dto.Parcela.Mensalidade.Tipo);
                BaseDados.AddParameter("@MULTA", dto.CalularMulta);
                BaseDados.AddParameter("@LIMITE", dto.Quantidade);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                int quantidade = dto.Quantidade;
                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();

                    dto.Parcela = new ParcelaMensalidadeDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                        Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                        Mensalidade = new MensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                            Descricao = dr["ITEM_DESCRICAO"].ToString(),
                            DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                            InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                            TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                            Tipo = dr["MENS_TIPO"].ToString(),
                            ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                            AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()), 
                        },
                        Data = dr["MENS_PAR_DATA"].ToString(),
                        Mes = int.Parse((dr["MENS_PAR_MES"].ToString())),
                        ValorUnitario = !string.IsNullOrEmpty(dr["MENS_PAR_VALOR_MENSAL"].ToString()) ? decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString()) : 0,
                        CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1",
                        Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1",
                    };
                    dto.Matricula = new MatriculaDTO(int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString()));
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString());
                    dto.Multa = dr["MULTA"].ToString();
                    dto.Valor = decimal.Parse(dr["VALOR"].ToString());
                    dto.Classe = dr["ORIGEM"].ToString();
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

        public bool HasContencioso(MensalidadeAlunoDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_CONTECIOSO";

                BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto.Sucesso = true;

                while (dr.Read())
                {
                    dto.Sucesso =  dr["HAS_CONTECIOSO"].ToString() != "0"; 
                }

            }
            catch (Exception ex)
            { 
                dto.Sucesso = true;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return dto.Sucesso;
        }

        public MensalidadeAlunoDTO ActualizaSituacaoFinanceira(MensalidadeAlunoDTO dto) 
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_ACTUALIZASITUACAO";
            
            BaseDados.AddParameter("@DATA_COBRANCA", dto.DataCobranca);
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

            return dto;
        }

        public List<MensalidadeAlunoDTO> ObterAtrasadas(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_ATRASADA";

            BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);

            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();
                    

                    bool isItemFaturacaoExterna = dr["MENS_EXTERNAL_FEE"].ToString() != string.Empty && dr["MENS_EXTERNAL_FEE"].ToString() == "1";

                    dto.Parcela = new ParcelaMensalidadeDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                        Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                        Mensalidade = new MensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                            Descricao = dr["ITEM_DESCRICAO"].ToString(),
                            DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                            InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                            TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                            Tipo = dr["MENS_TIPO"].ToString(),
                            ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                            AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                            Filial = dr["ITEM_CODIGO_FILIAL"].ToString(),
                            IsExternalFee = isItemFaturacaoExterna,
                            ExternalEntity = int.Parse(dr["MENS_TITULAR_ENTITY_ID"].ToString())
                        },
                        Data = dr["MENS_PAR_DATA"].ToString(),
                        Mes = int.Parse((dr["MENS_PAR_MES"].ToString())),
                        ValorUnitario = !string.IsNullOrEmpty(dr["MENS_PAR_VALOR_MENSAL"].ToString()) ? decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString()) : 0,
                        CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1",
                        Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1",
                    };

                    
                    dto.Matricula = new MatriculaDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString()),
                        AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                        AnoCivil = int.Parse(dr["ANO_ANO_LECTIVO"].ToString()),
                        Inscricao = dr["ALU_INSCRICAO"].ToString(),
                        NomeAluno = dr["ENT_NOME_COMPLETO"].ToString()
                    };
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString());
                    dto.OrigemMulta = dr["ORIGEM"].ToString();
                    dto.Valor = decimal.Parse(dr["MENS_ALU_VALOR"].ToString());
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

        public void ExcluirDuplicadas()
        {

            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_EXCLUIRDUPLICADAS";

            try
            {
                
                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MensalidadeAlunoDTO dto = new MensalidadeAlunoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }
        }

        public List<MensalidadeAlunoDTO> ObterMensalidadesAbertas(MensalidadeAlunoDTO dto)
        {

            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_OBTERABERTAS"; 
            BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
            BaseDados.AddParameter("@TIPO", dto.Parcela.Mensalidade.Tipo);
            

            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO
                    {
                        Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString()),
                        Situacao = dr["MENS_ALU_STATUS"].ToString().Trim(),

                        Parcela = new ParcelaMensalidadeDTO
                        {
                            Codigo = int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()),
                            Descricao = dr["MENS_PAR_DESCRICAO"].ToString(),
                            Mensalidade = new MensalidadeDTO
                            {
                                Codigo = int.Parse(dr["MENS_CODIGO_ITEM"].ToString()),
                                Descricao = dr["ITEM_DESCRICAO"].ToString(),
                                DiaLimite = int.Parse(dr["MENS_DIA_LIMITE"].ToString()),
                                InicioCobranca = dr["MENS_INICIO_COBRANCA"].ToString(),
                                TerminoCobranca = dr["MENS_TERMINO_COBRANCA"].ToString(),
                                Tipo = dr["MENS_TIPO"].ToString(),
                                ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString(),
                                AnoLectivo = int.Parse(dr["MAT_CODIGO_ANO"].ToString()),
                                Filial = dr["ITEM_CODIGO_FILIAL"].ToString(),
                                ExternalEntity = int.Parse(dr["MENS_TITULAR_ENTITY_ID"].ToString())
                            },
                            Data = dr["MENS_PAR_DATA"].ToString(),
                            Mes = int.Parse((dr["MENS_PAR_MES"].ToString())),
                            ValorUnitario = !string.IsNullOrEmpty(dr["MENS_PAR_VALOR_MENSAL"].ToString()) ? decimal.Parse(dr["MENS_PAR_VALOR_MENSAL"].ToString()) : 0,
                            CobraMulta = dr["MENS_PAR_MULTA"].ToString() == "1",
                            Activa = dr["MENS_PAR_ACTIVA"].ToString() == "1",
                        }
                    };
                    if (dr["MENS_ALU_VALOR"].ToString() != null && dr["MENS_ALU_VALOR"].ToString() != "")
                    {
                        dto.Valor = decimal.Parse(dr["MENS_ALU_VALOR"].ToString());
                    }
                    else
                    {
                        dto.Valor = 0;
                    }

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

         
        public List<MensalidadeAlunoDTO> ObterPagamentosPorLiquidar(MensalidadeAlunoDTO dto)
        {

            
            BaseDados.ComandText = "stp_ACA_ALUNO_DIVIDA";
             

            BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
            BaseDados.AddParameter("@ANO", dto.Matricula.AnoLectivo);
            BaseDados.AddParameter("@FONTE", "SYS");

            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();

            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Designacao = dr["DESCRICAO"].ToString().ToUpper().Replace("00:00:00", "");
                     
                    dto.DataCobranca = DateTime.Parse(dr["COMPETENCIA"].ToString());
                    dto.Valor = Convert.ToDecimal(dr["PRE_MENS_PRECO"].ToString());
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString(); 

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

         

         

        
        public MensalidadeAlunoDTO PrimeiroMesLectivo(MensalidadeAlunoDTO dto)
        {

            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_PRIMEIRO_MES";



            BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
            BaseDados.AddParameter("@ANO", dto.AnoLectivo);
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MensalidadeAlunoDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0]).ToString();
                    

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

        public MensalidadeAlunoDTO PrimeiroMesPlano(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_PRIMEIRO_MES_PLANO";



            BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
            BaseDados.AddParameter("@TIPO", dto.Parcela.Mensalidade.Tipo);
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MensalidadeAlunoDTO();
                while (dr.Read())
                {
                    dto.Parcela = new ParcelaMensalidadeDTO(0, null, dr[1].ToString(), "", int.Parse(dr[0].ToString())); 

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

        public MensalidadeAlunoDTO UltimoMesLectivo(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ULTIMO_MES";



            BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
            BaseDados.AddParameter("@TIPO", dto.Parcela.Mensalidade.Tipo);

            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MensalidadeAlunoDTO();
                while (dr.Read())
                {
                    dto.Parcela = new ParcelaMensalidadeDTO(0, null, dr[1].ToString(), "", int.Parse(dr[0]));  
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

        public void ActualizarMes(MensalidadeAlunoDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALTERARPLANO";

                BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
                BaseDados.AddParameter("@PARCELA", dto.Parcela.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@MES", dto.Parcela.Mes);
                BaseDados.AddParameter("@MENSALIDADE", dto.Quantidade);

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

        public MensalidadeAlunoDTO IncluirPlano(MensalidadeAlunoDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_ALTERARPLANO";

                BaseDados.AddParameter("@MENSALIDADE", dto.Quantidade);
                BaseDados.AddParameter("@ALUNO", dto.Matricula.Aluno.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                 
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

        public void LimpaParcelaDivida(int pCodigo)
        {

            
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_DIVIDA_EXCLUIR";

                BaseDados.AddParameter("@CODIGO", pCodigo);
                

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string mesnagem = ex.Message;
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public List<MensalidadeAlunoDTO> ObterRelacaoWithBalance(MatriculaDTO dto)
        {
            

            //BaseDados.AddParameter("@SITUACAO", dto.MensAluMatricula.MatTurno);
            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();

            try
            {

                BaseDados.ComandText = "stp_FIN_LISTAGEM_SALDO_POSITIVO";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    MensalidadeAlunoDTO objSituacao = new MensalidadeAlunoDTO();

                    objSituacao.NumeroInscricao = dr[0].ToString();
                    objSituacao.NroRecibo = dr[1].ToString();
                    objSituacao.NomeAluno = dr[2].ToString();
                    objSituacao.Curso = dr[3].ToString();
                    objSituacao.Classe = dr[4].ToString();
                    objSituacao.Turma = dr[5].ToString();
                    objSituacao.FinValor = decimal.Parse(dr[6]).ToString();
                    objSituacao.Total = decimal.Parse(dr[7]).ToString();

                    if (objSituacao.FinValor > 0)
                    {
                        lista.Add(objSituacao);
                    }



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

        public List<MensalidadeAlunoDTO> ObterRelacaoWithNegativeBalance(MatriculaDTO dto)
        {

            
            //BaseDados.AddParameter("@SITUACAO", dto.MensAluMatricula.MatTurno);
            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();

            try
            {

                BaseDados.ComandText = "stp_FIN_LISTAGEM_SALDO_NEGATIVO";


                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@CURSO", dto.Curso);
                BaseDados.AddParameter("@CLASSE", dto.Classe);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@ALUNO", dto.Aluno.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    MensalidadeAlunoDTO objSituacao = new MensalidadeAlunoDTO();

                    objSituacao.NumeroInscricao = dr[0].ToString();
                    objSituacao.NroRecibo = dr[1].ToString();
                    objSituacao.NomeAluno = dr[2].ToString();
                    objSituacao.Curso = dr[3].ToString();
                    objSituacao.Classe = dr[4].ToString();
                    objSituacao.Turma = dr[5].ToString();
                    objSituacao.FinValor = decimal.Parse(dr[6]).ToString();
                    objSituacao.Total = decimal.Parse(dr[7]).ToString();

                    if (objSituacao.FinValor > 0)
                    {
                        lista.Add(objSituacao);
                    }



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


        public List<MensalidadeAlunoDTO> GetLaterFees(MensalidadeAlunoDTO dto)
        {
            
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_ACTUALIZASITUACAO";

            BaseDados.AddParameter("@DATA_COBRANCA", DateTime.Today);
            BaseDados.AddParameter("@FILIAL", dto.Filial);


            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Codigo = int.Parse(dr["MENS_ALU_CODIGO"].ToString());
                    dto.Matricula = new MatriculaDAO().ObterPorPK(new MatriculaDTO(int.Parse(dr["MENS_ALU_CODIGO_ALUNO"].ToString())));
                    dto.Parcela = new ParcelaMensalidadeDTO(int.Parse(dr["MENS_ALU_CODIGO_PARCELA"].ToString()));
                    dto.Situacao = dr["MENS_ALU_STATUS"].ToString().Trim();
                    dto.DataCobranca = Convert.ToDateTime(dr["MENS_ALU_DATA_LIMITE"].ToString());
                    if (dr["MENS_ALU_VALOR"].ToString() != null && dr["MENS_ALU_VALOR"].ToString() != "")
                    {
                        dto.Valor = decimal.Parse(dr["MENS_ALU_VALOR"].ToString());
                    }
                    else
                    {
                        dto.Valor = 0;
                    }

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
