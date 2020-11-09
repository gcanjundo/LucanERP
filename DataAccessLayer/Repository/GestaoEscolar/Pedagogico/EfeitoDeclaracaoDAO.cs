using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class EfeitoDeclaracaoDAO 
    {
        readonly ConexaoDB BaseDados;

        public EfeitoDeclaracaoDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public EfeitoDeclaracaoDTO Inserir(EfeitoDeclaracaoDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_GER_EFEITO_ADICIONAR";
            

                BaseDados.AddParameter("@DESCRICAO", dto.EfeDescricao);
                BaseDados.AddParameter("@SITUACAO", "A");
                BaseDados.AddParameter("@SIGLA", dto.EfeTipo);
                BaseDados.AddParameter("@CODIGO", dto.EfeCodigo);

                dto.EfeCodigo = BaseDados.ExecuteInsert();
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

        public void Apagar(EfeitoDeclaracaoDTO dto)
        {
           

            try
            {
                BaseDados.ComandText = "stp_GER_EFEITO_EXCLUIR";
                BaseDados.AddParameter("@CODIGO", dto.EfeCodigo);

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

        public EfeitoDeclaracaoDTO ObterPorPK(EfeitoDeclaracaoDTO dto)
        {
           
            try
            {
                BaseDados.ComandText = "stp_GER_EFEITO_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", dto.EfeCodigo);


                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new EfeitoDeclaracaoDTO();
                while (dr.Read())
                {
                    dto.EfeCodigo = Int32.Parse(dr["EFE_CODIGO"].ToString());
                    dto.EfeDescricao = dr["EFE_DESCRICAO"].ToString();
                    dto.EfeTipo = dr["EFE_SIGLA"].ToString();
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



        public List<EfeitoDeclaracaoDTO> ObterPorFiltro(EfeitoDeclaracaoDTO dto)
        {
            
            List<EfeitoDeclaracaoDTO> lista = new List<EfeitoDeclaracaoDTO>();
            try
            {
                BaseDados.ComandText = "stp_GER_EFEITO_OBTERPORFILTRO";
                BaseDados.AddParameter("@DESCRICAO", dto.EfeDescricao);
                BaseDados.AddParameter("@CODIGO", dto.EfeCodigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new EfeitoDeclaracaoDTO();

                    dto.EfeCodigo = Int32.Parse(dr["EFE_CODIGO"].ToString());
                    dto.EfeDescricao = dr["EFE_DESCRICAO"].ToString(); 
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
