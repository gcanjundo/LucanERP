using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    public class ArmazemController : Controller
    {
        public IActionResult Pesquisar(string dto)
        {
            //IEnumerable<ArmazemDTO> resultado = ArmazemRN.GetInstance().ObterPorFiltro(dto);
            return View();
        }
    }
}
