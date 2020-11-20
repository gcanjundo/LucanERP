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
    public class ProfissaoController : Controller
    {
        private List<ProfissaoDTO> lista;

        [HttpGet]
        public ActionResult CreateProfissao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProfissao([Bind] ProfissaoDTO dto)
        {
            if (ModelState.IsValid)
            {
                ProfissaoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateProfissao");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateProfissao(int? id, [Bind] ProfissaoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateProfissao([Bind] ProfissaoDTO dto)
        {
            if (ModelState.IsValid)
            {
                ProfissaoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateProfissao");
            }
            return View(dto);
        }
        public ActionResult DeleteProfissao(ProfissaoDTO dto)
        {
            ProfissaoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteProfissao");

        }
        public IActionResult ListProfissao(ProfissaoDTO dto)
        {
            lista = new List<ProfissaoDTO>();
            lista = ProfissaoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(ProfissaoDTO dto)
        {
            IEnumerable<ProfissaoDTO> resultado = ProfissaoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaProfissao(ProfissaoDTO dto)
        {
            IEnumerable<ProfissaoDTO> lista = ProfissaoRN.GetInstance().ObterPorFiltro(dto);
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