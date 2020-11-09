using DataAccessLayer.Geral;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Geral
{
    public class TaskRN
    {
        private static TaskRN _instancia;

        private TaskDAO dao;

        public TaskRN()
        {
            dao = new TaskDAO();
        }

        public static TaskRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new TaskRN();
            }

            return _instancia;
        }

        public TaskDTO Salvar(TaskDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public TaskDTO Excluir(TaskDTO dto)
        {
            return dao.Delete(dto);
        }

        public TaskDTO ObterPorPK(TaskDTO dto)
        {
            return dao.ObterPorFiltro(dto).Where(t=>t.Codigo == dto.Codigo).SingleOrDefault();
        }

        public List<TaskDTO> ObterPorFiltro(TaskDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }
    }
}
