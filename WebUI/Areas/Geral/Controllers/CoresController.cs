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
        public IActionResult SaveCores(int? codigo, [Bind] CoresDTO dto)
        {
            if(codigo == 0)
            {
                return View( new CoresDTO());
            }
            else
            {
                var result =  CoresRN.GetInstance().ObterPorPK(dto);
                return View(result);
            }
            
        }
        [HttpPost]
        public IActionResult SaveCores([Bind] CoresDTO dto)
        {
            if (ModelState.IsValid)
            {
                CoresRN.GetInstance().Salvar(dto);
                return Json(new { isValid = true });
            }
            return Json(new { isValid = false });
        }
        [HttpGet]
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


        public IActionResult Pesquisar(string? dto)
        {
             IEnumerable<CoresDTO> resultado = CoresRN.GetInstance().ListaCoress(dto);
            return View(resultado);
        }
    }


}
