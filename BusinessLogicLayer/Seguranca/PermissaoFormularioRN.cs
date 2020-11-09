using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;

namespace BusinessLogicLayer.Seguranca
{
    public class PermissaoFormularioRN
    {
        private static PermissaoFormularioRN _instancia;

        private PermissaoFormularioDAO daoPermissaoFormulario;

        private UtilizadorPermissaoFormularioDAO daoPermissaoUtilizador;

        public PermissaoFormularioRN()
        {
          daoPermissaoFormulario = new PermissaoFormularioDAO();
          daoPermissaoUtilizador = new UtilizadorPermissaoFormularioDAO();
        }

        public static PermissaoFormularioRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PermissaoFormularioRN();
            }

            return _instancia;
        }

        public void InserirPermissoesFormulario(PermissaoFormularioDTO dtoPermPerfil)
        {
            daoPermissaoFormulario.Excluir(dtoPermPerfil);
            daoPermissaoFormulario.Inserir(dtoPermPerfil);
        }

        public List<PermissaoFormularioDTO> ObterPermissoesFormularios(PermissaoFormularioDTO dto)
        {
            return daoPermissaoFormulario.ObterPermissoesFormulario(dto);
        }

        public List<PermissaoFormularioDTO> ObterPermissoesFormularioPorModulo(PermissaoFormularioDTO dto)
        {
            return daoPermissaoFormulario.ObterPermissoesFormularioPorModulo(dto);
        }

        public PermissaoFormularioDTO ObterPermissaoPerfil(PermissaoFormularioDTO dto)
        {
            return daoPermissaoFormulario.ObterPorPK(dto);
        }

        public List<PermissaoFormularioDTO> GetUserAccess(string pUtilizador)
        {
            PermissaoFormularioDTO dto = new PermissaoFormularioDTO();
            dto.Utilizador = pUtilizador;
            return daoPermissaoUtilizador.ObterAcessosDoUtilizador(dto);
        }

        public void ExcluirTodosAllowAccesssDoUtilizadorAosFormularios(PermissaoFormularioDTO dto)
        {
            daoPermissaoUtilizador.ExcluirTodosAcessos(dto);
        }


        public List<PermissaoFormularioDTO>ObterFormulariosDoUtilizador(PermissaoFormularioDTO dto)
        {
            List<PermissaoFormularioDTO>permissoesGerais = daoPermissaoUtilizador.ObterPermissoesFormulario(dto);
            List<PermissaoFormularioDTO> permissoesModulo = new List<PermissaoFormularioDTO>();
            if (dto.Formulario.Modulo.Codigo > 0)
            {
                foreach (PermissaoFormularioDTO permissao in permissoesGerais)
                {
                    if (dto.Formulario.Modulo.Codigo == permissao.Formulario.Modulo.Codigo)
                    {
                        permissoesModulo.Add(permissao);
                    }
                }

                return permissoesModulo;
            }
            else
            {
                return permissoesGerais;
            }
        }

        public List<PermissaoFormularioDTO> ObterFormulariosDoUtilizadorNoModulo(PermissaoFormularioDTO dto)
        {
            return daoPermissaoUtilizador.ObterPermissoesFormularioPorModulo(dto);
        }

        public PermissaoFormularioDTO ObterAcessoDoUtilizadorAoFormularioPorPK(PermissaoFormularioDTO dto)
        {
            return daoPermissaoUtilizador.ObterPorPK(dto);
        }

        public Boolean UtilizadorTemAllowAccessAoFormulario(PermissaoFormularioDTO dto)
        {
            bool confirmacao = false;
            PermissaoFormularioDTO dtoResultante = new PermissaoFormularioDTO();
            dtoResultante = daoPermissaoUtilizador.ObterPorPK(dto);

            if (dtoResultante != null && dtoResultante.Utilizador == dto.Utilizador && dtoResultante.AllowAccess == 1)
            {
                confirmacao = true;
            }

            return confirmacao;
        }

         

        public Boolean TemAllowAccessAoFormulario(PermissaoFormularioDTO dto)
        {
            Boolean confirmacao = false;

            dto = daoPermissaoFormulario.ObterPorPK(dto);

            if (dto.Codigo > 0 && dto.AllowAccess == 1)
            {
                confirmacao = true;
            }

            return confirmacao;
        }

        public void AddUserAccess(PermissaoFormularioDTO dto) 
        {
            daoPermissaoUtilizador.Excluir(dto);
            daoPermissaoUtilizador.Inserir(dto);
        }

        public Boolean SetUserAccess(UtilizadorDTO objUser, List<PermissaoFormularioDTO> Documentos)
        {
            bool sucesso = false;
            int tag = 0;
            foreach (PermissaoFormularioDTO pemissao in Documentos)
            {

                PermissaoFormularioDTO dto = new PermissaoFormularioDTO();
                pemissao.Utilizador = objUser.Utilizador;
                FormularioDTO cabecalhoForm = FormularioRN.GetInstance().ObterPorSubModulo(pemissao.Formulario);
                if (pemissao.AllowAccess == 1)
                {
                    if (tag != pemissao.Formulario.TAG)
                    {
                        dto = new PermissaoFormularioDTO();
                        dto.AllowAccess = 1;
                        dto.Formulario = cabecalhoForm;
                        dto.Utilizador = pemissao.Utilizador;
                        ExcluirAcessoDoUtilizadorAoFormulario(dto);
                        sucesso = daoPermissaoUtilizador.Inserir(dto).Sucesso;
                        tag = pemissao.Formulario.TAG;


                    }

                    if (pemissao.Formulario.Codigo.Equals(48))
                    {
                        pemissao.AllowSelect = 1;
                    }
                }
                sucesso = true;



                if (sucesso)
                {

                    ExcluirAcessoDoUtilizadorAoFormulario(pemissao);
                    daoPermissaoUtilizador.Inserir(pemissao);
                }
                else
                {
                    sucesso = false;
                    break;
                }
            }
            EliminarSolitarios(objUser, null);
            return sucesso;
        }


        // Método que Dá AllowAccess aos Modulos e Formulários para um Perfil
        public void AtribuirAllowAccessPerfil(PerfilDTO perfil, List<PermissaoFormularioDTO> permissoes)
        {
            int tag = 0;
            foreach (PermissaoFormularioDTO permissao in permissoes)
            {
                if (permissao.AllowAccess == 1)
                {
                    if (tag != permissao.Formulario.TAG)
                    {
                        PermissaoFormularioDTO dto = new PermissaoFormularioDTO();
                        foreach (FormularioDTO subMenu in FormularioRN.GetInstance().ObterFormulariosSubModulo())
                        {
                            if (subMenu.TAG == permissao.Formulario.TAG)
                            {
                                dto.Formulario = subMenu;
                                dto.AllowAccess = 1;
                                dto.Perfil = perfil;
                                daoPermissaoFormulario.Excluir(dto);
                                daoPermissaoFormulario.Inserir(dto);
                                tag = permissao.Formulario.TAG;
                                break;
                            }
                        }
                    }
                }

                daoPermissaoFormulario.Excluir(permissao);
                daoPermissaoFormulario.Inserir(permissao);
            }
            EliminarSolitarios(null, perfil);
        }

        // Obtem os SubModulos do Sistema
        

        public void EliminarSolitarios(UtilizadorDTO objUtilizador, PerfilDTO objPerfil)
        {

            if (objUtilizador != null && objUtilizador.Utilizador != "")
            {
                foreach (FormularioDTO menu in FormularioRN.GetInstance().ObterFormulariosSubModulo())
                {
                    PermissaoFormularioDTO dto = new PermissaoFormularioDTO();
                    dto.Formulario = menu;
                    dto.Utilizador = objUtilizador.Utilizador;
                    daoPermissaoUtilizador.ExcluirDesemparelhados(dto);

                }
            }
            else if (objPerfil != null && objPerfil.Codigo > 0)
            {
                foreach (FormularioDTO menu in FormularioRN.GetInstance().ObterFormulariosSubModulo())
                {
                    PermissaoFormularioDTO dto = new PermissaoFormularioDTO();
                    dto.Formulario = menu;
                    dto.Perfil = objPerfil;
                    daoPermissaoFormulario.ExcluirSolitarios(dto);

                }
            }
        }

         

        public void ExcluirAcessoDoUtilizadorAoFormulario(PermissaoFormularioDTO dto)
        {
            daoPermissaoUtilizador.Excluir(dto);
        }

       
        public void AtribuirPermissoesPadrao(string utilizador)
        {
            UtilizadorDTO dtoUser = new UtilizadorDTO();
            dtoUser.Utilizador = utilizador;

            List<FormularioDTO> formularios = FormularioRN.GetInstance().ObterTodosFormularios();
            List<PermissaoFormularioDTO> permissoes = new List<PermissaoFormularioDTO>();
            foreach (FormularioDTO dtoForm in formularios)
            {
                PermissaoFormularioDTO dtoPermissao = new PermissaoFormularioDTO();
                dtoPermissao.AllowAccess = 0;
                dtoPermissao.AllowUpdate = 0;
                dtoPermissao.AllowSelect = 0;
                dtoPermissao.AllowDelete = 0;
                dtoPermissao.Formulario = dtoForm;
                dtoPermissao.AllowPrint = 0; 
                dtoPermissao.Utilizador = dtoUser.Utilizador;
                permissoes.Add(dtoPermissao);
                this.AddUserAccess(dtoPermissao);
            }

        }

        public List<PermissaoFormularioDTO> ObterFormularioPOS()
        {
           return daoPermissaoFormulario.ObterFormulariosPOS();
        }

        public List<PermissaoFormularioDTO> ObterPermissoesFormularioPOS(UtilizadorDTO dto)
        {
            return daoPermissaoFormulario.ObterPermissoesFormularioPOS(dto);
        }


        public List<PermissaoFormularioDTO> ObterFormularioREST()
        {
            return daoPermissaoFormulario.ObterFormulariosREST();
        }
        public List<PermissaoFormularioDTO> ObterPermissoesFormularioREST(UtilizadorDTO dto)
        {
            var permissoes = daoPermissaoFormulario.ObterPermissoesFormularioREST(dto);

             
            return permissoes;
        }


        public void AddPermissoesSysAdmin()
        {
            daoPermissaoFormulario.AllowSysAdmin();
        }

        public void SavePOSAccess(PermissaoFormularioDTO dto)
        {
            daoPermissaoFormulario.AddPermissaoPOS(dto);
        }

        public void SaveRESTAccess(PermissaoFormularioDTO dto)
        {
            daoPermissaoFormulario.AddPermissaoREST(dto);
        }
    }
}
