using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class ArmazemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
