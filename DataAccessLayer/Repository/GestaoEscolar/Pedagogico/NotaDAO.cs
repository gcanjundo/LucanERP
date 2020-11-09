using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;
using DataAccessLayer.Seguranca;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class NotaDAO
    {

        readonly ConexaoDB BaseDados;

        public NotaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public NotaDTO Adicionar(NotaDTO dto) 
        {

            
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_ADICIONAR"; 
                
                BaseDados.AddParameter("@ALUNO", dto.Matricula);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@INSCRICAO", dto.Inscricao);
                BaseDados.AddParameter("@NOME", dto.NomeCompleto.Replace("&nbsp;", string.Empty));
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

        private string GetNomeAbreviado(string pNomeCompleto)
        {
            if (pNomeCompleto.Length >= 25)
            {
                string[] nome = pNomeCompleto.Split(' ');

                int totalNomes = nome.Length - 1, p = 1;

                string nomeAbreviado = nome[0];

                if (nome[1].Length <= 2)
                {
                    nomeAbreviado += " "+ nome[1] + " " + nome[2];
                    totalNomes -= 2;
                    p = 3;
                }


                if (p == totalNomes)
                {

                    while (totalNomes < nome.Length)
                    {
                        if (nome.Length - totalNomes == 1)
                        {
                            nomeAbreviado += nome[p];
                        }
                        else
                        {
                            if (nome[p].Length <= 3)
                            {
                                nomeAbreviado += " " + nome[p];
                            }
                            else
                            {
                                nomeAbreviado += " " + nome[p].Substring(0, 1) + ".";
                            }
                        }

                        p++;
                        totalNomes = p;
                    }

                }
                else
                {

                    if (p > totalNomes)
                    {
                        totalNomes = nome.Length;
                    }

                    for (int i = p; i <= totalNomes; i++)
                    {

                        if (totalNomes - i == 1)
                        {
                            int LastIndex = i + 1;
                            string LastName = "";
                            if (LastIndex < totalNomes)
                            {
                                LastName = nome[LastIndex];
                            }

                            LastName = LastName == "" ? nome[i] : LastName;

                            string nomeCompleto = nomeAbreviado + " " + LastName;
                            if (nomeCompleto.Length > 29)
                            {
                                nomeAbreviado = nome[0] + " " + LastName;
                            }
                            else
                            {
                                nomeAbreviado = nomeCompleto;
                            }
                        }
                        else if (i < totalNomes)
                        {
                            nomeAbreviado += (nome[i].Length <= 3 ? (" " + nome[i]) : (" " + nome[i].Substring(0, 1) + "."));
                        }
                        else if (totalNomes - i == 0 && nome.Length > totalNomes && nome[nome.Length-1] !="")
                        {
                            nomeAbreviado += " " + nome[i];
                        }
                    }
                }

                 

                 
                return nomeAbreviado.Replace("..", ".").Trim();
            }
            else
            {
                return pNomeCompleto.Trim();
            }
        }


        public NotaDTO Lancar(NotaDTO dto)
        {

            

            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_LANCAR";

                BaseDados.AddParameter("@ALUNO", dto.Matricula);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@INSCRICAO", dto.Inscricao);
                BaseDados.AddParameter("@NOME", dto.NomeCompleto);
                BaseDados.AddParameter("@PP1T1", dto.PP1T1);
                BaseDados.AddParameter("@PP2T1", dto.PP2T1);
                BaseDados.AddParameter("@TP1T1", dto.TP1T1);
                BaseDados.AddParameter("@TP2T1", dto.TP2T1);
                BaseDados.AddParameter("@MTPT1", dto.MTPT1);
                BaseDados.AddParameter("@MAC1", dto.MACT1);
                BaseDados.AddParameter("@MTT1", dto.MT1);
                BaseDados.AddParameter("@PP1T2", dto.PP1T2);
                BaseDados.AddParameter("@PP2T2", dto.PP2T2);
                BaseDados.AddParameter("@TP1T2", dto.TP1T2);
                BaseDados.AddParameter("@TP2T2", dto.TP2T2);
                BaseDados.AddParameter("@MTPT2", dto.MTPT2);
                BaseDados.AddParameter("@MAC2", dto.MACT2);
                BaseDados.AddParameter("@MTT2", dto.MT2);
                BaseDados.AddParameter("@PP1T3", dto.PP1T3);
                BaseDados.AddParameter("@PP2T3", dto.PP2T3);
                BaseDados.AddParameter("@TP1T3", dto.TP1T3);
                BaseDados.AddParameter("@TP2T3", dto.TP2T3);
                BaseDados.AddParameter("@MTPT3", dto.MTPT3);
                BaseDados.AddParameter("@MAC3", dto.MACT3);
                BaseDados.AddParameter("@MTT3", dto.MT3);
                BaseDados.AddParameter("@CAP", dto.CAP);
                BaseDados.AddParameter("@CPE", dto.CPE);
                BaseDados.AddParameter("@CF", dto.CF);
                BaseDados.AddParameter("@ESTADO", dto.Status);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.ExecuteNonQuery();


            }
            catch (Exception ex)
            { 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                //BaseDados.GenerateErroFile(dto.MensagemErro);
            }
            finally
            {
                BaseDados.FecharConexao();

            }
            return dto;
        }

        public NotaDTO AdicionarExame(NotaDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_ADICIONAR_EPOCA_EXTRA";

                BaseDados.AddParameter("@ALUNO", dto.Matricula);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@INSCRICAO", dto.Inscricao);
                BaseDados.AddParameter("@NOME", dto.NomeCompleto);
                BaseDados.AddParameter("@AVALIACAO", dto.Avaliacao);
                BaseDados.AddParameter("@RESULTADO", dto.Exame);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                BaseDados.GenerateErroFile(dto.MensagemErro);
            }
            finally
            {
                BaseDados.FecharConexao();

            }
            return dto;
        }

        public void AddCFDAnterior(NotaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_IMPORTAR_CAA";

                BaseDados.AddParameter("@MATRICULA", dto.Matricula); 
                BaseDados.AddParameter("@DISCIPLINA", dto.DisciplinaID);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo); 
                BaseDados.AddParameter("@CAA", dto.CFD);

                BaseDados.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                BaseDados.GenerateErroFile(dto.MensagemErro);
            }
            finally
            {
                BaseDados.FecharConexao();

            }
        }

        public void Excluir(NotaDTO dto)
        {
            

            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_EXCLUIR";


                BaseDados.AddParameter("@DISCIPLINA", dto.DisciplinaID);
                BaseDados.AddParameter("@TURMA", dto.TurmaID);
                BaseDados.AddParameter("@MATRICULA", dto.Matricula); 

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
        
        public List<NotaDTO> ObterPorFiltro(NotaDTO dto)
        {

            
           List<NotaDTO> notas = new List<NotaDTO>();
            try
            {

                BaseDados.ComandText = "stp_ACA_NOTA_OBTERPORFILTRO";

                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina ?? "-1");
                BaseDados.AddParameter("@TURMA", dto.Turma ?? "-1");
                BaseDados.AddParameter("@ALUNO", dto.Matricula );
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@FILIAL", dto: dto.Filial ?? "-1");
                string vNivel = dto.NivelEnsino;

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                    int ordem = 1;
                foreach (var dr in reader)
                {
                    dto = new NotaDTO
                    {
                        NumeroOrdem = ordem,
                        Matricula = int.Parse(dr[0]),
                        Turma = dr[1],
                        Disciplina = dr[2],
                        Inscricao = dr[3],
                        NomeCompleto = dr[4],
                        SocialName = dr[4],
                        PP1T1 = decimal.Parse(dr[5] ?? "-1"),
                        PP2T1 = decimal.Parse(dr[6] ?? "-1"),
                        TP1T1 = decimal.Parse(dr[7] ?? "-1"),
                        TP2T1 = decimal.Parse(dr[8] ?? "-1"),
                        MTPT1 = decimal.Parse(dr[9] ?? "-1"),
                        MACT1 = decimal.Parse(dr[10] ?? "-1"),
                        MT1 = decimal.Parse(dr[11] ?? "-1"),
                        PP1T2 = decimal.Parse(dr[12] ?? "-1"),
                        PP2T2 = decimal.Parse(dr[13] ?? "-1"),
                        TP1T2 = decimal.Parse(dr[14] ?? "-1"),
                        TP2T2 = decimal.Parse(dr[15] ?? "-1"),
                        MTPT2 = decimal.Parse(dr[16] ?? "-1"),
                        MACT2 = decimal.Parse(dr[17] ?? "-1"),
                        MT2 = decimal.Parse(dr[18] ?? "-1"),
                        PP1T3 = decimal.Parse(dr[19] ?? "-1"),
                        PP2T3 = decimal.Parse(dr[20] ?? "-1"),
                        TP1T3 = decimal.Parse(dr[21] ?? "-1"),
                        TP2T3 = decimal.Parse(dr[22] ?? "-1"),
                        MTPT3 = decimal.Parse(dr[23] ?? "-1"),
                        MACT3 = decimal.Parse(dr[24] ?? "-1"),
                        MT3 = decimal.Parse(dr[25] ?? "-1"),
                        CAP = decimal.Parse(dr[26] ?? "-1"),
                        CPE = decimal.Parse(dr[27] ?? "-1"),
                        CF = decimal.Parse(dr[28] ?? "-1"),
                        Curso = dr[29],
                        Classe = dr[30],
                        Observacao = dr[31],
                        AlunoID = dr[32],
                        NroProcesso = dr[33],
                        Formacao = dr[34],
                        CFD = decimal.Parse(dr[35] ?? "-1"),
                        Exame = decimal.Parse(dr[36] ?? "-1"),
                        Recurso = decimal.Parse(dr[37] ?? "-1"),
                        CAA = decimal.Parse(dr[38] ?? "-1"),
                        DirectorTurma = dr[39]
                    };

                    if (dr[40] == "M")
                        dto.Turno = "MANHÃ";
                    else if (dr[40] == "T")
                        dto.Turno = "TARDE";
                    else if (dr[40] == "N")
                        dto.Turno = "NOITE";
                    dto.Sala = dr[41];
                    dto.Sexo = dr[42];
                    dto.DisciplinaID = int.Parse(dr[43]);
                    dto.CA10 = decimal.Parse(dr[44] ?? "-1");
                    dto.Recurso = dto.Recurso <= 0 ? dto.Exame : dto.Recurso;
                    dto.Exame = dto.Exame <= 0 && dto.Recurso > 0 ? dto.Recurso : dto.Exame;
                    if (dr[45] == "F")
                    {
                        dto.IsFinalizante = true;
                    }
                    else if (dr[45] == "A")
                    {
                        dto.IsAnual = true;
                    }
                    else if (dr[45] == "J" || dr[45] == "P")
                    {
                        dto.IsProjectoFinal = true;

                        if (dr[45] == "P")
                        {
                            dto.IsPAP = true;
                        }
                    }
                    else if (dr[45] == "T")
                    {
                        dto.IsTerminal = true;
                    }
                    else if (dr[45] == "E")
                    {
                        dto.IsTerminalEspecial = true;
                    }
                    else
                    {
                        dto.IsContinua = true;
                    }

                    dto.AnoLectivo = int.Parse(dr[46]);
                    dto.TurmaID = int.Parse(dr[47]);
                    dto.DisciplinaID = int.Parse(dr[48]);
                    
                    dto.Picture = dr[49]!=null ? dr[49].ToString() : string.Empty;
                    dto.Pai = dr[50] != null ? dr[50].ToString() : string.Empty;
                    dto.Mae = dr[51] != null ? dr[51].ToString() : string.Empty;
                    dto.Encarregado = dr[52] != null ? dr[52].ToString() : string.Empty; 
                    dto.DataNascimento = dr[53] != null ? DateTime.Parse(dr[53]) : DateTime.MinValue;
                    dto.LocalNascimento = dr[54] != null ? dr[54].ToString() : string.Empty;
                    dto.Naturalidade = dr[55] != null ? dr[55].ToString() : string.Empty;
                     
                    dto.isValidaEpocaNormal = int.Parse(dr[57]) > 0 ? true : false;
                    dto.isValidaEpocaExame = int.Parse(dr[58]) > 0 ? true : false;
                    dto.isValidaEpocaEspecial = int.Parse(dr[59]) > 0 ? true : false;

                     notas.Add(dto);
                    ordem++;
                } 
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                notas = new List<NotaDTO>();
                notas.Add(dto);
                BaseDados.GenerateErroFile(dto.MensagemErro);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return notas;
        }

        public void ValidaMiniPauta(NotaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_VALIDACAO_MINI_PAUTA_ADICIONAR";

                BaseDados.AddParameter("@TURMA_ID", dto.Turma);
                BaseDados.AddParameter("@DISCIPLINA_ID", dto.Disciplina);
                BaseDados.AddParameter("@EPOCA_ID", dto.Epoca);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@PERIODO_ID", dto.Periodo??"-1");

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

        public UtilizadorDTO SalvarAutorizacao(UtilizadorDTO dto)
        {
            
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_VALIDACAO_ADICIONAR";

                
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@ACCAO", dto.Status);
                BaseDados.AddParameter("@USER_SESSION", dto.CreatedBy);

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
        public List<UtilizadorDTO> AutorizadosParaValidacao(UtilizadorDTO dto)
        {
            List<UtilizadorDTO> coleccao;
            
            try
            {

                BaseDados.ComandText = "stp_ACA_NOTA_VALIDACAO_OBTER_UTILIZADORES";

                BaseDados.AddParameter("@FILIAL", dto.CompanyID);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                coleccao = new List<UtilizadorDTO>();

                while (dr.Read())
                {
                    dto = new UtilizadorDTO();

                    dto.SocialName = dr["UTI_NOME"].ToString();
                    dto.Utilizador = dr["UTI_UTILIZADOR"].ToString(); 
                    coleccao.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new UtilizadorDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao = new List<UtilizadorDTO>();
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;
        }

        public NotaDTO LancarCFD10(NotaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_NOTA_LANCAR_CFD_10";

                BaseDados.AddParameter("@MATRICULA", dto.Matricula); 
                BaseDados.AddParameter("@DISCIPLINA", dto.DisciplinaID);
                BaseDados.AddParameter("@INSCRICAO", dto.Inscricao);
                BaseDados.AddParameter("@NOME", dto.NomeCompleto);
                BaseDados.AddParameter("@NOTA", dto.CA10);
                BaseDados.AddParameter("@DISPLAY", dto.Disciplina);
                BaseDados.AddParameter("@TURMA", dto.Turma);
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


        public List<NotaDTO> ObterCFD10(NotaDTO dto)
        {

            

            List<NotaDTO> notas = new List<NotaDTO>();
            try
            {

                BaseDados.ComandText = "stp_ACA_NOTA_OBTERCFD10";

                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina == null ? "-1" : dto.Disciplina);
                BaseDados.AddParameter("@TURMA", dto.Turma == null ? "-1" : dto.Turma);
                BaseDados.AddParameter("@ALUNO", dto.Matricula);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                int ordem = 1;
                while (dr.Read())
                {
                    dto = new NotaDTO();
                    dto.NumeroOrdem = ordem;
                    dto.Matricula = int.Parse(dr[0]);
                    dto.DisciplinaID = int.Parse(dr[1]);
                    dto.Inscricao = dr[2];
                    dto.NomeCompleto = dr[3];
                    dto.SocialName = dr[3];
                    dto.Exame = decimal.Parse(dr[4]);
                    dto.Disciplina = dr[5];
                    dto.Turma = dr[6];
                    dto.PP1T1 = -1;
                    dto.PP2T1 = -1;
                    dto.TP1T1 = -1;
                    dto.TP2T1 = -1;
                    dto.MTPT1 = -1;
                    dto.MACT1 = -1;
                    dto.MT1 = -1;
                    dto.PP1T2 = -1;
                    dto.PP2T2 = -1;
                    dto.TP1T2 = -1;
                    dto.TP2T2 = -1;
                    dto.MTPT2 = -1;
                    dto.MACT2 = -1;
                    dto.MT2 = -1;
                    dto.PP1T3 = -1;
                    dto.PP2T3 = -1;
                    dto.TP1T3 = -1;
                    dto.TP2T3 = -1;
                    dto.MTPT3 = -1;
                    dto.MACT3 = -1;
                    dto.MT3 = -1;
                    dto.CAP = -1;
                    dto.CPE = -1;
                    dto.CF = -1;
                    dto.Observacao = "";
                    dto.CA10 = dto.Exame;
                    notas.Add(dto);
                    ordem++;

                }
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                notas = new List<NotaDTO>();
                notas.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return notas;
        }


        
    }
}
