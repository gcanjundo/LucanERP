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
    public class CategoriaController : Controller
    {
        private List<CategoriaDTO> lista;

        [HttpGet]
        public ActionResult SaveCategoria(int? codigo, [Bind] CategoriaDTO dto)
        {
            if (codigo == 0)
            {
                return View();
            }
            else
            {
                var result = CategoriaRN.GetInstance().ObterPorPK(dto);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult SaveCategoria([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateCategoria");
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