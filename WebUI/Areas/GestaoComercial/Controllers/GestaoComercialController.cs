using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;
using WebUI.Extensions;

namespace WebUI.Areas.GestaoComercial.Controllers
{
    [Area("GestaoComercial")]
    public class GestaoComercialController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        private AcessoDTO acesso;
        private HomeController home;
        public GestaoComercialController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
            home = new HomeController(null, kitandaConfig);
        }
        public IActionResult Index()
        {
            GetSessionDetails();
            return View();
        }
        void GetSessionDetails()
        {
            acesso = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["SessionDetails"] = acesso;
        }
        public IActionResult Impostos()
        {
            return View();
        }

        public IActionResult TabelasConsulta()
        {
            GetSessionDetails();
            if (acesso == null)
                return home.SessaoExpirada();

            return View();
        }
    }
}
