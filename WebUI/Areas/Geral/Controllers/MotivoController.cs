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
    public class MotivoController : Controller
    {
        private List<MotivoDTO> lista;

        [HttpGet]
        public ActionResult CreateMotivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMotivo([Bind] MotivoDTO dto)
        {
            if (ModelState.IsValid)
            {
                MotivoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateMotivo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateMotivo(int? id, [Bind] MotivoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateMotivo([Bind] MotivoDTO dto)
        {
            if (ModelState.IsValid)
            {
                MotivoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateMotivo");
            }
            return View(dto);
        }
        public ActionResult DeleteMotivo(MotivoDTO dto)
        {
            MotivoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMotivo");

        }
        public IActionResult ListMotivo(MotivoDTO dto)
        {
            lista = new List<MotivoDTO>();
            lista = MotivoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(MotivoDTO dto)
        {
            IEnumerable<MotivoDTO> resultado = MotivoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaMotivo(MotivoDTO dto)
        {
            IEnumerable<MotivoDTO> lista = MotivoRN.GetInstance().ObterPorFiltro(dto);
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