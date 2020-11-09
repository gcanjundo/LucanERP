using System.Collections.Generic;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{
    public class RetencaoFonteRN
    {
        private static RetencaoFonteRN _instancia;

        private RetencaoFonteDAO dao;

        public RetencaoFonteRN()
        {
          dao = new RetencaoFonteDAO();
        }

        public static RetencaoFonteRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new RetencaoFonteRN();
            }

            return _instancia;
        }

        public RetencaoFonteDTO Salvar(RetencaoFonteDTO dto)
        {
            return dao.Adicionar(dto); 
        }

        

        public void Apagar(RetencaoFonteDTO dto)
        {
            dao.Eliminar(dto);
        }

        public RetencaoFonteDTO ObterPorPK(RetencaoFonteDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<RetencaoFonteDTO> ObterPorFiltro(RetencaoFonteDTO dto)
        { 
            return dao.ObterPorFiltro(dto);
        }

        public List<RetencaoFonteDTO> GetForDropDownList()
        {
            var lista = ObterPorFiltro(new RetencaoFonteDTO { Descricao = "" });
            lista.Insert(0, new RetencaoFonteDTO { Codigo = -1, Descricao = "-SELECCIONE-" });
            return lista;
        }
    }
}
