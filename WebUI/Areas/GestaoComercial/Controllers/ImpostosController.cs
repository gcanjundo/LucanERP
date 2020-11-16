using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.GestaoComercial.Controllers
{
    public class ImpostosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
