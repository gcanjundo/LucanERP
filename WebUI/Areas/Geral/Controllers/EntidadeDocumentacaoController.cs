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
        public class EntidadeDocumentacaoController : Controller
        {
            private List<EntidadeDocumentacaoDTO> lista;

            [HttpGet]
            public ActionResult CreateEntidadeDocumentacao()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateEntidadeDocumentacao([Bind] EntidadeDocumentacaoDTO dto)
            {
                if (ModelState.IsValid)
                {
                    EntidadeDocumentacaoRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateEntidadeDocumentacao");
                }
                return View(dto);
            }
            [HttpGet]
            public IActionResult UpdateEntidadeDocumentacao(int? id, [Bind] EntidadeDocumentacaoDTO dto)
            {
                return View(dto);
            }
            [HttpPut]
            public IActionResult UpdateEntidadeDocumentacao([Bind] EntidadeDocumentacaoDTO dto)
            {
                if (ModelState.IsValid)
                {
                    EntidadeDocumentacaoRN.GetInstance().Salvar(dto);
                    return RedirectToAction("UpdateEntidadeDocumentacao");
                }
                return View(dto);
            }
            public ActionResult DeleteEntidadeDocumentacao(EntidadeDocumentacaoDTO dto)
            {
                EntidadeDocumentacaoRN.GetInstance().Eliminar(dto);
                return RedirectToAction("DeleteEntidadeDocumentacao");

            }
            public IActionResult ListEntidadeDocumentacao(EntidadeDocumentacaoDTO dto)
            {
                lista = new List<EntidadeDocumentacaoDTO>();
                lista = EntidadeDocumentacaoRN.GetInstance().ObterPorFiltro(dto);
                return View(lista);
            }


            public IActionResult Pesquisar(EntidadeDocumentacaoDTO dto)
            {
                IEnumerable<EntidadeDocumentacaoDTO> resultado = EntidadeDocumentacaoRN.GetInstance().ObterPorFiltro(dto);
                return View(resultado);
            }

            public IActionResult ListaEntidadeDocumentacao(EntidadeDocumentacaoDTO dto)
            {
                IEnumerable<EntidadeDocumentacaoDTO> lista = EntidadeDocumentacaoRN.GetInstance().ObterPorFiltro(dto);
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