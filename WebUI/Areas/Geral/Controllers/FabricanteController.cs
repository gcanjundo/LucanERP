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
    public class FabricanteController : Controller
    {
        private List<FabricanteDTO> lista;

        [HttpGet]
        public ActionResult SaveFabricante(int? codigo, [Bind] FabricanteDTO dto)
        {
            if (codigo == 0)
            {
                return View();
            }
            else
            {
                var result = FabricanteRN.GetInstance().ObterPorPK(dto);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult SaveFabricante([Bind] FabricanteDTO dto)
        {
            if (ModelState.IsValid)
            {
                FabricanteRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateFabricante");
            }
            return View(dto);
        }

        public ActionResult DeleteFabricante(FabricanteDTO dto)
        {
            FabricanteRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteFabricante");

        }
        public IActionResult ListFabricante(FabricanteDTO dto)
        {
            lista = new List<FabricanteDTO>();
            lista = FabricanteRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(FabricanteDTO dto)
        {
            IEnumerable<FabricanteDTO> resultado = FabricanteRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaFabricante(FabricanteDTO dto)
        {
            IEnumerable<FabricanteDTO> lista = FabricanteRN.GetInstance().ObterPorFiltro(dto);
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
