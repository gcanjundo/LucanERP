using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class InscricaoDisciplinaDAO
    {
        readonly ConexaoDB BaseDados;

        public InscricaoDisciplinaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public InscricaoDisciplinaDTO Salvar(InscricaoDisciplinaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_INSCRICAO_DISCIPLINA_ADICIONAR";

                BaseDados.AddParameter("@MATRICULA", dto.Aluno);
                BaseDados.AddParameter("@DISCIPLINA", dto.Disciplina);
                BaseDados.AddParameter("@TURMA", dto.Turma);
                BaseDados.AddParameter("@REGIME", dto.Regime);
                 
                BaseDados.AddParameter("@ESTADO", dto.Situacao);

                BaseDados.AddParameter("@INSCRICAO", DateTime.Now);
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

        public InscricaoDisciplinaDTO Excluir(InscricaoDisciplinaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_ACA_INSCRICAO_DISCIPLINA_EXCLUIR";

                 
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

        public List<InscricaoDisciplinaDTO> ObterPorFiltro(InscricaoDisciplinaDTO dto)
        {
            List<InscricaoDisciplinaDTO> lista = new List<InscricaoDisciplinaDTO>();
            try
            {
                BaseDados.ComandText = "stp_ACA_INSCRICAO_DISCIPLINA_OBTERPORFILTRO";


                BaseDados.AddParameter("@MATRICULA", dto.Aluno);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                while (dr.Read())
                {
                    dto = new InscricaoDisciplinaDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Aluno = dr[1];
                    dto.Disciplina = dr[2];
                    dto.Regime = dr[3];
                    dto.Turma = dr[4];
                    dto.Situacao = dr[5];

                    lista.Add(dto);
                }

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

            return lista;
        }
    }
}
