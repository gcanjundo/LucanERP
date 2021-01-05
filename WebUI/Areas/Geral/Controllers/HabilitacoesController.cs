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
    public class HabilitacoesController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public HabilitacoesController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
        public ActionResult CreateHabilitacoes()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateHabilitacoes([Bind] HabilitacoesDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                HabilitacoesRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateHabilitacoes");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateHabilitacoes(int? id, [Bind] HabilitacoesDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateHabilitacoes([Bind] HabilitacoesDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                HabilitacoesRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateHabilitacoes");
            }
            return View(dto);
        }
        public ActionResult DeleteHabilitacoes(HabilitacoesDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            HabilitacoesRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteHabilitacoes");

        }
        public IActionResult ListHabilitacoes(HabilitacoesDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(HabilitacoesRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(HabilitacoesDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(HabilitacoesRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaHabilitacoes(HabilitacoesDTO dto)
        {
            IEnumerable<HabilitacoesDTO> lista = HabilitacoesRN.GetInstance().ObterPorFiltro(dto);
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