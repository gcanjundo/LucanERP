using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class PrinterController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public PrinterController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

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
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                PrinterRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreatePrinter");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdatePrinter(int? id, [Bind] PrinterDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdatePrinter([Bind] PrinterDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                PrinterRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdatePrinter");
            }
            return View(dto);
        }
        public ActionResult DeletePrinter(PrinterDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            PrinterRN.GetInstance().Remover(dto);
            return RedirectToAction("DeletePrinter");

        }
        public IActionResult ListPrinter(PrinterDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(PrinterRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(PrinterDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(PrinterRN.GetInstance().ObterPorFiltro(dto));
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