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
    public class MoedaController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public MoedaController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateMoeda()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMoeda([Bind] MoedaDTO dto)
        {
            if (ModelState.IsValid)
            {
                MoedaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateMoeda");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateMoeda(int? id, [Bind] MoedaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateMoeda([Bind] MoedaDTO dto)
        {
            if (ModelState.IsValid)
            {
                MoedaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateMoeda");
            }
            return View(dto);
        }
        public ActionResult DeleteMoeda(MoedaDTO dto)
        {
            MoedaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMoeda");

        }
        public IActionResult ListMoeda(MoedaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MoedaRN.GetInstance().ObterPorFiltro(dto));
        }
      

        public IActionResult Pesquisar(MoedaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MoedaRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaMoeda(MoedaDTO dto)
        {
            IEnumerable<MoedaDTO> lista = MoedaRN.GetInstance().ObterPorFiltro(dto);
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