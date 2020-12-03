using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class CategoriaController : Controller
    {

        private readonly KitandaConfig _kitandaConfig;
        public CategoriaController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateCategoria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategoria([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateCategoria");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateCategoria(int? id, [Bind] CategoriaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateCategoria([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                CategoriaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateCategoria");
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
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(CategoriaRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(CategoriaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(CategoriaRN.GetInstance().ObterPorFiltro(dto));
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