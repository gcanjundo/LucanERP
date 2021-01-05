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
    public class StatusController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public StatusController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStatus([Bind] StatusDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                StatusRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateStatus");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateStatus(int? id, [Bind] StatusDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateStatus([Bind] StatusDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                StatusRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateStatus");
            }
            return View(dto);
        }
        public ActionResult DeleteStatus(StatusDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            StatusRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteStatus");

        }
        public IActionResult ListStatus(StatusDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(StatusRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(StatusDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(StatusRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaStatus(StatusDTO dto)
        {
            IEnumerable<StatusDTO> lista = StatusRN.GetInstance().ObterPorFiltro(dto);
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