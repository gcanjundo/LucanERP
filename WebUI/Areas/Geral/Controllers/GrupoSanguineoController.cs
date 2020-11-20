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
        public class GrupoSanguineoController : Controller
        {
            private List<GrupoSanguineoDTO> lista;

            [HttpGet]
            public ActionResult CreateGrupoSanguineo()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateGrupoSanguineo([Bind] GrupoSanguineoDTO dto)
            {
                if (ModelState.IsValid)
                {
                    GrupoSanguineoRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateGrupoSanguineo");
                }
                return View(dto);
            }
            [HttpGet]
            public IActionResult UpdateGrupoSanguineo(int? id, [Bind] GrupoSanguineoDTO dto)
            {
                return View(dto);
            }
            [HttpPut]
            public IActionResult UpdateGrupoSanguineo([Bind] GrupoSanguineoDTO dto)
            {
                if (ModelState.IsValid)
                {
                    GrupoSanguineoRN.GetInstance().Salvar(dto);
                    return RedirectToAction("UpdateGrupoSanguineo");
                }
                return View(dto);
            }
            public ActionResult DeleteGrupoSanguineo(GrupoSanguineoDTO dto)
            {
                GrupoSanguineoRN.GetInstance().Excluir(dto);
                return RedirectToAction("DeleteGrupoSanguineo");

            }
            public IActionResult ListGrupoSanguineo(GrupoSanguineoDTO dto)
            {
                lista = new List<GrupoSanguineoDTO>();
                lista = GrupoSanguineoRN.GetInstance().ObterPorFiltro(dto);
                return View(lista);
            }


            public IActionResult Pesquisar(GrupoSanguineoDTO dto)
            {
                IEnumerable<GrupoSanguineoDTO> resultado = GrupoSanguineoRN.GetInstance().ObterPorFiltro(dto);
                return View(resultado);
            }

            public IActionResult ListaGrupoSanguineo(GrupoSanguineoDTO dto)
            {
                IEnumerable<GrupoSanguineoDTO> lista = GrupoSanguineoRN.GetInstance().ObterPorFiltro(dto);
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