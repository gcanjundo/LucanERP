using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    public class EntidadeContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}