
using Dominio.Comercial.Restauracao;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Comercial.Restauracao
{
    public class AtendimentoDAO : ConexaoDB
    {

        public AtendimentoDTO Adicionar(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_ADICIONAR";

                // CODIGO, ATENDENTE, CLIENTE, INICIO, TERMINO, FILIAL, DESTINO, OUT LSTID
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@ATENDENTE", dto.Funcionario);
                AddParameter("@CLIENTE", dto.Cliente);
                AddParameter("@INICIO", dto.Inicio);
                AddParameter("@TERMINO", dto.Termino);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@MESA_ID", dto.Mesa);
                AddParameter("@SITUACAO", dto.Status);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@FATURA", dto.Fatura); 

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

        public AtendimentoDTO Fechar(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_FECHAR";

                // CODIGO, ATENDENTE, CLIENTE, INICIO, TERMINO, FILIAL, DESTINO, OUT LSTID
                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@ATENDENTE", dto.Funcionario);
                AddParameter("@CLIENTE", dto.Cliente); 
                AddParameter("@TERMINO", dto.Termino);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@DESTINO", dto.Destino);
                AddParameter("@SITUACAO", dto.Status);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@FATURA", dto.Fatura);

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

        public AtendimentoDTO Eliminar(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_EXCLUIR";

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

        public AtendimentoDTO CustomerUpdate(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_ACTUALIZAR_CLIENTE";

                AddParameter("CUSTOMER_ID", dto.Cliente);
                AddParameter("CONTA", dto.Conta);
                AddParameter("ATENDIMENTO_ID", dto.Codigo);

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

        public List<AtendimentoItemDTO> ObterConsulta(AtendimentoDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<AtendimentoDTO> ObterPorFiltro(AtendimentoDTO dto)
        {
            List<AtendimentoDTO> lista;
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_OBTERPORFILTRO";

                // ATENDENTE, CLIENTE, FILIAL
                AddParameter("@ATENDIMENTO_ID", dto.Codigo);
                AddParameter("@CREATION_INI", dto.LookupDate1 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate1);
                AddParameter("@CREATION_TERM", dto.LookupDate2 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate2); 
                AddParameter("@CUSTOMER_ID", dto.Cliente);
                AddParameter("@EMPLOYEE_ID", dto.FuncionarioID);
                AddParameter("@SITUACAO", dto.Situacao);
                AddParameter("@FILIAL", dto.Filial);
                AddParameter("@CLOSED_INI", dto.LookupDate3 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate3);
                AddParameter("@CLOSED_TERM", dto.LookupDate4 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate4);
                AddParameter("@CANCELED_INI", dto.LookupDate5 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate5);
                AddParameter("@CANCELED_TERM", dto.LookupDate6 == DateTime.MinValue ? (object)DBNull.Value : dto.LookupDate6);  
                AddParameter("@INVOICE", dto.TituloDocumento);
                AddParameter("@CREATED_BY", dto.CreatedBy);
                AddParameter("@CLOSED_BY", dto.LookupField1);
                AddParameter("@CANCELED_BY", dto.UpdatedBy);



                MySqlDataReader dr = ExecuteReader();

                lista = new List<AtendimentoDTO>();

                while (dr.Read())
                {
                     
                    dto = new AtendimentoDTO
                    {
                        Codigo = int.Parse(dr[0].ToString()),
                        Conta = decimal.Parse(dr[1].ToString()),
                        Funcionario = dr[2].ToString(),
                        Inicio = DateTime.Parse(dr[3].ToString()),
                        Cliente = dr[4].ToString(),
                        Mesa = dr[6].ToString(),
                        Termino = DateTime.Parse(dr[7].ToString() == "" ? DateTime.MinValue.ToShortDateString() : dr[7].ToString()),
                        Situacao = GetStatus(dr[8].ToString()),
                        CreatedBy = dr[9].ToString(),
                        LookupField1 = dr[10].ToString(),
                        UpdatedBy = dr[11].ToString(),
                        TituloDocumento = dr[12].ToString()
                    };
                    dto.LookupField2 = dto.Termino != DateTime.MinValue ? dto.Termino.ToString() : string.Empty;

                    lista.Add(dto);
                }

            }
            catch (Exception ex)
            {
                dto = new AtendimentoDTO
                {
                    Sucesso = false,
                    MensagemErro = ex.Message.Replace("'", ""),
                    Funcionario = ex.Message.Replace("'", "")
                }; 

                lista = new List<AtendimentoDTO>
                {
                    dto
                };
            }
            finally
            {
                FecharConexao();
            }

            return lista;
        }

        public void TrocarMesa(AtendimentoDTO dto)
        {
            throw new NotImplementedException();
        }

        private string GetStatus(string PStatus)
        {
            if(PStatus == "0")
            {
                return "ABERTO";
            }else if (PStatus == "C")
            {
                return "CANCELADO";
            }
            else
            {
                return "FECHADO";
            }
        }

        

        public AtendimentoDTO ObterPorPK(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_OBTERPORPK";

                // CODIGO
                AddParameter("CODIGO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                dto = new AtendimentoDTO();

                if (dr.Read())
                {
                    dto.Codigo = int.Parse(dr[0].ToString());
                    dto.Funcionario = dr[1].ToString();
                    dto.Cliente = dr[2].ToString();
                    dto.Inicio = DateTime.Parse(dr[3].ToString());
                    dto.Termino = DateTime.Parse(dr[4].ToString() == "" ? DateTime.MinValue.ToShortDateString() : dr[4].ToString());
                    dto.Filial = dr[5].ToString();
                    dto.Destino = int.Parse(dr[6].ToString());
                    dto.Situacao = dr[7].ToString();
                    dto.FuncionarioID = dr[8].ToString();
                    dto.Entidade = int.Parse(dr[9].ToString());
                    dto.Utilizador = dr[10].ToString();
                }

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

        public AtendimentoItemDTO AdicionarItem(AtendimentoItemDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_ITEM_ADICIONAR"; 

                AddParameter("@ATENDIMENTO", dto.Atendimento);
                AddParameter("@ARTIGO", dto.Artigo);
                AddParameter("@QUANTIDADE", dto.Quantidade);
                AddParameter("@PRECO", dto.Preco);
                AddParameter("@TOTAL", dto.Total);
                AddParameter("@SOLICITANTE", dto.Solicitante);
                AddParameter("@ORDEM", dto.NroOrdenacao);
                AddParameter("@PEDIDO_ID", dto.OrderID == null ? (object)DBNull.Value : (int)dto.OrderID);
                AddParameter("@DELETED", dto.Deleted  ? 0 : 1);
                AddParameter("@DELETED_BY", dto.UpdatedBy);
                AddParameter("@DELETED_DATE", dto.UpdatedDate == DateTime.MinValue ? (object)DBNull.Value : dto.UpdatedDate); 
                AddParameter("@MOTIVO_ANULACAO", dto.DeleteNotes);
                AddParameter("@OBSERVACOES", dto.Notes);
                AddParameter("@ITEM_STATUS", dto.Situacao);
                AddParameter("@COOKER", dto.Cooker <=0 ? (object)DBNull.Value : (int)dto.Cooker);
                AddParameter("@DESCONTO", dto.DescontoLinha);
                AddParameter("@PRIORIDADE", dto.Prioridade);
                AddParameter("@IMPOSTO", dto.ValorImposto);
                AddParameter("@ISPAID", dto.IsPaid ? 1 : 0);
                AddParameter("@TAX_ID", dto.TaxID);
                AddParameter("@TAX_VALUE", dto.TaxValue);
                AddParameter("@DISCOUNT_VALUE", dto.DiscountValue);
                AddParameter("@WAREHOUSE_ID", dto.WareHouseName);

                if (dto.NroOrdenacao <= 0)
                {
                    dto.NroOrdenacao = -1;
                }
                ExecuteNonQuery();

                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            }

            return dto;
        }

        public List<AtendimentoItemDTO> ObterItens(AtendimentoDTO dto)
        {

            List<AtendimentoItemDTO> lista = new List<AtendimentoItemDTO>();
            AtendimentoItemDTO item;
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_ITEM_OBTERFILTRO";

                 
                AddParameter("ATENDIMENTO", dto.Codigo);

                MySqlDataReader dr = ExecuteReader();

                while (dr.Read())
                {
                    item = new AtendimentoItemDTO
                    {
                        Atendimento = int.Parse(dr[0].ToString()),
                        Artigo = int.Parse(dr[1].ToString()),
                        Designacao = dr[2].ToString(),
                        Quantidade = !string.IsNullOrEmpty(dr[3].ToString()) ? decimal.Parse(dr[3].ToString()) : 0,
                        Preco = !string.IsNullOrEmpty(dr[4].ToString()) ? decimal.Parse(dr[4].ToString()) : 0,
                        Total = (decimal.Parse(dr[4].ToString()) * decimal.Parse(dr[3].ToString())),
                        Solicitante = dr[6].ToString(),
                        NroOrdenacao = int.Parse(dr[7].ToString()),
                        OrderID = int.Parse(dr[8].ToString()),
                        Saved = true,
                        Deleted = int.Parse(dr[9].ToString()) == 1 ? false : true,
                        UpdatedBy = dr[10].ToString(),
                        UpdatedDate = DateTime.Parse(dr[11].ToString() != null && dr[10].ToString() != "" ? dr[11].ToString() : DateTime.MinValue.ToShortDateString()),
                        Notes = dr[12].ToString(),
                        DeleteNotes = dr[13].ToString(),
                        Situacao = dr[14].ToString(),
                        Cooker = int.Parse(dr[15].ToString()),
                        DescontoLinha = decimal.Parse(dr[16].ToString()),
                        Prioridade = dr[17].ToString(),
                        ValorImposto = decimal.Parse(dr[18].ToString()),
                        SocialName = dr[19].ToString(),
                        IsPaid = dr[20].ToString()!="1" ? false :true,
                        TaxID = int.Parse(dr[21].ToString() == "" ? "-1" : dr[21].ToString()),
                        TaxValue = decimal.Parse(dr[22].ToString() == "" ? "0" : dr[22].ToString()),
                        DiscountValue = decimal.Parse(dr[23].ToString() == "" ? "0" : dr[23].ToString()),
                        WareHouseName = dr[24].ToString() == "" ? "-1" : dr[24].ToString()



                    };
                    
                    lista.Add(item);
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




        public void ExcluirItens(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_ITEM_EXCLUIR";

                AddParameter("@ATENDIMENTO", dto.Codigo); 

                ExecuteNonQuery();

                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            } 
        }

        public void Excluir(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_EXCLUIR";

                AddParameter("@CODIGO", dto.Codigo);
                AddParameter("@UTILIZADOR", dto.Utilizador);
                AddParameter("@MOTIVO_ANULACAO", dto.Observacao);

                ExecuteNonQuery();

                dto.Sucesso = true;

            }
            catch (Exception ex)
            {
                dto.MensagemErro = ex.Message.Replace("'", string.Empty);
            }
            finally
            {
                FecharConexao();
            } 
        }

        public AtendimentoDTO GravarPedido(AtendimentoDTO dto)
        {
            try
            {
                ComandText = "stp_REST_ATENDIMENTO_PEDIDO_ADICIONAR";

                // CODIGO, ATENDENTE, CLIENTE, INICIO, TERMINO, FILIAL, DESTINO, OUT LSTID
                AddParameter("@ATENDIMENTO_ID", dto.Codigo);
                AddParameter("@REQUESTER_ID", dto.Funcionario);
                AddParameter("@MESA_ID", string.IsNullOrEmpty(dto.Mesa) || dto.Mesa =="-1" ? (object)DBNull.Value : dto.Mesa); 
                AddParameter("@FILIAL", dto.Filial); 
                AddParameter("@UTILIZADOR", dto.Utilizador);  
                dto.PedidoID = ExecuteInsert();
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
    }
}
