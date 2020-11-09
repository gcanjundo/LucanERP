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
        private readonly string LocalHost = "127.0.0.0"; 

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
                EmpresaDTO entidade = EmpresaRN.GetInstance().ObterEmpresaSistema();
                acesso.FuncionarioID = acesso.Codigo.ToString();
                int SerieFaturacao = GetPeriodoFaturacao(entidade.Codigo);
                 
                if (string.IsNullOrEmpty(entidade.MensagemErro))
                {
                    if (entidade.Codigo == 0)
                    {
                        if (isMasterAdmin(acesso.Utilizador))
                        {
                            acesso.Url = "CreateNewCompany();";
                        }
                        else
                        {
                            acesso.MensagemErro = "Ops!! a sua conta de Utilizador não tem autorização para  acessar o Sistema";
                        }
                    }
                    else if (SerieFaturacao <= 0)
                    {
                        if ((isMasterAdmin(acesso.Utilizador)))
                        {
                            acesso.MensagemErro = "CreateNewSerialDocumentation()";
                        }
                        else
                        {
                            acesso.MensagemErro = "O Sistema não tem uma Série de Facturação configurada. Deve Contactar a Equipa da LucanSoft para configuração do mesmo"; 
                        }
                    }
                    else
                    {
                        bool UserAllowed = false;
                        if (!acesso.IsRestUser)
                        {
                            if (string.IsNullOrEmpty(acesso.Filial))
                            {
                                UserAllowed = UtilizadorRN.GetInstance().isAccessAllowed(acesso.Utilizador, acesso.CurrentPassword);
                            }
                            else
                            {
                                UserAllowed = true;
                                var userDetails = UtilizadorRN.GetInstance().ObterPorPK(new UtilizadorDTO { Utilizador = acesso.Utilizador });
                                acesso.CurrentPassword = userDetails.CurrentPassword;
                            }
                        }
                        else
                        {
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
                                    UserAllowed = true;
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

                        if (UserAllowed)
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
                            }*/

                            if (!LicenseRN.GetInstance().ExistLicFile(acesso.Url))
                            {
                                if (isMasterAdmin(acesso.Utilizador) /*&& dto.IP == LocalHost*/)
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
                            }
                        }
                        else
                        {
                            if(acesso.MensagemErro==string.Empty)
                                acesso.MensagemErro = "Ops!! O nome de Utilizador ou a senha estão incorrectos. Volte a Tentar";
                        }

                    }
                }
                else
                {
                    acesso.MensagemErro = "Ocorreu um erro ao carregar as Configurações do Sistema: " + entidade.MensagemErro;
                }

                string pFrom = acesso.IsRestUser ? "LockScreen" : string.Empty;
                if (acesso.MensagemErro == string.Empty)
                    IniciarSessao(acesso, SerieFaturacao, acesso.CurrentSystem, pFrom, acesso.Filial);
                 

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

                if (SystemLogged == "REST")
                { 
                    acesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioREST(new UtilizadorDTO(acesso.Utilizador)); 
                }
                else
                {
                    SerieDTO PosSerieDefault = acesso.UserPOS != null ? SerieRN.GetInstance().ObterPorPK(new SerieDTO { Codigo = acesso.UserPOS.DocumentSerieID })
                        : null;
                    SystemConfigurations.DesignationDefaultSeriePOS = PosSerieDefault != null ? PosSerieDefault.Descricao : string.Empty;
                    if (SystemLogged == "POS")
                    {
                        acesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioPOS(new UtilizadorDTO(acesso.Utilizador));
                    }
                    else
                    if (SystemLogged == "COM")
                    {
                        acesso.UserAccess = PermissaoFormularioRN.GetInstance().GetUserAccess(acesso.Utilizador);

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

        private AcessoDTO IniciarSessao(AcessoDTO acesso, int pSerieFaturacao, string pSys, string pFrom, string BranchID)
        {
            try
            {

                UtilizadorDTO dto = UtilizadorRN.GetInstance().ObterPorPK(new UtilizadorDTO(acesso.Utilizador));

                acesso.UseID = dto.Codigo;
                acesso.Utilizador = dto.Utilizador;
                acesso.SocialName = dto.SocialName;
                acesso.UserProfile = dto.Perfil.Codigo;
                acesso.Supervisor = dto.Supervisor;
               
                EmpresaDTO objFilial;
                List<EmpresaDTO> filiais;
                if (isMasterAdmin(dto.Utilizador))
                {
                    filiais = EmpresaRN.GetInstance().ObterTodas();
                    ParametrizarAdmin();
                    foreach (var unidade in filiais)
                    {
                        EmpresaRN.GetInstance().IncluirUtilizador(new UtilizadorDTO(dto.Utilizador, unidade.Codigo));
                    }
                    objFilial = !string.IsNullOrEmpty(BranchID) ? filiais.Where(t => t.Codigo == int.Parse(BranchID)).ToList().FirstOrDefault() : filiais[0];

                }
                else
                {
                    filiais = EmpresaRN.GetInstance().ObterMinhasFiliais(dto.Utilizador);
                    if (filiais.Count > 1)
                        objFilial = !string.IsNullOrEmpty(BranchID) ? filiais.Where(t => t.Codigo == int.Parse(BranchID)).ToList().FirstOrDefault() : filiais.Where(f => f.IsDefault).ToList().FirstOrDefault();
                    else
                        objFilial = filiais[0];
                     
                }

                if (objFilial == null)
                {
                    if (isMasterAdmin(dto.Utilizador))
                    {
                        acesso.Url = "RegistarSucursal();";
                    }
                    else
                    {
                        acesso.MensagemErro = "alert('Lamentamos, mas a sua conta de Utilizador não tem permissão para aceder as Unidades Filiais do Sistema. Por favor contacte o Administrador do Sistema.');";
                    }

                }
                else
                {
                    if (filiais.Count > 1 && string.IsNullOrEmpty(BranchID))
                    {
                        acesso.Url = "window.location.href='../BranchSelectionSection';";
                        acesso.Sucesso = true;
                    }else
                    {
                        acesso = LoadSelectedBranchSettings(acesso, pSerieFaturacao, pSys, pFrom, objFilial);
                         
                    }
                        
                }

            }
            catch (Exception ex)
            {
                acesso.MensagemErro = "alert('Ocorreu um Erro durante no inicio de Sessão: " + ex.Message.Replace("'", "")+ "');";
            }


            return acesso;
        }

        public AcessoDTO LoadSelectedBranchSettings(AcessoDTO acesso, int pSerieFaturacao, string pSys, string pFrom, EmpresaDTO objFilial)
        {

            try
            {
                ConfiguracaoDTO SystemConfigurations = ConfiguracaoRN.GetInstance().GetSystemConfiguration(objFilial);
                if (SystemConfigurations.Sucesso)
                {
                    SystemConfigurations.SerieFaturacao = pSerieFaturacao;
                    acesso.Filial = SystemConfigurations.Filial;
                }
                else
                {
                    //SystemConfigurations = new ConfiguracaoDTO();
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
                    .Where(t => t.Codigo == SystemConfigurations.PosDefaultWarehouse).FirstOrDefault();
                    acesso.WareHouseName = acesso.UserDefaultWarehouse.Descricao;
                    acesso.UserPOS = PosRN.GetInstance().ObtePostosVendas(new PosDTO(acesso.Utilizador, acesso.Filial)).Where(t => t.Descricao == acesso.Utilizador).SingleOrDefault();
                    //acesso.DataLogin = DateTime.Today;.ToLocalTime(); DateTime.Parse(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
                    acesso.StatusSessao = "A";
                    acesso.CurrentSystem = pSys;
                    acesso.Filial = objFilial.Codigo.ToString();

                   var saveLogin = InserirAcesso(acesso);

                    acesso.Sucesso = !acesso.Sucesso ? saveLogin.Sucesso : acesso.Sucesso;

                    if (acesso.Sucesso)
                    {
                        acesso.Url = "window.location.href='../Default';";


                        if (pSys == "REST")
                        {
                            //acesso.Url = "window.location.href='../Menu';";
                            acesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioREST(new UtilizadorDTO(acesso.Utilizador));
                            if (acesso.UserProfile == AcessoDTO.CashRegisterProfile)
                            {
                                acesso.Url = "window.location.href='/RestPOS'";
                                SystemConfigurations.IsCashRegister = true;

                            }
                            else if (acesso.FuncionarioID != "" && acesso.FuncionarioID != "-1" && acesso.FuncionarioID != "0" || pFrom == "LockScreen")
                            {
                                acesso.Url = "window.location.href='/AtendimentoSala?pE=" + acesso.FuncionarioID + "'";
                            }

                        }
                        else
                        {
                            SerieDTO PosSerieDefault = acesso.UserPOS != null ?
                                SerieRN.GetInstance().ObterPorPK(new SerieDTO { Codigo = acesso.UserPOS.DocumentSerieID })
                                : null;
                            SystemConfigurations.DesignationDefaultSeriePOS = PosSerieDefault != null ? PosSerieDefault.Descricao : string.Empty;
                            if (pSys == "POS")
                            {
                                acesso.UserAccess = PermissaoFormularioRN.GetInstance().ObterPermissoesFormularioPOS(new UtilizadorDTO(acesso.Utilizador));
                            }
                            else
                            if (pSys == "COM")
                            {
                                acesso.UserAccess = PermissaoFormularioRN.GetInstance().GetUserAccess(acesso.Utilizador);

                            }

                            if (acesso.UserProfile == AcessoDTO.CashRegisterProfile)
                            {
                                if(SystemConfigurations.BranchDetails.Categoria=="2")
                                  acesso.Url = "window.location.href='../Lavandaria/Home';";
                                else
                                    acesso.Url = "window.location.href='../Comercial/POS';";
                                SystemConfigurations.IsCashRegister = true;
                            }

                        }
                        acesso.CurrentSystem = pSys;

                        acesso.Settings = SystemConfigurations;
                    }
                    else
                    {

                        acesso.MensagemErro = "alert('Ocorreu um erro ao Gravar a Sessão: " + saveLogin.MensagemErro + "');";
                    }
                }
                else
                {
                    if (SystemConfigurations.MensagemErro != "")
                        acesso.MensagemErro = "alert('Ocorreu um erro ao carregar as configurações: " + SystemConfigurations.MensagemErro + "');";
                    else
                        acesso.MensagemErro = "alert('A Empresa não tem está configurada'); window.location.href = '../Seguranca/Login';";
                }
            }
            catch(Exception ex)
            {
                acesso.MensagemErro = "alert('Ocorreu um Erro durante o inicio de Sessão: "+ex.Message.Replace("'", "")+" Acesso: "+ acesso.StatusSessao +acesso.Url+"');";
            }

            return acesso;
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
