using BusinessLogicLayer.Geral;
using DataAccessLayer.RecursosHumanos;
using Dominio.RecursosHumanos;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class FuncionarioRN
    {
        private static FuncionarioRN _instancia;

        private FuncionarioDAO dao;

        public FuncionarioRN()
        {
            dao = new FuncionarioDAO();
        }

        public static FuncionarioRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new FuncionarioRN();
            }

            return _instancia;
        }

        public string Salvar(FuncionarioDTO dto, List<EntidadeDocumentacaoDTO> documentos, List<FuncionarioAgregadoDTO> dependentes)
        {
            
            dto = dao.Adicionar(dto);
            if (dto.MensagemErro.Equals(string.Empty))
            {
                dto.MensagemErro = "alert('A Ficha de " + dto.NomeCompleto + ", foi Guardada com Sucesso'); window.location.href='GestaoColabodores.aspx'";
                EntidadeDocumentacaoRN.GetInstance().ExcluirPorEntidade(dto.Codigo);
                foreach (var documento in documentos)
                {
                    documento.Entidade = dto.Codigo;
                    EntidadeDocumentacaoRN.GetInstance().Salvar(documento);
                }
            }
            else
            {
                dto.MensagemErro = "alert('Ops!! Ocorreu um erro ao guardar os dados: "+dto.MensagemErro+"')";
            }

            return dto.MensagemErro;
        }

        public FuncionarioDTO Excluir(FuncionarioDTO dto)
        {
            return dao.Excluir(dto);
        }


        public List<FuncionarioDTO> ListaFuncionarios(FuncionarioDTO dto)
        {
            
            return dao.ObterPorFiltro(dto);
        }

        public FuncionarioDTO ObterPorPK(FuncionarioDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<FuncionarioDTO> ListaFuncionarios(string pFilial, string pNome, string pInicio, string pTermino, string pFiltro, string pIdentificacao, string pSituacao, string pOrdem)
        {
            FuncionarioDTO dto = new FuncionarioDTO();
            if (string.IsNullOrEmpty(pFilial))
            {
                dto.Filial = "-1";
            }
            else
            {
                dto.Filial = pFilial;
            }

            if (string.IsNullOrEmpty(pNome))
            {
                dto.NomeCompleto = "";
            }
            else
            {
                dto.NomeCompleto = pNome;
            }

            if (string.IsNullOrEmpty(pInicio))
            {
                dto.DataInicio = DateTime.MinValue;
            }
            else
            {
                dto.DataInicio = Convert.ToDateTime(pInicio);
            }

            if (string.IsNullOrEmpty(pTermino))
            {
                dto.DataTermino = DateTime.MinValue;
            }
            else
            {
                dto.DataTermino = Convert.ToDateTime(pTermino);
            }

            if (string.IsNullOrEmpty(pSituacao))
            {
                dto.Situacao = "-1";
            }
            else
            {
                dto.Situacao = pSituacao;
            }

            

            return ListaFuncionarios(dto);
        }
    }
}
