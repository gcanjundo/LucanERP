using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Comercial;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class ArtigoController : Controller
    { 

        private readonly KitandaConfig _kitandaConfig;
        public ArtigoController(KitandaConfig kitandaConfig)
        { 
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateArtigo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateArtigo([Bind] ArtigoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                ArtigoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateArtigo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateArtigo(int? id, [Bind] ArtigoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            ArtigoRN.GetInstance().Salvar(dto);
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateArtigo([Bind] ArtigoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                ArtigoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateArtigo");
            }
            return View(dto);
        }
        public ActionResult DeleteArtigo(ArtigoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            ArtigoRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteArtigo");

        }
        public IActionResult ListArtigo(ArtigoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ArtigoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(ArtigoDTO  dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ArtigoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaArtigos(ArtigoDTO dto)
        {
            IEnumerable<ArtigoDTO> lista = ArtigoRN.GetInstance().ObterPorFiltro(dto);
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
