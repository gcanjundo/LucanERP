using Dominio.GestaoEscolar.Faturacao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class AlunoRubricaMensalDAO
    {
        readonly ConexaoDB BaseDados;

        public AlunoRubricaMensalDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public void AdicionarRubricaMensal(MensalidadeAlunoDTO dto)
        {
            
            try
            {
                
                BaseDados.ComandText = "stp_ACA_ALUNO_RUBRICAS_MENSAIS_ADICIONAR";

                BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@ITEM", dto.Parcela.Codigo);
                BaseDados.AddParameter("@pSTATUS", dto.Situacao == "S" ? 1 : 0);
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
        }

        public void RemoverRubricaMensal(MensalidadeAlunoDTO dto)
        {
            
            try
            {

                BaseDados.ComandText = "stp_ACA_ALUNO_RUBRICAS_MENSAIS_REMOVER";

                BaseDados.AddParameter("@RUBRICA", dto.Parcela.Codigo);
                BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo); 
                BaseDados.AddParameter("@FILIAL", dto.Filial);
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
        }

        public List<MensalidadeAlunoDTO>GetRubricaMensaisAluno(MensalidadeAlunoDTO dto)
        {
            
            List<MensalidadeAlunoDTO> lista = new List<MensalidadeAlunoDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_ALUNO_RUBRICAS_MENSAIS_OBTERPORFILTRO";

                BaseDados.AddParameter("@ALUNO", dto.Matricula.Codigo);
                BaseDados.AddParameter("@ANO", dto.AnoLectivo);
                BaseDados.AddParameter("@ITEM", dto.Parcela.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeAlunoDTO();
                    dto.Matricula = new DTO.GestaoEscolar.Pedagogia.MatriculaDTO(new DTO.GestaoEscolar.Pedagogia.AlunoDTO(dr[0], dr[1], dr[2]));
                    dto.Parcela = new ParcelaMensalidadeDTO(int.Parse(dr[3]), null, dr[4], "", 0);
                    dto.Situacao = dr[5] == "1" ? "S" : "N";
                    dto.Classe = dr[6];
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
