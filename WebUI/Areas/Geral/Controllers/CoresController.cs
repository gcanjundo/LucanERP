using BusinessLogicLayer;
using BusinessLogicLayer.Geral;
using DataAccessLayer.Geral;
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
        private List<CoresDTO> lista;

        [HttpGet]
        public ActionResult CreateCores()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCores([Bind] CoresDTO dto)
        {
            if (ModelState.IsValid)
            {
                CoresRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreateCores");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateCores(int? id, [Bind] CoresDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateCores([Bind] CoresDTO dto)
        {
            if (ModelState.IsValid)
            {
                CoresRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateCores");
            }
            return View(dto);
        }
        public ActionResult DeleteCores(CoresDTO dto)
        {
            CoresRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteArmazem");

        }
        public IActionResult ListCores(CoresDTO dto)
        {
            lista = new List<CoresDTO>();
            lista = CoresRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(CoresDTO dto)
        {
             IEnumerable<CoresDTO> resultado = CoresRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }
    }


}
