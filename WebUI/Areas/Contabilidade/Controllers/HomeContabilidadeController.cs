using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Contabilidade.Controllers
{
    [Area("Contabilidade")]
    public class HomeContabilidadeController : Controller
    {
        public IActionResult DashboardContabilidade()
        {
            return View();
        }
    }
}
