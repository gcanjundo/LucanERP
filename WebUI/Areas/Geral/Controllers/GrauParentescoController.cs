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
    public class GrauParentescoController : Controller
    {
        private List<GrauParentescoDTO> lista;

        [HttpGet]
        public ActionResult CreateGrauParentesco()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrauParentesco([Bind] GrauParentescoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GrauParentescoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateGrauParentesco");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateGrauParentesco(int? id, [Bind] GrauParentescoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateGrauParentesco([Bind] GrauParentescoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GrauParentescoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateGrauParentesco");
            }
            return View(dto);
        }
        public ActionResult DeleteGrauParentesco(GrauParentescoDTO dto)
        {
            GrauParentescoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteGrauParentesco");

        }
        public IActionResult ListGrauParentesco(GrauParentescoDTO dto)
        {
            lista = new List<GrauParentescoDTO>();
            lista = GrauParentescoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(GrauParentescoDTO dto)
        {
            IEnumerable<GrauParentescoDTO> resultado = GrauParentescoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaGrauParentesco(GrauParentescoDTO dto)
        {
            IEnumerable<GrauParentescoDTO> lista = GrauParentescoRN.GetInstance().ObterPorFiltro(dto);
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