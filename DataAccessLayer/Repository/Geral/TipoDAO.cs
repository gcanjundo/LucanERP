using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dominio.Geral;
using MySql.Data.MySqlClient;

namespace DataAccessLayer.Geral
{
    public class TipoDAO :ConexaoDB
    {


        public TipoDTO Adicionar(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ADICIONAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("@UTILIZADOR", dto.Utilizador); 
                dto.Codigo = ExecuteInsert();
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

        public TipoDTO Alterar(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_ALTERAR";
                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("SIGLA", dto.Sigla);
                AddParameter("SITUACAO", dto.Estado);
                AddParameter("CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@TIPO", dto.Operacao);

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

        public TipoDTO Eliminar(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_EXCLUIR";

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

        public List<TipoDTO> ObterPorFiltro(TipoDTO dto)
        {
            List<TipoDTO> listaTipoContactos;
            try
            {
                ComandText = "stp_GER_TIPO_OBTERPORFILTRO";

                AddParameter("DESCRICAO", dto.Descricao);
                AddParameter("@SIGLA", dto.Sigla);

                MySqlDataReader dr = ExecuteReader();

                listaTipoContactos = new List<TipoDTO>();

                while (dr.Read())
                {
                    dto = new TipoDTO();

                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    listaTipoContactos.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new TipoDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                listaTipoContactos = new List<TipoDTO>();
                listaTipoContactos.Add(dto);
            }
            finally
            {
                FecharConexao();
            }

            return listaTipoContactos;
        }

        public TipoDTO ObterPorPK(TipoDTO dto)
        {
            try
            {
                ComandText = "stp_GER_TIPO_OBTERPORPK";

                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new TipoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());


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

        public List<TipoDTO> GetEntityTypeList()
        {
            var lista = new List<TipoDTO>();

            lista.Add(new TipoDTO {Codigo = 1, Descricao = "CLIENTE", Sigla = "C", Operacao = "R"});
            lista.Add(new TipoDTO { Codigo = 2, Descricao = "FORNECEDOR", Sigla = "F", Operacao = "P" });
            lista.Add(new TipoDTO { Codigo = 3, Descricao = "ASSIONISTA/SÓCIO", Sigla = "AS", Operacao = "A" });
            lista.Add(new TipoDTO { Codigo = 4, Descricao = "ESTADO/ENTE PÚBLICO", Sigla = "E", Operacao = "A" });
            lista.Add(new TipoDTO { Codigo = 5, Descricao = "FORN. IMOBILIZADO", Sigla = "FI", Operacao = "P" });
            lista.Add(new TipoDTO { Codigo = 6, Descricao = "FUNCIONÁRIO", Sigla = "FN", Operacao = "A" });
            lista.Add(new TipoDTO { Codigo = 7, Descricao = "SINDICATO", Sigla = "SD", Operacao = "P" });
            lista.Add(new TipoDTO { Codigo = 8, Descricao = "SUBSCRITOR DE CAPITAL", Sigla = "SC", Operacao = "R" });
            lista.Add(new TipoDTO { Codigo = 9, Descricao = "OBRIGACIONISTA", Sigla = "O", Operacao = "R" }); 
            lista.Add(new TipoDTO { Codigo = 10, Descricao = "OUTRO CREDOR", Sigla = "OC", Operacao = "R" });
            lista.Add(new TipoDTO { Codigo = 11, Descricao = "OUTRO DEVEDOR", Sigla = "OD", Operacao = "P" });
            lista.Add(new TipoDTO { Codigo = 12, Descricao = "ENTIDADE BANCÁRIA", Sigla = "EB", Operacao = "R" });
            lista.Add(new TipoDTO { Codigo = 13, Descricao = "CLIENTE/FORNECEDOR", Sigla = "A", Operacao = "" });

            return lista;
        }
    }
}
