using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class AlunoDocEntregueDAO
    {

        readonly ConexaoDB BaseDados;
        public AlunoDocEntregueDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public AlunoDocEntregueDTO Adicionar(AlunoDocEntregueDTO dto) 
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_DOCUMENTACAO_ADICIONAR";

                BaseDados.AddParameter("@ALUNO", dto.StudentID);
                BaseDados.AddParameter("@DOCUMENTO", dto.DocumentID);
                if (dto.DocumentPath != null && !dto.DocumentPath.Equals(string.Empty))
                {
                    BaseDados.AddParameter("@IMAGEM", dto.Ficheiro);
                    

                }
                else
                {
                    BaseDados.AddParameter("@IMAGEM", DBNull.Value);

                }
                BaseDados.AddParameter("@PATH", dto.DocumentPath);
                BaseDados.AddParameter("@EXTENSAO", dto.FileByte);
                BaseDados.AddParameter("@CONTEUDO", dto.ContentType);
                BaseDados.AddParameter("@FICHEIRO", -1);
                BaseDados.AddParameter("@ENTREGUE", dto.Sucesso);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@QUANTIDADE", dto.Quantidade);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

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

        public void Excluir(AlunoDocEntregueDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_DOCUMENTACAO_EXCLUIR";

                BaseDados.AddParameter("@ALUNO", dto.StudentID);
                BaseDados.AddParameter("@DOCUMENTO", dto.DocumentID); 

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

        public List<AlunoDocEntregueDTO> Obter(AlunoDocEntregueDTO dto) 
        {
            List<AlunoDocEntregueDTO> lista = new List<AlunoDocEntregueDTO>();

            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_DOCUMENTACAO_OBTERPORFILTRO";

                BaseDados.AddParameter("@ALUNO", dto.StudentID);

                MySqlDataReader dr = BaseDados.ExecuteReader();
               
                while (dr.Read()) 
                {
                    dto = new AlunoDocEntregueDTO();

                    dto.StudentID = int.Parse(dr[0]);
                    dto.DocumentID = int.Parse(dr[1]);
                    string docScanner = dr[2] == null || dr[2] == "" ? null : string.Empty;
                    if (docScanner != null)
                    {
                         
                        dto.FileByte = dr[2];
                    }
                     


                    dto.DocumentPath = dr[3];
                    dto.DocExtension = dr[4];
                    dto.ContentType = dr[5];
                    string statusDocumento = dr[6];
                    if (statusDocumento == "1")
                    {
                        dto.Sucesso = true;
                    }
                    else
                    {
                        dto.Sucesso = false;
                    }
                    dto.Quantidade = int.Parse(dr[7] == "" ? "-1" : dr[7]);
                    dto.DocName = dr[13];

                    
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

        public AlunoDocEntregueDTO ObterPorPK(AlunoDocEntregueDTO dto)
        {
            

            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_DOCUMENTACAO_OBTERPORPK";

                BaseDados.AddParameter("@ALUNO", dto.StudentID);
                BaseDados.AddParameter("@DOCUMENTO", dto.DocumentID);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new AlunoDocEntregueDTO();

                while (dr.Read())
                {
                    dto.StudentID = int.Parse(dr[0]);
                    dto.DocumentID = int.Parse(dr[1]);
                    string docScanner = dr[2] == null || dr[2] == "" ? null : string.Empty;
                    if (docScanner != null)
                    {
                         
                        dto.FileByte = dr[2];
                    }



                    dto.DocumentPath = dr[3];
                    dto.DocExtension = dr[4];
                    dto.ContentType = dr[5];
                    string statusDocumento = dr[6];
                    if (statusDocumento == "1")
                    {
                        dto.Sucesso = true;
                    }
                    else
                    {
                        dto.Sucesso = false;
                    }
                    dto.Quantidade = int.Parse(dr[7] == "" ? "-1" : dr[7]);
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
