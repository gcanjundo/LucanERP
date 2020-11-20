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
    public class StatusController : Controller
    {
        private List<StatusDTO> lista;

        [HttpGet]
        public ActionResult CreateStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStatus([Bind] StatusDTO dto)
        {
            if (ModelState.IsValid)
            {
                StatusRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateStatus");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateStatus(int? id, [Bind] StatusDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateStatus([Bind] StatusDTO dto)
        {
            if (ModelState.IsValid)
            {
                StatusRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateStatus");
            }
            return View(dto);
        }
        public ActionResult DeleteStatus(StatusDTO dto)
        {
            StatusRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteStatus");

        }
        public IActionResult ListStatus(StatusDTO dto)
        {
            lista = new List<StatusDTO>();
            lista = StatusRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(StatusDTO dto)
        {
            IEnumerable<StatusDTO> resultado = StatusRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaStatus(StatusDTO dto)
        {
            IEnumerable<StatusDTO> lista = StatusRN.GetInstance().ObterPorFiltro(dto);
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