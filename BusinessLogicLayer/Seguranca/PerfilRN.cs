using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class PerfilRN 
    {
        private static PerfilRN _instancia;

        private PerfilDAO dao;

        public PerfilRN()
        {
          dao = new PerfilDAO();
        }

        public static PerfilRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PerfilRN();
            }

            return _instancia;
        }

        public PerfilDTO Salvar(PerfilDTO dto)
        {
            PerfilDTO dtoPerfil = new PerfilDTO();
            if (dto.Codigo > 0)
            {
                dtoPerfil = dao.Alterar(dto);
            }
            else
            {
                if (ObterPorPK(dto).Codigo == 0)
                {
                    dtoPerfil = dao.Inserir(dto);
                }
            }

            return dtoPerfil;
             
        }

        public PerfilDTO InserirPerfil(PerfilDTO dto)
        {
            PerfilDTO dtoPerfil = dao.Inserir(dto);

            if (dtoPerfil.Sucesso.Equals(true))
            {
                this.ZeraAcessos(dto);
            }

            return dtoPerfil;
        }

        public void Excluir(PerfilDTO dto)
        {
            dao.Eliminar(dto);
        }

        public  List<PerfilDTO> ObterPorFiltro(PerfilDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<PerfilDTO> ObterTodos(PerfilDTO dto) 
        {
            return dao.ObterTodos(dto);
        }

        public PerfilDTO ObterPorPK(PerfilDTO dto)
        {
            PerfilDTO perfil = null;

            if (dto.Codigo > 0)
            {
                perfil = dao.ObterPorPK(dto);

            }
            else if (!string.IsNullOrEmpty(dto.Designacao))
            {
                perfil = dao.ObterPorNome(dto);
            }
            return perfil;
        }

        private void ZeraAcessos(PerfilDTO dto)
        {
            if (dto.Codigo > 0)
            {

                List<ModuloDTO> modulos = ModuloRN.GetInstance().ObterTodosModulos();
                ModuloDTO dtoModulo = null;
                for (int i = 0; i < modulos.Count; i++)
                {
                    PermissaoModuloDTO dtoPerMod = new PermissaoModuloDTO();

                    dtoModulo = new ModuloDTO();
                    dtoModulo.Codigo = modulos[i].Codigo;
                    dtoPerMod.Modulo = dtoModulo;

                    dto = new PerfilDTO();
                    dtoPerMod.Perfil = dto;

                    dtoPerMod.Acesso = 0;
                    dtoPerMod.Autorizar = 0;
                    dtoPerMod.Visibilidade = 0;
                    PermissaoModuloRN.GetInstance().Inserir(dtoPerMod);

                    // Instancia os Formulários
                    FormularioDTO dtoForm = new FormularioDTO();
                    dtoForm.Modulo = dtoModulo;

                    // Obtem os Formulário do Módulo
                   List<FormularioDTO> formularios = FormularioRN.GetInstance().ObterFormulariosPorModulo(dtoForm);

                    if (formularios.Count > 0)
                    {
                        for (int f = 0; f < formularios.Count; f++)
                        {
                            PermissaoFormularioDTO dtoPermForm = new PermissaoFormularioDTO();

                            dtoForm = new FormularioDTO();
                            dtoForm.Codigo = formularios[f].Codigo;

                            dtoPermForm.Formulario = dtoForm;
                            dtoPermForm.Perfil = dto;
                            dtoPermForm.AllowAccess = 0;
                            dtoPermForm.AllowUpdate = 0;
                            dtoPermForm.AllowSelect = 0;
                            dtoPermForm.AllowDelete = 0;
                            dtoPermForm.AllowPrint = 0;
                            dtoPermForm.AllowInsert = 0;
                            PermissaoFormularioRN.GetInstance().InserirPermissoesFormulario(dtoPermForm);
                        }
                    }
                }
            }

        }

        public void DefineAcessoPerfilAdministradoresSistema()
        {
            PermissaoFormularioRN.GetInstance().AddPermissoesSysAdmin();
        }

    }
}
