using DataAccessLayer.Clinica;
using Dominio.Clinica;
using System.Collections.Generic;

namespace BusinessLogicLayer.Clinica
{
    public class LaboratorioAmostraExameRN
    {
        private static LaboratorioAmostraExameRN _instancia;

        private LaboratorioAmostraExameDAO dao;

        public LaboratorioAmostraExameRN()
        {
            dao = new LaboratorioAmostraExameDAO();
        }

        public static LaboratorioAmostraExameRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LaboratorioAmostraExameRN();
            }

            return _instancia;
        }

        public LaboratorioAmostraExameDTO Salvar(LaboratorioAmostraExameDTO dto)
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

        public LaboratorioAmostraExameDTO Excluir(LaboratorioAmostraExameDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<LaboratorioAmostraExameDTO> ObterPorFiltro(LaboratorioAmostraExameDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public LaboratorioAmostraExameDTO ObterPorPK(LaboratorioAmostraExameDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<LaboratorioAmostraExameDTO> GetAmostrasForDropDownList()
        {
            var lista = dao.ObterPorFiltro(new LaboratorioAmostraExameDTO { Descricao = string.Empty }); 
            lista.Insert(0, new LaboratorioAmostraExameDTO { Codigo = -1, Descricao = "-SELECCIONE-" }); 
            
            return lista;

        }
    }
}
