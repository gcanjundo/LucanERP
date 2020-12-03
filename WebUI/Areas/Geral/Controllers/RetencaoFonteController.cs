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
        public class RetencaoFonteController : Controller
        {
        private readonly KitandaConfig _kitandaConfig;
        public RetencaoFonteController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
            public ActionResult CreateRetencaoFonte()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateRetencaoFonte([Bind] RetencaoFonteDTO dto)
            {
                if (ModelState.IsValid)
                {
                    RetencaoFonteRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateRetencaoFonte");
                }
                return View(dto);
            }
            [HttpGet]
            public IActionResult UpdateRetencaoFonte(int? id, [Bind] RetencaoFonteDTO dto)
            {
                return View(dto);
            }
            [HttpPut]
            public IActionResult UpdateRetencaoFonte([Bind] RetencaoFonteDTO dto)
            {
                if (ModelState.IsValid)
                {
                    RetencaoFonteRN.GetInstance().Salvar(dto);
                    return RedirectToAction("UpdateRetencaoFonte");
                }
                return View(dto);
            }
            public ActionResult DeleteRetencaoFonte(RetencaoFonteDTO dto)
            {
                RetencaoFonteRN.GetInstance().Apagar(dto);
                return RedirectToAction("DeleteRetencaoFonte");

            }
            public IActionResult ListRetencaoFonte(RetencaoFonteDTO dto)
            {
               GetSessionDetails();
               dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
               dto.Filial = _kitandaConfig.pSessionInfo.Filial;
               return View(RetencaoFonteRN.GetInstance().ObterPorFiltro(dto));

            }


            public IActionResult Pesquisar(RetencaoFonteDTO dto)
            {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(RetencaoFonteRN.GetInstance().ObterPorFiltro(dto));
            }

            public IActionResult ListaRetencaoFonte(RetencaoFonteDTO dto)
            {
                IEnumerable<RetencaoFonteDTO> lista = RetencaoFonteRN.GetInstance().ObterPorFiltro(dto);
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