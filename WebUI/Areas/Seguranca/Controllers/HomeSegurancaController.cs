using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Seguranca.Controllers
{
    public class HomeSegurancaController : Controller
    {
        public IActionResult IndexSeguranca()
        {
            return View();
        }
    }
}
