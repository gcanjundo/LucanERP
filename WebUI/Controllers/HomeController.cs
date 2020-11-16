using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using BusinessLogicLayer.Geral;
using BusinessLogicLayer.Seguranca;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KitandaConfig _kitandaConfig; 
        public HomeController(ILogger<HomeController> logger, KitandaConfig kitandaConfig)
        {
            _logger = logger;
            _kitandaConfig = kitandaConfig;
        }

        public IActionResult Login()
        {
            HttpContext.Session.Set<AcessoDTO>("userCredencials", null);
            return View();
        }

        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        public IActionResult BranchSelection()
        {
            GetSessionDetails();
            if (_kitandaConfig.pSessionInfo==null)
                return Login();

            var myBranchList = EmpresaRN.GetInstance().ObterMinhasFiliais(_kitandaConfig.pSessionInfo.Utilizador);
            return View(myBranchList);

        }

        public IActionResult Index()
        { 
            GetSessionDetails(); 
            if (_kitandaConfig.pSessionInfo == null)
                return LogOut();

            var agenda = TaskRN.GetInstance().ObterPorFiltro(new TaskDTO { Utilizador = _kitandaConfig.pSessionInfo.Utilizador, Filial = _kitandaConfig.pSessionInfo.Filial });
            return View(agenda);
        } 
         
        

 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SessaoExpirada()
        {
            return View();
        }

        public ActionResult Entrar(AcessoDTO userCredentials)
        {
            userCredentials.Maquina = Environment.MachineName;
            userCredentials.IP = ObterEnderecoIP();
            userCredentials.ServerName = Environment.MachineName;
            userCredentials.CurrentPassword = _kitandaConfig.Encrypt(userCredentials.CurrentPassword);
            userCredentials.Url = _kitandaConfig.FilePath;
            userCredentials = AcessoRN.GetInstance().Entrar(userCredentials);

            if (userCredentials.Sucesso)
            {
                HttpContext.Session.Set<AcessoDTO>("userCredencials", userCredentials);
            }
            
            return Json(userCredentials);
        }

        protected string ObterEnderecoIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
            return ipHostInfo.AddressList.Where(t => t.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
        }

        public IActionResult LogOut()
        { 
            
            return Login();
        }

    }
}
