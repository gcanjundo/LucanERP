using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Comercial;
using Dominio.Comercial;

namespace BusinessLogicLayer.Comercial
{
    public class SerieRN
    {
        private static SerieRN _instancia;

        private SerieDAO dao;

        public SerieRN()
        {
          dao = new SerieDAO();
        }

        public static SerieRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new SerieRN();
            }

            return _instancia;
        }

        public SerieDTO Salvar(SerieDTO dto) 
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

        public bool Excluir(SerieDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<SerieDTO> ObterPorFiltro(SerieDTO dto)
        {
            return dao.ObterPorFiltro(dto).ToList();
        }
         

        public SerieDTO ObterPorPK(SerieDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
