using System;
using System.Collections.Generic;
using System.Linq;

using Dominio.Geral;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.Geral
{
    public class StatusDAO: IAcessoBD<StatusDTO>
    {
        ConexaoDB BaseDados = new ConexaoDB();

        public StatusDTO Adicionar(StatusDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_STATUS_ADICIONAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@TIPO", dto.Operacao);
                dto.Codigo = BaseDados.ExecuteInsert();
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

        public StatusDTO Alterar(StatusDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_STATUS_ALTERAR";

                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("SIGLA", dto.Sigla);
                BaseDados.AddParameter("SITUACAO", dto.Estado);
                BaseDados.AddParameter("CODIGO", dto.Codigo);
                BaseDados.AddParameter("@UTILIZADOR", dto.Utilizador);
                BaseDados.AddParameter("@TIPO", dto.Operacao);

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

        public bool Eliminar(StatusDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_GER_STATUS_EXCLUIR";

                
                BaseDados.AddParameter("CODIGO", dto.Codigo);

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

            return dto.Sucesso;
        }

        public List<StatusDTO> ObterPorFiltro(StatusDTO dto)
        {
            List<StatusDTO> listaStatus = new List<StatusDTO>();
            try
            {
                
                BaseDados.ComandText = "stp_GER_STATUS_OBTERPORFILTRO";
                BaseDados.AddParameter("DESCRICAO", dto.Descricao);
                BaseDados.AddParameter("@SIGLA", dto.Sigla);


                MySqlDataReader dr = BaseDados.ExecuteReader();
                
                while (dr.Read()) 
                {
                    dto = new StatusDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    listaStatus.Add(dto);
                }
            }
            catch (Exception ex)
            {
                dto = new StatusDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return listaStatus;
        }

        public StatusDTO ObterPorPK(StatusDTO dto)
        {
            try
            {


                BaseDados.ComandText = "stp_GER_STATUS_OBTERPORPK";
                BaseDados.AddParameter("CODIGO", dto.Codigo);

                dto = new StatusDTO();
                MySqlDataReader dr = BaseDados.ExecuteReader();
                

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
                BaseDados.FecharConexao();
            }

            return dto;
        }

        public List<StatusDTO> DocumentStatusList() // Status dos Documentos de Facturação
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "RASCUNHO", ""),
                new StatusDTO(2, "EM FACTURAÇÃO", ""),
                new StatusDTO(3, "PENDENTE", ""),
                new StatusDTO(4, "A AGUARDAR APROVAÇÃO", "O"),
                new StatusDTO(5, "ACEITE", "O"),
                new StatusDTO(6, "RECUSADO(A)", ""),
                new StatusDTO(7, "ANULADO(A)", ""),
                new StatusDTO(8, "FINALIZADO(A)", ""), 
                new StatusDTO(9, "AGUARDANDO ENVIO/ENTREGA AO CLIENTE", ""),
                new StatusDTO(10, "EM PROCESSAMENTO", ""),
                new StatusDTO(11, "A AGUARDAR CONCLUSÃO DO PAGAMENTO", ""),

            };
            return lista;
        }

        public List<StatusDTO> DocumentLineStatusList() // Status das Linhas dos Documentos
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "PENDENTE", ""),
                new StatusDTO(2, "ENTREGUE PARCIAL", ""),
                new StatusDTO(3, "ENTREGUE", ""),
                new StatusDTO(4, "EM ESPERA", ""),
                new StatusDTO(5, "ANULADA", ""),
                new StatusDTO(6, "EM PROCESSAMENTO", ""),
                new StatusDTO(7, "EM PRONTA PARA ENTREGA", ""),
            };
            return lista;
        }


        public List<StatusDTO> PaymentStatusList() // Status do Pagamento do Documento
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "EM FACTURAÇÃO", ""),
                new StatusDTO(2, "PARCIALMENTE LÍQUIDADO(A)", ""),
                new StatusDTO(3, "LIQUIDADO(A)", ""),
                new StatusDTO(4, "NÃO LIQUIDADO(A)", ""), 
                new StatusDTO(5, "EM ATRASO", ""),
                new StatusDTO(6, "ANULADO(A)", ""),
            };
            return lista;
        }

        public List<StatusDTO> CustomerOrderStatusList() // Status de Encomedas de Clientes
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "EM RASCUNHO", ""),
                new StatusDTO(3, "PENDENTE", ""),
                new StatusDTO(8, "SATISFEITA(FINALIZADO)", ""),
                new StatusDTO(4, "PARCIALMENTE SATISFEITA", ""),
                new StatusDTO(7, "ANULADA", "")
            };
            return lista;
        }

        public List<StatusDTO> CustomerRequestServiceStatusList() // Status de Requisição de Serviços de Clientes
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "EM RASCUNHO", ""),
                new StatusDTO(2, "PENDENTE", ""),
                new StatusDTO(3, "EM PROCESSAMENTO", ""),
                new StatusDTO(4, "PARCIALMENTE PROCESSADA", ""), 
                new StatusDTO(5, "AGUARDANDO ENTREGA", ""),
                new StatusDTO(6, "ENTREGUE PARCIALMENTE", ""),
                new StatusDTO(7, "ANULADA", ""),
                new StatusDTO(8, "ENTREGUE E ENCERRADA", ""),
            };
            return lista;
        }

        public List<StatusDTO> CustomerRequestServicesItemStatusList() // Status Linhas de Requisição de Serviços
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "EM ESPERA", "W"),
                new StatusDTO(2, "A RECOLHER AMOSTRA", "R"),
                new StatusDTO(3, "EM PROCESSAMENTO", "P"),
                new StatusDTO(4, "PROCESSADO", "F"),
                new StatusDTO(5, "ENTREGUE", "E"),
                new StatusDTO(6, "EXCLUÍDO", "X"),
            };
            return lista;
        }

        public List<StatusDTO> KitchenItemStatus() // Status das Linhas dos Documentos
        {
            List<StatusDTO> lista = new List<StatusDTO>
            {
                new StatusDTO(1, "EM ESPERA", "W"),
                new StatusDTO(2, "EM PREPARAÇÃO", "P"),
                new StatusDTO(3, "PREPARADO", "C"),
                new StatusDTO(4, "ENTREGUE", "E"),
                new StatusDTO(5, "CANCELADO", "X"), 
            };
            return lista;
        }


        public List<StatusDTO> ClinicalScheduleStatusList() // Status das Marcações de Consulta e Ordens de Serviço
        {
            List<StatusDTO> lista = new List<StatusDTO>();
            try
            { 
                BaseDados.ComandText = "stp_CLI_STATUS_MARCACAO_OBTERPORFILTRO"; 

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                   StatusDTO  dto = new StatusDTO();
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Descricao = dr[1].ToString();
                    dto.Sigla = dr[2].ToString();
                    dto.Estado = int.Parse(dr[3].ToString());

                    lista.Add(dto);
                }

                lista.Insert(0, new StatusDTO { Codigo = -1, Descricao = "-Seleccione-" });
            }
            catch (Exception ex)
            {
                 
            }
            finally
            {
                BaseDados.FecharConexao();
            }

            return lista;
        } 

    }
}
