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
    public class MunicipioController : Controller
    {
        private List<MunicipioDTO> lista;

        [HttpGet]
        public ActionResult CreateMunicipio()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMunicipio([Bind] MunicipioDTO dto)
        {
            if (ModelState.IsValid)
            {
                MunicipioRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateMunicipio");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateMunicipio(int? id, [Bind] MunicipioDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateMunicipio([Bind] MunicipioDTO dto)
        {
            if (ModelState.IsValid)
            {
                MunicipioRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateMunicipio");
            }
            return View(dto);
        }
        public ActionResult DeleteMunicipio(MunicipioDTO dto)
        {
            MunicipioRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMunicipio");

        }
        public IActionResult ListMunicipio(MunicipioDTO dto)
        {
            lista = new List<MunicipioDTO>();
            lista = MunicipioRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(MunicipioDTO dto)
        {
            IEnumerable<MunicipioDTO> resultado = MunicipioRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaMunicipio(MunicipioDTO dto)
        {
            IEnumerable<MunicipioDTO> lista = MunicipioRN.GetInstance().ObterPorFiltro(dto);
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