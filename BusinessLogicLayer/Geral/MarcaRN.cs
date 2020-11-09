using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Geral
{
    public class MarcaRN
    {
        private static MarcaRN _instancia;

        private MarcaDAO dao;

        public MarcaRN()
        {
          dao = new MarcaDAO();
        }

        public static MarcaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MarcaRN();
            }

            return _instancia;
        }

        public MarcaDTO Salvar(MarcaDTO dto) 
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

        public bool Excluir(MarcaDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<MarcaDTO> ObterPorFiltro(MarcaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<MarcaDTO> ListaMarca(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new MarcaDTO(0, descricao));
        }

        public MarcaDTO ObterPorPK(MarcaDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

        public List<MarcaDTO> ListaModelos(MarcaDTO dto)
        {
            return dao.ObterModelos(dto);
        }
    }
}
