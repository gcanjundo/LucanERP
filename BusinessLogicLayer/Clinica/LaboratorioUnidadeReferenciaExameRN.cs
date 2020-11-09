using DataAccessLayer.Clinica;
using Dominio.Geral;
using System.Collections.Generic;

namespace BusinessLogicLayer.Clinica
{
    public class LaboratorioUnidadeReferenciaExameRN
    {
        private static LaboratorioUnidadeReferenciaExameRN _instancia;

        private LaboratorioUnidadeReferenciaExameDAO dao;

        public LaboratorioUnidadeReferenciaExameRN()
        {
            dao = new LaboratorioUnidadeReferenciaExameDAO();
        }

        public static LaboratorioUnidadeReferenciaExameRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LaboratorioUnidadeReferenciaExameRN();
            }

            return _instancia;
        }

        public UnidadeDTO Salvar(UnidadeDTO dto)
        {
            return dao.Adicionar(dto);
        }

         

        public List<UnidadeDTO> ObterPorFiltro(UnidadeDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<UnidadeDTO> GetUnidadeExameForDropDownList()
        {
            var lista = dao.ObterPorFiltro(new UnidadeDTO { Descricao = "", Filial = "-1" });
            lista.Insert(0, new UnidadeDTO { Codigo = -1, Descricao = "-SELECCIONE-", Sigla= "-SELECCIONE-" });

            return lista;
        }
    }
}
