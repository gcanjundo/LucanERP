using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class TamanhoRN
    {
        private static TamanhoRN _instancia;

        private TamanhoDAO dao;

        public TamanhoRN()
        {
          dao = new TamanhoDAO();
        }

        public static TamanhoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new TamanhoRN();
            }

            return _instancia;
        }

        public TamanhoDTO Salvar(TamanhoDTO dto) 
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

        public TamanhoDTO Excluir(TamanhoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<TamanhoDTO> ObterPorFiltro(TamanhoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<TamanhoDTO> ListaTamanhos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new TamanhoDTO(0,""));
        }

        public TamanhoDTO ObterPorPK(TamanhoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
