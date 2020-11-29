using BusinessLogicLayer.Geral;
using DataAccessLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class ArmazemController : Controller
    {
       
        private List<ArmazemDTO> lista;
        private IEnumerable<ArmazemDTO> resultado;

        public  ActionResult Index() 
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateArmazem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateArmazem([Bind] ArmazemDTO dto)
        {
            if (ModelState.IsValid)
            {
                ArmazemRN.GetInstance().Salvar(dto);
              
                return RedirectToAction("CreateArmazem");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateArmazem(int? id, [Bind] ArmazemDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateArmazem([Bind] ArmazemDTO dto)
        {
            if (ModelState.IsValid)
            {
                ArmazemRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateArmazem");
            }
            return View(dto);
        }
        public ActionResult DeleteArmazem(ArmazemDTO dto)
        {
            ArmazemRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteArmazem");

        }
        public IActionResult ListArmazem(ArmazemDTO dto)
        {
            lista = new List<ArmazemDTO>();
            lista = ArmazemRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(ArmazemDTO dto)
        {
            resultado = ArmazemRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

    }
}
