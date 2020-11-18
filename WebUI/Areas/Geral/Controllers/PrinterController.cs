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
    public class PrinterController : Controller
    {
        private List<PrinterDTO> lista;

        [HttpGet]
        public ActionResult CreatePrinter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePrinter([Bind] PrinterDTO dto)
        {
            if (ModelState.IsValid)
            {
                PrinterRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreatePrinter");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdatePrinter(int? id, [Bind] PrinterDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdatePrinter([Bind] PrinterDTO dto)
        {
            if (ModelState.IsValid)
            {
                PrinterRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdatePrinter");
            }
            return View(dto);
        }
        public ActionResult DeletePrinter(PrinterDTO dto)
        {
            PrinterRN.GetInstance().Remover(dto);
            return RedirectToAction("DeletePrinter");

        }
        public IActionResult ListPrinter(PrinterDTO dto)
        {
            lista = new List<PrinterDTO>();
            lista = PrinterRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(PrinterDTO dto)
        {
            IEnumerable<PrinterDTO> resultado = PrinterRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaPrinter(PrinterDTO dto)
        {
            IEnumerable<PrinterDTO> lista = PrinterRN.GetInstance().ObterPorFiltro(dto);
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