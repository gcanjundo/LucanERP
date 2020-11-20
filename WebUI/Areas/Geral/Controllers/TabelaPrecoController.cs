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
    public class TabelaPrecoController : Controller
    {
        private List<TabelaPrecoDTO> lista;

        [HttpGet]
        public ActionResult CreateTabelaPreco()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTabelaPreco([Bind] TabelaPrecoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TabelaPrecoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTabelaPreco");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTabelaPreco(int? id, [Bind] TabelaPrecoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTabelaPreco([Bind] TabelaPrecoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TabelaPrecoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTabelaPreco");
            }
            return View(dto);
        }
        public ActionResult DeleteTabelaPreco(TabelaPrecoDTO dto)
        {
            TabelaPrecoRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteTabelaPreco");

        }
        public IActionResult ListTabelaPreco(TabelaPrecoDTO dto)
        {
            lista = new List<TabelaPrecoDTO>();
            lista = TabelaPrecoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(TabelaPrecoDTO dto)
        {
            IEnumerable<TabelaPrecoDTO> resultado = TabelaPrecoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaTabelaPreco(TabelaPrecoDTO dto)
        {
            IEnumerable<TabelaPrecoDTO> lista = TabelaPrecoRN.GetInstance().ObterPorFiltro(dto);
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