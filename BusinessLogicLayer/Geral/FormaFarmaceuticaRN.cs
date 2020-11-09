using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class FormaFarmaceuticaRN
    {
        private static FormaFarmaceuticaRN _instancia;

        private FormaFarmaceuticaDAO dao;

        public FormaFarmaceuticaRN()
        {
          dao = new FormaFarmaceuticaDAO();
        }

        public static FormaFarmaceuticaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new FormaFarmaceuticaRN();
            }

            return _instancia;
        }

        public FormaFarmaceuticaDTO Salvar(FormaFarmaceuticaDTO dto) 
        {
            if (dto.Codigo > 0)
            {
                return dao.Alterar(dto);
            }
            else 
            {
                return dao.Adicionar(dto);
            }
        }

        public FormaFarmaceuticaDTO Excluir(FormaFarmaceuticaDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<FormaFarmaceuticaDTO> ObterPorFiltro(FormaFarmaceuticaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<FormaFarmaceuticaDTO> ListaFormaFarmaceuticas(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new FormaFarmaceuticaDTO(0,""));
        }

        public FormaFarmaceuticaDTO ObterPorPK(FormaFarmaceuticaDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
