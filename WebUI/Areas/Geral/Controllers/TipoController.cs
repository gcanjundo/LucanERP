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
    public class TipoController : Controller
    {
        private List<TipoDTO> lista;

        [HttpGet]
        public ActionResult CreateTipo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTipo([Bind] TipoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TipoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTipo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTipo(int? id, [Bind] TipoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTipo([Bind] TipoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TipoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTipo");
            }
            return View(dto);
        }
        public ActionResult DeleteTipo(TipoDTO dto)
        {
            TipoRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteTipo");

        }
        public IActionResult ListTipo(TipoDTO dto)
        {
            lista = new List<TipoDTO>();
            lista = TipoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(TipoDTO dto)
        {
            IEnumerable<TipoDTO> resultado = TipoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaTipo(TipoDTO dto)
        {
            IEnumerable<TipoDTO> lista = TipoRN.GetInstance().ObterPorFiltro(dto);
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