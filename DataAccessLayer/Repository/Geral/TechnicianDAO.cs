using Dominio.Geral;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace DataAccessLayer.Geral
{
    public class TechnicianDAO
    {
        readonly ConexaoDB BaseDados;

        public TechnicianDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public TechnicianDTO Adicionar(TechnicianDTO dto)
        {   
             
            try
            {
                BaseDados.ComandText = "stp_GER_TECNICO_ADICIONAR";
                BaseDados.AddParameter("@ENTIDADE", dto.Entity.Codigo);
                BaseDados.AddParameter("@COMISSAO", dto.Comissao <=0 ? (object)DBNull.Value : dto.Comissao);
                BaseDados.AddParameter("@FILIAL", dto.Entity.Filial);
                BaseDados.AddParameter("@GRUPO", dto.GroupID <=0 ? (object)DBNull.Value : dto.GroupID);
                BaseDados.AddParameter("@VALOR_COMISSAO", dto.ValorComissao);
                BaseDados.AddParameter("@USERNAME", dto.Utilizador);
                BaseDados.AddParameter("@UTILIZADOR", dto.CreatedBy);
                BaseDados.AddParameter("@NOME_TECNICO", dto.Entity.NomeCompleto);
                BaseDados.AddParameter("@SENHA", dto.CurrentPassword);
                BaseDados.AddParameter("@PERFIL", dto.UserProfile);
                BaseDados.AddParameter("@EMAIL", dto.Email);
                BaseDados.AddParameter("@TIPO", dto.Tipo); 

                BaseDados.ExecuteNonQuery();
                dto.Sucesso = true; 
            }
            catch(Exception ex)
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

        public TechnicianDTO Excluir(TechnicianDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_TECNICO_EXCLUIR"; 
                BaseDados.AddParameter("@ENTIDADE", dto.Entity.Codigo);
                

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

        public TechnicianDTO ObterPorPK(TechnicianDTO pTecnico)
        {

            try
            {
                BaseDados.ComandText = "stp_GER_TECNICO_OBTERPORPK";
                BaseDados.AddParameter("@CODIGO", pTecnico.Entity.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader(); 
                
                pTecnico = new TechnicianDTO();

                if (dr.Read())
                {     
                    pTecnico.Entity = new EntidadeDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        NomeCompleto = dr[1].ToString(),
                        Categoria = dr[2].ToString(),
                        Identificacao = dr[3].ToString(),
                        Nacionalidade = !String.IsNullOrEmpty(dr[4].ToString()) ? int.Parse(dr[4].ToString()) : -1,
                        Rua = dr[5].ToString(),
                        Bairro = dr[6].ToString(),
                        Provincia = dr[7].ToString(),
                        MunicipioMorada = int.Parse(dr[8].ToString()),
                        Telefone = dr[9].ToString(),
                        TelefoneAlt = dr[10].ToString(),
                        Email = dr[11].ToString(), 
                    };    
                    pTecnico.FuncionarioID = dr[12].ToString();
                    pTecnico.GroupID = !String.IsNullOrEmpty(dr[13].ToString()) ? int.Parse(dr[13].ToString()) : -1; 
                    pTecnico.Tipo = dr[14].ToString();
                    pTecnico.Filial = !String.IsNullOrEmpty(dr[13].ToString()) ? dr[13].ToString() : "-1";
                    pTecnico.ValorComissao = decimal.Parse(dr["COM_COMISSAO"].ToString());
                }
            }
            catch (Exception ex)
            {
                pTecnico.Sucesso = false;
                pTecnico.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return pTecnico;
        }

        public List<TechnicianDTO> ObterPorFiltro(TechnicianDTO dto)
        {
            List<TechnicianDTO> lista;

            try
            {
                BaseDados.ComandText = "stp_GER_TECNICO_OBTERPORFILTRO";

                BaseDados.AddParameter("@NOME", dto.Entity.NomeCompleto);
                BaseDados.AddParameter("@FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                lista = new List<TechnicianDTO>();
                while (dr.Read())
                {
                    dto = new TechnicianDTO();
                    var entity = new EntidadeDTO();

                    entity.Codigo = int.Parse(dr[0].ToString());
                    entity.NomeCompleto = dr[1].ToString();
                    entity.Telefone = dr[2].ToString();
                    entity.TelefoneAlt = dr[3].ToString();
                    dto.Entity = entity;
                    dto.ValorComissao = decimal.Parse(dr[4].ToString());
                    dto.DesignacaoEntidade = dr[1].ToString();
                    dto.ProfissionalID = int.Parse(dr[0].ToString());
                    dto.FuncionarioID = dr[7].ToString();
                    lista.Add(dto);
                }


            }
            catch (Exception ex)
            {
                dto = new TechnicianDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

                lista = new List<TechnicianDTO>();
                lista.Add(dto);

            }
            finally
            {

                BaseDados.FecharConexao();
            }

            return lista;
        }
    }
}
