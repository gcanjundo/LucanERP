using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;
using Dominio.Comercial;
using BusinessLogicLayer.Comercial;
using BusinessLogicLayer.Geral;
using Dominio.Geral;

namespace BusinessLogicLayer.Seguranca
{
    public class UtilizadorRN
    {
        private static UtilizadorRN _instancia;
        private UtilizadorDAO daoUtilizador;

        public UtilizadorRN()
        {
            daoUtilizador = new UtilizadorDAO();
        }

        public static UtilizadorRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new UtilizadorRN();
            }

            return _instancia;
        }

        public void ParametrizarAdmin()
        {

            PerfilDTO dtoPerfil = new PerfilDTO();
            dtoPerfil.SocialName = "Administradores do Sistema";
            dtoPerfil.Utilizador = "administrador";

            List<PerfilDTO> perfis = PerfilRN.GetInstance().ObterPorFiltro(dtoPerfil);

            if (perfis.Count == 0)
            {
                // Cria o Perfil Administrador Caso nao Exista;
                dtoPerfil.Descricao = "Grupo com Acesso Total no Sistema";
                dtoPerfil.Email = "";
                dtoPerfil.Situacao = "A"; //(A) - Activo ; (I) - Inactivo ; (E) - Excluido
                dtoPerfil = PerfilRN.GetInstance().InserirPerfil(dtoPerfil);
                UtilizadorDTO dto = new UtilizadorDTO();

                dto.Email = "";
                dto.SocialName = "Administrador do Sistema";
                dto.CurrentPassword = "67nTuZNKN3Ig4fWIdYu+4w==";
                dto.Utilizador = "administrador";
                dto.Situacao = "A"; // (A)- Activo; (I) - Inactivo; (B) - Bloqueiado
                dto.Perfil = dtoPerfil;
                dto = UtilizadorRN.GetInstance().Adicionar(dto);

            }
            else
            {
                dtoPerfil = perfis[0];
            }

            PerfilRN.GetInstance().DefineAcessoPerfilAdministradoresSistema();
        }

        public UtilizadorDTO Adicionar(UtilizadorDTO dto)
        {
            var entity = EntidadeRN.GetInstance().Salvar(dto);

            dto.Codigo = entity.Codigo;
            var addedUser = daoUtilizador.Inserir(dto);
            if (addedUser.Perfil.Codigo == 2 || dto.Supervisor == 1)
            {
                CreatePOS(addedUser);
            }
            return addedUser;
        }

        public UtilizadorDTO AlterarSenha(UtilizadorDTO dto)
        {
            return daoUtilizador.AlterSenha(dto);
        }


        public bool isAccessAllowed(string pUtilizador, string pSenha)
        {
            UtilizadorDTO dto = new UtilizadorDTO();
            dto.Utilizador = pUtilizador;
            dto.CurrentPassword = pSenha;
            return daoUtilizador.ConfirmarLogin(dto).Sucesso; 
        }

        public List<UtilizadorDTO> ObterUtilizadoresPorFiltro(UtilizadorDTO dto)
        {
            return daoUtilizador.ObterPorFiltro(dto);
        }

        public UtilizadorDTO ObterPorPK(UtilizadorDTO dto)
        {
            return daoUtilizador.ObterPorPK(dto);
        }

        public UtilizadorDTO ObterPorID(UtilizadorDTO dto)
        {
            return daoUtilizador.ObterPorID(dto);
        }

        public List<UtilizadorDTO> ObterUtilizadoresPorStatus(UtilizadorDTO dto)
        {
            return daoUtilizador.ObterPorSituacao(dto);
        }

        public void ResetarSenha(UtilizadorDTO dto)
        {
            AlterarSenha(dto);
        }

         

        public bool isSuperivor(UtilizadorDTO dto)
        {
            return (daoUtilizador.Superivisor(dto) == 1 || AcessoRN.GetInstance().isMasterAdmin(dto.Utilizador)) ? true : false;
        }

        private void CreatePOS(UtilizadorDTO dto)
        {
            PosDTO oPOS = new PosDTO
            {
                Descricao = dto.Utilizador,
                Estado = 1,
                Sigla = "",
                PinCode = dto.CurrentPassword
            };
            PosRN.GetInstance().CreateNewPOS(oPOS);
        }

        public List<UtilizadorDTO> UsersDropDownList(UtilizadorDTO dto)
        {
            dto.Email = string.Empty; dto.SocialName = string.Empty;
            var UserList = ObterUtilizadoresPorFiltro(dto);
            UserList.Insert(0, new UtilizadorDTO("-1", "-SELECCIONE-"));

            return UserList; 
        }
    }
}
