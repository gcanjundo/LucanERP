using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.GestaoClinica.Controllers
{
    [Area("GestaoClinica")]
    public class HomeClinicaController : Controller
    {
        public IActionResult DashboardClinica()
        {
            return View();
        }
    }
}
