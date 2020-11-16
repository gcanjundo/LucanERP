using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.GestaoOficina.Controllers
{
    [Area("GestaoClinica")]
    public class HomeOficinaController : Controller
    {
        public IActionResult DashboardOficina()
        {
            return View();
        }
    }
}
