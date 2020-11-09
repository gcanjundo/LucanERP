using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class ModuloRN
    {
        private static ModuloRN _instancia;
        private ModuloDAO daoModulo;

        public ModuloRN()
        {
            daoModulo = new ModuloDAO();
        }

        public static ModuloRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ModuloRN();
            }

            return _instancia;
        }

        public List<ModuloDTO> ObterTodosModulos()
        {
            return daoModulo.ObterTodos();
        }

        public ModuloDTO ObterModuloPorPK(ModuloDTO dto)
        {
            return daoModulo.ObterPorPK(dto);
        }

        public ModuloDTO ObterModuloPorNome(ModuloDTO dto)
        {
            return daoModulo.ObterPorNome(dto);
        }

        public List<ModuloDTO> GetByLicense(LicencaDTO dto)
        {
            var modulesList = ObterTodosModulos();

            if(dto.LicType == "GF")
            {
                modulesList.Where(t => t.Codigo == 1 || t.Codigo == 8).ToList();
            }else if (dto.LicType == "GP")
            {
                modulesList.Where(t => t.Codigo == 1 || t.Codigo == 2 || t.Codigo == 12 || t.Codigo == 8).ToList();
            }
            else if (dto.LicType == "GC")
            {
                modulesList.Where(t => t.Codigo == 1 || t.Codigo == 2 || t.Codigo == 12 || t.Codigo == 8).ToList();
            }
            else if (dto.LicType == "GL")
            {
                modulesList.Where(t => t.Codigo == 1 || t.Codigo == 2 || t.Codigo == 12 || t.Codigo == 8).ToList();
            }
            else
            {
                modulesList = new List<ModuloDTO>();
            }

            return modulesList;
        }
    }

 
}
