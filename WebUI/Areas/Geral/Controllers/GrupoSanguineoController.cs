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
        public class GrupoSanguineoController : Controller
        {
        private readonly KitandaConfig _kitandaConfig;
        public GrupoSanguineoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
            public ActionResult CreateGrupoSanguineo()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateGrupoSanguineo([Bind] GrupoSanguineoDTO dto)
            {
                if (ModelState.IsValid)
                {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                GrupoSanguineoRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateGrupoSanguineo");
                }
                return View(dto);
            }
            [HttpGet]
            public IActionResult UpdateGrupoSanguineo(int? id, [Bind] GrupoSanguineoDTO dto)
            {
               GetSessionDetails();
               dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
               dto.Filial = _kitandaConfig.pSessionInfo.Filial;
               return View(dto);
            }
            [HttpPut]
            public IActionResult UpdateGrupoSanguineo([Bind] GrupoSanguineoDTO dto)
            {
                if (ModelState.IsValid)
                {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                GrupoSanguineoRN.GetInstance().Salvar(dto);
                    return RedirectToAction("UpdateGrupoSanguineo");
                }
                return View(dto);
            }
            public ActionResult DeleteGrupoSanguineo(GrupoSanguineoDTO dto)
            {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            GrupoSanguineoRN.GetInstance().Excluir(dto);
                return RedirectToAction("DeleteGrupoSanguineo");

            }
            public IActionResult ListGrupoSanguineo(GrupoSanguineoDTO dto)
            {
               GetSessionDetails();
               dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
               dto.Filial = _kitandaConfig.pSessionInfo.Filial;
               return View(GrupoSanguineoRN.GetInstance().ObterPorFiltro(dto));
            }


            public IActionResult Pesquisar(GrupoSanguineoDTO dto)
            {
               GetSessionDetails();
               dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
               dto.Filial = _kitandaConfig.pSessionInfo.Filial;
               return View(GrupoSanguineoRN.GetInstance().ObterPorFiltro(dto));
            }

            public IActionResult ListaGrupoSanguineo(GrupoSanguineoDTO dto)
            {
                IEnumerable<GrupoSanguineoDTO> lista = GrupoSanguineoRN.GetInstance().ObterPorFiltro(dto);
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