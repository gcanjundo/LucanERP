using System;
using System.Collections.Generic;
using DataAccessLayer.Seguranca;
using Dominio.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class EmpresaRN
    {
        private static EmpresaRN _instancia;

        private EmpresaDAO dao;

        public EmpresaRN()
        {
            dao = new EmpresaDAO();
        }

        public static EmpresaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new EmpresaRN();
            }

            return _instancia;
        }

        public EmpresaDTO Salvar(EmpresaDTO dto)
        {
            if (dto.Codigo == 0)
            {
                dto = dao.Adicionar(dto);

            }
            else
            {
                dto = dao.Alterar(dto);
            }

            return dto;
        }
        public List<EmpresaDTO> ObterMinhasFiliais(string pUtilizador)
        {
            UtilizadorDTO dtoUtilizador = new UtilizadorDTO(pUtilizador);
            return dao.ObterAcessoFiliais(dtoUtilizador);
        }

        public List<EmpresaDTO> ObterTodas()
        {
            EmpresaDTO dto = new EmpresaDTO();
            return dao.ObterFiliais(dto);
        }

        public void IncluirUtilizador(UtilizadorDTO dto)
        {
            dao.Incluir(dto);
        }

        internal EmpresaDTO ObterEmpresaSistema()
        {
            return dao.ObterEmpresaSistema();
        }

        public void ExcluirTodas(UtilizadorDTO dto)
        {
            dao.Remover(dto);
        }
    }
}
