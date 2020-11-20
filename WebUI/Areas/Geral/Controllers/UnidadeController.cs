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
    public class UnidadeController : Controller
    {
        private List<UnidadeDTO> lista;

        [HttpGet]
        public ActionResult CreateUnidade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUnidade([Bind] UnidadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                UnidadeRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateUnidade");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateUnidade(int? id, [Bind] UnidadeDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateUnidade([Bind] UnidadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                UnidadeRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateUnidade");
            }
            return View(dto);
        }
        public ActionResult DeleteUnidade(UnidadeDTO dto)
        {
            UnidadeRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteUnidade");

        }
        public IActionResult ListUnidade(UnidadeDTO dto)
        {
            lista = new List<UnidadeDTO>();
            lista = UnidadeRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(UnidadeDTO dto)
        {
            IEnumerable<UnidadeDTO> resultado = UnidadeRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaUnidade(UnidadeDTO dto)
        {
            IEnumerable<UnidadeDTO> lista = UnidadeRN.GetInstance().ObterPorFiltro(dto);
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