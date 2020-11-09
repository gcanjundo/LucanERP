using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class PermissaoModuloRN
    {
        private static PermissaoModuloRN _instancia;

        private PermissaoModuloDAO daoPermissaoModulo;
        private UtilizadorPermissaoModuloDAO daoPermissaoUtilizador;

        public PermissaoModuloRN()
        {
          daoPermissaoModulo = new PermissaoModuloDAO();
          daoPermissaoUtilizador = new UtilizadorPermissaoModuloDAO();
        }

        public static PermissaoModuloRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PermissaoModuloRN();
            }

            return _instancia;
        }

        public List<PermissaoModuloDTO> ObterPermissoesModulos(PermissaoModuloDTO dto)
        {
            return daoPermissaoModulo.ObterPermissoesModulo(dto);
        }


        public void Inserir(PermissaoModuloDTO dto) 
        {
            daoPermissaoModulo.Inserir(dto);
        }

        public void InserirPermissoesModulos(PermissaoModuloDTO dto)
        {
            daoPermissaoModulo.Excluir(dto);
            daoPermissaoModulo.Inserir(dto);

        }

        public PermissaoModuloDTO ObterPermissaoModuloPorPK(PermissaoModuloDTO dto)
        {
            return daoPermissaoModulo.ObterPermissaoModuloPorPK(dto);
        }

        public Boolean TemAcessoAoModulo(PermissaoModuloDTO dto)
        {
            Boolean confirmacao = false;

            dto = ObterPermissaoModuloPorPK(dto);

            if (dto.Codigo > 0 && dto.Acesso == 1)
            {
                confirmacao = true;
            }

            return confirmacao;
        }

        // Permissoes do Utilizador

        public List<PermissaoModuloDTO> ObterModulosDoUtilizador(UtilizadorDTO dto)
        {

            return daoPermissaoUtilizador.ObterPermissoesModulo(dto);
        }

        // Obter os Módulo do Perfil e Pelo Utilizador(HOME e MENU HORIZONTAL)
        public List<PermissaoModuloDTO> ListaModulosNoMenu(UtilizadorDTO dto)
        {
            dto = UtilizadorRN.GetInstance().ObterPorPK(dto);
            return daoPermissaoUtilizador.ObterModulosDoMenu(dto);
        }

        
    }
}
