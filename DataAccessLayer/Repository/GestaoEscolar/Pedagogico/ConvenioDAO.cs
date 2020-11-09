using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Dominio.GestaoEscolar.Pedagogia;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class ConvenioDAO 
    {
        readonly ConexaoDB BaseDados;

        public ConvenioDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public ConvenioDTO Adicionar(ConvenioDTO dto) 
        {
           
            try
            {

                string _commandText= "spt_ACA_CONVENIO_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@INSTITUICAO", dto.Instituicao);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@ESTADO", dto.Estado);
                
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

        public ConvenioDTO Alterar(ConvenioDTO dto)
        {

            try
            {

                BaseDados.ComandText = "spt_ACA_CONVENIO_ALTERAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@INSTITUICAO", dto.Instituicao);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@ESTADO", dto.Estado);

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

        public ConvenioDTO Apagar(ConvenioDTO dto)
        {

            try
            {

                BaseDados.ComandText = "spt_ACA_CONVENIO_EXCLUIR";

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

        public ConvenioDTO ObterPorCodigo(ConvenioDTO dto) 
        {
           
            try
            {
                BaseDados.ComandText = "spt_ACA_CONVENIO_OBTERPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new ConvenioDTO();
                while (dr.Read()) 
                {
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Tipo = dr[1];
                    dto.Instituicao = int.Parse(dr[2]);
                    dto.Descricao = dr[3];
                    dto.Inicio = Convert.ToDateTime(dr[4]);
                    dto.Termino = Convert.ToDateTime(dr[5]);
                    dto.Estado = int.Parse(dr[6]);
                    dto.NomeInstituicao = dr[7];
                    
                    
                    
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

        public List<ConvenioDTO> ObterPorFiltro(ConvenioDTO dto)
        {
            List<ConvenioDTO> convenios;
            
            try
            {
                BaseDados.ComandText = "spt_ACA_CONVENIO_OBTERPORFILTRO";

                BaseDados.AddParameter("@INSTITUICAO", dto.Instituicao);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@DESCRICAO",dto.Descricao);
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                convenios = new List<ConvenioDTO>();
                while (dr.Read())
                {
                    dto = new ConvenioDTO();
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Tipo = dr[1];
                    dto.Instituicao = int.Parse(dr[2]);
                    dto.Descricao = dr[3];
                    dto.Inicio = Convert.ToDateTime(dr[4]);
                    dto.Termino = Convert.ToDateTime(dr[5]);
                    dto.Estado = int.Parse(dr[6]);
                    dto.NomeInstituicao = dr[7];
                    

                    convenios.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new ConvenioDTO();
                convenios = new List<ConvenioDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                convenios.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
                
            }
            return convenios;

        }


    }
}
