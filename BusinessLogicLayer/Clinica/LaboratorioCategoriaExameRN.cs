using DataAccessLayer.Clinica;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Clinica
{
    public class LaboratorioCategoriaExameRN
    {
        
            private static LaboratorioCategoriaExameRN _instancia;

            private LaboratorioCategoriaExameDAO dao;

            public LaboratorioCategoriaExameRN()
            {
                dao = new LaboratorioCategoriaExameDAO();
            }

            public static LaboratorioCategoriaExameRN GetInstance()
            {
                if (_instancia == null)
                {
                    _instancia = new LaboratorioCategoriaExameRN();
                }

                return _instancia;
            }

            public CategoriaDTO Salvar(CategoriaDTO dto)
            {
                return dao.Adicionar(dto);
            }

            public CategoriaDTO Excluir(CategoriaDTO dto)
            {
                return dao.Eliminar(dto);
            }

            public List<CategoriaDTO> ObterPorFiltro(CategoriaDTO dto)
            {
                return dao.ObterPorFiltro(dto);
            }

        public List<CategoriaDTO> GetCategoriaExameForDropDownList()
        {
            var lista = dao.ObterPorFiltro(new CategoriaDTO { Descricao = "", Filial="-1"});
            lista.Insert(0, new CategoriaDTO { Codigo = -1, Descricao = "-SELECCIONE-" });

            return lista;
        }

        public List<CategoriaDTO> GetSubCategoriaExameForDropDownList(CategoriaDTO dto)
        {
            var lista = dao.ObterPorFiltro(new CategoriaDTO { Descricao = "", Filial = "-1", Categoria = dto.Categoria});
            lista.Insert(0, new CategoriaDTO { Codigo = -1, Descricao = "-SELECCIONE-" });

            return lista;
        }




    }
}
