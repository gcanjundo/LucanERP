using BusinessLogicLayer;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class CoresController : Controller
    {

        public IActionResult Pesquisar(String? dto)
        {
             IEnumerable<CoresDTO> resultado = CoresRN.GetInstance().ListaCoress(dto);
            return View(resultado);
        }
    }


}
