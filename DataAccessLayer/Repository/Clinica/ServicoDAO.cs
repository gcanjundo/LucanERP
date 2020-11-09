using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Clinica
{
    public class ServicoDAO: ConexaoDB 
    {
         

        public ArtigoDTO Adicionar(ArtigoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_SERVICO_ADICIONAR";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("ESPECIALIDADE", dto.Especialidade);
                AddParameter("PRECO", dto.PrecoVenda);
                AddParameter("UNIDADE", dto.UnidadeVenda);
                AddParameter("DURACAO", dto.DuracaoPreparo);
                AddParameter("ESTADO", dto.Status); 
                AddParameter("TIPO", dto.Categoria??(object)DBNull.Value);
                AddParameter("UTILIZADOR", dto.Utilizador);
                ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

      
        public ServicoDTO Eliminar(ServicoDTO dto)
        {
            try
            {
                ComandText = "stp_CLI_SERVICO_EXCLUIR";
                 
                AddParameter("CODIGO", dto.Codigo);

                dto.Codigo = ExecuteNonQuery();
                dto.Sucesso = true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }


        public List<ServicoDTO> ObterPorFiltro(ServicoDTO dto)
        {
            List<ServicoDTO> listaServicos = new List<ServicoDTO>();
            try
            {
                ComandText = "stp_CLI_SERVICO_OBTERPORFILTRO";

                AddParameter("CODIGO", dto.Codigo);
                AddParameter("@DESCRICAO", dto.Descricao);

                MySqlDataReader dr = ExecuteReader();

                while(dr.Read())
                {
                   dto = new ServicoDTO();

                   dto.Codigo = int.Parse(dr[0].ToString());
                   dto.Especialidade = dr[1].ToString();
                   dto.Referencia = dr[2].ToString();
                   dto.PrecoVenda = decimal.Parse(dr[3].ToString());  

                   listaServicos.Add(dto);
                }

            }
            catch (Exception ex)
            { 
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", ""); 
            }
            finally
            {
                FecharConexao();
            }

            return listaServicos;
        }
       
    }
}
