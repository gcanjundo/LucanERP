using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class PaisController : Controller
    {
        private List<PaisDTO> lista;

        [HttpGet]
        public ActionResult CreatePais()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePais([Bind] PaisDTO dto)
        {
            if (ModelState.IsValid)
            {
                PaisRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreatePais");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdatePais(int? id, [Bind] PaisDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdatePais([Bind] PaisDTO dto)
        {
            if (ModelState.IsValid)
            {
                PaisRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdatePais");
            }
            return View(dto);
        }
        public ActionResult DeletePais(PaisDTO dto)
        {
            PaisRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeletePais");

        }
        public IActionResult ListPais(PaisDTO dto)
        {
            lista = new List<PaisDTO>();
            lista = PaisRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(PaisDTO dto)
        {
            IEnumerable<PaisDTO> resultado = PaisRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaPais(PaisDTO dto)
        {
            IEnumerable<PaisDTO> lista = PaisRN.GetInstance().ObterPorFiltro(dto);
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