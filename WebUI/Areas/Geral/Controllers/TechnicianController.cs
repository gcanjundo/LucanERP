using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Comercial;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class TechnicianController : Controller
    {
        private List<TechnicianDTO> lista;

        [HttpGet]
        public ActionResult CreateTechnician()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTechnician([Bind] TechnicianDTO dto)
        {
            if (ModelState.IsValid)
            {
                TechnicianRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTechnician");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTechnician(int? id, [Bind] TechnicianDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTechnician([Bind] TechnicianDTO dto)
        {
            if (ModelState.IsValid)
            {
                TechnicianRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTechnician");
            }
            return View(dto);
        }
      
        public IActionResult ListTechnician(TechnicianDTO dto)
        {
            lista = new List<TechnicianDTO>();
            lista = TechnicianRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(TechnicianDTO dto)
        {
            IEnumerable<TechnicianDTO> resultado = TechnicianRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaTechnician(TechnicianDTO dto)
        {
            IEnumerable<TechnicianDTO> lista = TechnicianRN.GetInstance().ObterPorFiltro(dto);
            var lblRegisto = "";
            if (lista.Count() >= 1)
            {
                lblRegisto = lista.Count() + " Artigo(s) Encontrado(s)";
            }
            else
            {
                lblRegisto = "Nenhum Artigo Encontrado";
            }
            return View(lista);
        }
    }
}