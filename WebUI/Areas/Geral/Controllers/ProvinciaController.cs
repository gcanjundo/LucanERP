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
    public class ProvinciaController : Controller
    {
        private List<ProvinciaDTO> lista;

        [HttpGet]
        public ActionResult CreateProvincia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProvincia([Bind] ProvinciaDTO dto)
        {
            if (ModelState.IsValid)
            {
                ProvinciaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateProvincia");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateProvincia(int? id, [Bind] ProvinciaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateProvincia([Bind] ProvinciaDTO dto)
        {
            if (ModelState.IsValid)
            {
                ProvinciaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateProvincia");
            }
            return View(dto);
        }
        public ActionResult DeleteProvincia(ProvinciaDTO dto)
        {
            ProvinciaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteProvincia");

        }
        public IActionResult ListProvincia(ProvinciaDTO dto)
        {
            lista = new List<ProvinciaDTO>();
            lista = ProvinciaRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(ProvinciaDTO dto)
        {
            IEnumerable<ProvinciaDTO> resultado = ProvinciaRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaProvincia(ProvinciaDTO dto)
        {
            IEnumerable<ProvinciaDTO> lista = ProvinciaRN.GetInstance().ObterPorFiltro(dto);
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