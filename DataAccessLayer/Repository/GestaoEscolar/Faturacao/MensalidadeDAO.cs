using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dominio.GestaoEscolar.Pedagogia;
using Dominio.GestaoEscolar.Faturacao;
using MySql.Data.MySqlClient;


namespace DataAccessLayer.GestaoEscolar.Faturacao
{
    public class MensalidadeDAO  
    {
        readonly ConexaoDB BaseDados;

        public MensalidadeDAO()
        {
            BaseDados = new ConexaoDB();
        }

        public MensalidadeDTO Inserir(MensalidadeDTO dto)
        {    
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ADICIONAR";
                 

                BaseDados.AddParameter("@SERVICO", dto.Codigo);
                BaseDados.AddParameter("@DIA_LIMITE", dto.Dia);
                BaseDados.AddParameter("@INICIO_COBRANCA", dto.Inicio);
                BaseDados.AddParameter("@TERMINO_COBRANCA", dto.Termino);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@MODO_COBRANCA", dto.ModalidadeCobranca);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@PRIORITY_PAYMENT", dto.PaymentPriority);

                BaseDados.ExecuteNonQuery();
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

        public Boolean AssociarMulta(MultaItemCobrancaDTO dto)
        {
            
            
            try
            {

                BaseDados.ComandText = "stp_FIN_MULTA_PLANO_MENSALIDADE_ADICIONAR";

                BaseDados.AddParameter("@MULTA", dto.PlanoMultaMensalidade.MulCodigo);
                BaseDados.AddParameter("@PLANO", dto.PlanoMensalidade.Codigo); 
                
                BaseDados.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
                return false;

            }
            finally
            {
                BaseDados.FecharConexao();
            }
            
        }

