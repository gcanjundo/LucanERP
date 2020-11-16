using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Tesouraria.Controllers
{
    [Area("Tesouraria")]
    public class HomeTesourariaController : Controller
    {
        public IActionResult IndexTesouria()
        {
            return View();
        }
    }
}
