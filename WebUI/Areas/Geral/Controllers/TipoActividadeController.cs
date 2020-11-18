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
    public class TipoActividadeController : Controller
    {
        private List<TipoActividadeDTO> lista;

        [HttpGet]
        public ActionResult CreateTipoActividade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTipoActividade([Bind] TipoActividadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                TipoActividadeRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTipoActividade");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTipoActividade(int? id, [Bind] TipoActividadeDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTipoActividade([Bind] TipoActividadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                TipoActividadeRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTipoActividade");
            }
            return View(dto);
        }
        public ActionResult DeleteTipoActividade(TipoActividadeDTO dto)
        {
            TipoActividadeRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteTipoActividade");

        }
        public IActionResult ListTipoActividade(TipoActividadeDTO dto)
        {
            lista = new List<TipoActividadeDTO>();
            lista = TipoActividadeRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(TipoActividadeDTO dto)
        {
            IEnumerable<TipoActividadeDTO> resultado = TipoActividadeRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaTipoActividade(TipoActividadeDTO dto)
        {
            IEnumerable<TipoActividadeDTO> lista = TipoActividadeRN.GetInstance().ObterPorFiltro(dto);
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