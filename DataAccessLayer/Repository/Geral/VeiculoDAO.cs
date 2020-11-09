using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class VeiculoDAO:ConexaoDB 
    {
         

        public VeiculoDTO Adicionar(VeiculoDTO dto)
        {
            try
            {

                ComandText = "stp_GER_VIATURA_ADICIONAR";

                AddParameter("CODIGO", dto.VeiculoID);
                AddParameter("DESIGNACAO", dto.Designation);
                AddParameter("MATRICULA_ID", dto.MatriculaID);
                AddParameter("OWNER_ID", dto.OwnerID);
                AddParameter("CHASSI", dto.ChassiNumber);
                AddParameter("MOTOR", dto.MotorNumber);
                AddParameter("MARCA_ID", dto.MarcaID);
                AddParameter("FLUEL_ID", dto.FluelID);
                AddParameter("CATEGORY_ID", dto.CategoryID);
                AddParameter("CILINDRADA", dto.Cilindrada);
                AddParameter("COLOR_ID", dto.ColorID);
                AddParameter("IMAGE_PATH", dto.PathImage);
                AddParameter("SITUACAO", dto.Status);
                AddParameter("CAIXA", dto.CaixaVelocidade);
                AddParameter("UTILIZADOR", dto.Utilizador);
                AddParameter("CILINDROS", dto.NroCilindros);
                AddParameter("PNEUMATICOS", dto.Pnematicos);
                AddParameter("PESO", dto.Peso);
                AddParameter("TARA", dto.Tara);
                AddParameter("EIXOS", dto.Eixos);
                AddParameter("PORTAS", dto.Portas);
                AddParameter("ANO", dto.AnoFabrico);
                AddParameter("CLASSE", dto.Classe);
                AddParameter("VALVULAS", dto.NroValvulas); 

                dto.VeiculoID = ExecuteInsert();
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
         

        public VeiculoDTO Eliminar(VeiculoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_VIATURA_EXCLUIR";
                 
                AddParameter("CODIGO", dto.VeiculoID);
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

        public List<VeiculoDTO> ObterPorFiltro(VeiculoDTO dto)
        {
            List<VeiculoDTO> lista = new List<VeiculoDTO>();
            try
            {
                ComandText = "stp_GER_VIATURA_OBTERPORFILTRO";

                AddParameter("@DESIGNACAO", dto.Designation == null ? string.Empty : dto.Designation);
                AddParameter("@MARCA_ID", dto.MarcaID);
                AddParameter("@MODELO_ID", dto.ModeloID);
                AddParameter("@MATRICULA_ID", dto.MatriculaID);
                AddParameter("@OWNER_ID", dto.OwnerID);  
                MySqlDataReader dr = ExecuteReader();



                while (dr.Read())
                {
                    dto = new VeiculoDTO();

                    dto.VeiculoID = int.Parse(dr[0].ToString());
                    dto.Designation = dr[1].ToString();
                    dto.MatriculaID = dr[2].ToString();
                    dto.DesignacaoEntidade = dr[3].ToString();
                    dto.LastIntervencao = dr[4].ToString() == null || dr[4].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[4].ToString());
                    dto.PathImage = dr[5].ToString() == string.Empty ? "~/imagens/SemFoto.jpg" : dr[5].ToString();
                    dto.NextRevision = dr[6].ToString() == null || dr[6].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[6].ToString());
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
                FecharConexao();
            }

            return lista;
        }

        public VeiculoDTO ObterPorPK(VeiculoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_VIATURA_OBTERPORPK";

                AddParameter("CODIGO", dto.VeiculoID);

                MySqlDataReader dr = ExecuteReader();

                dto = new VeiculoDTO();

                if (dr.Read())
                {
                    dto.VeiculoID = int.Parse(dr[0].ToString());
                    dto.MatriculaID = dr[1].ToString();
                    dto.OwnerID = int.Parse(dr[2].ToString());
                    dto.Designation = dr[3].ToString();
                    dto.ChassiNumber = dr[4].ToString();
                    dto.MotorNumber = dr[5].ToString();
                    dto.MarcaID = int.Parse(dr[6].ToString());
                    string marca = dr["MAR_ROOT"].ToString() == null || dr["MAR_ROOT"].ToString()==string.Empty ? "-1" : dr["MAR_ROOT"].ToString();
                    if (marca != "-1")
                    {
                        dto.ModeloID = (int)dto.MarcaID;
                        dto.MarcaID = marca;
                    }
                    else
                    {
                        dto.ModeloID = -1;
                    }

                    dto.FluelID = int.Parse(dr[7].ToString());
                    dto.CategoryID = int.Parse(dr[8].ToString());
                    dto.Cilindrada = decimal.Parse(dr[9].ToString());
                    dto.ColorID = int.Parse(dr[10].ToString());
                    dto.PathImage = dr[11].ToString();
                    dto.Status = int.Parse(dr[12].ToString());
                    dto.CaixaVelocidade = dr[13].ToString();
                    dto.LastIntervencao = dr[14].ToString() == null || dr[14].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[14].ToString());
                    dto.NextRevision = dr[15].ToString() == null || dr[15].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[15].ToString()); 
                    dto.CreatedBy = dr[16].ToString();
                    dto.CreatedDate = dr[17].ToString() == null || dr[17].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[17].ToString());
                    dto.UpdatedBy = dr[18].ToString();
                    dto.UpdatedDate = dr[19].ToString() == null || dr[19].ToString() == string.Empty ? DateTime.MinValue : DateTime.Parse(dr[19].ToString());
                    dto.NroCilindros = int.Parse(dr[20].ToString());
                    dto.Pnematicos =dr[21].ToString();
                    dto.Peso = decimal.Parse(dr[22].ToString());
                    dto.Tara = decimal.Parse(dr[23].ToString());
                    dto.Eixos = decimal.Parse(dr[24].ToString());
                    dto.Portas = int.Parse(dr[25].ToString());
                    dto.AnoFabrico = int.Parse(dr[26].ToString());
                    dto.Classe = dr[27].ToString();
                    dto.NroValvulas = int.Parse(dr[28].ToString());
                    dto.DesignacaoEntidade = dr[30].ToString();
                    dto.LookupField1 = dr[31].ToString();
                    dto.LookupField2 = dr[32].ToString();
                    dto.LookupField3 = dr[33].ToString();
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

            return dto;
        }

        
    }
}
