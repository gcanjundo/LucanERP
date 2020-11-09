using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class UtilizadorPermissaoModuloDAO
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public void Inserir(PermissaoModuloDTO dto)
        {

            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_MODULO_NOVO";


                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@VISIBILIDADE", dto.Visibilidade);
                BaseDados.AddParameter("@AUTORIZACAO", dto.Autorizar);
                BaseDados.AddParameter("@ACESSO", dto.Acesso);

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public void Excluir(PermissaoModuloDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_MODULO_EXCLUIR";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }

        }

        public void ExcluirTodas(PermissaoModuloDTO dto)
        {
            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_MODULO_EXCLUIR_TODAS";

                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                BaseDados.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }
             

        }

        public List<PermissaoModuloDTO> ObterPermissoesModulo(UtilizadorDTO pUtilizador)
        {
            PermissaoModuloDTO dto;

            List<PermissaoModuloDTO> coleccao = new List<PermissaoModuloDTO>();
            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_MODULO_OBTERPORUTILIZADOR";

                BaseDados.AddParameter("@UTILIZADOR", pUtilizador.Utilizador);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read())
                {
                   dto = new PermissaoModuloDTO();
                    ModuloDTO dtoMod = new ModuloDTO();
                    ModuloDAO daoMod = new ModuloDAO();
                    dtoMod.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Modulo = daoMod.ObterPorPK(dtoMod);

                    coleccao.Add(dto);
                }
                 
                

            }
            catch (Exception ex)
            {
                dto = new PermissaoModuloDTO();
                dto.MensagemErro = ex.Message.Replace("'", "");
                dto.Sucesso = false;
                coleccao = new List<PermissaoModuloDTO>();
                coleccao.Add(dto);
            }
            finally 
            {
                BaseDados.FecharConexao();
            }
            return coleccao;


        }

        public List<PermissaoModuloDTO> ObterModulosDoMenu(UtilizadorDTO pUtilizador)
        {
            List<PermissaoModuloDTO> coleccao;
            PermissaoModuloDTO dto;

            try
            {

                coleccao = new List<PermissaoModuloDTO>();

                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_UTILIZADOR_MODULOS";

                BaseDados.AddParameter("@UTILIZADOR", pUtilizador.Utilizador);
                BaseDados.AddParameter("@PERFIL", pUtilizador.Perfil.Codigo);


                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new PermissaoModuloDTO();
                    ModuloDTO dtoMod = new ModuloDTO();
                    ModuloDAO daoMod = new ModuloDAO();
                    dtoMod.Codigo = Int32.Parse(dr["MOD_CODIGO"].ToString());
                    dto.Modulo = daoMod.ObterPorPK(dtoMod);

                    coleccao.Add(dto);
                }

            }
            catch (Exception ex)
            {
                coleccao = new List<PermissaoModuloDTO>();
                dto = new PermissaoModuloDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                coleccao.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return coleccao;


        }

        public PermissaoModuloDTO ObterPermissaoModuloPorPK(PermissaoModuloDTO dto)
        {

            try
            {
                BaseDados.ComandText ="stp_SIS_UTILIZADOR_PERMISSAO_MODULO_OBTERPORPK";

                BaseDados.AddParameter("@MODULO", dto.Modulo.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                if (dr.Read())
                {
                    dto = new PermissaoModuloDTO();

                    dto.Acesso = Int32.Parse(dr["UTI_PERM_MOD_ACESSO"].ToString());
                    dto.Visibilidade = Int32.Parse(dr["UTI_PERM_MOD_VISUALIZAR"].ToString());
                    dto.Autorizar = Int32.Parse(dr["UTI_PERM_MOD_AUTORIZAR"].ToString());

                    ModuloDTO dtoMod = new ModuloDTO();
                    ModuloDAO daoMod = new ModuloDAO();
                    dtoMod.Codigo = Int32.Parse(dr["UTI_PERM_MOD_CODIGO_MODULO"].ToString());
                    dto.Modulo = daoMod.ObterPorPK(dtoMod);

                    UtilizadorDAO daoUtilizador = new UtilizadorDAO();
                    UtilizadorDTO dtoUtilizador = new UtilizadorDTO();
                    dto.Utilizador = dr["UTI_PERM_MOD_UTILIZADOR"].ToString();


                }
            }
            catch (Exception ex)
            {
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
