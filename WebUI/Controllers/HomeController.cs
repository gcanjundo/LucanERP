using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using BusinessLogicLayer.Geral;
using BusinessLogicLayer.Seguranca;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;
using WebUI.Models.Geral;

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
            return View();
        } 
        
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult BranchSelection()
        {
            var myBranchList =EmpresaRN.GetInstance().ObterMinhasFiliais(Request.Query["pUs"]);
            return View(myBranchList);
        }

         

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                //TempData["myModel"] = userCredentials; 
            }
            
            return Json(userCredentials);
        }

        protected string ObterEnderecoIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
            return ipHostInfo.AddressList.Where(t => t.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
        }



    }
}
