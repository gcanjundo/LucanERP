﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class ImpostosController : Controller
    {
        private List<ImpostosDTO> lista;

        [HttpGet]
        public ActionResult CreateImpostos()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateImpostos([Bind] ImpostosDTO dto)
        {
            if (ModelState.IsValid)
            {
                ImpostosRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateImpostos");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateImpostos(int? id, [Bind] ImpostosDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateImpostos([Bind] ImpostosDTO dto)
        {
            if (ModelState.IsValid)
            {
                ImpostosRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateImpostos");
            }
            return View(dto);
        }
        public ActionResult DeleteImpostos(ImpostosDTO dto)
        {
            ImpostosRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteImpostos");

        }
        public IActionResult ListImpostos(ImpostosDTO dto)
        {
            lista = new List<ImpostosDTO>();
            lista = ImpostosRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(ImpostosDTO dto)
        {
            IEnumerable<ImpostosDTO> resultado = ImpostosRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaImpostos(ImpostosDTO dto)
        {
            IEnumerable<ImpostosDTO> lista = ImpostosRN.GetInstance().ObterPorFiltro(dto);
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