        public Boolean AssociarDesconto(DescontoMensalidadeDTO dto)
        {
             

            try
            {

                BaseDados.ComandText = "stp_FIN_DESCONTO_PLANO_MENSALIDADE_ADICIONAR";

                BaseDados.AddParameter("@DESCONTO", dto.PlanoDesconto.DesCodigo);
                BaseDados.AddParameter("@PLANO", dto.PlanoMensalidade.Codigo);   
                BaseDados.ExecuteNonQuery();
                
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

        public MensalidadeDTO Alterar(MensalidadeDTO dto)
        {
              
            try
            {

                BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALTERAR";




                BaseDados.AddParameter("@SERVICO", dto.Codigo);
                BaseDados.AddParameter("@DIA_LIMITE", dto.Dia);
                BaseDados.AddParameter("@INICIO_COBRANCA", dto.Inicio);
                BaseDados.AddParameter("@TERMINO_COBRANCA", dto.Termino);
                BaseDados.AddParameter("@TIPO", dto.Tipo);
                BaseDados.AddParameter("@MODO_COBRANCA", dto.ModalidadeCobranca);
                BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);
                BaseDados.AddParameter("@PRIORITY_PAYMENT", dto.PaymentPriority);
                
                BaseDados.ExecuteNonQuery();
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

        
        public MensalidadeDTO ObterPorPK(MensalidadeDTO dto)
        {
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_OBTERPORPK"; 
            
            BaseDados.AddParameter("@CODIGO", dto.Codigo);

            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MensalidadeDTO();
                while (dr.Read())
                {
                    
                    ItemCobrancaDTO item = new ItemCobrancaDTO();
                    item.ItemCodigo =  Int32.Parse(dr["MENS_CODIGO_ITEM"].ToString());
                    item = new ItemCobrancaDAO().ObterPorPK(item);
                    dto.Codigo = item.ItemCodigo;
                    dto.Descricao = item.ItemDescricao.ToUpper();
                    dto.Dia = Int32.Parse(dr["MENS_DIA_LIMITE"].ToString());
                    dto.Inicio = dr["MENS_INICIO_COBRANCA"].ToString();
                    dto.Termino = dr["MENS_TERMINO_COBRANCA"].ToString();                                        
                    dto.Tipo = dr["MENS_TIPO"].ToString();
                    dto.Preco = item.ItemValor;
                    dto.ModalidadeCobranca = dr["MENS_PAGAMENTO"].ToString();
                    dto.AnoLectivo = int.Parse(dr["MENS_ANO_LECTIVO"].ToString());
                    
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

         

        public List<MultaItemCobrancaDTO> ObterMultasDoPlano(MensalidadeDTO dto)
        {

            BaseDados.ComandText = "stp_FIN_MULTA_PLANO_MENSALIDADE_OBTERPORFILTRO";

            
            BaseDados.AddParameter("@PLANO", dto.Codigo);
            BaseDados.AddParameter("@MULTA", -1);
            BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);

            List<MultaItemCobrancaDTO> multas = new List<MultaItemCobrancaDTO>();
            

            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                

                while (dr.Read())
                {
                    MultaItemCobrancaDTO objPlano = new MultaItemCobrancaDTO();
                    dto = new MensalidadeDTO(); 
                    dto.Codigo = Int32.Parse(dr["MUL_CODIGO_MULTA"].ToString()); 
                   

                    MultaDTO objMulta = new MultaDTO();
                    objMulta.MulCodigo = int.Parse(dr["MUL_CODIGO"].ToString());
                    objMulta.MulDescricao = dr["MUL_DESCRICAO"].ToString();
                    objMulta.MulValor = decimal.Parse(dr["MUL_VALOR"].ToString());
                    objMulta.MulInicio = Convert.ToDateTime(dr["MUL_INICIO"].ToString());
                    //dto.MulTermino = Convert.ToDateTime(dr["MUL_TERMINO"].ToString());
                    objMulta.MulDe = int.Parse(dr["MUL_DE"].ToString());
                    objMulta.MulAte = int.Parse(dr["MUL_ATE"].ToString());
                    objMulta.MulPercentual = decimal.Parse(dr["MUL_PERCENTAGEM"].ToString());
                    objMulta.MulTipo = dr["MUL_TIPO"].ToString();
                    objMulta.Estado = int.Parse(dr["MUL_STATUS"].ToString());
                    objPlano.AnoLectivo = int.Parse(dr["MUL_CODIGO_ANO"].ToString());

                    objPlano.PlanoMultaMensalidade = objMulta;
                    objPlano.PlanoMensalidade = dto;
                    
                    multas.Add(objPlano);

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

            return multas;

        }

        public List<DescontoMensalidadeDTO> ObterDescontosDoPlano(MensalidadeDTO dto)
        {
            BaseDados.ComandText = "stp_FIN_DESCONTO_PLANO_MENSALIDADE_OBTERPORFILTRO"; 

            BaseDados.AddParameter("@PLANO", dto.Codigo);
            BaseDados.AddParameter("@DESCONTO", -1);

            List<DescontoMensalidadeDTO> descontos = new List<DescontoMensalidadeDTO>(); 

            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();



                while (dr.Read())
                {
                    DescontoMensalidadeDTO descontoPlano = new DescontoMensalidadeDTO();
                    dto = new MensalidadeDTO();
                    
                    dto.Codigo = Int32.Parse(dr[1]);
                    descontoPlano.PlanoMensalidade = dto;
                    descontoPlano.PlanoDesconto = new DescontoDTO(Int32.Parse(dr[0]));
                    descontos.Add(descontoPlano);

                }
            }
            catch (Exception ex)
            {
                DescontoMensalidadeDTO desconto = new DescontoMensalidadeDTO();
                desconto.Sucesso = false;
                desconto.MensagemErro = ex.Message.Replace("'", "");
                descontos = new List<DescontoMensalidadeDTO>();
                descontos.Add(desconto);
            }
            finally
            {
                BaseDados.FecharConexao();

                foreach(var descontoPlano in descontos)
                { 
                    descontoPlano.PlanoDesconto = new DescontoDAO().ObterPorPK(descontoPlano.PlanoDesconto);
                }
            }

            return descontos;

        }

        public List<MensalidadeDTO> ObterServicosMensais(MensalidadeDTO dto)
        {
            
            
            List<MensalidadeDTO> lista = new List<MensalidadeDTO>();
            try
            {
                BaseDados.ComandText = "stp_FIN_MENSALIDADE_OBTERPORSERVICOS";

                BaseDados.AddParameter("FILIAL", dto.Filial);
                BaseDados.AddParameter("ANO_LECTIVO", dto.AnoLectivo);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeDTO();

                    dto.Codigo = Int32.Parse(dr["MENS_CODIGO_ITEM"].ToString());

                    dto.Dia = Int32.Parse(dr["MENS_DIA_LIMITE"].ToString());
                    dto.Inicio = dr["MENS_INICIO_COBRANCA"].ToString();
                    dto.Termino = dr["MENS_TERMINO_COBRANCA"].ToString();

                    dto.Tipo = dr["MENS_TIPO"].ToString();
                    ItemCobrancaDTO item = new ItemCobrancaDTO(dto.Codigo, "", -1, "", "", -1);
                    item = new DAO.GestaoEscolar.Faturacao.ItemCobrancaDAO().ObterPorPK(item);
                    dto.Descricao = item.ItemDescricao.ToUpper();

                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto = new MensalidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");

            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return lista;
        }

        public List<MensalidadeDTO> ObterServicosAderidos(MatriculaDTO dtoInscricao)
        {
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_ALUNO_PLANOADERIDO";




            MensalidadeDTO dto;
            
            BaseDados.AddParameter("@MATRICULA", dtoInscricao.Codigo);
            List<MensalidadeDTO> lista = new List<MensalidadeDTO>();
            try
            {
                
                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                   dto = new MensalidadeDTO();

                    ItemCobrancaDTO item = new ItemCobrancaDTO();
                    item.ItemCodigo = Int32.Parse(dr["MENS_CODIGO_ITEM"].ToString());
                    item = new DAO.GestaoEscolar.Faturacao.ItemCobrancaDAO().ObterPorPK(item);
                    dto.Codigo = item.ItemCodigo;
                    dto.Descricao = item.ItemDescricao.ToUpper();
                    dto.Dia = Int32.Parse(dr["MENS_DIA_LIMITE"].ToString());
                    dto.Inicio = dr["MENS_INICIO_COBRANCA"].ToString();
                    dto.Termino = dr["MENS_TERMINO_COBRANCA"].ToString();

                    dto.Tipo = dr["MENS_TIPO"].ToString();
                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto = new MensalidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", "");
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return lista;
        }

        public void RemoverMulta(MultaItemCobrancaDTO dto)
        { 
            try
            {
                BaseDados.ComandText = "stp_FIN_MULTA_PLANO_MENSALIDADE_EXCLUIR";

                BaseDados.AddParameter("@MULTA", dto.MulMensMulta);
                BaseDados.AddParameter("@PLANO", dto.MulMensmensalidade);
                BaseDados.ExecuteNonQuery();
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
        }

        public List<ItemCobrancaDTO> ObterListaPropinas(MensalidadeDTO dto)
        {
            BaseDados.ComandText = "stp_FIN_MENSALIDADE_OBTERPLANO";


            List<ItemCobrancaDTO> lista = new List<ItemCobrancaDTO>();

            BaseDados.AddParameter("@PLANO", dto.Codigo);
            BaseDados.AddParameter("@ANO_LECTIVO", dto.AnoLectivo);

            try
            {

                MySqlDataReader dr = BaseDados.ExecuteReader();
                dto = new MensalidadeDTO();
               while (dr.Read())
                {
                    ItemCobrancaDTO item = new ItemCobrancaDTO();
                    item.ItemCodigo = Int32.Parse(dr["MENS_CODIGO_ITEM"].ToString());
                    item = new ItemCobrancaDAO().ObterPorPK(item);
                    dto.Codigo = item.ItemCodigo;
                    dto.Descricao = dr["ITEM_DESCRICAO"].ToString().ToUpper();
                    dto.Dia = Int32.Parse(dr["MENS_DIA_LIMITE"].ToString());
                    dto.Inicio = dr["MENS_INICIO_COBRANCA"].ToString();
                    dto.Termino = dr["MENS_TERMINO_COBRANCA"].ToString();
                    dto.Tipo = dr["MENS_TIPO"].ToString();
                    item.Rubrica = dto.Tipo;
                    item.ItemDescricao = dto.Descricao;
                    dto.AnoLectivo = int.Parse(dr["MENS_ANO_LECTIVO"].ToString()); 

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
                BaseDados.FecharConexao();
            }

            return lista;

        }

        public void GravaExtraSettings(MensalidadeDTO dto)
        {
            try
            {
                BaseDados.ComandText = "stp_SIS_CONFIGURACAO_EXTRA_ADICIONAR";


                BaseDados.AddParameter("@SERVICO", dto.Tipo);
                BaseDados.AddParameter("@FILIAL", dto.Filial);
                BaseDados.AddParameter("@COBRANCA", dto.InicioCobranca);
                BaseDados.AddParameter("@PRIMEIRO_MES", dto.MultaPrimeiroMes);
                BaseDados.AddParameter("@ULTIMO_MES", dto.MultaUltimoMes);
                BaseDados.AddParameter("@INSCRICAO", dto.MultaInscricao); 

                BaseDados.ExecuteNonQuery();
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
        }

        public List<MensalidadeDTO> GetExtraConfig(MensalidadeDTO dto)
        {
            List<MensalidadeDTO> lista = new List<MensalidadeDTO>();
            try
            {
                BaseDados.ComandText = "stp_SIS_CONFIGURACAO_EXTRA_OBTERPORFILTRO";

                BaseDados.AddParameter("FILIAL", dto.Filial);

                MySqlDataReader dr = BaseDados.ExecuteReader();

                while (dr.Read())
                {
                    dto = new MensalidadeDTO();

                    dto.Tipo = dr[0];
                    dto.Descricao = GetActivdadesExtra(dto.Tipo);
                    dto.InicioCobranca = dr[2];
                    dto.MultaPrimeiroMes = dr[3];
                    dto.MultaUltimoMes = dr[4];
                    dto.MultaInscricao = dr[5];

                    lista.Add(dto);

                }
            }
            catch (Exception ex)
            {
                dto = new MensalidadeDTO();
                dto.Sucesso = false;
                dto.MensagemErro = ex.Message.Replace("'", ""); 
            }
            finally
            {
                BaseDados.FecharConexao();
            }
            return lista;
        }

        private string GetActivdadesExtra(string pType)
        {
            

            string pActividade = "";
            switch (pType)
            {
                case "TP":
                    pActividade ="TRANSPORTE";
                    break;
                case "AE":
                    pActividade ="ACTIVIDADES EXTRA-CURRICULARES";
                    break;
                case "AT":
                    pActividade ="ATL - ACTIVIDADE DE TEMPOS LIVRES";
                    break;
                case "PL":
                    pActividade ="PROLONGAMENTO";
                    break;
                case "AL":
                    pActividade="REFEIÇÃO";
                    break;
            }

            return pActividade;
        }

         
    }
}
