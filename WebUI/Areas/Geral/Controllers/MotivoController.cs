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
    public class MotivoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public MotivoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
        public ActionResult CreateMotivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMotivo([Bind] MotivoDTO dto)
        {
            if (ModelState.IsValid)
            {
                MotivoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateMotivo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateMotivo(int? id, [Bind] MotivoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateMotivo([Bind] MotivoDTO dto)
        {
            if (ModelState.IsValid)
            {
                MotivoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateMotivo");
            }
            return View(dto);
        }
        public ActionResult DeleteMotivo(MotivoDTO dto)
        {
            MotivoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMotivo");

        }
        public IActionResult ListMotivo(MotivoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MotivoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(MotivoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MotivoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaMotivo(MotivoDTO dto)
        {
            IEnumerable<MotivoDTO> lista = MotivoRN.GetInstance().ObterPorFiltro(dto);
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