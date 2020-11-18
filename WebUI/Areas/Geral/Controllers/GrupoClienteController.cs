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
    public class GrupoClienteController : Controller
    {
        private List<CategoriaDTO> lista;

        [HttpGet]
        public ActionResult CreateGrupoCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrupoCliente([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateGrupoCliente");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateGrupoCliente(int? id, [Bind] CategoriaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateGrupoCliente([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateGrupoCliente");
            }
            return View(dto);
        }
        public ActionResult DeleteGrupoCliente(CategoriaDTO dto)
        {
            CategoriaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteGrupoCliente");

        }
        public IActionResult ListGrupoCliente(CategoriaDTO dto)
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

        public IActionResult ListaGrupoCliente(CategoriaDTO dto)
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