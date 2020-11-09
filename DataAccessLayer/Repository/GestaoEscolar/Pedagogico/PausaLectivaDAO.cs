using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.GestaoEscolar.Pedagogia;
using System.Data;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.GestaoEscolar.Pedagogia
{
    public class PausaLectivaDAO 
    {

        readonly ConexaoDB BaseDados;

        public PausaLectivaDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public PausaLectivaDTO Adicionar(PausaLectivaDTO dto)
        {
            try
            {

                BaseDados.ComandText = "spt_ACA_ANO_LECTIVO_CALENDARIO_PAUSA_ADICIONAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@NATUREZA", dto.Descricao);
                 

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

        public PausaLectivaDTO Alterar(PausaLectivaDTO dto)
        {
            try
            {

                BaseDados.ComandText = "spt_ACA_ANO_LECTIVO_CALENDARIO_PAUSA_ALTERAR";

                BaseDados.AddParameter("@CODIGO", dto.Codigo);
                BaseDados.AddParameter("@INICIO", dto.Inicio);
                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@TERMINO", dto.Termino);
                BaseDados.AddParameter("@NATUREZA", dto.Descricao);


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

        public PausaLectivaDTO Apagar(PausaLectivaDTO dto)
        {
            try
            {

                BaseDados.ComandText = "spt_ACA_ANO_LECTIVO_CALENDARIO_PAUSA_EXCLUIR";

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

        public PausaLectivaDTO ObterPorCodigo(PausaLectivaDTO dto)
        {
            try
            {
                BaseDados.ComandText = "spt_ACA_ANO_LECTIVO_CALENDARIO_PAUSA_OBTERPK";
                BaseDados.AddParameter("@CODIGO", dto.Codigo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                dto = new PausaLectivaDTO();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Periodo = int.Parse(dr[1]);
                    dto.Inicio = Convert.ToDateTime(dr[2]);
                    dto.Termino = Convert.ToDateTime(dr[3]);
                    dto.NomePeriodo = dr[4];
                    dto.Descricao = dr[5];
                    dto.IsPausa = true;
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

        public List<PausaLectivaDTO> ObterPorFiltro(PausaLectivaDTO dto)
        {
            List<PausaLectivaDTO> pausas;

            try
            {
                BaseDados.ComandText = "spt_ACA_ANO_LECTIVO_CALENDARIO_PAUSA_OBTERPORFILTRO";

                BaseDados.AddParameter("@PERIODO", dto.Periodo);
                BaseDados.AddParameter("@NATUREZA", dto.Descricao);

                MySqlDataReader dr = BaseDados.ExecuteReader();
                pausas = new List<PausaLectivaDTO>();
                while (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0]);
                    dto.Periodo = int.Parse(dr[1]);
                    dto.Inicio = Convert.ToDateTime(dr[2]);
                    dto.Termino = Convert.ToDateTime(dr[3]);
                    dto.NomePeriodo = dr[4];
                    dto.Descricao = dr[5];
                    dto.IsPausa = true;

                    pausas.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new PausaLectivaDTO();
                pausas = new List<PausaLectivaDTO>();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                pausas.Add(dto);
            }
            finally
            {
                BaseDados.FecharConexao();

            }
            return pausas;

        }
    }
}
