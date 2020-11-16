using Dominio.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Controllers;
using WebUI.Extensions;

namespace WebUI.Areas.RecusosHumanos.Controllers
{
    [Area("RecursosHumanos")]
    public class HomeRHController : Controller
    {

        private readonly KitandaConfig _kitandaConfig;
        private readonly HomeController _home;
        public HomeRHController(HomeController home, KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
            _home = home;
        }

        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        public IActionResult DashboardRH()
        { 
            return _kitandaConfig.pSessionInfo == null ? _home.LogOut() : View();
        } 
        
        public IActionResult TabelasConsultaRH()
        {
            return _kitandaConfig.pSessionInfo == null ? _home.LogOut() : View();
        }

         
    }
}
