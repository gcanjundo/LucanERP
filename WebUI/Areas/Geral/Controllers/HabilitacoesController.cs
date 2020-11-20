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
    public class HabilitacoesController : Controller
    {
        private List<HabilitacoesDTO> lista;

        [HttpGet]
        public ActionResult CreateHabilitacoes()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateHabilitacoes([Bind] HabilitacoesDTO dto)
        {
            if (ModelState.IsValid)
            {
                HabilitacoesRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateHabilitacoes");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateHabilitacoes(int? id, [Bind] HabilitacoesDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateHabilitacoes([Bind] HabilitacoesDTO dto)
        {
            if (ModelState.IsValid)
            {
                HabilitacoesRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateHabilitacoes");
            }
            return View(dto);
        }
        public ActionResult DeleteHabilitacoes(HabilitacoesDTO dto)
        {
            HabilitacoesRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteHabilitacoes");

        }
        public IActionResult ListHabilitacoes(HabilitacoesDTO dto)
        {
            lista = new List<HabilitacoesDTO>();
            lista = HabilitacoesRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(HabilitacoesDTO dto)
        {
            IEnumerable<HabilitacoesDTO> resultado = HabilitacoesRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaHabilitacoes(HabilitacoesDTO dto)
        {
            IEnumerable<HabilitacoesDTO> lista = HabilitacoesRN.GetInstance().ObterPorFiltro(dto);
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