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
        public class ReligiaoController : Controller
        {
            private List<ReligiaoDTO> lista;

            [HttpGet]
            public ActionResult CreateReligiao()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateReligiao([Bind] ReligiaoDTO dto)
            {
                if (ModelState.IsValid)
                {
                    ReligiaoRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateReligiao");
                }
                return View(dto);
            }
            [HttpGet]
            public IActionResult UpdateReligiao(int? id, [Bind] ReligiaoDTO dto)
            {
                return View(dto);
            }
            [HttpPut]
            public IActionResult UpdateReligiao([Bind] ReligiaoDTO dto)
            {
                if (ModelState.IsValid)
                {
                    ReligiaoRN.GetInstance().Salvar(dto);
                    return RedirectToAction("UpdateReligiao");
                }
                return View(dto);
            }
            public ActionResult DeleteReligiao(ReligiaoDTO dto)
            {
                ReligiaoRN.GetInstance().Excluir(dto);
                return RedirectToAction("DeleteReligiao");

            }
            public IActionResult ListReligiao(ReligiaoDTO dto)
            {
                lista = new List<ReligiaoDTO>();
                lista = ReligiaoRN.GetInstance().ObterPorFiltro(dto);
                return View(lista);
            }


            public IActionResult Pesquisar(ReligiaoDTO dto)
            {
                IEnumerable<ReligiaoDTO> resultado = ReligiaoRN.GetInstance().ObterPorFiltro(dto);
                return View(resultado);
            }

            public IActionResult ListaReligiao(ReligiaoDTO dto)
            {
                IEnumerable<ReligiaoDTO> lista = ReligiaoRN.GetInstance().ObterPorFiltro(dto);
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