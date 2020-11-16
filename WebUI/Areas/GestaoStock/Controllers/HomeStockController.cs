using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.GestaoStock.Controllers
{
    [Area("GestaoStock")]
    public class HomeStockController : Controller
    {
        public IActionResult DashboardStock()
        {
            return View();
        }
    }
}
