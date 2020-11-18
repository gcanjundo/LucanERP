using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    public class CategoriaVeiculoController : Controller
    {
        private List<CategoriaDTO> lista;

        [HttpGet]
        public ActionResult CreateCategoria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategoria([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateCategoria");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateCategoria(int? id, [Bind] CategoriaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateCategoria([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateCategoria");
            }
            return View(dto);
        }
        public ActionResult DeleteCategoria(CategoriaDTO dto)
        {
            CategoriaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteArtigo");

        }
        public IActionResult ListCategoria(CategoriaDTO dto)
        {
            lista = new List<CategoriaDTO>();
            lista = CategoriaRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(CategoriaDTO dto)
        {
            IEnumerable<CategoriaDTO> resultado = CategoriaRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaArtigos(CategoriaDTO dto)
        {
            IEnumerable<CategoriaDTO> lista = CategoriaRN.GetInstance().ObterPorFiltro(dto);
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