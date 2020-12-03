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
        public class EntidadeController : Controller
        {
        private readonly KitandaConfig _kitandaConfig;
        public EntidadeController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
            public ActionResult CreateEntidade()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateEntidade([Bind] EntidadeDTO dto)
            {
                if (ModelState.IsValid)
                {
                    EntidadeRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateEntidade");
                }
                return View(dto);
            }
            [HttpGet]
            public IActionResult UpdateEntidade(int? id, [Bind] EntidadeDTO dto)
            {
                return View(dto);
            }
            [HttpPut]
            public IActionResult UpdateEntidade([Bind] EntidadeDTO dto)
            {
                if (ModelState.IsValid)
                {
                    EntidadeRN.GetInstance().Salvar(dto);
                    return RedirectToAction("UpdateEntidade");
                }
                return View(dto);
            }
            public ActionResult DeleteEntidade(EntidadeDTO dto)
            {
                EntidadeRN.GetInstance().Eliminar(dto);
                return RedirectToAction("DeleteEntidade");

            }
            public IActionResult ListEntidade(EntidadeDTO dto)
            {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(EntidadeRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(EntidadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(EntidadeRN.GetInstance().ObterPorFiltro(dto));
        }

            public IActionResult ListaDocumento(EntidadeDTO dto)
            {
                IEnumerable<EntidadeDTO> lista = EntidadeRN.GetInstance().ObterPorFiltro(dto);
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