using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class PerfilFilialRN
    {
        private static PerfilFilialRN _instancia;

        private PerfilFilialDAO dao;

        public PerfilFilialRN()
        {
          dao = new PerfilFilialDAO();
        }

        public static PerfilFilialRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PerfilFilialRN();
            }

            return _instancia;
        }

        public void Adicionar(PerfilEmpresaDTO dto) 
        {   
            dao.AdicionarPerfil(dto);
        }

        public void Excluir(PerfilEmpresaDTO dto) 
        {
            dao.ExcluirPerfil(dto);
        }

        public List<PerfilEmpresaDTO> ListaPerfis(PerfilEmpresaDTO dto) 
        {
            return dao.ObterPerfisFilial(dto);
        }


    }
}
