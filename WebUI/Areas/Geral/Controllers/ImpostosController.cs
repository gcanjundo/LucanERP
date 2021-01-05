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
    public class ImpostosController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public ImpostosController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateImpostos()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateImpostos([Bind] ImpostosDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                ImpostosRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreateImpostos");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateImpostos(int? id, [Bind] ImpostosDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateImpostos([Bind] ImpostosDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                ImpostosRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateImpostos");
            }
            return View(dto);
        }
        public ActionResult DeleteImpostos(ImpostosDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            ImpostosRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteImpostos");

        }
        public IActionResult ListImpostos(ImpostosDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ImpostosRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(ImpostosDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ImpostosRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaImpostos(ImpostosDTO dto)
        {
            IEnumerable<ImpostosDTO> lista = ImpostosRN.GetInstance().ObterPorFiltro(dto);
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