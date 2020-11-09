using DataAccessLayer.Clinica;
using Dominio.Clinica;
using System.Collections.Generic;

namespace BusinessLogicLayer.Clinica
{
    public class LaboratorioExameFaixaEtariaRN
    {
        private static LaboratorioExameFaixaEtariaRN _instancia;

        private LaboratorioExameFaixaEtariaDAO dao;

        public LaboratorioExameFaixaEtariaRN()
        {
            dao = new LaboratorioExameFaixaEtariaDAO();
        }

        public static LaboratorioExameFaixaEtariaRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LaboratorioExameFaixaEtariaRN();
            }

            return _instancia;
        }

        public LaboratorioExameFaixaEtariaDTO Salvar(LaboratorioExameFaixaEtariaDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public LaboratorioExameFaixaEtariaDTO Excluir(LaboratorioExameFaixaEtariaDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<LaboratorioExameFaixaEtariaDTO> ObterPorFiltro(LaboratorioExameFaixaEtariaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public LaboratorioExameFaixaEtariaDTO ObterPorPK(LaboratorioExameFaixaEtariaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<LaboratorioExameFaixaEtariaDTO> GetFaixasEtariaForDropDownList()
        {
            var lista = dao.ObterPorFiltro(new LaboratorioExameFaixaEtariaDTO { Descricao = string.Empty }); 
           
            if(lista.Count == 0)
            {
                foreach(var item in new GenericRN().GetGeneroList())
                {
                    item.Codigo = -1;
                    Salvar(item);
                }

                lista = new GenericRN().GetGeneroList();
            }
            lista.Insert(0, new LaboratorioExameFaixaEtariaDTO { Codigo = -1, Descricao = "-SELECCIONE-", Sigla = "-1", LookupField1 = "-SELECCIONE-" });

            return lista;

        }
    }
}
