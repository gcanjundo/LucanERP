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
    public class ArtigoController : Controller
    {
        private List<ArtigoDTO> lista;

        [HttpGet]
        public ActionResult CreateArtigo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateArtigo([Bind] ArtigoDTO dto)
        {
            if (ModelState.IsValid)
            {
                ArtigoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateArtigo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateArtigo(int? id, [Bind] ArtigoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateArtigo([Bind] ArtigoDTO dto)
        {
            if (ModelState.IsValid)
            {
                ArtigoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateArtigo");
            }
            return View(dto);
        }
        public ActionResult DeleteArtigo(ArtigoDTO dto)
        {
            ArtigoRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteArtigo");

        }
        public IActionResult ListArtigo(ArtigoDTO dto)
        {
            lista = new List<ArtigoDTO>();
            lista = ArtigoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(ArtigoDTO  dto)
        {
            IEnumerable<ArtigoDTO> resultado = ArtigoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaArtigos(ArtigoDTO dto)
        {
            IEnumerable<ArtigoDTO> lista = ArtigoRN.GetInstance().ObterPorFiltro(dto);
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
