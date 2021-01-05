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
    public class TipoActividadeController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public TipoActividadeController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateTipoActividade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTipoActividade([Bind] TipoActividadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                TipoActividadeRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTipoActividade");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTipoActividade(int? id, [Bind] TipoActividadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTipoActividade([Bind] TipoActividadeDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                TipoActividadeRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTipoActividade");
            }
            return View(dto);
        }
        public ActionResult DeleteTipoActividade(TipoActividadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            TipoActividadeRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteTipoActividade");

        }
        public IActionResult ListTipoActividade(TipoActividadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TipoActividadeRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(TipoActividadeDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TipoActividadeRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaTipoActividade(TipoActividadeDTO dto)
        {
            IEnumerable<TipoActividadeDTO> lista = TipoActividadeRN.GetInstance().ObterPorFiltro(dto);
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