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
    public class ProvinciaController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public ProvinciaController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateProvincia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProvincia([Bind] ProvinciaDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                ProvinciaRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreateProvincia");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateProvincia(int? id, [Bind] ProvinciaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateProvincia([Bind] ProvinciaDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                ProvinciaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateProvincia");
            }
            return View(dto);
        }
        public ActionResult DeleteProvincia(ProvinciaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            ProvinciaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteProvincia");

        }
        public IActionResult ListProvincia(ProvinciaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ProvinciaRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(ProvinciaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ProvinciaRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaProvincia(ProvinciaDTO dto)
        {
            IEnumerable<ProvinciaDTO> lista = ProvinciaRN.GetInstance().ObterPorFiltro(dto);
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