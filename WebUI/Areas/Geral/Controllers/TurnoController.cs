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
    public class TurnoController : Controller
    {
        private List<TurnoDTO> lista;

        [HttpGet]
        public ActionResult CreateTurno()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTurno([Bind] TurnoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TurnoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTurno");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTurno(int? id, [Bind] TurnoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTurno([Bind] TurnoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TurnoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTurno");
            }
            return View(dto);
        }
        public ActionResult DeleteTurno(TurnoDTO dto)
        {
            TurnoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteTurno");

        }
        public IActionResult ListTurno(TurnoDTO dto)
        {
            lista = new List<TurnoDTO>();
            lista = TurnoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(TurnoDTO dto)
        {
            IEnumerable<TurnoDTO> resultado = TurnoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaTurno(TurnoDTO dto)
        {
            IEnumerable<TurnoDTO> lista = TurnoRN.GetInstance().ObterPorFiltro(dto);
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