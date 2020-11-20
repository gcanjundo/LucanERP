using BusinessLogicLayer.Geral;
using DataAccessLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Seguranca.Controllers
{
    [Area("Seguranca")]
    public class PerfisController : Controller
    {
        public ActionResult Perfis()
        {
            return View();
        }
    }
}
