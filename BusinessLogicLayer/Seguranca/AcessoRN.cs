using BusinessLogicLayer.Comercial;
using BusinessLogicLayer.Geral;
using DataAccessLayer.Seguranca;
using Dominio.Comercial;
using Dominio.Geral;
using Dominio.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Seguranca
{
    public class AcessoRN
    {
        private static AcessoRN _instancia;
        private AcessoDAO daoAcesso;
        private readonly string KITANDAGC = "COM", KITANDAPOS= "POS", KITANDAREST="REST", LOCKSCREEN="LockScreen", SIKOLA="SIKOLA";

        public AcessoRN() 
        {
            daoAcesso = new AcessoDAO(); 
        }

        public static AcessoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new AcessoRN();
            }

            return _instancia;
        }

        private int GetPeriodoFaturacao(int pFilial)
        {
            AnoFaturacaoDTO dto = PeriodoFaturacaoRN.GetInstance().GetForDropDowList(pFilial).Find(t => t.Ano == DateTime.Today.Year && t.Termino >= DateTime.Today && t.Filial == pFilial.ToString());

            if (dto == null)
            {
                return -1;
            }
            else
            {
                return dto.Ano;
            }
        }

         
        public AcessoDTO Entrar(AcessoDTO acesso)
        { 
            try
            {
                //Obter a Empresa Princial do Sistema
                EmpresaDTO entidade = EmpresaRN.GetInstance().ObterEmpresaSistema();

                acesso.FuncionarioID = acesso.Codigo.ToString(); 

                // Obter a Série de Facturação
                int SerieFaturacao = GetPeriodoFaturacao(entidade.Codigo);
                 
                if (string.IsNullOrEmpty(entidade.MensagemErro))
                {
                    if (entidade.Codigo == 0)
                    {
                        if (isMasterAdmin(acesso.Utilizador))
                        {
                            acesso.Url = "CreateBranch";
                        }
                        else
                        {
                            acesso.MensagemErro = "Ops!! a sua conta de Utilizador não tem autorização para  acessar o Sistema";
                        }
                    }
                    else if (SerieFaturacao <= 0)
                    {
                        acesso.MensagemErro = "O Sistema não tem uma Série de Facturação configurada. Deve Contactar a Equipa da LucanSoft para configuração do mesmo";
                    }
                    else
                    {
                        bool userTemAcessoAoSistema = false;

                        if (!acesso.IsRestUser)
                        {
                            if (string.IsNullOrEmpty(acesso.Filial)) // A partir da Página de Login
                            {
                                userTemAcessoAoSistema = UtilizadorRN.GetInstance().isAccessAllowed(acesso.Utilizador, acesso.CurrentPassword);
                            }
                            else
                            {
                                userTemAcessoAoSistema = true;
                                /*
                                var userDetails = UtilizadorRN.GetInstance().ObterPorPK(new UtilizadorDTO { Utilizador = acesso.Utilizador });
                                acesso.CurrentPassword = userDetails.CurrentPassword;*/
                            }
                        }
                        else
                        {
                            // Em caso de Utilizador Vindo Módulo de Restauração(KitandaRest)
                            var PostOfSales = PosRN.GetInstance().GetPostOfSalesDetails(new PosDTO
                            {
                                Codigo = acesso.Codigo,
                                Filial = "-1"
                            });

                            if(PostOfSales!=null && PostOfSales.Estado == 1)
                            {
                                if(PostOfSales.PinCode == "fc0iUkg331qk3V8HY6MWvQ==" || PostOfSales.PinCode ==string.Empty)
                                  acesso.MensagemErro = "ShowModal('"+acesso.Utilizador+"', '"+acesso.Codigo+"');";
                                else if(PostOfSales.PinCode == acesso.CurrentPassword)
                                {
                                    userTemAcessoAoSistema = true;
                                    acesso.CurrentPassword =  PostOfSales.CurrentPassword;
                                }
                                else
                                {
                                    acesso.MensagemErro = "PIN Incorrecto, digite novamente";
                                }

                            }
                            else
                            {
                                acesso.MensagemErro = "A Conta de Utilizador ou Posto de Venda desactivado";
                            }
                        }

                        if (userTemAcessoAoSistema)
                        {
                            acesso.Codigo = entidade.Codigo;
                            GenericRN clsGeneric = new GenericRN();
                            /*
                            Tuple<bool, string> serverCredencials = clsGeneric.CheckServer();

                            IPHostEntry hostEntry = Dns.GetHostEntry(serverCredencials.Item2);

                            if (!serverCredencials.Item1 && serverCredencials.Item2!=dto.IP)
                            {
                                hostEntry = Dns.GetHostEntry(serverCredencials.Item2);
                                string hostName = hostEntry.HostName;
                            }

                            if (!LicenseRN.GetInstance().ExistLicFile(acesso.Url))
                            {
                                if (isMasterAdmin(acesso.Utilizador))
                                {
                                    var licenca = LicenseRN.GetInstance().GenerateLicense(new LicencaDTO
                                    {
                                        Filial = entidade.NomeCompleto,
                                        HostName = acesso.ServerName,
                                        HostMacAddress = acesso.IP,
                                        LicType = "F"
                                    }, acesso.Url);

                                    acesso.MensagemErro = licenca.MensagemErro!="" ? licenca.MensagemErro : string.Empty;
                                }
                                else
                                {
                                    acesso.MensagemErro = "O Sistema não tem Licença Válida";
                                }
                            }
                            else
                            {
                                var IsValidLicense = LicenseRN.GetInstance().GetSystemValidLicense(new LicencaDTO
                                {
                                    HostName = acesso.ServerName
                                });

                                if (!IsValidLicense.Item1)
                                {
                                    acesso.MensagemErro = IsValidLicense.Item2; 
                                }
                            }*/
                        }
                        else
                        {
                            if(acesso.MensagemErro==string.Empty)
                                acesso.MensagemErro = "Ops!! O nome de Utilizador ou a senha estão incorrectos. Volte a Tentar";
                        }


                        if (userTemAcessoAoSistema && acesso.MensagemErro == string.Empty)
                        {
                            string pFrom = acesso.IsRestUser ? LOCKSCREEN : string.Empty; 
                            IniciarSessao(acesso, SerieFaturacao, acesso.CurrentSystem, pFrom, acesso.Filial);
                        }

                    }
                }
                else
                {
                    acesso.MensagemErro = "Ocorreu um erro ao durante a obtenção dos dados da empresa licenciada: " + entidade.MensagemErro;
                }

                
                 

            }catch(Exception ex)
            {
                acesso.MensagemErro = "Erro durante a entrada no Sistema: " + ex.Message.Replace("'", "");
            }

            return acesso;
        }

        public AcessoDTO TryReloadSession(AcessoDTO dto)
        {
            dto = daoAcesso.GetLastSession(dto);

            if (dto.Codigo > 0)
            {
                TimeSpan SessionTime = DateTime.Now - dto.HoraLogin; 
                
                if(SessionTime.TotalHours <= 24)
                {
                    var userDetails = UtilizadorRN.GetInstance().ObterPorPK(new UtilizadorDTO(dto.Utilizador));
                    dto.CurrentPassword = userDetails.CurrentPassword;
                    dto.SocialName = userDetails.SocialName;
                    dto.UserProfile = userDetails.Perfil.Codigo;
                    dto.Supervisor = userDetails.Supervisor;
                    dto.IsCashRegister = userDetails.IsCashRegister;
                    return ReloadSessionSettings(dto, dto.HoraLogin.Year, dto.CurrentSystem, new EmpresaDTO { Codigo = int.Parse(dto.Filial) });
                }  
            }
                
            return null;
        }



        AcessoDTO ReloadSessionSettings(AcessoDTO acesso, int pSerieFaturacao, string SystemLogged, EmpresaDTO objFilial)
        {
            ConfiguracaoDTO SystemConfigurations = ConfiguracaoRN.GetInstance().GetSystemConfiguration(objFilial);
            if (SystemConfigurations.Sucesso)
            {
                SystemConfigurations.SerieFaturacao = pSerieFaturacao;
                acesso.Filial = SystemConfigurations.Filial;
            }
            else
            {     
                SystemConfigurations.BranchDetails = objFilial;
                SystemConfigurations.Filial = objFilial.Codigo.ToString();

            }

            if (SystemConfigurations.MensagemErro == string.Empty && SystemConfigurations.Sucesso)
            {

                if (AcessoRN.GetInstance().SessaoIniciada(acesso) && !string.IsNullOrEmpty(acesso.Maquina))
                {

                    TerminarSessao(acesso.Utilizador);
                }
                 
                acesso.Filial = objFilial.Codigo.ToString();

                acesso.DefaultLanguage = "pt-PT";
                acesso.Language = string.Empty;
                acesso.UserDefaultWarehouse = ArmazemRN.GetInstance().ObterPorFiltro(new ArmazemDTO { Filial = acesso.Filial, Descricao = string.Empty })
                .Where(t => t.Codigo == SystemConfigurations.PosDefaultWarehouse).SingleOrDefault();
                     
                acesso.WareHouseName = acesso.UserDefaultWarehouse.Descricao;
                acesso.UserPOS = PosRN.GetInstance().ObtePostosVendas(new PosDTO(acesso.Utilizador, acesso.Filial)).Where(t => t.Descricao == acesso.Utilizador).SingleOrDefault();

                if (SystemLogged == KITANDAREST)
                { 
                    acesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioREST(new UtilizadorDTO(acesso.Utilizador)); 
                }
                else
                {
                    SerieDTO PosSerieDefault = acesso.UserPOS != null ? SerieRN.GetInstance().ObterPorPK(new SerieDTO { Codigo = acesso.UserPOS.DocumentSerieID })
                        : null;
                    SystemConfigurations.DesignationDefaultSeriePOS = PosSerieDefault != null ? PosSerieDefault.Descricao : string.Empty;
                    if (SystemLogged == KITANDAPOS)
                    {
                        acesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioPOS(new UtilizadorDTO(acesso.Utilizador));
                    }
                    else if (SystemLogged == KITANDAGC)
                    {
                        acesso.UserAccess = PermissaoFormularioRN.GetInstance().GetUserAccess(acesso.Utilizador);

                    }else if(SystemLogged == SIKOLA) 
                    { 
                    
                    }

                    if (acesso.UserProfile == AcessoDTO.CashRegisterProfile)
                    { 
                        SystemConfigurations.IsCashRegister = true;
                    }

                }

                acesso.Settings = SystemConfigurations;
            }

            return acesso;
        }
        private void ParametrizarAdmin()
        {
            UtilizadorRN.GetInstance().ParametrizarAdmin();
        }

        public bool isMasterAdmin(string pUtilizador)
        {
            return pUtilizador.Equals(AcessoDTO.AdminMaster) ? true : false;
        }

        public bool HasGerenteProfile(string pUtilizador, int pProfile)
        {
            return pProfile == AcessoDTO.isGerente || isMasterAdmin(pUtilizador) ? true: false;
        }

        private AcessoDTO IniciarSessao(AcessoDTO pAcesso, int pSerieFaturacao, string pSys, string pFrom, string pEmpresaSeleccionada)
        {
            try
            {

                UtilizadorDTO user = UtilizadorRN.GetInstance().ObterPorPK(new UtilizadorDTO(pAcesso.Utilizador));

                pAcesso.UserID = user.Codigo;
                pAcesso.Utilizador = user.Utilizador;
                pAcesso.SocialName = user.SocialName;
                pAcesso.UserProfile = user.Perfil.Codigo;
                pAcesso.Supervisor = user.Supervisor;
                pAcesso.CurrentPassword = user.CurrentPassword;

                EmpresaDTO empresa; 
                List<EmpresaDTO> filiais;

                if (isMasterAdmin(user.Utilizador))
                {
                    filiais = EmpresaRN.GetInstance().ObterTodas();
                    ParametrizarAdmin();
                    foreach (var unidade in filiais)
                    {
                        EmpresaRN.GetInstance().IncluirUtilizador(new UtilizadorDTO(user.Utilizador, unidade.Codigo));
                    } 
                }
                else
                {
                    filiais = EmpresaRN.GetInstance().ObterMinhasFiliais(user.Utilizador);  
                }

                empresa = !string.IsNullOrEmpty(pEmpresaSeleccionada) ? filiais.Where(t => t.Codigo == int.Parse(pEmpresaSeleccionada)).ToList().FirstOrDefault() : filiais.FirstOrDefault();

                if (empresa == null)
                {
                    if (isMasterAdmin(user.Utilizador))
                    {
                        pAcesso.Url = "RegistarSucursal";
                    }
                    else
                    {
                        pAcesso.MensagemErro = "Lamentamos, mas a sua conta de Utilizador não tem permissão para aceder as Unidades Filiais do Sistema. Por favor contacte o Administrador do Sistema.";
                    }

                }
                else
                {
                    if (filiais.Count > 1 && string.IsNullOrEmpty(pEmpresaSeleccionada))
                    {
                        pAcesso.Url = "BranchSelection";
                        pAcesso.Sucesso = true;
                        pAcesso.CompanyName = "Seleccione a Empresa";
                        pAcesso.CompanyLogo = "../template/app-assets/upload/favicon.png";
                    }
                    else
                    {
                        pAcesso = LoadSelectedBranchSettings(pAcesso, pSerieFaturacao, pSys, pFrom, empresa);
                         
                    }
                        
                }

            }
            catch (Exception ex)
            {
                pAcesso.MensagemErro = "alert('Ocorreu um Erro durante no inicio de Sessão: " + ex.Message.Replace("'", "")+ "');";
            }


            return pAcesso;
        }

        public AcessoDTO LoadSelectedBranchSettings(AcessoDTO pAcesso, int pSerieFaturacao, string pSys, string pFrom, EmpresaDTO pEmpresa)
        {

            try
            {
                ConfiguracaoDTO SystemConfigurations = ConfiguracaoRN.GetInstance().GetSystemConfiguration(pEmpresa);

                if (SystemConfigurations.Sucesso)
                {
                    SystemConfigurations.SerieFaturacao = pSerieFaturacao;
                    pAcesso.Filial = SystemConfigurations.Filial;
                    pAcesso.CompanyName = SystemConfigurations.BranchDetails.NomeComercial;
                    pAcesso.CompanyLogo = SystemConfigurations.BranchDetails.CompanyLogo;
                }
                else
                {
                    //SystemConfigurations = new ConfiguracaoDTO();
                    SystemConfigurations.BranchDetails = pEmpresa;
                    SystemConfigurations.Filial = pEmpresa.Codigo.ToString();

                }

                if (SystemConfigurations.MensagemErro == string.Empty && SystemConfigurations.Sucesso)
                {

                    if (AcessoRN.GetInstance().SessaoIniciada(pAcesso) && !string.IsNullOrEmpty(pAcesso.Maquina))
                    {

                        TerminarSessao(pAcesso.Utilizador);
                    }

                     
                    pAcesso.Filial = pEmpresa.Codigo.ToString(); 
                    pAcesso.DefaultLanguage = "pt-PT";
                    pAcesso.Language = string.Empty;
                    pAcesso.UserDefaultWarehouse = ArmazemRN.GetInstance().ObterPorFiltro(new ArmazemDTO { Filial = pAcesso.Filial, Descricao = string.Empty })
                    .Where(t => t.Codigo == SystemConfigurations.PosDefaultWarehouse).FirstOrDefault(); 
                    pAcesso.WareHouseName = pAcesso.UserDefaultWarehouse.Descricao;

                    pAcesso.UserPOS = PosRN.GetInstance().ObtePostosVendas(new PosDTO(pAcesso.Utilizador, pAcesso.Filial)).Where(t => t.Descricao == pAcesso.Utilizador).SingleOrDefault();
                    pAcesso.StatusSessao = "A";
                    pAcesso.CurrentSystem = pSys;
                    pAcesso.Filial = pEmpresa.Codigo.ToString();

                   var saveLogin = InserirAcesso(pAcesso);

                    pAcesso.Sucesso = !pAcesso.Sucesso ? saveLogin.Sucesso : pAcesso.Sucesso;

                    if (pAcesso.Sucesso)
                    {
                        pAcesso.Url = "Index";


                        if (pSys == "REST")
                        { 
                            pAcesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioREST(new UtilizadorDTO(pAcesso.Utilizador));

                            if (pAcesso.UserProfile == AcessoDTO.CashRegisterProfile)
                            {
                                pAcesso.Url = "window.location.href='/RestPOS'";
                                SystemConfigurations.IsCashRegister = true;

                            }
                            else if (pAcesso.FuncionarioID != "" && pAcesso.FuncionarioID != "-1" && pAcesso.FuncionarioID != "0" || pFrom == LOCKSCREEN)
                            {
                                pAcesso.Url = "window.location.href='/AtendimentoSala?pE=" + pAcesso.FuncionarioID + "'";
                            }

                        }
                        else
                        {
                            SerieDTO PosSerieDefault = pAcesso.UserPOS != null ?
                                SerieRN.GetInstance().ObterPorPK(new SerieDTO { Codigo = pAcesso.UserPOS.DocumentSerieID })
                                : null;
                            SystemConfigurations.DesignationDefaultSeriePOS = PosSerieDefault != null ? PosSerieDefault.Descricao : string.Empty;
                            if (pSys == "POS")
                            {
                                pAcesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioPOS(new UtilizadorDTO(pAcesso.Utilizador));
                            }
                            else if (pSys == "COM")
                            {
                                pAcesso.UserAccess = PermissaoFormularioRN.GetInstance().GetUserAccess(pAcesso.Utilizador);

                            }else if(pSys == "SIKOLA")
                            {

                            }

                            if (pAcesso.UserProfile == AcessoDTO.CashRegisterProfile)
                            {
                                if(SystemConfigurations.BranchDetails.Categoria=="2")
                                  pAcesso.Url = "window.location.href='../Lavandaria/Home';";
                                else
                                    pAcesso.Url = "window.location.href='../Comercial/POS';";
                                SystemConfigurations.IsCashRegister = true;
                            }

                        }
                        pAcesso.CurrentSystem = pSys;

                        pAcesso.Settings = SystemConfigurations;
                    }
                    else
                    {

                        pAcesso.MensagemErro = "alert('Ocorreu um erro ao Gravar a Sessão: " + saveLogin.MensagemErro + "');";
                    }
                }
                else
                {
                    if (SystemConfigurations.MensagemErro != "")
                        pAcesso.MensagemErro = "alert('Ocorreu um erro ao carregar as configurações: " + SystemConfigurations.MensagemErro + "');";
                    else
                        pAcesso.MensagemErro = "alert('A Empresa não tem está configurada'); window.location.href = '../Seguranca/Login';";
                }
            }
            catch(Exception ex)
            {
                pAcesso.MensagemErro = "alert('Ocorreu um Erro durante o inicio de Sessão: "+ex.Message.Replace("'", "")+" Acesso: "+ pAcesso.StatusSessao +pAcesso.Url+"');";
            }

            return pAcesso;
        }

        public void TerminarSessao(string pUtilizador)
        { 
            AcessoDTO dto = new AcessoDTO();
            dto.Utilizador = pUtilizador;
            //dto.DataLogout = DateTime.Today; DateTime.Parse(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
            dto.StatusSessao = "I";
            AlterarAcesso(dto);
        }

        public AcessoDTO  InserirAcesso(AcessoDTO dto)
        { 
             return !string.IsNullOrEmpty(dto.Maquina) ? daoAcesso.Inserir(dto) : new AcessoDTO();
        }

        public void AlterarAcesso(AcessoDTO dto)
        {
            daoAcesso.Alterar(dto);
        }

        public Boolean SessaoIniciada(AcessoDTO dto)
        {
            return daoAcesso.SessaoIniciada(dto);
        }

        public AcessoDTO ObterAcessoPorPK(AcessoDTO dto)
        {
            return daoAcesso.ObterPorPK(dto);
        }

        public List<AcessoDTO> ObterAcessosPorFiltro(AcessoDTO dto)
        {
            return daoAcesso.ObterPorFiltro(dto);
        }

        public List<AcessoDTO> ObterAcessosPorStatus(AcessoDTO dto)
        {
            return daoAcesso.ObterPorStatus(dto);
        }  
         
    }
}
