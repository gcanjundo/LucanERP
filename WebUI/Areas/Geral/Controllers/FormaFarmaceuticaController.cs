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
    public class FormaFarmaceuticaController : Controller
    {
        private List<FormaFarmaceuticaDTO> lista;

        [HttpGet]
        public ActionResult CreateFormaFarmaceutica()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFormaFarmaceutica([Bind] FormaFarmaceuticaDTO dto)
        {
            if (ModelState.IsValid)
            {
                FormaFarmaceuticaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateFormaFarmaceutica");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateFormaFarmaceutica(int? id, [Bind] FormaFarmaceuticaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateFormaFarmaceutica([Bind] FormaFarmaceuticaDTO dto)
        {
            if (ModelState.IsValid)
            {
                FormaFarmaceuticaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateFormaFarmaceutica");
            }
            return View(dto);
        }
        public ActionResult DeleteFormaFarmaceutica(FormaFarmaceuticaDTO dto)
        {
            FormaFarmaceuticaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteFormaFarmaceutica");

        }
        public IActionResult ListFormaFarmaceutica(FormaFarmaceuticaDTO dto)
        {
            lista = new List<FormaFarmaceuticaDTO>();
            lista = FormaFarmaceuticaRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(FormaFarmaceuticaDTO dto)
        {
            IEnumerable<FormaFarmaceuticaDTO> resultado = FormaFarmaceuticaRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaFormaFarmaceutica(FormaFarmaceuticaDTO dto)
        {
            IEnumerable<FormaFarmaceuticaDTO> lista = FormaFarmaceuticaRN.GetInstance().ObterPorFiltro(dto);
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