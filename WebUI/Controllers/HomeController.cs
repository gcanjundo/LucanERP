using System.Collections.Generic;
using System.Diagnostics;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;
using WebUI.Models.Geral;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        KitandaConfig _kitandaConfig;
        public HomeController(ILogger<HomeController> logger, KitandaConfig kitandaConfig)
        {
            _logger = logger;
            _kitandaConfig = kitandaConfig;
        }

        public IActionResult Index()
        {  

            return Login();
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

        public IActionResult Login()
        {
            return View();
        }
         
    }
}
