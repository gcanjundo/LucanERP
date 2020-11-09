using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class CategoriaRN 
    {
        private static CategoriaRN _instancia;

        private CategoriaDAO dao;

        public CategoriaRN()
        {
          dao = new CategoriaDAO();
        }

        public static CategoriaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new CategoriaRN();
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

        public CategoriaDTO ObterPorPK(CategoriaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<CategoriaDTO> ObterComProdutos(bool onlyRest)
        {
            return dao.ObterComProdutos(onlyRest);
        }

        public CategoriaDTO GetByDescription(CategoriaDTO dto)
        {
            List<CategoriaDTO> CategoryList = ObterPorFiltro(dto);
            if (CategoryList.Count == 0)
            {
               return Salvar(new CategoriaDTO
                {
                    Descricao = dto.Descricao,
                    Sigla = string.Empty,
                    Estado =1,
                    Categoria = dto.Categoria,
                    Utilizador = "administrador",
                    Filial = "2"
                });
            }
            else
            {
                return CategoryList.FirstOrDefault(t => t.Descricao == dto.Descricao);
            }


        }

        public List<CategoriaDTO> GetAllMedicalProfissionalCategoryList()
        {
            return dao.ObterCategoriaClinicoProfissional();
        }
    }
}
