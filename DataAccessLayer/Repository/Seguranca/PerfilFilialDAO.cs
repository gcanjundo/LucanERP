using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Seguranca;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Seguranca
{
    public class PerfilFilialDAO:ConexaoDB
    {
        public void AdicionarPerfil(PerfilEmpresaDTO dto)
        {
            ComandText = "stp_SIS_PERFIL_FILIAL_ADICIONAR";

            
            AddParameter("@FILIAL", dto.Sucursal.Codigo);
            AddParameter("@PERFIL", dto.Perfil.Codigo);

            try
            {
                ExecuteNonQuery();
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
        }

        public void ExcluirPerfil(PerfilEmpresaDTO dto)
        {
            ComandText = "stp_SIS_PERFIL_FILIAL_EXCLUIR";


            AddParameter("@FILIAL", dto.Sucursal.Codigo);
            AddParameter("@PERFIL", dto.Perfil.Codigo);

            try
            {
                ExecuteNonQuery();
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
        }

        public List<PerfilEmpresaDTO> ObterPerfisFilial(PerfilEmpresaDTO dto)
        {
            List<PerfilEmpresaDTO> Perfis = new List<PerfilEmpresaDTO>();
            try
            {

                ComandText = "stp_SIS_PERFIL_FILIAL_OBTERPORFILTRO";

                AddParameter("@FILIAL", dto.Sucursal.Codigo);
                AddParameter("@PERFIL", dto.Perfil.Codigo);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    dto = new PerfilEmpresaDTO();

                    EmpresaDTO objFilial = new EmpresaDTO();
                    PerfilDTO objPerfil = new PerfilDTO();
                    objFilial.Codigo = int.Parse(dr[0].ToString());

                    objPerfil.Codigo = int.Parse(dr[1].ToString());
                    objPerfil.Descricao = dr[2].ToString(); 
                    objFilial.NomeCompleto = dr[3].ToString();
                    objFilial.NomeComercial = dr[4].ToString();
                    objFilial.Categoria =  dr[5].ToString();
                    dto.Sucursal = objFilial;
                    dto.Perfil = objPerfil;

                    Perfis.Add(dto);
                }

            }
            catch (Exception ex) 
            {
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                FecharConexao();
            }

            return Perfis;
        }
    }
}
