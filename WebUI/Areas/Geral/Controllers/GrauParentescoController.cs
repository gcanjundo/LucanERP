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
    public class GrauParentescoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public GrauParentescoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
        public ActionResult CreateGrauParentesco()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrauParentesco([Bind] GrauParentescoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GrauParentescoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateGrauParentesco");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateGrauParentesco(int? id, [Bind] GrauParentescoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateGrauParentesco([Bind] GrauParentescoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GrauParentescoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateGrauParentesco");
            }
            return View(dto);
        }
        public ActionResult DeleteGrauParentesco(GrauParentescoDTO dto)
        {
            GrauParentescoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteGrauParentesco");

        }
        public IActionResult ListGrauParentesco(GrauParentescoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(GrauParentescoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(GrauParentescoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(GrauParentescoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaGrauParentesco(GrauParentescoDTO dto)
        {
            IEnumerable<GrauParentescoDTO> lista = GrauParentescoRN.GetInstance().ObterPorFiltro(dto);
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