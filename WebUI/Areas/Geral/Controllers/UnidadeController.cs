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
    public class UnidadeController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public UnidadeController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateUnidade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUnidade([Bind] UnidadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                UnidadeRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateUnidade");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateUnidade(int? id, [Bind] UnidadeDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateUnidade([Bind] UnidadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                UnidadeRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateUnidade");
            }
            return View(dto);
        }
        public ActionResult DeleteUnidade(UnidadeDTO dto)
        {
            UnidadeRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteUnidade");

        }
        public IActionResult ListUnidade(UnidadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(UnidadeRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(UnidadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(UnidadeRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaUnidade(UnidadeDTO dto)
        {
            IEnumerable<UnidadeDTO> lista = UnidadeRN.GetInstance().ObterPorFiltro(dto);
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